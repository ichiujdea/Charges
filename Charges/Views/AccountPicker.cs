using Charges.Models;
using RightNow.AddIns.AddInViews;
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
    public partial class AccountPicker : Form
    {
        private IGlobalContext _globalContext;
        private IRecordContext _recordContext;
        public string RetAccountNumber { get; set; }
        public int? RetAccountID { get; set; }
        public string RetAccountName { get; set; }
        public string RetSiteID { get; set; }
        public string RetSiteNumber { get; set; }
        public string RetStreet { get; set; }
        public string RetCity { get; set; }
        public string RetState { get; set; }
        public string RetZip { get; set; }
        public AccountPicker(IGlobalContext GlobalContext, IRecordContext _rContext)
        {
            _recordContext = _rContext;
            _globalContext = GlobalContext;

            InitializeComponent();
        }

        private void searchAccounts_Click(object sender, EventArgs e)
        {
            // If search performed with no data, error and bail
            if (accountName.Text == "" && streetAddress.Text == "" && customerNumber.Text == "")
            {
                MessageBox.Show("Please search on at least one value to return a list of accounts.");
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            ReportResults results = WorkspaceChargesAddIn.SearchAccounts(_recordContext, _globalContext, accountName.Text, streetAddress.Text, customerNumber.Text);

            if (results.rows.Count == 0)
            {
                // No rows returned. Bail. 
                return;
            }

            // Clear previous results
            dataGridView1.Rows.Clear();

            // Populate the datagrid with returned rows
            foreach (List<string> row in results.rows)
            {
                // 0 - "Account Number", 1 - "Account Name", 2 - "Street", 3 - "City", 4 - "State", 5 - "Site ID", 6 - "Account ID"
                dataGridView1.Rows.Add(
                    row[0] != null ? row[0] : "No Account Number",
                    row[1] != null ? row[1] : "No Account Name",
                    row[2] != null ? row[2] : "No Street",
                    row[3] != null ? row[3] : "No City",
                    row[4] != null ? row[4] : "No State",
                    row[5] != null ? row[5] : "0",
                    row[6] != null ? row[6] : "0");
            }
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Handles for hitting enter in the search box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accountSearchEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchAccounts_Click(this, new EventArgs());
            }
        }

        private void selectAddress(object sender, DataGridViewCellEventArgs e)
        {
            var selectedRow = this.dataGridView1.Rows[e.RowIndex];
            RetAccountName = selectedRow.Cells[1].Value.ToString();
            RetAccountNumber = selectedRow.Cells[0].Value.ToString();
            RetStreet = selectedRow.Cells[2].Value.ToString();
            RetCity = selectedRow.Cells[3].Value.ToString();
            RetState = selectedRow.Cells[4].Value.ToString();
            RetSiteID = selectedRow.Cells[5].Value.ToString();
            RetAccountID = Int32.Parse(selectedRow.Cells[6].Value.ToString());
            DialogResult = DialogResult.OK;
            Close();
        }

    }
}
