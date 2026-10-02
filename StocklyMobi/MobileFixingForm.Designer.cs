namespace StocklyMobi
{
    partial class MobileFixingForm
    {
        private System.ComponentModel.IContainer components=null;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.SplitContainer _split;
        private System.Windows.Forms.Panel pnlEntry;
        private System.Windows.Forms.TableLayoutPanel tblEntry;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.TextBox _customerName;
        private System.Windows.Forms.Label lblPhoneNumber;
        private System.Windows.Forms.TextBox _phoneNumber;
        private System.Windows.Forms.Label lblPhoneModel;
        private System.Windows.Forms.TextBox _phoneModel;
        private System.Windows.Forms.Label lblEstimated;
        private System.Windows.Forms.DateTimePicker _estimated;
        private System.Windows.Forms.Label lblProblem;
        private System.Windows.Forms.TextBox _problem;
        private System.Windows.Forms.Label lblCost;
        private System.Windows.Forms.NumericUpDown _costPrice;
        private System.Windows.Forms.Label lblFinal;
        private System.Windows.Forms.NumericUpDown _finalPrice;
        private System.Windows.Forms.FlowLayoutPanel flowEntryActions;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSavePreview;
        private System.Windows.Forms.Button btnSavePrint;
        private System.Windows.Forms.Label lblProfitCaption;
        private System.Windows.Forms.Label _profit;
        private System.Windows.Forms.Label lblEditMode;
        private System.Windows.Forms.Panel pnlList;
        private System.Windows.Forms.TableLayoutPanel tblFilter;
        private System.Windows.Forms.Label lblFindFix;
        private System.Windows.Forms.TextBox _search;
        private System.Windows.Forms.ComboBox _statusFilter;
        private System.Windows.Forms.Button btnClearSearch;
        private System.Windows.Forms.FlowLayoutPanel flowStatusActions;
        private System.Windows.Forms.Button btnEditSelected;
        private System.Windows.Forms.Button btnInProgress;
        private System.Windows.Forms.Button btnReady;
        private System.Windows.Forms.Button btnCancelJob;
        private System.Windows.Forms.Button btnPreviewTicket;
        private System.Windows.Forms.Button btnPrintTicket;
        private System.Windows.Forms.DataGridView _grid;
        protected override void Dispose(bool disposing){if(disposing&&components!=null)components.Dispose();
        base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.lblHeader = new System.Windows.Forms.Label();
            this._split = new System.Windows.Forms.SplitContainer();
            this.pnlEntry = new System.Windows.Forms.Panel();
            this.tblEntry = new System.Windows.Forms.TableLayoutPanel();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this._customerName = new System.Windows.Forms.TextBox();
            this.lblPhoneNumber = new System.Windows.Forms.Label();
            this._phoneNumber = new System.Windows.Forms.TextBox();
            this.lblPhoneModel = new System.Windows.Forms.Label();
            this._phoneModel = new System.Windows.Forms.TextBox();
            this.lblEstimated = new System.Windows.Forms.Label();
            this._estimated = new System.Windows.Forms.DateTimePicker();
            this.lblProblem = new System.Windows.Forms.Label();
            this._problem = new System.Windows.Forms.TextBox();
            this.lblCost = new System.Windows.Forms.Label();
            this._costPrice = new System.Windows.Forms.NumericUpDown();
            this.lblFinal = new System.Windows.Forms.Label();
            this._finalPrice = new System.Windows.Forms.NumericUpDown();
            this.flowEntryActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSavePreview = new System.Windows.Forms.Button();
            this.btnSavePrint = new System.Windows.Forms.Button();
            this.lblProfitCaption = new System.Windows.Forms.Label();
            this._profit = new System.Windows.Forms.Label();
            this.lblEditMode = new System.Windows.Forms.Label();
            this.pnlList = new System.Windows.Forms.Panel();
            this._grid = new System.Windows.Forms.DataGridView();
            this.tblFilter = new System.Windows.Forms.TableLayoutPanel();
            this.lblFindFix = new System.Windows.Forms.Label();
            this._search = new System.Windows.Forms.TextBox();
            this._statusFilter = new System.Windows.Forms.ComboBox();
            this.btnClearSearch = new System.Windows.Forms.Button();
            this.flowStatusActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnEditSelected = new System.Windows.Forms.Button();
            this.btnInProgress = new System.Windows.Forms.Button();
            this.btnReady = new System.Windows.Forms.Button();
            this.btnCancelJob = new System.Windows.Forms.Button();
            this.btnPreviewTicket = new System.Windows.Forms.Button();
            this.btnPrintTicket = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._split)).BeginInit();
            this._split.Panel1.SuspendLayout();
            this._split.Panel2.SuspendLayout();
            this._split.SuspendLayout();
            this.pnlEntry.SuspendLayout();
            this.tblEntry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._costPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._finalPrice)).BeginInit();
            this.flowEntryActions.SuspendLayout();
            this.pnlList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            this.tblFilter.SuspendLayout();
            this.flowStatusActions.SuspendLayout();
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
            this.lblHeader.Size = new System.Drawing.Size(1240, 72);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Mobile Fixing";
            // 
            // _split
            // 
            this._split.Dock = System.Windows.Forms.DockStyle.Fill;
            this._split.Location = new System.Drawing.Point(0, 72);
            this._split.Name = "_split";
            this._split.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _split.Panel1
            // 
            this._split.Panel1.Controls.Add(this.pnlEntry);
            this._split.Panel1.Padding = new System.Windows.Forms.Padding(10, 10, 10, 4);
            // 
            // _split.Panel2
            // 
            this._split.Panel2.Controls.Add(this.pnlList);
            this._split.Panel2.Padding = new System.Windows.Forms.Padding(10, 4, 10, 10);
            this._split.Size = new System.Drawing.Size(1240, 768);
            this._split.SplitterDistance = 384;
            this._split.SplitterWidth = 7;
            this._split.TabIndex = 0;
            // 
            // pnlEntry
            // 
            this.pnlEntry.BackColor = System.Drawing.Color.White;
            this.pnlEntry.Controls.Add(this.tblEntry);
            this.pnlEntry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEntry.Location = new System.Drawing.Point(10, 10);
            this.pnlEntry.Name = "pnlEntry";
            this.pnlEntry.Padding = new System.Windows.Forms.Padding(14);
            this.pnlEntry.Size = new System.Drawing.Size(1220, 370);
            this.pnlEntry.TabIndex = 0;
            // 
            // tblEntry
            // 
            this.tblEntry.ColumnCount = 4;
            this.tblEntry.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tblEntry.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tblEntry.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tblEntry.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tblEntry.Controls.Add(this.lblCustomerName, 0, 0);
            this.tblEntry.Controls.Add(this._customerName, 1, 0);
            this.tblEntry.Controls.Add(this.lblPhoneNumber, 2, 0);
            this.tblEntry.Controls.Add(this._phoneNumber, 3, 0);
            this.tblEntry.Controls.Add(this.lblPhoneModel, 0, 1);
            this.tblEntry.Controls.Add(this._phoneModel, 1, 1);
            this.tblEntry.Controls.Add(this.lblEstimated, 2, 1);
            this.tblEntry.Controls.Add(this._estimated, 3, 1);
            this.tblEntry.Controls.Add(this.lblProblem, 0, 2);
            this.tblEntry.Controls.Add(this._problem, 1, 2);
            this.tblEntry.Controls.Add(this.lblCost, 0, 3);
            this.tblEntry.Controls.Add(this._costPrice, 1, 3);
            this.tblEntry.Controls.Add(this.lblFinal, 2, 3);
            this.tblEntry.Controls.Add(this._finalPrice, 3, 3);
            this.tblEntry.Controls.Add(this.flowEntryActions, 0, 4);
            this.tblEntry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblEntry.Location = new System.Drawing.Point(14, 14);
            this.tblEntry.Name = "tblEntry";
            this.tblEntry.RowCount = 5;
            this.tblEntry.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tblEntry.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tblEntry.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tblEntry.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tblEntry.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblEntry.Size = new System.Drawing.Size(1192, 342);
            this.tblEntry.TabIndex = 0;
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCustomerName.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblCustomerName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblCustomerName.Location = new System.Drawing.Point(3, 0);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(196, 48);
            this.lblCustomerName.TabIndex = 0;
            this.lblCustomerName.Text = "Customer name";
            this.lblCustomerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _customerName
            // 
            this._customerName.Dock = System.Windows.Forms.DockStyle.Fill;
            this._customerName.Font = new System.Drawing.Font("Century Gothic", 10F);
            this._customerName.Location = new System.Drawing.Point(205, 3);
            this._customerName.Name = "_customerName";
            this._customerName.Size = new System.Drawing.Size(387, 24);
            this._customerName.TabIndex = 1;
            // 
            // lblPhoneNumber
            // 
            this.lblPhoneNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPhoneNumber.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblPhoneNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblPhoneNumber.Location = new System.Drawing.Point(598, 0);
            this.lblPhoneNumber.Name = "lblPhoneNumber";
            this.lblPhoneNumber.Size = new System.Drawing.Size(196, 48);
            this.lblPhoneNumber.TabIndex = 2;
            this.lblPhoneNumber.Text = "Phone number";
            this.lblPhoneNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _phoneNumber
            // 
            this._phoneNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this._phoneNumber.Font = new System.Drawing.Font("Century Gothic", 10F);
            this._phoneNumber.Location = new System.Drawing.Point(800, 3);
            this._phoneNumber.Name = "_phoneNumber";
            this._phoneNumber.Size = new System.Drawing.Size(389, 24);
            this._phoneNumber.TabIndex = 3;
            // 
            // lblPhoneModel
            // 
            this.lblPhoneModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPhoneModel.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblPhoneModel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblPhoneModel.Location = new System.Drawing.Point(3, 48);
            this.lblPhoneModel.Name = "lblPhoneModel";
            this.lblPhoneModel.Size = new System.Drawing.Size(196, 48);
            this.lblPhoneModel.TabIndex = 4;
            this.lblPhoneModel.Text = "Phone model";
            this.lblPhoneModel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _phoneModel
            // 
            this._phoneModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._phoneModel.Font = new System.Drawing.Font("Century Gothic", 10F);
            this._phoneModel.Location = new System.Drawing.Point(205, 51);
            this._phoneModel.Name = "_phoneModel";
            this._phoneModel.Size = new System.Drawing.Size(387, 24);
            this._phoneModel.TabIndex = 5;
            // 
            // lblEstimated
            // 
            this.lblEstimated.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstimated.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblEstimated.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblEstimated.Location = new System.Drawing.Point(598, 48);
            this.lblEstimated.Name = "lblEstimated";
            this.lblEstimated.Size = new System.Drawing.Size(196, 48);
            this.lblEstimated.TabIndex = 6;
            this.lblEstimated.Text = "Estimated finish";
            this.lblEstimated.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _estimated
            // 
            this._estimated.CustomFormat = "dd/MM/yyyy hh:mm tt";
            this._estimated.Dock = System.Windows.Forms.DockStyle.Fill;
            this._estimated.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._estimated.Location = new System.Drawing.Point(800, 51);
            this._estimated.Name = "_estimated";
            this._estimated.ShowUpDown = true;
            this._estimated.Size = new System.Drawing.Size(389, 23);
            this._estimated.TabIndex = 7;
            // 
            // lblProblem
            // 
            this.lblProblem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProblem.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblProblem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblProblem.Location = new System.Drawing.Point(3, 96);
            this.lblProblem.Name = "lblProblem";
            this.lblProblem.Size = new System.Drawing.Size(196, 82);
            this.lblProblem.TabIndex = 8;
            this.lblProblem.Text = "Problem description";
            this.lblProblem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _problem
            // 
            this.tblEntry.SetColumnSpan(this._problem, 3);
            this._problem.Dock = System.Windows.Forms.DockStyle.Fill;
            this._problem.Font = new System.Drawing.Font("Century Gothic", 10F);
            this._problem.Location = new System.Drawing.Point(205, 99);
            this._problem.Multiline = true;
            this._problem.Name = "_problem";
            this._problem.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._problem.Size = new System.Drawing.Size(984, 76);
            this._problem.TabIndex = 9;
            // 
            // lblCost
            // 
            this.lblCost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCost.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblCost.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblCost.Location = new System.Drawing.Point(3, 178);
            this.lblCost.Name = "lblCost";
            this.lblCost.Size = new System.Drawing.Size(196, 48);
            this.lblCost.TabIndex = 10;
            this.lblCost.Text = "Cost price";
            this.lblCost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _costPrice
            // 
            this._costPrice.DecimalPlaces = 2;
            this._costPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this._costPrice.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this._costPrice.Location = new System.Drawing.Point(205, 181);
            this._costPrice.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this._costPrice.Name = "_costPrice";
            this._costPrice.Size = new System.Drawing.Size(387, 23);
            this._costPrice.TabIndex = 11;
            this._costPrice.ThousandsSeparator = true;
            this._costPrice.ValueChanged += new System.EventHandler(this.RecalculateProfit);
            // 
            // lblFinal
            // 
            this.lblFinal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFinal.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblFinal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblFinal.Location = new System.Drawing.Point(598, 178);
            this.lblFinal.Name = "lblFinal";
            this.lblFinal.Size = new System.Drawing.Size(196, 48);
            this.lblFinal.TabIndex = 12;
            this.lblFinal.Text = "Final price";
            this.lblFinal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _finalPrice
            // 
            this._finalPrice.DecimalPlaces = 2;
            this._finalPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this._finalPrice.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this._finalPrice.Location = new System.Drawing.Point(800, 181);
            this._finalPrice.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this._finalPrice.Name = "_finalPrice";
            this._finalPrice.Size = new System.Drawing.Size(389, 23);
            this._finalPrice.TabIndex = 13;
            this._finalPrice.ThousandsSeparator = true;
            this._finalPrice.ValueChanged += new System.EventHandler(this.RecalculateProfit);
            // 
            // flowEntryActions
            // 
            this.tblEntry.SetColumnSpan(this.flowEntryActions, 4);
            this.flowEntryActions.Controls.Add(this.btnClear);
            this.flowEntryActions.Controls.Add(this.btnSave);
            this.flowEntryActions.Controls.Add(this.btnSavePreview);
            this.flowEntryActions.Controls.Add(this.btnSavePrint);
            this.flowEntryActions.Controls.Add(this.lblProfitCaption);
            this.flowEntryActions.Controls.Add(this._profit);
            this.flowEntryActions.Controls.Add(this.lblEditMode);
            this.flowEntryActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowEntryActions.Location = new System.Drawing.Point(3, 229);
            this.flowEntryActions.Name = "flowEntryActions";
            this.flowEntryActions.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.flowEntryActions.Size = new System.Drawing.Size(1186, 110);
            this.flowEntryActions.TabIndex = 14;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(3, 7);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(120, 42);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "Clear / New";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(129, 7);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 42);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnSavePreview
            // 
            this.btnSavePreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(165)))), ((int)(((byte)(233)))));
            this.btnSavePreview.FlatAppearance.BorderSize = 0;
            this.btnSavePreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSavePreview.ForeColor = System.Drawing.Color.White;
            this.btnSavePreview.Location = new System.Drawing.Point(230, 7);
            this.btnSavePreview.Name = "btnSavePreview";
            this.btnSavePreview.Size = new System.Drawing.Size(145, 42);
            this.btnSavePreview.TabIndex = 2;
            this.btnSavePreview.Text = "Save + Preview";
            this.btnSavePreview.UseVisualStyleBackColor = false;
            this.btnSavePreview.Click += new System.EventHandler(this.BtnSavePreview_Click);
            // 
            // btnSavePrint
            // 
            this.btnSavePrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnSavePrint.FlatAppearance.BorderSize = 0;
            this.btnSavePrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSavePrint.ForeColor = System.Drawing.Color.White;
            this.btnSavePrint.Location = new System.Drawing.Point(381, 7);
            this.btnSavePrint.Name = "btnSavePrint";
            this.btnSavePrint.Size = new System.Drawing.Size(135, 42);
            this.btnSavePrint.TabIndex = 3;
            this.btnSavePrint.Text = "Save + Print";
            this.btnSavePrint.UseVisualStyleBackColor = false;
            this.btnSavePrint.Click += new System.EventHandler(this.BtnSavePrint_Click);
            // 
            // lblProfitCaption
            // 
            this.lblProfitCaption.AutoSize = true;
            this.lblProfitCaption.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblProfitCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblProfitCaption.Location = new System.Drawing.Point(537, 19);
            this.lblProfitCaption.Margin = new System.Windows.Forms.Padding(18, 15, 4, 0);
            this.lblProfitCaption.Name = "lblProfitCaption";
            this.lblProfitCaption.Size = new System.Drawing.Size(38, 16);
            this.lblProfitCaption.TabIndex = 4;
            this.lblProfitCaption.Text = "Profit:";
            // 
            // _profit
            // 
            this._profit.AutoSize = true;
            this._profit.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold);
            this._profit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this._profit.Location = new System.Drawing.Point(582, 16);
            this._profit.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
            this._profit.Name = "_profit";
            this._profit.Size = new System.Drawing.Size(48, 18);
            this._profit.TabIndex = 5;
            this._profit.Text = "$ 0.00";
            // 
            // lblEditMode
            // 
            this.lblEditMode.AutoSize = true;
            this.lblEditMode.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblEditMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblEditMode.Location = new System.Drawing.Point(651, 19);
            this.lblEditMode.Margin = new System.Windows.Forms.Padding(18, 15, 4, 0);
            this.lblEditMode.Name = "lblEditMode";
            this.lblEditMode.Size = new System.Drawing.Size(90, 16);
            this.lblEditMode.TabIndex = 6;
            this.lblEditMode.Text = "New fixing job";
            // 
            // pnlList
            // 
            this.pnlList.BackColor = System.Drawing.Color.White;
            this.pnlList.Controls.Add(this._grid);
            this.pnlList.Controls.Add(this.tblFilter);
            this.pnlList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlList.Location = new System.Drawing.Point(10, 4);
            this.pnlList.Name = "pnlList";
            this.pnlList.Padding = new System.Windows.Forms.Padding(14);
            this.pnlList.Size = new System.Drawing.Size(1220, 363);
            this.pnlList.TabIndex = 0;
            // 
            // _grid
            // 
            this._grid.AllowUserToAddRows = false;
            this._grid.AllowUserToDeleteRows = false;
            this._grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._grid.BackgroundColor = System.Drawing.Color.White;
            this._grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.Location = new System.Drawing.Point(14, 118);
            this._grid.MultiSelect = false;
            this._grid.Name = "_grid";
            this._grid.ReadOnly = true;
            this._grid.RowHeadersVisible = false;
            this._grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._grid.Size = new System.Drawing.Size(1192, 231);
            this._grid.TabIndex = 0;
            this._grid.DoubleClick += new System.EventHandler(this.BtnEditSelected_Click);
            // 
            // tblFilter
            // 
            this.tblFilter.ColumnCount = 4;
            this.tblFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tblFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tblFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tblFilter.Controls.Add(this.lblFindFix, 0, 0);
            this.tblFilter.Controls.Add(this._search, 1, 0);
            this.tblFilter.Controls.Add(this._statusFilter, 2, 0);
            this.tblFilter.Controls.Add(this.btnClearSearch, 3, 0);
            this.tblFilter.Controls.Add(this.flowStatusActions, 0, 1);
            this.tblFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblFilter.Location = new System.Drawing.Point(14, 14);
            this.tblFilter.Name = "tblFilter";
            this.tblFilter.RowCount = 2;
            this.tblFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tblFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tblFilter.Size = new System.Drawing.Size(1192, 104);
            this.tblFilter.TabIndex = 1;
            // 
            // lblFindFix
            // 
            this.lblFindFix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFindFix.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblFindFix.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblFindFix.Location = new System.Drawing.Point(3, 0);
            this.lblFindFix.Name = "lblFindFix";
            this.lblFindFix.Size = new System.Drawing.Size(144, 46);
            this.lblFindFix.TabIndex = 0;
            this.lblFindFix.Text = "Find Fix\r\n(type 3, 26-3, or a name)";
            this.lblFindFix.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _search
            // 
            this._search.Dock = System.Windows.Forms.DockStyle.Fill;
            this._search.Font = new System.Drawing.Font("Century Gothic", 10F);
            this._search.Location = new System.Drawing.Point(153, 3);
            this._search.Name = "_search";
            this._search.Size = new System.Drawing.Size(726, 24);
            this._search.TabIndex = 1;
            this._search.TextChanged += new System.EventHandler(this.Search_TextChanged);
            // 
            // _statusFilter
            // 
            this._statusFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this._statusFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._statusFilter.Items.AddRange(new object[] {
            "All Statuses",
            "Received",
            "In Progress",
            "Ready",
            "Delivered",
            "Cancelled"});
            this._statusFilter.Location = new System.Drawing.Point(885, 3);
            this._statusFilter.Name = "_statusFilter";
            this._statusFilter.Size = new System.Drawing.Size(134, 25);
            this._statusFilter.TabIndex = 2;
            this._statusFilter.SelectedIndexChanged += new System.EventHandler(this.StatusFilter_SelectedIndexChanged);
            // 
            // btnClearSearch
            // 
            this.btnClearSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnClearSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClearSearch.FlatAppearance.BorderSize = 0;
            this.btnClearSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearSearch.ForeColor = System.Drawing.Color.White;
            this.btnClearSearch.Location = new System.Drawing.Point(1025, 3);
            this.btnClearSearch.Name = "btnClearSearch";
            this.btnClearSearch.Size = new System.Drawing.Size(164, 40);
            this.btnClearSearch.TabIndex = 3;
            this.btnClearSearch.Text = "Clear Search";
            this.btnClearSearch.UseVisualStyleBackColor = false;
            this.btnClearSearch.Click += new System.EventHandler(this.BtnClearSearch_Click);
            // 
            // flowStatusActions
            // 
            this.flowStatusActions.AutoScroll = true;
            this.tblFilter.SetColumnSpan(this.flowStatusActions, 4);
            this.flowStatusActions.Controls.Add(this.btnEditSelected);
            this.flowStatusActions.Controls.Add(this.btnInProgress);
            this.flowStatusActions.Controls.Add(this.btnReady);
            this.flowStatusActions.Controls.Add(this.btnCancelJob);
            this.flowStatusActions.Controls.Add(this.btnPreviewTicket);
            this.flowStatusActions.Controls.Add(this.btnPrintTicket);
            this.flowStatusActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowStatusActions.Location = new System.Drawing.Point(3, 49);
            this.flowStatusActions.Name = "flowStatusActions";
            this.flowStatusActions.Size = new System.Drawing.Size(1186, 52);
            this.flowStatusActions.TabIndex = 4;
            this.flowStatusActions.WrapContents = false;
            // 
            // btnEditSelected
            // 
            this.btnEditSelected.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(58)))), ((int)(((byte)(237)))));
            this.btnEditSelected.FlatAppearance.BorderSize = 0;
            this.btnEditSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditSelected.ForeColor = System.Drawing.Color.White;
            this.btnEditSelected.Location = new System.Drawing.Point(3, 3);
            this.btnEditSelected.Name = "btnEditSelected";
            this.btnEditSelected.Size = new System.Drawing.Size(125, 42);
            this.btnEditSelected.TabIndex = 0;
            this.btnEditSelected.Text = "Edit Selected";
            this.btnEditSelected.UseVisualStyleBackColor = false;
            this.btnEditSelected.Click += new System.EventHandler(this.BtnEditSelected_Click);
            // 
            // btnInProgress
            // 
            this.btnInProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnInProgress.FlatAppearance.BorderSize = 0;
            this.btnInProgress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInProgress.ForeColor = System.Drawing.Color.White;
            this.btnInProgress.Location = new System.Drawing.Point(134, 3);
            this.btnInProgress.Name = "btnInProgress";
            this.btnInProgress.Size = new System.Drawing.Size(115, 42);
            this.btnInProgress.TabIndex = 1;
            this.btnInProgress.Text = "In Progress";
            this.btnInProgress.UseVisualStyleBackColor = false;
            this.btnInProgress.Click += new System.EventHandler(this.BtnInProgress_Click);
            // 
            // btnReady
            // 
            this.btnReady.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnReady.FlatAppearance.BorderSize = 0;
            this.btnReady.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReady.ForeColor = System.Drawing.Color.White;
            this.btnReady.Location = new System.Drawing.Point(255, 3);
            this.btnReady.Name = "btnReady";
            this.btnReady.Size = new System.Drawing.Size(115, 42);
            this.btnReady.TabIndex = 2;
            this.btnReady.Text = "Mark Ready";
            this.btnReady.UseVisualStyleBackColor = false;
            this.btnReady.Click += new System.EventHandler(this.BtnReady_Click);
            // 
            // btnCancelJob
            // 
            this.btnCancelJob.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnCancelJob.FlatAppearance.BorderSize = 0;
            this.btnCancelJob.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelJob.ForeColor = System.Drawing.Color.White;
            this.btnCancelJob.Location = new System.Drawing.Point(376, 3);
            this.btnCancelJob.Name = "btnCancelJob";
            this.btnCancelJob.Size = new System.Drawing.Size(110, 42);
            this.btnCancelJob.TabIndex = 3;
            this.btnCancelJob.Text = "Cancel Job";
            this.btnCancelJob.UseVisualStyleBackColor = false;
            this.btnCancelJob.Click += new System.EventHandler(this.BtnCancelJob_Click);
            // 
            // btnPreviewTicket
            // 
            this.btnPreviewTicket.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(165)))), ((int)(((byte)(233)))));
            this.btnPreviewTicket.FlatAppearance.BorderSize = 0;
            this.btnPreviewTicket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreviewTicket.ForeColor = System.Drawing.Color.White;
            this.btnPreviewTicket.Location = new System.Drawing.Point(492, 3);
            this.btnPreviewTicket.Name = "btnPreviewTicket";
            this.btnPreviewTicket.Size = new System.Drawing.Size(125, 42);
            this.btnPreviewTicket.TabIndex = 4;
            this.btnPreviewTicket.Text = "Preview Ticket";
            this.btnPreviewTicket.UseVisualStyleBackColor = false;
            this.btnPreviewTicket.Click += new System.EventHandler(this.PreviewSelectedTicket_Click);
            // 
            // btnPrintTicket
            // 
            this.btnPrintTicket.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnPrintTicket.FlatAppearance.BorderSize = 0;
            this.btnPrintTicket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintTicket.ForeColor = System.Drawing.Color.White;
            this.btnPrintTicket.Location = new System.Drawing.Point(623, 3);
            this.btnPrintTicket.Name = "btnPrintTicket";
            this.btnPrintTicket.Size = new System.Drawing.Size(110, 42);
            this.btnPrintTicket.TabIndex = 5;
            this.btnPrintTicket.Text = "Print Ticket";
            this.btnPrintTicket.UseVisualStyleBackColor = false;
            this.btnPrintTicket.Click += new System.EventHandler(this.PrintSelectedTicket_Click);
            // 
            // MobileFixingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1240, 840);
            this.Controls.Add(this._split);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Century Gothic", 9.5F);
            this.MinimumSize = new System.Drawing.Size(820, 640);
            this.Name = "MobileFixingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mobile Fixing";
            this.Load += new System.EventHandler(this.MobileFixingForm_Load);
            this.Shown += new System.EventHandler(this.MobileFixingForm_Shown);
            this.Resize += new System.EventHandler(this.MobileFixingForm_Resize);
            this._split.Panel1.ResumeLayout(false);
            this._split.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._split)).EndInit();
            this._split.ResumeLayout(false);
            this.pnlEntry.ResumeLayout(false);
            this.tblEntry.ResumeLayout(false);
            this.tblEntry.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._costPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._finalPrice)).EndInit();
            this.flowEntryActions.ResumeLayout(false);
            this.flowEntryActions.PerformLayout();
            this.pnlList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            this.tblFilter.ResumeLayout(false);
            this.tblFilter.PerformLayout();
            this.flowStatusActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
