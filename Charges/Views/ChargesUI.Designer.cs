using System;
using System.Drawing;
using System.Windows.Forms;

namespace Charges.Views
{
    partial class ChargesUI
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ActualTotal = new System.Windows.Forms.Label();
            this.AddChargeLine = new System.Windows.Forms.Button();
            this.SubmitCharges = new System.Windows.Forms.Button();
            this.ItemsButton = new System.Windows.Forms.Button();
            this.CoverageButton = new System.Windows.Forms.Button();
            this.ShippingButton = new System.Windows.Forms.Button();
            this.BillingButton = new System.Windows.Forms.Button();
            this.PricingButton = new System.Windows.Forms.Button();
            this.EstimateTotal = new System.Windows.Forms.Label();
            this.Total = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.TotalValue = new System.Windows.Forms.Label();
            this.ActualValue = new System.Windows.Forms.Label();
            this.EstimateValue = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ChargeLinesGrid = new System.Windows.Forms.DataGridView();
            this.ChargeLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LineType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OperatingUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServiceActivity = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UOM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReturnReason = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.BusinessProcess = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OverridePriceList = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PriceList = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ZeroCharge = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.OverrideReason = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ListPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SellingPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExtendedPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillingCurrency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.User = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IncidentID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IncidentReference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PriceListHeader = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NetPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemInstance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemInstanceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemInstanceID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SerialNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CoverageTermName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PurchaseOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShippingMethod = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.FreightTerms = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ShippingInstructions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PackingInstructions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShipToAccountNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShipToName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShipToAddress1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShipToAccountID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShipToSiteID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShipToCity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShipToState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShipToZip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContractDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillToAccountID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillToAccountNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillToSiteID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillToName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillToAddress1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillToAddress2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillToCity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillToState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillToZip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SalesOrderNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContractNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContractNumberModifier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContractID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContractLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrigSellingPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EntitledIB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrevItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PreviousPriceList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chargeLinesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.deleteChargeBtn = new System.Windows.Forms.Button();
            this.processChargesBtn = new System.Windows.Forms.Button();
            this.ShipToPhone = new System.Windows.Forms.TextBox();
            this.ShipToPhnLbl = new System.Windows.Forms.Label();
            this.ShipToContact = new System.Windows.Forms.TextBox();
            this.PONumber = new System.Windows.Forms.TextBox();
            this.ShipToContLbl = new System.Windows.Forms.Label();
            this.SalesRep = new System.Windows.Forms.Label();
            this.POLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.productsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.chargesUIBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChargeLinesGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chargeLinesBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.productsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chargesUIBindingSource)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ActualTotal
            // 
            this.ActualTotal.AutoSize = true;
            this.ActualTotal.Location = new System.Drawing.Point(3, 48);
            this.ActualTotal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ActualTotal.Name = "ActualTotal";
            this.ActualTotal.Size = new System.Drawing.Size(64, 13);
            this.ActualTotal.TabIndex = 1;
            this.ActualTotal.Text = "Actual Total";
            // 
            // AddChargeLine
            // 
            this.AddChargeLine.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.AddChargeLine.Location = new System.Drawing.Point(277, 4);
            this.AddChargeLine.Name = "AddChargeLine";
            this.AddChargeLine.Size = new System.Drawing.Size(66, 27);
            this.AddChargeLine.TabIndex = 2;
            this.AddChargeLine.Text = "Add";
            this.AddChargeLine.UseVisualStyleBackColor = true;
            this.AddChargeLine.Click += new System.EventHandler(this.AddChargeLine_Click);
            // 
            // SubmitCharges
            // 
            this.SubmitCharges.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.SubmitCharges.Location = new System.Drawing.Point(250, 75);
            this.SubmitCharges.Name = "SubmitCharges";
            this.SubmitCharges.Size = new System.Drawing.Size(93, 27);
            this.SubmitCharges.TabIndex = 3;
            this.SubmitCharges.Text = "Submit Charges";
            this.SubmitCharges.UseVisualStyleBackColor = true;
            this.SubmitCharges.Click += new System.EventHandler(this.SubmitCharges_Click);
            // 
            // ItemsButton
            // 
            this.ItemsButton.Location = new System.Drawing.Point(3, 3);
            this.ItemsButton.Name = "ItemsButton";
            this.ItemsButton.Size = new System.Drawing.Size(61, 27);
            this.ItemsButton.TabIndex = 0;
            this.ItemsButton.Text = "Items";
            this.ItemsButton.UseVisualStyleBackColor = true;
            this.ItemsButton.Click += new System.EventHandler(this.ItemsButton_Click);
            // 
            // CoverageButton
            // 
            this.CoverageButton.Location = new System.Drawing.Point(70, 3);
            this.CoverageButton.Name = "CoverageButton";
            this.CoverageButton.Size = new System.Drawing.Size(62, 27);
            this.CoverageButton.TabIndex = 1;
            this.CoverageButton.Text = "Coverage";
            this.CoverageButton.UseVisualStyleBackColor = true;
            this.CoverageButton.Click += new System.EventHandler(this.CoverageButton_Click);
            // 
            // ShippingButton
            // 
            this.ShippingButton.Location = new System.Drawing.Point(206, 3);
            this.ShippingButton.Name = "ShippingButton";
            this.ShippingButton.Size = new System.Drawing.Size(63, 27);
            this.ShippingButton.TabIndex = 2;
            this.ShippingButton.Text = "Shipping";
            this.ShippingButton.UseVisualStyleBackColor = true;
            this.ShippingButton.Click += new System.EventHandler(this.ShippingButton_Click);
            // 
            // BillingButton
            // 
            this.BillingButton.Location = new System.Drawing.Point(275, 3);
            this.BillingButton.Name = "BillingButton";
            this.BillingButton.Size = new System.Drawing.Size(65, 27);
            this.BillingButton.TabIndex = 4;
            this.BillingButton.Text = "Billing";
            this.BillingButton.UseVisualStyleBackColor = true;
            this.BillingButton.Click += new System.EventHandler(this.BillingButton_Click);
            // 
            // PricingButton
            // 
            this.PricingButton.Location = new System.Drawing.Point(138, 3);
            this.PricingButton.Name = "PricingButton";
            this.PricingButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.PricingButton.Size = new System.Drawing.Size(62, 27);
            this.PricingButton.TabIndex = 5;
            this.PricingButton.Text = "Pricing";
            this.PricingButton.UseVisualStyleBackColor = true;
            this.PricingButton.Click += new System.EventHandler(this.PricingButton_Click);
            // 
            // EstimateTotal
            // 
            this.EstimateTotal.AutoSize = true;
            this.EstimateTotal.Location = new System.Drawing.Point(3, 24);
            this.EstimateTotal.Name = "EstimateTotal";
            this.EstimateTotal.Size = new System.Drawing.Size(74, 13);
            this.EstimateTotal.TabIndex = 9;
            this.EstimateTotal.Text = "Estimate Total";
            // 
            // Total
            // 
            this.Total.AutoSize = true;
            this.Total.Location = new System.Drawing.Point(3, 72);
            this.Total.Name = "Total";
            this.Total.Size = new System.Drawing.Size(31, 13);
            this.Total.TabIndex = 10;
            this.Total.Text = "Total";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.ItemsButton);
            this.panel2.Controls.Add(this.CoverageButton);
            this.panel2.Controls.Add(this.PricingButton);
            this.panel2.Controls.Add(this.BillingButton);
            this.panel2.Controls.Add(this.ShippingButton);
            this.panel2.Location = new System.Drawing.Point(3, 93);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1052, 35);
            this.panel2.TabIndex = 11;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.TotalValue);
            this.panel3.Controls.Add(this.ActualValue);
            this.panel3.Controls.Add(this.EstimateValue);
            this.panel3.Controls.Add(this.Total);
            this.panel3.Controls.Add(this.ActualTotal);
            this.panel3.Controls.Add(this.EstimateTotal);
            this.panel3.Location = new System.Drawing.Point(6, 406);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(228, 107);
            this.panel3.TabIndex = 12;
            // 
            // TotalValue
            // 
            this.TotalValue.AutoSize = true;
            this.TotalValue.Location = new System.Drawing.Point(104, 72);
            this.TotalValue.Name = "TotalValue";
            this.TotalValue.Size = new System.Drawing.Size(28, 13);
            this.TotalValue.TabIndex = 13;
            this.TotalValue.Text = "0.00";
            // 
            // ActualValue
            // 
            this.ActualValue.AutoSize = true;
            this.ActualValue.Location = new System.Drawing.Point(104, 48);
            this.ActualValue.Name = "ActualValue";
            this.ActualValue.Size = new System.Drawing.Size(28, 13);
            this.ActualValue.TabIndex = 12;
            this.ActualValue.Text = "0.00";
            // 
            // EstimateValue
            // 
            this.EstimateValue.AutoSize = true;
            this.EstimateValue.Location = new System.Drawing.Point(104, 24);
            this.EstimateValue.Name = "EstimateValue";
            this.EstimateValue.Size = new System.Drawing.Size(28, 13);
            this.EstimateValue.TabIndex = 11;
            this.EstimateValue.Text = "0.00";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.AutoScroll = true;
            this.panel4.Controls.Add(this.ChargeLinesGrid);
            this.panel4.Location = new System.Drawing.Point(3, 134);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.panel4.Size = new System.Drawing.Size(1052, 266);
            this.panel4.TabIndex = 13;
            // 
            // ChargeLinesGrid
            // 
            this.ChargeLinesGrid.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(134)))), ((int)(((byte)(244)))));
            this.ChargeLinesGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ChargeLinesGrid.AutoGenerateColumns = false;
            this.ChargeLinesGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ChargeLinesGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.ChargeLinesGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ChargeLinesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ChargeLinesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ChargeLine,
            this.LineType,
            this.Status,
            this.ItemNumber,
            this.ItemName,
            this.OperatingUnit,
            this.ServiceActivity,
            this.Quantity,
            this.UOM,
            this.ReturnReason,
            this.BusinessProcess,
            this.OverridePriceList,
            this.PriceList,
            this.ZeroCharge,
            this.OverrideReason,
            this.ListPrice,
            this.SellingPrice,
            this.ExtendedPrice,
            this.BillingCurrency,
            this.User,
            this.IncidentID,
            this.IncidentReference,
            this.PriceListHeader,
            this.ItemID,
            this.NetPrice,
            this.ItemInstance,
            this.ItemInstanceName,
            this.ItemInstanceID,
            this.SerialNumber,
            this.CoverageTermName,
            this.PurchaseOrder,
            this.ShippingMethod,
            this.FreightTerms,
            this.ShippingInstructions,
            this.PackingInstructions,
            this.ShipToAccountNumber,
            this.ShipToName,
            this.ShipToAddress1,
            this.ShipToAccountID,
            this.ShipToSiteID,
            this.AccountID,
            this.ShipToCity,
            this.ShipToState,
            this.ShipToZip,
            this.dataGridViewTextBoxColumn1,
            this.ContractDiscount,
            this.BillToAccountID,
            this.BillToAccountNumber,
            this.BillToSiteID,
            this.BillToName,
            this.BillToAddress1,
            this.BillToAddress2,
            this.BillToCity,
            this.BillToState,
            this.BillToZip,
            this.SalesOrderNumber,
            this.ContractNumber,
            this.ContractNumberModifier,
            this.ContractID,
            this.ContractLine,
            this.OrigSellingPrice,
            this.EntitledIB,
            this.PrevItem,
            this.PreviousPriceList});
            this.ChargeLinesGrid.DataSource = this.chargeLinesBindingSource;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ChargeLinesGrid.DefaultCellStyle = dataGridViewCellStyle5;
            this.ChargeLinesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChargeLinesGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.ChargeLinesGrid.Location = new System.Drawing.Point(0, 0);
            this.ChargeLinesGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ChargeLinesGrid.Name = "ChargeLinesGrid";
            this.ChargeLinesGrid.RowHeadersVisible = false;
            this.ChargeLinesGrid.RowHeadersWidth = 51;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(134)))), ((int)(((byte)(234)))));
            this.ChargeLinesGrid.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.ChargeLinesGrid.RowTemplate.Height = 40;
            this.ChargeLinesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ChargeLinesGrid.Size = new System.Drawing.Size(1100, 266);
            this.ChargeLinesGrid.TabIndex = 1;
            this.ChargeLinesGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DisplayItemPicker);
            this.ChargeLinesGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.PopulatePricing);
            this.ChargeLinesGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.PriceOverride);
            this.ChargeLinesGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.PriceListOverride);
            this.ChargeLinesGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.GetEntitlements);
            this.ChargeLinesGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DefaultQuantity);
            this.ChargeLinesGrid.CurrentCellDirtyStateChanged += new System.EventHandler(this.DataGridView1_CurrentCellDirtyStateChanged);
            this.ChargeLinesGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.chargeLinesGrid_DataError);
            // 
            // ChargeLine
            // 
            this.ChargeLine.DataPropertyName = "ChargeLine";
            this.ChargeLine.FillWeight = 58.56211F;
            this.ChargeLine.HeaderText = "Line";
            this.ChargeLine.MinimumWidth = 6;
            this.ChargeLine.Name = "ChargeLine";
            this.ChargeLine.ReadOnly = true;
            // 
            // LineType
            // 
            this.LineType.DataPropertyName = "LineType";
            this.LineType.DropDownWidth = 150;
            this.LineType.FillWeight = 109.5111F;
            this.LineType.HeaderText = "Line Type";
            this.LineType.Items.AddRange(new object[] {
            "Actual",
            "Estimate"});
            this.LineType.MinimumWidth = 6;
            this.LineType.Name = "LineType";
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.FillWeight = 109.5111F;
            this.Status.HeaderText = "Status";
            this.Status.MinimumWidth = 6;
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // ItemNumber
            // 
            this.ItemNumber.DataPropertyName = "ItemNumber";
            this.ItemNumber.FillWeight = 109.5111F;
            this.ItemNumber.HeaderText = "Part Number";
            this.ItemNumber.MinimumWidth = 6;
            this.ItemNumber.Name = "ItemNumber";
            this.ItemNumber.ReadOnly = true;
            // 
            // ItemName
            // 
            this.ItemName.DataPropertyName = "Item";
            this.ItemName.FillWeight = 109.5111F;
            this.ItemName.HeaderText = "Description";
            this.ItemName.MinimumWidth = 6;
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            // 
            // OperatingUnit
            // 
            this.OperatingUnit.DataPropertyName = "OperatingUnit";
            this.OperatingUnit.FillWeight = 109.5111F;
            this.OperatingUnit.HeaderText = "OperatingUnit";
            this.OperatingUnit.MinimumWidth = 6;
            this.OperatingUnit.Name = "OperatingUnit";
            this.OperatingUnit.ReadOnly = true;
            // 
            // ServiceActivity
            // 
            this.ServiceActivity.DataPropertyName = "ServiceActivity";
            this.ServiceActivity.DividerWidth = 1;
            this.ServiceActivity.DropDownWidth = 150;
            this.ServiceActivity.FillWeight = 109.5111F;
            this.ServiceActivity.HeaderText = "Service Activity";
            this.ServiceActivity.MinimumWidth = 6;
            this.ServiceActivity.Name = "ServiceActivity";
            // 
            // Quantity
            // 
            this.Quantity.DataPropertyName = "Quantity";
            this.Quantity.FillWeight = 55.83757F;
            this.Quantity.HeaderText = "QTY";
            this.Quantity.MinimumWidth = 6;
            this.Quantity.Name = "Quantity";
            // 
            // UOM
            // 
            this.UOM.DataPropertyName = "UOM";
            this.UOM.FillWeight = 109.5111F;
            this.UOM.HeaderText = "UOM";
            this.UOM.MinimumWidth = 6;
            this.UOM.Name = "UOM";
            this.UOM.ReadOnly = true;
            // 
            // ReturnReason
            // 
            this.ReturnReason.DataPropertyName = "ReturnReason";
            this.ReturnReason.DropDownWidth = 150;
            this.ReturnReason.FillWeight = 109.5111F;
            this.ReturnReason.HeaderText = "Return Reason";
            this.ReturnReason.MinimumWidth = 6;
            this.ReturnReason.Name = "ReturnReason";
            this.ReturnReason.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ReturnReason.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // BusinessProcess
            // 
            this.BusinessProcess.DataPropertyName = "BusinessProcess";
            this.BusinessProcess.FillWeight = 109.5111F;
            this.BusinessProcess.HeaderText = "Business Process";
            this.BusinessProcess.MinimumWidth = 6;
            this.BusinessProcess.Name = "BusinessProcess";
            this.BusinessProcess.ReadOnly = true;
            this.BusinessProcess.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.BusinessProcess.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // OverridePriceList
            // 
            this.OverridePriceList.HeaderText = "Override Price List";
            this.OverridePriceList.MinimumWidth = 6;
            this.OverridePriceList.Name = "OverridePriceList";
            this.OverridePriceList.Visible = false;
            // 
            // PriceList
            // 
            this.PriceList.DataPropertyName = "PriceList";
            this.PriceList.HeaderText = "Price List";
            this.PriceList.MinimumWidth = 6;
            this.PriceList.Name = "PriceList";
            this.PriceList.ReadOnly = true;
            this.PriceList.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PriceList.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.PriceList.Visible = false;
            // 
            // ZeroCharge
            // 
            this.ZeroCharge.DataPropertyName = "ZeroCharge";
            this.ZeroCharge.HeaderText = "Override";
            this.ZeroCharge.MinimumWidth = 6;
            this.ZeroCharge.Name = "ZeroCharge";
            this.ZeroCharge.Visible = false;
            // 
            // OverrideReason
            // 
            this.OverrideReason.DataPropertyName = "OverrideReason";
            this.OverrideReason.HeaderText = "Override Reason";
            this.OverrideReason.MinimumWidth = 6;
            this.OverrideReason.Name = "OverrideReason";
            this.OverrideReason.ReadOnly = true;
            this.OverrideReason.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.OverrideReason.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.OverrideReason.Visible = false;
            // 
            // ListPrice
            // 
            this.ListPrice.DataPropertyName = "ListPrice";
            dataGridViewCellStyle2.Format = "0.00";
            this.ListPrice.DefaultCellStyle = dataGridViewCellStyle2;
            this.ListPrice.HeaderText = "List Price";
            this.ListPrice.MinimumWidth = 6;
            this.ListPrice.Name = "ListPrice";
            this.ListPrice.ReadOnly = true;
            this.ListPrice.Visible = false;
            // 
            // SellingPrice
            // 
            this.SellingPrice.DataPropertyName = "SellingPrice";
            dataGridViewCellStyle3.Format = "0.00";
            this.SellingPrice.DefaultCellStyle = dataGridViewCellStyle3;
            this.SellingPrice.HeaderText = "Selling Price";
            this.SellingPrice.MinimumWidth = 6;
            this.SellingPrice.Name = "SellingPrice";
            this.SellingPrice.ReadOnly = true;
            this.SellingPrice.Visible = false;
            // 
            // ExtendedPrice
            // 
            this.ExtendedPrice.DataPropertyName = "ExtendedPrice";
            dataGridViewCellStyle4.Format = "0.00";
            this.ExtendedPrice.DefaultCellStyle = dataGridViewCellStyle4;
            this.ExtendedPrice.HeaderText = "Extended Price";
            this.ExtendedPrice.MinimumWidth = 6;
            this.ExtendedPrice.Name = "ExtendedPrice";
            this.ExtendedPrice.ReadOnly = true;
            this.ExtendedPrice.Visible = false;
            // 
            // BillingCurrency
            // 
            this.BillingCurrency.DataPropertyName = "BillingCurrency";
            this.BillingCurrency.HeaderText = "Currency";
            this.BillingCurrency.MinimumWidth = 6;
            this.BillingCurrency.Name = "BillingCurrency";
            this.BillingCurrency.ReadOnly = true;
            this.BillingCurrency.Visible = false;
            // 
            // User
            // 
            this.User.DataPropertyName = "User";
            this.User.HeaderText = "User";
            this.User.MinimumWidth = 6;
            this.User.Name = "User";
            this.User.ReadOnly = true;
            this.User.Visible = false;
            // 
            // IncidentID
            // 
            this.IncidentID.DataPropertyName = "IncidentID";
            this.IncidentID.HeaderText = "Incident ID";
            this.IncidentID.MinimumWidth = 6;
            this.IncidentID.Name = "IncidentID";
            this.IncidentID.ReadOnly = true;
            this.IncidentID.Visible = false;
            // 
            // IncidentReference
            // 
            this.IncidentReference.DataPropertyName = "IncidentReference";
            this.IncidentReference.HeaderText = "Reference No";
            this.IncidentReference.MinimumWidth = 6;
            this.IncidentReference.Name = "IncidentReference";
            this.IncidentReference.ReadOnly = true;
            this.IncidentReference.Visible = false;
            // 
            // PriceListHeader
            // 
            this.PriceListHeader.DataPropertyName = "PriceListHeader";
            this.PriceListHeader.HeaderText = "Price List Header";
            this.PriceListHeader.MinimumWidth = 6;
            this.PriceListHeader.Name = "PriceListHeader";
            this.PriceListHeader.ReadOnly = true;
            this.PriceListHeader.Visible = false;
            // 
            // ItemID
            // 
            this.ItemID.DataPropertyName = "ItemID";
            this.ItemID.HeaderText = "Item ID";
            this.ItemID.MinimumWidth = 6;
            this.ItemID.Name = "ItemID";
            this.ItemID.ReadOnly = true;
            this.ItemID.Visible = false;
            // 
            // NetPrice
            // 
            this.NetPrice.DataPropertyName = "NetPrice";
            this.NetPrice.HeaderText = "NetPrice";
            this.NetPrice.MinimumWidth = 6;
            this.NetPrice.Name = "NetPrice";
            this.NetPrice.Visible = false;
            // 
            // ItemInstance
            // 
            this.ItemInstance.DataPropertyName = "ItemInstance";
            this.ItemInstance.HeaderText = "Item Instance";
            this.ItemInstance.MinimumWidth = 6;
            this.ItemInstance.Name = "ItemInstance";
            this.ItemInstance.ReadOnly = true;
            this.ItemInstance.Visible = false;
            // 
            // ItemInstanceName
            // 
            this.ItemInstanceName.DataPropertyName = "ItemInstanceName";
            this.ItemInstanceName.HeaderText = "Item Instance Desc";
            this.ItemInstanceName.MinimumWidth = 6;
            this.ItemInstanceName.Name = "ItemInstanceName";
            this.ItemInstanceName.ReadOnly = true;
            this.ItemInstanceName.Visible = false;
            // 
            // ItemInstanceID
            // 
            this.ItemInstanceID.DataPropertyName = "ItemInstanceID";
            this.ItemInstanceID.HeaderText = "Instance ID";
            this.ItemInstanceID.MinimumWidth = 6;
            this.ItemInstanceID.Name = "ItemInstanceID";
            this.ItemInstanceID.ReadOnly = true;
            this.ItemInstanceID.Visible = false;
            // 
            // SerialNumber
            // 
            this.SerialNumber.DataPropertyName = "SerialNumber";
            this.SerialNumber.HeaderText = "Serial Number";
            this.SerialNumber.MinimumWidth = 6;
            this.SerialNumber.Name = "SerialNumber";
            this.SerialNumber.ReadOnly = true;
            this.SerialNumber.Visible = false;
            // 
            // CoverageTermName
            // 
            this.CoverageTermName.DataPropertyName = "CoverageTermName";
            this.CoverageTermName.HeaderText = "Coverage";
            this.CoverageTermName.MinimumWidth = 6;
            this.CoverageTermName.Name = "CoverageTermName";
            this.CoverageTermName.ReadOnly = true;
            this.CoverageTermName.Visible = false;
            // 
            // PurchaseOrder
            // 
            this.PurchaseOrder.DataPropertyName = "PurchaseOrder";
            this.PurchaseOrder.HeaderText = "Purchase Order";
            this.PurchaseOrder.MinimumWidth = 6;
            this.PurchaseOrder.Name = "PurchaseOrder";
            this.PurchaseOrder.Visible = false;
            // 
            // ShippingMethod
            // 
            this.ShippingMethod.DataPropertyName = "ShippingMethod";
            this.ShippingMethod.DropDownWidth = 150;
            this.ShippingMethod.HeaderText = "Shipping Method";
            this.ShippingMethod.MinimumWidth = 6;
            this.ShippingMethod.Name = "ShippingMethod";
            this.ShippingMethod.Visible = false;
            // 
            // FreightTerms
            // 
            this.FreightTerms.DataPropertyName = "FreightTerms";
            this.FreightTerms.DropDownWidth = 150;
            this.FreightTerms.HeaderText = "Freight Terms";
            this.FreightTerms.MinimumWidth = 6;
            this.FreightTerms.Name = "FreightTerms";
            this.FreightTerms.Visible = false;
            // 
            // ShippingInstructions
            // 
            this.ShippingInstructions.DataPropertyName = "ShippingInstructions";
            this.ShippingInstructions.HeaderText = "Shipping Instructions";
            this.ShippingInstructions.MinimumWidth = 6;
            this.ShippingInstructions.Name = "ShippingInstructions";
            this.ShippingInstructions.Visible = false;
            // 
            // PackingInstructions
            // 
            this.PackingInstructions.DataPropertyName = "PackingInstructions";
            this.PackingInstructions.HeaderText = "Packing Instructions";
            this.PackingInstructions.MinimumWidth = 6;
            this.PackingInstructions.Name = "PackingInstructions";
            this.PackingInstructions.Visible = false;
            // 
            // ShipToAccountNumber
            // 
            this.ShipToAccountNumber.DataPropertyName = "ShipToAccountNumber";
            this.ShipToAccountNumber.HeaderText = "Ship-To Account Number";
            this.ShipToAccountNumber.MinimumWidth = 6;
            this.ShipToAccountNumber.Name = "ShipToAccountNumber";
            this.ShipToAccountNumber.ReadOnly = true;
            this.ShipToAccountNumber.Visible = false;
            // 
            // ShipToName
            // 
            this.ShipToName.DataPropertyName = "ShipToName";
            this.ShipToName.HeaderText = "Ship-To Party";
            this.ShipToName.MinimumWidth = 6;
            this.ShipToName.Name = "ShipToName";
            this.ShipToName.ReadOnly = true;
            this.ShipToName.Visible = false;
            // 
            // ShipToAddress1
            // 
            this.ShipToAddress1.DataPropertyName = "ShipToAddress1";
            this.ShipToAddress1.HeaderText = "Ship-To Address";
            this.ShipToAddress1.MinimumWidth = 6;
            this.ShipToAddress1.Name = "ShipToAddress1";
            this.ShipToAddress1.ReadOnly = true;
            this.ShipToAddress1.Visible = false;
            // 
            // ShipToAccountID
            // 
            this.ShipToAccountID.DataPropertyName = "ShipToAccountID";
            this.ShipToAccountID.HeaderText = "Ship-To Account ID";
            this.ShipToAccountID.MinimumWidth = 6;
            this.ShipToAccountID.Name = "ShipToAccountID";
            this.ShipToAccountID.ReadOnly = true;
            this.ShipToAccountID.Visible = false;
            // 
            // ShipToSiteID
            // 
            this.ShipToSiteID.DataPropertyName = "ShipToSiteID";
            this.ShipToSiteID.HeaderText = "Ship-To Site ID";
            this.ShipToSiteID.MinimumWidth = 6;
            this.ShipToSiteID.Name = "ShipToSiteID";
            this.ShipToSiteID.ReadOnly = true;
            this.ShipToSiteID.Visible = false;
            // 
            // AccountID
            // 
            this.AccountID.DataPropertyName = "AccountID";
            this.AccountID.HeaderText = "Account ID";
            this.AccountID.MinimumWidth = 6;
            this.AccountID.Name = "AccountID";
            this.AccountID.ReadOnly = true;
            this.AccountID.Visible = false;
            // 
            // ShipToCity
            // 
            this.ShipToCity.DataPropertyName = "ShipToCity";
            this.ShipToCity.HeaderText = "Ship-To City";
            this.ShipToCity.MinimumWidth = 6;
            this.ShipToCity.Name = "ShipToCity";
            this.ShipToCity.ReadOnly = true;
            this.ShipToCity.Visible = false;
            // 
            // ShipToState
            // 
            this.ShipToState.DataPropertyName = "ShipToState";
            this.ShipToState.HeaderText = "Ship-To State";
            this.ShipToState.MinimumWidth = 6;
            this.ShipToState.Name = "ShipToState";
            this.ShipToState.ReadOnly = true;
            this.ShipToState.Visible = false;
            // 
            // ShipToZip
            // 
            this.ShipToZip.DataPropertyName = "ShipToZip";
            this.ShipToZip.HeaderText = "Ship-To Zip";
            this.ShipToZip.MinimumWidth = 6;
            this.ShipToZip.Name = "ShipToZip";
            this.ShipToZip.ReadOnly = true;
            this.ShipToZip.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "BusinessProcess";
            this.dataGridViewTextBoxColumn1.HeaderText = "Business Process";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // ContractDiscount
            // 
            this.ContractDiscount.DataPropertyName = "ContractDiscount";
            this.ContractDiscount.HeaderText = "Contract Discount";
            this.ContractDiscount.MinimumWidth = 6;
            this.ContractDiscount.Name = "ContractDiscount";
            this.ContractDiscount.Visible = false;
            // 
            // BillToAccountID
            // 
            this.BillToAccountID.DataPropertyName = "BillToAccountID";
            this.BillToAccountID.HeaderText = "Bill-To Account ID";
            this.BillToAccountID.MinimumWidth = 6;
            this.BillToAccountID.Name = "BillToAccountID";
            this.BillToAccountID.ReadOnly = true;
            this.BillToAccountID.Visible = false;
            // 
            // BillToAccountNumber
            // 
            this.BillToAccountNumber.DataPropertyName = "BillToAccountNumber";
            this.BillToAccountNumber.HeaderText = "Bill-To Account";
            this.BillToAccountNumber.MinimumWidth = 6;
            this.BillToAccountNumber.Name = "BillToAccountNumber";
            this.BillToAccountNumber.ReadOnly = true;
            this.BillToAccountNumber.Visible = false;
            // 
            // BillToSiteID
            // 
            this.BillToSiteID.DataPropertyName = "BillToSiteID";
            this.BillToSiteID.HeaderText = "Bill-To Site ID";
            this.BillToSiteID.MinimumWidth = 6;
            this.BillToSiteID.Name = "BillToSiteID";
            this.BillToSiteID.ReadOnly = true;
            this.BillToSiteID.Visible = false;
            // 
            // BillToName
            // 
            this.BillToName.DataPropertyName = "BillToName";
            this.BillToName.HeaderText = "Bill-To Party";
            this.BillToName.MinimumWidth = 6;
            this.BillToName.Name = "BillToName";
            this.BillToName.ReadOnly = true;
            this.BillToName.Visible = false;
            // 
            // BillToAddress1
            // 
            this.BillToAddress1.DataPropertyName = "BillToAddress1";
            this.BillToAddress1.HeaderText = "Bill To Address";
            this.BillToAddress1.MinimumWidth = 6;
            this.BillToAddress1.Name = "BillToAddress1";
            this.BillToAddress1.ReadOnly = true;
            this.BillToAddress1.Visible = false;
            // 
            // BillToAddress2
            // 
            this.BillToAddress2.DataPropertyName = "BillToAddress2";
            this.BillToAddress2.HeaderText = "Bill-To Address 2";
            this.BillToAddress2.MinimumWidth = 6;
            this.BillToAddress2.Name = "BillToAddress2";
            this.BillToAddress2.ReadOnly = true;
            this.BillToAddress2.Visible = false;
            // 
            // BillToCity
            // 
            this.BillToCity.DataPropertyName = "BillToCity";
            this.BillToCity.HeaderText = "Bill-To City";
            this.BillToCity.MinimumWidth = 6;
            this.BillToCity.Name = "BillToCity";
            this.BillToCity.ReadOnly = true;
            this.BillToCity.Visible = false;
            // 
            // BillToState
            // 
            this.BillToState.DataPropertyName = "BillToState";
            this.BillToState.HeaderText = "Bill-To State";
            this.BillToState.MinimumWidth = 6;
            this.BillToState.Name = "BillToState";
            this.BillToState.ReadOnly = true;
            this.BillToState.Visible = false;
            // 
            // BillToZip
            // 
            this.BillToZip.DataPropertyName = "BillToZip";
            this.BillToZip.HeaderText = "Bill-To Zip";
            this.BillToZip.MinimumWidth = 6;
            this.BillToZip.Name = "BillToZip";
            this.BillToZip.ReadOnly = true;
            this.BillToZip.Visible = false;
            // 
            // SalesOrderNumber
            // 
            this.SalesOrderNumber.DataPropertyName = "SalesOrderNumber";
            this.SalesOrderNumber.HeaderText = "Sales Order";
            this.SalesOrderNumber.MaxInputLength = 50;
            this.SalesOrderNumber.MinimumWidth = 6;
            this.SalesOrderNumber.Name = "SalesOrderNumber";
            this.SalesOrderNumber.ReadOnly = true;
            // 
            // ContractNumber
            // 
            this.ContractNumber.DataPropertyName = "ContractNumber";
            this.ContractNumber.HeaderText = "Contract Number";
            this.ContractNumber.MinimumWidth = 6;
            this.ContractNumber.Name = "ContractNumber";
            this.ContractNumber.ReadOnly = true;
            this.ContractNumber.Visible = false;
            // 
            // ContractNumberModifier
            // 
            this.ContractNumberModifier.DataPropertyName = "ContractNumberModifier";
            this.ContractNumberModifier.HeaderText = "Contract Number Modifier";
            this.ContractNumberModifier.MinimumWidth = 6;
            this.ContractNumberModifier.Name = "ContractNumberModifier";
            this.ContractNumberModifier.ReadOnly = true;
            this.ContractNumberModifier.Visible = false;
            // 
            // ContractID
            // 
            this.ContractID.DataPropertyName = "ContractID";
            this.ContractID.HeaderText = "Contract ID";
            this.ContractID.MinimumWidth = 6;
            this.ContractID.Name = "ContractID";
            this.ContractID.ReadOnly = true;
            this.ContractID.Visible = false;
            // 
            // ContractLine
            // 
            this.ContractLine.DataPropertyName = "ContractLineID";
            this.ContractLine.HeaderText = "Contract Line";
            this.ContractLine.MinimumWidth = 6;
            this.ContractLine.Name = "ContractLine";
            this.ContractLine.ReadOnly = true;
            this.ContractLine.Visible = false;
            // 
            // OrigSellingPrice
            // 
            this.OrigSellingPrice.DataPropertyName = "SellingPrice";
            this.OrigSellingPrice.HeaderText = "Original Selling Price";
            this.OrigSellingPrice.MinimumWidth = 6;
            this.OrigSellingPrice.Name = "OrigSellingPrice";
            this.OrigSellingPrice.ReadOnly = true;
            this.OrigSellingPrice.Visible = false;
            // 
            // EntitledIB
            // 
            this.EntitledIB.DataPropertyName = "PrevItemInstanceID";
            this.EntitledIB.HeaderText = "Prev IB";
            this.EntitledIB.MinimumWidth = 6;
            this.EntitledIB.Name = "EntitledIB";
            this.EntitledIB.ReadOnly = true;
            this.EntitledIB.Visible = false;
            // 
            // PrevItem
            // 
            this.PrevItem.DataPropertyName = "PrevItemID";
            this.PrevItem.HeaderText = "Prev Item";
            this.PrevItem.MinimumWidth = 6;
            this.PrevItem.Name = "PrevItem";
            this.PrevItem.ReadOnly = true;
            this.PrevItem.Visible = false;
            // 
            // PreviousPriceList
            // 
            this.PreviousPriceList.DataPropertyName = "PreviousPriceList";
            this.PreviousPriceList.HeaderText = "PreviousPriceList";
            //this.PreviousPriceList.MinimumWidth = 8;
            this.PreviousPriceList.Name = "PreviousPriceList";
            this.PreviousPriceList.Visible = false;
            // 
            // chargeLinesBindingSource
            // 
            this.chargeLinesBindingSource.DataSource = typeof(Charges.Models.ChargeLines);
            // 
            // deleteChargeBtn
            // 
            this.deleteChargeBtn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.deleteChargeBtn.Location = new System.Drawing.Point(113, 4);
            this.deleteChargeBtn.Name = "deleteChargeBtn";
            this.deleteChargeBtn.Size = new System.Drawing.Size(57, 27);
            this.deleteChargeBtn.TabIndex = 5;
            this.deleteChargeBtn.Text = "Delete";
            this.deleteChargeBtn.UseVisualStyleBackColor = true;
            this.deleteChargeBtn.Click += new System.EventHandler(this.DeleteChargeBtn_Click);
            // 
            // processChargesBtn
            // 
            this.processChargesBtn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.processChargesBtn.Enabled = false;
            this.processChargesBtn.Location = new System.Drawing.Point(251, 39);
            this.processChargesBtn.Name = "processChargesBtn";
            this.processChargesBtn.Size = new System.Drawing.Size(92, 26);
            this.processChargesBtn.TabIndex = 4;
            this.processChargesBtn.Text = "Process Debrief";
            this.processChargesBtn.UseVisualStyleBackColor = true;
            this.processChargesBtn.Click += new System.EventHandler(this.ProcessChargesBtn_Click);
            // 
            // ShipToPhone
            // 
            this.ShipToPhone.Location = new System.Drawing.Point(792, 45);
            this.ShipToPhone.Name = "ShipToPhone";
            this.ShipToPhone.Size = new System.Drawing.Size(100, 20);
            this.ShipToPhone.TabIndex = 14;
            // 
            // ShipToPhnLbl
            // 
            this.ShipToPhnLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ShipToPhnLbl.AutoSize = true;
            this.ShipToPhnLbl.Location = new System.Drawing.Point(708, 42);
            this.ShipToPhnLbl.Name = "ShipToPhnLbl";
            this.ShipToPhnLbl.Size = new System.Drawing.Size(78, 13);
            this.ShipToPhnLbl.TabIndex = 12;
            this.ShipToPhnLbl.Text = "Ship To Phone";
            // 
            // ShipToContact
            // 
            this.ShipToContact.Location = new System.Drawing.Point(792, 3);
            this.ShipToContact.Name = "ShipToContact";
            this.ShipToContact.Size = new System.Drawing.Size(100, 20);
            this.ShipToContact.TabIndex = 13;
            // 
            // PONumber
            // 
            this.PONumber.Location = new System.Drawing.Point(266, 3);
            this.PONumber.Name = "PONumber";
            this.PONumber.Size = new System.Drawing.Size(100, 20);
            this.PONumber.TabIndex = 7;
            // 
            // ShipToContLbl
            // 
            this.ShipToContLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ShipToContLbl.AutoSize = true;
            this.ShipToContLbl.Location = new System.Drawing.Point(702, 0);
            this.ShipToContLbl.Name = "ShipToContLbl";
            this.ShipToContLbl.Size = new System.Drawing.Size(84, 13);
            this.ShipToContLbl.TabIndex = 11;
            this.ShipToContLbl.Text = "Ship To Contact";
            // 
            // SalesRep
            // 
            this.SalesRep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SalesRep.AutoSize = true;
            this.SalesRep.Location = new System.Drawing.Point(152, 48);
            this.SalesRep.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.SalesRep.Name = "SalesRep";
            this.SalesRep.Size = new System.Drawing.Size(108, 13);
            this.SalesRep.TabIndex = 8;
            this.SalesRep.Text = "Sales Representative";
            // 
            // POLabel
            // 
            this.POLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.POLabel.AutoSize = true;
            this.POLabel.Location = new System.Drawing.Point(139, 6);
            this.POLabel.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.POLabel.Name = "POLabel";
            this.POLabel.Size = new System.Drawing.Size(121, 13);
            this.POLabel.TabIndex = 6;
            this.POLabel.Text = "Purchase Order Number";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1052, 84);
            this.panel1.TabIndex = 5;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.POLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ShipToContact, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.ShipToPhone, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.ShipToContLbl, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.SalesRep, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.PONumber, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.ShipToPhnLbl, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1052, 84);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // productsBindingSource
            // 
            this.productsBindingSource.DataSource = typeof(Charges.Models.Products);
            // 
            // chargesUIBindingSource
            // 
            this.chargesUIBindingSource.DataSource = typeof(Charges.Views.ChargesUI);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.SubmitCharges, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.deleteChargeBtn, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.AddChargeLine, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.processChargesBtn, 1, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(699, 406);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(346, 107);
            this.tableLayoutPanel2.TabIndex = 15;
            // 
            // ChargesUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ChargesUI";
            this.Size = new System.Drawing.Size(1057, 534);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChargeLinesGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chargeLinesBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.productsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chargesUIBindingSource)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion
        private System.Windows.Forms.BindingSource chargeLinesBindingSource;
        private System.Windows.Forms.BindingSource productsBindingSource;
        private System.Windows.Forms.BindingSource chargesUIBindingSource;
        private System.Windows.Forms.Label ActualTotal;
        private System.Windows.Forms.Button AddChargeLine;
        private System.Windows.Forms.Button SubmitCharges;
        private System.Windows.Forms.Button ItemsButton;
        private System.Windows.Forms.Button CoverageButton;
        private System.Windows.Forms.Button ShippingButton;
        private System.Windows.Forms.Button BillingButton;
        private System.Windows.Forms.Button PricingButton;
        private System.Windows.Forms.Label EstimateTotal;
        private System.Windows.Forms.Label Total;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label ShipToPhnLbl;
        private System.Windows.Forms.Label ShipToContLbl;
        public System.Windows.Forms.Label SalesRep;
        private System.Windows.Forms.Label POLabel;
        private System.Windows.Forms.Panel panel1;
        public DataGridView ChargeLinesGrid;
        public Label TotalValue;
        public Label ActualValue;
        public Label EstimateValue;
        public TextBox ShipToPhone;
        public TextBox ShipToContact;
        public TextBox PONumber;
        public Button processChargesBtn;
        private Button deleteChargeBtn;
        private DataGridViewTextBoxColumn ChargeLine;
        private DataGridViewComboBoxColumn LineType;
        private DataGridViewTextBoxColumn Status;
        private DataGridViewTextBoxColumn ItemNumber;
        private DataGridViewTextBoxColumn ItemName;
        private DataGridViewTextBoxColumn OperatingUnit;
        private DataGridViewComboBoxColumn ServiceActivity;
        private DataGridViewTextBoxColumn Quantity;
        private DataGridViewTextBoxColumn UOM;
        private DataGridViewComboBoxColumn ReturnReason;
        private DataGridViewTextBoxColumn BusinessProcess;
        private DataGridViewCheckBoxColumn OverridePriceList;
        private DataGridViewComboBoxColumn PriceList;
        private DataGridViewCheckBoxColumn ZeroCharge;
        private DataGridViewComboBoxColumn OverrideReason;
        private DataGridViewTextBoxColumn ListPrice;
        private DataGridViewTextBoxColumn SellingPrice;
        private DataGridViewTextBoxColumn ExtendedPrice;
        private DataGridViewTextBoxColumn BillingCurrency;
        private DataGridViewTextBoxColumn User;
        private DataGridViewTextBoxColumn IncidentID;
        private DataGridViewTextBoxColumn IncidentReference;
        private DataGridViewTextBoxColumn PriceListHeader;
        private DataGridViewTextBoxColumn ItemID;
        private DataGridViewTextBoxColumn NetPrice;
        private DataGridViewTextBoxColumn ItemInstance;
        private DataGridViewTextBoxColumn ItemInstanceName;
        private DataGridViewTextBoxColumn ItemInstanceID;
        private DataGridViewTextBoxColumn SerialNumber;
        private DataGridViewTextBoxColumn CoverageTermName;
        private DataGridViewTextBoxColumn PurchaseOrder;
        private DataGridViewComboBoxColumn ShippingMethod;
        private DataGridViewComboBoxColumn FreightTerms;
        private DataGridViewTextBoxColumn ShippingInstructions;
        private DataGridViewTextBoxColumn PackingInstructions;
        private DataGridViewTextBoxColumn ShipToAccountNumber;
        private DataGridViewTextBoxColumn ShipToName;
        private DataGridViewTextBoxColumn ShipToAddress1;
        private DataGridViewTextBoxColumn ShipToAccountID;
        private DataGridViewTextBoxColumn ShipToSiteID;
        private DataGridViewTextBoxColumn AccountID;
        private DataGridViewTextBoxColumn ShipToCity;
        private DataGridViewTextBoxColumn ShipToState;
        private DataGridViewTextBoxColumn ShipToZip;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn ContractDiscount;
        private DataGridViewTextBoxColumn BillToAccountID;
        private DataGridViewTextBoxColumn BillToAccountNumber;
        private DataGridViewTextBoxColumn BillToSiteID;
        private DataGridViewTextBoxColumn BillToName;
        private DataGridViewTextBoxColumn BillToAddress1;
        private DataGridViewTextBoxColumn BillToAddress2;
        private DataGridViewTextBoxColumn BillToCity;
        private DataGridViewTextBoxColumn BillToState;
        private DataGridViewTextBoxColumn BillToZip;
        private DataGridViewTextBoxColumn SalesOrderNumber;
        private DataGridViewTextBoxColumn ContractNumber;
        private DataGridViewTextBoxColumn ContractNumberModifier;
        private DataGridViewTextBoxColumn ContractID;
        private DataGridViewTextBoxColumn ContractLine;
        private DataGridViewTextBoxColumn OrigSellingPrice;
        private DataGridViewTextBoxColumn EntitledIB;
        private DataGridViewTextBoxColumn PrevItem;
        private DataGridViewTextBoxColumn PreviousPriceList;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
    }
}
