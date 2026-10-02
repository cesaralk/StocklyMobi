using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace StocklyMobi
{
    public partial class ProductStockIn : Form
    {
        private readonly SqlConnection cn;
        private SqlCommand cm;
        private readonly DBConnect dbcon = new DBConnect();
        private readonly string stitle = "StocklyMobi POS";
        private readonly StockIn stockIn;
        private readonly Timer searchTimer = new Timer();

        public ProductStockIn(StockIn stk)
        {
            InitializeComponent();
            ResponsiveLayoutHelper.Apply(this);
            cn = new SqlConnection(dbcon.myConnection());
            stockIn = stk;

            try
            {
                txtBarcode.TextChanged -= txtSearch_TextChanged; // designer had this wired incorrectly
            }
            catch { }

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

        public int AskQuantity()
        {
            Form f = new Form();
            f.Width = 280;
            f.Height = 160;
            f.Text = "Enter Quantity";
            f.StartPosition = FormStartPosition.CenterParent;
            f.FormBorderStyle = FormBorderStyle.FixedDialog;
            f.MaximizeBox = false;
            f.MinimizeBox = false;

            Label lbl = new Label();
            lbl.Text = "Quantity (0 = price only):";
            lbl.Location = new Point(30, 20);
            lbl.AutoSize = true;

            TextBox txt = new TextBox();
            txt.Text = "1";
            txt.Location = new Point(30, 45);
            txt.Width = 200;

            Button btnOk = new Button();
            btnOk.Text = "OK";
            btnOk.Location = new Point(55, 85);
            btnOk.DialogResult = DialogResult.OK;

            Button btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.Location = new Point(145, 85);
            btnCancel.DialogResult = DialogResult.Cancel;

            f.Controls.Add(lbl);
            f.Controls.Add(txt);
            f.Controls.Add(btnOk);
            f.Controls.Add(btnCancel);
            f.AcceptButton = btnOk;
            f.CancelButton = btnCancel;

            if (f.ShowDialog() == DialogResult.OK)
            {
                int qty;
                if (int.TryParse(txt.Text, out qty) && qty >= 0)
                    return qty;

                MessageBox.Show("Quantity must be 0 or more.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return -1;
        }

        public void addStockInWithQty(string pcode, int qty)
        {
            try
            {
                cn.Open();

                cm = new SqlCommand(
                    "UPDATE tbStockIn SET qty = qty + @qty WHERE refno=@refno AND pcode=@pcode", cn);

                cm.Parameters.AddWithValue("@qty", qty);
                cm.Parameters.AddWithValue("@refno", stockIn.txtRefNo.Text);
                cm.Parameters.AddWithValue("@pcode", pcode);

                int rowsAffected = cm.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    cm = new SqlCommand(
                        "INSERT INTO tbStockIn (refno, pcode, qty, sdate, stockinby, supplierid) VALUES (@refno, @pcode, @qty, @sdate, @stockinby, @supplierid)", cn);

                    cm.Parameters.AddWithValue("@refno", stockIn.txtRefNo.Text);
                    cm.Parameters.AddWithValue("@pcode", pcode);
                    cm.Parameters.AddWithValue("@qty", qty);
                    cm.Parameters.AddWithValue("@sdate", stockIn.dtStockIn.Value);
                    cm.Parameters.AddWithValue("@stockinby", stockIn.txtStockInBy.Text);
                    cm.Parameters.AddWithValue("@supplierid", stockIn.lblId.Text);

                    cm.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, stitle);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        private string BuildSql(bool hasSearch)
        {
            if (!hasSearch)
            {
                return @"
                    SELECT TOP (200) pcode, barcode, pdesc, qty
                    FROM tbProduct
                    WHERE isActive=1
                    ORDER BY pdesc";
            }

            return @"
                SELECT TOP (500) pcode, barcode, pdesc, qty
                FROM tbProduct
                WHERE isActive=1
                  AND (pcode LIKE @search
                   OR barcode LIKE @search
                   OR pdesc LIKE @search)
                ORDER BY pdesc";
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

                using (SqlCommand cmd = new SqlCommand(BuildSql(hasSearch), cn))
                {
                    if (hasSearch)
                        cmd.Parameters.AddWithValue("@search", "%" + search + "%");

                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            i++;
                            dgvProduct.Rows.Add(i, dr["pcode"].ToString(), dr["pdesc"].ToString(), dr["qty"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, stitle);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();

                dgvProduct.ResumeLayout();
            }
        }

        private bool TryLoadScannedProduct(string barcode, out string pcode)
        {
            pcode = string.Empty;
            string description = string.Empty;
            string qty = "0";

            try
            {
                dgvProduct.SuspendLayout();
                dgvProduct.Rows.Clear();

                using (SqlCommand cmd = new SqlCommand(@"SELECT TOP (1) pcode, pdesc, qty FROM tbProduct WHERE barcode=@barcode AND isActive=1", cn))
                {
                    cmd.Parameters.AddWithValue("@barcode", barcode);
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (!dr.Read())
                            return false;

                        pcode = dr["pcode"].ToString();
                        description = dr["pdesc"].ToString();
                        qty = dr["qty"].ToString();
                    }
                }

                dgvProduct.Rows.Add(1, pcode, description, qty);
                if (dgvProduct.Rows.Count > 0)
                {
                    dgvProduct.ClearSelection();
                    dgvProduct.Rows[0].Selected = true;
                    dgvProduct.CurrentCell = dgvProduct.Rows[0].Cells[1];
                }

                txtSearch.TextChanged -= txtSearch_TextChanged;
                txtSearch.Text = description;
                txtSearch.TextChanged += txtSearch_TextChanged;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, stitle);
                return false;
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
                if (string.IsNullOrWhiteSpace(stockIn.txtStockInBy.Text))
                {
                    MessageBox.Show("Please enter stock in by name first.", stitle,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    stockIn.txtStockInBy.Focus();
                    return;
                }

                if (MessageBox.Show("Add this item?", stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string pcode = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                    int qty = AskQuantity();
                    if (qty < 0)
                        return;

                    stockIn.ScanBarcodeManual(pcode, qty);
                    MessageBox.Show("Successfully added", stitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            searchTimer.Stop();
            searchTimer.Start();
        }

        private void ProductStockIn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Dispose();
        }

        private void ProductStockIn_Load(object sender, EventArgs e)
        {
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                string barcode = txtBarcode.Text.Trim();
                txtBarcode.Clear();

                if (string.IsNullOrEmpty(barcode))
                    return;

                if (string.IsNullOrWhiteSpace(stockIn.txtStockInBy.Text))
                {
                    MessageBox.Show("Please enter stock in by name first.", stitle,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    stockIn.txtStockInBy.Focus();
                    return;
                }

                if (!TryLoadScannedProduct(barcode, out string pcode))
                {
                    MessageBox.Show("Product not found", stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int qty = AskQuantity();
                if (qty < 0)
                    return;

                stockIn.ScanBarcodeManual(pcode, qty);
                MessageBox.Show("Successfully added", stitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
