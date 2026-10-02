namespace StocklyMobi
{
    partial class ServiceManagementForm
    {
        private System.ComponentModel.IContainer components=null;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabServices;
        private System.Windows.Forms.TabPage tabProviders;
        private System.Windows.Forms.FlowLayoutPanel _categoryBar;
        private System.Windows.Forms.Button btnMobileRecharge;
        private System.Windows.Forms.Button btnTransferBalance;
        private System.Windows.Forms.Button btnValidity;
        private System.Windows.Forms.Button btnInternetBundles;
        private System.Windows.Forms.Button btnApps;
        private System.Windows.Forms.SplitContainer _servicesSplit;
        private System.Windows.Forms.Panel pnlServicesList;
        private System.Windows.Forms.Label lblServicesListTitle;
        private System.Windows.Forms.DataGridView _servicesGrid;
        private System.Windows.Forms.Panel pnlServiceEditor;
        private System.Windows.Forms.Label _selectedCategoryLabel;
        private System.Windows.Forms.TableLayoutPanel tblServiceEditor;
        private System.Windows.Forms.Label lblServiceDescription;
        private System.Windows.Forms.ComboBox _serviceDescription;
        private System.Windows.Forms.Label lblServiceProvider;
        private System.Windows.Forms.ComboBox _serviceProvider;
        private System.Windows.Forms.Label lblServiceOperator;
        private System.Windows.Forms.ComboBox _serviceOperator;
        private System.Windows.Forms.Label lblServiceStatus;
        private System.Windows.Forms.CheckBox _serviceActive;
        private System.Windows.Forms.Label lblServiceCost;
        private System.Windows.Forms.NumericUpDown _serviceCost;
        private System.Windows.Forms.Label lblServiceCostLbp;
        private System.Windows.Forms.NumericUpDown _serviceCostLbp;
        private System.Windows.Forms.Label lblServiceDeducted;
        private System.Windows.Forms.NumericUpDown _serviceDeducted;
        private System.Windows.Forms.Label lblServiceDeductedLbp;
        private System.Windows.Forms.NumericUpDown _serviceDeductedLbp;
        private System.Windows.Forms.Label lblServicePrice;
        private System.Windows.Forms.NumericUpDown _servicePrice;
        private System.Windows.Forms.Label lblServicePriceLbp;
        private System.Windows.Forms.NumericUpDown _servicePriceLbp;
        private System.Windows.Forms.Label lblServiceExchangeRate;
        private System.Windows.Forms.Label lblServiceProfit;
        private System.Windows.Forms.Label _serviceProfit;
        private System.Windows.Forms.FlowLayoutPanel flowServiceActions;
        private System.Windows.Forms.Button btnNewService;
        private System.Windows.Forms.Button btnSaveService;
        private System.Windows.Forms.Button btnRefreshServices;
        private System.Windows.Forms.SplitContainer _providersSplit;
        private System.Windows.Forms.Panel pnlProvidersList;
        private System.Windows.Forms.Label lblProvidersTitle;
        private System.Windows.Forms.DataGridView _providersGrid;
        private System.Windows.Forms.Panel pnlProviderEditor;
        private System.Windows.Forms.TableLayoutPanel tblProviderEditor;
        private System.Windows.Forms.Label lblProviderName;
        private System.Windows.Forms.TextBox _providerName;
        private System.Windows.Forms.Label lblProviderCode;
        private System.Windows.Forms.TextBox _providerCode;
        private System.Windows.Forms.Label lblProviderType;
        private System.Windows.Forms.ComboBox _providerType;
        private System.Windows.Forms.Label lblProviderBalance;
        private System.Windows.Forms.Label _providerBalance;
        private System.Windows.Forms.Label lblProviderStatus;
        private System.Windows.Forms.CheckBox _providerActive;
        private System.Windows.Forms.Label lblBalanceAmount;
        private System.Windows.Forms.NumericUpDown _providerAdjustment;
        private System.Windows.Forms.Label lblBalanceAmountLbp;
        private System.Windows.Forms.NumericUpDown _providerAdjustmentLbp;
        private System.Windows.Forms.Label lblProviderNotes;
        private System.Windows.Forms.TextBox _providerNotes;
        private System.Windows.Forms.FlowLayoutPanel flowProviderActions;
        private System.Windows.Forms.Button btnNewProvider;
        private System.Windows.Forms.Button btnSaveProvider;
        private System.Windows.Forms.Button btnAddBalance;
        private System.Windows.Forms.Button btnRemoveBalance;
        protected override void Dispose(bool disposing){if(disposing&&components!=null)components.Dispose();
        base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.lblHeader = new System.Windows.Forms.Label();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabServices = new System.Windows.Forms.TabPage();
            this._servicesSplit = new System.Windows.Forms.SplitContainer();
            this.pnlServicesList = new System.Windows.Forms.Panel();
            this._servicesGrid = new System.Windows.Forms.DataGridView();
            this.lblServicesListTitle = new System.Windows.Forms.Label();
            this.pnlServiceEditor = new System.Windows.Forms.Panel();
            this.tblServiceEditor = new System.Windows.Forms.TableLayoutPanel();
            this.lblServiceDescription = new System.Windows.Forms.Label();
            this._serviceDescription = new System.Windows.Forms.ComboBox();
            this.lblServiceProvider = new System.Windows.Forms.Label();
            this._serviceProvider = new System.Windows.Forms.ComboBox();
            this.lblServiceOperator = new System.Windows.Forms.Label();
            this._serviceOperator = new System.Windows.Forms.ComboBox();
            this.lblServiceStatus = new System.Windows.Forms.Label();
            this._serviceActive = new System.Windows.Forms.CheckBox();
            this.lblServiceCost = new System.Windows.Forms.Label();
            this._serviceCost = new System.Windows.Forms.NumericUpDown();
            this.lblServiceDeducted = new System.Windows.Forms.Label();
            this._serviceDeducted = new System.Windows.Forms.NumericUpDown();
            this.lblServiceCostLbp = new System.Windows.Forms.Label();
            this._serviceCostLbp = new System.Windows.Forms.NumericUpDown();
            this.lblServiceDeductedLbp = new System.Windows.Forms.Label();
            this._serviceDeductedLbp = new System.Windows.Forms.NumericUpDown();
            this.lblServicePrice = new System.Windows.Forms.Label();
            this._servicePrice = new System.Windows.Forms.NumericUpDown();
            this.lblServiceProfit = new System.Windows.Forms.Label();
            this._serviceProfit = new System.Windows.Forms.Label();
            this.lblServicePriceLbp = new System.Windows.Forms.Label();
            this._servicePriceLbp = new System.Windows.Forms.NumericUpDown();
            this.lblServiceExchangeRate = new System.Windows.Forms.Label();
            this.flowServiceActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNewService = new System.Windows.Forms.Button();
            this.btnSaveService = new System.Windows.Forms.Button();
            this.btnRefreshServices = new System.Windows.Forms.Button();
            this._selectedCategoryLabel = new System.Windows.Forms.Label();
            this._categoryBar = new System.Windows.Forms.FlowLayoutPanel();
            this.btnMobileRecharge = new System.Windows.Forms.Button();
            this.btnTransferBalance = new System.Windows.Forms.Button();
            this.btnValidity = new System.Windows.Forms.Button();
            this.btnInternetBundles = new System.Windows.Forms.Button();
            this.btnApps = new System.Windows.Forms.Button();
            this.tabProviders = new System.Windows.Forms.TabPage();
            this._providersSplit = new System.Windows.Forms.SplitContainer();
            this.pnlProvidersList = new System.Windows.Forms.Panel();
            this._providersGrid = new System.Windows.Forms.DataGridView();
            this.lblProvidersTitle = new System.Windows.Forms.Label();
            this.pnlProviderEditor = new System.Windows.Forms.Panel();
            this.tblProviderEditor = new System.Windows.Forms.TableLayoutPanel();
            this.lblProviderName = new System.Windows.Forms.Label();
            this._providerName = new System.Windows.Forms.TextBox();
            this.lblProviderCode = new System.Windows.Forms.Label();
            this._providerCode = new System.Windows.Forms.TextBox();
            this.lblProviderType = new System.Windows.Forms.Label();
            this._providerType = new System.Windows.Forms.ComboBox();
            this.lblProviderBalance = new System.Windows.Forms.Label();
            this._providerBalance = new System.Windows.Forms.Label();
            this.lblProviderStatus = new System.Windows.Forms.Label();
            this._providerActive = new System.Windows.Forms.CheckBox();
            this.lblBalanceAmount = new System.Windows.Forms.Label();
            this._providerAdjustment = new System.Windows.Forms.NumericUpDown();
            this.lblBalanceAmountLbp = new System.Windows.Forms.Label();
            this._providerAdjustmentLbp = new System.Windows.Forms.NumericUpDown();
            this.lblProviderNotes = new System.Windows.Forms.Label();
            this._providerNotes = new System.Windows.Forms.TextBox();
            this.flowProviderActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNewProvider = new System.Windows.Forms.Button();
            this.btnSaveProvider = new System.Windows.Forms.Button();
            this.btnAddBalance = new System.Windows.Forms.Button();
            this.btnRemoveBalance = new System.Windows.Forms.Button();
            this.tabMain.SuspendLayout();
            this.tabServices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._servicesSplit)).BeginInit();
            this._servicesSplit.Panel1.SuspendLayout();
            this._servicesSplit.Panel2.SuspendLayout();
            this._servicesSplit.SuspendLayout();
            this.pnlServicesList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._servicesGrid)).BeginInit();
            this.pnlServiceEditor.SuspendLayout();
            this.tblServiceEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._serviceCost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._serviceDeducted)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._serviceCostLbp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._serviceDeductedLbp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._servicePrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._servicePriceLbp)).BeginInit();
            this.flowServiceActions.SuspendLayout();
            this._categoryBar.SuspendLayout();
            this.tabProviders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._providersSplit)).BeginInit();
            this._providersSplit.Panel1.SuspendLayout();
            this._providersSplit.Panel2.SuspendLayout();
            this._providersSplit.SuspendLayout();
            this.pnlProvidersList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._providersGrid)).BeginInit();
            this.pnlProviderEditor.SuspendLayout();
            this.tblProviderEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._providerAdjustment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._providerAdjustmentLbp)).BeginInit();
            this.flowProviderActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Padding = new System.Windows.Forms.Padding(18, 10, 10, 4);
            this.lblHeader.Size = new System.Drawing.Size(1180, 72);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Services";
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabServices);
            this.tabMain.Controls.Add(this.tabProviders);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.tabMain.Location = new System.Drawing.Point(0, 72);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Drawing.Point(18, 7);
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1180, 628);
            this.tabMain.TabIndex = 0;
            // 
            // tabServices
            // 
            this.tabServices.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.tabServices.Controls.Add(this._servicesSplit);
            this.tabServices.Controls.Add(this._categoryBar);
            this.tabServices.Location = new System.Drawing.Point(4, 33);
            this.tabServices.Name = "tabServices";
            this.tabServices.Padding = new System.Windows.Forms.Padding(8);
            this.tabServices.Size = new System.Drawing.Size(1172, 591);
            this.tabServices.TabIndex = 0;
            this.tabServices.Text = "Service Buttons";
            // 
            // _servicesSplit
            // 
            this._servicesSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this._servicesSplit.Location = new System.Drawing.Point(8, 72);
            this._servicesSplit.Name = "_servicesSplit";
            // 
            // _servicesSplit.Panel1
            // 
            this._servicesSplit.Panel1.Controls.Add(this.pnlServicesList);
            this._servicesSplit.Panel1.Padding = new System.Windows.Forms.Padding(2, 8, 2, 2);
            // 
            // _servicesSplit.Panel2
            // 
            this._servicesSplit.Panel2.Controls.Add(this.pnlServiceEditor);
            this._servicesSplit.Panel2.Padding = new System.Windows.Forms.Padding(2);
            this._servicesSplit.Size = new System.Drawing.Size(1156, 511);
            this._servicesSplit.SplitterDistance = 759;
            this._servicesSplit.SplitterWidth = 7;
            this._servicesSplit.TabIndex = 0;
            // 
            // pnlServicesList
            // 
            this.pnlServicesList.BackColor = System.Drawing.Color.White;
            this.pnlServicesList.Controls.Add(this._servicesGrid);
            this.pnlServicesList.Controls.Add(this.lblServicesListTitle);
            this.pnlServicesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlServicesList.Location = new System.Drawing.Point(2, 8);
            this.pnlServicesList.Name = "pnlServicesList";
            this.pnlServicesList.Padding = new System.Windows.Forms.Padding(14);
            this.pnlServicesList.Size = new System.Drawing.Size(755, 501);
            this.pnlServicesList.TabIndex = 0;
            // 
            // _servicesGrid
            // 
            this._servicesGrid.AllowUserToAddRows = false;
            this._servicesGrid.AllowUserToDeleteRows = false;
            this._servicesGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._servicesGrid.BackgroundColor = System.Drawing.Color.White;
            this._servicesGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._servicesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._servicesGrid.Location = new System.Drawing.Point(14, 46);
            this._servicesGrid.MultiSelect = false;
            this._servicesGrid.Name = "_servicesGrid";
            this._servicesGrid.ReadOnly = true;
            this._servicesGrid.RowHeadersVisible = false;
            this._servicesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._servicesGrid.Size = new System.Drawing.Size(727, 441);
            this._servicesGrid.TabIndex = 0;
            this._servicesGrid.SelectionChanged += new System.EventHandler(this.ServicesGrid_SelectionChanged);
            // 
            // lblServicesListTitle
            // 
            this.lblServicesListTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblServicesListTitle.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold);
            this.lblServicesListTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblServicesListTitle.Location = new System.Drawing.Point(14, 14);
            this.lblServicesListTitle.Name = "lblServicesListTitle";
            this.lblServicesListTitle.Size = new System.Drawing.Size(727, 32);
            this.lblServicesListTitle.TabIndex = 1;
            this.lblServicesListTitle.Text = "CONFIGURED SERVICES";
            // 
            // pnlServiceEditor
            // 
            this.pnlServiceEditor.AutoScroll = true;
            this.pnlServiceEditor.BackColor = System.Drawing.Color.White;
            this.pnlServiceEditor.Controls.Add(this.tblServiceEditor);
            this.pnlServiceEditor.Controls.Add(this._selectedCategoryLabel);
            this.pnlServiceEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlServiceEditor.Location = new System.Drawing.Point(2, 2);
            this.pnlServiceEditor.Name = "pnlServiceEditor";
            this.pnlServiceEditor.Padding = new System.Windows.Forms.Padding(14);
            this.pnlServiceEditor.Size = new System.Drawing.Size(386, 507);
            this.pnlServiceEditor.TabIndex = 0;
            // 
            // tblServiceEditor
            // 
            this.tblServiceEditor.ColumnCount = 4;
            this.tblServiceEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.41899F));
            this.tblServiceEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.18436F));
            this.tblServiceEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.6988F));
            this.tblServiceEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.20482F));
            this.tblServiceEditor.Controls.Add(this.lblServiceDescription, 0, 0);
            this.tblServiceEditor.Controls.Add(this._serviceDescription, 1, 0);
            this.tblServiceEditor.Controls.Add(this.lblServiceProvider, 2, 0);
            this.tblServiceEditor.Controls.Add(this._serviceProvider, 3, 0);
            this.tblServiceEditor.Controls.Add(this.lblServiceOperator, 0, 1);
            this.tblServiceEditor.Controls.Add(this._serviceOperator, 1, 1);
            this.tblServiceEditor.Controls.Add(this.lblServiceStatus, 2, 1);
            this.tblServiceEditor.Controls.Add(this._serviceActive, 3, 1);
            this.tblServiceEditor.Controls.Add(this.lblServiceCost, 0, 2);
            this.tblServiceEditor.Controls.Add(this._serviceCost, 1, 2);
            this.tblServiceEditor.Controls.Add(this.lblServiceDeducted, 2, 2);
            this.tblServiceEditor.Controls.Add(this._serviceDeducted, 3, 2);
            this.tblServiceEditor.Controls.Add(this.lblServiceCostLbp, 0, 3);
            this.tblServiceEditor.Controls.Add(this._serviceCostLbp, 1, 3);
            this.tblServiceEditor.Controls.Add(this.lblServiceDeductedLbp, 2, 3);
            this.tblServiceEditor.Controls.Add(this._serviceDeductedLbp, 3, 3);
            this.tblServiceEditor.Controls.Add(this.lblServicePrice, 0, 4);
            this.tblServiceEditor.Controls.Add(this._servicePrice, 1, 4);
            this.tblServiceEditor.Controls.Add(this.lblServiceProfit, 2, 4);
            this.tblServiceEditor.Controls.Add(this._serviceProfit, 3, 4);
            this.tblServiceEditor.Controls.Add(this.lblServicePriceLbp, 0, 5);
            this.tblServiceEditor.Controls.Add(this._servicePriceLbp, 1, 5);
            this.tblServiceEditor.Controls.Add(this.lblServiceExchangeRate, 2, 5);
            this.tblServiceEditor.Controls.Add(this.flowServiceActions, 0, 6);
            this.tblServiceEditor.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblServiceEditor.Location = new System.Drawing.Point(14, 46);
            this.tblServiceEditor.Name = "tblServiceEditor";
            this.tblServiceEditor.RowCount = 7;
            this.tblServiceEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tblServiceEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tblServiceEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tblServiceEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tblServiceEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblServiceEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tblServiceEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tblServiceEditor.Size = new System.Drawing.Size(358, 396);
            this.tblServiceEditor.TabIndex = 0;
            // 
            // lblServiceDescription
            // 
            this.lblServiceDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServiceDescription.Location = new System.Drawing.Point(3, 0);
            this.lblServiceDescription.Name = "lblServiceDescription";
            this.lblServiceDescription.Size = new System.Drawing.Size(85, 46);
            this.lblServiceDescription.TabIndex = 0;
            this.lblServiceDescription.Text = "Description";
            this.lblServiceDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _serviceDescription
            // 
            this._serviceDescription.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this._serviceDescription.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this._serviceDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this._serviceDescription.Location = new System.Drawing.Point(94, 3);
            this._serviceDescription.Name = "_serviceDescription";
            this._serviceDescription.Size = new System.Drawing.Size(77, 24);
            this._serviceDescription.TabIndex = 1;
            // 
            // lblServiceProvider
            // 
            this.lblServiceProvider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServiceProvider.Location = new System.Drawing.Point(177, 0);
            this.lblServiceProvider.Name = "lblServiceProvider";
            this.lblServiceProvider.Size = new System.Drawing.Size(82, 46);
            this.lblServiceProvider.TabIndex = 2;
            this.lblServiceProvider.Text = "Provider";
            this.lblServiceProvider.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _serviceProvider
            // 
            this._serviceProvider.Dock = System.Windows.Forms.DockStyle.Fill;
            this._serviceProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._serviceProvider.Location = new System.Drawing.Point(265, 3);
            this._serviceProvider.Name = "_serviceProvider";
            this._serviceProvider.Size = new System.Drawing.Size(90, 24);
            this._serviceProvider.TabIndex = 3;
            this._serviceProvider.SelectedIndexChanged += new System.EventHandler(this.ServiceProvider_SelectedIndexChanged);
            // 
            // lblServiceOperator
            // 
            this.lblServiceOperator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServiceOperator.Location = new System.Drawing.Point(3, 46);
            this.lblServiceOperator.Name = "lblServiceOperator";
            this.lblServiceOperator.Size = new System.Drawing.Size(85, 46);
            this.lblServiceOperator.TabIndex = 4;
            this.lblServiceOperator.Text = "Operator";
            this.lblServiceOperator.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _serviceOperator
            // 
            this._serviceOperator.Dock = System.Windows.Forms.DockStyle.Fill;
            this._serviceOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._serviceOperator.Items.AddRange(new object[] {
            "MTC",
            "Alfa"});
            this._serviceOperator.Location = new System.Drawing.Point(94, 49);
            this._serviceOperator.Name = "_serviceOperator";
            this._serviceOperator.Size = new System.Drawing.Size(77, 24);
            this._serviceOperator.TabIndex = 5;
            // 
            // lblServiceStatus
            // 
            this.lblServiceStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServiceStatus.Location = new System.Drawing.Point(177, 46);
            this.lblServiceStatus.Name = "lblServiceStatus";
            this.lblServiceStatus.Size = new System.Drawing.Size(82, 46);
            this.lblServiceStatus.TabIndex = 6;
            this.lblServiceStatus.Text = "Status";
            this.lblServiceStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _serviceActive
            // 
            this._serviceActive.AutoSize = true;
            this._serviceActive.Checked = true;
            this._serviceActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this._serviceActive.Location = new System.Drawing.Point(265, 49);
            this._serviceActive.Name = "_serviceActive";
            this._serviceActive.Size = new System.Drawing.Size(70, 21);
            this._serviceActive.TabIndex = 7;
            this._serviceActive.Text = "Active";
            // 
            // lblServiceCost
            // 
            this.lblServiceCost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServiceCost.Location = new System.Drawing.Point(3, 92);
            this.lblServiceCost.Name = "lblServiceCost";
            this.lblServiceCost.Size = new System.Drawing.Size(85, 46);
            this.lblServiceCost.TabIndex = 8;
            this.lblServiceCost.Text = "Cost price (USD)";
            this.lblServiceCost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _serviceCost
            // 
            this._serviceCost.DecimalPlaces = 2;
            this._serviceCost.Dock = System.Windows.Forms.DockStyle.Fill;
            this._serviceCost.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this._serviceCost.Location = new System.Drawing.Point(94, 95);
            this._serviceCost.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this._serviceCost.Name = "_serviceCost";
            this._serviceCost.Size = new System.Drawing.Size(77, 24);
            this._serviceCost.TabIndex = 9;
            this._serviceCost.ThousandsSeparator = true;
            this._serviceCost.ValueChanged += new System.EventHandler(this.ServiceCostUsd_ValueChanged);
            // 
            // lblServiceDeducted
            // 
            this.lblServiceDeducted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServiceDeducted.Location = new System.Drawing.Point(177, 92);
            this.lblServiceDeducted.Name = "lblServiceDeducted";
            this.lblServiceDeducted.Size = new System.Drawing.Size(82, 46);
            this.lblServiceDeducted.TabIndex = 10;
            this.lblServiceDeducted.Text = "Deducted price (USD)";
            this.lblServiceDeducted.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _serviceDeducted
            // 
            this._serviceDeducted.DecimalPlaces = 2;
            this._serviceDeducted.Dock = System.Windows.Forms.DockStyle.Fill;
            this._serviceDeducted.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this._serviceDeducted.Location = new System.Drawing.Point(265, 95);
            this._serviceDeducted.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this._serviceDeducted.Name = "_serviceDeducted";
            this._serviceDeducted.Size = new System.Drawing.Size(90, 24);
            this._serviceDeducted.TabIndex = 11;
            this._serviceDeducted.ThousandsSeparator = true;
            this._serviceDeducted.ValueChanged += new System.EventHandler(this.ServiceDeductedUsd_ValueChanged);
            // 
            // lblServiceCostLbp
            // 
            this.lblServiceCostLbp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServiceCostLbp.Location = new System.Drawing.Point(3, 138);
            this.lblServiceCostLbp.Name = "lblServiceCostLbp";
            this.lblServiceCostLbp.Size = new System.Drawing.Size(85, 46);
            this.lblServiceCostLbp.TabIndex = 12;
            this.lblServiceCostLbp.Text = "Cost price (LBP)";
            this.lblServiceCostLbp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _serviceCostLbp
            // 
            this._serviceCostLbp.Dock = System.Windows.Forms.DockStyle.Fill;
            this._serviceCostLbp.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this._serviceCostLbp.Location = new System.Drawing.Point(94, 141);
            this._serviceCostLbp.Maximum = new decimal(new int[] {
            276447232,
            23283,
            0,
            0});
            this._serviceCostLbp.Name = "_serviceCostLbp";
            this._serviceCostLbp.Size = new System.Drawing.Size(77, 24);
            this._serviceCostLbp.TabIndex = 13;
            this._serviceCostLbp.ThousandsSeparator = true;
            this._serviceCostLbp.ValueChanged += new System.EventHandler(this.ServiceCostLbp_ValueChanged);
            // 
            // lblServiceDeductedLbp
            // 
            this.lblServiceDeductedLbp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServiceDeductedLbp.Location = new System.Drawing.Point(177, 138);
            this.lblServiceDeductedLbp.Name = "lblServiceDeductedLbp";
            this.lblServiceDeductedLbp.Size = new System.Drawing.Size(82, 46);
            this.lblServiceDeductedLbp.TabIndex = 14;
            this.lblServiceDeductedLbp.Text = "Deducted price (LBP)";
            this.lblServiceDeductedLbp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _serviceDeductedLbp
            // 
            this._serviceDeductedLbp.Dock = System.Windows.Forms.DockStyle.Fill;
            this._serviceDeductedLbp.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this._serviceDeductedLbp.Location = new System.Drawing.Point(265, 141);
            this._serviceDeductedLbp.Maximum = new decimal(new int[] {
            276447232,
            23283,
            0,
            0});
            this._serviceDeductedLbp.Name = "_serviceDeductedLbp";
            this._serviceDeductedLbp.Size = new System.Drawing.Size(90, 24);
            this._serviceDeductedLbp.TabIndex = 15;
            this._serviceDeductedLbp.ThousandsSeparator = true;
            this._serviceDeductedLbp.ValueChanged += new System.EventHandler(this.ServiceDeductedLbp_ValueChanged);
            // 
            // lblServicePrice
            // 
            this.lblServicePrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServicePrice.Location = new System.Drawing.Point(3, 184);
            this.lblServicePrice.Name = "lblServicePrice";
            this.lblServicePrice.Size = new System.Drawing.Size(85, 44);
            this.lblServicePrice.TabIndex = 16;
            this.lblServicePrice.Text = "Final price (USD)";
            this.lblServicePrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _servicePrice
            // 
            this._servicePrice.DecimalPlaces = 2;
            this._servicePrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this._servicePrice.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this._servicePrice.Location = new System.Drawing.Point(94, 187);
            this._servicePrice.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this._servicePrice.Name = "_servicePrice";
            this._servicePrice.Size = new System.Drawing.Size(77, 24);
            this._servicePrice.TabIndex = 17;
            this._servicePrice.ThousandsSeparator = true;
            this._servicePrice.ValueChanged += new System.EventHandler(this.ServicePriceUsd_ValueChanged);
            // 
            // lblServiceProfit
            // 
            this.lblServiceProfit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServiceProfit.Location = new System.Drawing.Point(177, 184);
            this.lblServiceProfit.Name = "lblServiceProfit";
            this.lblServiceProfit.Size = new System.Drawing.Size(82, 44);
            this.lblServiceProfit.TabIndex = 18;
            this.lblServiceProfit.Text = "Profit";
            this.lblServiceProfit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _serviceProfit
            // 
            this._serviceProfit.AutoSize = true;
            this._serviceProfit.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this._serviceProfit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this._serviceProfit.Location = new System.Drawing.Point(265, 184);
            this._serviceProfit.Name = "_serviceProfit";
            this._serviceProfit.Size = new System.Drawing.Size(53, 19);
            this._serviceProfit.TabIndex = 19;
            this._serviceProfit.Text = "$ 0.00";
            // 
            // lblServicePriceLbp
            // 
            this.lblServicePriceLbp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServicePriceLbp.Location = new System.Drawing.Point(3, 228);
            this.lblServicePriceLbp.Name = "lblServicePriceLbp";
            this.lblServicePriceLbp.Size = new System.Drawing.Size(85, 46);
            this.lblServicePriceLbp.TabIndex = 20;
            this.lblServicePriceLbp.Text = "Final price (LBP)";
            this.lblServicePriceLbp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _servicePriceLbp
            // 
            this._servicePriceLbp.Dock = System.Windows.Forms.DockStyle.Fill;
            this._servicePriceLbp.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this._servicePriceLbp.Location = new System.Drawing.Point(94, 231);
            this._servicePriceLbp.Maximum = new decimal(new int[] {
            276447232,
            23283,
            0,
            0});
            this._servicePriceLbp.Name = "_servicePriceLbp";
            this._servicePriceLbp.Size = new System.Drawing.Size(77, 24);
            this._servicePriceLbp.TabIndex = 21;
            this._servicePriceLbp.ThousandsSeparator = true;
            this._servicePriceLbp.ValueChanged += new System.EventHandler(this.ServicePriceLbp_ValueChanged);
            // 
            // lblServiceExchangeRate
            // 
            this.tblServiceEditor.SetColumnSpan(this.lblServiceExchangeRate, 2);
            this.lblServiceExchangeRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServiceExchangeRate.Font = new System.Drawing.Font("Century Gothic", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblServiceExchangeRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblServiceExchangeRate.Location = new System.Drawing.Point(177, 228);
            this.lblServiceExchangeRate.Name = "lblServiceExchangeRate";
            this.lblServiceExchangeRate.Size = new System.Drawing.Size(178, 46);
            this.lblServiceExchangeRate.TabIndex = 22;
            this.lblServiceExchangeRate.Text = "Rate: 1 USD = 89,500 LBP";
            this.lblServiceExchangeRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flowServiceActions
            // 
            this.flowServiceActions.AutoScroll = true;
            this.tblServiceEditor.SetColumnSpan(this.flowServiceActions, 4);
            this.flowServiceActions.Controls.Add(this.btnNewService);
            this.flowServiceActions.Controls.Add(this.btnSaveService);
            this.flowServiceActions.Controls.Add(this.btnRefreshServices);
            this.flowServiceActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowServiceActions.Location = new System.Drawing.Point(3, 277);
            this.flowServiceActions.Name = "flowServiceActions";
            this.flowServiceActions.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.flowServiceActions.Size = new System.Drawing.Size(352, 116);
            this.flowServiceActions.TabIndex = 23;
            // 
            // btnNewService
            // 
            this.btnNewService.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnNewService.FlatAppearance.BorderSize = 0;
            this.btnNewService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewService.ForeColor = System.Drawing.Color.White;
            this.btnNewService.Location = new System.Drawing.Point(3, 9);
            this.btnNewService.Name = "btnNewService";
            this.btnNewService.Size = new System.Drawing.Size(125, 42);
            this.btnNewService.TabIndex = 0;
            this.btnNewService.Text = "New Service";
            this.btnNewService.UseVisualStyleBackColor = false;
            this.btnNewService.Click += new System.EventHandler(this.BtnNewService_Click);
            // 
            // btnSaveService
            // 
            this.btnSaveService.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnSaveService.FlatAppearance.BorderSize = 0;
            this.btnSaveService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveService.ForeColor = System.Drawing.Color.White;
            this.btnSaveService.Location = new System.Drawing.Point(134, 9);
            this.btnSaveService.Name = "btnSaveService";
            this.btnSaveService.Size = new System.Drawing.Size(135, 42);
            this.btnSaveService.TabIndex = 1;
            this.btnSaveService.Text = "Save Service";
            this.btnSaveService.UseVisualStyleBackColor = false;
            this.btnSaveService.Click += new System.EventHandler(this.SaveService_Click);
            // 
            // btnRefreshServices
            // 
            this.btnRefreshServices.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(165)))), ((int)(((byte)(233)))));
            this.btnRefreshServices.FlatAppearance.BorderSize = 0;
            this.btnRefreshServices.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshServices.ForeColor = System.Drawing.Color.White;
            this.btnRefreshServices.Location = new System.Drawing.Point(3, 57);
            this.btnRefreshServices.Name = "btnRefreshServices";
            this.btnRefreshServices.Size = new System.Drawing.Size(100, 42);
            this.btnRefreshServices.TabIndex = 2;
            this.btnRefreshServices.Text = "Refresh";
            this.btnRefreshServices.UseVisualStyleBackColor = false;
            this.btnRefreshServices.Click += new System.EventHandler(this.BtnRefreshServices_Click);
            // 
            // _selectedCategoryLabel
            // 
            this._selectedCategoryLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._selectedCategoryLabel.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold);
            this._selectedCategoryLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._selectedCategoryLabel.Location = new System.Drawing.Point(14, 14);
            this._selectedCategoryLabel.Name = "_selectedCategoryLabel";
            this._selectedCategoryLabel.Size = new System.Drawing.Size(358, 32);
            this._selectedCategoryLabel.TabIndex = 1;
            this._selectedCategoryLabel.Text = "MOBILE RECHARGE";
            // 
            // _categoryBar
            // 
            this._categoryBar.AutoScroll = true;
            this._categoryBar.BackColor = System.Drawing.Color.White;
            this._categoryBar.Controls.Add(this.btnMobileRecharge);
            this._categoryBar.Controls.Add(this.btnTransferBalance);
            this._categoryBar.Controls.Add(this.btnValidity);
            this._categoryBar.Controls.Add(this.btnInternetBundles);
            this._categoryBar.Controls.Add(this.btnApps);
            this._categoryBar.Dock = System.Windows.Forms.DockStyle.Top;
            this._categoryBar.Location = new System.Drawing.Point(8, 8);
            this._categoryBar.Name = "_categoryBar";
            this._categoryBar.Padding = new System.Windows.Forms.Padding(6);
            this._categoryBar.Size = new System.Drawing.Size(1156, 64);
            this._categoryBar.TabIndex = 1;
            this._categoryBar.WrapContents = false;
            // 
            // btnMobileRecharge
            // 
            this.btnMobileRecharge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnMobileRecharge.FlatAppearance.BorderSize = 0;
            this.btnMobileRecharge.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMobileRecharge.ForeColor = System.Drawing.Color.White;
            this.btnMobileRecharge.Location = new System.Drawing.Point(9, 9);
            this.btnMobileRecharge.Name = "btnMobileRecharge";
            this.btnMobileRecharge.Size = new System.Drawing.Size(145, 44);
            this.btnMobileRecharge.TabIndex = 0;
            this.btnMobileRecharge.Tag = "Mobile Recharge";
            this.btnMobileRecharge.Text = "Mobile Recharge";
            this.btnMobileRecharge.UseVisualStyleBackColor = false;
            this.btnMobileRecharge.Click += new System.EventHandler(this.CategoryButton_Click);
            // 
            // btnTransferBalance
            // 
            this.btnTransferBalance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnTransferBalance.FlatAppearance.BorderSize = 0;
            this.btnTransferBalance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransferBalance.ForeColor = System.Drawing.Color.White;
            this.btnTransferBalance.Location = new System.Drawing.Point(160, 9);
            this.btnTransferBalance.Name = "btnTransferBalance";
            this.btnTransferBalance.Size = new System.Drawing.Size(145, 44);
            this.btnTransferBalance.TabIndex = 1;
            this.btnTransferBalance.Tag = "Transfer Balance";
            this.btnTransferBalance.Text = "Transfer Balance";
            this.btnTransferBalance.UseVisualStyleBackColor = false;
            this.btnTransferBalance.Click += new System.EventHandler(this.CategoryButton_Click);
            // 
            // btnValidity
            // 
            this.btnValidity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnValidity.FlatAppearance.BorderSize = 0;
            this.btnValidity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnValidity.ForeColor = System.Drawing.Color.White;
            this.btnValidity.Location = new System.Drawing.Point(311, 9);
            this.btnValidity.Name = "btnValidity";
            this.btnValidity.Size = new System.Drawing.Size(165, 44);
            this.btnValidity.TabIndex = 2;
            this.btnValidity.Tag = "Validity Recharge";
            this.btnValidity.Text = "Validity Recharge";
            this.btnValidity.UseVisualStyleBackColor = false;
            this.btnValidity.Click += new System.EventHandler(this.CategoryButton_Click);
            // 
            // btnInternetBundles
            // 
            this.btnInternetBundles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnInternetBundles.FlatAppearance.BorderSize = 0;
            this.btnInternetBundles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInternetBundles.ForeColor = System.Drawing.Color.White;
            this.btnInternetBundles.Location = new System.Drawing.Point(482, 9);
            this.btnInternetBundles.Name = "btnInternetBundles";
            this.btnInternetBundles.Size = new System.Drawing.Size(165, 44);
            this.btnInternetBundles.TabIndex = 3;
            this.btnInternetBundles.Tag = "Internet Bundles";
            this.btnInternetBundles.Text = "Internet Bundles";
            this.btnInternetBundles.UseVisualStyleBackColor = false;
            this.btnInternetBundles.Click += new System.EventHandler(this.CategoryButton_Click);
            // 
            // btnApps
            // 
            this.btnApps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnApps.FlatAppearance.BorderSize = 0;
            this.btnApps.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApps.ForeColor = System.Drawing.Color.White;
            this.btnApps.Location = new System.Drawing.Point(653, 9);
            this.btnApps.Name = "btnApps";
            this.btnApps.Size = new System.Drawing.Size(145, 44);
            this.btnApps.TabIndex = 4;
            this.btnApps.Tag = "Apps";
            this.btnApps.Text = "Apps";
            this.btnApps.UseVisualStyleBackColor = false;
            this.btnApps.Click += new System.EventHandler(this.CategoryButton_Click);
            // 
            // tabProviders
            // 
            this.tabProviders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.tabProviders.Controls.Add(this._providersSplit);
            this.tabProviders.Location = new System.Drawing.Point(4, 33);
            this.tabProviders.Name = "tabProviders";
            this.tabProviders.Padding = new System.Windows.Forms.Padding(8);
            this.tabProviders.Size = new System.Drawing.Size(1172, 591);
            this.tabProviders.TabIndex = 1;
            this.tabProviders.Text = "Provider Balances";
            // 
            // _providersSplit
            // 
            this._providersSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this._providersSplit.Location = new System.Drawing.Point(8, 8);
            this._providersSplit.Name = "_providersSplit";
            // 
            // _providersSplit.Panel1
            // 
            this._providersSplit.Panel1.Controls.Add(this.pnlProvidersList);
            // 
            // _providersSplit.Panel2
            // 
            this._providersSplit.Panel2.Controls.Add(this.pnlProviderEditor);
            this._providersSplit.Size = new System.Drawing.Size(1156, 575);
            this._providersSplit.SplitterDistance = 926;
            this._providersSplit.SplitterWidth = 7;
            this._providersSplit.TabIndex = 0;
            // 
            // pnlProvidersList
            // 
            this.pnlProvidersList.BackColor = System.Drawing.Color.White;
            this.pnlProvidersList.Controls.Add(this._providersGrid);
            this.pnlProvidersList.Controls.Add(this.lblProvidersTitle);
            this.pnlProvidersList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProvidersList.Location = new System.Drawing.Point(0, 0);
            this.pnlProvidersList.Name = "pnlProvidersList";
            this.pnlProvidersList.Padding = new System.Windows.Forms.Padding(14);
            this.pnlProvidersList.Size = new System.Drawing.Size(926, 575);
            this.pnlProvidersList.TabIndex = 0;
            // 
            // _providersGrid
            // 
            this._providersGrid.AllowUserToAddRows = false;
            this._providersGrid.AllowUserToDeleteRows = false;
            this._providersGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._providersGrid.BackgroundColor = System.Drawing.Color.White;
            this._providersGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._providersGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._providersGrid.Location = new System.Drawing.Point(14, 48);
            this._providersGrid.MultiSelect = false;
            this._providersGrid.Name = "_providersGrid";
            this._providersGrid.ReadOnly = true;
            this._providersGrid.RowHeadersVisible = false;
            this._providersGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._providersGrid.Size = new System.Drawing.Size(898, 513);
            this._providersGrid.TabIndex = 0;
            this._providersGrid.SelectionChanged += new System.EventHandler(this.ProvidersGrid_SelectionChanged);
            // 
            // lblProvidersTitle
            // 
            this.lblProvidersTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProvidersTitle.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold);
            this.lblProvidersTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblProvidersTitle.Location = new System.Drawing.Point(14, 14);
            this.lblProvidersTitle.Name = "lblProvidersTitle";
            this.lblProvidersTitle.Size = new System.Drawing.Size(898, 34);
            this.lblProvidersTitle.TabIndex = 1;
            this.lblProvidersTitle.Text = "PROVIDER / LINE BALANCES";
            // 
            // pnlProviderEditor
            // 
            this.pnlProviderEditor.AutoScroll = true;
            this.pnlProviderEditor.BackColor = System.Drawing.Color.White;
            this.pnlProviderEditor.Controls.Add(this.tblProviderEditor);
            this.pnlProviderEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProviderEditor.Location = new System.Drawing.Point(0, 0);
            this.pnlProviderEditor.Name = "pnlProviderEditor";
            this.pnlProviderEditor.Padding = new System.Windows.Forms.Padding(14);
            this.pnlProviderEditor.Size = new System.Drawing.Size(223, 575);
            this.pnlProviderEditor.TabIndex = 0;
            // 
            // tblProviderEditor
            // 
            this.tblProviderEditor.ColumnCount = 4;
            this.tblProviderEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tblProviderEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tblProviderEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tblProviderEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tblProviderEditor.Controls.Add(this.lblProviderName, 0, 0);
            this.tblProviderEditor.Controls.Add(this._providerName, 1, 0);
            this.tblProviderEditor.Controls.Add(this.lblProviderCode, 2, 0);
            this.tblProviderEditor.Controls.Add(this._providerCode, 3, 0);
            this.tblProviderEditor.Controls.Add(this.lblProviderType, 0, 1);
            this.tblProviderEditor.Controls.Add(this._providerType, 1, 1);
            this.tblProviderEditor.Controls.Add(this.lblProviderBalance, 2, 1);
            this.tblProviderEditor.Controls.Add(this._providerBalance, 3, 1);
            this.tblProviderEditor.Controls.Add(this.lblProviderStatus, 0, 2);
            this.tblProviderEditor.Controls.Add(this._providerActive, 1, 2);
            this.tblProviderEditor.Controls.Add(this.lblProviderNotes, 2, 2);
            this.tblProviderEditor.Controls.Add(this._providerNotes, 3, 2);
            this.tblProviderEditor.Controls.Add(this.lblBalanceAmount, 0, 3);
            this.tblProviderEditor.Controls.Add(this._providerAdjustment, 1, 3);
            this.tblProviderEditor.Controls.Add(this.lblBalanceAmountLbp, 2, 3);
            this.tblProviderEditor.Controls.Add(this._providerAdjustmentLbp, 3, 3);
            this.tblProviderEditor.Controls.Add(this.flowProviderActions, 0, 4);
            this.tblProviderEditor.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblProviderEditor.Location = new System.Drawing.Point(14, 14);
            this.tblProviderEditor.Name = "tblProviderEditor";
            this.tblProviderEditor.RowCount = 5;
            this.tblProviderEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tblProviderEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tblProviderEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tblProviderEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tblProviderEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tblProviderEditor.Size = new System.Drawing.Size(195, 314);
            this.tblProviderEditor.TabIndex = 0;
            // 
            // lblProviderName
            // 
            this.lblProviderName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProviderName.Location = new System.Drawing.Point(3, 0);
            this.lblProviderName.Name = "lblProviderName";
            this.lblProviderName.Size = new System.Drawing.Size(27, 46);
            this.lblProviderName.TabIndex = 0;
            this.lblProviderName.Text = "Provider name";
            this.lblProviderName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _providerName
            // 
            this._providerName.Dock = System.Windows.Forms.DockStyle.Fill;
            this._providerName.Location = new System.Drawing.Point(36, 3);
            this._providerName.Name = "_providerName";
            this._providerName.Size = new System.Drawing.Size(58, 24);
            this._providerName.TabIndex = 1;
            // 
            // lblProviderCode
            // 
            this.lblProviderCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProviderCode.Location = new System.Drawing.Point(100, 0);
            this.lblProviderCode.Name = "lblProviderCode";
            this.lblProviderCode.Size = new System.Drawing.Size(27, 46);
            this.lblProviderCode.TabIndex = 2;
            this.lblProviderCode.Text = "Provider code";
            this.lblProviderCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _providerCode
            // 
            this._providerCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this._providerCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this._providerCode.Location = new System.Drawing.Point(133, 3);
            this._providerCode.Name = "_providerCode";
            this._providerCode.Size = new System.Drawing.Size(59, 24);
            this._providerCode.TabIndex = 3;
            // 
            // lblProviderType
            // 
            this.lblProviderType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProviderType.Location = new System.Drawing.Point(3, 46);
            this.lblProviderType.Name = "lblProviderType";
            this.lblProviderType.Size = new System.Drawing.Size(27, 46);
            this.lblProviderType.TabIndex = 4;
            this.lblProviderType.Text = "Provider type";
            this.lblProviderType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _providerType
            // 
            this._providerType.Dock = System.Windows.Forms.DockStyle.Fill;
            this._providerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._providerType.Items.AddRange(new object[] {
            "Wallet",
            "Mobile Line"});
            this._providerType.Location = new System.Drawing.Point(36, 49);
            this._providerType.Name = "_providerType";
            this._providerType.Size = new System.Drawing.Size(58, 24);
            this._providerType.TabIndex = 5;
            // 
            // lblProviderBalance
            // 
            this.lblProviderBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProviderBalance.Location = new System.Drawing.Point(100, 46);
            this.lblProviderBalance.Name = "lblProviderBalance";
            this.lblProviderBalance.Size = new System.Drawing.Size(27, 46);
            this.lblProviderBalance.TabIndex = 6;
            this.lblProviderBalance.Text = "Current balance";
            this.lblProviderBalance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _providerBalance
            // 
            this._providerBalance.AutoSize = true;
            this._providerBalance.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this._providerBalance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this._providerBalance.Location = new System.Drawing.Point(133, 46);
            this._providerBalance.Name = "_providerBalance";
            this._providerBalance.Size = new System.Drawing.Size(53, 19);
            this._providerBalance.TabIndex = 7;
            this._providerBalance.Text = "$ 0.00";
            // 
            // lblProviderStatus
            // 
            this.lblProviderStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProviderStatus.Location = new System.Drawing.Point(3, 92);
            this.lblProviderStatus.Name = "lblProviderStatus";
            this.lblProviderStatus.Size = new System.Drawing.Size(27, 46);
            this.lblProviderStatus.TabIndex = 8;
            this.lblProviderStatus.Text = "Status";
            this.lblProviderStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _providerActive
            // 
            this._providerActive.AutoSize = true;
            this._providerActive.Checked = true;
            this._providerActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this._providerActive.Location = new System.Drawing.Point(36, 95);
            this._providerActive.Name = "_providerActive";
            this._providerActive.Size = new System.Drawing.Size(59, 21);
            this._providerActive.TabIndex = 9;
            this._providerActive.Text = "Active";
            // 
            // lblProviderNotes
            // 
            this.lblProviderNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProviderNotes.Location = new System.Drawing.Point(100, 92);
            this.lblProviderNotes.Name = "lblProviderNotes";
            this.lblProviderNotes.Size = new System.Drawing.Size(27, 46);
            this.lblProviderNotes.TabIndex = 10;
            this.lblProviderNotes.Text = "Note";
            this.lblProviderNotes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _providerNotes
            // 
            this._providerNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this._providerNotes.Location = new System.Drawing.Point(133, 95);
            this._providerNotes.Name = "_providerNotes";
            this._providerNotes.Size = new System.Drawing.Size(59, 24);
            this._providerNotes.TabIndex = 11;
            // 
            // lblBalanceAmount
            // 
            this.lblBalanceAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBalanceAmount.Location = new System.Drawing.Point(3, 138);
            this.lblBalanceAmount.Name = "lblBalanceAmount";
            this.lblBalanceAmount.Size = new System.Drawing.Size(27, 54);
            this.lblBalanceAmount.TabIndex = 12;
            this.lblBalanceAmount.Text = "Balance amount (USD)";
            this.lblBalanceAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _providerAdjustment
            // 
            this._providerAdjustment.DecimalPlaces = 2;
            this._providerAdjustment.Dock = System.Windows.Forms.DockStyle.Fill;
            this._providerAdjustment.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this._providerAdjustment.Location = new System.Drawing.Point(36, 141);
            this._providerAdjustment.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this._providerAdjustment.Name = "_providerAdjustment";
            this._providerAdjustment.Size = new System.Drawing.Size(58, 24);
            this._providerAdjustment.TabIndex = 13;
            this._providerAdjustment.ThousandsSeparator = true;
            this._providerAdjustment.ValueChanged += new System.EventHandler(this.ProviderAdjustmentUsd_ValueChanged);
            // 
            // lblBalanceAmountLbp
            // 
            this.lblBalanceAmountLbp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBalanceAmountLbp.Location = new System.Drawing.Point(100, 138);
            this.lblBalanceAmountLbp.Name = "lblBalanceAmountLbp";
            this.lblBalanceAmountLbp.Size = new System.Drawing.Size(27, 54);
            this.lblBalanceAmountLbp.TabIndex = 14;
            this.lblBalanceAmountLbp.Text = "Balance amount (LBP)";
            this.lblBalanceAmountLbp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _providerAdjustmentLbp
            // 
            this._providerAdjustmentLbp.DecimalPlaces = 0;
            this._providerAdjustmentLbp.Dock = System.Windows.Forms.DockStyle.Fill;
            this._providerAdjustmentLbp.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this._providerAdjustmentLbp.Location = new System.Drawing.Point(133, 141);
            this._providerAdjustmentLbp.Maximum = new decimal(new int[] {
            276447232,
            23283,
            0,
            0});
            this._providerAdjustmentLbp.Name = "_providerAdjustmentLbp";
            this._providerAdjustmentLbp.Size = new System.Drawing.Size(59, 24);
            this._providerAdjustmentLbp.TabIndex = 15;
            this._providerAdjustmentLbp.ThousandsSeparator = true;
            this._providerAdjustmentLbp.ValueChanged += new System.EventHandler(this.ProviderAdjustmentLbp_ValueChanged);
            // 
            // flowProviderActions
            // 
            this.flowProviderActions.AutoScroll = true;
            this.tblProviderEditor.SetColumnSpan(this.flowProviderActions, 4);
            this.flowProviderActions.Controls.Add(this.btnNewProvider);
            this.flowProviderActions.Controls.Add(this.btnSaveProvider);
            this.flowProviderActions.Controls.Add(this.btnAddBalance);
            this.flowProviderActions.Controls.Add(this.btnRemoveBalance);
            this.flowProviderActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowProviderActions.Location = new System.Drawing.Point(3, 195);
            this.flowProviderActions.Name = "flowProviderActions";
            this.flowProviderActions.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.flowProviderActions.Size = new System.Drawing.Size(189, 116);
            this.flowProviderActions.TabIndex = 16;
            // 
            // btnNewProvider
            // 
            this.btnNewProvider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnNewProvider.FlatAppearance.BorderSize = 0;
            this.btnNewProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewProvider.ForeColor = System.Drawing.Color.White;
            this.btnNewProvider.Location = new System.Drawing.Point(3, 9);
            this.btnNewProvider.Name = "btnNewProvider";
            this.btnNewProvider.Size = new System.Drawing.Size(135, 42);
            this.btnNewProvider.TabIndex = 0;
            this.btnNewProvider.Text = "New Provider";
            this.btnNewProvider.UseVisualStyleBackColor = false;
            this.btnNewProvider.Click += new System.EventHandler(this.BtnNewProvider_Click);
            // 
            // btnSaveProvider
            // 
            this.btnSaveProvider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnSaveProvider.FlatAppearance.BorderSize = 0;
            this.btnSaveProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveProvider.ForeColor = System.Drawing.Color.White;
            this.btnSaveProvider.Location = new System.Drawing.Point(3, 57);
            this.btnSaveProvider.Name = "btnSaveProvider";
            this.btnSaveProvider.Size = new System.Drawing.Size(140, 42);
            this.btnSaveProvider.TabIndex = 1;
            this.btnSaveProvider.Text = "Save Provider";
            this.btnSaveProvider.UseVisualStyleBackColor = false;
            this.btnSaveProvider.Click += new System.EventHandler(this.SaveProvider_Click);
            // 
            // btnAddBalance
            // 
            this.btnAddBalance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnAddBalance.FlatAppearance.BorderSize = 0;
            this.btnAddBalance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddBalance.ForeColor = System.Drawing.Color.White;
            this.btnAddBalance.Location = new System.Drawing.Point(3, 105);
            this.btnAddBalance.Name = "btnAddBalance";
            this.btnAddBalance.Size = new System.Drawing.Size(125, 42);
            this.btnAddBalance.TabIndex = 2;
            this.btnAddBalance.Text = "Add Balance";
            this.btnAddBalance.UseVisualStyleBackColor = false;
            this.btnAddBalance.Click += new System.EventHandler(this.BtnAddBalance_Click);
            // 
            // btnRemoveBalance
            // 
            this.btnRemoveBalance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnRemoveBalance.FlatAppearance.BorderSize = 0;
            this.btnRemoveBalance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveBalance.ForeColor = System.Drawing.Color.White;
            this.btnRemoveBalance.Location = new System.Drawing.Point(3, 153);
            this.btnRemoveBalance.Name = "btnRemoveBalance";
            this.btnRemoveBalance.Size = new System.Drawing.Size(145, 42);
            this.btnRemoveBalance.TabIndex = 3;
            this.btnRemoveBalance.Text = "Remove Balance";
            this.btnRemoveBalance.UseVisualStyleBackColor = false;
            this.btnRemoveBalance.Click += new System.EventHandler(this.BtnRemoveBalance_Click);
            // 
            // ServiceManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1180, 700);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Century Gothic", 9.5F);
            this.MinimumSize = new System.Drawing.Size(760, 520);
            this.Name = "ServiceManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Services";
            this.Load += new System.EventHandler(this.ServiceManagementForm_Load);
            this.Shown += new System.EventHandler(this.ServiceManagementForm_Shown);
            this.Resize += new System.EventHandler(this.ServiceManagementForm_Resize);
            this.tabMain.ResumeLayout(false);
            this.tabServices.ResumeLayout(false);
            this._servicesSplit.Panel1.ResumeLayout(false);
            this._servicesSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._servicesSplit)).EndInit();
            this._servicesSplit.ResumeLayout(false);
            this.pnlServicesList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._servicesGrid)).EndInit();
            this.pnlServiceEditor.ResumeLayout(false);
            this.tblServiceEditor.ResumeLayout(false);
            this.tblServiceEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._serviceCost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._serviceDeducted)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._serviceCostLbp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._serviceDeductedLbp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._servicePrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._servicePriceLbp)).EndInit();
            this.flowServiceActions.ResumeLayout(false);
            this._categoryBar.ResumeLayout(false);
            this.tabProviders.ResumeLayout(false);
            this._providersSplit.Panel1.ResumeLayout(false);
            this._providersSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._providersSplit)).EndInit();
            this._providersSplit.ResumeLayout(false);
            this.pnlProvidersList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._providersGrid)).EndInit();
            this.pnlProviderEditor.ResumeLayout(false);
            this.tblProviderEditor.ResumeLayout(false);
            this.tblProviderEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._providerAdjustment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._providerAdjustmentLbp)).EndInit();
            this.flowProviderActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
