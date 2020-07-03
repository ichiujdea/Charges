using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RightNow.AddIns.AddInViews;
using Charges.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Charges.Controllers;
using System.Globalization;
using System.Diagnostics;

namespace Charges.Views
{
    public partial class ChargesUI : UserControl
    {
        public IRecordContext _rContext;
        public IGlobalContext _gContext;
        public string EBSToken;
        public DateTime TokenExpiry;
        public BindingList<ChargeLines> chargeLines;
        public ChargesObject _charges;
        public LovList lovs;
        public ChargesObject CachedCharges;
        public List<ChargeLines> DebriefCharges;
        public WorkspaceChargesAddIn _workspaceAddIn;

        public ChargesUI(IRecordContext _recordContext, IGlobalContext _globalContext, WorkspaceChargesAddIn workspaceAddIn)
        {
            _rContext = _recordContext;
            _gContext = _globalContext;
            _workspaceAddIn = workspaceAddIn;

            // Create a top level object for charge lines to live within
            _charges = new ChargesObject();

            // Bind a list of values to the datagrid
            var bindingSource = new BindingSource();
            chargeLines = new BindingList<ChargeLines>();
            bindingSource.DataSource = chargeLines;

            InitializeComponent();

            ChargeLinesGrid.DataSource = bindingSource;
        }

        /// <summary>
        /// Encapsulates the functions that we have to do to reload charges into the grid 
        /// </summary>
        /// <param name="charges">list of charges to populate to grid</param>
        /// <param name="inc">incident object</param>
        public void PopulateCharges(List<ChargeLines> charges, IIncident inc)
        {
            // If we already have content in chargeLines, then we don't need to add previous charges. Its already been done. Bail
            if (chargeLines.Count > 0)
            {
                return;
            }

            foreach (ICustomAttribute ca in inc.CustomAttributes)
            {
                var gf = ca.GenericField;
                if (gf.Name == "Integration$charges_started_flag")
                {
                    gf.DataValue.Value = true;
                }
            }

            // If we do not already have LOV populated, business process from incident and get LOV from that
            if (lovs == null)
            {
                UpdateLOVs(charges);
            }

            foreach (ChargeLines charge in charges)
            {
                chargeLines.Add(charge);
                if (charge.Status == "Submitted")
                {
                    PONumber.Enabled = false;
                }
            }

            ChargeLinesGrid.DataSource = null;
            ChargeLinesGrid.DataSource = chargeLines;

            // Make all rows that were submitted read only
            foreach (DataGridViewRow row in ChargeLinesGrid.Rows)
            {
                if (row.Cells[Status.Index].Value.ToString() == "Submitted")
                {
                    row.ReadOnly = true;
                    row.DefaultCellStyle.BackColor = Color.Gray;
                }
            }

            // Make the last line (theoretically the one just added) selected
            for (int i = 0; i < ChargeLinesGrid.Rows.Count; i++)
            {
                ChargeLinesGrid.Rows[i].Selected = false;
            }
            ChargeLinesGrid.Rows[ChargeLinesGrid.Rows.Count - 1].Selected = true;
        }

