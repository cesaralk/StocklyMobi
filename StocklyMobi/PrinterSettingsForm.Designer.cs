namespace StocklyMobi
{
    partial class PrinterSettingsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlCard = new System.Windows.Forms.Panel();
            this.tblMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblReceiptPrinter = new System.Windows.Forms.Label();
            this.cboReceiptPrinter = new System.Windows.Forms.ComboBox();
            this.btnTestReceipt = new System.Windows.Forms.Button();
            this.lblInvoicePrinter = new System.Windows.Forms.Label();
            this.cboInvoicePrinter = new System.Windows.Forms.ComboBox();
            this.btnTestInvoice = new System.Windows.Forms.Button();
            this.chkAutoPrintReceipt = new System.Windows.Forms.CheckBox();
            this.chkAutoOpenCashDrawer = new System.Windows.Forms.CheckBox();
            this.btnTestDrawer = new System.Windows.Forms.Button();
            this.lblDrawerPin = new System.Windows.Forms.Label();
            this.cboDrawerPin = new System.Windows.Forms.ComboBox();
            this.lblDrawerProfile = new System.Windows.Forms.Label();
            this.cboDrawerProfile = new System.Windows.Forms.ComboBox();
            this.lblDrawerHelp = new System.Windows.Forms.Label();
            this.lblReceiptTopTrim = new System.Windows.Forms.Label();
            this.numReceiptTopTrim = new System.Windows.Forms.NumericUpDown();
            this.lblReceiptTopTrimHelp = new System.Windows.Forms.Label();
            this.lblReceiptHelp = new System.Windows.Forms.Label();
            this.lblInvoiceHelp = new System.Windows.Forms.Label();
            this.pnlActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblSaved = new System.Windows.Forms.Label();
            this.lblDefaultPrinter = new System.Windows.Forms.Label();
            this.pnlCard.SuspendLayout();
            this.tblMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numReceiptTopTrim)).BeginInit();
            this.pnlActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Century Gothic", 15F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Padding = new System.Windows.Forms.Padding(22, 10, 10, 4);
            this.lblHeader.Size = new System.Drawing.Size(920, 76);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Printer Settings\r\nChoose a separate default printer for receipts and A4 stock-in invoices.";
            // 
            // pnlCard
            // 
            this.pnlCard.BackColor = System.Drawing.Color.White;
            this.pnlCard.Controls.Add(this.tblMain);
            this.pnlCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCard.Location = new System.Drawing.Point(0, 76);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Padding = new System.Windows.Forms.Padding(24);
            this.pnlCard.Size = new System.Drawing.Size(920, 668);
            this.pnlCard.TabIndex = 1;
            // 
            // tblMain
            // 
            this.tblMain.ColumnCount = 3;
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 190F));
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tblMain.Controls.Add(this.lblReceiptPrinter, 0, 0);
            this.tblMain.Controls.Add(this.cboReceiptPrinter, 1, 0);
            this.tblMain.Controls.Add(this.btnTestReceipt, 2, 0);
            this.tblMain.Controls.Add(this.lblReceiptHelp, 1, 1);
            this.tblMain.Controls.Add(this.lblInvoicePrinter, 0, 2);
            this.tblMain.Controls.Add(this.cboInvoicePrinter, 1, 2);
            this.tblMain.Controls.Add(this.btnTestInvoice, 2, 2);
            this.tblMain.Controls.Add(this.lblInvoiceHelp, 1, 3);
            this.tblMain.Controls.Add(this.chkAutoPrintReceipt, 1, 4);
            this.tblMain.Controls.Add(this.chkAutoOpenCashDrawer, 1, 5);
            this.tblMain.Controls.Add(this.btnTestDrawer, 2, 5);
            this.tblMain.Controls.Add(this.lblDrawerPin, 0, 6);
            this.tblMain.Controls.Add(this.cboDrawerPin, 1, 6);
            this.tblMain.Controls.Add(this.lblDrawerProfile, 0, 7);
            this.tblMain.Controls.Add(this.cboDrawerProfile, 1, 7);
            this.tblMain.Controls.Add(this.lblDrawerHelp, 1, 8);
            this.tblMain.Controls.Add(this.lblReceiptTopTrim, 0, 9);
            this.tblMain.Controls.Add(this.numReceiptTopTrim, 1, 9);
            this.tblMain.Controls.Add(this.lblReceiptTopTrimHelp, 2, 9);
            this.tblMain.Controls.Add(this.lblDefaultPrinter, 1, 10);
            this.tblMain.Controls.Add(this.pnlActions, 0, 11);
            this.tblMain.Controls.Add(this.lblSaved, 1, 12);
            this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMain.Location = new System.Drawing.Point(24, 24);
            this.tblMain.Name = "tblMain";
            this.tblMain.RowCount = 13;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.Size = new System.Drawing.Size(872, 620);
            this.tblMain.TabIndex = 0;
            this.tblMain.SetColumnSpan(this.lblReceiptHelp, 2);
            this.tblMain.SetColumnSpan(this.lblInvoiceHelp, 2);
            this.tblMain.SetColumnSpan(this.chkAutoPrintReceipt, 2);
            this.tblMain.SetColumnSpan(this.lblDrawerHelp, 2);
            this.tblMain.SetColumnSpan(this.lblDefaultPrinter, 2);
            this.tblMain.SetColumnSpan(this.pnlActions, 3);
            this.tblMain.SetColumnSpan(this.lblSaved, 2);
            // 
            // lblReceiptPrinter
            // 
            this.lblReceiptPrinter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReceiptPrinter.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.lblReceiptPrinter.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblReceiptPrinter.Location = new System.Drawing.Point(3, 0);
            this.lblReceiptPrinter.Name = "lblReceiptPrinter";
            this.lblReceiptPrinter.Size = new System.Drawing.Size(184, 52);
            this.lblReceiptPrinter.TabIndex = 0;
            this.lblReceiptPrinter.Text = "Cashier receipt printer";
            this.lblReceiptPrinter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboReceiptPrinter
            // 
            this.cboReceiptPrinter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboReceiptPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReceiptPrinter.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.cboReceiptPrinter.FormattingEnabled = true;
            this.cboReceiptPrinter.Location = new System.Drawing.Point(193, 10);
            this.cboReceiptPrinter.Margin = new System.Windows.Forms.Padding(3, 10, 8, 8);
            this.cboReceiptPrinter.Name = "cboReceiptPrinter";
            this.cboReceiptPrinter.Size = new System.Drawing.Size(533, 29);
            this.cboReceiptPrinter.TabIndex = 1;
            // 
            // btnTestReceipt
            // 
            this.btnTestReceipt.BackColor = System.Drawing.Color.FromArgb(14, 165, 233);
            this.btnTestReceipt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTestReceipt.FlatAppearance.BorderSize = 0;
            this.btnTestReceipt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestReceipt.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnTestReceipt.ForeColor = System.Drawing.Color.White;
            this.btnTestReceipt.Location = new System.Drawing.Point(742, 7);
            this.btnTestReceipt.Margin = new System.Windows.Forms.Padding(8, 7, 3, 7);
            this.btnTestReceipt.Name = "btnTestReceipt";
            this.btnTestReceipt.Size = new System.Drawing.Size(127, 38);
            this.btnTestReceipt.TabIndex = 2;
            this.btnTestReceipt.Text = "Test Receipt";
            this.btnTestReceipt.UseVisualStyleBackColor = false;
            this.btnTestReceipt.Click += new System.EventHandler(this.BtnTestReceipt_Click);
            // 
            // lblReceiptHelp
            // 
            this.lblReceiptHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReceiptHelp.Font = new System.Drawing.Font("Century Gothic", 8.5F);
            this.lblReceiptHelp.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblReceiptHelp.Location = new System.Drawing.Point(193, 52);
            this.lblReceiptHelp.Name = "lblReceiptHelp";
            this.lblReceiptHelp.Size = new System.Drawing.Size(676, 42);
            this.lblReceiptHelp.TabIndex = 3;
            this.lblReceiptHelp.Text = "Used after Settle and when reprinting receipts. The receipt paper stays 80mm.";
            this.lblReceiptHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblInvoicePrinter
            // 
            this.lblInvoicePrinter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInvoicePrinter.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.lblInvoicePrinter.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblInvoicePrinter.Location = new System.Drawing.Point(3, 94);
            this.lblInvoicePrinter.Name = "lblInvoicePrinter";
            this.lblInvoicePrinter.Size = new System.Drawing.Size(184, 52);
            this.lblInvoicePrinter.TabIndex = 4;
            this.lblInvoicePrinter.Text = "Stock-in invoice printer";
            this.lblInvoicePrinter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboInvoicePrinter
            // 
            this.cboInvoicePrinter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboInvoicePrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInvoicePrinter.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.cboInvoicePrinter.FormattingEnabled = true;
            this.cboInvoicePrinter.Location = new System.Drawing.Point(193, 104);
            this.cboInvoicePrinter.Margin = new System.Windows.Forms.Padding(3, 10, 8, 8);
            this.cboInvoicePrinter.Name = "cboInvoicePrinter";
            this.cboInvoicePrinter.Size = new System.Drawing.Size(533, 29);
            this.cboInvoicePrinter.TabIndex = 5;
            // 
            // btnTestInvoice
            // 
            this.btnTestInvoice.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnTestInvoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTestInvoice.FlatAppearance.BorderSize = 0;
            this.btnTestInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestInvoice.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnTestInvoice.ForeColor = System.Drawing.Color.White;
            this.btnTestInvoice.Location = new System.Drawing.Point(742, 101);
            this.btnTestInvoice.Margin = new System.Windows.Forms.Padding(8, 7, 3, 7);
            this.btnTestInvoice.Name = "btnTestInvoice";
            this.btnTestInvoice.Size = new System.Drawing.Size(127, 38);
            this.btnTestInvoice.TabIndex = 6;
            this.btnTestInvoice.Text = "Test A4";
            this.btnTestInvoice.UseVisualStyleBackColor = false;
            this.btnTestInvoice.Click += new System.EventHandler(this.BtnTestInvoice_Click);
            // 
            // lblInvoiceHelp
            // 
            this.lblInvoiceHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInvoiceHelp.Font = new System.Drawing.Font("Century Gothic", 8.5F);
            this.lblInvoiceHelp.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblInvoiceHelp.Location = new System.Drawing.Point(193, 146);
            this.lblInvoiceHelp.Name = "lblInvoiceHelp";
            this.lblInvoiceHelp.Size = new System.Drawing.Size(676, 42);
            this.lblInvoiceHelp.TabIndex = 7;
            this.lblInvoiceHelp.Text = "Used by Products & Stock-In and Invoice History. Paper size is forced to A4 portrait.";
            this.lblInvoiceHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkAutoPrintReceipt
            // 
            this.chkAutoPrintReceipt.AutoSize = true;
            this.chkAutoPrintReceipt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkAutoPrintReceipt.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.chkAutoPrintReceipt.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.chkAutoPrintReceipt.Location = new System.Drawing.Point(193, 191);
            this.chkAutoPrintReceipt.Name = "chkAutoPrintReceipt";
            this.chkAutoPrintReceipt.Size = new System.Drawing.Size(676, 46);
            this.chkAutoPrintReceipt.TabIndex = 8;
            this.chkAutoPrintReceipt.Text = "Automatically print the receipt after a successful settlement";
            this.chkAutoPrintReceipt.UseVisualStyleBackColor = true;
            // 
            // chkAutoOpenCashDrawer
            // 
            this.chkAutoOpenCashDrawer.AutoSize = true;
            this.chkAutoOpenCashDrawer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkAutoOpenCashDrawer.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.chkAutoOpenCashDrawer.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.chkAutoOpenCashDrawer.Location = new System.Drawing.Point(193, 237);
            this.chkAutoOpenCashDrawer.Name = "chkAutoOpenCashDrawer";
            this.chkAutoOpenCashDrawer.Size = new System.Drawing.Size(533, 46);
            this.chkAutoOpenCashDrawer.TabIndex = 9;
            this.chkAutoOpenCashDrawer.Text = "Open the cash drawer after a successful settlement";
            this.chkAutoOpenCashDrawer.UseVisualStyleBackColor = true;
            // 
            // btnTestDrawer
            // 
            this.btnTestDrawer.BackColor = System.Drawing.Color.FromArgb(124, 58, 237);
            this.btnTestDrawer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTestDrawer.FlatAppearance.BorderSize = 0;
            this.btnTestDrawer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestDrawer.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnTestDrawer.ForeColor = System.Drawing.Color.White;
            this.btnTestDrawer.Location = new System.Drawing.Point(742, 244);
            this.btnTestDrawer.Margin = new System.Windows.Forms.Padding(8, 7, 3, 7);
            this.btnTestDrawer.Name = "btnTestDrawer";
            this.btnTestDrawer.Size = new System.Drawing.Size(127, 38);
            this.btnTestDrawer.TabIndex = 10;
            this.btnTestDrawer.Text = "Test Drawer";
            this.btnTestDrawer.UseVisualStyleBackColor = false;
            this.btnTestDrawer.Click += new System.EventHandler(this.BtnTestDrawer_Click);
            // 
            // lblDrawerPin
            // 
            this.lblDrawerPin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDrawerPin.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.lblDrawerPin.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblDrawerPin.Location = new System.Drawing.Point(3, 286);
            this.lblDrawerPin.Name = "lblDrawerPin";
            this.lblDrawerPin.Size = new System.Drawing.Size(184, 48);
            this.lblDrawerPin.TabIndex = 11;
            this.lblDrawerPin.Text = "Drawer connector";
            this.lblDrawerPin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboDrawerPin
            // 
            this.cboDrawerPin.Dock = System.Windows.Forms.DockStyle.Left;
            this.cboDrawerPin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDrawerPin.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.cboDrawerPin.FormattingEnabled = true;
            this.cboDrawerPin.Items.AddRange(new object[] {
            "Pin 2 — most common",
            "Pin 5"});
            this.cboDrawerPin.Location = new System.Drawing.Point(193, 295);
            this.cboDrawerPin.Margin = new System.Windows.Forms.Padding(3, 9, 8, 8);
            this.cboDrawerPin.Name = "cboDrawerPin";
            this.cboDrawerPin.Size = new System.Drawing.Size(250, 29);
            this.cboDrawerPin.TabIndex = 12;
            // 
            // lblDrawerProfile
            // 
            this.lblDrawerProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDrawerProfile.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.lblDrawerProfile.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblDrawerProfile.Location = new System.Drawing.Point(3, 334);
            this.lblDrawerProfile.Name = "lblDrawerProfile";
            this.lblDrawerProfile.Size = new System.Drawing.Size(184, 48);
            this.lblDrawerProfile.TabIndex = 13;
            this.lblDrawerProfile.Text = "Command profile";
            this.lblDrawerProfile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboDrawerProfile
            // 
            this.cboDrawerProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboDrawerProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDrawerProfile.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.cboDrawerProfile.FormattingEnabled = true;
            this.cboDrawerProfile.Items.AddRange(new object[] {
            "Generic ASCII 48/49 — recommended",
            "Standard binary 0/1",
            "Standard ASCII 48/49",
            "Short pulse binary 0/1",
            "Short pulse ASCII 48/49",
            "Legacy binary without initialize"});
            this.cboDrawerProfile.Location = new System.Drawing.Point(193, 343);
            this.cboDrawerProfile.Margin = new System.Windows.Forms.Padding(3, 9, 8, 8);
            this.cboDrawerProfile.Name = "cboDrawerProfile";
            this.cboDrawerProfile.Size = new System.Drawing.Size(533, 27);
            this.cboDrawerProfile.TabIndex = 14;
            // 
            // 
            // lblDrawerHelp
            // 
            this.lblDrawerHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDrawerHelp.Font = new System.Drawing.Font("Century Gothic", 8.5F);
            this.lblDrawerHelp.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblDrawerHelp.Location = new System.Drawing.Point(193, 382);
            this.lblDrawerHelp.Name = "lblDrawerHelp";
            this.lblDrawerHelp.Size = new System.Drawing.Size(676, 46);
            this.lblDrawerHelp.TabIndex = 13;
            this.lblDrawerHelp.Text = "Test the recommended profile first. If the drawer stays closed, answer No and the next compatible command will be selected automatically.";
            this.lblDrawerHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 
            // lblReceiptTopTrim
            // 
            this.lblReceiptTopTrim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReceiptTopTrim.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.lblReceiptTopTrim.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblReceiptTopTrim.Location = new System.Drawing.Point(3, 428);
            this.lblReceiptTopTrim.Name = "lblReceiptTopTrim";
            this.lblReceiptTopTrim.Size = new System.Drawing.Size(184, 50);
            this.lblReceiptTopTrim.TabIndex = 9;
            this.lblReceiptTopTrim.Text = "Fine top trim (mm)";
            this.lblReceiptTopTrim.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numReceiptTopTrim
            // 
            this.numReceiptTopTrim.DecimalPlaces = 1;
            this.numReceiptTopTrim.Dock = System.Windows.Forms.DockStyle.Left;
            this.numReceiptTopTrim.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.numReceiptTopTrim.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numReceiptTopTrim.Location = new System.Drawing.Point(193, 438);
            this.numReceiptTopTrim.Margin = new System.Windows.Forms.Padding(3, 10, 8, 8);
            this.numReceiptTopTrim.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numReceiptTopTrim.Name = "numReceiptTopTrim";
            this.numReceiptTopTrim.Size = new System.Drawing.Size(140, 28);
            this.numReceiptTopTrim.TabIndex = 9;
            this.numReceiptTopTrim.ThousandsSeparator = true;
            // 
            // lblReceiptTopTrimHelp
            // 
            this.lblReceiptTopTrimHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReceiptTopTrimHelp.Font = new System.Drawing.Font("Century Gothic", 8F);
            this.lblReceiptTopTrimHelp.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblReceiptTopTrimHelp.Location = new System.Drawing.Point(737, 428);
            this.lblReceiptTopTrimHelp.Name = "lblReceiptTopTrimHelp";
            this.lblReceiptTopTrimHelp.Size = new System.Drawing.Size(132, 50);
            this.lblReceiptTopTrimHelp.TabIndex = 10;
            this.lblReceiptTopTrimHelp.Text = "Optional fine adjustment only: 0–2 mm.";
            this.lblReceiptTopTrimHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 
            // lblDefaultPrinter
            // 
            this.lblDefaultPrinter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDefaultPrinter.Font = new System.Drawing.Font("Century Gothic", 8.5F);
            this.lblDefaultPrinter.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblDefaultPrinter.Location = new System.Drawing.Point(193, 478);
            this.lblDefaultPrinter.Name = "lblDefaultPrinter";
            this.lblDefaultPrinter.Size = new System.Drawing.Size(676, 38);
            this.lblDefaultPrinter.TabIndex = 9;
            this.lblDefaultPrinter.Text = "Windows default printer:";
            this.lblDefaultPrinter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlActions
            // 
            this.pnlActions.Controls.Add(this.btnSave);
            this.pnlActions.Controls.Add(this.btnRefresh);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlActions.Location = new System.Drawing.Point(3, 519);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.pnlActions.Size = new System.Drawing.Size(866, 56);
            this.pnlActions.TabIndex = 10;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(16, 185, 129);
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Century Gothic", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(6, 13);
            this.btnSave.Margin = new System.Windows.Forms.Padding(6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 42);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save Settings";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Century Gothic", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(168, 13);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(6);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(140, 42);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh List";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // lblSaved
            // 
            this.lblSaved.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSaved.Font = new System.Drawing.Font("Century Gothic", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblSaved.ForeColor = System.Drawing.Color.FromArgb(16, 185, 129);
            this.lblSaved.Location = new System.Drawing.Point(193, 578);
            this.lblSaved.Name = "lblSaved";
            this.lblSaved.Size = new System.Drawing.Size(676, 42);
            this.lblSaved.TabIndex = 11;
            this.lblSaved.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PrinterSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.ClientSize = new System.Drawing.Size(920, 768);
            this.Controls.Add(this.pnlCard);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Century Gothic", 9.5F);
            this.MinimumSize = new System.Drawing.Size(720, 720);
            this.Name = "PrinterSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Printer Settings";
            this.Load += new System.EventHandler(this.PrinterSettingsForm_Load);
            this.pnlCard.ResumeLayout(false);
            this.tblMain.ResumeLayout(false);
            this.tblMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numReceiptTopTrim)).EndInit();
            this.pnlActions.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.TableLayoutPanel tblMain;
        private System.Windows.Forms.Label lblReceiptPrinter;
        private System.Windows.Forms.ComboBox cboReceiptPrinter;
        private System.Windows.Forms.Button btnTestReceipt;
        private System.Windows.Forms.Label lblInvoicePrinter;
        private System.Windows.Forms.ComboBox cboInvoicePrinter;
        private System.Windows.Forms.Button btnTestInvoice;
        private System.Windows.Forms.CheckBox chkAutoPrintReceipt;
        private System.Windows.Forms.CheckBox chkAutoOpenCashDrawer;
        private System.Windows.Forms.Button btnTestDrawer;
        private System.Windows.Forms.Label lblDrawerPin;
        private System.Windows.Forms.ComboBox cboDrawerPin;
        private System.Windows.Forms.Label lblDrawerProfile;
        private System.Windows.Forms.ComboBox cboDrawerProfile;
        private System.Windows.Forms.Label lblDrawerHelp;
        private System.Windows.Forms.Label lblReceiptTopTrim;
        private System.Windows.Forms.NumericUpDown numReceiptTopTrim;
        private System.Windows.Forms.Label lblReceiptTopTrimHelp;
        private System.Windows.Forms.Label lblReceiptHelp;
        private System.Windows.Forms.Label lblInvoiceHelp;
        private System.Windows.Forms.FlowLayoutPanel pnlActions;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblSaved;
        private System.Windows.Forms.Label lblDefaultPrinter;
    }
}
