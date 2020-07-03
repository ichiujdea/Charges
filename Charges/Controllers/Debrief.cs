using Charges.Models;
using Newtonsoft.Json;
using RightNow.AddIns.AddInViews;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Charges.Controllers
{
    class Debrief
    {
        public List<ChargeLines> charges = new List<ChargeLines>();
        private Views.ChargesUI chargesUI;
        private IIncident incident;
        private IOrganization org;
        private int firstLine;
        private IGlobalContext _globalContext;
        private IRecordContext _recordContext;
        private enum TravelTime
        {
            DEV = 83,
            TST = 36,
            PROD = 36
        }
        private enum SystemLabor
        {
            DEV = 122,
            TST = 34,
            PROD = 34
        }
        private enum ClientDamageLabor
        {
            DEV = 123,
            TST = 24,
            PROD = 24
        }
        private enum DishLabor
        {
            DEV = 124,
            TST = 26,
            PROD = 26
        }
        private enum InstallLabor
        {
            DEV = 121,
            TST = 29,
            PROD = 29
        }
        private enum SiteSurveyLabor
        {
            DEV = 125,
            TST = 32,
            PROD = 32
        }
        private enum DTOCBillableLabor
        {
            DEV = 126,
            TST = 27,
            PROD = 27
        }
        private enum DTOCWarrantyLabor
        {
            DEV = 127,
            TST = 28,
            PROD = 28
        }
        private enum SourceLabor
        {
            DEV = 127,
            TST = 33,
            PROD = 33
        }
        private enum TechSalesLabor
        {
            DEV = 128,
            TST = 35,
            PROD = 35
        }


        public Debrief(IGlobalContext globalContext, IRecordContext recordContext, Views.ChargesUI _chargesUI, WorkspaceChargesAddIn workspaceAddIn)
        {
            // Get our global and record vars
            _globalContext = globalContext;
            _recordContext = recordContext;
            chargesUI = _chargesUI;

            // We're going to be working a lot with the org and the incident, so grabbing them now. 
            incident = workspaceAddIn.ChargeIncident;
            org = workspaceAddIn.ChargeOrganization;

            _chargesUI.CheckToken();
            // Get Current number of charges to populate the beginning line number that debrief starts on.
            firstLine = _chargesUI.chargeLines.Count + 1;

            // Now that we have our defaults for creating charge lines, check for a value in each of the labor/travel time custom fields and process them
            processNonInventory(workspaceAddIn);

            // Then Process inventory based charge lines. 
            processInventory(workspaceAddIn);

            // Finally, get entitlements for each line
            for (int index = 0; index < charges.Count; ++index)
            {
                charges[index] = _chargesUI.GetEntitlements(charges[index]);
            }

        }

        private void processInventory(WorkspaceChargesAddIn workspaceAddIn)
        {
            OFSCInstall installDetail = new OFSCInstall();
            OFSCDeinstall deinstallDetail = new OFSCDeinstall();
            // Check to see if we have a JSON in Install/Deinstall
            foreach (ICustomAttribute ca in incident.CustomAttributes)
            {    
                var gf = ca.GenericField;
                if (gf.Name == "Integration$OFSC_Install_Data")
                {
                    if (gf.DataValue.Value != null)
                    {
                        installDetail = JsonConvert.DeserializeObject<OFSCInstall>(gf.DataValue.Value.ToString());
                    }
                    else
                    {
                        installDetail = null;
                    }
                }
                if (gf.Name == "Integration$OFSC_Deinstall_Data")
                {
                    if (gf.DataValue.Value != null)
                    {
                        deinstallDetail = JsonConvert.DeserializeObject<OFSCDeinstall>(gf.DataValue.Value.ToString());
                    }
                    else
                    {
                        deinstallDetail = null;
                    }
                }
            }

            // Process Install Details
            if (installDetail != null)
            {
                if (installDetail.items == null)
                {
                    return;
                }
                foreach (OFSCInstallItem item in installDetail.items)
                {
                    ChargeLines line = new ChargeLines();
                    line = ChargeLines.populateDefaultLine(line, chargesUI, workspaceAddIn);
                    string[] partNumbers = item.dmx_equipment_part_num.Split('|');
                    // If present, get Item from Product Catalog
                    if (partNumbers[0] != null)
                    {
                        ReportResults products = WorkspaceChargesAddIn.DebriefProductSearch(partNumbers[0], _globalContext);
                        // If we got one (and only one) result, add it to the line, otherwise, toss error for part number issues and continue to next row
                        if (products.rows.Count == 1)
                        {
                            int itemID = 0;
                            int.TryParse(products.rows[0][1], out itemID);
                            line.ItemID = itemID;
                            line.ItemNumber = products.rows[0][0];
                            line.Item = products.rows[0][6];
                            line.UOM = products.rows[0][3];
                        }
                        else
                        {
                            MessageBox.Show($"Item Number: {partNumbers[0]} not found (or found more than once). Unable to add.");
                            continue;
                        }
                    }
                        
                    // If present, get IB from product Catalog
                    if (partNumbers.Length > 1)
                    {
                        ReportResults results = WorkspaceChargesAddIn.GetInstallBase(_recordContext, _globalContext);
                        // Find the IB ID in the IBS
                        foreach (List<string> row in results.rows)
                        {
                            int instanceID = 0;
                            int.TryParse(row[5], out instanceID);
                            // Only interested in the IB row that matches the one in question
                            if (row[5] == partNumbers[1])
                            {
                                // 0 - "OSVC Item ID", 1 - "Product", 2 - "Category", 3 - "Serial", 4 - "EBS Instance ID", 5 - "EBS Item Number"
                                // Item Number (IB), Instance Name, Item Instance ID, Serial Number
                                line.ItemInstance = row[6];
                                line.ItemInstanceName = row[1];
                                line.ItemInstanceID = instanceID;
                                line.SerialNumber = row[4];
                            }
                        }
                    }

                    // Populate SA based on Event Type and Business Process
                    if (workspaceAddIn.BusinessProcessValue == "DMX Customer Support")
                    {
                        line.ServiceActivity = "Service Parts";
                    }
                    else if (workspaceAddIn.BusinessProcessValue == "DTOC Customer Support")
                    {
                        line.ServiceActivity = "DTOC Dispatch Use";
                    }
                    else if (workspaceAddIn.BusinessProcessValue == "DMX Installation")
                    {
                        line.ServiceActivity = "Install Support";
                    }
                    else if (workspaceAddIn.BusinessProcessValue == "Tech Sales")
                    {
                        line.ServiceActivity = "Tech Sales - Field";
                    }
                    else
                    {
                        // Event type not right to determine SA. Inform user.
                        MessageBox.Show($"Unable to determine Service Activity for Item {partNumbers[0]}, please select the appropriate Service Activity.");
                    }
                    line.Quantity = item.quantity;
                    line.ChargeLine = firstLine + charges.Count;
                    line.ChargeSource = "OFSC";
                    charges.Add(line);
                }
            }
            if (deinstallDetail != null)
            { 
                if (deinstallDetail.items == null)
                {
                    return;
                }
                foreach (OFSCDeinstallItem item in deinstallDetail.items)
                {
                    ChargeLines line = new ChargeLines();
                    line = ChargeLines.populateDefaultLine(line, chargesUI, workspaceAddIn);
                    string[] partNumbers = item.dmx_equipment_part_num.Split('|');
                    // If present, get Item from Product Catalog
                    if (partNumbers[0] != null)
                    {
                        ReportResults products = WorkspaceChargesAddIn.DebriefProductSearch(partNumbers[0], _globalContext);
                        // If we got one (and only one) result, add it to the line, otherwise, toss error for part number issues and continue to next row
                        if (products.rows.Count == 1)
                        {
                            int itemID = 0;
                            int.TryParse(products.rows[0][1], out itemID);
                            line.ItemID = itemID;
                            line.ItemNumber = products.rows[0][0];
                            line.Item = products.rows[0][6];
                            line.UOM = products.rows[0][3];
                        }
                        else
                        {
                            MessageBox.Show($"Item Number: {partNumbers[0]} not found (or found more than once). Unable to add.");
                            continue;
                        }
                    }

                    // If present, get IB from product Catalog
                    if (partNumbers.Length > 1)
                    {
                        ReportResults results = WorkspaceChargesAddIn.GetInstallBase(_recordContext, _globalContext);
                        // Find the IB ID in the IBS
                        foreach (List<string> row in results.rows)
                        {
                            int instanceID = 0;
                            int.TryParse(row[5], out instanceID);
                            // Only interested in the IB row that matches the one in question
                            if (row[5] == partNumbers[1])
                            {
                                // 0 - "OSVC Item ID", 1 - "Product", 2 - "Category", 3 - "Serial", 4 - "EBS Instance ID", 5 - "EBS Item Number"
                                // Item Number (IB), Instance Name, Item Instance ID, Serial Number
                                line.ItemInstance = row[6];
                                line.ItemInstanceName = row[1];
                                line.ItemInstanceID = instanceID;
                                line.SerialNumber = row[4];
                            }
                        }
                    }
                    
                    // Populate Service Activity
                    if (workspaceAddIn.BusinessProcessValue == "DMX Customer Support")
                    {
                        line.ServiceActivity = "Rental Debrief Receive";
                    }
                    else if (workspaceAddIn.BusinessProcessValue == "DTOC Customer Support")
                    {
                        line.ServiceActivity = "DTOC Dispatch Receive";
                    }
                    else if (workspaceAddIn.BusinessProcessValue == "DMX Installation")
                    {
                        line.ServiceActivity = "Install Support";
                    }
                    else if (workspaceAddIn.BusinessProcessValue == "Tech Sales")
                    {
                        line.ServiceActivity = "Tech Sales Return";
                    }
                    else
                    {
                        // Event type not right to determine SA. Inform user.
                        MessageBox.Show($"Unable to determine Service Activity for Item {partNumbers[0]}, please select the appropriate Service Activity.");
                    }
                    // Make quantity negative in cases of deinstall
                    line.Quantity = item.quantity * -1;
                    line.ChargeLine = firstLine + charges.Count;
                    line.ChargeSource = "OFSC";
                    charges.Add(line);
                }
            }
        }

        private void processNonInventory(WorkspaceChargesAddIn workspaceAddIn)
        {
            // The if blocks below look to see if the custom field for each of our data types have a value and if so, set a charge line up for them. 
            if (float.TryParse(WorkspaceChargesAddIn.getCFValue(incident, (int)System.Enum.Parse(typeof(TravelTime), workspaceAddIn.environment)), out float travel))
            {
                if (travel == 0.0)
                {

                }
                else
                {
                    // DMX Installation cannot have travel time. If this node is travel time and Business Process is DMX Installation, skip
                    if (workspaceAddIn.BusinessProcessValue != "DMX Installation")
                    {
                        ChargeLines line = new ChargeLines();
                        line = ChargeLines.populateDefaultLine(line, chargesUI, workspaceAddIn);
                        line.ChargeSource = "OFSC";
                        // Per Ravi, do not auto Populate Service Activity
                        line.ServiceActivity = "Travel Time";
                        line.Quantity = travel;
                        line.ChargeLine = firstLine + charges.Count;
                        line.ItemID = 37203;
                        line.ItemNumber = "LABOR SERVICE STD RATE (US) INTERNAL";
                        charges.Add(line);
                    }
                }
            }
            if (float.TryParse(WorkspaceChargesAddIn.getCFValue(incident, (int)System.Enum.Parse(typeof(SystemLabor), workspaceAddIn.environment)), out float system))
            {
                if (system == 0.0)
                {

                }
                else
                {
                    ChargeLines line = new ChargeLines();
                    line = ChargeLines.populateDefaultLine(line, chargesUI, workspaceAddIn);
                    line.ChargeSource = "OFSC";
                    line.ServiceActivity = "System Labor";
                    line.Quantity = system;
                    line.ChargeLine = firstLine + charges.Count;
                    line.ItemID = 37203;
                    line.ItemNumber = "LABOR SERVICE STD RATE (US) INTERNAL";
                    charges.Add(line);
                }
            }
            if (float.TryParse(WorkspaceChargesAddIn.getCFValue(incident, (int)System.Enum.Parse(typeof(ClientDamageLabor), workspaceAddIn.environment)), out float clntDamageLabor))
            {
                if (clntDamageLabor == 0.0)
                {

                }
                else
                {
                    ChargeLines line = new ChargeLines();
                    line = ChargeLines.populateDefaultLine(line, chargesUI, workspaceAddIn);
                    line.ChargeSource = "OFSC";
                    line.ServiceActivity = "Client Damage Labor";
                    line.Quantity = clntDamageLabor;
                    line.ChargeLine = firstLine + charges.Count;
                    line.ItemID = 37203;
                    line.ItemNumber = "LABOR SERVICE STD RATE (US) INTERNAL";

                    charges.Add(line);
                }
            }
            if (float.TryParse(WorkspaceChargesAddIn.getCFValue(incident, (int)System.Enum.Parse(typeof(DishLabor), workspaceAddIn.environment)), out float dshLabor))
            {
                if (dshLabor == 0.0)
                {

                }
                else
                {
                    ChargeLines line = new ChargeLines();
                    line = ChargeLines.populateDefaultLine(line, chargesUI, workspaceAddIn);
                    line.ChargeSource = "OFSC";
                    line.ServiceActivity = "Dish Realignment Labor";
                    line.Quantity = dshLabor;
                    line.ChargeLine = firstLine + charges.Count;
                    line.ItemID = 37203;
                    line.ItemNumber = "LABOR SERVICE STD RATE (US) INTERNAL";

                    charges.Add(line);
                }
            }
            if (float.TryParse(WorkspaceChargesAddIn.getCFValue(incident, (int)System.Enum.Parse(typeof(InstallLabor), workspaceAddIn.environment)), out float instllLabor))
            {
                if (instllLabor == 0.0)
                {

                }
                else
                {
                    ChargeLines line = new ChargeLines();
                    line = ChargeLines.populateDefaultLine(line, chargesUI, workspaceAddIn);
                    line.ChargeSource = "OFSC";
                    line.Quantity = instllLabor;
                    line.ChargeLine = firstLine + charges.Count;
                    line.ItemID = 37203;
                    line.ItemNumber = "LABOR INSTALL INTERNAL TECH (US)";
                    if (workspaceAddIn.BusinessProcessValue == "DMX Installation")
                    {
                        line.ServiceActivity = "Install Support";
                    }
                    else
                    {
                        line.ServiceActivity = "Tech Sales - Field";
                    }

                    charges.Add(line);
                }
            }
            if (float.TryParse(WorkspaceChargesAddIn.getCFValue(incident, (int)System.Enum.Parse(typeof(SiteSurveyLabor), workspaceAddIn.environment)), out float siteSrvyLabor))
            {
                if (siteSrvyLabor == 0.0)
                {

                }
                else
                {
                    ChargeLines line = new ChargeLines();
                    line = ChargeLines.populateDefaultLine(line, chargesUI, workspaceAddIn);
                    line.ChargeSource = "OFSC";
                    line.ServiceActivity = "LABOR SITE SURVEY (US) INTERNAL TECH";
                    line.Quantity = siteSrvyLabor;
                    line.ChargeLine = firstLine + charges.Count;
                    line.ItemID = 164017;
                    line.ItemNumber = "LABOR SITE SURVEY (US) INTERNAL TECH";

                    charges.Add(line);
                }
            }
            if (float.TryParse(WorkspaceChargesAddIn.getCFValue(incident, (int)System.Enum.Parse(typeof(DTOCBillableLabor), workspaceAddIn.environment)), out float dtocBillLabor))
            {
                if (dtocBillLabor == 0.0)
                {

                }
                else
                {
                    ChargeLines line = new ChargeLines();
                    line = ChargeLines.populateDefaultLine(line, chargesUI, workspaceAddIn);
                    line.ChargeSource = "OFSC";
                    line.ServiceActivity = "DTOC Billable Labor";
                    line.Quantity = dtocBillLabor;
                    line.ChargeLine = firstLine + charges.Count;
                    line.ItemID = 37203;
                    line.ItemNumber = "LABOR SERVICE STD RATE (US) INTERNAL";

                    charges.Add(line);
                }
            }
            if (float.TryParse(WorkspaceChargesAddIn.getCFValue(incident, (int)System.Enum.Parse(typeof(DTOCWarrantyLabor), workspaceAddIn.environment)), out float dtocWarrLabor))
            {
                if (dtocWarrLabor == 0.0)
                {

                }
                else
                {
                    ChargeLines line = new ChargeLines();
                    line = ChargeLines.populateDefaultLine(line, chargesUI, workspaceAddIn);
                    line.ChargeSource = "OFSC";
                    line.ServiceActivity = "DTOC Warranty Labor";
                    line.Quantity = dtocWarrLabor;
                    line.ChargeLine = firstLine + charges.Count;
                    line.ItemID = 37203;
                    line.ItemNumber = "LABOR SERVICE STD RATE (US) INTERNAL";

                    charges.Add(line);
                }
            }
            if (float.TryParse(WorkspaceChargesAddIn.getCFValue(incident, (int)System.Enum.Parse(typeof(TechSalesLabor), workspaceAddIn.environment)), out float techLabor))
            {
                if (techLabor == 0.0)
                {

                }
                else
                {
                    ChargeLines line = new ChargeLines();
                    line = ChargeLines.populateDefaultLine(line, chargesUI, workspaceAddIn);
                    line.ChargeSource = "OFSC";
                    line.ServiceActivity = "Tech Sales - Field";
                    line.Quantity = techLabor;
                    line.ChargeLine = firstLine + charges.Count;

                    charges.Add(line);
                }
            }
            if (float.TryParse(WorkspaceChargesAddIn.getCFValue(incident, (int)System.Enum.Parse(typeof(SourceLabor), workspaceAddIn.environment)), out float srcLabor))
            {
                if (srcLabor == 0.0)
                {

                }
                else
                {
                    ChargeLines line = new ChargeLines();
                    line = ChargeLines.populateDefaultLine(line, chargesUI, workspaceAddIn);
                    line.ChargeSource = "OFSC";
                    line.ServiceActivity = "Source Labor";
                    line.Quantity = srcLabor;
                    line.ChargeLine = firstLine + charges.Count;
                    line.ItemID = 37203;
                    line.ItemNumber = "LABOR SERVICE STD RATE (US) INTERNAL";

                    charges.Add(line);
                }
            }
        }
    }
}
