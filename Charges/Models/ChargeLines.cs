using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charges.Models
{
    public class ChargesObject
    {
        public ChargeHeader header { get; set; }
        public List<ChargeLines> lines { get; set; }
    }

    public class ChargeHeader
    {
        public string PurchaseOrderNumber { get; set; }
        public string ShipToContact { get; set; }
        public string ShipToPhone { get; set; }
        public decimal EstimatedTotal { get; set; }
        public decimal ActualTotal { get; set; }
        public decimal TotalTotal { get; set; }
    }

    public class ChargeLines
    {
        // Needed for Submit Charges
        public int ChargeLine { get; set; }
        public int? IncidentTypeID { get; set; }
        public int? IncidentCategoryID { get; set; }
        public int? ParentIncidentID { get; set; }
        public string ParentIncidentReference { get; set; }
        public string Status { get; set; }
        public string OperatingUnit { get; set; }
        public string BusinessProcess { get; set; }
        public string ServiceActivity { get; set; }
        public string Item { get; set; }
        public string ItemNumber { get; set; }
        // Needed for Submit Charges
        public int ItemID { get; set; }
        public int PrevItemID { get; set; }
        public string ItemInstance { get; set; }
        public string ItemInstanceName { get; set; }
        // Needed for Submit Charges as "customer_product_id"
        public int? ItemInstanceID { get; set; }
        public int? PrevItemInstanceID { get; set; }
        // Needed for Submit Charges
        public string SerialNumber { get; set; }
        // Needed for Submit Charges
        public string UOM { get; set; }
        // Needed for Submit Charges
        public float Quantity { get; set; }
        // Needed for Submit Charges
        public string ReturnReason { get; set; }
        // Needed for Submit Charges
        public string ShippingMethod { get; set; }
        public string BillingCurrency { get; set; }
        // Returned from Entitlements. Needed for Submit Charges
        public decimal ListPrice { get; set; }
        // Returned as Selling Price from Entitlements. User Changeable. Needed for Submit Charges
        public decimal OverrideUnitPrice { get; set; }
        // Returned as Selling Price from Entitlements. Needed for Submit Charges. This stores the original price
        public decimal SellingPrice { get; set; }
        public decimal OriginalSellingPrice { get; set; }
        // Qty * Selling Price. Needed for Submit Charges as "after_warranty_cost"
        public decimal ExtendedPrice { get; set; }
        public decimal ContractDiscount { get; set; }
        public string ContractNumber { get; set; }
        public string ContractNumberModifier { get; set; }
        // ContractID and ContractLine Returned from Get Entitlements if the item is still under warrantee
        public int ContractID { get; set; }
        public string ContractLineID { get; set; }
        public string CoverageTermName { get; set; }
        // Not needed. Possibly store the pre override selling price here?
        public decimal NetPrice { get; set; }
        // Needed for Submit Charges
        public bool ZeroCharge { get; set; }
        public string OverrideReason { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        // In context EBS Account ID. Needed for Submit Charges
        public int? AccountID { get; set; }
        public string ShipToName { get; set;}
        // Needed for Submit Charges as "ship_to_account_id"
        public int? ShipToAccountID { get; set; }
        public string ShipToAccountNumber { get; set; }
        // Needed for Submit Charges as "ship_to_org_id"
        public long ShipToSiteID { get; set; }
        public string ShipToAddress1 { get; set; }
        public string ShipToAddress2 { get; set; }
        public string ShipToCity { get; set; }
        public string ShipToState { get; set; }
        public string ShipToZip { get; set; }
        public string ShippingInstructions { get; set; }
        public string PackingInstructions { get; set; }
        public string FreightTerms { get; set; }
        public string BillToName { get; set; }
        // Needed for Submit Charges as "invoice_to_account_id"
        public int? BillToAccountID { get; set; }
        // Needed for Submit Charges as "invoice_to_org_id"
        public long BillToSiteID { get; set; }
        public string BillToAccountNumber { get; set; }
        public string BillToAddress1 { get; set; }
        public string BillToAddress2 { get; set; }
        public string BillToCity { get; set; }
        public string BillToState { get; set; }
        public string BillToZip { get; set; }
        public string StoreNumber { get; set; }
        // User Name Passed to EBS. Needed for Submit Charges
        public string User { get; set; }
        // Needed for Submit Charges
        public int IncidentID { get; set; }
        // Needed for Submit Charges
        public string IncidentReference { get; set; }
        // In Context Site ID
        public int OrgID { get; set; }
        // Returned from Entitlements. Needed for Submit Charges - Is sent as list_header_id
        public int PriceListHeader { get; set; }
        public string PreviousPriceList { get; set; }
        public string PriceList { get; set; }
        // Needed for Submit Charges
        public string PurchaseOrder { get; set; }
        public string LineType { get; set; }
        public string ChargeSource { get; set; }
        // Returned from Submit Charges
        public int SalesOrderNumber { get; set; }

        /// <summary>
        /// All new lines added by the system require certain basic attributes filled in as a starting point. This function collects all defaults in one place to make line creation easier
        /// </summary>
        /// <param name="line">An empty ChargeLine Object</param>
        /// <param name="siteID">In context Site ID</param>
        /// <param name="charges">Array of charge lines to add</param>
        /// <returns></returns>
        public static ChargeLines populateDefaultLine(ChargeLines line, Views.ChargesUI charges, WorkspaceChargesAddIn workspaceAddIn)
        {
            line.Status = "New";
            line.OperatingUnit = workspaceAddIn.OperatingUnit;
            line.BusinessProcess = workspaceAddIn.BusinessProcessValue;
            line.AccountName = workspaceAddIn.AccountName;
            line.AccountNumber = workspaceAddIn.AccountNumber;
            line.AccountID = workspaceAddIn.AccountID;
            line.ShipToName = workspaceAddIn.shipTo.customer_name;
            line.ShipToAccountID = workspaceAddIn.shipTo.cust_account_id;
            line.ShipToAccountNumber = workspaceAddIn.shipTo.account_number;
            line.ShipToSiteID = workspaceAddIn.shipTo.cust_acct_site_id;
            line.ShipToAddress1 = workspaceAddIn.shipTo.address1;
            line.ShipToCity = workspaceAddIn.shipTo.city;
            line.ShipToState = workspaceAddIn.shipTo.state;
            line.ShipToZip = workspaceAddIn.shipTo.postal_code;
            line.BillToName = workspaceAddIn.billTo.customer_name;
            line.BillToAccountID = workspaceAddIn.billTo.cust_account_id;
            line.BillToSiteID = workspaceAddIn.billTo.cust_acct_site_id;
            line.BillToAccountNumber = workspaceAddIn.billTo.account_number;
            line.BillToAddress1 = workspaceAddIn.billTo.address1;
            line.BillToCity = workspaceAddIn.billTo.city;
            line.BillToState = workspaceAddIn.billTo.state;
            line.BillToZip = workspaceAddIn.billTo.postal_code;
            line.ShippingMethod = workspaceAddIn.DefaultShippingValue;
            line.User = workspaceAddIn.OSVCUser;
            line.IncidentID = workspaceAddIn.incident_ID;
            line.IncidentReference = workspaceAddIn.incident_number;
            line.ParentIncidentID = workspaceAddIn.parent_incident_id;
            line.ParentIncidentReference = workspaceAddIn.parent_incident_number;
            line.IncidentCategoryID = workspaceAddIn.incident_category_ID;
            line.IncidentTypeID = workspaceAddIn.incident_type_ID;
            line.OrgID = workspaceAddIn.SiteID;
            line.ChargeSource = "OSVC";
            line.LineType = "Actual";
            return line;
        }
    }
}
