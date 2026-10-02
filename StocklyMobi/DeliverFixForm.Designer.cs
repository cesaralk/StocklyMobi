namespace StocklyMobi
{
    partial class DeliverFixForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.TableLayoutPanel tblMain;
        private System.Windows.Forms.Label lblFixId;
        private System.Windows.Forms.FlowLayoutPanel flowSearch;
        private System.Windows.Forms.TextBox _fixCode;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label _normalizedCode;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.Label _customer;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label _phone;
        private System.Windows.Forms.Label lblProblem;
        private System.Windows.Forms.Label _problem;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label _status;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label _price;
        private System.Windows.Forms.FlowLayoutPanel flowActions;
        private System.Windows.Forms.Button btnAddDelivery;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlCard = new System.Windows.Forms.Panel();
            this.tblMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblFixId = new System.Windows.Forms.Label();
            this.flowSearch = new System.Windows.Forms.FlowLayoutPanel();
            this._fixCode = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this._normalizedCode = new System.Windows.Forms.Label();
            this.lblCustomer = new System.Windows.Forms.Label();
            this._customer = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this._phone = new System.Windows.Forms.Label();
            this.lblProblem = new System.Windows.Forms.Label();
            this._problem = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this._status = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this._price = new System.Windows.Forms.Label();
            this.flowActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddDelivery = new System.Windows.Forms.Button();
            this.pnlCard.SuspendLayout();
            this.tblMain.SuspendLayout();
            this.flowSearch.SuspendLayout();
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
            this.lblHeader.Size = new System.Drawing.Size(720, 72);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Deliver Fixed Phone";
            // 
            // pnlCard
            // 
            this.pnlCard.BackColor = System.Drawing.Color.White;
            this.pnlCard.Controls.Add(this.tblMain);
            this.pnlCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCard.Location = new System.Drawing.Point(0, 72);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Padding = new System.Windows.Forms.Padding(24);
            this.pnlCard.Size = new System.Drawing.Size(720, 518);
            this.pnlCard.TabIndex = 0;
            // 
            // tblMain
            // 
            this.tblMain.ColumnCount = 2;
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.Controls.Add(this.lblFixId, 0, 0);
            this.tblMain.Controls.Add(this.flowSearch, 1, 0);
            this.tblMain.Controls.Add(this.lblCustomer, 0, 1);
            this.tblMain.Controls.Add(this._customer, 1, 1);
            this.tblMain.Controls.Add(this.lblPhone, 0, 2);
            this.tblMain.Controls.Add(this._phone, 1, 2);
            this.tblMain.Controls.Add(this.lblProblem, 0, 3);
            this.tblMain.Controls.Add(this._problem, 1, 3);
            this.tblMain.Controls.Add(this.lblStatus, 0, 4);
            this.tblMain.Controls.Add(this._status, 1, 4);
            this.tblMain.Controls.Add(this.lblPrice, 0, 5);
            this.tblMain.Controls.Add(this._price, 1, 5);
            this.tblMain.Controls.Add(this.flowActions, 0, 7);
            this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMain.Location = new System.Drawing.Point(24, 24);
            this.tblMain.Name = "tblMain";
            this.tblMain.RowCount = 8;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tblMain.Size = new System.Drawing.Size(672, 470);
            this.tblMain.TabIndex = 0;
            // 
            // lblFixId
            // 
            this.lblFixId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFixId.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblFixId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblFixId.Location = new System.Drawing.Point(3, 0);
            this.lblFixId.Name = "lblFixId";
            this.lblFixId.Size = new System.Drawing.Size(144, 62);
            this.lblFixId.TabIndex = 0;
            this.lblFixId.Text = "Fix number / ID";
            this.lblFixId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flowSearch
            // 
            this.flowSearch.Controls.Add(this._fixCode);
            this.flowSearch.Controls.Add(this.btnSearch);
            this.flowSearch.Controls.Add(this._normalizedCode);
            this.flowSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowSearch.Location = new System.Drawing.Point(153, 3);
            this.flowSearch.Name = "flowSearch";
            this.flowSearch.Size = new System.Drawing.Size(516, 56);
            this.flowSearch.TabIndex = 1;
            // 
            // _fixCode
            // 
            this._fixCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this._fixCode.Font = new System.Drawing.Font("Century Gothic", 10F);
            this._fixCode.Location = new System.Drawing.Point(3, 3);
            this._fixCode.Name = "_fixCode";
            this._fixCode.Size = new System.Drawing.Size(230, 24);
            this._fixCode.TabIndex = 0;
            this._fixCode.TextChanged += new System.EventHandler(this.FixCode_TextChanged);
            this._fixCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FixCode_KeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(239, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(105, 38);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // _normalizedCode
            // 
            this._normalizedCode.AutoSize = true;
            this._normalizedCode.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this._normalizedCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._normalizedCode.Location = new System.Drawing.Point(355, 12);
            this._normalizedCode.Margin = new System.Windows.Forms.Padding(8, 12, 0, 0);
            this._normalizedCode.Name = "_normalizedCode";
            this._normalizedCode.Size = new System.Drawing.Size(49, 16);
            this._normalizedCode.TabIndex = 2;
            this._normalizedCode.Text = "FIX-26-";
            // 
            // lblCustomer
            // 
            this.lblCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCustomer.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblCustomer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblCustomer.Location = new System.Drawing.Point(3, 62);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(144, 48);
            this.lblCustomer.TabIndex = 2;
            this.lblCustomer.Text = "Customer";
            this.lblCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _customer
            // 
            this._customer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._customer.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this._customer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._customer.Location = new System.Drawing.Point(153, 62);
            this._customer.Name = "_customer";
            this._customer.Size = new System.Drawing.Size(516, 48);
            this._customer.TabIndex = 3;
            this._customer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPhone
            // 
            this.lblPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPhone.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblPhone.Location = new System.Drawing.Point(3, 110);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(144, 48);
            this.lblPhone.TabIndex = 4;
            this.lblPhone.Text = "Phone model";
            this.lblPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _phone
            // 
            this._phone.Dock = System.Windows.Forms.DockStyle.Fill;
            this._phone.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this._phone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._phone.Location = new System.Drawing.Point(153, 110);
            this._phone.Name = "_phone";
            this._phone.Size = new System.Drawing.Size(516, 48);
            this._phone.TabIndex = 5;
            this._phone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProblem
            // 
            this.lblProblem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProblem.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblProblem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblProblem.Location = new System.Drawing.Point(3, 158);
            this.lblProblem.Name = "lblProblem";
            this.lblProblem.Size = new System.Drawing.Size(144, 72);
            this.lblProblem.TabIndex = 6;
            this.lblProblem.Text = "Problem";
            this.lblProblem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _problem
            // 
            this._problem.Dock = System.Windows.Forms.DockStyle.Fill;
            this._problem.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this._problem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._problem.Location = new System.Drawing.Point(153, 158);
            this._problem.Name = "_problem";
            this._problem.Size = new System.Drawing.Size(516, 72);
            this._problem.TabIndex = 7;
            this._problem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblStatus.Location = new System.Drawing.Point(3, 230);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(144, 48);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "Status";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _status
            // 
            this._status.Dock = System.Windows.Forms.DockStyle.Fill;
            this._status.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this._status.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._status.Location = new System.Drawing.Point(153, 230);
            this._status.Name = "_status";
            this._status.Size = new System.Drawing.Size(516, 48);
            this._status.TabIndex = 9;
            this._status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPrice
            // 
            this.lblPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPrice.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblPrice.Location = new System.Drawing.Point(3, 278);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(144, 48);
            this.lblPrice.TabIndex = 10;
            this.lblPrice.Text = "Final price";
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _price
            // 
            this._price.Dock = System.Windows.Forms.DockStyle.Fill;
            this._price.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this._price.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._price.Location = new System.Drawing.Point(153, 278);
            this._price.Name = "_price";
            this._price.Size = new System.Drawing.Size(516, 48);
            this._price.TabIndex = 11;
            this._price.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flowActions
            // 
            this.tblMain.SetColumnSpan(this.flowActions, 2);
            this.flowActions.Controls.Add(this.btnAddDelivery);
            this.flowActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowActions.Location = new System.Drawing.Point(3, 401);
            this.flowActions.Name = "flowActions";
            this.flowActions.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.flowActions.Size = new System.Drawing.Size(666, 66);
            this.flowActions.TabIndex = 12;
            // 
            // btnAddDelivery
            // 
            this.btnAddDelivery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnAddDelivery.FlatAppearance.BorderSize = 0;
            this.btnAddDelivery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddDelivery.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddDelivery.ForeColor = System.Drawing.Color.White;
            this.btnAddDelivery.Location = new System.Drawing.Point(3, 15);
            this.btnAddDelivery.Name = "btnAddDelivery";
            this.btnAddDelivery.Size = new System.Drawing.Size(285, 48);
            this.btnAddDelivery.TabIndex = 0;
            this.btnAddDelivery.Text = "Add Fix Delivery to Transaction";
            this.btnAddDelivery.UseVisualStyleBackColor = false;
            this.btnAddDelivery.Click += new System.EventHandler(this.Add_Click);
            // 
            // DeliverFixForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(720, 590);
            this.Controls.Add(this.pnlCard);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Century Gothic", 9.5F);
            this.MinimumSize = new System.Drawing.Size(560, 470);
            this.Name = "DeliverFixForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Deliver Fixed Phone";
            this.Load += new System.EventHandler(this.DeliverFixForm_Load);
            this.pnlCard.ResumeLayout(false);
            this.tblMain.ResumeLayout(false);
            this.flowSearch.ResumeLayout(false);
            this.flowSearch.PerformLayout();
            this.flowActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
