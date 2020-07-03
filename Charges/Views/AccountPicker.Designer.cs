namespace Charges.Views
{
    partial class AccountPicker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountPicker));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblAcctName = new System.Windows.Forms.Label();
            this.lblEBSCustNumber = new System.Windows.Forms.Label();
            this.lblStreetAddr = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.AccountNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameAcct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Street = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.City = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.State = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SiteID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.searchAccounts = new System.Windows.Forms.Button();
            this.accountName = new System.Windows.Forms.TextBox();
            this.customerNumber = new System.Windows.Forms.TextBox();
            this.streetAddress = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.04288F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.95712F));
            this.tableLayoutPanel1.Controls.Add(this.lblAcctName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblEBSCustNumber, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblStreetAddr, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.searchAccounts, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.accountName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.customerNumber, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.streetAddress, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox4, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(583, 490);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblAcctName
            // 
            this.lblAcctName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAcctName.AutoSize = true;
            this.lblAcctName.Location = new System.Drawing.Point(3, 8);
            this.lblAcctName.Name = "lblAcctName";
            this.lblAcctName.Size = new System.Drawing.Size(100, 17);
            this.lblAcctName.TabIndex = 1;
            this.lblAcctName.Text = "Account Name";
            // 
            // lblEBSCustNumber
            // 
            this.lblEBSCustNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEBSCustNumber.AutoSize = true;
            this.lblEBSCustNumber.Location = new System.Drawing.Point(3, 42);
            this.lblEBSCustNumber.Name = "lblEBSCustNumber";
            this.lblEBSCustNumber.Size = new System.Drawing.Size(122, 17);
            this.lblEBSCustNumber.TabIndex = 2;
            this.lblEBSCustNumber.Text = "Customer Number";
            // 
            // lblStreetAddr
            // 
            this.lblStreetAddr.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStreetAddr.AutoSize = true;
            this.lblStreetAddr.Location = new System.Drawing.Point(3, 76);
            this.lblStreetAddr.Name = "lblStreetAddr";
            this.lblStreetAddr.Size = new System.Drawing.Size(102, 17);
            this.lblStreetAddr.TabIndex = 3;
            this.lblStreetAddr.Text = "Street Address";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "label4";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AccountNumber,
            this.NameAcct,
            this.Street,
            this.City,
            this.State,
            this.SiteID,
            this.AccountID});
            this.tableLayoutPanel1.SetColumnSpan(this.dataGridView1, 2);
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 172);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(577, 316);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.selectAddress);
            // 
            // AccountNumber
            // 
            this.AccountNumber.HeaderText = "Account Number";
            this.AccountNumber.MinimumWidth = 6;
            this.AccountNumber.Name = "AccountNumber";
            this.AccountNumber.ReadOnly = true;
            // 
            // NameAcct
            // 
            this.NameAcct.HeaderText = "Account Name";
            this.NameAcct.MinimumWidth = 6;
            this.NameAcct.Name = "NameAcct";
            this.NameAcct.ReadOnly = true;
            // 
            // Street
            // 
            this.Street.HeaderText = "Street";
            this.Street.MinimumWidth = 6;
            this.Street.Name = "Street";
            this.Street.ReadOnly = true;
            // 
            // City
            // 
            this.City.HeaderText = "City";
            this.City.MinimumWidth = 6;
            this.City.Name = "City";
            this.City.ReadOnly = true;
            // 
            // State
            // 
            this.State.HeaderText = "State";
            this.State.MinimumWidth = 6;
            this.State.Name = "State";
            this.State.ReadOnly = true;
            // 
            // SiteID
            // 
            this.SiteID.HeaderText = "Site ID";
            this.SiteID.MinimumWidth = 6;
            this.SiteID.Name = "SiteID";
            this.SiteID.ReadOnly = true;
            this.SiteID.Visible = false;
            // 
            // AccountID
            // 
            this.AccountID.HeaderText = "AccountID";
            this.AccountID.MinimumWidth = 6;
            this.AccountID.Name = "AccountID";
            this.AccountID.ReadOnly = true;
            this.AccountID.Visible = false;
            // 
            // searchAccounts
            // 
            this.searchAccounts.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.searchAccounts.Location = new System.Drawing.Point(505, 141);
            this.searchAccounts.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.searchAccounts.Name = "searchAccounts";
            this.searchAccounts.Size = new System.Drawing.Size(75, 23);
            this.searchAccounts.TabIndex = 6;
            this.searchAccounts.Text = "Search";
            this.searchAccounts.UseVisualStyleBackColor = true;
            this.searchAccounts.Click += new System.EventHandler(this.searchAccounts_Click);
            // 
            // accountName
            // 
            this.accountName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accountName.Location = new System.Drawing.Point(148, 2);
            this.accountName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.accountName.Name = "accountName";
            this.accountName.Size = new System.Drawing.Size(432, 22);
            this.accountName.TabIndex = 7;
            this.accountName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.accountSearchEnter);
            // 
            // customerNumber
            // 
            this.customerNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customerNumber.Location = new System.Drawing.Point(148, 36);
            this.customerNumber.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.customerNumber.Name = "customerNumber";
            this.customerNumber.Size = new System.Drawing.Size(432, 22);
            this.customerNumber.TabIndex = 8;
            this.customerNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.accountSearchEnter);
            // 
            // streetAddress
            // 
            this.streetAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.streetAddress.Location = new System.Drawing.Point(148, 70);
            this.streetAddress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.streetAddress.Name = "streetAddress";
            this.streetAddress.Size = new System.Drawing.Size(432, 22);
            this.streetAddress.TabIndex = 9;
            this.streetAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.accountSearchEnter);
            // 
            // textBox4
            // 
            this.textBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox4.Location = new System.Drawing.Point(148, 104);
            this.textBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(432, 22);
            this.textBox4.TabIndex = 10;
            // 
            // AccountPicker
            // 
            this.AcceptButton = this.searchAccounts;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 490);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "AccountPicker";
            this.Text = "Address Search";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblAcctName;
        private System.Windows.Forms.Label lblEBSCustNumber;
        private System.Windows.Forms.Label lblStreetAddr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button searchAccounts;
        private System.Windows.Forms.TextBox accountName;
        private System.Windows.Forms.TextBox customerNumber;
        private System.Windows.Forms.TextBox streetAddress;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameAcct;
        private System.Windows.Forms.DataGridViewTextBoxColumn Street;
        private System.Windows.Forms.DataGridViewTextBoxColumn City;
        private System.Windows.Forms.DataGridViewTextBoxColumn State;
        private System.Windows.Forms.DataGridViewTextBoxColumn SiteID;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountID;
    }
}