using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;

namespace StocklyMobi
{
    public partial class LookUpProduct : Form
    {
        private readonly SqlConnection cn;
        private readonly DBConnect dbcon = new DBConnect();
        private readonly Cashier cashier;
        private readonly Timer searchTimer = new Timer();

        public LookUpProduct(Cashier cash)
        {
            InitializeComponent();
            ResponsiveLayoutHelper.Apply(this);
            cn = new SqlConnection(dbcon.myConnection());
            cashier = cash;

            OptimizeGrid();

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
                    SELECT TOP (200)
                        p.pcode,
                        p.barcode,
                        p.pdesc,
                        b.brand,
                        c.category,
                        p.price,
                        p.qty
                    FROM tbProduct AS p
                    INNER JOIN tbBrand AS b ON b.id = p.bid
                    INNER JOIN tbCategory AS c ON c.id = p.cid
                    WHERE p.isActive=1
                    ORDER BY p.pdesc";
            }

            return @"
                SELECT TOP (500)
                    p.pcode,
                    p.barcode,
                    p.pdesc,
                    b.brand,
                    c.category,
                    p.price,
                    p.qty
                FROM tbProduct AS p
                INNER JOIN tbBrand AS b ON b.id = p.bid
                INNER JOIN tbCategory AS c ON c.id = p.cid
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
                            decimal.TryParse(dr["price"].ToString(), out decimal sellPrice);
                            dgvProduct.Rows.Add(
                                i,
                                dr["pcode"].ToString(),
                                dr["barcode"].ToString(),
                                dr["pdesc"].ToString(),
                                dr["brand"].ToString(),
                                dr["category"].ToString(),
                                sellPrice.ToString("0.00"),
                                dr["qty"].ToString());
                        }
                    }
                }
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string colName = dgvProduct.Columns[e.ColumnIndex].Name;
            if (colName == "Select")
            {
                Qty qty = new Qty(cashier);
                qty.ProductDetails(
                    dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    decimal.Parse(dgvProduct.Rows[e.RowIndex].Cells[6].Value.ToString()),
                    cashier.lblTranNo.Text,
                    int.Parse(dgvProduct.Rows[e.RowIndex].Cells[7].Value.ToString()));
                qty.ShowDialog();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            searchTimer.Stop();
            searchTimer.Start();
        }

        private void LookUpProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Dispose();
        }

        private void metroTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void barcodeScan_KeyDown(object sender, KeyEventArgs e)
        {
        }
    }
}
