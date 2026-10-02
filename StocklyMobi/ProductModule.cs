

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;






namespace StocklyMobi
{
    public partial class ProductModule : Form
    {
        SqlConnection cn = new SqlConnection();
        DBConnect dbcon = new DBConnect();
        Product product;
        string stitle = "StocklyMobi POS";
        // Prevent recursive TextChanged updates when syncing Cost/Markup/Price
        private bool _syncPricing = false;
        private Button btnPrintLabel;


        public ProductModule(Product pd)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            product = pd;

            LoadBrand();
            LoadCategory();
            Clear(); // Initialize form

            // Hook up Add Brand button (you need to add this button in designer)
            btnAddBrand.Click += btnAddBrand_Click;

            // Pricing sync (both directions)
            txtMarkup.TextChanged += TxtMarkup_TextChanged;
            txtPrice.TextChanged += TxtPrice_TextChanged;
            txtCostPrice.TextChanged += TxtCostPrice_TextChanged;

            InitializeBarcodeButtons();
            ApplyResponsiveLayout();

        }
        private void TxtMarkup_TextChanged(object sender, EventArgs e)
        {
            if (_syncPricing) return;
            // If user edits markup, calculate selling price
            SyncPriceFromMarkup();
        }

        private void TxtPrice_TextChanged(object sender, EventArgs e)
        {
            if (_syncPricing) return;
            // If user edits price, calculate markup percent
            SyncMarkupFromPrice();
        }

        private void TxtCostPrice_TextChanged(object sender, EventArgs e)
        {
            if (_syncPricing) return;

            // When cost changes, keep whichever field (markup or price) the user already filled.
            // Prefer markup -> price, otherwise price -> markup.
            decimal cost = ParseMoney(txtCostPrice);
            if (cost <= 0m)
                return;

            if (!string.IsNullOrWhiteSpace(txtMarkup.Text))
                SyncPriceFromMarkup();
            else if (!string.IsNullOrWhiteSpace(txtPrice.Text))
                SyncMarkupFromPrice();
        }

        private void SyncPriceFromMarkup()
        {
            try
            {
                _syncPricing = true;

                decimal cost = ParseMoney(txtCostPrice);
                decimal markup = ParseMoney(txtMarkup);

                if (cost <= 0m)
                {
                    txtPrice.Text = "0.00";
                    return;
                }

                decimal selling = cost + (cost * markup / 100m);
                txtPrice.Text = selling.ToString("0.00");
            }
            finally
            {
                _syncPricing = false;
            }
        }

