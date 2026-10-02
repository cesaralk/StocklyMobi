using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StocklyMobi
{
    public sealed partial class NewProductQuickForm : Form
    {
        private readonly DBConnect _db = new DBConnect();
        private readonly string _productCode;
        private bool _pricingChange;
        private bool IsEditMode { get { return !string.IsNullOrWhiteSpace(_productCode); } }

        public string CreatedProductCode { get; private set; }

        public NewProductQuickForm()
            : this(null)
        {
        }

        public NewProductQuickForm(string productCode)
        {
            _productCode = string.IsNullOrWhiteSpace(productCode) ? null : productCode.Trim();
            InitializeComponent();
        }

        private void NewProductQuickForm_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            LoadLookups();
            if (IsEditMode)
            {
                Text = "Edit Product";
                lblHeader.Text = "Edit Product";
                btnSave.Text = "Save Changes";
                LoadProduct();
            }
        }

        private void BtnAddBrand_Click(object sender, EventArgs e)
        {
            AddLookupValue(_brand, "Brand", "tbBrand", "brand");
        }

        private void BtnAddCategory_Click(object sender, EventArgs e)
        {
            AddLookupValue(_category, "Category", "tbCategory", "category");
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void AddLookupValue(ComboBox combo, string caption, string table, string column)
        {
            string value = TextEntryDialog.Show(
                "Enter the new " + caption.ToLowerInvariant() + " name", "New " + caption);
            if (string.IsNullOrWhiteSpace(value)) return;

            try
            {
                using (SqlConnection con = new SqlConnection(_db.myConnection()))
                using (SqlCommand cmd = new SqlCommand(
                    "IF NOT EXISTS(SELECT 1 FROM dbo." + table + " WHERE " + column +
                    "=@value) INSERT INTO dbo." + table + "(" + column + ") VALUES(@value)", con))
                {
                    cmd.Parameters.Add("@value", SqlDbType.NVarChar, 100).Value = value.Trim();
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                LoadLookups();
                SelectLookupByText(combo, value.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static bool SelectLookupByText(ComboBox combo, string value)
        {
            if (combo == null || string.IsNullOrWhiteSpace(value)) return false;

            DataTable data = combo.DataSource as DataTable;
            if (data == null || string.IsNullOrWhiteSpace(combo.DisplayMember)) return false;

            foreach (DataRow row in data.Rows)
            {
                if (string.Equals(Convert.ToString(row[combo.DisplayMember]), value.Trim(),
                    StringComparison.CurrentCultureIgnoreCase))
                {
                    combo.SelectedValue = row[combo.ValueMember];
                    return true;
                }
            }

            combo.Text = value.Trim();
            return false;
        }

        private void LoadLookups()
        {
            using (SqlConnection con = new SqlConnection(_db.myConnection()))
            {
                con.Open();
                EnsureGeneralLookup(con, "tbBrand", "brand", "General");
                EnsureGeneralLookup(con, "tbCategory", "category", "General");

                _brand.DataSource = LoadLookup(con, "tbBrand", "brand");
                _brand.DisplayMember = "brand";
                _brand.ValueMember = "id";
                _category.DataSource = LoadLookup(con, "tbCategory", "category");
                _category.DisplayMember = "category";
                _category.ValueMember = "id";
            }
        }

        private void LoadProduct()
        {
            using (SqlConnection con = new SqlConnection(_db.myConnection()))
            using (SqlCommand cmd = new SqlCommand(@"
SELECT p.pcode,p.barcode,p.pdesc,p.bid,p.cid,
       ISNULL(p.CostPrice,0) CostPrice,ISNULL(p.Markup,0) Markup,
       ISNULL(p.price,0) Price,ISNULL(p.reorder,0) Reorder
FROM dbo.tbProduct p
WHERE p.pcode=@pcode", con))
            {
                cmd.Parameters.Add("@pcode", SqlDbType.VarChar, 50).Value = _productCode;
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                        throw new InvalidOperationException("The selected product no longer exists.");

                    _barcode.Text = Convert.ToString(reader["barcode"]);
                    _description.Text = Convert.ToString(reader["pdesc"]);
                    _brand.SelectedValue = Convert.ToInt32(reader["bid"]);
                    _category.SelectedValue = Convert.ToInt32(reader["cid"]);
                    SetNumericValue(_cost, Convert.ToDecimal(reader["CostPrice"]));
                    SetNumericValue(_markup, Convert.ToDecimal(reader["Markup"]));
                    SetNumericValue(_price, Convert.ToDecimal(reader["Price"]));
                    SetNumericValue(_reorder, Convert.ToDecimal(reader["Reorder"]));
                }
            }
        }

        private static void SetNumericValue(NumericUpDown control, decimal value)
        {
            control.Value = Math.Max(control.Minimum, Math.Min(control.Maximum, value));
        }

        private static DataTable LoadLookup(SqlConnection con, string table, string column)
        {
            using (SqlDataAdapter da = new SqlDataAdapter(
                "SELECT id," + column + " FROM dbo." + table + " ORDER BY " + column, con))
            {
                DataTable data = new DataTable();
                da.Fill(data);
                return data;
            }
        }

        private static void EnsureGeneralLookup(
            SqlConnection con, string table, string column, string value)
        {
            using (SqlCommand cmd = new SqlCommand(
                "IF NOT EXISTS(SELECT 1 FROM dbo." + table + " WHERE " + column +
                "=@value) INSERT INTO dbo." + table + "(" + column + ") VALUES(@value)", con))
            {
                cmd.Parameters.Add("@value", SqlDbType.NVarChar, 100).Value = value;
                cmd.ExecuteNonQuery();
            }
        }

        private void CostOrMarkupChanged(object sender, EventArgs e)
        {
            if (_pricingChange) return;
            try
            {
                _pricingChange = true;
                _price.Value = Math.Min(_price.Maximum,
                    Math.Round(_cost.Value * (1m + _markup.Value / 100m), 2,
                        MidpointRounding.AwayFromZero));
            }
            finally
            {
                _pricingChange = false;
            }
        }

        private void PriceChanged(object sender, EventArgs e)
        {
            if (_pricingChange || _cost.Value <= 0m) return;
            try
            {
                _pricingChange = true;
                decimal markup = ((_price.Value - _cost.Value) / _cost.Value) * 100m;
                _markup.Value = Math.Max(_markup.Minimum,
                    Math.Min(_markup.Maximum,
                        Math.Round(markup, 2, MidpointRounding.AwayFromZero)));
            }
            finally
            {
                _pricingChange = false;
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateInput();

                using (SqlConnection con = new SqlConnection(_db.myConnection()))
                {
                    con.Open();
                    using (SqlTransaction tr = con.BeginTransaction(IsolationLevel.Serializable))
                    {
                        try
                        {
                            EnsureBarcodeIsUnique(con, tr);
                            int brandId = EnsureLookup(con, tr, "tbBrand", "brand", _brand.Text.Trim());
                            int categoryId = EnsureLookup(con, tr, "tbCategory", "category", _category.Text.Trim());

                            if (IsEditMode)
                                UpdateProduct(con, tr, brandId, categoryId);
                            else
                                InsertProduct(con, tr, brandId, categoryId);

                            tr.Commit();
                        }
                        catch
                        {
                            tr.Rollback();
                            throw;
                        }
                    }
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, IsEditMode ? "Edit Product" : "New Product",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(_barcode.Text))
                throw new InvalidOperationException("Barcode is required.");
            if (string.IsNullOrWhiteSpace(_description.Text))
                throw new InvalidOperationException("Description is required.");
            if (string.IsNullOrWhiteSpace(_brand.Text))
                throw new InvalidOperationException("Brand is required.");
            if (string.IsNullOrWhiteSpace(_category.Text))
                throw new InvalidOperationException("Category is required.");
        }

        private void EnsureBarcodeIsUnique(SqlConnection con, SqlTransaction tr)
        {
            using (SqlCommand duplicate = new SqlCommand(@"
SELECT COUNT(*)
FROM dbo.tbProduct
WHERE barcode=@barcode AND (@pcode IS NULL OR pcode<>@pcode)", con, tr))
            {
                duplicate.Parameters.Add("@barcode", SqlDbType.VarChar, 100)
                    .Value = _barcode.Text.Trim();
                duplicate.Parameters.Add("@pcode", SqlDbType.VarChar, 50)
                    .Value = (object)_productCode ?? DBNull.Value;
                if (Convert.ToInt32(duplicate.ExecuteScalar()) > 0)
                    throw new InvalidOperationException(
                        "This barcode already belongs to another product.");
            }
        }

        private void InsertProduct(SqlConnection con, SqlTransaction tr, int brandId, int categoryId)
        {
            string pcode = GenerateProductCode(con, tr);
            using (SqlCommand cmd = new SqlCommand(@"
INSERT INTO dbo.tbProduct
(pcode,barcode,pdesc,bid,cid,CostPrice,Markup,price,qty,reorder)
VALUES
(@pcode,@barcode,@description,@brand,@category,@cost,@markup,@price,0,@reorder)", con, tr))
            {
                AddProductParameters(cmd, pcode, brandId, categoryId);
                cmd.ExecuteNonQuery();
            }
            CreatedProductCode = pcode;
        }

        private void UpdateProduct(SqlConnection con, SqlTransaction tr, int brandId, int categoryId)
        {
            using (SqlCommand cmd = new SqlCommand(@"
UPDATE dbo.tbProduct
SET barcode=@barcode,
    pdesc=@description,
    bid=@brand,
    cid=@category,
    CostPrice=@cost,
    Markup=@markup,
    price=@price,
    reorder=@reorder
WHERE pcode=@pcode", con, tr))
            {
                AddProductParameters(cmd, _productCode, brandId, categoryId);
                if (cmd.ExecuteNonQuery() != 1)
                    throw new InvalidOperationException("The product could not be updated.");
            }
            CreatedProductCode = _productCode;
        }

        private void AddProductParameters(
            SqlCommand cmd, string pcode, int brandId, int categoryId)
        {
            cmd.Parameters.Add("@pcode", SqlDbType.VarChar, 50).Value = pcode;
            cmd.Parameters.Add("@barcode", SqlDbType.VarChar, 100)
                .Value = _barcode.Text.Trim();
            cmd.Parameters.Add("@description", SqlDbType.NVarChar, 300)
                .Value = _description.Text.Trim();
            cmd.Parameters.Add("@brand", SqlDbType.Int).Value = brandId;
            cmd.Parameters.Add("@category", SqlDbType.Int).Value = categoryId;
            AddMoney(cmd, "@cost", _cost.Value);
            AddMoney(cmd, "@markup", _markup.Value);
            AddMoney(cmd, "@price", _price.Value);
            cmd.Parameters.Add("@reorder", SqlDbType.Int)
                .Value = Convert.ToInt32(_reorder.Value);
        }

        private static int EnsureLookup(
            SqlConnection con, SqlTransaction tr, string table, string column, string value)
        {
            using (SqlCommand find = new SqlCommand(
                "SELECT TOP 1 id FROM dbo." + table + " WHERE " + column + "=@value", con, tr))
            {
                find.Parameters.Add("@value", SqlDbType.NVarChar, 100).Value = value;
                object existing = find.ExecuteScalar();
                if (existing != null && existing != DBNull.Value)
                    return Convert.ToInt32(existing);
            }

            using (SqlCommand insert = new SqlCommand(
                "INSERT INTO dbo." + table + "(" + column +
                ") VALUES(@value); SELECT CAST(SCOPE_IDENTITY() AS INT);", con, tr))
            {
                insert.Parameters.Add("@value", SqlDbType.NVarChar, 100).Value = value;
                return Convert.ToInt32(insert.ExecuteScalar());
            }
        }

        private static string GenerateProductCode(SqlConnection con, SqlTransaction tr)
        {
            using (SqlCommand cmd = new SqlCommand(@"
SELECT ISNULL(MAX(TRY_CONVERT(INT,SUBSTRING(pcode,3,20))),0)+1
FROM dbo.tbProduct WITH (UPDLOCK,HOLDLOCK)
WHERE pcode LIKE 'P-%'", con, tr))
            {
                return "P-" + Convert.ToInt32(cmd.ExecuteScalar()).ToString("000000");
            }
        }

        private static void AddMoney(SqlCommand cmd, string name, decimal value)
        {
            SqlParameter parameter = cmd.Parameters.Add(name, SqlDbType.Decimal);
            parameter.Precision = 18;
            parameter.Scale = 4;
            parameter.Value = value;
        }
    }
}