        /// <summary>
        /// Binds cells to pop their item search/pickers. Controls presentation of IBPicker, Item Picker and Account/Address Picker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">event details</param>
        private void DisplayItemPicker(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Do not handle for any row that has a status of submitted
                if (ChargeLinesGrid.Rows[e.RowIndex].Cells[Status.Index].Value.ToString() == "Submitted")
                {
                    return;
                }

                // Handle for Item column selected
                if (e.ColumnIndex == ItemName.Index || e.ColumnIndex == ItemNumber.Index || e.ColumnIndex == ItemID.Index)
                {
                    ItemPicker picker = new ItemPicker(_gContext);
                    var result = picker.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        string prodName = picker.Product;
                        string prodModel = picker.Model;

                        // Set the value of this row to the model
                        ChargeLinesGrid.Rows[e.RowIndex].Cells[ItemName.Index].Value = prodName;
                        ChargeLinesGrid.Rows[e.RowIndex].Cells[ItemNumber.Index].Value = prodModel;
                        ChargeLinesGrid.Rows[e.RowIndex].Cells[ItemID.Index].Value = picker.EBSProductID;
                        ChargeLinesGrid.Rows[e.RowIndex].Cells[UOM.Index].Value = picker.UnitOfMeasure;
                    }
                }
                // Handle for Install Base Columns Selected
                else if (e.ColumnIndex == ItemInstance.Index || e.ColumnIndex == SerialNumber.Index|| e.ColumnIndex == ItemInstanceID.Index)
                {
                    // Get the list of IB for this Organization
                    Cursor.Current = Cursors.WaitCursor;
                    ReportResults results = WorkspaceChargesAddIn.GetInstallBase(_rContext, _gContext);
                    if (results.rows.Count == 0)
                    {
                        // No rows returned. Bail. 
                        MessageBox.Show("This account has no associated install base to choose from.");
                        return;
                    }
                    else
                    {
                        IBPicker ibPicker = new IBPicker(_gContext, _rContext, results);
                        var result = ibPicker.ShowDialog();

                        if (result == DialogResult.OK)
                        {
                            // Set the value of this row to the IB Values. 
                            ChargeLinesGrid.Rows[e.RowIndex].Cells[ItemInstanceID.Index].Value = ibPicker.InstanceID;
                            ChargeLinesGrid.Rows[e.RowIndex].Cells[SerialNumber.Index].Value = ibPicker.SerialNumberRet;
                            ChargeLinesGrid.Rows[e.RowIndex].Cells[ItemInstance.Index].Value = ibPicker.ItemNumberRet;
                            ChargeLinesGrid.Rows[e.RowIndex].Cells[ItemInstanceName.Index].Value = ibPicker.ItemNameRet;
                        }
                    }
                }
                // Handle for Ship To account selected
                else if (e.ColumnIndex == ShipToAddress1.Index || e.ColumnIndex == ShipToCity.Index || e.ColumnIndex == ShipToState.Index || e.ColumnIndex == ShipToZip.Index || e.ColumnIndex == ShipToAccountNumber.Index || e.ColumnIndex == ShipToName.Index)
                {
                    AccountPicker accPicker = new AccountPicker(_gContext, _rContext);
                    var result = accPicker.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        //Populate the Fields with address information. 
                        ChargeLinesGrid.Rows[e.RowIndex].Cells[ShipToName.Index].Value = accPicker.RetAccountName;
                        ChargeLinesGrid.Rows[e.RowIndex].Cells[ShipToAddress1.Index].Value = accPicker.RetStreet;
                        ChargeLinesGrid.Rows[e.RowIndex].Cells[ShipToCity.Index].Value = accPicker.RetCity;
                        ChargeLinesGrid.Rows[e.RowIndex].Cells[ShipToState.Index].Value = accPicker.RetState;
                        ChargeLinesGrid.Rows[e.RowIndex].Cells[ShipToZip.Index].Value = accPicker.RetZip;
                        ChargeLinesGrid.Rows[e.RowIndex].Cells[ShipToSiteID.Index].Value = accPicker.RetSiteID;
                        ChargeLinesGrid.Rows[e.RowIndex].Cells[ShipToAccountID.Index].Value = accPicker.RetAccountID;
                        ChargeLinesGrid.Rows[e.RowIndex].Cells[ShipToAccountNumber.Index].Value = accPicker.RetAccountNumber;
                    }
                }
            }
        }

        /// <summary>
        /// Populates debrief information to the datagrid
        /// </summary>
        /// <param name="debriefDetail">collection of lines from debrief</param>
        internal void PopulateDebrief(Debrief debriefDetail)
        {
            foreach(ChargeLines line in debriefDetail.charges)
            {
                chargeLines.Add(line);
            }

            // Populate pricing for each line
            foreach (ChargeLines line in chargeLines)
            {
                PopulatePricing(line);
            }

            ChargeLinesGrid.DataSource = null;
            ChargeLinesGrid.DataSource = chargeLines;

            // Make the last line selected
            for (int i = 0; i < ChargeLinesGrid.Rows.Count; i++)
            {
                ChargeLinesGrid.Rows[i].Selected = false;
            }
            ChargeLinesGrid.Rows[ChargeLinesGrid.Rows.Count - 1].Selected = true;
        }

        /// <summary>
        /// When we get a bad error response from the EBS web services, the charge process needs to lock. 
        /// Charges will be stored and retrieved in state by the sheve/retreive charges functions, but when errors happen, we need to lock down the charges addin until they can be reviewed
        /// </summary>
        internal void DisableCharges()
        {
            // Disable the Add Line Button
            AddChargeLine.Enabled = false;
            // Make all row content currently in the system read only
            ChargeLinesGrid.ReadOnly = true;
            // Unhook click listeners
            ChargeLinesGrid.CellClick -= new System.Windows.Forms.DataGridViewCellEventHandler(this.DisplayItemPicker);
            ChargeLinesGrid.CellValueChanged -= new System.Windows.Forms.DataGridViewCellEventHandler(this.GetEntitlements);
            // Set Background Color of cells to indicate not interactive
            ChargeLinesGrid.DefaultCellStyle.SelectionBackColor = Color.LightGray;
        }

        /// <summary>
        /// Takes the LOVs return list and uses it to populate the three (at this point) dropdown menus in Charges
        /// </summary>
        /// <param name="lov_values">A single list of values from the LOVs array specific to the passed DropDown name</param>
        /// <param name="dropDownName">Which dropdown does this LOV populate</param>
        internal void PopulateDropdown(List<string> lov_values, string dropDownName)
        {
            // Need to be alphebetized...but not if shipping
            if(dropDownName != "Shipping Method")
            {
                lov_values.Sort();
            }
            
            foreach(string value in lov_values)
            {
                switch (dropDownName){
                    case "Return Reason":
                        ReturnReason.Items.Add(value);
                        break;
                    case "Service Activity":
                        ServiceActivity.Items.Add(value);
                        break;
                    case "Shipping Method":
                        ShippingMethod.Items.Add(value);
                        break;
                    case "Freight Terms":
                        FreightTerms.Items.Add(value);
                        break;
                    case "Price Override Reason":
                        OverrideReason.Items.Add(value);
                        break;
                    case "Price List":
                        PriceList.Items.Add(value);
                        break;
                }
            }
        }

        /// <summary>
        /// When the Item Number and Service Activity for a row are populated, get the pricing for the item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">event details</param>
        private void GetEntitlements(object sender, DataGridViewCellEventArgs e)
        {
             
            if (e.RowIndex != -1)
            {
                int.TryParse(ChargeLinesGrid.Rows[e.RowIndex].Cells[ItemID.Index].Value?.ToString(), out int itemID);
                string serviceActivity = ChargeLinesGrid.Rows[e.RowIndex].Cells[ServiceActivity.Index].Value?.ToString();
                int.TryParse(ChargeLinesGrid.Rows[e.RowIndex].Cells[ItemInstanceID.Index].Value?.ToString(), out int instanceID);
                int.TryParse(ChargeLinesGrid.Rows[e.RowIndex].Cells[EntitledIB.Index].Value?.ToString(), out int prevInstanceID);
                int.TryParse(ChargeLinesGrid.Rows[e.RowIndex].Cells[PrevItem.Index].Value?.ToString(), out int prevItemID);
                var instanceCheck = ChargeLinesGrid.Rows[e.RowIndex].Cells[EntitledIB.Index].Value;

                // Only fire if Item ID, Instance ID and Service Activity is populated.
                // This handles for the IB Required possibility
                if (itemID != 0 && serviceActivity != null && prevItemID != itemID || ChargeLinesGrid.Rows[e.RowIndex].Cells[PriceList.Index].Value != ChargeLinesGrid.Rows[e.RowIndex].Cells[PreviousPriceList.Index].Value)
                {
                    // Check to see if service activity is equal to any of the values in our list that Instance ID is required for. If so, check if instance ID is populated
                    if ((lovs.EntitlementNotRequired.Contains(serviceActivity) && instanceID != 0 && instanceID != prevInstanceID) ||(!lovs.EntitlementNotRequired.Contains(serviceActivity)))
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        // We didn't get an EBS ID for this account. Bail. 
                        if (_workspaceAddIn.AccountID == null)
                        {
                            MessageBox.Show("This Account has no EBS ID. Please select a different account.");
                            return;
                        }

                        // Verify token is still valid. If not, get a new one. 
                        CheckToken();

                        // Make Web Service call to Get Entitlements
                        string itemNumber = ChargeLinesGrid.Rows[e.RowIndex].Cells[ItemNumber.Index].Value.ToString();
                        string busProc = ChargeLinesGrid.Rows[e.RowIndex].Cells[BusinessProcess.Index].Value.ToString();
                        Pricing price = WorkspaceChargesAddIn.GetPricing(EBSToken, instanceID, itemNumber, serviceActivity, _workspaceAddIn.AccountID, _gContext, busProc, chargeLines[e.RowIndex].PriceList, _workspaceAddIn.billTo.cust_acct_site_id);

                        // If we got an error on pricing, disable charges
                        if (price.return_status != "S")
                        {
                            DisableCharges();
                            return;
                        }

                        // Detatch event listeners while filling in data
                        ChargeLinesGrid.CellValueChanged -= new System.Windows.Forms.DataGridViewCellEventHandler(GetEntitlements);

                        // Trying to update the underlying data binding instead
                        chargeLines[e.RowIndex].ListPrice = price.list_price;
                        chargeLines[e.RowIndex].OriginalSellingPrice = price.selling_price;
                        chargeLines[e.RowIndex].SellingPrice = price.selling_price;
                        chargeLines[e.RowIndex].PriceListHeader = price.list_header_id;
                        chargeLines[e.RowIndex].PriceList = price.price_list;
                        chargeLines[e.RowIndex].PreviousPriceList = price.price_list;
                        chargeLines[e.RowIndex].BillingCurrency = price.currency_code;
                        chargeLines[e.RowIndex].ContractNumber = price.contract_number;
                        chargeLines[e.RowIndex].ContractID = price.contract_id;
                        chargeLines[e.RowIndex].ContractNumberModifier = price.contract_number_modifier;
                        chargeLines[e.RowIndex].CoverageTermName = price.coverage_term_name;
                        chargeLines[e.RowIndex].ContractLineID = price.contract_line_id;
                        chargeLines[e.RowIndex].PrevItemID = chargeLines[e.RowIndex].ItemID;
                        chargeLines[e.RowIndex].PrevItemInstanceID = chargeLines[e.RowIndex].ItemInstanceID;
                        chargeLines[e.RowIndex].ExtendedPrice = price.selling_price * ((decimal)chargeLines[e.RowIndex].Quantity);

                        // Handle for override possibly being checked
                        string PriceOverridden = "false";
                        if (ChargeLinesGrid.Rows[e.RowIndex].Cells[OverridePriceList.Index].Value != null)
                        {
                            PriceOverridden = ChargeLinesGrid.Rows[e.RowIndex].Cells[OverridePriceList.Index].Value.ToString() ?? "false";
                        }

                        ChargeLinesGrid.DataSource = null;
                        ChargeLinesGrid.DataSource = chargeLines;

                        if (PriceOverridden == "True")
                        {
                            ChargeLinesGrid.Rows[e.RowIndex].Cells[OverridePriceList.Index].Value = true;
                        }

                        // Populate Actual/Estimate Totals
                        decimal sumEstimate = 0;
                        decimal sumActual = 0;
                        decimal sumTotal = 0;

                        foreach (ChargeLines line in chargeLines)
                        {
                            if (line.LineType == "Actual")
                            {
                                sumActual += line.ExtendedPrice;
                            }
                            if (line.LineType == "Estimate")
                            {
                                sumEstimate += line.ExtendedPrice;
                            }
                            sumTotal += line.ExtendedPrice;
                        }

                        ActualValue.Text = sumActual.ToString("n");
                        ActualValue.Width = 40;
                        EstimateValue.Text = sumEstimate.ToString("n");
                        EstimateValue.Width = 40;
                        TotalValue.Text = sumTotal.ToString("n");
                        TotalValue.Width = 40;

                        // Reattach event listeners
                        ChargeLinesGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(GetEntitlements);

                        Cursor.Current = Cursors.Default;
                    }
                }

                // Handle for IB being populated to get entitlements
                if (itemID != 0 && serviceActivity != null && prevInstanceID != instanceID)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    // We didn't get an EBS ID for this account. Bail. 
                    if (_workspaceAddIn.AccountID == null)
                    {
                        MessageBox.Show("This Account has no EBS ID. Please select a different account.");
                        return;
                    }

                    // Verify token is still valid. If not, get a new one. 
                    CheckToken();

                    // Make Web Service call to Get Entitlements
                    string itemNumber = ChargeLinesGrid.Rows[e.RowIndex].Cells[ItemNumber.Index].Value.ToString();
                    string busProc = ChargeLinesGrid.Rows[e.RowIndex].Cells[BusinessProcess.Index].Value.ToString();
                    Pricing price = WorkspaceChargesAddIn.GetPricing(EBSToken, instanceID, itemNumber, serviceActivity, _workspaceAddIn.AccountID, _gContext, busProc, chargeLines[e.RowIndex].PriceList, _workspaceAddIn.billTo.cust_acct_site_id);

                    // If we got an error on pricing, disable charges
                    if (price.return_status != "S")
                    {
                        DisableCharges();
                        return;
                    }

                    // Detatch event listeners while filling in data
                    ChargeLinesGrid.CellValueChanged -= new System.Windows.Forms.DataGridViewCellEventHandler(GetEntitlements);

                    // Trying to update the underlying data binding instead
                    chargeLines[e.RowIndex].ListPrice = price.list_price;
                    chargeLines[e.RowIndex].OriginalSellingPrice = price.selling_price;
                    chargeLines[e.RowIndex].SellingPrice = price.selling_price;
                    chargeLines[e.RowIndex].PriceListHeader = price.list_header_id;
                    chargeLines[e.RowIndex].PriceList = price.price_list;
                    chargeLines[e.RowIndex].PreviousPriceList = price.price_list;
                    chargeLines[e.RowIndex].BillingCurrency = price.currency_code;
                    chargeLines[e.RowIndex].ContractNumber = price.contract_number;
                    chargeLines[e.RowIndex].ContractID = price.contract_id;
                    chargeLines[e.RowIndex].ContractNumberModifier = price.contract_number_modifier;
                    chargeLines[e.RowIndex].CoverageTermName = price.coverage_term_name;
                    chargeLines[e.RowIndex].ContractLineID = price.contract_line_id;
                    chargeLines[e.RowIndex].PrevItemID = chargeLines[e.RowIndex].ItemID;
                    chargeLines[e.RowIndex].PrevItemInstanceID = chargeLines[e.RowIndex].ItemInstanceID;
                    chargeLines[e.RowIndex].ExtendedPrice = price.selling_price * ((decimal)chargeLines[e.RowIndex].Quantity);

                    // Handle for override possibly being checked
                    string PriceOverridden = "false";
                    if (ChargeLinesGrid.Rows[e.RowIndex].Cells[OverridePriceList.Index].Value != null)
                    {
                        PriceOverridden = ChargeLinesGrid.Rows[e.RowIndex].Cells[OverridePriceList.Index].Value.ToString() ?? "false";
                    }

                    ChargeLinesGrid.DataSource = null;
                    ChargeLinesGrid.DataSource = chargeLines;

                    if (PriceOverridden == "True")
                    {
                        ChargeLinesGrid.Rows[e.RowIndex].Cells[OverridePriceList.Index].Value = true;
                    }

                    // Populate Actual/Estimate Totals
                    decimal sumEstimate = 0;
                    decimal sumActual = 0;
                    decimal sumTotal = 0;

                    foreach (ChargeLines line in chargeLines)
                    {
                        if (line.LineType == "Actual")
                        {
                            sumActual += line.ExtendedPrice;
                        }
                        if (line.LineType == "Estimate")
                        {
                            sumEstimate += line.ExtendedPrice;
                        }
                        sumTotal += line.ExtendedPrice;
                    }

                    ActualValue.Text = sumActual.ToString("n");
                    EstimateValue.Text = sumEstimate.ToString("n");
                    TotalValue.Text = sumTotal.ToString("n");

                    // Reattach event listeners
                    ChargeLinesGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(GetEntitlements);

                    Cursor.Current = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// Used programmatically to retrieve entitlements for lines added by the addin itself. 
        /// </summary>
        /// <param name="line"></param>
        public ChargeLines GetEntitlements(ChargeLines line)
        {
            if (line.ItemID != 0 && line.ServiceActivity != null)
            {
                // Check to see if service activity is equal to any of the values in our list that Instance ID is required for. If so, check if instance ID is populated
                if ((lovs.EntitlementNotRequired.Contains(line.ServiceActivity) && line.ItemInstanceID != 0) || (!lovs.EntitlementNotRequired.Contains(line.ServiceActivity)))
                {
                    // Verify token is still valid. If not, get a new one. 
                    CheckToken();

                    // Make Web Service call to Get Entitlements
                    Pricing price = WorkspaceChargesAddIn.GetPricing(EBSToken, line.ItemInstanceID, line.ItemNumber, line.ServiceActivity, _workspaceAddIn.AccountID, _gContext, _workspaceAddIn.BusinessProcessValue, line.PriceList, _workspaceAddIn.billTo.cust_acct_site_id);

                    // If we got an error on pricing, disable charges
                    if (price.return_status != "S")
                    {
                        DisableCharges();
                        return line;
                    }

                    // Populate the charge elements with returned data
                    line.PrevItemID = line.ItemID;
                    line.PrevItemInstanceID = line.ItemInstanceID;
                    line.ListPrice = price.list_price;
                    line.SellingPrice = price.selling_price;
                    line.OriginalSellingPrice = price.selling_price;
                    line.PriceListHeader = price.list_header_id;
                    line.PriceList = price.price_list;
                    line.PreviousPriceList = price.price_list;
                    line.BillingCurrency = price.currency_code;
                    line.ContractNumber = price.contract_number;
                    line.ContractNumberModifier = price.contract_number_modifier;
                    line.CoverageTermName = price.coverage_term_name;
                    line.ContractLineID = price.contract_line_id;
                    line.ContractNumber = price.contract_number;
                    line.ContractID = price.contract_id;
                }
            }

            return line;
        }

        /// <summary>
        /// When there is a value in both Quantity and Selling price, multiply them and put this value in extended price
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopulatePricing(object sender, DataGridViewCellEventArgs e)
        {
            // Only fire if at least one row exists
            if (e.RowIndex != -1)
            {
                decimal extPrice = 0;
                // Populate Extended Price
                if (decimal.TryParse(ChargeLinesGrid.Rows[e.RowIndex].Cells[Quantity.Index].Value.ToString(), out decimal qty) && decimal.TryParse(ChargeLinesGrid.Rows[e.RowIndex].Cells[SellingPrice.Index].Value.ToString(), out decimal sellingPrice))
                {
                    extPrice = qty * sellingPrice;
                    ChargeLinesGrid.Rows[e.RowIndex].Cells[ExtendedPrice.Index].Value = extPrice;
                }

                // Populate Actual/Estimate Totals
                decimal sumEstimate = 0;
                decimal sumActual = 0;
                decimal sumTotal = 0;

                foreach (ChargeLines line in chargeLines)
                {
                    if (line.LineType == "Actual")
                    {
                        sumActual += line.ExtendedPrice;
                    }
                    if (line.LineType == "Estimate")
                    {
                        sumEstimate += line.ExtendedPrice;
                    }
                    sumTotal += line.ExtendedPrice;
                }

                ActualValue.Text = sumActual.ToString("n");
                EstimateValue.Text = sumEstimate.ToString("n");
                TotalValue.Text = sumTotal.ToString("n");
            }
        }

        /// <summary>
        /// This handles for checking to see if the dropdown for SA has changed and if so, fire a commit edit. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // Only fire if this is a combobox AND if its the Service Activity dropdown
            if (ChargeLinesGrid.CurrentCell is DataGridViewComboBoxCell && ChargeLinesGrid.CurrentCell.ColumnIndex == ServiceActivity.Index)
            {
                if (ChargeLinesGrid.IsCurrentCellDirty)
                {
                    // This fires the cell value changed handler below
                    ChargeLinesGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);

                    chargeLines[ChargeLinesGrid.CurrentCell.RowIndex] = GetEntitlements(chargeLines[ChargeLinesGrid.CurrentCell.RowIndex]);

                    // Make all rows that were submitted read only
                    foreach (DataGridViewRow row in ChargeLinesGrid.Rows)
                    {
                        if (row.Cells[Status.Index].Value.ToString() == "Submitted")
                        {
                            row.ReadOnly = true;
                            row.DefaultCellStyle.BackColor = Color.Gray;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Fires every time the SA changes to default qty to 1 or -1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefaultQuantity(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Do not handle for any row that has a status of submitted
                if (ChargeLinesGrid.Rows[e.RowIndex].Cells[Status.Index].Value.ToString() == "Submitted")
                {
                    return;
                }

                // Handle for SA being changed
                if (e.ColumnIndex == ServiceActivity.Index)
                {
                    // Certain instances can cause service activity to be null when you come into an incident. In these cases, leave the qty as is.
                    if (ChargeLinesGrid.Rows[e.RowIndex].Cells[ServiceActivity.Index].Value == null)
                    {
                        return;
                    }

                    // If SA = ASCAP Mix Incoming Items, iHeartRadio Incoming Items, Pandora Incoming Items, Rental Return, Return
                    if(ChargeLinesGrid.Rows[e.RowIndex].Cells[ServiceActivity.Index].Value.ToString() == "ASCAP Mix Incoming Items" ||
                        ChargeLinesGrid.Rows[e.RowIndex].Cells[ServiceActivity.Index].Value.ToString() == "iHeartRadio Incoming Items" ||
                        ChargeLinesGrid.Rows[e.RowIndex].Cells[ServiceActivity.Index].Value.ToString() == "Pandora Incoming Items" ||
                        ChargeLinesGrid.Rows[e.RowIndex].Cells[ServiceActivity.Index].Value.ToString() == "Rental Return" ||
                        ChargeLinesGrid.Rows[e.RowIndex].Cells[ServiceActivity.Index].Value.ToString() == "Return")
                    {
                        ChargeLinesGrid.Rows[e.RowIndex].Cells[Quantity.Index].Value = -1;
                    }
                    else
                    {
                        ChargeLinesGrid.Rows[e.RowIndex].Cells[Quantity.Index].Value = 1;
                    }
                }
            }
        }

        /// <summary>
        /// Programatically populates pricing for the line.
        /// </summary>
        /// <param name="line">Line in ChargeLines object</param>
        private void PopulatePricing(ChargeLines line)
        {
            // Populate Extended Price
            if (decimal.TryParse(line.Quantity.ToString(), out decimal qty) && decimal.TryParse(line.SellingPrice.ToString(), out decimal sellingPrice))
            {
                decimal extPrice = qty * sellingPrice;
                line.ExtendedPrice = extPrice;
            }

            // Populate Actual/Estimate Totals
        }

        /// <summary>
        /// When a user clicks the override price box, make both Selling Price and Override Reason editalbe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PriceOverride(object sender, DataGridViewCellEventArgs e)
        {
            // Only fire if at least one row exists
            if (e.RowIndex != -1)
            {
                if (Convert.ToBoolean(ChargeLinesGrid.Rows[e.RowIndex].Cells[ZeroCharge.Index].Value) == true)
                {
                    // Enable Editable on the Selling Price cell and allow the reason dropdown to be populated
                    ChargeLinesGrid.Rows[e.RowIndex].Cells[SellingPrice.Index].ReadOnly = false;
                    ChargeLinesGrid.Rows[e.RowIndex].Cells[OverrideReason.Index].ReadOnly = false;
                }
                else
                {
                    // Disable editing, set selling price back to original selling price
                    ChargeLinesGrid.Rows[e.RowIndex].Cells[SellingPrice.Index].ReadOnly = true;
                    ChargeLinesGrid.Rows[e.RowIndex].Cells[SellingPrice.Index].Value = ChargeLinesGrid.Rows[e.RowIndex].Cells[OrigSellingPrice.Index].Value;
                    ChargeLinesGrid.Rows[e.RowIndex].Cells[OverrideReason.Index].ReadOnly = true;
                    ChargeLinesGrid.Rows[e.RowIndex].Cells[OverrideReason.Index].Value = null;
                }
            }
        }

        /// <summary>
        /// When a user clicks the override price list box, allow the user to change the price list. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PriceListOverride(object sender, DataGridViewCellEventArgs e)
        {
            // Only fire if at least one row exists
            if (e.RowIndex != -1)
            {
                if (Convert.ToBoolean(ChargeLinesGrid.Rows[e.RowIndex].Cells[OverridePriceList.Index].Value) == true)
                {
                    // Enable Editable on the Price list cell
                    ChargeLinesGrid.Rows[e.RowIndex].Cells[PriceList.Index].ReadOnly = false;
                }
                else
                {
                    // Disable editing, set selling price back to original selling price
                    ChargeLinesGrid.Rows[e.RowIndex].Cells[PriceList.Index].ReadOnly = true;
                }
            }
        }

        /// <summary>
        /// Grabs a list of LOVs from ebs and populates them. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdateLOVs()
        {
            // If we do not have a business process, we cannot get LOV. Bail and inform the user
            if (_workspaceAddIn.BusinessProcessValue == null)
            {
                MessageBox.Show("This incident does not have a populated business process. Cannot get list of values.");
                return;
            }

            // Now we cannot get LOV without an in context BillTo. If not populated, get that
            if (_workspaceAddIn.billTo == null)
            {
                int siteID = WorkspaceChargesAddIn.getEBSSiteID(_rContext, _workspaceAddIn.ChargeOrganization);
                _workspaceAddIn.billTo = WorkspaceChargesAddIn.GetBillTos(EBSToken, siteID, _workspaceAddIn.AccountID, _gContext, _rContext);
                if (_workspaceAddIn.billTo.cust_account_id == 0)
                {
                    MessageBox.Show("No Bill To Selected. Charges cannot poroceed. Please refresh incident and select BillTo");
                    DisableCharges();
                }
            }
            
            lovs = WorkspaceChargesAddIn.GetLovsFromEBS(EBSToken, _workspaceAddIn.BusinessProcessValue, _gContext, WorkspaceChargesAddIn.EBSEnv, _workspaceAddIn.billTo.cust_acct_site_id);

            // Populate the dropdown fields in the ChargesUI
            if (lovs.lovs.Count == 6)
            {
                PopulateDropdown(lovs.lovs[0].lov_values, lovs.lovs[0].lov_name);
                PopulateDropdown(lovs.lovs[1].lov_values, lovs.lovs[1].lov_name);
                PopulateDropdown(lovs.lovs[2].lov_values, lovs.lovs[2].lov_name);
                PopulateDropdown(lovs.lovs[3].lov_values, lovs.lovs[3].lov_name);
                PopulateDropdown(lovs.lovs[4].lov_values, lovs.lovs[4].lov_name);
                PopulateDropdown(lovs.lovs[5].lov_values, lovs.lovs[5].lov_name);
            }
            else
            {
                MessageBox.Show("Bad response when getting dropdown values from EBS. See Addin Logs for more information.");
                // Turn off charges
                DisableCharges();
                return;
            }
        }

        /// <summary>
        /// Grabs a list of LOVs from ebs and populates them, assuming that previous charges exist 
        /// </summary>
        public void UpdateLOVs(List<ChargeLines> charges)
        {
            // If we do not have a business process, we cannot get LOV. Bail and inform the user
            if (_workspaceAddIn.BusinessProcessValue == null)
            {
                MessageBox.Show("This incident does not have a populated business process. Cannot get list of values.");
                return;
            }

            // Now we cannot get LOV without an in context BillTo. If not populated, get that
            if (_workspaceAddIn.billTo == null)
            {
                _workspaceAddIn.billTo = new BillTo();
                int siteID = WorkspaceChargesAddIn.getEBSSiteID(_rContext, _workspaceAddIn.ChargeOrganization);

                // We already have a billto from a previous line. Before getting BillTo, check to see if there is a charge line, and get it from there
                foreach (ChargeLines charge in charges)
                {
                    if (charge.BillToAccountID != null)
                    {
                        _workspaceAddIn.billTo.address1 = charge.BillToAddress1;
                        _workspaceAddIn.billTo.cust_account_id = charge.BillToAccountID;
                        _workspaceAddIn.billTo.account_number = charge.BillToAccountNumber;
                        _workspaceAddIn.billTo.city = charge.BillToCity;
                        _workspaceAddIn.billTo.customer_name = charge.BillToName;
                        // _workspaceAddIn.billTo.location_id = addrs.BillTo[0].location_id;
                        // _workspaceAddIn.billTo.party_site_id = addrs.BillTo[0].party_site_id;
                        // addr.party_site_number = addrs.BillTo[0].party_site_number;
                        _workspaceAddIn.billTo.cust_acct_site_id = charge.BillToSiteID;
                        _workspaceAddIn.billTo.state = charge.BillToState;
                        _workspaceAddIn.billTo.postal_code = charge.BillToZip;
                        //_workspaceAddIn.billTo.postal_plus4_code = addrs.BillTo[0].postal_plus4_code;
                    }
                }

                // If we haven't populated billTo yet, grab it the old way
                if(_workspaceAddIn.billTo.cust_account_id == null)
                {
                    _workspaceAddIn.billTo = WorkspaceChargesAddIn.GetBillTos(EBSToken, siteID, _workspaceAddIn.AccountID, _gContext, _rContext);
                    if (_workspaceAddIn.billTo.cust_account_id == 0)
                    {
                        MessageBox.Show("No Bill To Selected. Charges cannot poroceed. Please refresh incident and select BillTo");
                        DisableCharges();
                    }
                }
            }

            lovs = WorkspaceChargesAddIn.GetLovsFromEBS(EBSToken, _workspaceAddIn.BusinessProcessValue, _gContext, WorkspaceChargesAddIn.EBSEnv, _workspaceAddIn.billTo.cust_acct_site_id);

            // Populate the dropdown fields in the ChargesUI
            if (lovs.lovs.Count == 6)
            {
                PopulateDropdown(lovs.lovs[0].lov_values, lovs.lovs[0].lov_name);
                PopulateDropdown(lovs.lovs[1].lov_values, lovs.lovs[1].lov_name);
                PopulateDropdown(lovs.lovs[2].lov_values, lovs.lovs[2].lov_name);
                PopulateDropdown(lovs.lovs[3].lov_values, lovs.lovs[3].lov_name);
                PopulateDropdown(lovs.lovs[4].lov_values, lovs.lovs[4].lov_name);
                PopulateDropdown(lovs.lovs[5].lov_values, lovs.lovs[5].lov_name);
            }
            else
            {
                MessageBox.Show("Bad response when getting dropdown values from EBS. See Addin Logs for more information.");
                // Turn off charges
                DisableCharges();
                return;
            }
        }

        /// <summary>
        /// Checks the current token expiry. Gets a new token if needed. Writes it back to the token details on the ChargesUI object. 
        /// </summary>
        public void CheckToken()
        {
            if (this.TokenExpiry < DateTime.Now)
            {
                Token token = WorkspaceChargesAddIn.GetEBSToken(_gContext);
                if (token != null)
                {
                    TokenExpiry = DateTime.Now.AddSeconds(token.expires_in);
                    EBSToken = token.access_token;
                }
                else
                {
                    MessageBox.Show("Unable to refresh Access Token. Please try again later.");
                    return;
                }

            }
        }

        /// <summary>
        /// Handles all the data fill in that needs to be defaulted on a new charge line. 
        /// Get Bill To
        /// Default Incident Values
        /// </summary>
        /// <param name="sender">DataGrid</param>
        /// <param name="e">Row Add line</param>
        private void AddChargeLine_Click(object sender, EventArgs e)
        {
            int siteID = WorkspaceChargesAddIn.getEBSSiteID(_rContext, _workspaceAddIn.ChargeOrganization);
            string ou = WorkspaceChargesAddIn.getOU(_rContext, _gContext, _workspaceAddIn.ChargeOrganization);
            string environment = WorkspaceChargesAddIn.GetEnvironment(_gContext);
            IOrganization org = _workspaceAddIn.ChargeOrganization;
            string accountNumber = WorkspaceChargesAddIn.getCFValue(org, (int)System.Enum.Parse(typeof(WorkspaceChargesAddIn.EBSAccountNumber), environment));

            // Verify token is still valid. If not, get a new one. 
            CheckToken();

            // If this is the first line added to charges, set the Charges Started flag on the incident, get the default Business Process from Incident
            if (ChargeLinesGrid.RowCount == 0)
            {
                // Charges Started
                IIncident inc = _workspaceAddIn.ChargeIncident;
                foreach (ICustomAttribute ca in inc.CustomAttributes)
                {
                    var gf = ca.GenericField;
                    if (gf.Name == "Integration$charges_started_flag")
                    {
                        gf.DataValue.Value = true;

                    }
                }
            }

            // If we do not already have LOV populated, business process from incident and get LOV from that
            if (lovs == null)
            {
                UpdateLOVs();
            }

            // If we do not  have Bill To populated, retrieve Bill To information. 
            if (_workspaceAddIn.billTo == null)
            {
                _workspaceAddIn.billTo = WorkspaceChargesAddIn.GetBillTos(EBSToken, siteID, _workspaceAddIn.AccountID, _gContext, _rContext);
                if (_workspaceAddIn.billTo.cust_account_id == 0)
                {
                    MessageBox.Show("No Bill To Selected. Charges cannot poroceed. Please refresh incident and select BillTo");
                    DisableCharges();
                }
            }

            // Populate Ship to values from in context Account. May be altered by the user. 
            BillTo shipTo = new BillTo();
            // Find the first valid address
            foreach (IOrgAddr address in org.Oaddr)
            {
                if (address.AddrStreet != null)
                {
                    shipTo.address1 = address.AddrStreet;
                    shipTo.city = address.AddrCity != null ? address.AddrCity : "";
                    shipTo.state = address.AddrProvID != null ? address.AddrProvID.Value.ToString() : "";
                    shipTo.postal_code = address.AddrPostalCode != null ? address.AddrPostalCode : "";
                    shipTo.customer_name = org.Name;
                    shipTo.cust_account_id = _workspaceAddIn.AccountID;
                    shipTo.cust_acct_site_id = WorkspaceChargesAddIn.getEBSSiteID(_rContext, _workspaceAddIn.ChargeOrganization);
                    shipTo.account_number = accountNumber;
                    break;
                }
            }

            ChargeLines line = new ChargeLines();
            line.ChargeLine = ChargeLinesGrid.RowCount + 1;
            // Populate default values
            chargeLines.Add(ChargeLines.populateDefaultLine(line, this, _workspaceAddIn));

            ChargeLinesGrid.DataSource = null;
            ChargeLinesGrid.DataSource = chargeLines;

            // Make the last line (theoretically the one just added) selected
            for (int i = 0; i < ChargeLinesGrid.Rows.Count; i++)
            {
                ChargeLinesGrid.Rows[i].Selected = false;
            }
            ChargeLinesGrid.Rows[ChargeLinesGrid.Rows.Count - 1].Selected = true;

            // Make all rows that were submitted read only
            foreach (DataGridViewRow row in ChargeLinesGrid.Rows)
            {
                if (row.Cells[Status.Index].Value.ToString() == "Submitted")
                {
                    row.ReadOnly = true;
                    row.DefaultCellStyle.BackColor = Color.Gray;
                }
            }
        }

        /// <summary>
        /// Collects the various lines, does validation on the data contained and submits them to the charges module. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitCharges_Click(object sender, EventArgs e)
        {
            // Clear previous errors before submitting
            foreach(ChargeLines line in chargeLines)
            {
                if (line.Status != "Submitted")
                {
                    line.Status = "";
                }
            }
            // Refresh the grid
            ChargeLinesGrid.DataSource = null;
            ChargeLinesGrid.DataSource = chargeLines;

            // Call to validate the data before continuing
            ChargeSubmission preValidate = ValidateCharges(chargeLines, _workspaceAddIn, _workspaceAddIn.ChargeIncident as IIncident);
            
            if (preValidate.overall_status != "S")
            {
                if (preValidate.Errors != null)
                {
                    MessageBox.Show("There were errors in your charge lines. Please see status for more information");
                    foreach (Error err in preValidate.Errors)
                    {
                        int lineToUpdate = err.line_number - 1;
                        // Update the value in the status to equal Error: + Error message
                        try
                        {
                            chargeLines[err.line_number - 1].Status = $"Error(s): {err.error_message}";
                        }
                        catch (IndexOutOfRangeException)
                        {
                            MessageBox.Show($"Line {err.line_number} not found in charge lines");
                        }
                    }
                }
                else
                {
                    //MessageBox.Show("There were errors in your charge lines. Please see status for more information");
                }
                

                // Refresh the grid
                ChargeLinesGrid.DataSource = null;
                ChargeLinesGrid.DataSource = chargeLines;

                // And bail on submitting charges. 
                return;
            }

            // We only want to submit lines that have not currently been successfully submitted or is in an estimate type, so make a subest of just those items
            List<ChargeLines> chargesToSubmit = new List<ChargeLines>();
            foreach (ChargeLines charge in chargeLines)
            {
                if (charge.Status != "Submitted" && charge.LineType != "Estimate")
                {
                    chargesToSubmit.Add(charge);
                }
            }

            // If no lines exist, bail
            if (chargesToSubmit.Count == 0)
            {
                MessageBox.Show("Cannot submit charges when no lines have been added");
                return;
            }

            int siteID = WorkspaceChargesAddIn.getEBSSiteID(_rContext, _workspaceAddIn.ChargeOrganization);

            // Construct Charges JSON
            JObject charges =
                new JObject(
                    new JProperty("Charges",
                        new JArray(
                            from charge in chargesToSubmit
                            select new JObject(
                                new JProperty("user", charge.User),
                                new JProperty("line_number", charge.ChargeLine),
                                new JProperty("incident_id", charge.IncidentID),
                                new JProperty("incident_number", charge.IncidentReference),
                                new JProperty("incident_type_id", charge.IncidentTypeID),
                                new JProperty("incident_category_id", charge.IncidentCategoryID),
                                new JProperty("parent_incident_id", charge.ParentIncidentID),
                                new JProperty("parent_incident_number", charge.ParentIncidentReference),
                                new JProperty("business_process", charge.BusinessProcess),
                                new JProperty("service_activity", charge.ServiceActivity),
                                new JProperty("price_list_header_id", charge.PriceListHeader),
                                new JProperty("inventory_item_id", charge.ItemID),
                                new JProperty("quantity_required", charge.Quantity),
                                new JProperty("list_price", charge.ListPrice),
                                new JProperty("orig_selling_price", charge.OriginalSellingPrice),
                                new JProperty("selling_price", charge.SellingPrice),
                                new JProperty("after_warranty_cost", charge.ExtendedPrice),
                                new JProperty("customer_product_id", charge.ItemInstanceID),
                                new JProperty("purchase_order_num", PONumber.Text),
                                new JProperty("return_reason_code", charge.ReturnReason),
                                new JProperty("price_override_reason", charge.OverrideReason),
                                new JProperty("serial_number", charge.SerialNumber),
                                new JProperty("invoice_to_account_id", charge.BillToAccountID),
                                new JProperty("invoice_to_site_id", charge.BillToSiteID),
                                new JProperty("ship_to_account_id", charge.ShipToAccountID),
                                new JProperty("ship_to_site_id", charge.ShipToSiteID),
                                new JProperty("ship_to_contact", ShipToContact.Text),
                                new JProperty("ship_to_phone", ShipToPhone.Text),
                                new JProperty("shipping_method", charge.ShippingMethod),
                                new JProperty("freight_terms", charge.FreightTerms),
                                new JProperty("no_charge_flag", ConvertToYN(charge.ZeroCharge)),
                                new JProperty("incident_account_id", _workspaceAddIn.AccountID),
                                new JProperty("incident_org_contact_id", siteID),
                                new JProperty("contract_id", charge.ContractID),
                                new JProperty("contract_line_id", charge.ContractLineID),
                                new JProperty("coverage_term_name", charge.CoverageTermName),
                                new JProperty("packing_instructions", charge.PackingInstructions),
                                new JProperty("shipping_instructions", charge.ShippingInstructions),
                                new JProperty("charge_source", charge.ChargeSource)
                            )
                        )
                    )
                );

            string payload = charges.ToString();

            // Verify token is still valid. If not, get a new one. 
            CheckToken();

            // Make Web Service call to SubmitCharges
            ChargeSubmission chargeResult = WorkspaceChargesAddIn.SubmitCharges(EBSToken, _gContext, payload, _rContext);

            // If we got an error on submission, populate Status and pop an error
            if (chargeResult.overall_status != "S")
            {
                if (chargeResult.Errors == null)
                {
                    // We Didnt get line level errors
                    if (chargeResult.overall_diagnostics != null)
                    {
                        Debug.WriteLine(">>> ChargesUI.SubmitCharges_Click chargeResult = " + chargeResult);
                        MessageBox.Show("Error on Charges Submission. No further information given.");
                    }
                    else
                    {
                        MessageBox.Show($"Error on Charges Submission: {chargeResult.overall_diagnostics}");
                    }

                }
                else
                {
                    MessageBox.Show("Error on Charges Submission. Please Review Status Column for each line to review/fix errors and resubmit");
                    foreach (Error err in chargeResult.Errors)
                    {
                        int lineToUpdate = err.line_number - 1;
                        // Update the value in the status to equal Error: + Error message
                        try
                        {
                            chargeLines[err.line_number - 1].Status = $"Error(s): {err.error_message}";
                        }
                        catch (IndexOutOfRangeException)
                        {
                            MessageBox.Show($"Line {err.line_number} not found in charge lines");
                        }

                    }

                    // Refresh the grid
                    ChargeLinesGrid.DataSource = null;
                    ChargeLinesGrid.DataSource = chargeLines;
                }
            }
            else
            {
                // Handle for success
                foreach (ChargeLines charge in chargeLines)
                {
                    // Only mark submitted if we sent it to EBS
                    foreach (ChargeLines subbedCharge in chargesToSubmit)
                    {
                        if (subbedCharge.ChargeLine == charge.ChargeLine)
                        {
                            charge.Status = "Submitted";
                        }
                    }
                }

                // Stamp the Sales order ID to the charge line in question
                if (chargeResult.Orders != null)
                {
                    foreach (Order order in chargeResult.Orders)
                    {
                        foreach (ChargeLines line in chargeLines)
                        {
                            if (order.line_number == line.ChargeLine)
                            {
                                line.SalesOrderNumber = order.order_number;
                            }
                        }
                    }
                }
                
                // Lock the PO Button
                PONumber.Enabled = false;

                // Refresh the grid
                ChargeLinesGrid.DataSource = null;
                ChargeLinesGrid.DataSource = chargeLines;
                // Make all rows that were submitted read only
                foreach (DataGridViewRow row in ChargeLinesGrid.Rows)
                {
                    if (row.Cells[Status.Index].Value.ToString() == "Submitted")
                    {
                        row.ReadOnly = true;
                        row.DefaultCellStyle.BackColor = Color.Gray;
                    }
                }

                // Save the Charge Lines to the Incident forcefully. 
            }
        }

        /// <summary>
        /// Constructs pre submission validation for the charges before submitting to EBS. All validations gathered in this function
        /// </summary>
        /// <param name="chargeLines">Collection of charge lines to validat</param>
        /// <param name="workspaceAddIn">reference to the Workspace Add In</param>
        /// <returns>collection of errors and total status of chargelines grid</returns>
        private ChargeSubmission ValidateCharges(BindingList<ChargeLines> chargeLines, WorkspaceChargesAddIn workspaceAddIn, IIncident incident)
        {
            ChargeSubmission preChargeResults = new ChargeSubmission();
            preChargeResults.overall_status = "S";

            foreach (ChargeLines charge in chargeLines)
            {
                // Qty must not be 0
                if (charge.Quantity == 0)
                {
                    if (preChargeResults.Errors == null)
                    {
                        preChargeResults.Errors = new List<Error>();
                    }

                    preChargeResults.overall_status = "E";
                    Error err = new Error();
                    err.error_message = "Line Quantity must not be zero.";
                    err.line_number = charge.ChargeLine;
                    
                    preChargeResults.Errors.Add(err);
                }

                // If price overriden, override reason is required
                if (charge.ZeroCharge == true && charge.OverrideReason == null)
                {
                    if (preChargeResults.Errors == null)
                    {
                        preChargeResults.Errors = new List<Error>();
                    }

                    preChargeResults.overall_status = "E";
                    preChargeResults.Errors.Add(new Error
                    {
                        error_message = "Overriden prices require an override reason.",
                        line_number = charge.ChargeLine
                    });
                }

                // If Actual total exceeds NTE, do not submit
                decimal NTEAmount;
                decimal ActualTotalDecimal;
                string strNTE = workspaceAddIn.getCAValue(incident, "MoodCustom$PO_Amount");
                decimal.TryParse(strNTE, out NTEAmount);
                decimal.TryParse(ActualValue.Text, out ActualTotalDecimal);
                
                if(NTEAmount < ActualTotalDecimal)
                {
                    preChargeResults.overall_status = "E";
                    MessageBox.Show($"Order total exceeds current NTE / PO Amount of ${NTEAmount}. Please seek necessary approvals and update NTE / PO Amount to equal or exceed the order total.");
                }

                // If qty is negative, return reason is required

            }

            return preChargeResults;
        }

        /// <summary>
        /// EBS takes boolean values as "Y" and "N" instead of true and false. This will return the addin value in the format EBS expects
        /// </summary>
        /// <param name="zeroCharge"></param>
        /// <returns>String "Y" or "N" based on True/False</returns>
        private string ConvertToYN(bool attribute)
        {
            if (attribute == true)
            {
                return "Y";
            }
            else
            {
                return "N";
            }
        }

        /// <summary>
        /// Shows/Hides relevant columns of the Datagrid when the Items button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Button clicked</param>
        private void ItemsButton_Click(object sender, EventArgs e)
        {
            // Highlight selected button
            Button b = (Button)sender;
            b.BackColor = Color.DodgerBlue;

            foreach (Button bt in b.Parent.Controls.OfType<Button>())
            {
                if (bt != b)
                    bt.BackColor = Color.LightGray;
            }
            // Columns to Show:
            string[] show = new string[11] {
                "ChargeLine",
                "LineType",
                "Status",
                "OperatingUnit",
                "Business Process",
                "ServiceActivity",
                "ItemNumber",
                "ItemName",
                "Quantity",
                "UOM",
                "ReturnReason"
            };

            foreach (DataGridViewColumn column in ChargeLinesGrid.Columns)
            {
                if (show.Contains(column.Name)){
                    column.Visible = true;
                }
                else
                {
                    column.Visible = false;
                }
            }
        }

        /// <summary>
        /// Shows/Hides relevant columns of the Datagrid when the Pricing button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Button clicked</param>
        private void PricingButton_Click(object sender, EventArgs e)
        {
            // Highlight selected button
            Button b = (Button)sender;
            b.BackColor = Color.DodgerBlue;

            foreach (Button bt in b.Parent.Controls.OfType<Button>())
            {
                if (bt != b)
                    bt.BackColor = Color.LightGray;
            }
            // Columns to Show:
            string[] show = new string[12] {
                "ChargeLine",
                "ItemNumber",
                "Quantity",
                "UOM",
                "PriceList",
                "ZeroCharge",
                "OverrideReason",
                "ListPrice",
                "SellingPrice",
                "ExtendedPrice",
                "BillingCurrency",
                "OverridePriceList"
            };

            foreach (DataGridViewColumn column in ChargeLinesGrid.Columns)
            {
                if (show.Contains(column.Name))
                {
                    column.Visible = true;
                }
                else
                {
                    column.Visible = false;
                }
            }
        }

        /// <summary>
        /// Shows/Hides relevant columns of the Datagrid when the Billing button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Button clicked</param>
        private void BillingButton_Click(object sender, EventArgs e)
        {
            // Highlight selected button
            Button b = (Button)sender;
            b.BackColor = Color.DodgerBlue;

            foreach (Button bt in b.Parent.Controls.OfType<Button>())
            {
                if (bt != b)
                    bt.BackColor = Color.LightGray;
            }
            // Columns to Show:
            string[] show = new string[6] {
                "ChargeLine",
                "ItemNumber",
                "BillToAccountNumber",
                "BillToName",
                "BillToAddress1",
                "SalesOrderNumber"
            };

            {
            foreach (DataGridViewColumn column in ChargeLinesGrid.Columns)
                if (show.Contains(column.Name))
                {
                    column.Visible = true;
                }
                else
                {
                    column.Visible = false;
                }
            }
        }

        /// <summary>
        /// Shows/Hides relevant columns of the Datagrid when the Coverage button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Button clicked</param>
        private void CoverageButton_Click(object sender, EventArgs e)
        {
            // Highlight selected button
            Button b = (Button)sender;
            b.BackColor = Color.DodgerBlue;

            foreach (Button bt in b.Parent.Controls.OfType<Button>())
            {
                if (bt != b)
                    bt.BackColor = Color.LightGray;
            }
            // Columns to Show:
            string[] show = new string[7] {
                "ChargeLine",
                "ItemNumber",
                "ItemInstance",
                "ItemInstanceName",
                "ItemInstanceID",
                "SerialNumber",
                "CoverageTermName"
            };

            foreach (DataGridViewColumn column in ChargeLinesGrid.Columns)
            {
                if (show.Contains(column.Name))
                {
                    column.Visible = true;
                }
                else
                {
                    column.Visible = false;
                }
            }
        }

        /// <summary>
        /// Shows/Hides relevant columns of the Datagrid when the Shipping button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Button clicked</param>
        private void ShippingButton_Click(object sender, EventArgs e)
        {
            // Highlight selected button
            Button b = (Button)sender;
            b.BackColor = Color.DodgerBlue;

            foreach (Button bt in b.Parent.Controls.OfType<Button>())
            {
                if (bt != b)
                    bt.BackColor = Color.LightGray;
            }
            // Columns to Show:
            string[] show = new string[9] {
                "ChargeLine",
                "ItemNumber",
                "ShippingMethod",
                "FreightTerms",
                "ShippingInstructions",
                "PackingInstructions",
                "ShipToAccountNumber",
                "ShipToName",
                "ShipToAddress1"
            };

            foreach (DataGridViewColumn column in ChargeLinesGrid.Columns)
            {
                if (show.Contains(column.Name))
                {
                    column.Visible = true;
                }
                else
                {
                    column.Visible = false;
                }
            }
        }

        /// <summary>
        /// Handles for displaying a friendly error when a user inputs data into grid of the wrong datatype
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chargeLinesGrid_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {
            
            MessageBox.Show("Please enter the correct data form in this cell.");
            
            if (ChargeLinesGrid.CurrentCell is DataGridViewComboBoxCell)
            {
                //ChargeLinesGrid.Rows[anError.RowIndex].Cells[anError.ColumnIndex].Value =  ServiceActivity.Items[0];
            }
            else
            {
                //ChargeLinesGrid.Rows[anError.RowIndex].Cells[anError.ColumnIndex].Value = null;
            }
        }

        /// <summary>
        /// Click Handler for Process charges. When clicked, it pulls in the custom fields for hours worked/travelled as well 
        /// as inventory based debrief and creates a set of charge lines to add to charges. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessChargesBtn_Click(object sender, EventArgs e)
        {
            // Get Bill To if not already populated
            if (_workspaceAddIn.billTo == null)
            {
                _workspaceAddIn.billTo = WorkspaceChargesAddIn.GetBillTos(EBSToken, _workspaceAddIn.SiteID, _workspaceAddIn.AccountID, _gContext, _rContext);
            }

            // Update LOVs if not populated
            if (lovs == null)
            {
                UpdateLOVs();
            }

            Debrief debriefDetail = new Debrief(_gContext, _rContext, this, _workspaceAddIn);

            if (debriefDetail.charges.Count > 0)
            {
                PopulateDebrief(debriefDetail);
            }

            // Set the incident Debrif populated flag so that we don't do this on a second open of this incident. 
            IIncident incident = _workspaceAddIn.ChargeIncident;
            foreach (ICustomAttribute ca in incident.CustomAttributes)
            {
                var gf = ca.GenericField;
                if (gf.Name == "Integration$Debrief_Processed")
                {
                    gf.DataValue.Value = true;
                }
            }

            // Then disable the button
            processChargesBtn.Enabled = false;
        }

        /// <summary>
        /// Click handler for delete row. When  clicked, the selected row will be removed from the charge list and the grid. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteChargeBtn_Click(object sender, EventArgs e)
        {
            // Fetch the currently selected row(s)
            Int32 selectedRowCount = ChargeLinesGrid.Rows.GetRowCount(DataGridViewElementStates.Selected);
            // Only allowing for a single row at a time to be deleted
            if (selectedRowCount == 1)
            {
                for (int i = 0; i < selectedRowCount; i++)
                {
                    // We can only delete a row if it is not in status submitted, otherwise, throw an error and return
                    if (ChargeLinesGrid.SelectedRows[i].Cells[Status.Index].Value.ToString() != "Submitted")
                    {
                        int? chargeIndex = null;
                        // If status is not submitted, remove from the list of chargeLines
                        int.TryParse(ChargeLinesGrid.SelectedRows[i].Cells[ChargeLine.Index].Value.ToString(), out int lineToRemove);
                        for (int j=0; j < chargeLines.Count; j++)
                        {
                            if (chargeLines[j].ChargeLine == lineToRemove){
                                chargeIndex = j;
                            }
                        }

                        if (chargeIndex is int v2)
                        {
                            chargeLines.RemoveAt(v2);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cannot remove a submitted line");
                        return;
                    }
                    
                }
            }else if (selectedRowCount == 0)
            {
                MessageBox.Show("No rows are currently selected for deletion");
                return;
            }
            else
            {
                MessageBox.Show("Please only select a single row for deletion at a time");
                return;
            }
            ChargeLinesGrid.DataSource = null;
            ChargeLinesGrid.DataSource = chargeLines;

            // Make the last line selected
            for (int i = 0; i < ChargeLinesGrid.Rows.Count; i++)
            {
                ChargeLinesGrid.Rows[i].Selected = false;
            }
            if(ChargeLinesGrid.Rows.Count != 0)
            {
                ChargeLinesGrid.Rows[ChargeLinesGrid.Rows.Count - 1].Selected = true;
            }
        }
    }
}
