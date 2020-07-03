using System;
using System.AddIn;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Charges.Controllers;
using Charges.Models;
using Charges.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RightNow.AddIns.AddInViews;
using RightNow.AddIns.Common;
using System.Diagnostics;

////////////////////////////////////////////////////////////////////////////////
//
// File: WorkspaceAddIn.cs
//
// Comments: Entry point for the charges addin
//
// Notes: This file controls a lot of the functional calls to OSVC and EBS with visuals mostly happening in the veiw files. 
//
// Pre-Conditions: 
//
////////////////////////////////////////////////////////////////////////////////
namespace Charges
{
    public class WorkspaceChargesAddIn : Panel, IWorkspaceComponent2
    {
        /// <summary>
        /// The current workspace record context.
        /// </summary>
        private IRecordContext _recordContext;
        private ChargesUI _chargesUI;
        private IGlobalContext _globalContext;

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
        public enum EBSAccountNumber
        {
            DEV = 34,
            TST = 9,
            PROD = 9
        }
        public enum BusinessProcess
        {
            DEV = 150,
            TST = 126,
            PROD = 126
        }
        public enum IncidentType
        {
            DEV = 1,
            TST = 1,
            PROD = 1
        }
        public enum EBSAccountID
        {
            DEV = 49,
            TST = 92,
            PROD = 92
        }
        public enum StatusFSComplete
        {
            // Test is 114. Moving to 117 to test other functions
            DEV = 144,
            TST = 114,
            PROD = 114
        }
        public enum DefaultShippingMethod
        {
            DEV = 113,
            TST = 113,
            PROD = 113
        }
        public List<string> EntitlementNotRequired;
        public int incident_ID;
        public string incident_number;
        public int? incident_type_ID;
        public int? incident_category_ID;
        public int? parent_incident_id;
        public string parent_incident_number;
        public string environment;
        public static string EBSEnv;
        public string OperatingUnit;
        public string BusinessProcessValue;
        public string DefaultShippingValue;
        public int SiteID;
        public string OSVCUser;
        public string AccountName;
        public string PONumber;
        public string AccountNumber;
        public int? AccountID;
        public BillTo shipTo;
        public BillTo billTo;
        public IOrganization org;
        public List<KeyValuePair<int, string>> incidentCustomFields;
        public static string EBSRoot;
        public static string EBSTokenUN;
        public static string EBSTokenPW;
        public IIncident ChargeIncident;
        public IOrganization ChargeOrganization;

        
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="inDesignMode">Flag which indicates if the control is being drawn on the Workspace Designer. (Use this flag to determine if code should perform any logic on the workspace record)</param>
        /// <param name="RecordContext">The current workspace record context.</param>
        public WorkspaceChargesAddIn(bool inDesignMode, IRecordContext RecordContext, IGlobalContext GlobalContext, string _EBSRoot, string _EBSTokenUN, String _EBSTokenPW)
        {
            _chargesUI = new ChargesUI(_recordContext, _globalContext, this);
            
            if (!inDesignMode)
            {
                _recordContext = RecordContext;
                _globalContext = GlobalContext;

                environment = GetEnvironment(_globalContext);

                EBSRoot = _EBSRoot;
                EBSTokenUN = _EBSTokenUN;
                EBSTokenPW = _EBSTokenPW;

                // Set EBS endpoint fragment to be based on env
                if (environment == "DEV")
                {
                    EBSEnv = "mooa";
                }else if (environment == "TST")
                {
                    EBSEnv = "moot";
                }
                else
                {
                    EBSEnv = "moop";
                }

                // Charges only should work in the context of an Incident. If we aren't in an incident, do not continue. If we are, Populate Incident attributes to charges UI
                _recordContext.DataLoaded += (sender, args) =>
                {
                    if (_recordContext.WorkspaceType == RightNow.AddIns.Common.WorkspaceRecordType.Incident)
                    {
                        ChargeIncident = _recordContext.GetWorkspaceRecord(RightNow.AddIns.Common.WorkspaceRecordType.Incident) as IIncident;
                        // Get all incident custom fields
                        // incidentCustomFields = populateIncidentCustomFields(incident);

                        ChargeOrganization = _recordContext.GetWorkspaceRecord(RightNow.AddIns.Common.WorkspaceRecordType.Organization) as IOrganization;

                        // Get an initial token. This will be checked on future WS calls to EBS. 
                        Token token = GetEBSToken(_globalContext);
                        _chargesUI.EBSToken = token.access_token;
                        _chargesUI.TokenExpiry = DateTime.Now.AddSeconds(token.expires_in);

                        // Populate our global values for charges to use. 
                        incident_ID = ChargeIncident.ID;
                        incident_number = ChargeIncident.RefNo.ToString();
                        incident_type_ID = WorkspaceChargesAddIn.getCFID(ChargeIncident, (int)System.Enum.Parse(typeof(IncidentType), environment));
                        OperatingUnit = getOU(_recordContext, _globalContext, ChargeOrganization);
                        AccountNumber = getCFValue(ChargeOrganization, (int)System.Enum.Parse(typeof(EBSAccountNumber), environment));
                        incident_category_ID = ChargeIncident.CategoryID;
                        parent_incident_id = getParentIncidentID(ChargeIncident);
                        parent_incident_number = getParentIncidentNumber(parent_incident_id);
                        SiteID = getEBSSiteID(_recordContext, ChargeOrganization);
                        BusinessProcessValue = getCFValue(ChargeIncident, (int)System.Enum.Parse(typeof(BusinessProcess), environment));
                        DefaultShippingValue = getCFValue(ChargeOrganization, (int)System.Enum.Parse(typeof(DefaultShippingMethod), environment));
                        AccountName = ChargeOrganization.Name;
                        PONumber = getCAValue(ChargeIncident, "MoodCustom$PO_Number");
                        AccountID = getCFID(ChargeOrganization, (int)System.Enum.Parse(typeof(EBSAccountID), environment));
                        OSVCUser = _globalContext.Login;
                        
                        shipTo = new BillTo();
                        foreach (IOrgAddr address in ChargeOrganization.Oaddr)
                        {
                            if (address.AddrStreet != null)
                            {
                                shipTo.address1 = address.AddrStreet;
                                shipTo.city = address.AddrCity != null ? address.AddrCity : "";
                                shipTo.state = address.AddrProvID != null ? address.AddrProvID.Value.ToString() : "";
                                shipTo.postal_code = address.AddrPostalCode != null ? address.AddrPostalCode : "";
                                shipTo.customer_name = ChargeOrganization.Name;
                                shipTo.cust_account_id = AccountID;
                                shipTo.cust_acct_site_id = getEBSSiteID(_recordContext, ChargeOrganization);
                                shipTo.account_number = AccountNumber;
                                break;
                           }
                        }
                        
                        // Retrieve Cached charges to prepopulate Charges UI Grid
                        _chargesUI.CachedCharges = GetChargesFromIncident(_recordContext, ChargeIncident);
                        if (_chargesUI.CachedCharges.lines != null)
                        {
                            _chargesUI.PopulateCharges(_chargesUI.CachedCharges.lines, ChargeIncident);
                            // Populate header data
                            _chargesUI.ShipToContact.Text = _chargesUI.CachedCharges.header.ShipToContact;
                            _chargesUI.ShipToPhone.Text = _chargesUI.CachedCharges.header.ShipToPhone;
                            _chargesUI.PONumber.Text = _chargesUI.CachedCharges.header.PurchaseOrderNumber;
                            _chargesUI.EstimateValue.Text = _chargesUI.CachedCharges.header.EstimatedTotal.ToString();
                            _chargesUI.ActualValue.Text = _chargesUI.CachedCharges.header.ActualTotal.ToString();
                            _chargesUI.TotalValue.Text = _chargesUI.CachedCharges.header.TotalTotal.ToString();
                        }

                        // Populate PO number from Incident only if currently blank in charges
                        if (_chargesUI.PONumber.Text == null || _chargesUI.PONumber.Text == "")
                        {
                            _chargesUI.PONumber.Text = PONumber;
                        }

                        // Handle for process debrief
                        if (ChargeIncident.Status.StatusID == 1 || ChargeIncident.Status.StatusID == (int)System.Enum.Parse(typeof(StatusFSComplete), environment))
                        {
                            // Only enable button if charges havent been processed already
                            string debriefProcessed = getCAValue(ChargeIncident, "Integration$Debrief_Processed");

                            if (debriefProcessed == "True")
                            {
                                return;
                            }
                            else
                            {
                                _chargesUI.processChargesBtn.Enabled = true;
                            }
                            
                        }
                    }
                };

                _chargesUI._gContext = _globalContext;
                _chargesUI._rContext = _recordContext;
                
                _chargesUI.Refresh();
                _chargesUI.Visible = true;

                RecordContext.Saving += new CancelEventHandler(this.SaveCharges);
            }
        }

