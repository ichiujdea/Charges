namespace Charges.Views
{
    partial class BillToPicker
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.BillToGrid = new System.Windows.Forms.DataGridView();
            this.custaccountidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accountnumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.custacctsiteidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partysiteidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partysitenumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.locationidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.address1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.postalcodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.postalplus4codeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.countyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.countryDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customer_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.billToBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.BillToGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.billToBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // BillToGrid
            // 
            this.BillToGrid.AllowUserToAddRows = false;
            this.BillToGrid.AllowUserToDeleteRows = false;
            this.BillToGrid.AutoGenerateColumns = false;
            this.BillToGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.BillToGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BillToGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.custaccountidDataGridViewTextBoxColumn,
            this.accountnumberDataGridViewTextBoxColumn,
            this.custacctsiteidDataGridViewTextBoxColumn,
            this.partysiteidDataGridViewTextBoxColumn,
            this.partysitenumberDataGridViewTextBoxColumn,
            this.locationidDataGridViewTextBoxColumn,
            this.address1DataGridViewTextBoxColumn,
            this.cityDataGridViewTextBoxColumn,
            this.postalcodeDataGridViewTextBoxColumn,
            this.postalplus4codeDataGridViewTextBoxColumn,
            this.stateDataGridViewTextBoxColumn,
            this.countyDataGridViewTextBoxColumn,
            this.countryDataGridViewTextBoxColumn,
            this.customer_name});
            this.BillToGrid.DataSource = this.billToBindingSource;
            this.BillToGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BillToGrid.Location = new System.Drawing.Point(0, 0);
            this.BillToGrid.Name = "BillToGrid";
            this.BillToGrid.ReadOnly = true;
            this.BillToGrid.RowHeadersVisible = false;
            this.BillToGrid.Size = new System.Drawing.Size(800, 450);
            this.BillToGrid.TabIndex = 0;
            this.BillToGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.BillToGrid_SelectBillTo);
            // 
            // custaccountidDataGridViewTextBoxColumn
            // 
            this.custaccountidDataGridViewTextBoxColumn.DataPropertyName = "cust_account_id";
            this.custaccountidDataGridViewTextBoxColumn.HeaderText = "Account ID";
            this.custaccountidDataGridViewTextBoxColumn.Name = "custaccountidDataGridViewTextBoxColumn";
            this.custaccountidDataGridViewTextBoxColumn.ReadOnly = true;
            this.custaccountidDataGridViewTextBoxColumn.Visible = false;
            // 
            // accountnumberDataGridViewTextBoxColumn
            // 
            this.accountnumberDataGridViewTextBoxColumn.DataPropertyName = "account_number";
            this.accountnumberDataGridViewTextBoxColumn.HeaderText = "Account Number";
            this.accountnumberDataGridViewTextBoxColumn.Name = "accountnumberDataGridViewTextBoxColumn";
            this.accountnumberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // custacctsiteidDataGridViewTextBoxColumn
            // 
            this.custacctsiteidDataGridViewTextBoxColumn.DataPropertyName = "cust_acct_site_id";
            this.custacctsiteidDataGridViewTextBoxColumn.HeaderText = "Site ID";
            this.custacctsiteidDataGridViewTextBoxColumn.Name = "custacctsiteidDataGridViewTextBoxColumn";
            this.custacctsiteidDataGridViewTextBoxColumn.ReadOnly = true;
            this.custacctsiteidDataGridViewTextBoxColumn.Visible = false;
            // 
            // partysiteidDataGridViewTextBoxColumn
            // 
            this.partysiteidDataGridViewTextBoxColumn.DataPropertyName = "party_site_id";
            this.partysiteidDataGridViewTextBoxColumn.HeaderText = "Party Site ID";
            this.partysiteidDataGridViewTextBoxColumn.Name = "partysiteidDataGridViewTextBoxColumn";
            this.partysiteidDataGridViewTextBoxColumn.ReadOnly = true;
            this.partysiteidDataGridViewTextBoxColumn.Visible = false;
            // 
            // partysitenumberDataGridViewTextBoxColumn
            // 
            this.partysitenumberDataGridViewTextBoxColumn.DataPropertyName = "party_site_number";
            this.partysitenumberDataGridViewTextBoxColumn.HeaderText = "Party Site Number";
            this.partysitenumberDataGridViewTextBoxColumn.Name = "partysitenumberDataGridViewTextBoxColumn";
            this.partysitenumberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // locationidDataGridViewTextBoxColumn
            // 
            this.locationidDataGridViewTextBoxColumn.DataPropertyName = "location_id";
            this.locationidDataGridViewTextBoxColumn.HeaderText = "Location ID";
            this.locationidDataGridViewTextBoxColumn.Name = "locationidDataGridViewTextBoxColumn";
            this.locationidDataGridViewTextBoxColumn.ReadOnly = true;
            this.locationidDataGridViewTextBoxColumn.Visible = false;
            // 
            // address1DataGridViewTextBoxColumn
            // 
            this.address1DataGridViewTextBoxColumn.DataPropertyName = "address1";
            this.address1DataGridViewTextBoxColumn.HeaderText = "Address";
            this.address1DataGridViewTextBoxColumn.Name = "address1DataGridViewTextBoxColumn";
            this.address1DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cityDataGridViewTextBoxColumn
            // 
            this.cityDataGridViewTextBoxColumn.DataPropertyName = "city";
            this.cityDataGridViewTextBoxColumn.HeaderText = "City";
            this.cityDataGridViewTextBoxColumn.Name = "cityDataGridViewTextBoxColumn";
            this.cityDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // postalcodeDataGridViewTextBoxColumn
            // 
            this.postalcodeDataGridViewTextBoxColumn.DataPropertyName = "postal_code";
            this.postalcodeDataGridViewTextBoxColumn.HeaderText = "Postal Code";
            this.postalcodeDataGridViewTextBoxColumn.Name = "postalcodeDataGridViewTextBoxColumn";
            this.postalcodeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // postalplus4codeDataGridViewTextBoxColumn
            // 
            this.postalplus4codeDataGridViewTextBoxColumn.DataPropertyName = "postal_plus4_code";
            this.postalplus4codeDataGridViewTextBoxColumn.HeaderText = "Postal + 4";
            this.postalplus4codeDataGridViewTextBoxColumn.Name = "postalplus4codeDataGridViewTextBoxColumn";
            this.postalplus4codeDataGridViewTextBoxColumn.ReadOnly = true;
            this.postalplus4codeDataGridViewTextBoxColumn.Visible = false;
            // 
            // stateDataGridViewTextBoxColumn
            // 
            this.stateDataGridViewTextBoxColumn.DataPropertyName = "state";
            this.stateDataGridViewTextBoxColumn.HeaderText = "State";
            this.stateDataGridViewTextBoxColumn.Name = "stateDataGridViewTextBoxColumn";
            this.stateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // countyDataGridViewTextBoxColumn
            // 
            this.countyDataGridViewTextBoxColumn.DataPropertyName = "county";
            this.countyDataGridViewTextBoxColumn.HeaderText = "County";
            this.countyDataGridViewTextBoxColumn.Name = "countyDataGridViewTextBoxColumn";
            this.countyDataGridViewTextBoxColumn.ReadOnly = true;
            this.countyDataGridViewTextBoxColumn.Visible = false;
            // 
            // countryDataGridViewTextBoxColumn
            // 
            this.countryDataGridViewTextBoxColumn.DataPropertyName = "country";
            this.countryDataGridViewTextBoxColumn.HeaderText = "Country";
            this.countryDataGridViewTextBoxColumn.Name = "countryDataGridViewTextBoxColumn";
            this.countryDataGridViewTextBoxColumn.ReadOnly = true;
            this.countryDataGridViewTextBoxColumn.Visible = false;
            // 
            // customer_name
            // 
            this.customer_name.DataPropertyName = "customer_name";
            this.customer_name.HeaderText = "Party Name";
            this.customer_name.Name = "customer_name";
            this.customer_name.ReadOnly = true;
            // 
            // billToBindingSource
            // 
            this.billToBindingSource.DataSource = typeof(Charges.Models.BillTo);
            // 
            // BillToPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BillToGrid);
            this.Name = "BillToPicker";
            this.Text = "Address Search";
            ((System.ComponentModel.ISupportInitialize)(this.BillToGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.billToBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView BillToGrid;
        private System.Windows.Forms.BindingSource billToBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn custaccountidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountnumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn custacctsiteidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn partysiteidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn partysitenumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn locationidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn address1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn postalcodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn postalplus4codeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn countyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn countryDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer_name;
    }
}