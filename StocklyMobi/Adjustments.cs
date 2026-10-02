using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;

namespace StocklyMobi
{
    public partial class Adjustments : Form
    {
        private readonly SqlConnection cn;
        private readonly DBConnect dbcon = new DBConnect();
        private readonly MainForm main;
        private readonly Timer searchTimer = new Timer();
        private int _qty;

        public Adjustments(MainForm mn)
        {
            InitializeComponent();
            ResponsiveLayoutHelper.Apply(this);
            cn = new SqlConnection(dbcon.myConnection());
            main = mn;
            ReferenceNo();
            lblUsername.Text = main.lblUsername.Text;

            OptimizeGrid();
            searchTimer.Interval = 350;
            searchTimer.Tick += SearchTimer_Tick;

            LoadStock();
        }

        private void OptimizeGrid()
        {
            try
            {
                typeof(DataGridView)
                    .GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.SetValue(dgvAdjustment, true, null);
            }
            catch { }

            dgvAdjustment.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvAdjustment.RowHeadersVisible = false;
            dgvAdjustment.AllowUserToResizeRows = false;

            foreach (DataGridViewColumn col in dgvAdjustment.Columns)
            {
                if (col.AutoSizeMode == DataGridViewAutoSizeColumnMode.AllCells)
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();
            LoadStock();
        }

        public void ReferenceNo()
        {
            Random rnd = new Random();
            lblRefNo.Text = rnd.Next().ToString();
        }

        private string BuildSql(bool hasSearch)
        {
            if (!hasSearch)
            {
                return @"
                    SELECT TOP (200)
                        p.pcode, p.barcode, p.pdesc, b.brand, c.category, p.price, p.qty
                    FROM tbProduct AS p
                    INNER JOIN tbBrand AS b ON b.id = p.bid
                    INNER JOIN tbCategory AS c ON c.id = p.cid
                    WHERE p.isActive=1
                    ORDER BY p.pdesc";
            }

            return @"
                SELECT TOP (500)
                    p.pcode, p.barcode, p.pdesc, b.brand, c.category, p.price, p.qty
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

        public void LoadStock()
        {
            int i = 0;
            string search = txtSearch.Text.Trim();
            bool hasSearch = !string.IsNullOrWhiteSpace(search);

            try
            {
                dgvAdjustment.SuspendLayout();
                dgvAdjustment.Rows.Clear();

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
                            dgvAdjustment.Rows.Add(i, dr["pcode"].ToString(), dr["barcode"].ToString(), dr["pdesc"].ToString(), dr["brand"].ToString(), dr["category"].ToString(), dr["price"].ToString(), dr["qty"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();

                dgvAdjustment.ResumeLayout();
            }
        }

        private void dgvAdjustment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string colName = dgvAdjustment.Columns[e.ColumnIndex].Name;
            if (colName == "Select")
            {
                lblPcode.Text = dgvAdjustment.Rows[e.RowIndex].Cells[1].Value.ToString();
                lblDesc.Text = dgvAdjustment.Rows[e.RowIndex].Cells[3].Value.ToString() + " " + dgvAdjustment.Rows[e.RowIndex].Cells[4].Value.ToString() + " " + dgvAdjustment.Rows[e.RowIndex].Cells[5].Value.ToString();
                _qty = int.Parse(dgvAdjustment.Rows[e.RowIndex].Cells[7].Value.ToString());
                btnSave.Enabled = true;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            searchTimer.Stop();
            searchTimer.Start();
        }

        public void Clear()
        {
            lblDesc.Text = "";
            lblPcode.Text = "";
            txtQty.Clear();
            txtRemark.Clear();
            cbAction.Text = "";
            ReferenceNo();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbAction.Text == "")
                {
                    MessageBox.Show("Please select action for add or reduce.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbAction.Focus();
                    return;
                }

                if (txtQty.Text == "")
                {
                    MessageBox.Show("Please input quantity  for add or reduce.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtQty.Focus();
                    return;
                }

                if (txtRemark.Text == "")
                {
                    MessageBox.Show("Need reason for stock adjustment.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRemark.Focus();
                    return;
                }

                if (int.Parse(txtQty.Text) > _qty)
                {
                    MessageBox.Show("Stock on hand quantity should be greater than adjustment quantity.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cbAction.Text == "Remove From Inventory")
                {
                    dbcon.ExecuteQuery("UPDATE tbProduct SET qty = (qty - " + int.Parse(txtQty.Text) + ") WHERE pcode LIKE '" + lblPcode.Text + "'");
                }
                else if (cbAction.Text == "Add To Inventory")
                {
                    dbcon.ExecuteQuery("UPDATE tbProduct SET qty = (qty + " + int.Parse(txtQty.Text) + ") WHERE pcode LIKE '" + lblPcode.Text + "'");
                }

                dbcon.ExecuteQuery("INSERT INTO tbAdjustment(referenceno, pcode, qty, action, remarks, sdate, [user]) VALUES ('" + lblRefNo.Text + "','" + lblPcode.Text + "','" + int.Parse(txtQty.Text) + "', '" + cbAction.Text + "', '" + txtRemark.Text + "', '" + DateTime.Now.ToShortDateString() + "','" + lblUsername.Text + "')");
                MessageBox.Show("Stock has been successfully adjusted.", "Process completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadStock();
                Clear();
                btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