        /// <summary>
        /// Retreives if we're in Dev Test or Prod for selecting the correct CF IDs and other processes
        /// </summary>
        /// <param name="globalContext"></param>
        /// <returns>product environmnet we're in (DEV/TST/PROD)</returns>
        public static string GetEnvironment(IGlobalContext globalContext)
        {
            string env = "DEV";

            string url = globalContext.InterfaceURL;
            string[] urlParts = url.Split('/');
            if (urlParts[2] == "moodmedia--tst1.custhelp.com" || urlParts[2] == "mmint--tst1.custhelp.com")
            {
                env = "DEV";
            }
            else if (urlParts[2] == "moodmedia--tst2.custhelp.com" || urlParts[2] == "mmint--tst2.custhelp.com")
            {
                env = "TST";
            }else if(urlParts[2] == "moodmedia.custhelp.com" || urlParts[2] == "mmint.custhelp.com")
            {
                env = "PROD";
            }

            return env;
        }

        /// <summary>
        /// When a user closes the record we need to save the charges in the state they're in. Take the charge lines object, 
        /// serialize it to JSON and save to the record custom field for retrieval later. Do NOT do this if charges disabled for any reason.
        /// </summary>
        /// <param name="sender">Workspace Object</param>
        /// <param name="e">Cancel Event Arguments</param>
        private void SaveCharges(Object sender, CancelEventArgs e)
        {
            // If charge lines length is zero, bail on saving
            if (_chargesUI.chargeLines.Count < 1)
            {
                return;
            }

            // If Charges Grid Disabled, we have encountered an error. Do not save chargeLines
            if (_chargesUI.ChargeLinesGrid.ReadOnly == true)
            {
                return;
            }

            decimal actualTotal = 0;
            decimal estimateTotal = 0;
            decimal totalTotal = 0;

            // Serialize Charge Lines
            _chargesUI._charges.lines = _chargesUI.chargeLines.ToList();
            _chargesUI._charges.header = new ChargeHeader();
            if (Decimal.TryParse(_chargesUI.EstimateValue.Text, out estimateTotal))
                _chargesUI._charges.header.EstimatedTotal = estimateTotal;
            if (Decimal.TryParse(_chargesUI.ActualValue.Text, out actualTotal))
                _chargesUI._charges.header.ActualTotal = actualTotal;
            if (Decimal.TryParse(_chargesUI.TotalValue.Text, out totalTotal))
                _chargesUI._charges.header.TotalTotal = totalTotal;
            _chargesUI._charges.header.PurchaseOrderNumber = _chargesUI.PONumber.Text;
            _chargesUI._charges.header.ShipToContact = _chargesUI.ShipToContact.Text;
            _chargesUI._charges.header.ShipToPhone = _chargesUI.ShipToPhone.Text;

            string jsonCharges = JsonConvert.SerializeObject(_chargesUI._charges);

            // Save JsonCharges to Incident Custom Field
            foreach (ICustomAttribute ca in ChargeIncident.CustomAttributes)
            {
                var gf = ca.GenericField;
                if (gf.Name == "Integration$Unsubmitted_Charges")
                {
                    gf.DataValue.Value = jsonCharges;
                }
            }

            //e.Cancel = true;
        }

