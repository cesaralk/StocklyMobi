namespace StocklyMobi
{
    partial class NewProductQuickForm
    {
        private System.ComponentModel.IContainer components=null;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.TableLayoutPanel tblMain;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.TextBox _barcode;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox _description;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.TableLayoutPanel tblBrand;
        private System.Windows.Forms.ComboBox _brand;
        private System.Windows.Forms.Button btnAddBrand;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.TableLayoutPanel tblCategory;
        private System.Windows.Forms.ComboBox _category;
        private System.Windows.Forms.Button btnAddCategory;
        private System.Windows.Forms.Label lblCost;
        private System.Windows.Forms.NumericUpDown _cost;
        private System.Windows.Forms.Label lblMarkup;
        private System.Windows.Forms.NumericUpDown _markup;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.NumericUpDown _price;
        private System.Windows.Forms.Label lblReorder;
        private System.Windows.Forms.NumericUpDown _reorder;
        private System.Windows.Forms.FlowLayoutPanel flowActions;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        protected override void Dispose(bool disposing){if(disposing&&components!=null)components.Dispose();
        base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlCard = new System.Windows.Forms.Panel();
            this.tblMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblBarcode = new System.Windows.Forms.Label();
            this._barcode = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this._description = new System.Windows.Forms.TextBox();
            this.lblBrand = new System.Windows.Forms.Label();
            this.tblBrand = new System.Windows.Forms.TableLayoutPanel();
            this._brand = new System.Windows.Forms.ComboBox();
            this.btnAddBrand = new System.Windows.Forms.Button();
            this.lblCategory = new System.Windows.Forms.Label();
            this.tblCategory = new System.Windows.Forms.TableLayoutPanel();
            this._category = new System.Windows.Forms.ComboBox();
            this.btnAddCategory = new System.Windows.Forms.Button();
            this.lblCost = new System.Windows.Forms.Label();
            this._cost = new System.Windows.Forms.NumericUpDown();
            this.lblMarkup = new System.Windows.Forms.Label();
            this._markup = new System.Windows.Forms.NumericUpDown();
            this.lblPrice = new System.Windows.Forms.Label();
            this._price = new System.Windows.Forms.NumericUpDown();
            this.lblReorder = new System.Windows.Forms.Label();
            this._reorder = new System.Windows.Forms.NumericUpDown();
            this.flowActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlCard.SuspendLayout();
            this.tblMain.SuspendLayout();
            this.tblBrand.SuspendLayout();
            this.tblCategory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._cost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._markup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._price)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._reorder)).BeginInit();
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
            this.lblHeader.Text = "Create Product";
            // 
            // pnlCard
            // 
            this.pnlCard.BackColor = System.Drawing.Color.White;
            this.pnlCard.Controls.Add(this.tblMain);
            this.pnlCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCard.Location = new System.Drawing.Point(0, 72);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Padding = new System.Windows.Forms.Padding(24);
            this.pnlCard.Size = new System.Drawing.Size(720, 538);
            this.pnlCard.TabIndex = 0;
            // 
            // tblMain
            // 
            this.tblMain.ColumnCount = 2;
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.Controls.Add(this.lblBarcode, 0, 0);
            this.tblMain.Controls.Add(this._barcode, 1, 0);
            this.tblMain.Controls.Add(this.lblDescription, 0, 1);
            this.tblMain.Controls.Add(this._description, 1, 1);
            this.tblMain.Controls.Add(this.lblBrand, 0, 2);
            this.tblMain.Controls.Add(this.tblBrand, 1, 2);
            this.tblMain.Controls.Add(this.lblCategory, 0, 3);
            this.tblMain.Controls.Add(this.tblCategory, 1, 3);
            this.tblMain.Controls.Add(this.lblCost, 0, 4);
            this.tblMain.Controls.Add(this._cost, 1, 4);
            this.tblMain.Controls.Add(this.lblMarkup, 0, 5);
            this.tblMain.Controls.Add(this._markup, 1, 5);
            this.tblMain.Controls.Add(this.lblPrice, 0, 6);
            this.tblMain.Controls.Add(this._price, 1, 6);
            this.tblMain.Controls.Add(this.lblReorder, 0, 7);
            this.tblMain.Controls.Add(this._reorder, 1, 7);
            this.tblMain.Controls.Add(this.flowActions, 0, 8);
            this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMain.Location = new System.Drawing.Point(24, 24);
            this.tblMain.Name = "tblMain";
            this.tblMain.RowCount = 9;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.Size = new System.Drawing.Size(672, 490);
            this.tblMain.TabIndex = 0;
            // 
            // lblBarcode
            // 
            this.lblBarcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBarcode.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblBarcode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblBarcode.Location = new System.Drawing.Point(3, 0);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new System.Drawing.Size(164, 48);
            this.lblBarcode.TabIndex = 0;
            this.lblBarcode.Text = "Barcode";
            this.lblBarcode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _barcode
            // 
            this._barcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this._barcode.Font = new System.Drawing.Font("Century Gothic", 10F);
            this._barcode.Location = new System.Drawing.Point(173, 3);
            this._barcode.Name = "_barcode";
            this._barcode.Size = new System.Drawing.Size(496, 24);
            this._barcode.TabIndex = 1;
            // 
            // lblDescription
            // 
            this.lblDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDescription.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblDescription.Location = new System.Drawing.Point(3, 48);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(164, 48);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Description";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _description
            // 
            this._description.Dock = System.Windows.Forms.DockStyle.Fill;
            this._description.Font = new System.Drawing.Font("Century Gothic", 10F);
            this._description.Location = new System.Drawing.Point(173, 51);
            this._description.Name = "_description";
            this._description.Size = new System.Drawing.Size(496, 24);
            this._description.TabIndex = 3;
            // 
            // lblBrand
            // 
            this.lblBrand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBrand.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblBrand.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblBrand.Location = new System.Drawing.Point(3, 96);
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Size = new System.Drawing.Size(164, 48);
            this.lblBrand.TabIndex = 4;
            this.lblBrand.Text = "Brand";
            this.lblBrand.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tblBrand
            // 
            this.tblBrand.ColumnCount = 2;
            this.tblBrand.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblBrand.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tblBrand.Controls.Add(this._brand, 0, 0);
            this.tblBrand.Controls.Add(this.btnAddBrand, 1, 0);
            this.tblBrand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblBrand.Location = new System.Drawing.Point(173, 99);
            this.tblBrand.Name = "tblBrand";
            this.tblBrand.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblBrand.Size = new System.Drawing.Size(496, 42);
            this.tblBrand.TabIndex = 5;
            // 
            // _brand
            // 
            this._brand.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this._brand.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this._brand.Dock = System.Windows.Forms.DockStyle.Fill;
            this._brand.DropDownHeight = 240;
            this._brand.Font = new System.Drawing.Font("Century Gothic", 10F);
            this._brand.IntegralHeight = false;
            this._brand.Location = new System.Drawing.Point(3, 3);
            this._brand.MaxDropDownItems = 12;
            this._brand.Name = "_brand";
            this._brand.Size = new System.Drawing.Size(444, 25);
            this._brand.TabIndex = 0;
            // 
            // btnAddBrand
            // 
            this.btnAddBrand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnAddBrand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddBrand.FlatAppearance.BorderSize = 0;
            this.btnAddBrand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddBrand.ForeColor = System.Drawing.Color.White;
            this.btnAddBrand.Location = new System.Drawing.Point(453, 3);
            this.btnAddBrand.Name = "btnAddBrand";
            this.btnAddBrand.Size = new System.Drawing.Size(40, 36);
            this.btnAddBrand.TabIndex = 1;
            this.btnAddBrand.Text = "+";
            this.btnAddBrand.UseVisualStyleBackColor = false;
            this.btnAddBrand.Click += new System.EventHandler(this.BtnAddBrand_Click);
            // 
            // lblCategory
            // 
            this.lblCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCategory.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblCategory.Location = new System.Drawing.Point(3, 144);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(164, 48);
            this.lblCategory.TabIndex = 6;
            this.lblCategory.Text = "Category";
            this.lblCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tblCategory
            // 
            this.tblCategory.ColumnCount = 2;
            this.tblCategory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblCategory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tblCategory.Controls.Add(this._category, 0, 0);
            this.tblCategory.Controls.Add(this.btnAddCategory, 1, 0);
            this.tblCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblCategory.Location = new System.Drawing.Point(173, 147);
            this.tblCategory.Name = "tblCategory";
            this.tblCategory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblCategory.Size = new System.Drawing.Size(496, 42);
            this.tblCategory.TabIndex = 7;
            // 
            // _category
            // 
            this._category.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this._category.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this._category.Dock = System.Windows.Forms.DockStyle.Fill;
            this._category.DropDownHeight = 240;
            this._category.Font = new System.Drawing.Font("Century Gothic", 10F);
            this._category.IntegralHeight = false;
            this._category.Location = new System.Drawing.Point(3, 3);
            this._category.MaxDropDownItems = 12;
            this._category.Name = "_category";
            this._category.Size = new System.Drawing.Size(444, 25);
            this._category.TabIndex = 0;
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnAddCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddCategory.FlatAppearance.BorderSize = 0;
            this.btnAddCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCategory.ForeColor = System.Drawing.Color.White;
            this.btnAddCategory.Location = new System.Drawing.Point(453, 3);
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.Size = new System.Drawing.Size(40, 36);
            this.btnAddCategory.TabIndex = 1;
            this.btnAddCategory.Text = "+";
            this.btnAddCategory.UseVisualStyleBackColor = false;
            this.btnAddCategory.Click += new System.EventHandler(this.BtnAddCategory_Click);
            // 
            // lblCost
            // 
            this.lblCost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCost.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblCost.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblCost.Location = new System.Drawing.Point(3, 192);
            this.lblCost.Name = "lblCost";
            this.lblCost.Size = new System.Drawing.Size(164, 48);
            this.lblCost.TabIndex = 8;
            this.lblCost.Text = "Cost price";
            this.lblCost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _cost
            // 
            this._cost.DecimalPlaces = 2;
            this._cost.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cost.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this._cost.Location = new System.Drawing.Point(173, 195);
            this._cost.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this._cost.Name = "_cost";
            this._cost.Size = new System.Drawing.Size(496, 23);
            this._cost.TabIndex = 9;
            this._cost.ThousandsSeparator = true;
            this._cost.ValueChanged += new System.EventHandler(this.CostOrMarkupChanged);
            // 
            // lblMarkup
            // 
            this.lblMarkup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMarkup.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblMarkup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblMarkup.Location = new System.Drawing.Point(3, 240);
            this.lblMarkup.Name = "lblMarkup";
            this.lblMarkup.Size = new System.Drawing.Size(164, 48);
            this.lblMarkup.TabIndex = 10;
            this.lblMarkup.Text = "Markup %";
            this.lblMarkup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _markup
            // 
            this._markup.DecimalPlaces = 2;
            this._markup.Dock = System.Windows.Forms.DockStyle.Fill;
            this._markup.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this._markup.Location = new System.Drawing.Point(173, 243);
            this._markup.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this._markup.Name = "_markup";
            this._markup.Size = new System.Drawing.Size(496, 23);
            this._markup.TabIndex = 11;
            this._markup.ThousandsSeparator = true;
            this._markup.ValueChanged += new System.EventHandler(this.CostOrMarkupChanged);
            // 
            // lblPrice
            // 
            this.lblPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPrice.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblPrice.Location = new System.Drawing.Point(3, 288);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(164, 48);
            this.lblPrice.TabIndex = 12;
            this.lblPrice.Text = "Selling price";
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _price
            // 
            this._price.DecimalPlaces = 2;
            this._price.Dock = System.Windows.Forms.DockStyle.Fill;
            this._price.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this._price.Location = new System.Drawing.Point(173, 291);
            this._price.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this._price.Name = "_price";
            this._price.Size = new System.Drawing.Size(496, 23);
            this._price.TabIndex = 13;
            this._price.ThousandsSeparator = true;
            this._price.ValueChanged += new System.EventHandler(this.PriceChanged);
            // 
            // lblReorder
            // 
            this.lblReorder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReorder.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblReorder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblReorder.Location = new System.Drawing.Point(3, 336);
            this.lblReorder.Name = "lblReorder";
            this.lblReorder.Size = new System.Drawing.Size(164, 48);
            this.lblReorder.TabIndex = 14;
            this.lblReorder.Text = "Reorder level";
            this.lblReorder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _reorder
            // 
            this._reorder.Dock = System.Windows.Forms.DockStyle.Fill;
            this._reorder.Location = new System.Drawing.Point(173, 339);
            this._reorder.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this._reorder.Name = "_reorder";
            this._reorder.Size = new System.Drawing.Size(496, 23);
            this._reorder.TabIndex = 15;
            this._reorder.ThousandsSeparator = true;
            // 
            // flowActions
            // 
            this.tblMain.SetColumnSpan(this.flowActions, 2);
            this.flowActions.Controls.Add(this.btnSave);
            this.flowActions.Controls.Add(this.btnCancel);
            this.flowActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowActions.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowActions.Location = new System.Drawing.Point(3, 387);
            this.flowActions.Name = "flowActions";
            this.flowActions.Padding = new System.Windows.Forms.Padding(0, 14, 0, 0);
            this.flowActions.Size = new System.Drawing.Size(666, 100);
            this.flowActions.TabIndex = 16;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(518, 17);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(145, 42);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save Product";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.Save_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(412, 17);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 42);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // NewProductQuickForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(720, 610);
            this.Controls.Add(this.pnlCard);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Century Gothic", 9.5F);
            this.MinimumSize = new System.Drawing.Size(620, 520);
            this.Name = "NewProductQuickForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Product";
            this.Load += new System.EventHandler(this.NewProductQuickForm_Load);
            this.pnlCard.ResumeLayout(false);
            this.tblMain.ResumeLayout(false);
            this.tblMain.PerformLayout();
            this.tblBrand.ResumeLayout(false);
            this.tblCategory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._cost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._markup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._price)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._reorder)).EndInit();
            this.flowActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
