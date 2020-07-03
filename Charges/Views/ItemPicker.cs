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
    public partial class ItemPicker : Form
    {
        private IGlobalContext _globalContext;
        public string Product { get; set; }
        public string Model { get; set; }
        public int EBSProductID { get; set; }
        public string UnitOfMeasure { get; set; }

        public ItemPicker(IGlobalContext GlobalContext)
        {
            _globalContext = GlobalContext;
            InitializeComponent();
        }

        private void productSearchButton_Click(object sender, EventArgs e)
        {
            // Clear previous results
            dataGridView1.Rows.Clear();

            Cursor.Current = Cursors.WaitCursor;
            // Conduct a ROQL search of SalesProducts using the value in the text box
            ReportResults products = WorkspaceChargesAddIn.SearchProducts(prodNameSearch.Text, prodNumSearch.Text, _globalContext);

            foreach (List<string> row in products.rows)
            {
                // 0 - "EBS Item Number", 1 - "EBS Item ID", 2 - "EBS Item Name", 3 - "Unit of Measure", 4 - "", 5 - "", 6 - ""
                dataGridView1.Rows.Add(
                    row[0] != null ? row[0] : "No Item Number",
                    row[1] != null ? row[1] : "No Item ID",
                    row[2] != null ? row[2] : "No Item Name",
                    row[3] != null ? row[3] : "No Unit of Measure"
                );
            }
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Handles for hitting enter in the search box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void productSearchEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                productSearchButton_Click(this, new EventArgs());
            }
        }

        /// <summary>
        /// Returns the selected rows Model and Name to the calling form, then closes the item picker. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Event Args</param>
        private void SelectProduct(object sender, DataGridViewCellEventArgs e)
        {
            var selectedRow = this.dataGridView1.Rows[e.RowIndex];
            Product = selectedRow.Cells[2].Value.ToString();
            Model = selectedRow.Cells[0].Value.ToString();
            EBSProductID = Convert.ToInt32(selectedRow.Cells[1].Value.ToString());
            UnitOfMeasure = selectedRow.Cells[3].Value.ToString();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