        /// <summary>
        /// Checks the in context incident for details in the Unsubbed charges variable and returns the contents of that custom attribute
        /// </summary>
        /// <param name="_recordContext">Incident Record</param>
        /// <returns>List of Chargelines for ChargesGrid to populate</returns>
        internal static ChargesObject GetChargesFromIncident(IRecordContext _recordContext, IIncident incident)
        {
            ChargesObject charges = new ChargesObject();

            foreach (ICustomAttribute ca in incident.CustomAttributes)
            {
                var gf = ca.GenericField;
                if (gf.Name == "Integration$Unsubmitted_Charges")
                {
                    if (gf.DataValue.Value != null)
                    {
                        var settings = new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore
                        };
                        charges = JsonConvert.DeserializeObject<ChargesObject>(gf.DataValue.Value.ToString(), settings);
                    }
                }
            }

            return charges;
        }

        /// <summary>
        /// This WS call gets the data for the various dropdowns used in the ChargesUI form. They are added programmatically after retrieval.
        /// Updated WS Call to new format needed to get full payloads. 
        /// </summary>
        /// <param name="token">EBS Authentication Token</param>
        /// <param name="incType">Incident Type. Needs to be passed as a parameter to get the right values for the dropdowns.</param>
        /// <returns>List of Dropdown values for each type of dropdown.</returns>
        public static LovList GetLovsFromEBS(string token, string busProc, IGlobalContext _globalContext, string ebsEnv, long custAccountID)
        {
            LovList lovs = new LovList();
            RestClient getLOVs = new RestClient();
            getLOVs.endPoint = $"{EBSRoot}oic/lovs/";
            getLOVs.ebsBearerToken = token;
            getLOVs.jsonBody = "{\"lovs\":{\"business_process\": \"" + busProc + "\", \"cust_acct_site_id\": \"" + custAccountID + "\"}}";
            getLOVs.httpMethod = httpVerb.POST;

            Debug.WriteLine(">>> WorkspaceChargesAddIn.GetLovsFromEBS endPoint = " + getLOVs.endPoint);
            Debug.WriteLine(">>> WorkspaceChargesAddIn.GetLovsFromEBS endPoint = " + getLOVs.jsonBody);
            try
            {
                string strResponse = getLOVs.makeRequest();
                lovs = JsonConvert.DeserializeObject<LovList>(strResponse, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                Debug.WriteLine(">>> WorkspaceChargesAddIn.GetLovsFromEBS strResponse = " + strResponse);
                Debug.WriteLine(">>> WorkspaceChargesAddIn.GetLovsFromEBS lovs = " + lovs);
                // If we get a status of E, log that problem
                if (lovs.overall_status == "E")
                {
                    _globalContext.LogMessage($"Problem getting List of values for {busProc}: " + lovs.overall_diagnostics);
                }
            }
            catch (Exception e)
            {
                _globalContext.LogMessage(e.Message);
            }

            // We're expecting specific nodes in a get LOV call. If we don't get them, lock charges and report an error. 
            if (lovs.lovs.Count != 6)
            {
                MessageBox.Show("Unexpected Data Returned from Get LOV. Contact IT.");
                _globalContext.LogMessage(lovs.lovs.ToString());
                return lovs;
            }

            // LOV node 4 (now) is Service Activity. The values in this list need to be split on | and the first half becomes the LOV while the second becomes the list of options that doesn't require an entitlement
            lovs.EntitlementNotRequired = new List<string>();
            for (int index = 0; index < lovs.lovs[4].lov_values.Count; ++index)
            {
                string[] valuePair = lovs.lovs[4].lov_values[index].Split('|');
                if (valuePair[1] == "Y")
                {
                    // Add to the list and strip the value
                    lovs.EntitlementNotRequired.Add(valuePair[0]);
                    lovs.lovs[4].lov_values[index] = valuePair[0];
                }
                else
                {
                    // Strip the value
                    lovs.lovs[4].lov_values[index] = valuePair[0];
                }
            }

            return lovs;
        }

        /// <summary>
        /// Calls the EBS Server to get a token to make future entitlements calls
        /// </summary>
        /// <param name="_gContext">OSVC Global Context</param>
        /// <returns>Token Object deserialized from JSON response</returns>
        public static Token GetEBSToken(IGlobalContext _gContext)
        {
            Token token = new Models.Token();

            RestClient getEBSToken = new RestClient();
            getEBSToken.endPoint = $"{EBSRoot}oauth/token";

            Debug.WriteLine(">>> WorkspaceChargesAddIn.GetEBSToken endPoint = " + getEBSToken.endPoint);

            String tokenAuth = EBSTokenUN + ":" + EBSTokenPW;
            var authBytes = Encoding.UTF8.GetBytes(tokenAuth);
            tokenAuth = Convert.ToBase64String(authBytes);

            getEBSToken.httpMethod = httpVerb.POST;
            getEBSToken.ebsAuthToken = tokenAuth;

            var postData = "grant_type=client_credentials";
            var data = Encoding.ASCII.GetBytes(postData);
            getEBSToken.formData = data;

            try
            {
                string strResponse = getEBSToken.makeRequest();
                token = JsonConvert.DeserializeObject<Token>(strResponse, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                Debug.WriteLine(">>> WorkspaceChargesAddIn.GetEBSToken success.token = " + token);
            }
            catch (Exception e)
            {
                MessageBox.Show("Unable to retrieve a token to communicate with EBS. Please contact IT to resolve. Error: " + e.Message);
                _gContext.LogMessage(e.Message);
            }

            return token;
        }
        
        /// <summary>
        /// Given an Item Number and/or an instance ID, returns the pricing for the given account based on the activity type
        /// </summary>
        /// <param name="instanceID">Install Base Instance ID</param>
        /// <param name="itemNumber">Item List Item Number</param>
        /// <param name="serviceActivity">Selected Service Activity to get pricing for</param>
        /// <param name="accountID">Account ID we're retrieving Pricing for.</param>
        /// <returns></returns>
        internal static Pricing GetPricing(string token, int? instanceID, string itemNumber, string serviceActivity, int? accountID, IGlobalContext _globalContext, string businessProc, string priceList, long custAccountID)
        {
            Pricing price = new Models.Pricing();

            // Construct Charges JSON
            RestClient getPricing = new RestClient();
            getPricing.endPoint = $"{EBSRoot}oic/entitlements/";
            getPricing.ebsBearerToken = token;
            getPricing.jsonBody = "{\"entitlement\":{\"item_number\": \"" + itemNumber + "\", \"business_process\": \"" + businessProc + "\",\"service_activity\": \"" + serviceActivity + "\",\"cust_account_id\": " + accountID + ", \"price_list\": \"" + priceList + "\", \"cust_acct_site_id\": " + custAccountID;
            //getPricing.jsonBody = "{\"entitlement\":{\"item_number\": \"" + itemNumber + "\", \"business_process\": \"" + businessProc + "\",\"service_activity\": \"" + serviceActivity + "\",\"cust_account_id\": " + accountID;
            if (instanceID != 0 && instanceID != null)
            {
                getPricing.jsonBody += ",\"instance_id\": " + instanceID;
            }
            getPricing.jsonBody += "}}";
            getPricing.httpMethod = httpVerb.POST;

            try
            {
                string strResponse = getPricing.makeRequest();
                price = JsonConvert.DeserializeObject<Pricing>(strResponse, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                if (price.return_status == "E")
                {
                    MessageBox.Show("Unable to retrieve entitlements for this line. Please contact IT to resolve. Error: " + price.error_message);
                    Debug.WriteLine(">>> WorkspaceChargesAddIn.GetPricing strResponse = " + strResponse);
                    _globalContext.LogMessage($"Unable to get Entitlements. Account:{accountID}, Item Number: {itemNumber}, Instance ID: {instanceID}, Error: {price.error_message}");
                }
            }
           catch (Exception e)
            {
                MessageBox.Show("Unable to retrieve entitlements for this line. Please contact IT to resolve. Error: " + e.Message);
                Debug.WriteLine(">>> WorkspaceChargesAddIn.GetPricing error = " + e.Message);
                _globalContext.LogMessage(e.Message);
            }

            return price;
        }

        /// <summary>
        /// Gets the EBS site ID from the account record. 
        /// </summary>
        /// <param name="recordContext"></param>
        /// <returns></returns>
        internal static int getEBSSiteID(IRecordContext recordContext, IOrganization org)
        {
            int siteID = 0;

            foreach (ICustomAttribute ca in org.CustomAttributes)
            {
                var gf = ca.GenericField;
                if (gf.Name == "MoodCustom$EBS_Site_ID" && gf.DataValue.Value != null)
                {
                    siteID = Convert.ToInt32(gf.DataValue.Value.ToString());
                }
            }
            return siteID;
        }

        /// <summary>
        /// Given an incident ID returns the Incident number for that incident. Used to get the reference number of the parent incident. 
        /// </summary>
        /// <param name="parent_incident_id">Incident id to retrieve</param>
        /// <returns>string value of the parent reference number. Null if not passed an ID. </returns>
        private string getParentIncidentNumber(int? parent_incident_id)
        {
            if (parent_incident_id != null)
            {
                Incident parentIncident = new Incident();
                // Get the Incident
                // Make call to get ID for a particular Name
                RestClient getIncident = new RestClient();

                String url = _globalContext.InterfaceURL;
                // Need the site name, so explode on /
                string[] urlArray = url.Split('/');
                getIncident.endPoint = "https://" + urlArray[2] + "/services/rest/connect/v1.4/incidents/" + parent_incident_id;
                getIncident.osvcAuth = _globalContext.SessionId;
                getIncident.osvcContext = "Incident metadata";
                getIncident.httpMethod = httpVerb.GET;

                try
                {
                    string strResponse = getIncident.makeRequest();
                    parentIncident = JsonConvert.DeserializeObject<Incident>(strResponse, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                }
                catch (Exception e)
                {
                    _globalContext.LogMessage(e.Message);
                }
                return parentIncident.referenceNumber;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves the parent incident id from the incident. 
        /// </summary>
        /// <param name="incident">Incident Interface</param>
        /// <returns>Null or the Incident ID (INT) in the CF.</returns>
        internal static int? getParentIncidentID(IIncident incident)
        {
            int? parentIncidentID = null;

            foreach (ICustomAttribute ca in incident.CustomAttributes)
            {
                var gf = ca.GenericField;
                if (gf.Name == "MoodCustom$Parent_Incident_ID")
                {
                    if (gf.DataValue.Value != null)
                    {
                        parentIncidentID = Convert.ToInt32(gf.DataValue.Value.ToString());
                    }
                }
            }

            return parentIncidentID;
        }

        /// <summary>
        /// Given a record, find and return the Operating unit for the in context Account
        /// </summary>
        /// <param name="rContext">record context</param>
        /// <returns>Accounts Operating Unit</returns>
        internal static string getOU(IRecordContext rContext, IGlobalContext gContext, IOrganization org)
        {
            string OU = "";
            int OUID = 0;
            
            foreach (ICustomAttribute ca in org.CustomAttributes)
            {
                var gf = ca.GenericField;
                if (gf.Name == "MoodCustom$Operating_Unit" && gf.DataValue.Value != null)
                {
                    int.TryParse(gf.DataValue.Value.ToString(), out OUID);
                }
            }

            // Retreive the actual value for the ID
            OU = getNameFromID(OUID, "MoodCustom$Operating_Unit", gContext);

            return OU;
        }

        /// <summary>
        /// Given an ID and a field from which to retrieve the ID, get the name for that id and return it to the caller
        /// </summary>
        /// <param name="oUID">ID to retrieve name for</param>
        /// <param name="field">Named ID field to retrieve from</param>
        /// <returns>Name for the passed ID</returns>
        private static string getNameFromID(int oUID, string field, IGlobalContext _globalContext)
        {
            NamedID nameID = new NamedID();
            
            // Make call to get ID for a particular Name
            RestClient getNameFromID = new RestClient();

            // Field has $ in it. do a str replace for . 
            field = field.Replace('$', '.');

            String url = _globalContext.InterfaceURL;
            // Need the site name, so explode on /
            string[] urlArray = url.Split('/');
            getNameFromID.endPoint = "https://" + urlArray[2] + "/services/rest/connect/v1.4/" + field + "/" + oUID;
            getNameFromID.osvcAuth = _globalContext.SessionId;
            getNameFromID.osvcContext = "Organization metadata";
            getNameFromID.httpMethod = httpVerb.GET;

            try
            {
                string strResponse = getNameFromID.makeRequest();
                nameID = JsonConvert.DeserializeObject<NamedID>(strResponse, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception e)
            {
                _globalContext.LogMessage(e.Message);
            }

            return nameID.lookupName;
        }

        /// <summary>
        /// Retrieve a custom field value. This only works if the CF is a string type
        /// </summary>
        /// <param name="inc">incident object</param>
        /// <param name="cfid">integer custom field value</param>
        /// <returns>string of the value in the referenced Custom Field</returns>
        internal static string getCFValue(IIncident inc, int cfid)
        {
            string cfValue = "";
            {
                foreach (ICfVal cf in inc.CustomField)
                {
                    if (cf.CfId == cfid)
                    {
                        cfValue = cf.ValStr;
                    }
                }
            }
            return cfValue;
        }

        /// <summary>
        /// Retrieve a custom field value. This only works if the CF is a string type
        /// </summary>
        /// <param name="org">incident object</param>
        /// <param name="cfid">integer custom field value</param>
        /// <returns>string of the value in the referenced Custom Field</returns>
        internal static string getCFValue(IOrganization org, int cfid)
        {
            string cfValue = "";
            {
                foreach (ICfVal cf in org.CustomField)
                {
                    if (cf.CfId == cfid)
                    {
                        cfValue = cf.ValStr;
                    }
                }
            }
            return cfValue;
        }

        /// <summary>
        /// Retrieve a custom field integer value. This only works if the CF is an Integer type
        /// </summary>
        /// <param name="inc">incident object</param>
        /// <param name="cfid">integer custom field value</param>
        /// <returns>Integer of the value in the referenced Custom Field</returns>
        internal static int? getCFID(IIncident inc, int cfid)
        {
            int? id = null;
            {
                foreach (ICfVal cf in inc.CustomField)
                {
                    if (cf.CfId == cfid)
                    {
                        id = cf.ValInt;
                    }
                }
            }
            return id;
        }

        /// <summary>
        /// Retrieve a custom field integer value. This only works if the CF is an Integer type
        /// </summary>
        /// <param name="org">organization object</param>
        /// <param name="cfid">integer custom field value</param>
        /// <returns>Integer of the value in the referenced Custom Field</returns>
        internal static int? getCFID(IOrganization org, int cfid)
        {
            int? id = null;
            {
                foreach (ICfVal cf in org.CustomField)
                {
                    if (cf.CfId == cfid)
                    {
                        id = cf.ValInt;
                    }
                }
            }
            return id;
        }

        /// <summary>
        /// For a given CustomAttribute on an incident, return its value. 
        /// </summary>
        /// <param name="incident">Incident Object</param>
        /// <param name="customAttribute">Name of package and attribute to search</param>
        /// <returns>String value if found, empty string if not.</returns>
        public string getCAValue(IIncident incident, string customAttribute)
        {
            string attribValue = "";

            foreach (ICustomAttribute ca in incident.CustomAttributes)
            {
                var gf = ca.GenericField;
                if (gf.Name == customAttribute)
                {
                    if (gf.DataValue.Value != null)
                    {
                        attribValue = gf.DataValue.Value.ToString();
                    }
                }
            }

            return attribValue;
        }

        /// <summary>
        /// Calls the Get Bill To WS in EBS to retrieve the Billing info for the passed account/site. Possible returns are error, 
        /// zero addresses, one address or an array of addresses. 
        /// </summary>
        /// <param name="eBSToken">EBS token</param>
        /// <param name="siteID">Site ID</param>
        /// <param name="accountID">EBS Account ID</param>
        /// <param name="_gContext">Global Context</param>
        /// <returns></returns>
        internal static BillTo GetBillTos(string eBSToken, int siteID, int? accountID, IGlobalContext _gContext, IRecordContext _rContext)
        {
            BillTo addr = new BillTo();

            RestClient getBillTos = new RestClient();

            getBillTos.endPoint = $"{EBSRoot}oic/getbillto/{accountID.ToString()}/{siteID.ToString()}";
            getBillTos.ebsBearerToken = eBSToken;
            getBillTos.httpMethod = httpVerb.GET;

            try
            {
                BillToAddresses addrs = new BillToAddresses();
                string strResponse = getBillTos.makeRequest();
                addrs = JsonConvert.DeserializeObject<BillToAddresses>(strResponse, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                if (addrs.overall_status == "E")
                {
                    MessageBox.Show("An error occurred retrieving Bill To Information: " + addrs.overall_diagnostics);
                }
                if (addrs.BillTo.Count == 1) 
                {
                    // Handle for a Single Address
                    addr.address1 = addrs.BillTo[0].address1;
                    addr.cust_account_id = addrs.BillTo[0].cust_account_id;
                    addr.account_number = addrs.BillTo[0].account_number;
                    addr.city = addrs.BillTo[0].address1;
                    addr.country = addrs.BillTo[0].address1;
                    addr.county = addrs.BillTo[0].address1;
                    addr.customer_name = addrs.BillTo[0].customer_name;
                    addr.location_id = addrs.BillTo[0].location_id;
                    addr.party_site_id = addrs.BillTo[0].party_site_id;
                    addr.party_site_number = addrs.BillTo[0].party_site_number;
                    addr.cust_acct_site_id = addrs.BillTo[0].cust_acct_site_id;
                    addr.state = addrs.BillTo[0].state;
                    addr.postal_code = addrs.BillTo[0].postal_code;
                    addr.postal_plus4_code = addrs.BillTo[0].postal_plus4_code;
                }
                else if(addrs.BillTo.Count > 1)
                {
                    // Handle for Multiple Addresses - Display picker to select. 
                    BillToPicker picker = new BillToPicker(addrs);
                    var result = picker.ShowDialog();
                    addr.address1 = picker.BillToStreet;
                    addr.cust_account_id = Convert.ToInt32(picker.BillToAcctID);
                    addr.account_number = picker.BillToAcctNum;
                    addr.city = picker.BillToCity;
                    addr.country = picker.BillToCountry;
                    addr.county = picker.BillToCounty;
                    addr.customer_name = picker.BillToName;
                    addr.location_id = Convert.ToInt32(picker.BillToLocationID);
                    addr.party_site_id = Convert.ToInt32(picker.BillToPartySiteID);
                    addr.party_site_number = picker.BillToPartySiteNum;
                    addr.cust_acct_site_id = Convert.ToInt32(picker.BillToSiteID);
                    addr.state = picker.BillToState;
                    addr.postal_code = picker.BillToZip;
                    addr.postal_plus4_code = picker.BillToZip4;
                }
                else
                {
                    // Handle for No Addresses
                    AccountPicker accPicker = new AccountPicker(_gContext, _rContext);
                    var result = accPicker.ShowDialog();

                    if (result == DialogResult.OK)
                    {

                        //Populate the Fields with address information. 
                        addr.address1 = accPicker.RetStreet;
                        addr.cust_account_id = accPicker.RetAccountID;
                        addr.account_number = accPicker.RetAccountNumber;
                        addr.city = accPicker.RetCity;
                        addr.customer_name = accPicker.RetAccountName;
                        addr.country = "";
                        addr.county = "";
                        addr.location_id = 0;
                        addr.party_site_id = 0;
                        //addr.party_site_number = accPicker.RetSiteID;
                        addr.cust_acct_site_id = Convert.ToInt32(accPicker.RetSiteID);
                        addr.state = accPicker.RetState;
                        addr.postal_code = accPicker.RetZip;
                        addr.postal_plus4_code = "";
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Unable to get Bill To Addresses");
                _gContext.LogMessage(e.Message);

                // Handle for No Addresses
                AccountPicker accPicker = new AccountPicker(_gContext, _rContext);
                var result = accPicker.ShowDialog();

                if (result == DialogResult.OK)
                {
                    //Populate the Fields with address information. 
                    addr.address1 = accPicker.RetStreet;
                    addr.cust_account_id = accPicker.RetAccountID;
                    addr.account_number = accPicker.RetAccountNumber;
                    addr.city = accPicker.RetCity;
                    addr.customer_name = accPicker.RetAccountName;
                    addr.country = "";
                    addr.county = "";
                    addr.location_id = 0;
                    addr.party_site_id = 0;
                    //addr.party_site_number = "";
                    addr.cust_acct_site_id = Convert.ToInt32(accPicker.RetSiteID);
                    addr.state = accPicker.RetState;
                    addr.postal_code = accPicker.RetZip;
                    addr.postal_plus4_code = "";
                }
            }

            return addr;
        }

        /// <summary>
        /// Given a product string, returns the matching SalesProducts for that search.
        /// </summary>
        /// <param name="productString">Searched for Product</param>
        /// <param name="_globalContext">Global OSVC settings</param>
        /// <returns></returns>
        internal static ReportResults SearchProducts(string productName, string itemNumber, IGlobalContext _globalContext)
        {
            ReportResults products = new ReportResults();

            string filters = "";
            if (productName != "")
            {
                filters += "{\"name\": \"Name\",\"operator\": [ { \"lookupName\": \"LIKE\"} ],\"values\": [ \"%" + productName + "%\"]},";
            }
            if (itemNumber != "")
            {
                filters += "{\"name\": \"ItemNumber\",\"operator\": [ { \"lookupName\": \"LIKE\"} ],\"values\": [ \"%" + itemNumber + "%\"]},";
            }
            

            // Trim the trailing comma
            filters = filters.Substring(0, filters.Length - 3);

            // Retrieve Report Results based on filters
            RestClient getSalesProducts = new RestClient();
            String url = _globalContext.InterfaceURL;
            // Need the site name, so explode on /
            string[] urlArray = url.Split('/');
            getSalesProducts.endPoint = "https://" + urlArray[2] + "/services/rest/connect/v1.4/analyticsReportResults";
            getSalesProducts.osvcAuth = _globalContext.SessionId;
            getSalesProducts.osvcContext = "SalesProduct metadata";
            getSalesProducts.jsonBody = "{\"lookupName\": \"Items\",\"filters\": [" + filters + "]}] }";
            getSalesProducts.httpMethod = httpVerb.POST;

            try
            {
                string strResponse = getSalesProducts.makeRequest();
                products = JsonConvert.DeserializeObject<ReportResults>(strResponse, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception e)
            {
                _globalContext.LogMessage(e.Message);
            }

            return products;
        }

        /// <summary>
        /// Given a product string, returns the matching SalesProducts for that search.
        /// </summary>
        /// <param name="productString">Searched for Product</param>
        /// <param name="_globalContext">Global OSVC settings</param>
        /// <returns></returns>
        internal static ReportResults DebriefProductSearch(string itemNumber, IGlobalContext _globalContext)
        {
            ReportResults products = new ReportResults();

            string filters = "";
            if (itemNumber != "")
            {
                filters += "{\"name\": \"ItemNumber\",\"operator\": [ { \"lookupName\": \"=\"} ],\"values\": [ \""+itemNumber+"\"]},";
            }


            // Trim the trailing comma
            filters = filters.Substring(0, filters.Length - 1);

            // Retrieve Report Results based on filters
            RestClient getSalesProducts = new RestClient();
            String url = _globalContext.InterfaceURL;
            // Need the site name, so explode on /
            string[] urlArray = url.Split('/');
            getSalesProducts.endPoint = "https://" + urlArray[2] + "/services/rest/connect/v1.4/analyticsReportResults";
            getSalesProducts.osvcAuth = _globalContext.SessionId;
            getSalesProducts.osvcContext = "SalesProduct metadata";
            getSalesProducts.jsonBody = "{\"lookupName\": \"Items\",\"filters\": [" + filters + "]}";
            getSalesProducts.httpMethod = httpVerb.POST;

            try
            {
                string strResponse = getSalesProducts.makeRequest();
                products = JsonConvert.DeserializeObject<ReportResults>(strResponse, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception e)
            {
                _globalContext.LogMessage(e.Message);
            }

            return products;
        }

        /// <summary>
        /// Given a json payload of charges, submit the charges to ebs. Returns results to charges ui to update/create errors as needed
        /// </summary>
        /// <param name="ebsToken">Token for making calls to EBS</param>
        /// <param name="gContext">Global context for logging</param>
        /// <param name="payload">JSON Payload of charges to send to EBS</param>
        /// <returns>Charge Submission Object used by Charges UI to populate grid</returns>
        internal static ChargeSubmission SubmitCharges(string ebsToken, IGlobalContext gContext, string payload, IRecordContext rContext)
        {
            ChargeSubmission chargeStatus = new Models.ChargeSubmission();

            RestClient submitCharges = new RestClient();
            submitCharges.endPoint = $"{EBSRoot}oic/charges/";
            submitCharges.ebsBearerToken = ebsToken;
            submitCharges.jsonBody = payload;
            submitCharges.httpMethod = httpVerb.POST;

            Debug.WriteLine(">>> WorkspaceChargesAddIn.SubmitCharges payload = " + payload);

            try
            {
                string strResponse = submitCharges.makeRequest();
                Debug.WriteLine(">>> WorkspaceChargesAddIn.SubmitCharges strResponse = " + strResponse);
                chargeStatus = JsonConvert.DeserializeObject<ChargeSubmission>(strResponse, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                Debug.WriteLine(">>> WorkspaceChargesAddIn.SubmitCharges try.chargeStatus = " + chargeStatus);
                if (chargeStatus.overall_status == "E")
                {
                    // Apparently .Errors isnt always populated. If thats the case, we need to grab the general status
                    if (chargeStatus.Errors != null)
                    {
                        gContext.LogMessage($"Unable to submit charges, Error: {string.Join(";", chargeStatus.Errors)}");
                    }
                    else
                    {
                        gContext.LogMessage($"Unable to submit charges, Error: {string.Join(";", chargeStatus.overall_diagnostics)}");
                    }
                }
                else
                {
                    // We succeeded. Do a save of the incident before releasing control back to the user. 
                    rContext.TriggerNamedEvent("SaveIncident");
                }
            }
            catch (WebException e)
            {
                Debug.WriteLine(">>> WorkspaceChargesAddIn.SubmitCharges catch.chargeStatus = " + chargeStatus);
                MessageBox.Show("Unable to process submit charges. Please contact IT to resolve. Error: " + e.Message);
                // Process the response anyway
                if (e.Response != null)
                {
                    chargeStatus = JsonConvert.DeserializeObject<ChargeSubmission>(e.Response.ToString(), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                }
                gContext.LogMessage(e.Message);
            }

            return chargeStatus;
        }

        /// <summary>
        /// Retrieves the install base for the passed record. 
        /// </summary>
        /// <param name="_recordContext">Workspace In Context</param>
        /// <param name="_globalContext">Global OSVC parameters</param>
        /// <returns></returns>
        internal static ReportResults GetInstallBase(IRecordContext _recordContext, IGlobalContext _globalContext)
        {
            ReportResults ib = new ReportResults();

            int accountID = getAccountID(_recordContext);
            // If no account is bound to the incident, we cannot retrieve IB. Throw an error
            if (accountID == 0)
            {
                MessageBox.Show("This incident does not have an Account to retrieve Install Base for.");
                return ib;
            }
            
            // Retrieve Report Results for In Context Org ID
            RestClient getInstallBase = new RestClient();
            String url = _globalContext.InterfaceURL;
            // Need the site name, so explode on /
            string[] urlArray = url.Split('/');
            getInstallBase.endPoint = "https://" + urlArray[2] + "/services/rest/connect/v1.4/analyticsReportResults";
            getInstallBase.osvcAuth = _globalContext.SessionId;
            getInstallBase.osvcContext = "SalesProduct metadata";
            getInstallBase.jsonBody = "{\"lookupName\": \"Assets\",\"filters\": [{\"name\": \"Org ID\",\"operator\": [ { \"lookupName\": \"=\"} ],\"values\": [ \""+accountID+"\"]}] }";
            getInstallBase.httpMethod = httpVerb.POST;
            
            try
            {
                string strResponse = getInstallBase.makeRequest();
                ib = JsonConvert.DeserializeObject<ReportResults>(strResponse, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception e)
            {
                _globalContext.LogMessage(e.Message);
            }

            return ib;
        }

        /// <summary>
        /// Gets the OSVC Org ID (Account ID) for the in context record
        /// </summary>
        /// <param name="recordContext">Record Context</param>
        /// <returns>int Account ID</returns>
        public static int getAccountID(IRecordContext recordContext)
        {
            int accountID = 0;
            IIncident incident = recordContext.GetWorkspaceRecord(RightNow.AddIns.Common.WorkspaceRecordType.Incident) as IIncident;

            if (incident.OrgID != null)
            {
                accountID = incident.OrgID.Value;
            }

            return accountID;
        }

        /// <summary>
        /// Searches the Ship To Search Report for some combination of Account Name, Street Address, and/or Customer Number
        /// </summary>
        /// <param name="_recordContext">In Context Record (usually Incident)</param>
        /// <param name="_globalContext">Global Context</param>
        /// <param name="accountName">Searched for Account Name</param>
        /// <param name="streetAddress">Searched for Street Address</param>
        /// <param name="customerNumber">Searched for Customer Number</param>
        /// <returns>List of rows found in OSVC matching parameters</returns>
        internal static ReportResults SearchAccounts(IRecordContext _recordContext, IGlobalContext _globalContext, string accountName, string streetAddress, string customerNumber)
        {
            ReportResults accounts = new ReportResults();

            string filters = "";
            if (accountName != "")
            {
                filters += "{\"name\": \"OrgName\",\"operator\": [ { \"lookupName\": \"LIKE\"} ],\"values\": [ \"%" + accountName + "%\"]},";
            }
            if (streetAddress != "")
            {
                filters += "{\"name\": \"Street\",\"operator\": [ { \"lookupName\": \"LIKE\"} ],\"values\": [ \"%" + streetAddress + "%\"]},";
            }
            if (customerNumber != "")
            {
                filters += "{\"name\": \"CustomerNumber\",\"operator\": [ { \"lookupName\": \"=\"} ],\"values\": [ \"" + customerNumber + "\"]},";
            }

            // Trim the trailing comma
            filters = filters.Substring(0, filters.Length - 3);
            
            // Retrieve Report Results based on filters
            RestClient getAccounts = new RestClient();
            String url = _globalContext.InterfaceURL;
            // Need the site name, so explode on /
            string[] urlArray = url.Split('/');
            getAccounts.endPoint = "https://" + urlArray[2] + "/services/rest/connect/v1.4/analyticsReportResults";
            getAccounts.osvcAuth = _globalContext.SessionId;
            getAccounts.osvcContext = "SalesProduct metadata";
            getAccounts.jsonBody = "{\"lookupName\": \"Ship To Search\",\"filters\": [" + filters + "]}] }";
            getAccounts.httpMethod = httpVerb.POST;

            try
            {
                string strResponse = getAccounts.makeRequest();
                accounts = JsonConvert.DeserializeObject<ReportResults>(strResponse, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception e)
            {
                _globalContext.LogMessage(e.Message);
            }
            
            return accounts;
        }

        #region IAddInControl Members

        /// <summary>
        /// Method called by the Add-In framework to retrieve the control.
        /// </summary>
        /// <returns>The control, typically 'this'.</returns>
        public Control GetControl()
        {
            _chargesUI.Dock = DockStyle.Fill;
            this.Controls.Add(_chargesUI);
            return this;
        }

        #endregion

        #region IWorkspaceComponent2 Members

        /// <summary>
        /// Sets the ReadOnly property of this control.
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// Method which is called when any Workspace Rule Action is invoked.
        /// </summary>
        /// <param name="ActionName">The name of the Workspace Rule Action that was invoked.</param>
        public void RuleActionInvoked(string ActionName)
        {
        }

        /// <summary>
        /// Method which is called when any Workspace Rule Condition is invoked.
        /// </summary>
        /// <param name="ConditionName">The name of the Workspace Rule Condition that was invoked.</param>
        /// <returns>The result of the condition.</returns>
        public string RuleConditionInvoked(string ConditionName)
        {
            return string.Empty;
        }

        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
    }



    [AddIn("Workspace Factory AddIn", Version = "1.0.0.0")]
    public class WorkspaceAddInFactory : IWorkspaceComponentFactory2
    {
        IGlobalContext _globalContext;
        #region IWorkspaceComponentFactory2 Members

        // TST2
        [ServerConfigProperty(DefaultValue = "https://mooadmz.coresys.com/ords/moot/rest/")]
        // Prod
         //[ServerConfigProperty(DefaultValue = "https://moopdmz.coresys.com/ords/moop/rest/")]
        public String EBSRoot { get; set; }

        // TST2
        [ServerConfigProperty(DefaultValue = "SNVe6tFwKDyN8BYo7mxsFw..")]
        // Prod
         //[ServerConfigProperty(DefaultValue = "rYQydf_9-t_mhpGXq-V7yA..")]
        public String EBSTokenUN { get; set; }

        // TST2
        [ServerConfigProperty(DefaultValue = "kR0lydTtbLtFCRE0I7uSwQ..")]
        // Prod
         //[ServerConfigProperty(DefaultValue = "pMD9QvDXA_qeB1PztJpLjQ..")]
        public String EBSTokenPW { get; set; }

        /// <summary>
        /// Method which is invoked by the AddIn framework when the control is created.
        /// </summary>
        /// <param name="inDesignMode">Flag which indicates if the control is being drawn on the Workspace Designer. (Use this flag to determine if code should perform any logic on the workspace record)</param>
        /// <param name="RecordContext">The current workspace record context.</param>
        /// <returns>The control which implements the IWorkspaceComponent2 interface.</returns>
        public IWorkspaceComponent2 CreateControl(bool inDesignMode, IRecordContext RecordContext)
        {
            return new WorkspaceChargesAddIn(inDesignMode, RecordContext, _globalContext, EBSRoot, EBSTokenUN, EBSTokenPW);
        }

        #endregion

            #region IFactoryBase Members

            /// <summary>
            /// The 16x16 pixel icon to represent the Add-In in the Ribbon of the Workspace Designer.
            /// </summary>
        public Image Image16
        {
            get { return Properties.Resources.charges; }
        }

        /// <summary>
        /// The text to represent the Add-In in the Ribbon of the Workspace Designer.
        /// </summary>
        public string Text
        {
            get { return "Charges"; }
        }

        /// <summary>
        /// The tooltip displayed when hovering over the Add-In in the Ribbon of the Workspace Designer.
        /// </summary>
        public string Tooltip
        {
            get { return "Creates new lines to add to charges"; }
        }

        #endregion

        #region IAddInBase Members

        /// <summary>
        /// Method which is invoked from the Add-In framework and is used to programmatically control whether to load the Add-In.
        /// </summary>
        /// <param name="GlobalContext">The Global Context for the Add-In framework.</param>
        /// <returns>If true the Add-In to be loaded, if false the Add-In will not be loaded.</returns>
        public bool Initialize(IGlobalContext GlobalContext)
        {
            _globalContext = GlobalContext;
            return true;
        }

        #endregion
    }
}