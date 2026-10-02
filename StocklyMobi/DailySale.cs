using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace StocklyMobi
{
    public partial class DailySale : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        public string solduser;
        MainForm main;
        private Label lblSalesSummary;

        public DailySale(MainForm mn)
        {
            InitializeComponent();
            ResponsiveLayoutHelper.Apply(this);
            cn = new SqlConnection(dbcon.myConnection());
            main = mn;

            ConfigureDateTimeFilters();
            ConfigureSummaryArea();
            LoadCashier();

            cboCashier.SelectedIndex = 0;
        }

        private void ConfigureDateTimeFilters()
        {
            dtFrom.Format = DateTimePickerFormat.Custom;
            dtFrom.CustomFormat = "dd/MM/yyyy hh:mm tt";
            dtFrom.ShowUpDown = true;

            dtTo.Format = DateTimePickerFormat.Custom;
            dtTo.CustomFormat = "dd/MM/yyyy hh:mm tt";
            dtTo.ShowUpDown = true;

            dtFrom.Value = DateTime.Today;
            dtTo.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
        }

        private void ConfigureSummaryArea()
        {
            try
            {
                panel2.Height = 90;
                lblTotal.Visible = false;

                lblSalesSummary = new Label();
                lblSalesSummary.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                lblSalesSummary.Font = new Font("Century Gothic", 10.5F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                lblSalesSummary.ForeColor = Color.FromArgb(15, 23, 42);
                lblSalesSummary.Location = new Point(10, panel2.Height - 32);
                lblSalesSummary.Name = "lblSalesSummary";
                lblSalesSummary.Size = new Size(panel2.Width - 20, 24);
                lblSalesSummary.TextAlign = ContentAlignment.MiddleRight;
                lblSalesSummary.Text = "Sales Total: 0.00     Returned: 0.00";
                panel2.Controls.Add(lblSalesSummary);
                ResponsiveLayoutHelper.Apply(this);
            }
            catch
            {
                // Keep the old screen usable if runtime layout adjustment fails.
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadCashier()
        {
            cboCashier.Items.Clear();
            cboCashier.Items.Add("All Cashier");

            try
            {
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tbUser WHERE role LIKE 'Cashier'", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    cboCashier.Items.Add(dr["username"].ToString());
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                if (dr != null && !dr.IsClosed)
                    dr.Close();

                if (cn.State == ConnectionState.Open)
                    cn.Close();

                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void LoadSold()
        {
            int i = 0;
            decimal salesTotal = 0m;
            dgvSold.Rows.Clear();

            DateTime fromDate = dtFrom.Value;
            DateTime toDate = dtTo.Value;

            try
            {
                if (toDate < fromDate)
                {
                    MessageBox.Show("The end date/time must be greater than the start date/time.",
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                cn.Open();

                if (cboCashier.Text == "All Cashier")
                {
                    cm = new SqlCommand(@"
                        SELECT c.id, c.transno, c.pcode, COALESCE(NULLIF(c.itemDescription,''),p.pdesc,c.pcode) AS pdesc, c.price, c.qty, c.disc, c.total
                        FROM tbCart AS c
                        LEFT JOIN tbProduct AS p ON c.pcode = p.pcode
                        WHERE c.status = 'Sold'
                          AND c.sdate >= @fromDate
                          AND c.sdate <= @toDate
                        ORDER BY c.sdate DESC", cn);
                }
                else
                {
                    cm = new SqlCommand(@"
                        SELECT c.id, c.transno, c.pcode, COALESCE(NULLIF(c.itemDescription,''),p.pdesc,c.pcode) AS pdesc, c.price, c.qty, c.disc, c.total
                        FROM tbCart AS c
                        LEFT JOIN tbProduct AS p ON c.pcode = p.pcode
                        WHERE c.status = 'Sold'
                          AND c.sdate >= @fromDate
                          AND c.sdate <= @toDate
                          AND c.cashier = @cashier
                        ORDER BY c.sdate DESC", cn);

                    cm.Parameters.Add("@cashier", SqlDbType.VarChar).Value = cboCashier.Text;
                }

                cm.Parameters.Add("@fromDate", SqlDbType.DateTime).Value = fromDate;
                cm.Parameters.Add("@toDate", SqlDbType.DateTime).Value = toDate;

                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    decimal rowPrice = Convert.ToDecimal(dr["price"]);
                    decimal rowDisc = dr["disc"] == DBNull.Value ? 0m : Convert.ToDecimal(dr["disc"]);
                    decimal rowTotal = dr["total"] == DBNull.Value ? 0m : Convert.ToDecimal(dr["total"]);
                    salesTotal += rowTotal;

                    dgvSold.Rows.Add(
                        i,
                        dr["id"].ToString(),
                        dr["transno"].ToString(),
                        dr["pcode"].ToString(),
                        dr["pdesc"].ToString(),
                        rowPrice.ToString("#,##0.00"),
                        dr["qty"].ToString(),
                        rowDisc.ToString("#,##0.00"),
                        rowTotal.ToString("#,##0.00")
                    );
                }

                dr.Close();
                cn.Close();

                SalesSummary summary = GetSalesSummary(fromDate, toDate);
                // Use the same line total as the visible grid, then subtract tracked transaction discounts.
                summary.LineSalesTotal = salesTotal;
                summary.SalesTotal = Math.Max(0m, summary.LineSalesTotal - summary.TransactionDiscounts);
                UpdateSummaryLabels(summary);
            }
            catch (Exception ex)
            {
                if (dr != null && !dr.IsClosed)
                    dr.Close();

                if (cn.State == ConnectionState.Open)
                    cn.Close();

                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private SalesSummary GetSalesSummary(DateTime fromDate, DateTime toDate)
        {
            SalesSummary summary = new SalesSummary();

            using (SqlConnection con = new SqlConnection(dbcon.myConnection()))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(@"
                    SELECT ISNULL(SUM(c.total), 0)
                    FROM tbCart AS c
                    WHERE c.status = 'Sold'
                      AND c.sdate >= @fromDate
                      AND c.sdate <= @toDate
                      AND (@cashier = 'All Cashier' OR c.cashier = @cashier)", con))
                {
                    cmd.Parameters.Add("@fromDate", SqlDbType.DateTime).Value = fromDate;
                    cmd.Parameters.Add("@toDate", SqlDbType.DateTime).Value = toDate;
                    cmd.Parameters.Add("@cashier", SqlDbType.VarChar).Value = cboCashier.Text;
                    summary.LineSalesTotal = Convert.ToDecimal(cmd.ExecuteScalar());
                }

                using (SqlCommand cmd = new SqlCommand(@"
                    SELECT ISNULL(SUM(d.amount), 0)
                    FROM dbo.tbTransactionDiscount d
                    WHERE d.status='Sold'
                      AND ISNULL(d.soldAt,d.appliedAt)>=@fromDate
                      AND ISNULL(d.soldAt,d.appliedAt)<=@toDate
                      AND (@cashier = 'All Cashier' OR d.appliedBy=@cashier)", con))
                {
                    cmd.Parameters.Add("@fromDate", SqlDbType.DateTime).Value = fromDate;
                    cmd.Parameters.Add("@toDate", SqlDbType.DateTime).Value = toDate;
                    cmd.Parameters.Add("@cashier", SqlDbType.VarChar).Value = cboCashier.Text;
                    summary.TransactionDiscounts = Convert.ToDecimal(cmd.ExecuteScalar());
                }
                summary.SalesTotal = Math.Max(0m, summary.LineSalesTotal - summary.TransactionDiscounts);

                using (SqlCommand cmd = new SqlCommand(@"
                    SELECT ISNULL(SUM(x.total), 0)
                    FROM tbCancel AS x
                    WHERE x.sdate >= @fromDate
                      AND x.sdate <= @toDate
                      AND (
                            @cashier = 'All Cashier'
                            OR EXISTS
                            (
                                SELECT 1
                                FROM tbCart AS c
                                WHERE c.transno = x.transno
                                  AND c.pcode = x.pcode
                                  AND c.cashier = @cashier
                            )
                          )", con))
                {
                    cmd.Parameters.Add("@fromDate", SqlDbType.DateTime).Value = fromDate;
                    cmd.Parameters.Add("@toDate", SqlDbType.DateTime).Value = toDate;
                    cmd.Parameters.Add("@cashier", SqlDbType.VarChar).Value = cboCashier.Text;
                    summary.Returned = Convert.ToDecimal(cmd.ExecuteScalar());
                }
            }

            return summary;
        }

        private void UpdateSummaryLabels(SalesSummary summary)
        {
            lblTotal.Text = summary.SalesTotal.ToString("#,##0.00");

            if (lblSalesSummary != null)
            {
                lblSalesSummary.Text = "Net Sales: " + summary.SalesTotal.ToString("#,##0.00") +
                                       "     Transaction Discounts: " + summary.TransactionDiscounts.ToString("#,##0.00") +
                                       "     Returned: " + summary.Returned.ToString("#,##0.00");
            }
        }

        private string BuildSummaryText(SalesSummary summary)
        {
            return "Net Sales: " + summary.SalesTotal.ToString("#,##0.00") +
                   " | Transaction Discounts: " + summary.TransactionDiscounts.ToString("#,##0.00") +
                   " | Returned: " + summary.Returned.ToString("#,##0.00");
        }

        private void cboCashier_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSold();
        }

        private void dtFrom_ValueChanged(object sender, EventArgs e)
        {
            LoadSold();
        }

        private void dtTo_ValueChanged(object sender, EventArgs e)
        {
            LoadSold();
        }

        private void DailySale_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }

        private void dgvSold_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenCancelOrder(e);
        }

        private void dgvSold_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            OpenCancelOrder(e);
        }

        private void OpenCancelOrder(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            string colName = dgvSold.Columns[e.ColumnIndex].Name;
            if (colName != "Cancel")
                return;

            CancelOrder cancel = new CancelOrder(this);
            cancel.txtId.Text = dgvSold.Rows[e.RowIndex].Cells[1].Value.ToString();
            cancel.txtTransno.Text = dgvSold.Rows[e.RowIndex].Cells[2].Value.ToString();
            cancel.txtPcode.Text = dgvSold.Rows[e.RowIndex].Cells[3].Value.ToString();
            cancel.txtDesc.Text = dgvSold.Rows[e.RowIndex].Cells[4].Value.ToString();
            cancel.txtPrice.Text = dgvSold.Rows[e.RowIndex].Cells[5].Value.ToString();
            cancel.txtQty.Text = dgvSold.Rows[e.RowIndex].Cells[6].Value.ToString();
            cancel.txtDisc.Text = dgvSold.Rows[e.RowIndex].Cells[7].Value.ToString();
            cancel.txtTotal.Text = dgvSold.Rows[e.RowIndex].Cells[8].Value.ToString();

            if (lblTitle.Visible == false)
                cancel.txtCancelBy.Text = main.lblUsername.Text;
            else
                cancel.txtCancelBy.Text = solduser;

            if (cancel.PrepareForCancellation())
                cancel.ShowDialog();
            else
                cancel.Dispose();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            POSReport report = new POSReport();

            DateTime fromDate = dtFrom.Value;
            DateTime toDate = dtTo.Value;

            if (toDate < fromDate)
            {
                MessageBox.Show("The end date/time must be greater than the start date/time.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SalesSummary summary = GetSalesSummary(fromDate, toDate);
            string param = "Date From: " + fromDate.ToString("dd/MM/yyyy hh:mm tt") +
                           " To: " + toDate.ToString("dd/MM/yyyy hh:mm tt") +
                           "    " + BuildSummaryText(summary);

            string fromSql = fromDate.ToString("yyyy-MM-dd HH:mm:ss");
            string toSql = toDate.ToString("yyyy-MM-dd HH:mm:ss");

            if (cboCashier.Text == "All Cashier")
            {
                report.LoadDailyReport(
                    "select c.id, c.transno, c.pcode, COALESCE(NULLIF(c.itemDescription,''),p.pdesc,c.pcode) as pdesc, c.price, c.qty, c.disc as discount, c.total " +
                    "from tbCart as c left join tbProduct as p on c.pcode = p.pcode " +
                    "where status like 'Sold' and sdate >= '" + fromSql + "' and sdate <= '" + toSql + "'",
                    param,
                    cboCashier.Text
                );
            }
            else
            {
                string cashier = cboCashier.Text.Replace("'", "''");

                report.LoadDailyReport(
                    "select c.id, c.transno, c.pcode, COALESCE(NULLIF(c.itemDescription,''),p.pdesc,c.pcode) as pdesc, c.price, c.qty, c.disc as discount, c.total " +
                    "from tbCart as c left join tbProduct as p on c.pcode = p.pcode " +
                    "where status like 'Sold' and sdate >= '" + fromSql + "' and sdate <= '" + toSql + "' " +
                    "and cashier like '" + cashier + "'",
                    param,
                    cboCashier.Text
                );
            }

            report.ShowDialog();
        }

        private class SalesSummary
        {
            public decimal LineSalesTotal { get; set; }
            public decimal TransactionDiscounts { get; set; }
            public decimal SalesTotal { get; set; }
            public decimal Returned { get; set; }
        }
    }
}
