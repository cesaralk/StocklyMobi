namespace StocklyMobi
{
    partial class ServicePickerForm
    {
        private System.ComponentModel.IContainer components=null;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.TableLayoutPanel tblSearch;
        private System.Windows.Forms.Label lblFind;
        private System.Windows.Forms.TextBox _search;
        private System.Windows.Forms.Panel pnlBalances;
        private System.Windows.Forms.Label lblBalancesTitle;
        private System.Windows.Forms.Label lblBalancesUpdated;
        private System.Windows.Forms.FlowLayoutPanel _balancesHost;
        private System.Windows.Forms.Label lblBalanceTemplate;
        private System.Windows.Forms.Button btnRefreshBalances;
        private System.Windows.Forms.FlowLayoutPanel _buttonsHost;
        private System.Windows.Forms.Button _serviceButtonTemplate;
        protected override void Dispose(bool disposing){if(disposing&&components!=null)components.Dispose();
        base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.tblSearch = new System.Windows.Forms.TableLayoutPanel();
            this.lblFind = new System.Windows.Forms.Label();
            this._search = new System.Windows.Forms.TextBox();
            this.pnlBalances = new System.Windows.Forms.Panel();
            this._balancesHost = new System.Windows.Forms.FlowLayoutPanel();
            this.lblBalanceTemplate = new System.Windows.Forms.Label();
            this.btnRefreshBalances = new System.Windows.Forms.Button();
            this.lblBalancesUpdated = new System.Windows.Forms.Label();
            this.lblBalancesTitle = new System.Windows.Forms.Label();
            this._buttonsHost = new System.Windows.Forms.FlowLayoutPanel();
            this._serviceButtonTemplate = new System.Windows.Forms.Button();
            this.pnlSearch.SuspendLayout();
            this.tblSearch.SuspendLayout();
            this.pnlBalances.SuspendLayout();
            this._balancesHost.SuspendLayout();
            this._buttonsHost.SuspendLayout();
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
            this.lblHeader.Size = new System.Drawing.Size(1040, 72);
            this.lblHeader.TabIndex = 3;
            this.lblHeader.Text = "Mobile Recharge";
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.White;
            this.pnlSearch.Controls.Add(this.tblSearch);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(0, 164);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(16, 14, 16, 10);
            this.pnlSearch.Size = new System.Drawing.Size(1040, 66);
            this.pnlSearch.TabIndex = 1;
            // 
            // tblSearch
            // 
            this.tblSearch.ColumnCount = 2;
            this.tblSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblSearch.Controls.Add(this.lblFind, 0, 0);
            this.tblSearch.Controls.Add(this._search, 1, 0);
            this.tblSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblSearch.Location = new System.Drawing.Point(16, 14);
            this.tblSearch.Name = "tblSearch";
            this.tblSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tblSearch.Size = new System.Drawing.Size(1008, 42);
            this.tblSearch.TabIndex = 0;
            // 
            // lblFind
            // 
            this.lblFind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFind.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblFind.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblFind.Location = new System.Drawing.Point(3, 0);
            this.lblFind.Name = "lblFind";
            this.lblFind.Size = new System.Drawing.Size(114, 42);
            this.lblFind.TabIndex = 0;
            this.lblFind.Text = "Find service";
            this.lblFind.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _search
            // 
            this._search.Dock = System.Windows.Forms.DockStyle.Fill;
            this._search.Font = new System.Drawing.Font("Century Gothic", 10F);
            this._search.Location = new System.Drawing.Point(123, 3);
            this._search.Name = "_search";
            this._search.Size = new System.Drawing.Size(882, 24);
            this._search.TabIndex = 1;
            this._search.TextChanged += new System.EventHandler(this.Search_TextChanged);
            // 
            // pnlBalances
            // 
            this.pnlBalances.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlBalances.Controls.Add(this._balancesHost);
            this.pnlBalances.Controls.Add(this.btnRefreshBalances);
            this.pnlBalances.Controls.Add(this.lblBalancesUpdated);
            this.pnlBalances.Controls.Add(this.lblBalancesTitle);
            this.pnlBalances.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBalances.Location = new System.Drawing.Point(0, 72);
            this.pnlBalances.Name = "pnlBalances";
            this.pnlBalances.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.pnlBalances.Size = new System.Drawing.Size(1040, 92);
            this.pnlBalances.TabIndex = 2;
            // 
            // _balancesHost
            // 
            this._balancesHost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._balancesHost.AutoScroll = true;
            this._balancesHost.Controls.Add(this.lblBalanceTemplate);
            this._balancesHost.Location = new System.Drawing.Point(16, 31);
            this._balancesHost.Name = "_balancesHost";
            this._balancesHost.Size = new System.Drawing.Size(1740, 52);
            this._balancesHost.TabIndex = 0;
            this._balancesHost.WrapContents = false;
            // 
            // lblBalanceTemplate
            // 
            this.lblBalanceTemplate.BackColor = System.Drawing.Color.White;
            this.lblBalanceTemplate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBalanceTemplate.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblBalanceTemplate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblBalanceTemplate.Location = new System.Drawing.Point(4, 4);
            this.lblBalanceTemplate.Margin = new System.Windows.Forms.Padding(4);
            this.lblBalanceTemplate.Name = "lblBalanceTemplate";
            this.lblBalanceTemplate.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.lblBalanceTemplate.Size = new System.Drawing.Size(185, 40);
            this.lblBalanceTemplate.TabIndex = 0;
            this.lblBalanceTemplate.Text = "WishMoney: $0.00";
            this.lblBalanceTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRefreshBalances
            // 
            this.btnRefreshBalances.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshBalances.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(165)))), ((int)(((byte)(233)))));
            this.btnRefreshBalances.FlatAppearance.BorderSize = 0;
            this.btnRefreshBalances.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshBalances.ForeColor = System.Drawing.Color.White;
            this.btnRefreshBalances.Location = new System.Drawing.Point(1765, 39);
            this.btnRefreshBalances.Name = "btnRefreshBalances";
            this.btnRefreshBalances.Size = new System.Drawing.Size(98, 36);
            this.btnRefreshBalances.TabIndex = 1;
            this.btnRefreshBalances.Text = "Refresh";
            this.btnRefreshBalances.UseVisualStyleBackColor = false;
            this.btnRefreshBalances.Click += new System.EventHandler(this.BtnRefreshBalances_Click);
            // 
            // lblBalancesUpdated
            // 
            this.lblBalancesUpdated.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBalancesUpdated.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblBalancesUpdated.Location = new System.Drawing.Point(1400, 8);
            this.lblBalancesUpdated.Name = "lblBalancesUpdated";
            this.lblBalancesUpdated.Size = new System.Drawing.Size(280, 20);
            this.lblBalancesUpdated.TabIndex = 2;
            this.lblBalancesUpdated.Text = "Balances update after settlement";
            this.lblBalancesUpdated.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBalancesTitle
            // 
            this.lblBalancesTitle.AutoSize = true;
            this.lblBalancesTitle.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblBalancesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblBalancesTitle.Location = new System.Drawing.Point(16, 7);
            this.lblBalancesTitle.Name = "lblBalancesTitle";
            this.lblBalancesTitle.Size = new System.Drawing.Size(182, 16);
            this.lblBalancesTitle.TabIndex = 3;
            this.lblBalancesTitle.Text = "CURRENT PROVIDER BALANCES";
            // 
            // _buttonsHost
            // 
            this._buttonsHost.AutoScroll = true;
            this._buttonsHost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._buttonsHost.Controls.Add(this._serviceButtonTemplate);
            this._buttonsHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this._buttonsHost.Location = new System.Drawing.Point(0, 230);
            this._buttonsHost.Name = "_buttonsHost";
            this._buttonsHost.Padding = new System.Windows.Forms.Padding(14);
            this._buttonsHost.Size = new System.Drawing.Size(1040, 490);
            this._buttonsHost.TabIndex = 0;
            // 
            // _serviceButtonTemplate
            // 
            this._serviceButtonTemplate.BackColor = System.Drawing.Color.White;
            this._serviceButtonTemplate.Cursor = System.Windows.Forms.Cursors.Hand;
            this._serviceButtonTemplate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._serviceButtonTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._serviceButtonTemplate.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this._serviceButtonTemplate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._serviceButtonTemplate.Location = new System.Drawing.Point(22, 22);
            this._serviceButtonTemplate.Margin = new System.Windows.Forms.Padding(8);
            this._serviceButtonTemplate.Name = "_serviceButtonTemplate";
            this._serviceButtonTemplate.Padding = new System.Windows.Forms.Padding(13);
            this._serviceButtonTemplate.Size = new System.Drawing.Size(285, 120);
            this._serviceButtonTemplate.TabIndex = 0;
            this._serviceButtonTemplate.Text = "SERVICE BUTTON TEMPLATE\r\nPrice: $0.00\r\nEdit this button style in Designer";
            this._serviceButtonTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._serviceButtonTemplate.UseVisualStyleBackColor = false;
            // 
            // ServicePickerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1040, 720);
            this.Controls.Add(this._buttonsHost);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.pnlBalances);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Century Gothic", 9.5F);
            this.MinimumSize = new System.Drawing.Size(720, 500);
            this.Name = "ServicePickerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Services";
            this.Load += new System.EventHandler(this.ServicePickerForm_Load);
            this.Resize += new System.EventHandler(this.ServicePickerForm_Resize);
            this.pnlSearch.ResumeLayout(false);
            this.tblSearch.ResumeLayout(false);
            this.tblSearch.PerformLayout();
            this.pnlBalances.ResumeLayout(false);
            this.pnlBalances.PerformLayout();
            this._balancesHost.ResumeLayout(false);
            this._buttonsHost.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
