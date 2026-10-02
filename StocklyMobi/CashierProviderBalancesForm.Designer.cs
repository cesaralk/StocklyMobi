namespace StocklyMobi
{
    partial class CashierProviderBalancesForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.DataGridView dgvBalances;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblUpdated;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle headerStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle rowStyle = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.dgvBalances = new System.Windows.Forms.DataGridView();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lblUpdated = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBalances)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // lblHeader
            this.lblHeader.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Height = 72;
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Padding = new System.Windows.Forms.Padding(18, 10, 10, 4);
            this.lblHeader.Text = "Provider Balances\r\nRead-only balances for cashier services.";
            // pnlBody
            this.pnlBody.BackColor = System.Drawing.Color.White;
            this.pnlBody.Controls.Add(this.dgvBalances);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(16);
            // dgvBalances
            this.dgvBalances.AllowUserToAddRows = false;
            this.dgvBalances.AllowUserToDeleteRows = false;
            this.dgvBalances.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBalances.BackgroundColor = System.Drawing.Color.White;
            this.dgvBalances.BorderStyle = System.Windows.Forms.BorderStyle.None;
            headerStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            headerStyle.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            headerStyle.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            headerStyle.ForeColor = System.Drawing.Color.White;
            headerStyle.SelectionBackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            headerStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dgvBalances.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvBalances.ColumnHeadersHeight = 38;
            this.dgvBalances.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBalances.EnableHeadersVisualStyles = false;
            this.dgvBalances.MultiSelect = false;
            this.dgvBalances.Name = "dgvBalances";
            this.dgvBalances.ReadOnly = true;
            this.dgvBalances.RowHeadersVisible = false;
            rowStyle.Font = new System.Drawing.Font("Century Gothic", 9.5F);
            rowStyle.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            rowStyle.SelectionForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.dgvBalances.RowsDefaultCellStyle = rowStyle;
            this.dgvBalances.RowTemplate.Height = 34;
            this.dgvBalances.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // pnlFooter
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.pnlFooter.Controls.Add(this.lblUpdated);
            this.pnlFooter.Controls.Add(this.btnRefresh);
            this.pnlFooter.Controls.Add(this.btnClose);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Height = 68;
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            // lblUpdated
            this.lblUpdated.AutoEllipsis = true;
            this.lblUpdated.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUpdated.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblUpdated.Name = "lblUpdated";
            this.lblUpdated.Text = "Updated:";
            this.lblUpdated.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // btnRefresh
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(14, 165, 233);
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 44);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // btnClose
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 44);
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // form
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.ClientSize = new System.Drawing.Size(760, 500);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Century Gothic", 9.5F);
            this.MinimumSize = new System.Drawing.Size(620, 400);
            this.Name = "CashierProviderBalancesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Provider Balances";
            this.Load += new System.EventHandler(this.CashierProviderBalancesForm_Load);
            this.pnlBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBalances)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
