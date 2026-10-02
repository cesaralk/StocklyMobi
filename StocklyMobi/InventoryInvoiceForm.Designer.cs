namespace StocklyMobi
{
    partial class InventoryInvoiceForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventoryInvoiceForm));
            this._pageHeader = new System.Windows.Forms.Label();
            this._headerPanel = new System.Windows.Forms.Panel();
            this._headerLayout = new System.Windows.Forms.TableLayoutPanel();
            this._lblSupplierInvoiceNo = new System.Windows.Forms.Label();
            this._invoiceNo = new System.Windows.Forms.TextBox();
            this._lblSupplier = new System.Windows.Forms.Label();
            this._supplier = new System.Windows.Forms.ComboBox();
            this._lblDate = new System.Windows.Forms.Label();
            this._invoiceDate = new System.Windows.Forms.DateTimePicker();
            this._headerActions = new System.Windows.Forms.FlowLayoutPanel();
            this._btnNewInvoice = new System.Windows.Forms.Button();
            this._btnInvoiceHistory = new System.Windows.Forms.Button();
            this._btnAddSupplier = new System.Windows.Forms.Button();
            this._mainSplit = new System.Windows.Forms.SplitContainer();
            this._productCard = new System.Windows.Forms.Panel();
            this._productsGrid = new System.Windows.Forms.DataGridView();
            this._productTools = new System.Windows.Forms.TableLayoutPanel();
            this._lblSearch = new System.Windows.Forms.Label();
            this._search = new System.Windows.Forms.TextBox();
            this._btnClearSearch = new System.Windows.Forms.Button();
            this._barcodeTools = new System.Windows.Forms.FlowLayoutPanel();
            this._lblBarcodeLabelCount = new System.Windows.Forms.Label();
            this._barcodeLabelCount = new System.Windows.Forms.NumericUpDown();
            this._btnPrintBarcode = new System.Windows.Forms.Button();
            this._productActions = new System.Windows.Forms.FlowLayoutPanel();
            this._btnAddSelected = new System.Windows.Forms.Button();
            this._btnNewProduct = new System.Windows.Forms.Button();
            this._btnEditProduct = new System.Windows.Forms.Button();
            this._btnDeleteProduct = new System.Windows.Forms.Button();
            this._btnRefresh = new System.Windows.Forms.Button();
            this._productCount = new System.Windows.Forms.Label();
            this._invoiceCard = new System.Windows.Forms.Panel();
            this._invoiceGrid = new System.Windows.Forms.DataGridView();
            this._invoiceHelp = new System.Windows.Forms.Label();
            this._invoiceFooter = new System.Windows.Forms.TableLayoutPanel();
            this._invoiceTotal = new System.Windows.Forms.Label();
            this._invoiceActions = new System.Windows.Forms.FlowLayoutPanel();
            this._btnPostInvoice = new System.Windows.Forms.Button();
            this._btnRemoveLine = new System.Windows.Forms.Button();
            this._btnPrintOneEach = new System.Windows.Forms.Button();
            this._chkPrintInvoiceBarcodes = new System.Windows.Forms.CheckBox();
            this._headerPanel.SuspendLayout();
            this._headerLayout.SuspendLayout();
            this._headerActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._mainSplit)).BeginInit();
            this._mainSplit.Panel1.SuspendLayout();
            this._mainSplit.Panel2.SuspendLayout();
            this._mainSplit.SuspendLayout();
            this._productCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._productsGrid)).BeginInit();
            this._productTools.SuspendLayout();
            this._barcodeTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._barcodeLabelCount)).BeginInit();
            this._productActions.SuspendLayout();
            this._invoiceCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._invoiceGrid)).BeginInit();
            this._invoiceFooter.SuspendLayout();
            this._invoiceActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // _pageHeader
            // 
            this._pageHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._pageHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this._pageHeader.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold);
            this._pageHeader.ForeColor = System.Drawing.Color.White;
            this._pageHeader.Location = new System.Drawing.Point(0, 0);
            this._pageHeader.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._pageHeader.Name = "_pageHeader";
            this._pageHeader.Padding = new System.Windows.Forms.Padding(14, 8, 8, 3);
            this._pageHeader.Size = new System.Drawing.Size(1091, 58);
            this._pageHeader.TabIndex = 0;
            this._pageHeader.Text = "Products & Stock-In";
            // 
            // _headerPanel
            // 
            this._headerPanel.BackColor = System.Drawing.Color.White;
            this._headerPanel.Controls.Add(this._headerLayout);
            this._headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._headerPanel.Location = new System.Drawing.Point(0, 58);
            this._headerPanel.Margin = new System.Windows.Forms.Padding(2);
            this._headerPanel.Name = "_headerPanel";
            this._headerPanel.Padding = new System.Windows.Forms.Padding(11, 6, 11, 6);
            this._headerPanel.Size = new System.Drawing.Size(1091, 94);
            this._headerPanel.TabIndex = 1;
            // 
            // _headerLayout
            // 
            this._headerLayout.ColumnCount = 6;
            this._headerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this._headerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this._headerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this._headerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this._headerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this._headerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this._headerLayout.Controls.Add(this._lblSupplierInvoiceNo, 0, 0);
            this._headerLayout.Controls.Add(this._invoiceNo, 1, 0);
            this._headerLayout.Controls.Add(this._lblSupplier, 2, 0);
            this._headerLayout.Controls.Add(this._supplier, 3, 0);
            this._headerLayout.Controls.Add(this._lblDate, 4, 0);
            this._headerLayout.Controls.Add(this._invoiceDate, 5, 0);
            this._headerLayout.Controls.Add(this._headerActions, 0, 1);
            this._headerLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._headerLayout.Location = new System.Drawing.Point(11, 6);
            this._headerLayout.Margin = new System.Windows.Forms.Padding(2);
            this._headerLayout.Name = "_headerLayout";
            this._headerLayout.RowCount = 2;
            this._headerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this._headerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._headerLayout.Size = new System.Drawing.Size(1069, 82);
            this._headerLayout.TabIndex = 0;
            // 
            // _lblSupplierInvoiceNo
            // 
            this._lblSupplierInvoiceNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lblSupplierInvoiceNo.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this._lblSupplierInvoiceNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this._lblSupplierInvoiceNo.Location = new System.Drawing.Point(2, 0);
            this._lblSupplierInvoiceNo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._lblSupplierInvoiceNo.Name = "_lblSupplierInvoiceNo";
            this._lblSupplierInvoiceNo.Size = new System.Drawing.Size(116, 34);
            this._lblSupplierInvoiceNo.TabIndex = 0;
            this._lblSupplierInvoiceNo.Text = "Supplier Invoice No.";
            this._lblSupplierInvoiceNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _invoiceNo
            // 
            this._invoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._invoiceNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this._invoiceNo.Font = new System.Drawing.Font("Century Gothic", 10F);
            this._invoiceNo.Location = new System.Drawing.Point(122, 2);
            this._invoiceNo.Margin = new System.Windows.Forms.Padding(2);
            this._invoiceNo.MaxLength = 50;
            this._invoiceNo.Name = "_invoiceNo";
            this._invoiceNo.Size = new System.Drawing.Size(245, 24);
            this._invoiceNo.TabIndex = 1;
            // 
            // _lblSupplier
            // 
            this._lblSupplier.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lblSupplier.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this._lblSupplier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this._lblSupplier.Location = new System.Drawing.Point(371, 0);
            this._lblSupplier.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._lblSupplier.Name = "_lblSupplier";
            this._lblSupplier.Size = new System.Drawing.Size(62, 34);
            this._lblSupplier.TabIndex = 2;
            this._lblSupplier.Text = "Supplier";
            this._lblSupplier.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _supplier
            // 
            this._supplier.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this._supplier.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this._supplier.Dock = System.Windows.Forms.DockStyle.Fill;
            this._supplier.DropDownHeight = 240;
            this._supplier.Font = new System.Drawing.Font("Century Gothic", 10F);
            this._supplier.FormattingEnabled = true;
            this._supplier.IntegralHeight = false;
            this._supplier.Location = new System.Drawing.Point(437, 2);
            this._supplier.Margin = new System.Windows.Forms.Padding(2);
            this._supplier.MaxDropDownItems = 12;
            this._supplier.Name = "_supplier";
            this._supplier.Size = new System.Drawing.Size(287, 25);
            this._supplier.TabIndex = 3;
            // 
            // _lblDate
            // 
            this._lblDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lblDate.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this._lblDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this._lblDate.Location = new System.Drawing.Point(728, 0);
            this._lblDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._lblDate.Name = "_lblDate";
            this._lblDate.Size = new System.Drawing.Size(46, 34);
            this._lblDate.TabIndex = 4;
            this._lblDate.Text = "Date";
            this._lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _invoiceDate
            // 
            this._invoiceDate.CustomFormat = "dd/MM/yyyy hh:mm tt";
            this._invoiceDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this._invoiceDate.Font = new System.Drawing.Font("Century Gothic", 10F);
            this._invoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._invoiceDate.Location = new System.Drawing.Point(778, 2);
            this._invoiceDate.Margin = new System.Windows.Forms.Padding(2);
            this._invoiceDate.Name = "_invoiceDate";
            this._invoiceDate.ShowUpDown = true;
            this._invoiceDate.Size = new System.Drawing.Size(289, 24);
            this._invoiceDate.TabIndex = 5;
            // 
            // _headerActions
            // 
            this._headerLayout.SetColumnSpan(this._headerActions, 6);
            this._headerActions.Controls.Add(this._btnNewInvoice);
            this._headerActions.Controls.Add(this._btnInvoiceHistory);
            this._headerActions.Controls.Add(this._btnAddSupplier);
            this._headerActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this._headerActions.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._headerActions.Location = new System.Drawing.Point(2, 36);
            this._headerActions.Margin = new System.Windows.Forms.Padding(2);
            this._headerActions.Name = "_headerActions";
            this._headerActions.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this._headerActions.Size = new System.Drawing.Size(1065, 44);
            this._headerActions.TabIndex = 6;
            // 
            // _btnNewInvoice
            // 
            this._btnNewInvoice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._btnNewInvoice.Cursor = System.Windows.Forms.Cursors.Hand;
            this._btnNewInvoice.FlatAppearance.BorderSize = 0;
            this._btnNewInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnNewInvoice.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this._btnNewInvoice.ForeColor = System.Drawing.Color.White;
            this._btnNewInvoice.Location = new System.Drawing.Point(948, 8);
            this._btnNewInvoice.Margin = new System.Windows.Forms.Padding(5);
            this._btnNewInvoice.Name = "_btnNewInvoice";
            this._btnNewInvoice.Size = new System.Drawing.Size(112, 34);
            this._btnNewInvoice.TabIndex = 0;
            this._btnNewInvoice.Text = "New Invoice";
            this._btnNewInvoice.UseVisualStyleBackColor = false;
            this._btnNewInvoice.Click += new System.EventHandler(this.BtnNewInvoice_Click);
            // 
            // _btnInvoiceHistory
            // 
            this._btnInvoiceHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(165)))), ((int)(((byte)(233)))));
            this._btnInvoiceHistory.Cursor = System.Windows.Forms.Cursors.Hand;
            this._btnInvoiceHistory.FlatAppearance.BorderSize = 0;
            this._btnInvoiceHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnInvoiceHistory.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this._btnInvoiceHistory.ForeColor = System.Drawing.Color.White;
            this._btnInvoiceHistory.Location = new System.Drawing.Point(822, 8);
            this._btnInvoiceHistory.Margin = new System.Windows.Forms.Padding(5);
            this._btnInvoiceHistory.Name = "_btnInvoiceHistory";
            this._btnInvoiceHistory.Size = new System.Drawing.Size(116, 34);
            this._btnInvoiceHistory.TabIndex = 1;
            this._btnInvoiceHistory.Text = "Invoice History";
            this._btnInvoiceHistory.UseVisualStyleBackColor = false;
            this._btnInvoiceHistory.Click += new System.EventHandler(this.BtnInvoiceHistory_Click);
            // 
            // _btnAddSupplier
            // 
            this._btnAddSupplier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._btnAddSupplier.Cursor = System.Windows.Forms.Cursors.Hand;
            this._btnAddSupplier.FlatAppearance.BorderSize = 0;
            this._btnAddSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnAddSupplier.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this._btnAddSupplier.ForeColor = System.Drawing.Color.White;
            this._btnAddSupplier.Location = new System.Drawing.Point(716, 8);
            this._btnAddSupplier.Margin = new System.Windows.Forms.Padding(5);
            this._btnAddSupplier.Name = "_btnAddSupplier";
            this._btnAddSupplier.Size = new System.Drawing.Size(96, 34);
            this._btnAddSupplier.TabIndex = 2;
            this._btnAddSupplier.Text = "+ Supplier";
            this._btnAddSupplier.UseVisualStyleBackColor = false;
            this._btnAddSupplier.Click += new System.EventHandler(this.AddSupplier_Click);
            // 
            // _mainSplit
            // 
            this._mainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainSplit.Location = new System.Drawing.Point(0, 152);
            this._mainSplit.Margin = new System.Windows.Forms.Padding(2);
            this._mainSplit.Name = "_mainSplit";
            // 
            // _mainSplit.Panel1
            // 
            this._mainSplit.Panel1.Controls.Add(this._productCard);
            this._mainSplit.Panel1.Padding = new System.Windows.Forms.Padding(8);
            // 
            // _mainSplit.Panel2
            // 
            this._mainSplit.Panel2.Controls.Add(this._invoiceCard);
            this._mainSplit.Panel2.Padding = new System.Windows.Forms.Padding(8);
            this._mainSplit.Size = new System.Drawing.Size(1091, 497);
            this._mainSplit.SplitterDistance = 542;
            this._mainSplit.SplitterWidth = 6;
            this._mainSplit.TabIndex = 2;
            // 
            // _productCard
            // 
            this._productCard.BackColor = System.Drawing.Color.White;
            this._productCard.Controls.Add(this._productsGrid);
            this._productCard.Controls.Add(this._productTools);
            this._productCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._productCard.Location = new System.Drawing.Point(8, 8);
            this._productCard.Margin = new System.Windows.Forms.Padding(2);
            this._productCard.Name = "_productCard";
            this._productCard.Padding = new System.Windows.Forms.Padding(11);
            this._productCard.Size = new System.Drawing.Size(526, 481);
            this._productCard.TabIndex = 0;
            // 
            // _productsGrid
            // 
            this._productsGrid.AllowUserToAddRows = false;
            this._productsGrid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._productsGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this._productsGrid.BackgroundColor = System.Drawing.Color.White;
            this._productsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._productsGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this._productsGrid.ColumnHeadersHeight = 36;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 9.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._productsGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this._productsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._productsGrid.EnableHeadersVisualStyles = false;
            this._productsGrid.Location = new System.Drawing.Point(11, 130);
            this._productsGrid.Margin = new System.Windows.Forms.Padding(2);
            this._productsGrid.MultiSelect = false;
            this._productsGrid.Name = "_productsGrid";
            this._productsGrid.ReadOnly = true;
            this._productsGrid.RowHeadersVisible = false;
            this._productsGrid.RowTemplate.Height = 32;
            this._productsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._productsGrid.Size = new System.Drawing.Size(504, 340);
            this._productsGrid.TabIndex = 1;
            this._productsGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProductsGrid_CellDoubleClick);
            this._productsGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProductsGrid_KeyDown);
            // 
            // _productTools
            // 
            this._productTools.ColumnCount = 3;
            this._productTools.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this._productTools.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._productTools.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this._productTools.Controls.Add(this._lblSearch, 0, 0);
            this._productTools.Controls.Add(this._search, 1, 0);
            this._productTools.Controls.Add(this._btnClearSearch, 2, 0);
            this._productTools.Controls.Add(this._barcodeTools, 0, 1);
            this._productTools.Controls.Add(this._productActions, 0, 2);
            this._productTools.Dock = System.Windows.Forms.DockStyle.Top;
            this._productTools.Location = new System.Drawing.Point(11, 11);
            this._productTools.Margin = new System.Windows.Forms.Padding(2);
            this._productTools.Name = "_productTools";
            this._productTools.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this._productTools.RowCount = 3;
            this._productTools.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this._productTools.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this._productTools.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._productTools.Size = new System.Drawing.Size(504, 119);
            this._productTools.TabIndex = 0;
            // 
            // _lblSearch
            // 
            this._lblSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lblSearch.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this._lblSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._lblSearch.Location = new System.Drawing.Point(2, 2);
            this._lblSearch.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._lblSearch.Name = "_lblSearch";
            this._lblSearch.Size = new System.Drawing.Size(82, 34);
            this._lblSearch.TabIndex = 0;
            this._lblSearch.Text = "Search / Scan";
            this._lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _search
            // 
            this._search.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this._search.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._search.Dock = System.Windows.Forms.DockStyle.Fill;
            this._search.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this._search.Location = new System.Drawing.Point(88, 6);
            this._search.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this._search.Name = "_search";
            this._search.Size = new System.Drawing.Size(380, 24);
            this._search.TabIndex = 1;
            this._search.TextChanged += new System.EventHandler(this.Search_TextChanged);
            this._search.HandleCreated += new System.EventHandler(this.Search_HandleCreated);
            this._search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Search_KeyDown);
            // 
            // _btnClearSearch
            // 
            this._btnClearSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._btnClearSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this._btnClearSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this._btnClearSearch.FlatAppearance.BorderSize = 0;
            this._btnClearSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnClearSearch.Font = new System.Drawing.Font("Century Gothic", 13F, System.Drawing.FontStyle.Bold);
            this._btnClearSearch.ForeColor = System.Drawing.Color.White;
            this._btnClearSearch.Location = new System.Drawing.Point(473, 3);
            this._btnClearSearch.Margin = new System.Windows.Forms.Padding(3, 1, 0, 4);
            this._btnClearSearch.Name = "_btnClearSearch";
            this._btnClearSearch.Size = new System.Drawing.Size(31, 29);
            this._btnClearSearch.TabIndex = 2;
            this._btnClearSearch.Text = "×";
            this._btnClearSearch.UseVisualStyleBackColor = false;
            this._btnClearSearch.Click += new System.EventHandler(this.BtnClearSearch_Click);
            // 
            // _barcodeTools
            // 
            this._barcodeTools.AutoScroll = true;
            this._productTools.SetColumnSpan(this._barcodeTools, 3);
            this._barcodeTools.Controls.Add(this._lblBarcodeLabelCount);
            this._barcodeTools.Controls.Add(this._barcodeLabelCount);
            this._barcodeTools.Controls.Add(this._btnPrintBarcode);
            this._barcodeTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this._barcodeTools.Location = new System.Drawing.Point(2, 38);
            this._barcodeTools.Margin = new System.Windows.Forms.Padding(2);
            this._barcodeTools.Name = "_barcodeTools";
            this._barcodeTools.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this._barcodeTools.Size = new System.Drawing.Size(500, 33);
            this._barcodeTools.TabIndex = 3;
            this._barcodeTools.WrapContents = false;
            // 
            // _lblBarcodeLabelCount
            // 
            this._lblBarcodeLabelCount.AutoSize = true;
            this._lblBarcodeLabelCount.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this._lblBarcodeLabelCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this._lblBarcodeLabelCount.Location = new System.Drawing.Point(5, 9);
            this._lblBarcodeLabelCount.Margin = new System.Windows.Forms.Padding(5, 7, 4, 0);
            this._lblBarcodeLabelCount.Name = "_lblBarcodeLabelCount";
            this._lblBarcodeLabelCount.Size = new System.Drawing.Size(79, 16);
            this._lblBarcodeLabelCount.TabIndex = 0;
            this._lblBarcodeLabelCount.Text = "No. of labels";
            // 
            // _barcodeLabelCount
            // 
            this._barcodeLabelCount.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this._barcodeLabelCount.Location = new System.Drawing.Point(90, 6);
            this._barcodeLabelCount.Margin = new System.Windows.Forms.Padding(2, 4, 6, 2);
            this._barcodeLabelCount.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this._barcodeLabelCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._barcodeLabelCount.Name = "_barcodeLabelCount";
            this._barcodeLabelCount.Size = new System.Drawing.Size(66, 22);
            this._barcodeLabelCount.TabIndex = 1;
            this._barcodeLabelCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._barcodeLabelCount.ThousandsSeparator = true;
            this._barcodeLabelCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // _btnPrintBarcode
            // 
            this._btnPrintBarcode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(58)))), ((int)(((byte)(237)))));
            this._btnPrintBarcode.Cursor = System.Windows.Forms.Cursors.Hand;
            this._btnPrintBarcode.FlatAppearance.BorderSize = 0;
            this._btnPrintBarcode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnPrintBarcode.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this._btnPrintBarcode.ForeColor = System.Drawing.Color.White;
            this._btnPrintBarcode.Location = new System.Drawing.Point(167, 4);
            this._btnPrintBarcode.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this._btnPrintBarcode.Name = "_btnPrintBarcode";
            this._btnPrintBarcode.Size = new System.Drawing.Size(114, 27);
            this._btnPrintBarcode.TabIndex = 2;
            this._btnPrintBarcode.Text = "Print Barcode";
            this._btnPrintBarcode.UseVisualStyleBackColor = false;
            this._btnPrintBarcode.Click += new System.EventHandler(this.BtnPrintBarcode_Click);
            // 
            // _productActions
            // 
            this._productTools.SetColumnSpan(this._productActions, 3);
            this._productActions.Controls.Add(this._btnAddSelected);
            this._productActions.Controls.Add(this._btnNewProduct);
            this._productActions.Controls.Add(this._btnEditProduct);
            this._productActions.Controls.Add(this._btnDeleteProduct);
            this._productActions.Controls.Add(this._btnRefresh);
            this._productActions.Controls.Add(this._productCount);
            this._productActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this._productActions.Location = new System.Drawing.Point(2, 75);
            this._productActions.Margin = new System.Windows.Forms.Padding(2);
            this._productActions.Name = "_productActions";
            this._productActions.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this._productActions.Size = new System.Drawing.Size(500, 40);
            this._productActions.TabIndex = 3;
            // 
            // _btnAddSelected
            // 
            this._btnAddSelected.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._btnAddSelected.Cursor = System.Windows.Forms.Cursors.Hand;
            this._btnAddSelected.FlatAppearance.BorderSize = 0;
            this._btnAddSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnAddSelected.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this._btnAddSelected.ForeColor = System.Drawing.Color.White;
            this._btnAddSelected.Location = new System.Drawing.Point(5, 7);
            this._btnAddSelected.Margin = new System.Windows.Forms.Padding(5);
            this._btnAddSelected.Name = "_btnAddSelected";
            this._btnAddSelected.Size = new System.Drawing.Size(94, 30);
            this._btnAddSelected.TabIndex = 0;
            this._btnAddSelected.Text = "Add Selected";
            this._btnAddSelected.UseVisualStyleBackColor = false;
            this._btnAddSelected.Click += new System.EventHandler(this.BtnAddSelected_Click);
            // 
            // _btnNewProduct
            // 
            this._btnNewProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this._btnNewProduct.Cursor = System.Windows.Forms.Cursors.Hand;
            this._btnNewProduct.FlatAppearance.BorderSize = 0;
            this._btnNewProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnNewProduct.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this._btnNewProduct.ForeColor = System.Drawing.Color.White;
            this._btnNewProduct.Location = new System.Drawing.Point(109, 7);
            this._btnNewProduct.Margin = new System.Windows.Forms.Padding(5);
            this._btnNewProduct.Name = "_btnNewProduct";
            this._btnNewProduct.Size = new System.Drawing.Size(94, 30);
            this._btnNewProduct.TabIndex = 1;
            this._btnNewProduct.Text = "New Product";
            this._btnNewProduct.UseVisualStyleBackColor = false;
            this._btnNewProduct.Click += new System.EventHandler(this.NewProduct_Click);
            // 
            // _btnEditProduct
            // 
            this._btnEditProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this._btnEditProduct.Cursor = System.Windows.Forms.Cursors.Hand;
            this._btnEditProduct.FlatAppearance.BorderSize = 0;
            this._btnEditProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnEditProduct.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this._btnEditProduct.ForeColor = System.Drawing.Color.White;
            this._btnEditProduct.Location = new System.Drawing.Point(213, 7);
            this._btnEditProduct.Margin = new System.Windows.Forms.Padding(5);
            this._btnEditProduct.Name = "_btnEditProduct";
            this._btnEditProduct.Size = new System.Drawing.Size(86, 30);
            this._btnEditProduct.TabIndex = 2;
            this._btnEditProduct.Text = "Edit Product";
            this._btnEditProduct.UseVisualStyleBackColor = false;
            this._btnEditProduct.Click += new System.EventHandler(this.BtnEditProduct_Click);
            // 
            // _btnDeleteProduct
            // 
            this._btnDeleteProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this._btnDeleteProduct.Cursor = System.Windows.Forms.Cursors.Hand;
            this._btnDeleteProduct.FlatAppearance.BorderSize = 0;
            this._btnDeleteProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnDeleteProduct.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this._btnDeleteProduct.ForeColor = System.Drawing.Color.White;
            this._btnDeleteProduct.Location = new System.Drawing.Point(309, 7);
            this._btnDeleteProduct.Margin = new System.Windows.Forms.Padding(5);
            this._btnDeleteProduct.Name = "_btnDeleteProduct";
            this._btnDeleteProduct.Size = new System.Drawing.Size(94, 30);
            this._btnDeleteProduct.TabIndex = 3;
            this._btnDeleteProduct.Text = "Delete Product";
            this._btnDeleteProduct.UseVisualStyleBackColor = false;
            this._btnDeleteProduct.Click += new System.EventHandler(this.BtnDeleteProduct_Click);
            // 
            // _btnRefresh
            // 
            this._btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(165)))), ((int)(((byte)(233)))));
            this._btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this._btnRefresh.FlatAppearance.BorderSize = 0;
            this._btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnRefresh.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this._btnRefresh.ForeColor = System.Drawing.Color.White;
            this._btnRefresh.Location = new System.Drawing.Point(413, 7);
            this._btnRefresh.Margin = new System.Windows.Forms.Padding(5);
            this._btnRefresh.Name = "_btnRefresh";
            this._btnRefresh.Size = new System.Drawing.Size(74, 30);
            this._btnRefresh.TabIndex = 4;
            this._btnRefresh.Text = "Refresh";
            this._btnRefresh.UseVisualStyleBackColor = false;
            this._btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // _productCount
            // 
            this._productCount.AutoSize = true;
            this._productCount.Font = new System.Drawing.Font("Century Gothic", 8.5F, System.Drawing.FontStyle.Bold);
            this._productCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this._productCount.Location = new System.Drawing.Point(7, 53);
            this._productCount.Margin = new System.Windows.Forms.Padding(7, 11, 2, 2);
            this._productCount.Name = "_productCount";
            this._productCount.Size = new System.Drawing.Size(212, 16);
            this._productCount.TabIndex = 3;
            this._productCount.Text = "Type description, barcode or code";
            // 
            // _invoiceCard
            // 
            this._invoiceCard.BackColor = System.Drawing.Color.White;
            this._invoiceCard.Controls.Add(this._invoiceGrid);
            this._invoiceCard.Controls.Add(this._invoiceHelp);
            this._invoiceCard.Controls.Add(this._invoiceFooter);
            this._invoiceCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._invoiceCard.Location = new System.Drawing.Point(8, 8);
            this._invoiceCard.Margin = new System.Windows.Forms.Padding(2);
            this._invoiceCard.Name = "_invoiceCard";
            this._invoiceCard.Padding = new System.Windows.Forms.Padding(11);
            this._invoiceCard.Size = new System.Drawing.Size(527, 481);
            this._invoiceCard.TabIndex = 0;
            // 
            // _invoiceGrid
            // 
            this._invoiceGrid.AllowUserToAddRows = false;
            this._invoiceGrid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._invoiceGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this._invoiceGrid.BackgroundColor = System.Drawing.Color.White;
            this._invoiceGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Century Gothic", 9.5F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._invoiceGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this._invoiceGrid.ColumnHeadersHeight = 36;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Century Gothic", 9.5F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._invoiceGrid.DefaultCellStyle = dataGridViewCellStyle6;
            this._invoiceGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._invoiceGrid.EnableHeadersVisualStyles = false;
            this._invoiceGrid.Location = new System.Drawing.Point(11, 38);
            this._invoiceGrid.Margin = new System.Windows.Forms.Padding(2);
            this._invoiceGrid.MultiSelect = false;
            this._invoiceGrid.Name = "_invoiceGrid";
            this._invoiceGrid.RowHeadersVisible = false;
            this._invoiceGrid.RowTemplate.Height = 32;
            this._invoiceGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._invoiceGrid.Size = new System.Drawing.Size(505, 358);
            this._invoiceGrid.TabIndex = 1;
            this._invoiceGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.InvoiceGrid_CellEndEdit);
            this._invoiceGrid.CurrentCellDirtyStateChanged += new System.EventHandler(this.InvoiceGrid_CurrentCellDirtyStateChanged);
            this._invoiceGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.InvoiceGrid_DataError);
            // 
            // _invoiceHelp
            // 
            this._invoiceHelp.AutoEllipsis = true;
            this._invoiceHelp.Dock = System.Windows.Forms.DockStyle.Top;
            this._invoiceHelp.Font = new System.Drawing.Font("Century Gothic", 8.5F, System.Drawing.FontStyle.Bold);
            this._invoiceHelp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this._invoiceHelp.Location = new System.Drawing.Point(11, 11);
            this._invoiceHelp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._invoiceHelp.Name = "_invoiceHelp";
            this._invoiceHelp.Padding = new System.Windows.Forms.Padding(3, 6, 3, 2);
            this._invoiceHelp.Size = new System.Drawing.Size(505, 27);
            this._invoiceHelp.TabIndex = 0;
            // 
            // _invoiceFooter
            // 
            this._invoiceFooter.ColumnCount = 1;
            this._invoiceFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._invoiceFooter.Controls.Add(this._invoiceTotal, 0, 0);
            this._invoiceFooter.Controls.Add(this._invoiceActions, 0, 1);
            this._invoiceFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._invoiceFooter.Location = new System.Drawing.Point(11, 396);
            this._invoiceFooter.Margin = new System.Windows.Forms.Padding(2);
            this._invoiceFooter.Name = "_invoiceFooter";
            this._invoiceFooter.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this._invoiceFooter.RowCount = 2;
            this._invoiceFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this._invoiceFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._invoiceFooter.Size = new System.Drawing.Size(505, 74);
            this._invoiceFooter.TabIndex = 2;
            // 
            // _invoiceTotal
            // 
            this._invoiceTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this._invoiceTotal.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold);
            this._invoiceTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._invoiceTotal.Location = new System.Drawing.Point(2, 2);
            this._invoiceTotal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._invoiceTotal.Name = "_invoiceTotal";
            this._invoiceTotal.Padding = new System.Windows.Forms.Padding(0, 2, 6, 0);
            this._invoiceTotal.Size = new System.Drawing.Size(501, 27);
            this._invoiceTotal.TabIndex = 0;
            this._invoiceTotal.Text = "Lines: 0   Total QTY: 0   Invoice cost: $ 0.00";
            this._invoiceTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _invoiceActions
            // 
            this._invoiceActions.AutoScroll = true;
            this._invoiceActions.Controls.Add(this._btnPostInvoice);
            this._invoiceActions.Controls.Add(this._btnRemoveLine);
            this._invoiceActions.Controls.Add(this._btnPrintOneEach);
            this._invoiceActions.Controls.Add(this._chkPrintInvoiceBarcodes);
            this._invoiceActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this._invoiceActions.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._invoiceActions.Location = new System.Drawing.Point(2, 31);
            this._invoiceActions.Margin = new System.Windows.Forms.Padding(2);
            this._invoiceActions.Name = "_invoiceActions";
            this._invoiceActions.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this._invoiceActions.Size = new System.Drawing.Size(501, 41);
            this._invoiceActions.TabIndex = 1;
            this._invoiceActions.WrapContents = false;
            // 
            // _btnPostInvoice
            // 
            this._btnPostInvoice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this._btnPostInvoice.Cursor = System.Windows.Forms.Cursors.Hand;
            this._btnPostInvoice.FlatAppearance.BorderSize = 0;
            this._btnPostInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnPostInvoice.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this._btnPostInvoice.ForeColor = System.Drawing.Color.White;
            this._btnPostInvoice.Location = new System.Drawing.Point(376, 6);
            this._btnPostInvoice.Margin = new System.Windows.Forms.Padding(5);
            this._btnPostInvoice.Name = "_btnPostInvoice";
            this._btnPostInvoice.Size = new System.Drawing.Size(152, 34);
            this._btnPostInvoice.TabIndex = 0;
            this._btnPostInvoice.Text = "Post Stock-In Invoice";
            this._btnPostInvoice.UseVisualStyleBackColor = false;
            this._btnPostInvoice.Click += new System.EventHandler(this.PostInvoice_Click);
            // 
            // _btnRemoveLine
            // 
            this._btnRemoveLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this._btnRemoveLine.Cursor = System.Windows.Forms.Cursors.Hand;
            this._btnRemoveLine.FlatAppearance.BorderSize = 0;
            this._btnRemoveLine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnRemoveLine.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this._btnRemoveLine.ForeColor = System.Drawing.Color.White;
            this._btnRemoveLine.Location = new System.Drawing.Point(273, 6);
            this._btnRemoveLine.Margin = new System.Windows.Forms.Padding(5);
            this._btnRemoveLine.Name = "_btnRemoveLine";
            this._btnRemoveLine.Size = new System.Drawing.Size(93, 34);
            this._btnRemoveLine.TabIndex = 1;
            this._btnRemoveLine.Text = "Remove Line";
            this._btnRemoveLine.UseVisualStyleBackColor = false;
            this._btnRemoveLine.Click += new System.EventHandler(this.RemoveLine_Click);
            // 
            // _btnPrintOneEach
            // 
            this._btnPrintOneEach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(58)))), ((int)(((byte)(237)))));
            this._btnPrintOneEach.Cursor = System.Windows.Forms.Cursors.Hand;
            this._btnPrintOneEach.FlatAppearance.BorderSize = 0;
            this._btnPrintOneEach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnPrintOneEach.Font = new System.Drawing.Font("Century Gothic", 8.5F, System.Drawing.FontStyle.Bold);
            this._btnPrintOneEach.ForeColor = System.Drawing.Color.White;
            this._btnPrintOneEach.Location = new System.Drawing.Point(173, 6);
            this._btnPrintOneEach.Margin = new System.Windows.Forms.Padding(5);
            this._btnPrintOneEach.Name = "_btnPrintOneEach";
            this._btnPrintOneEach.Size = new System.Drawing.Size(90, 34);
            this._btnPrintOneEach.TabIndex = 2;
            this._btnPrintOneEach.Text = "Print 1 Each";
            this._btnPrintOneEach.UseVisualStyleBackColor = false;
            this._btnPrintOneEach.Click += new System.EventHandler(this.BtnPrintOneEach_Click);
            // 
            // _chkPrintInvoiceBarcodes
            // 
            this._chkPrintInvoiceBarcodes.AutoSize = true;
            this._chkPrintInvoiceBarcodes.Font = new System.Drawing.Font("Century Gothic", 8.5F, System.Drawing.FontStyle.Bold);
            this._chkPrintInvoiceBarcodes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._chkPrintInvoiceBarcodes.Location = new System.Drawing.Point(6, 14);
            this._chkPrintInvoiceBarcodes.Margin = new System.Windows.Forms.Padding(6, 13, 6, 2);
            this._chkPrintInvoiceBarcodes.Name = "_chkPrintInvoiceBarcodes";
            this._chkPrintInvoiceBarcodes.Size = new System.Drawing.Size(156, 20);
            this._chkPrintInvoiceBarcodes.TabIndex = 3;
            this._chkPrintInvoiceBarcodes.Text = "After post: invoice QTY";
            this._chkPrintInvoiceBarcodes.UseVisualStyleBackColor = true;
            // 
            // InventoryInvoiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1091, 649);
            this.Controls.Add(this._mainSplit);
            this.Controls.Add(this._headerPanel);
            this.Controls.Add(this._pageHeader);
            this.Font = new System.Drawing.Font("Century Gothic", 9.5F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(611, 440);
            this.Name = "InventoryInvoiceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Products & Stock-In Invoice";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InventoryInvoiceForm_FormClosed);
            this.Load += new System.EventHandler(this.InventoryInvoiceForm_Load);
            this.Shown += new System.EventHandler(this.InventoryInvoiceForm_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InventoryInvoiceForm_KeyDown);
            this.Resize += new System.EventHandler(this.InventoryInvoiceForm_Resize);
            this._headerPanel.ResumeLayout(false);
            this._headerLayout.ResumeLayout(false);
            this._headerLayout.PerformLayout();
            this._headerActions.ResumeLayout(false);
            this._mainSplit.Panel1.ResumeLayout(false);
            this._mainSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._mainSplit)).EndInit();
            this._mainSplit.ResumeLayout(false);
            this._productCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._productsGrid)).EndInit();
            this._productTools.ResumeLayout(false);
            this._productTools.PerformLayout();
            this._barcodeTools.ResumeLayout(false);
            this._barcodeTools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._barcodeLabelCount)).EndInit();
            this._productActions.ResumeLayout(false);
            this._productActions.PerformLayout();
            this._invoiceCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._invoiceGrid)).EndInit();
            this._invoiceFooter.ResumeLayout(false);
            this._invoiceActions.ResumeLayout(false);
            this._invoiceActions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label _pageHeader;
        private System.Windows.Forms.Panel _headerPanel;
        private System.Windows.Forms.TableLayoutPanel _headerLayout;
        private System.Windows.Forms.Label _lblSupplierInvoiceNo;
        private System.Windows.Forms.TextBox _invoiceNo;
        private System.Windows.Forms.Label _lblSupplier;
        private System.Windows.Forms.ComboBox _supplier;
        private System.Windows.Forms.Label _lblDate;
        private System.Windows.Forms.DateTimePicker _invoiceDate;
        private System.Windows.Forms.FlowLayoutPanel _headerActions;
        private System.Windows.Forms.Button _btnNewInvoice;
        private System.Windows.Forms.Button _btnInvoiceHistory;
        private System.Windows.Forms.Button _btnAddSupplier;
        private System.Windows.Forms.SplitContainer _mainSplit;
        private System.Windows.Forms.Panel _productCard;
        private System.Windows.Forms.DataGridView _productsGrid;
        private System.Windows.Forms.TableLayoutPanel _productTools;
        private System.Windows.Forms.Label _lblSearch;
        private System.Windows.Forms.TextBox _search;
        private System.Windows.Forms.Button _btnClearSearch;
        private System.Windows.Forms.FlowLayoutPanel _barcodeTools;
        private System.Windows.Forms.Label _lblBarcodeLabelCount;
        private System.Windows.Forms.NumericUpDown _barcodeLabelCount;
        private System.Windows.Forms.Button _btnPrintBarcode;
        private System.Windows.Forms.FlowLayoutPanel _productActions;
        private System.Windows.Forms.Button _btnAddSelected;
        private System.Windows.Forms.Button _btnNewProduct;
        private System.Windows.Forms.Button _btnEditProduct;
        private System.Windows.Forms.Button _btnDeleteProduct;
        private System.Windows.Forms.Button _btnRefresh;
        private System.Windows.Forms.Panel _invoiceCard;
        private System.Windows.Forms.DataGridView _invoiceGrid;
        private System.Windows.Forms.Label _invoiceHelp;
        private System.Windows.Forms.TableLayoutPanel _invoiceFooter;
        private System.Windows.Forms.Label _invoiceTotal;
        private System.Windows.Forms.FlowLayoutPanel _invoiceActions;
        private System.Windows.Forms.Button _btnPostInvoice;
        private System.Windows.Forms.Button _btnRemoveLine;
        private System.Windows.Forms.Button _btnPrintOneEach;
        private System.Windows.Forms.CheckBox _chkPrintInvoiceBarcodes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductBarcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductBrand;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductMarkup;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductReorder;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInvoiceCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInvoiceBarcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInvoiceDescription;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colInvoicePrintBarcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInvoiceQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInvoiceCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInvoiceMarkup;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInvoicePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInvoiceLineCost;
        private System.Windows.Forms.Label _productCount;
    }
}
