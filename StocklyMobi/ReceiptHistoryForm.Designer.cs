namespace StocklyMobi
{
    partial class ReceiptHistoryForm
    {
        private System.ComponentModel.IContainer components=null;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.TableLayoutPanel tblFilters;
        private System.Windows.Forms.Label lblReceipt;
        private System.Windows.Forms.TextBox _receiptNo;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker _from;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker _to;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.SplitContainer _split;
        private System.Windows.Forms.Panel pnlReceipts;
        private System.Windows.Forms.Label lblReceiptsTitle;
        private System.Windows.Forms.DataGridView _receipts;
        private System.Windows.Forms.Panel pnlDetails;
        private System.Windows.Forms.DataGridView _lines;
        private System.Windows.Forms.TableLayoutPanel tblFooter;
        private System.Windows.Forms.Label _summary;
        private System.Windows.Forms.FlowLayoutPanel flowActions;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnPreview;
        protected override void Dispose(bool disposing){if(disposing&&components!=null)components.Dispose();
        base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReceiptHistoryForm));
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlFilters = new System.Windows.Forms.Panel();
            this.tblFilters = new System.Windows.Forms.TableLayoutPanel();
            this.lblReceipt = new System.Windows.Forms.Label();
            this._receiptNo = new System.Windows.Forms.TextBox();
            this.lblFrom = new System.Windows.Forms.Label();
            this._from = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this._to = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnToday = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this._split = new System.Windows.Forms.SplitContainer();
            this.pnlReceipts = new System.Windows.Forms.Panel();
            this._receipts = new System.Windows.Forms.DataGridView();
            this.lblReceiptsTitle = new System.Windows.Forms.Label();
            this.pnlDetails = new System.Windows.Forms.Panel();
            this._lines = new System.Windows.Forms.DataGridView();
            this.tblFooter = new System.Windows.Forms.TableLayoutPanel();
            this._summary = new System.Windows.Forms.Label();
            this.flowActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.pnlFilters.SuspendLayout();
            this.tblFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._split)).BeginInit();
            this._split.Panel1.SuspendLayout();
            this._split.Panel2.SuspendLayout();
            this._split.SuspendLayout();
            this.pnlReceipts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._receipts)).BeginInit();
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
            this.lblHeader.Size = new System.Drawing.Size(1360, 72);
            this.lblHeader.TabIndex = 2;
            this.lblHeader.Text = "Receipt Search\r\nFind a settled receipt by receipt number or exact date and time, " +
    "review every transaction line and discount, then preview or print it again.";
            // 
            // pnlFilters
            // 
            this.pnlFilters.BackColor = System.Drawing.Color.White;
            this.pnlFilters.Controls.Add(this.tblFilters);
            this.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilters.Location = new System.Drawing.Point(0, 72);
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Padding = new System.Windows.Forms.Padding(14);
            this.pnlFilters.Size = new System.Drawing.Size(1360, 98);
            this.pnlFilters.TabIndex = 1;
            // 
            // tblFilters
            // 
            this.tblFilters.ColumnCount = 9;
            this.tblFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tblFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tblFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tblFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tblFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tblFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tblFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tblFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tblFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tblFilters.Controls.Add(this.lblReceipt, 0, 0);
            this.tblFilters.Controls.Add(this._receiptNo, 1, 0);
            this.tblFilters.Controls.Add(this.lblFrom, 2, 0);
            this.tblFilters.Controls.Add(this._from, 3, 0);
            this.tblFilters.Controls.Add(this.lblTo, 4, 0);
            this.tblFilters.Controls.Add(this._to, 5, 0);
            this.tblFilters.Controls.Add(this.btnSearch, 6, 0);
            this.tblFilters.Controls.Add(this.btnToday, 7, 0);
            this.tblFilters.Controls.Add(this.btnAll, 8, 0);
            this.tblFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblFilters.Location = new System.Drawing.Point(14, 14);
            this.tblFilters.Name = "tblFilters";
            this.tblFilters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblFilters.Size = new System.Drawing.Size(1332, 70);
            this.tblFilters.TabIndex = 0;
            // 
            // lblReceipt
            // 
            this.lblReceipt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReceipt.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblReceipt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblReceipt.Location = new System.Drawing.Point(3, 0);
            this.lblReceipt.Name = "lblReceipt";
            this.lblReceipt.Size = new System.Drawing.Size(76, 70);
            this.lblReceipt.TabIndex = 0;
            this.lblReceipt.Text = "Receipt No.";
            this.lblReceipt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _receiptNo
            // 
            this._receiptNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this._receiptNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this._receiptNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this._receiptNo.Font = new System.Drawing.Font("Century Gothic", 10F);
            this._receiptNo.Location = new System.Drawing.Point(85, 3);
            this._receiptNo.Name = "_receiptNo";
            this._receiptNo.Size = new System.Drawing.Size(321, 24);
            this._receiptNo.TabIndex = 1;
            this._receiptNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReceiptNo_KeyDown);
            // 
            // lblFrom
            // 
            this.lblFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFrom.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblFrom.Location = new System.Drawing.Point(412, 0);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(49, 70);
            this.lblFrom.TabIndex = 2;
            this.lblFrom.Text = "From";
            this.lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _from
            // 
            this._from.CustomFormat = "dd/MM/yyyy hh:mm:ss tt";
            this._from.Dock = System.Windows.Forms.DockStyle.Fill;
            this._from.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._from.Location = new System.Drawing.Point(467, 3);
            this._from.Name = "_from";
            this._from.ShowUpDown = true;
            this._from.Size = new System.Drawing.Size(251, 23);
            this._from.TabIndex = 3;
            // 
            // lblTo
            // 
            this.lblTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTo.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblTo.Location = new System.Drawing.Point(724, 0);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(32, 70);
            this.lblTo.TabIndex = 4;
            this.lblTo.Text = "To";
            this.lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _to
            // 
            this._to.CustomFormat = "dd/MM/yyyy hh:mm:ss tt";
            this._to.Dock = System.Windows.Forms.DockStyle.Fill;
            this._to.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._to.Location = new System.Drawing.Point(762, 3);
            this._to.Name = "_to";
            this._to.ShowUpDown = true;
            this._to.Size = new System.Drawing.Size(251, 23);
            this._to.TabIndex = 5;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(1019, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(99, 64);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // btnToday
            // 
            this.btnToday.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(165)))), ((int)(((byte)(233)))));
            this.btnToday.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnToday.FlatAppearance.BorderSize = 0;
            this.btnToday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToday.ForeColor = System.Drawing.Color.White;
            this.btnToday.Location = new System.Drawing.Point(1124, 3);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(99, 64);
            this.btnToday.TabIndex = 7;
            this.btnToday.Text = "Today";
            this.btnToday.UseVisualStyleBackColor = false;
            this.btnToday.Click += new System.EventHandler(this.BtnToday_Click);
            // 
            // btnAll
            // 
            this.btnAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAll.FlatAppearance.BorderSize = 0;
            this.btnAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAll.ForeColor = System.Drawing.Color.White;
            this.btnAll.Location = new System.Drawing.Point(1229, 3);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(100, 64);
            this.btnAll.TabIndex = 8;
            this.btnAll.Text = "All";
            this.btnAll.UseVisualStyleBackColor = false;
            this.btnAll.Click += new System.EventHandler(this.BtnAll_Click);
            // 
            // _split
            // 
            this._split.Dock = System.Windows.Forms.DockStyle.Fill;
            this._split.Location = new System.Drawing.Point(0, 170);
            this._split.Name = "_split";
            // 
            // _split.Panel1
            // 
            this._split.Panel1.Controls.Add(this.pnlReceipts);
            this._split.Panel1.Padding = new System.Windows.Forms.Padding(10);
            // 
            // _split.Panel2
            // 
            this._split.Panel2.Controls.Add(this.pnlDetails);
            this._split.Panel2.Padding = new System.Windows.Forms.Padding(10);
            this._split.Size = new System.Drawing.Size(1360, 650);
            this._split.SplitterDistance = 453;
            this._split.SplitterWidth = 8;
            this._split.TabIndex = 0;
            // 
            // pnlReceipts
            // 
            this.pnlReceipts.BackColor = System.Drawing.Color.White;
            this.pnlReceipts.Controls.Add(this._receipts);
            this.pnlReceipts.Controls.Add(this.lblReceiptsTitle);
            this.pnlReceipts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlReceipts.Location = new System.Drawing.Point(10, 10);
            this.pnlReceipts.Name = "pnlReceipts";
            this.pnlReceipts.Padding = new System.Windows.Forms.Padding(8);
            this.pnlReceipts.Size = new System.Drawing.Size(433, 630);
            this.pnlReceipts.TabIndex = 0;
            // 
            // _receipts
            // 
            this._receipts.AllowUserToAddRows = false;
            this._receipts.AllowUserToDeleteRows = false;
            this._receipts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._receipts.BackgroundColor = System.Drawing.Color.White;
            this._receipts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._receipts.Dock = System.Windows.Forms.DockStyle.Fill;
            this._receipts.Location = new System.Drawing.Point(8, 42);
            this._receipts.MultiSelect = false;
            this._receipts.Name = "_receipts";
            this._receipts.ReadOnly = true;
            this._receipts.RowHeadersVisible = false;
            this._receipts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._receipts.Size = new System.Drawing.Size(417, 580);
            this._receipts.TabIndex = 0;
            this._receipts.SelectionChanged += new System.EventHandler(this.Receipts_SelectionChanged);
            this._receipts.DoubleClick += new System.EventHandler(this.Receipts_DoubleClick);
            // 
            // lblReceiptsTitle
            // 
            this.lblReceiptsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblReceiptsTitle.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.lblReceiptsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblReceiptsTitle.Location = new System.Drawing.Point(8, 8);
            this.lblReceiptsTitle.Name = "lblReceiptsTitle";
            this.lblReceiptsTitle.Size = new System.Drawing.Size(417, 34);
            this.lblReceiptsTitle.TabIndex = 1;
            this.lblReceiptsTitle.Text = "Settled receipts";
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
            this.pnlDetails.Size = new System.Drawing.Size(879, 630);
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
            this._lines.Size = new System.Drawing.Size(863, 522);
            this._lines.TabIndex = 0;
            // 
            // tblFooter
            // 
            this.tblFooter.ColumnCount = 2;
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 320F));
            this.tblFooter.Controls.Add(this._summary, 0, 0);
            this.tblFooter.Controls.Add(this.flowActions, 1, 0);
            this.tblFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tblFooter.Location = new System.Drawing.Point(8, 530);
            this.tblFooter.Name = "tblFooter";
            this.tblFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblFooter.Size = new System.Drawing.Size(863, 92);
            this.tblFooter.TabIndex = 1;
            // 
            // _summary
            // 
            this._summary.Dock = System.Windows.Forms.DockStyle.Fill;
            this._summary.Font = new System.Drawing.Font("Century Gothic", 9.5F, System.Drawing.FontStyle.Bold);
            this._summary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._summary.Location = new System.Drawing.Point(3, 0);
            this._summary.Name = "_summary";
            this._summary.Size = new System.Drawing.Size(537, 92);
            this._summary.TabIndex = 0;
            this._summary.Text = "Select a receipt";
            this._summary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flowActions
            // 
            this.flowActions.AutoScroll = true;
            this.flowActions.Controls.Add(this.btnPrint);
            this.flowActions.Controls.Add(this.btnPreview);
            this.flowActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowActions.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowActions.Location = new System.Drawing.Point(546, 3);
            this.flowActions.Name = "flowActions";
            this.flowActions.Padding = new System.Windows.Forms.Padding(0, 18, 0, 0);
            this.flowActions.Size = new System.Drawing.Size(314, 86);
            this.flowActions.TabIndex = 1;
            this.flowActions.WrapContents = false;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(211, 21);
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
            this.btnPreview.Location = new System.Drawing.Point(70, 21);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(135, 42);
            this.btnPreview.TabIndex = 1;
            this.btnPreview.Text = "Print Preview";
            this.btnPreview.UseVisualStyleBackColor = false;
            this.btnPreview.Click += new System.EventHandler(this.BtnPreview_Click);
            // 
            // ReceiptHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1360, 820);
            this.Controls.Add(this._split);
            this.Controls.Add(this.pnlFilters);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Century Gothic", 9.5F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(760, 520);
            this.Name = "ReceiptHistoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Receipt Search";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ReceiptHistoryForm_Load);
            this.Shown += new System.EventHandler(this.ReceiptHistoryForm_Shown);
            this.Resize += new System.EventHandler(this.ReceiptHistoryForm_Resize);
            this.pnlFilters.ResumeLayout(false);
            this.tblFilters.ResumeLayout(false);
            this.tblFilters.PerformLayout();
            this._split.Panel1.ResumeLayout(false);
            this._split.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._split)).EndInit();
            this._split.ResumeLayout(false);
            this.pnlReceipts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._receipts)).EndInit();
            this.pnlDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._lines)).EndInit();
            this.tblFooter.ResumeLayout(false);
            this.flowActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
