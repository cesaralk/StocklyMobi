using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;

namespace StocklyMobi
{
    public partial class Product : Form
    {
        private readonly SqlConnection cn;
        private readonly DBConnect dbcon = new DBConnect();
        private readonly Timer searchTimer = new Timer();

        public Product()
        {
            InitializeComponent();
            ResponsiveLayoutHelper.Apply(this);
            cn = new SqlConnection(dbcon.myConnection());

            OptimizeGrid();

            txtSearch.TextChanged -= txtSearch_TextChanged;
            txtSearch.TextChanged += txtSearch_TextChanged;
            txtSearch.KeyDown -= txtSearch_KeyDown;
            txtSearch.KeyDown += txtSearch_KeyDown;

            searchTimer.Interval = 350;
            searchTimer.Tick += SearchTimer_Tick;

            LoadProduct();
        }

        private void OptimizeGrid()
        {
            try
            {
                typeof(DataGridView)
                    .GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.SetValue(dgvProduct, true, null);
            }
            catch { }

            dgvProduct.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvProduct.RowHeadersVisible = false;
            dgvProduct.AllowUserToResizeRows = false;

            foreach (DataGridViewColumn col in dgvProduct.Columns)
            {
                if (col.AutoSizeMode == DataGridViewAutoSizeColumnMode.AllCells)
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();
            LoadProduct();
        }

        private string BuildSql(bool hasSearch)
        {
            if (!hasSearch)
            {
                return @"
                    SELECT TOP (300)
                        p.pcode,
                        p.barcode,
                        p.pdesc,
                        b.brand,
                        c.category,
                        p.CostPrice,
                        p.Markup,
                        p.price,
                        p.reorder
                    FROM tbProduct p
                    INNER JOIN tbBrand b ON p.bid = b.id
                    INNER JOIN tbCategory c ON p.cid = c.id
                    WHERE p.isActive=1
                    ORDER BY p.pdesc";
            }

            return @"
                SELECT TOP (1000)
                    p.pcode,
                    p.barcode,
                    p.pdesc,
                    b.brand,
                    c.category,
                    p.CostPrice,
                    p.Markup,
                    p.price,
                    p.reorder
                FROM tbProduct p
                INNER JOIN tbBrand b ON p.bid = b.id
                INNER JOIN tbCategory c ON p.cid = c.id
                WHERE p.isActive=1
                  AND (p.pcode LIKE @search
                   OR p.barcode LIKE @search
                   OR p.pdesc LIKE @search
                   OR b.brand LIKE @search
                   OR c.category LIKE @search)
                ORDER BY p.pdesc";
        }

        public void LoadProduct()
        {
            int i = 0;
            string search = txtSearch.Text.Trim();
            bool hasSearch = !string.IsNullOrWhiteSpace(search);

            try
            {
                dgvProduct.SuspendLayout();
                dgvProduct.Rows.Clear();

                using (SqlCommand cm = new SqlCommand(BuildSql(hasSearch), cn))
                {
                    if (hasSearch)
                        cm.Parameters.AddWithValue("@search", "%" + search + "%");

                    cn.Open();
                    using (SqlDataReader dr = cm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            i++;
                            decimal.TryParse(dr["CostPrice"].ToString(), out decimal costPrice);
                            decimal.TryParse(dr["Markup"].ToString(), out decimal markup);
                            decimal.TryParse(dr["price"].ToString(), out decimal sellPrice);

                            dgvProduct.Rows.Add(
                                i,
                                dr["pcode"].ToString(),
                                dr["barcode"].ToString(),
                                dr["pdesc"].ToString(),
                                dr["brand"].ToString(),
                                dr["category"].ToString(),
                                costPrice.ToString("0.00"),
                                markup.ToString("0.00"),
                                sellPrice.ToString("0.00"),
                                dr["reorder"].ToString());
                        }
                    }
                }

                if (dgvProduct.Rows.Count > 0)
                    dgvProduct.Rows[0].Selected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();

                dgvProduct.ResumeLayout();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            searchTimer.Stop();
            searchTimer.Start();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                searchTimer.Stop();
                LoadProduct();
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductModule product = new ProductModule(this);
            product.ShowDialog();
            LoadProduct();
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string colName = dgvProduct.Columns[e.ColumnIndex].Name;
            string pcode = dgvProduct.Rows[e.RowIndex].Cells[1].Value?.ToString() ?? "";

            if (colName == "Edit")
            {
                ProductModule product = new ProductModule(this);

                string brandText = dgvProduct.Rows[e.RowIndex].Cells[4].Value?.ToString() ?? "";
                string categoryText = dgvProduct.Rows[e.RowIndex].Cells[5].Value?.ToString() ?? "";

                product.txtPcode.Text = pcode;
                product.txtBarcode.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value?.ToString() ?? "";
                product.txtPdesc.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value?.ToString() ?? "";
                product.txtCostPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[6].Value?.ToString() ?? "0";
                product.txtMarkup.Text = dgvProduct.Rows[e.RowIndex].Cells[7].Value?.ToString() ?? "0";
                product.txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[8].Value?.ToString() ?? "0";

                int brandIndex = product.cboBrand.FindStringExact(brandText);
                if (brandIndex >= 0)
                    product.cboBrand.SelectedIndex = brandIndex;
                else
                    product.cboBrand.Text = brandText;

                int categoryIndex = product.cboCategory.FindStringExact(categoryText);
                if (categoryIndex >= 0)
                    product.cboCategory.SelectedIndex = categoryIndex;
                else
                    product.cboCategory.Text = categoryText;

                int reorderValue = 1;
                int.TryParse(dgvProduct.Rows[e.RowIndex].Cells[9].Value?.ToString(), out reorderValue);
                product.UDReOrder.Value = reorderValue;

                product.txtPcode.Enabled = false;
                product.btnSave.Enabled = false;
                product.btnUpdate.Enabled = true;

                product.ShowDialog();
                LoadProduct();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show(
                    "Are you sure you want to delete this product?",
                    "Delete Product",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlCommand pendingCmd = new SqlCommand(@"
SELECT
    (SELECT COUNT(*) FROM dbo.tbCart WHERE pcode=@pcode AND status='Pending') +
    (SELECT COUNT(*) FROM dbo.tbStockIn WHERE pcode=@pcode AND status='Pending')", cn))
                        {
                            pendingCmd.Parameters.AddWithValue("@pcode", pcode);
                            cn.Open();
                            int pending = Convert.ToInt32(pendingCmd.ExecuteScalar());
                            if (pending > 0)
                                throw new InvalidOperationException(
                                    "This product is currently used in a pending cashier transaction or stock-in entry.");
                        }

                        using (SqlCommand cm = new SqlCommand(@"
UPDATE dbo.tbProduct
SET isActive=0,qty=0,reorder=0,
    barcode=LEFT('DELETED-' + CONVERT(VARCHAR(20),id) + '-' +
                 REPLACE(CONVERT(VARCHAR(36),NEWID()),'-',''),100)
WHERE pcode=@pcode AND isActive=1", cn))
                        {
                            cm.Parameters.AddWithValue("@pcode", pcode);
                            if (cm.ExecuteNonQuery() != 1)
                                throw new InvalidOperationException("The product no longer exists or was already deleted.");
                        }

                        MessageBox.Show("Product deleted successfully. Previous history was preserved.", "POS");
                        LoadProduct();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        if (cn.State == ConnectionState.Open)
                            cn.Close();
                    }
                }
            }
        }
    }
}
