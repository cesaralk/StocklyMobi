namespace StocklyMobi
{
    partial class ExchangeRateSettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.TableLayoutPanel tblMain;
        private System.Windows.Forms.Label lblRate;
        private System.Windows.Forms.NumericUpDown numExchangeRate;
        private System.Windows.Forms.Label lblHelp;
        private System.Windows.Forms.Label lblUsdPreview;
        private System.Windows.Forms.Label lblLbpPreview;
        private System.Windows.Forms.Label lblPrecision;
        private System.Windows.Forms.FlowLayoutPanel pnlActions;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblSaved;

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
            this.lblRate = new System.Windows.Forms.Label();
            this.numExchangeRate = new System.Windows.Forms.NumericUpDown();
            this.lblHelp = new System.Windows.Forms.Label();
            this.lblUsdPreview = new System.Windows.Forms.Label();
            this.lblLbpPreview = new System.Windows.Forms.Label();
            this.lblPrecision = new System.Windows.Forms.Label();
            this.pnlActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblSaved = new System.Windows.Forms.Label();
            this.pnlCard.SuspendLayout();
            this.tblMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numExchangeRate)).BeginInit();
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
            this.lblHeader.Text = "Exchange Rate Settings\r\nSet the system USD to LBP rate used by Cashier and Services.";
            // 
            // pnlCard
            // 
            this.pnlCard.BackColor = System.Drawing.Color.White;
            this.pnlCard.Controls.Add(this.tblMain);
            this.pnlCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCard.Location = new System.Drawing.Point(0, 76);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Padding = new System.Windows.Forms.Padding(28);
            this.pnlCard.Size = new System.Drawing.Size(920, 390);
            this.pnlCard.TabIndex = 1;
            // 
            // tblMain
            // 
            this.tblMain.ColumnCount = 2;
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.Controls.Add(this.lblRate, 0, 0);
            this.tblMain.Controls.Add(this.numExchangeRate, 1, 0);
            this.tblMain.Controls.Add(this.lblHelp, 1, 1);
            this.tblMain.Controls.Add(this.lblUsdPreview, 0, 2);
            this.tblMain.Controls.Add(this.lblLbpPreview, 0, 3);
            this.tblMain.Controls.Add(this.lblPrecision, 0, 4);
            this.tblMain.Controls.Add(this.pnlActions, 0, 5);
            this.tblMain.Controls.Add(this.lblSaved, 1, 6);
            this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMain.Location = new System.Drawing.Point(28, 28);
            this.tblMain.Name = "tblMain";
            this.tblMain.RowCount = 7;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.Size = new System.Drawing.Size(864, 334);
            this.tblMain.TabIndex = 0;
            this.tblMain.SetColumnSpan(this.lblHelp, 1);
            this.tblMain.SetColumnSpan(this.lblUsdPreview, 2);
            this.tblMain.SetColumnSpan(this.lblLbpPreview, 2);
            this.tblMain.SetColumnSpan(this.lblPrecision, 2);
            this.tblMain.SetColumnSpan(this.pnlActions, 2);
            // 
            // lblRate
            // 
            this.lblRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRate.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.lblRate.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblRate.Location = new System.Drawing.Point(3, 0);
            this.lblRate.Name = "lblRate";
            this.lblRate.Size = new System.Drawing.Size(224, 58);
            this.lblRate.TabIndex = 0;
            this.lblRate.Text = "1 USD equals (LBP)";
            this.lblRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numExchangeRate
            // 
            this.numExchangeRate.Dock = System.Windows.Forms.DockStyle.Left;
            this.numExchangeRate.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.numExchangeRate.Location = new System.Drawing.Point(233, 11);
            this.numExchangeRate.Margin = new System.Windows.Forms.Padding(3, 11, 3, 3);
            this.numExchangeRate.Maximum = new decimal(new int[] {10000000, 0, 0, 0});
            this.numExchangeRate.Minimum = new decimal(new int[] {1, 0, 0, 0});
            this.numExchangeRate.Name = "numExchangeRate";
            this.numExchangeRate.Size = new System.Drawing.Size(260, 32);
            this.numExchangeRate.TabIndex = 1;
            this.numExchangeRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numExchangeRate.ThousandsSeparator = true;
            this.numExchangeRate.Value = new decimal(new int[] {89500, 0, 0, 0});
            this.numExchangeRate.ValueChanged += new System.EventHandler(this.NumExchangeRate_ValueChanged);
            // 
            // lblHelp
            // 
            this.lblHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHelp.Font = new System.Drawing.Font("Century Gothic", 8.5F);
            this.lblHelp.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblHelp.Location = new System.Drawing.Point(233, 58);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(628, 44);
            this.lblHelp.TabIndex = 2;
            this.lblHelp.Text = "This rate is managed by Admin and is no longer editable from Cashier.";
            this.lblHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUsdPreview
            // 
            this.lblUsdPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUsdPreview.Font = new System.Drawing.Font("Century Gothic", 13F, System.Drawing.FontStyle.Bold);
            this.lblUsdPreview.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblUsdPreview.Location = new System.Drawing.Point(3, 102);
            this.lblUsdPreview.Name = "lblUsdPreview";
            this.lblUsdPreview.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblUsdPreview.Size = new System.Drawing.Size(858, 48);
            this.lblUsdPreview.TabIndex = 3;
            this.lblUsdPreview.Text = "$1.00 = 89,500 LBP";
            this.lblUsdPreview.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLbpPreview
            // 
            this.lblLbpPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLbpPreview.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold);
            this.lblLbpPreview.ForeColor = System.Drawing.Color.FromArgb(16, 185, 129);
            this.lblLbpPreview.Location = new System.Drawing.Point(3, 150);
            this.lblLbpPreview.Name = "lblLbpPreview";
            this.lblLbpPreview.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblLbpPreview.Size = new System.Drawing.Size(858, 48);
            this.lblLbpPreview.TabIndex = 4;
            this.lblLbpPreview.Text = "100,000 LBP = $1.12";
            this.lblLbpPreview.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPrecision
            // 
            this.lblPrecision.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPrecision.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.lblPrecision.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblPrecision.Location = new System.Drawing.Point(3, 198);
            this.lblPrecision.Name = "lblPrecision";
            this.lblPrecision.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblPrecision.Size = new System.Drawing.Size(858, 58);
            this.lblPrecision.TabIndex = 5;
            this.lblPrecision.Text = "Service administration keeps up to 3 USD decimals for accurate recharge pricing. Cashier and receipts continue displaying 2 decimals.";
            this.lblPrecision.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlActions
            // 
            this.pnlActions.Controls.Add(this.btnSave);
            this.pnlActions.Controls.Add(this.btnRefresh);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlActions.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.pnlActions.Location = new System.Drawing.Point(3, 259);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Padding = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.pnlActions.Size = new System.Drawing.Size(858, 58);
            this.pnlActions.TabIndex = 6;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(11, 11);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 42);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save Rate";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(14, 165, 233);
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(167, 11);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(125, 42);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // lblSaved
            // 
            this.lblSaved.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSaved.Font = new System.Drawing.Font("Century Gothic", 8.5F);
            this.lblSaved.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblSaved.Location = new System.Drawing.Point(233, 320);
            this.lblSaved.Name = "lblSaved";
            this.lblSaved.Size = new System.Drawing.Size(628, 14);
            this.lblSaved.TabIndex = 7;
            this.lblSaved.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ExchangeRateSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.ClientSize = new System.Drawing.Size(920, 520);
            this.Controls.Add(this.pnlCard);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(720, 480);
            this.Name = "ExchangeRateSettingsForm";
            this.Text = "Exchange Rate";
            this.Load += new System.EventHandler(this.ExchangeRateSettingsForm_Load);
            this.pnlCard.ResumeLayout(false);
            this.tblMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numExchangeRate)).EndInit();
            this.pnlActions.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion
    }
}
