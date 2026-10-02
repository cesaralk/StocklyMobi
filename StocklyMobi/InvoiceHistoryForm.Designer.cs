namespace StocklyMobi
{
    partial class InvoiceHistoryForm
    {
        private System.ComponentModel.IContainer components=null;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.TableLayoutPanel tblFilter;
        private System.Windows.Forms.Label lblInvoice;
        private System.Windows.Forms.TextBox _search;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.TextBox _supplierSearch;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker _from;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker _to;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.SplitContainer _split;
        private System.Windows.Forms.Panel pnlInvoices;
        private System.Windows.Forms.Label lblInvoicesTitle;
        private System.Windows.Forms.DataGridView _invoices;
        private System.Windows.Forms.Panel pnlDetails;
        private System.Windows.Forms.DataGridView _lines;
        private System.Windows.Forms.TableLayoutPanel tblFooter;
        private System.Windows.Forms.Label _selectedInfo;
        private System.Windows.Forms.FlowLayoutPanel flowActions;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnDelete;
        protected override void Dispose(bool disposing){if(disposing&&components!=null)components.Dispose();
        base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvoiceHistoryForm));
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.tblFilter = new System.Windows.Forms.TableLayoutPanel();
            this.lblInvoice = new System.Windows.Forms.Label();
            this._search = new System.Windows.Forms.TextBox();
            this.lblSupplier = new System.Windows.Forms.Label();
            this._supplierSearch = new System.Windows.Forms.TextBox();
            this.lblFrom = new System.Windows.Forms.Label();
            this._from = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this._to = new System.Windows.Forms.DateTimePicker();
            this.btnFind = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this._split = new System.Windows.Forms.SplitContainer();
            this.pnlInvoices = new System.Windows.Forms.Panel();
            this._invoices = new System.Windows.Forms.DataGridView();
            this.lblInvoicesTitle = new System.Windows.Forms.Label();
            this.pnlDetails = new System.Windows.Forms.Panel();
            this._lines = new System.Windows.Forms.DataGridView();
            this.tblFooter = new System.Windows.Forms.TableLayoutPanel();
            this._selectedInfo = new System.Windows.Forms.Label();
            this.flowActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.pnlFilter.SuspendLayout();
            this.tblFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._split)).BeginInit();
            this._split.Panel1.SuspendLayout();
            this._split.Panel2.SuspendLayout();
            this._split.SuspendLayout();
            this.pnlInvoices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._invoices)).BeginInit();
            this.pnlDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._lines)).BeginInit();
            this.tblFooter.SuspendLayout();
            this.flowActions.SuspendLayout();
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
            this.lblHeader.Size = new System.Drawing.Size(1320, 72);
            this.lblHeader.TabIndex = 2;
            this.lblHeader.Text = "Stock-In Invoice History\r\nFind, inspect, print, or safely delete a posted supplie" +
    "r invoice.";
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.White;
            this.pnlFilter.Controls.Add(this.tblFilter);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 72);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Padding = new System.Windows.Forms.Padding(14);
            this.pnlFilter.Size = new System.Drawing.Size(1320, 92);
            this.pnlFilter.TabIndex = 1;
            // 
            // tblFilter
            // 
            this.tblFilter.ColumnCount = 10;
            this.tblFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tblFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tblFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tblFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26F));
            this.tblFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tblFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tblFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tblFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tblFilter.Controls.Add(this.lblInvoice, 0, 0);
            this.tblFilter.Controls.Add(this._search, 1, 0);
            this.tblFilter.Controls.Add(this.lblSupplier, 2, 0);
            this.tblFilter.Controls.Add(this._supplierSearch, 3, 0);
            this.tblFilter.Controls.Add(this.lblFrom, 4, 0);
            this.tblFilter.Controls.Add(this._from, 5, 0);
            this.tblFilter.Controls.Add(this.lblTo, 6, 0);
            this.tblFilter.Controls.Add(this._to, 7, 0);
            this.tblFilter.Controls.Add(this.btnFind, 8, 0);
            this.tblFilter.Controls.Add(this.btnAll, 9, 0);
            this.tblFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblFilter.Location = new System.Drawing.Point(14, 14);
            this.tblFilter.Name = "tblFilter";
            this.tblFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tblFilter.Size = new System.Drawing.Size(1292, 64);
            this.tblFilter.TabIndex = 0;
            // 
            // lblInvoice
            // 
            this.lblInvoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInvoice.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblInvoice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblInvoice.Location = new System.Drawing.Point(3, 0);
            this.lblInvoice.Name = "lblInvoice";
            this.lblInvoice.Size = new System.Drawing.Size(74, 64);
            this.lblInvoice.TabIndex = 0;
            this.lblInvoice.Text = "Invoice No.";
            this.lblInvoice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _search
            // 
            this._search.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this._search.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this._search.Dock = System.Windows.Forms.DockStyle.Fill;
            this._search.Font = new System.Drawing.Font("Century Gothic", 10F);
            this._search.Location = new System.Drawing.Point(83, 3);
            this._search.Name = "_search";
            this._search.Size = new System.Drawing.Size(216, 24);
            this._search.TabIndex = 1;
            this._search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Search_KeyDown);
            // 
            // lblSupplier
            // 
            this.lblSupplier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSupplier.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblSupplier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblSupplier.Location = new System.Drawing.Point(305, 0);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(64, 64);
            this.lblSupplier.TabIndex = 2;
            this.lblSupplier.Text = "Supplier";
            this.lblSupplier.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _supplierSearch
            // 
            this._supplierSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this._supplierSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this._supplierSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this._supplierSearch.Font = new System.Drawing.Font("Century Gothic", 10F);
            this._supplierSearch.Location = new System.Drawing.Point(375, 3);
            this._supplierSearch.Name = "_supplierSearch";
            this._supplierSearch.Size = new System.Drawing.Size(257, 24);
            this._supplierSearch.TabIndex = 3;
            this._supplierSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Search_KeyDown);
            // 
            // lblFrom
            // 
            this.lblFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFrom.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblFrom.Location = new System.Drawing.Point(638, 0);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(42, 64);
            this.lblFrom.TabIndex = 4;
            this.lblFrom.Text = "From";
            this.lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _from
            // 
            this._from.CustomFormat = "dd/MM/yyyy";
            this._from.Dock = System.Windows.Forms.DockStyle.Fill;
            this._from.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._from.Location = new System.Drawing.Point(686, 3);
            this._from.Name = "_from";
            this._from.Size = new System.Drawing.Size(176, 23);
            this._from.TabIndex = 5;
            // 
            // lblTo
            // 
            this.lblTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTo.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblTo.Location = new System.Drawing.Point(868, 0);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(22, 64);
            this.lblTo.TabIndex = 6;
            this.lblTo.Text = "To";
            this.lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _to
            // 
            this._to.CustomFormat = "dd/MM/yyyy";
            this._to.Dock = System.Windows.Forms.DockStyle.Fill;
            this._to.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._to.Location = new System.Drawing.Point(896, 3);
            this._to.Name = "_to";
            this._to.Size = new System.Drawing.Size(176, 23);
            this._to.TabIndex = 7;
            // 
            // btnFind
            // 
            this.btnFind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnFind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFind.FlatAppearance.BorderSize = 0;
            this.btnFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFind.ForeColor = System.Drawing.Color.White;
            this.btnFind.Location = new System.Drawing.Point(1078, 3);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(99, 58);
            this.btnFind.TabIndex = 8;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = false;
            this.btnFind.Click += new System.EventHandler(this.BtnFind_Click);
            // 
            // btnAll
            // 
            this.btnAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAll.FlatAppearance.BorderSize = 0;
            this.btnAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAll.ForeColor = System.Drawing.Color.White;
            this.btnAll.Location = new System.Drawing.Point(1183, 3);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(106, 58);
            this.btnAll.TabIndex = 9;
            this.btnAll.Text = "All";
            this.btnAll.UseVisualStyleBackColor = false;
            this.btnAll.Click += new System.EventHandler(this.BtnAll_Click);
            // 
            // _split
            // 
            this._split.Dock = System.Windows.Forms.DockStyle.Fill;
            this._split.Location = new System.Drawing.Point(0, 164);
            this._split.Name = "_split";
            // 
            // _split.Panel1
            // 
            this._split.Panel1.Controls.Add(this.pnlInvoices);
            this._split.Panel1.Padding = new System.Windows.Forms.Padding(10);
            // 
            // _split.Panel2
            // 
            this._split.Panel2.Controls.Add(this.pnlDetails);
            this._split.Panel2.Padding = new System.Windows.Forms.Padding(10);
            this._split.Size = new System.Drawing.Size(1320, 656);
            this._split.SplitterDistance = 440;
            this._split.SplitterWidth = 8;
            this._split.TabIndex = 0;
            // 
            // pnlInvoices
            // 
            this.pnlInvoices.BackColor = System.Drawing.Color.White;
            this.pnlInvoices.Controls.Add(this._invoices);
            this.pnlInvoices.Controls.Add(this.lblInvoicesTitle);
            this.pnlInvoices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInvoices.Location = new System.Drawing.Point(10, 10);
            this.pnlInvoices.Name = "pnlInvoices";
            this.pnlInvoices.Padding = new System.Windows.Forms.Padding(8);
            this.pnlInvoices.Size = new System.Drawing.Size(420, 636);
            this.pnlInvoices.TabIndex = 0;
            // 
            // _invoices
            // 
            this._invoices.AllowUserToAddRows = false;
            this._invoices.AllowUserToDeleteRows = false;
            this._invoices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._invoices.BackgroundColor = System.Drawing.Color.White;
            this._invoices.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._invoices.Dock = System.Windows.Forms.DockStyle.Fill;
            this._invoices.Location = new System.Drawing.Point(8, 42);
            this._invoices.MultiSelect = false;
            this._invoices.Name = "_invoices";
            this._invoices.ReadOnly = true;
            this._invoices.RowHeadersVisible = false;
            this._invoices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._invoices.Size = new System.Drawing.Size(404, 586);
            this._invoices.TabIndex = 0;
            this._invoices.SelectionChanged += new System.EventHandler(this.Invoices_SelectionChanged);
            this._invoices.DoubleClick += new System.EventHandler(this.Invoices_DoubleClick);
            // 
            // lblInvoicesTitle
            // 
            this.lblInvoicesTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblInvoicesTitle.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.lblInvoicesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblInvoicesTitle.Location = new System.Drawing.Point(8, 8);
            this.lblInvoicesTitle.Name = "lblInvoicesTitle";
            this.lblInvoicesTitle.Size = new System.Drawing.Size(404, 34);
            this.lblInvoicesTitle.TabIndex = 1;
            this.lblInvoicesTitle.Text = "Posted invoices";
            this.lblInvoicesTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlDetails
            // 
            this.pnlDetails.BackColor = System.Drawing.Color.White;
            this.pnlDetails.Controls.Add(this._lines);
            this.pnlDetails.Controls.Add(this.tblFooter);
            this.pnlDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetails.Location = new System.Drawing.Point(10, 10);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Padding = new System.Windows.Forms.Padding(8);
            this.pnlDetails.Size = new System.Drawing.Size(852, 636);
            this.pnlDetails.TabIndex = 0;
            // 
            // _lines
            // 
            this._lines.AllowUserToAddRows = false;
            this._lines.AllowUserToDeleteRows = false;
            this._lines.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._lines.BackgroundColor = System.Drawing.Color.White;
            this._lines.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._lines.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lines.Location = new System.Drawing.Point(8, 8);
            this._lines.Name = "_lines";
            this._lines.ReadOnly = true;
            this._lines.RowHeadersVisible = false;
            this._lines.Size = new System.Drawing.Size(836, 544);
            this._lines.TabIndex = 0;
            // 
            // tblFooter
            // 
            this.tblFooter.ColumnCount = 2;
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tblFooter.Controls.Add(this._selectedInfo, 0, 0);
            this.tblFooter.Controls.Add(this.flowActions, 1, 0);
            this.tblFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tblFooter.Location = new System.Drawing.Point(8, 552);
            this.tblFooter.Name = "tblFooter";
            this.tblFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblFooter.Size = new System.Drawing.Size(836, 76);
            this.tblFooter.TabIndex = 1;
            // 
            // _selectedInfo
            // 
            this._selectedInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this._selectedInfo.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this._selectedInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._selectedInfo.Location = new System.Drawing.Point(3, 0);
            this._selectedInfo.Name = "_selectedInfo";
            this._selectedInfo.Size = new System.Drawing.Size(430, 76);
            this._selectedInfo.TabIndex = 0;
            this._selectedInfo.Text = "Select an invoice";
            this._selectedInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flowActions
            // 
            this.flowActions.Controls.Add(this.btnPrint);
            this.flowActions.Controls.Add(this.btnPreview);
            this.flowActions.Controls.Add(this.btnDelete);
            this.flowActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowActions.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowActions.Location = new System.Drawing.Point(439, 3);
            this.flowActions.Name = "flowActions";
            this.flowActions.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.flowActions.Size = new System.Drawing.Size(394, 70);
            this.flowActions.TabIndex = 1;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(291, 15);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 42);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnPreview.FlatAppearance.BorderSize = 0;
            this.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreview.ForeColor = System.Drawing.Color.White;
            this.btnPreview.Location = new System.Drawing.Point(150, 15);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(135, 42);
            this.btnPreview.TabIndex = 1;
            this.btnPreview.Text = "Print Preview";
            this.btnPreview.UseVisualStyleBackColor = false;
            this.btnPreview.Click += new System.EventHandler(this.BtnPreview_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnDelete.Enabled = false;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(14, 15);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(130, 42);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete Invoice";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // InvoiceHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1320, 820);
            this.Controls.Add(this._split);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Century Gothic", 9.5F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(900, 620);
            this.Name = "InvoiceHistoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Stock-In Invoice History";
            this.Load += new System.EventHandler(this.InvoiceHistoryForm_Load);
            this.Shown += new System.EventHandler(this.InvoiceHistoryForm_Shown);
            this.Resize += new System.EventHandler(this.InvoiceHistoryForm_Resize);
            this.pnlFilter.ResumeLayout(false);
            this.tblFilter.ResumeLayout(false);
            this.tblFilter.PerformLayout();
            this._split.Panel1.ResumeLayout(false);
            this._split.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._split)).EndInit();
            this._split.ResumeLayout(false);
            this.pnlInvoices.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._invoices)).EndInit();
            this.pnlDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._lines)).EndInit();
            this.tblFooter.ResumeLayout(false);
            this.flowActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
