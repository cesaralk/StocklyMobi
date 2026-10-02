namespace StocklyMobi
{
    partial class ValidityRetakeDialog
    {
        private System.ComponentModel.IContainer components=null;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.TableLayoutPanel tblMain;
        private System.Windows.Forms.Label lblService;
        private System.Windows.Forms.Label lblServiceValue;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblPriceValue;
        private System.Windows.Forms.Label lblRetake;
        private System.Windows.Forms.NumericUpDown _retake;
        private System.Windows.Forms.FlowLayoutPanel flowActions;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        protected override void Dispose(bool disposing){if(disposing&&components!=null)components.Dispose();
        base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlCard = new System.Windows.Forms.Panel();
            this.tblMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblService = new System.Windows.Forms.Label();
            this.lblServiceValue = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblPriceValue = new System.Windows.Forms.Label();
            this.lblRetake = new System.Windows.Forms.Label();
            this._retake = new System.Windows.Forms.NumericUpDown();
            this.flowActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlCard.SuspendLayout();
            this.tblMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._retake)).BeginInit();
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
            this.lblHeader.Size = new System.Drawing.Size(540, 72);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Validity Recharge\r\nEnter the Retake $ amount.";
            // 
            // pnlCard
            // 
            this.pnlCard.BackColor = System.Drawing.Color.White;
            this.pnlCard.Controls.Add(this.tblMain);
            this.pnlCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCard.Location = new System.Drawing.Point(0, 72);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Padding = new System.Windows.Forms.Padding(22);
            this.pnlCard.Size = new System.Drawing.Size(540, 288);
            this.pnlCard.TabIndex = 0;
            // 
            // tblMain
            // 
            this.tblMain.ColumnCount = 2;
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 155F));
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.Controls.Add(this.lblService, 0, 0);
            this.tblMain.Controls.Add(this.lblServiceValue, 1, 0);
            this.tblMain.Controls.Add(this.lblPrice, 0, 1);
            this.tblMain.Controls.Add(this.lblPriceValue, 1, 1);
            this.tblMain.Controls.Add(this.lblRetake, 0, 2);
            this.tblMain.Controls.Add(this._retake, 1, 2);
            this.tblMain.Controls.Add(this.flowActions, 0, 3);
            this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMain.Location = new System.Drawing.Point(22, 22);
            this.tblMain.Name = "tblMain";
            this.tblMain.RowCount = 4;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.Size = new System.Drawing.Size(496, 244);
            this.tblMain.TabIndex = 0;
            // 
            // lblService
            // 
            this.lblService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblService.Location = new System.Drawing.Point(3, 0);
            this.lblService.Name = "lblService";
            this.lblService.Size = new System.Drawing.Size(149, 48);
            this.lblService.TabIndex = 0;
            this.lblService.Text = "Service";
            this.lblService.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblServiceValue
            // 
            this.lblServiceValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServiceValue.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.lblServiceValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblServiceValue.Location = new System.Drawing.Point(158, 0);
            this.lblServiceValue.Name = "lblServiceValue";
            this.lblServiceValue.Size = new System.Drawing.Size(335, 48);
            this.lblServiceValue.TabIndex = 1;
            this.lblServiceValue.Text = "Service name";
            // 
            // lblPrice
            // 
            this.lblPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPrice.Location = new System.Drawing.Point(3, 48);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(149, 48);
            this.lblPrice.TabIndex = 2;
            this.lblPrice.Text = "Final price";
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPriceValue
            // 
            this.lblPriceValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPriceValue.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.lblPriceValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblPriceValue.Location = new System.Drawing.Point(158, 48);
            this.lblPriceValue.Name = "lblPriceValue";
            this.lblPriceValue.Size = new System.Drawing.Size(335, 48);
            this.lblPriceValue.TabIndex = 3;
            this.lblPriceValue.Text = "$ 0.00";
            // 
            // lblRetake
            // 
            this.lblRetake.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRetake.Location = new System.Drawing.Point(3, 96);
            this.lblRetake.Name = "lblRetake";
            this.lblRetake.Size = new System.Drawing.Size(149, 58);
            this.lblRetake.TabIndex = 4;
            this.lblRetake.Text = "Retake $";
            this.lblRetake.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _retake
            // 
            this._retake.DecimalPlaces = 2;
            this._retake.Dock = System.Windows.Forms.DockStyle.Fill;
            this._retake.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this._retake.Location = new System.Drawing.Point(158, 99);
            this._retake.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this._retake.Name = "_retake";
            this._retake.Size = new System.Drawing.Size(335, 23);
            this._retake.TabIndex = 5;
            this._retake.ThousandsSeparator = true;
            // 
            // flowActions
            // 
            this.tblMain.SetColumnSpan(this.flowActions, 2);
            this.flowActions.Controls.Add(this.btnAdd);
            this.flowActions.Controls.Add(this.btnCancel);
            this.flowActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowActions.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowActions.Location = new System.Drawing.Point(3, 157);
            this.flowActions.Name = "flowActions";
            this.flowActions.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.flowActions.Size = new System.Drawing.Size(490, 84);
            this.flowActions.TabIndex = 6;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnAdd.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(312, 15);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(175, 42);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add to Transaction";
            this.btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(206, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 42);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // ValidityRetakeDialog
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(540, 360);
            this.Controls.Add(this.pnlCard);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Century Gothic", 9.5F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(460, 300);
            this.Name = "ValidityRetakeDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Validity Recharge";
            this.pnlCard.ResumeLayout(false);
            this.tblMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._retake)).EndInit();
            this.flowActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
