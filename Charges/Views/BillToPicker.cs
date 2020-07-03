using Charges.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Charges.Views
{
    public partial class BillToPicker : Form
    {
        public string BillToAcctID { get; set; }
        public string BillToAcctNum { get; set; }
        public string BillToSiteID { get; set; }
        public string BillToPartySiteID { get; set; }
        public string BillToPartySiteNum { get; set; }
        public string BillToLocationID { get; set; }
        public string BillToStreet { get; set; }
        public string BillToCity { get; set; }
        public string BillToZip { get; set; }
        public string BillToZip4 { get; set; }
        public string BillToState { get; set; }
        public string BillToCounty { get; set; }
        public string BillToCountry { get; set; }
        public string BillToName { get; set; }
        public List<BillTo> billToAddrs;
        public BillToPicker(BillToAddresses addrs)
        {
            // First, remove previous rows - Note: this may not be needed as the BillTo Picker only happens once?
            // billToAddrs.Clear();

            // Bind a list of values to the datagrid
            billToAddrs = new List<BillTo>();
            foreach (BillTo addr in addrs.BillTo)
            {
                billToAddrs.Add(new BillTo
                {
                    address1 = addr.address1,
                    cust_account_id = addr.cust_account_id,
                    account_number = addr.account_number,
                    cust_acct_site_id = addr.cust_acct_site_id,
                    party_site_id = addr.party_site_id,
                    party_site_number = addr.party_site_number,
                    location_id = addr.location_id,
                    city = addr.city,
                    postal_code = addr.postal_code,
                    postal_plus4_code = addr.postal_plus4_code,
                    state = addr.state,
                    country = addr.country,
                    county = addr.county,
                    customer_name = addr.customer_name
                });
            }
            var bindingList = new BindingList<BillTo>(billToAddrs);
            var bindingSource = new BindingSource(bindingList, null);
            
            InitializeComponent();
            BillToGrid.DataSource = bindingSource;

            
        }


        /// <summary>
        /// Return the selected row to Workspace Addin to be used for billto rows added to Charges
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BillToGrid_SelectBillTo(object sender, DataGridViewCellEventArgs e)
        {
            var selectedRow = BillToGrid.Rows[e.RowIndex];
            BillToAcctID = selectedRow.Cells[0].Value != null ? selectedRow.Cells[0].Value.ToString() : "0";
            BillToAcctNum = selectedRow.Cells[1].Value != null ? selectedRow.Cells[1].Value.ToString() : "No Account Number";
            BillToSiteID = selectedRow.Cells[2].Value != null ? selectedRow.Cells[2].Value.ToString() : "0";
            BillToPartySiteID = selectedRow.Cells[3].Value != null ? selectedRow.Cells[3].Value.ToString() : "0";
            BillToPartySiteNum = selectedRow.Cells[4].Value != null ? selectedRow.Cells[4].Value.ToString() : "No Site Number";
            BillToLocationID = selectedRow.Cells[5].Value != null ? selectedRow.Cells[5].Value.ToString() : "0";
            BillToStreet = selectedRow.Cells[6].Value != null ? selectedRow.Cells[6].Value.ToString() : "No Street";
            BillToCity = selectedRow.Cells[7].Value != null ? selectedRow.Cells[7].Value.ToString() : "No City";
            BillToZip = selectedRow.Cells[8].Value != null ? selectedRow.Cells[8].Value.ToString() : "No Zip Code";
            BillToZip4 = selectedRow.Cells[9].Value != null ? selectedRow.Cells[9].Value.ToString() : "";
            BillToState = selectedRow.Cells[10].Value != null ? selectedRow.Cells[10].Value.ToString() : "No State";
            BillToCounty = selectedRow.Cells[11].Value != null ? selectedRow.Cells[11].Value.ToString() : "No County";
            BillToCountry = selectedRow.Cells[12].Value != null ? selectedRow.Cells[12].Value.ToString() : "No Country";
            BillToName = selectedRow.Cells[13].Value != null ? selectedRow.Cells[13].Value.ToString() : "No Account Name";
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}