        private void SyncMarkupFromPrice()
        {
            try
            {
                _syncPricing = true;

                decimal cost = ParseMoney(txtCostPrice);
                decimal price = ParseMoney(txtPrice);

                if (cost <= 0m)
                {
                    txtMarkup.Text = "0";
                    return;
                }

                decimal markup = ((price - cost) / cost) * 100m;
                txtMarkup.Text = markup.ToString("0.00");
            }
            finally
            {
                _syncPricing = false;
            }
        }

    
        public void LoadBrand()
        {
            DataTable dt = dbcon.getTable("SELECT * FROM tbBrand");
            cboBrand.DataSource = null;
            cboBrand.DataSource = dt;
            cboBrand.DisplayMember = "brand";
            cboBrand.ValueMember = "id";

            cboBrand.DropDownStyle = ComboBoxStyle.DropDown;
            cboBrand.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboBrand.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        // Load categories into combobox
        public void LoadCategory()
        {
            DataTable dt = dbcon.getTable("SELECT * FROM tbCategory");
            cboCategory.DataSource = null;
            cboCategory.DataSource = dt;
            cboCategory.DisplayMember = "category";
            cboCategory.ValueMember = "id";

            cboCategory.DropDownStyle = ComboBoxStyle.DropDown;
            cboCategory.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboCategory.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        // Clear form and reset for new entry
        public void Clear()
        {
            txtPcode.Text = "AUTO";      // Show AUTO, disabled
            txtPcode.Enabled = false;

            txtBarcode.Clear();
            txtPdesc.Clear();
            txtCostPrice.Clear();
            txtMarkup.Clear();
            txtPrice.Clear();
            cboBrand.SelectedIndex = 0;
            cboCategory.SelectedIndex = 0;
            UDReOrder.Value = 1;

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;

            txtPdesc.Focus();
        }

        // Close form
        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


private void InitializeBarcodeButtons()
{
    btnPrintLabel = new Button();
    btnPrintLabel.Name = "btnPrintLabel";
    btnPrintLabel.Text = "Print Label";
    btnPrintLabel.BackColor = Color.WhiteSmoke;
    btnPrintLabel.FlatStyle = FlatStyle.Flat;
    btnPrintLabel.FlatAppearance.BorderColor = Color.Silver;
    btnPrintLabel.Click += btnPrintLabel_Click;
    Controls.Add(btnPrintLabel);

    Resize += (s, e) => ApplyResponsiveLayout();
}


        private void ApplyResponsiveLayout()
        {
            MinimumSize = new Size(880, 430);

            txtPdesc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboBrand.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboCategory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnAddBrand.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtBarcode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnPrintLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnUpdate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            int rightMargin = 28;
            int buttonGap = 8;
            int bottomGap = 12;
            int printWidth = 108;

            txtBarcode.SetBounds(txtBarcode.Left, txtBarcode.Top, Math.Max(280, ClientSize.Width - rightMargin - txtBarcode.Left), txtBarcode.Height);

            btnAddBrand.SetBounds(ClientSize.Width - rightMargin - btnAddBrand.Width, btnAddBrand.Top, btnAddBrand.Width, btnAddBrand.Height);
            cboBrand.SetBounds(cboBrand.Left, cboBrand.Top, Math.Max(220, btnAddBrand.Left - cboBrand.Left - 10), cboBrand.Height);
            cboCategory.SetBounds(cboCategory.Left, cboCategory.Top, Math.Max(280, ClientSize.Width - rightMargin - cboCategory.Left), cboCategory.Height);
            txtPdesc.SetBounds(txtPdesc.Left, txtPdesc.Top, Math.Max(280, ClientSize.Width - rightMargin - txtPdesc.Left), txtPdesc.Height);

            int actionsBottom = ClientSize.Height - bottomGap - btnSave.Height;
            int right = ClientSize.Width - rightMargin;
            btnCancel.SetBounds(right - btnCancel.Width, actionsBottom, btnCancel.Width, btnCancel.Height);
            btnUpdate.SetBounds(btnCancel.Left - buttonGap - btnUpdate.Width, actionsBottom, btnUpdate.Width, btnUpdate.Height);
            btnSave.SetBounds(btnUpdate.Left - buttonGap - btnSave.Width, actionsBottom, btnSave.Width, btnSave.Height);
            btnPrintLabel.SetBounds(btnSave.Left - buttonGap - printWidth, actionsBottom, printWidth, btnSave.Height);
        }


        private decimal ParseMoney(TextBox textBox)
        {
            decimal.TryParse(textBox.Text.Trim(), out decimal value);
            return value;
        }


        private void btnPrintLabel_Click(object sender, EventArgs e)
        {
            string barcode = txtBarcode.Text.Trim();
            if (string.IsNullOrWhiteSpace(barcode))
            {
                MessageBox.Show("Please scan or enter the barcode first.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBarcode.Focus();
                return;
            }

            string description = txtPdesc.Text.Trim();
            if (string.IsNullOrWhiteSpace(description))
                description = "Product";

            BarcodeLabelHelper.ShowPrintPreview(this, barcode, description, ParseMoney(txtPrice));
        }

        private void PromptPrintLabel(string barcode, string description, decimal price)
        {
            if (string.IsNullOrWhiteSpace(barcode))
                return;

            if (MessageBox.Show("Do you want to print the barcode label now?", stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                BarcodeLabelHelper.ShowPrintPreview(this, barcode, description, price);
        }

        // Helper prompt dialog
        private string Prompt(string text, string caption)
        {
            using (Form prompt = new Form())
            {
                prompt.Width = 300;
                prompt.Height = 150;
                prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
                prompt.Text = caption;
                prompt.StartPosition = FormStartPosition.CenterScreen;

                Label textLabel = new Label() { Left = 10, Top = 20, Text = text, AutoSize = true };
                TextBox inputBox = new TextBox() { Left = 10, Top = 50, Width = 260 };
                Button confirmation = new Button() { Text = "OK", Left = 180, Width = 80, Top = 80, DialogResult = DialogResult.OK };

                prompt.Controls.Add(textLabel);
                prompt.Controls.Add(inputBox);
                prompt.Controls.Add(confirmation);
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text.Trim() : "";
            }
        }

        // Add new brand directly
        private void btnAddBrand_Click(object sender, EventArgs e)
        {
            string brandName = Prompt("Enter new brand name:", "Add Brand");
            if (string.IsNullOrWhiteSpace(brandName)) return;

            try
            {
                cn.Open();

                // Check for duplicate
                using (SqlCommand cm = new SqlCommand("SELECT COUNT(*) FROM tbBrand WHERE brand=@brand", cn))
                {
                    cm.Parameters.AddWithValue("@brand", brandName);
                    int count = (int)cm.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("This brand already exists.", "Duplicate Brand", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Insert new brand
                using (SqlCommand cm = new SqlCommand("INSERT INTO tbBrand(brand) VALUES(@brand)", cn))
                {
                    cm.Parameters.AddWithValue("@brand", brandName);
                    cm.ExecuteNonQuery();
                }

                cn.Close();
                LoadBrand();
                cboBrand.SelectedIndex = cboBrand.Items.Count - 1; // Select newly added brand
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open) cn.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Save new product
        //private void btnSave_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (MessageBox.Show("Are you sure you want to save this product?", stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //        {
        //            string barcode = txtBarcode.Text.Trim();

        //            if (string.IsNullOrEmpty(barcode))
        //            {
        //                MessageBox.Show("Please enter a barcode.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                return;
        //            }

        //            cn.Open();

        //            // Check duplicate barcode
        //            using (SqlCommand cmdCheck = new SqlCommand("SELECT COUNT(*) FROM tbProduct WHERE barcode=@barcode", cn))
        //            {
        //                cmdCheck.Parameters.AddWithValue("@barcode", barcode);
        //                int count = (int)cmdCheck.ExecuteScalar();
        //                if (count > 0)
        //                {
        //                    MessageBox.Show("This barcode is already assigned to another product.", "Duplicate Barcode", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                    cn.Close();
        //                    return;
        //                }
        //            }

        //            // Generate next PCode
        //            int nextNum = 1;
        //            using (SqlCommand cmdMax = new SqlCommand(
        //                "SELECT MAX(CAST(SUBSTRING(pcode,3,6) AS INT)) FROM tbProduct WHERE pcode LIKE 'P-%'", cn))
        //            {
        //                object result = cmdMax.ExecuteScalar();
        //                if (result != DBNull.Value) nextNum = Convert.ToInt32(result) + 1;
        //            }

        //            string newPCode = "P-" + nextNum.ToString("D6");

        //            // Insert product
        //            using (SqlCommand cm = new SqlCommand(
        //                "INSERT INTO tbProduct(pcode, barcode, pdesc, bid, cid, CostPrice, price, reorder) " +
        //                "VALUES(@pcode, @barcode, @pdesc, @bid, @cid, @costprice, @price, @reorder)", cn))
        //            {
        //                cm.Parameters.AddWithValue("@pcode", newPCode);
        //                cm.Parameters.AddWithValue("@barcode", barcode);
        //                cm.Parameters.AddWithValue("@pdesc", txtPdesc.Text.Trim());
        //                cm.Parameters.AddWithValue("@bid", cboBrand.SelectedValue);
        //                cm.Parameters.AddWithValue("@cid", cboCategory.SelectedValue);
        //                double cost = 0;
        //                if (!double.TryParse(txtCostPrice.Text, out cost))
        //                {
        //                    MessageBox.Show("Invalid cost price");
        //                    return;
        //                }

        //                cm.Parameters.AddWithValue("@costprice", cost);


        //                cm.Parameters.AddWithValue("@price", double.Parse(txtPrice.Text));
        //                cm.Parameters.AddWithValue("@reorder", UDReOrder.Value);

        //                cm.ExecuteNonQuery();
        //            }

        //            cn.Close();
        //            MessageBox.Show("Product has been successfully saved.\nPCode: " + newPCode, stitle);
        //            Clear();
        //            product.LoadProduct();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (cn.State == ConnectionState.Open) cn.Close();
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this product?", stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string barcode = txtBarcode.Text.Trim();
                    if (string.IsNullOrWhiteSpace(barcode))
                    {
                        MessageBox.Show("Please scan or enter a barcode.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtBarcode.Focus();
                        return;
                    }

                    cn.Open();

                    // Check duplicate barcode
                    using (SqlCommand cmdCheck = new SqlCommand("SELECT COUNT(*) FROM tbProduct WHERE barcode=@barcode", cn))
                    {
                        cmdCheck.Parameters.AddWithValue("@barcode", barcode);
                        int count = (int)cmdCheck.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("This barcode is already assigned to another product.", "Duplicate Barcode", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            cn.Close();
                            return;
                        }
                    }

                    // Generate next PCode
                    int nextNum = 1;
                    using (SqlCommand cmdMax = new SqlCommand(
                        "SELECT MAX(CAST(SUBSTRING(pcode,3,6) AS INT)) FROM tbProduct WHERE pcode LIKE 'P-%'", cn))
                    {
                        object result = cmdMax.ExecuteScalar();
                        if (result != DBNull.Value)
                            nextNum = Convert.ToInt32(result) + 1;
                    }

                    string newPCode = "P-" + nextNum.ToString("D6");

                    // Parse numbers
                    decimal cost = Math.Round(
                        ParseMoney(txtCostPrice), 2,
                        MidpointRounding.AwayFromZero);
                    decimal markup = Math.Round(
                        ParseMoney(txtMarkup), 2,
                        MidpointRounding.AwayFromZero);
                    decimal price = Math.Round(
                        ParseMoney(txtPrice), 2,
                        MidpointRounding.AwayFromZero);

                    // Insert product
                    using (SqlCommand cm = new SqlCommand(
                        "INSERT INTO tbProduct(pcode, barcode, pdesc, bid, cid, CostPrice, price, Markup, reorder) " +
                        "VALUES(@pcode, @barcode, @pdesc, @bid, @cid, @costprice, @price, @markup, @reorder)", cn))
                    {
                        cm.Parameters.AddWithValue("@pcode", newPCode);
                        cm.Parameters.AddWithValue("@barcode", barcode);
                        cm.Parameters.AddWithValue("@pdesc", txtPdesc.Text.Trim());
                        cm.Parameters.AddWithValue("@bid", cboBrand.SelectedValue);
                        cm.Parameters.AddWithValue("@cid", cboCategory.SelectedValue);
                        SqlParameter pCost = cm.Parameters.Add("@costprice", SqlDbType.Decimal);
                        pCost.Precision = 18;
                        pCost.Scale = 4;
                        pCost.Value = cost;

                        SqlParameter pPrice = cm.Parameters.Add("@price", SqlDbType.Decimal);
                        pPrice.Precision = 18;
                        pPrice.Scale = 4;
                        pPrice.Value = price;

                        SqlParameter pMarkup = cm.Parameters.Add("@markup", SqlDbType.Decimal);
                        pMarkup.Precision = 18;
                        pMarkup.Scale = 4;
                        pMarkup.Value = markup;
                        cm.Parameters.AddWithValue("@reorder", UDReOrder.Value);

                        cm.ExecuteNonQuery();
                    }

                    cn.Close();
                    MessageBox.Show("Product has been successfully saved.\nPCode: " + newPCode, stitle);
                    PromptPrintLabel(barcode, txtPdesc.Text.Trim(), (decimal)price);
                    Clear();
                    product.LoadProduct();
                }
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open) cn.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Update existing product
        //private void btnUpdate_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (MessageBox.Show("Are you sure you want to update this product?", stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //        {
        //            string barcode = txtBarcode.Text.Trim();

        //            cn.Open();
        //            // Check duplicate barcode excluding current product
        //            using (SqlCommand cmdCheck = new SqlCommand("SELECT COUNT(*) FROM tbProduct WHERE barcode=@barcode AND pcode<>@pcode", cn))
        //            {
        //                cmdCheck.Parameters.AddWithValue("@barcode", barcode);
        //                cmdCheck.Parameters.AddWithValue("@pcode", txtPcode.Text);
        //                int count = (int)cmdCheck.ExecuteScalar();
        //                if (count > 0)
        //                {
        //                    MessageBox.Show("This barcode is already assigned to another product.", "Duplicate Barcode", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                    cn.Close();
        //                    return;
        //                }
        //            }

        //            // Update product
        //            using (SqlCommand cm = new SqlCommand(
        //                // "UPDATE tbProduct SET barcode=@barcode, pdesc=@pdesc, bid=@bid, cid=@cid, price=@price, reorder=@reorder WHERE pcode=@pcode", cn))
        //                "UPDATE tbProduct SET barcode=@barcode, pdesc=@pdesc, bid=@bid, cid=@cid, CostPrice=@cost, price=@price, reorder=@reorder WHERE pcode=@pcode", cn))

        //            {
        //                cm.Parameters.AddWithValue("@pcode", txtPcode.Text);
        //                cm.Parameters.AddWithValue("@barcode", barcode);
        //                cm.Parameters.AddWithValue("@pdesc", txtPdesc.Text.Trim());
        //                cm.Parameters.AddWithValue("@bid", cboBrand.SelectedValue);
        //                cm.Parameters.AddWithValue("@cid", cboCategory.SelectedValue);
        //                cm.Parameters.AddWithValue("@cost", double.Parse(txtCostPrice.Text));
        //                cm.Parameters.AddWithValue("@price", double.Parse(txtPrice.Text));
        //                cm.Parameters.AddWithValue("@reorder", UDReOrder.Value);

        //                cm.ExecuteNonQuery();
        //            }

        //            cn.Close();
        //            MessageBox.Show("Product has been successfully updated.", stitle);
        //            Clear();
        //            this.Dispose();
        //            product.LoadProduct();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (cn.State == ConnectionState.Open) cn.Close();
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this product?", stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string barcode = txtBarcode.Text.Trim();
                    if (string.IsNullOrWhiteSpace(barcode))
                    {
                        MessageBox.Show("Please scan or enter a barcode.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtBarcode.Focus();
                        return;
                    }

                    cn.Open();

                    // Check duplicate barcode excluding current product
                    using (SqlCommand cmdCheck = new SqlCommand("SELECT COUNT(*) FROM tbProduct WHERE barcode=@barcode AND pcode<>@pcode", cn))
                    {
                        cmdCheck.Parameters.AddWithValue("@barcode", barcode);
                        cmdCheck.Parameters.AddWithValue("@pcode", txtPcode.Text);
                        int count = (int)cmdCheck.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("This barcode is already assigned to another product.", "Duplicate Barcode", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            cn.Close();
                            return;
                        }
                    }

                    // Parse numbers
                    decimal cost = Math.Round(
                        ParseMoney(txtCostPrice), 2,
                        MidpointRounding.AwayFromZero);
                    decimal markup = Math.Round(
                        ParseMoney(txtMarkup), 2,
                        MidpointRounding.AwayFromZero);
                    decimal price = Math.Round(
                        ParseMoney(txtPrice), 2,
                        MidpointRounding.AwayFromZero);

                    // Update product
                    using (SqlCommand cm = new SqlCommand(
                        "UPDATE tbProduct SET barcode=@barcode, pdesc=@pdesc, bid=@bid, cid=@cid, CostPrice=@costprice, price=@price, Markup=@markup, reorder=@reorder WHERE pcode=@pcode", cn))
                    {
                        cm.Parameters.AddWithValue("@pcode", txtPcode.Text);
                        cm.Parameters.AddWithValue("@barcode", barcode);
                        cm.Parameters.AddWithValue("@pdesc", txtPdesc.Text.Trim());
                        cm.Parameters.AddWithValue("@bid", cboBrand.SelectedValue);
                        cm.Parameters.AddWithValue("@cid", cboCategory.SelectedValue);
                        SqlParameter pCost = cm.Parameters.Add("@costprice", SqlDbType.Decimal);
                        pCost.Precision = 18;
                        pCost.Scale = 4;
                        pCost.Value = cost;

                        SqlParameter pPrice = cm.Parameters.Add("@price", SqlDbType.Decimal);
                        pPrice.Precision = 18;
                        pPrice.Scale = 4;
                        pPrice.Value = price;

                        SqlParameter pMarkup = cm.Parameters.Add("@markup", SqlDbType.Decimal);
                        pMarkup.Precision = 18;
                        pMarkup.Scale = 4;
                        pMarkup.Value = markup;
                        cm.Parameters.AddWithValue("@reorder", UDReOrder.Value);

                        cm.ExecuteNonQuery();
                    }

                    cn.Close();
                    MessageBox.Show("Product has been successfully updated.", stitle);
                    PromptPrintLabel(barcode, txtPdesc.Text.Trim(), (decimal)price);
                    Clear();
                    this.Dispose();
                    product.LoadProduct();
                }
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open) cn.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void ProductModule_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Dispose();
        }
    }
}
