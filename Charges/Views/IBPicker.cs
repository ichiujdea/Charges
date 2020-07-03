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
    public partial class IBPicker : Form
    {
        private IGlobalContext _globalContext;
        private IRecordContext _recordContext;
        public string InstanceID { get; set; }
        public string ItemNumberRet { get; set; }
        public string SerialNumberRet { get; set; }
        public string ItemNameRet { get; set; }
        public IBPicker(IGlobalContext GlobalContext, IRecordContext _rContext, ReportResults results)
        {
            _recordContext = _rContext;
            _globalContext = GlobalContext;

            InitializeComponent();

            // First remove any previous records
            dataGridView1.Rows.Clear();

            // Populate the datagrid with returned rows
            foreach (List<string> row in results.rows)
            {
                // 0 - "Asset ID", 1 - "Product", 2 - "Product - Description", 3 - "Item Category", 4 - "Serial Number", 5 - "Instance Number", 6 - "Ebs_Item_Numb"
                dataGridView1.Rows.Add(
                    row[2] != null ? row[2] : "No Product",
                    row[3] != null ? row[3] : "No Category",
                    row[4] != null ? row[4] : "No Serial Number",
                    row[5] != null ? row[5] : "No Instance Number",
                    row[6] != null ? row[6] : "No Item Number");
            }
            Cursor.Current = Cursors.Default;
        }

        private void SelectIB(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                var selectedRow = this.dataGridView1.Rows[e.RowIndex];
                InstanceID = selectedRow.Cells[3].Value.ToString();
                SerialNumberRet = selectedRow.Cells[2].Value.ToString();
                ItemNumberRet = selectedRow.Cells[4].Value.ToString();
                ItemNameRet = selectedRow.Cells[0].Value.ToString();
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
