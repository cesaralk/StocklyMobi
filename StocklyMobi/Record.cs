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
    public partial class Record : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;

        // Runtime columns
        private const string ColInvCostPrice = "colInvCostPrice";
        private const string ColInvMarkup = "colInvMarkup";
        private const string ColStockInCostPrice = "colStockInCostPrice";
        private const string ColStockInMarkup = "colStockInMarkup";
        private const string ColStockInPrice = "colStockInPrice";
        public Record()
        {
            InitializeComponent();
            ResponsiveLayoutHelper.Apply(this);
            cn = new SqlConnection(dbcon.myConnection());
            ConfigureSoldItemsSummaryLabel();
            LoadCriticalItems();
            LoadInventoryList();
        }

        private void EnsureInventoryPricingColumns()
        {
            if (!dgvInventoryList.Columns.Contains(ColInvCostPrice))
            {
                dgvInventoryList.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = ColInvCostPrice,
                    HeaderText = "Cost Price",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                    ReadOnly = true,
                    DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "#,##0.00" }
                });
            }
            if (!dgvInventoryList.Columns.Contains(ColInvMarkup))
            {
                dgvInventoryList.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = ColInvMarkup,
                    HeaderText = "Markup %",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                    ReadOnly = true,
                    DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "#,##0.00" }
                });
            }
        }

        private void EnsureStockInPricingColumns()
        {
            if (!dgvStockIn.Columns.Contains(ColStockInCostPrice))
            {
                dgvStockIn.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = ColStockInCostPrice,
                    HeaderText = "Cost Price",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                    ReadOnly = true,
                    DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "#,##0.00" }
                });
            }
            if (!dgvStockIn.Columns.Contains(ColStockInMarkup))
            {
                dgvStockIn.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = ColStockInMarkup,
                    HeaderText = "Markup %",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                    ReadOnly = true,
                    DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "#,##0.00" }
                });
            }
            if (!dgvStockIn.Columns.Contains(ColStockInPrice))
            {
                dgvStockIn.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = ColStockInPrice,
                    HeaderText = "Price",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                    ReadOnly = true,
                    DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "#,##0.00" }
                });
            }
        }


        private void ConfigureSoldItemsSummaryLabel()
        {
            try
            {
                lblTotal.Location = new Point(420, 21);
                lblTotal.Size = new Size(548, 31);
                lblTotal.TextAlign = ContentAlignment.MiddleRight;
                lblTotal.Text = "Sales Total: 0.00     Returned: 0.00";
            }
            catch
            {
                // Keep the original layout if runtime adjustment fails.
            }
        }

        private decimal GetCancelledTotal(DateTime fromDate, DateTime toDate)
        {
            using (SqlConnection con = new SqlConnection(dbcon.myConnection()))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(@"
                    SELECT ISNULL(SUM(total), 0)
                    FROM tbCancel
                    WHERE sdate >= @fromDate
                      AND sdate <= @toDate", con))
                {
                    cmd.Parameters.Add("@fromDate", SqlDbType.DateTime).Value = fromDate;
                    cmd.Parameters.Add("@toDate", SqlDbType.DateTime).Value = toDate;
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
            }
        }

        private string BuildGrossCancelNetText(decimal salesTotal, decimal returned)
        {
            return "Sales Total: " + salesTotal.ToString("#,##0.00") +
                   "     Returned: " + returned.ToString("#,##0.00");
        }

        public void LoadTopSelling()
        {
            int i = 0;
            dgvTopSelling.Rows.Clear();
            cn.Open();

            //Sort By Total Amount
            if (cbTopSell.Text == "Sort By Qty")
            {
                cm = new SqlCommand("SELECT TOP 10 pcode, pdesc, isnull(sum(qty),0) AS qty, ISNULL(SUM(total),0) AS total FROM vwTopSelling WHERE sdate BETWEEN '" + dtFromTopSell.Value.ToString() + "' AND '" + dtToTopSell.Value.ToString() + "' AND status LIKE 'Sold' GROUP BY pcode, pdesc ORDER BY qty DESC", cn);
            }
            else if (cbTopSell.Text == "Sort By Total Amount")
            {
                cm = new SqlCommand("SELECT TOP 10 pcode, pdesc, isnull(sum(qty),0) AS qty, ISNULL(SUM(total),0) AS total FROM vwTopSelling WHERE sdate BETWEEN '" + dtFromTopSell.Value.ToString() + "' AND '" + dtToTopSell.Value.ToString() + "' AND status LIKE 'Sold' GROUP BY pcode, pdesc ORDER BY total DESC", cn);
            }
            dr = cm.ExecuteReader();
            while(dr.Read())
            {
                i++;
                dgvTopSelling.Rows.Add(i, dr["pcode"].ToString(), dr["pdesc"].ToString(), dr["qty"].ToString(), double.Parse(dr["total"].ToString()).ToString("#,##0.00"));
            }
            dr.Close();
            cn.Close();
        }

        public void LoadSoldItems()
        {
            try
            {
                dgvSoldItems.Rows.Clear();
                int i = 0;
                DateTime fromDate = dtFromSoldItems.Value;
                DateTime toDate = dtToSoldItems.Value;
                decimal gross = 0m;

                cn.Open();
                cm = new SqlCommand(@"
                    SELECT c.pcode,
                           COALESCE(NULLIF(c.itemDescription,''),p.pdesc,c.pcode) AS pdesc,
                           c.price, SUM(c.qty) AS qty, SUM(c.disc) AS disc, SUM(c.total) AS total
                    FROM tbCart AS c
                    LEFT JOIN tbProduct AS p ON c.pcode = p.pcode
                    WHERE c.status = 'Sold'
                      AND c.sdate >= @fromDate
                      AND c.sdate <= @toDate
                    GROUP BY c.pcode, COALESCE(NULLIF(c.itemDescription,''),p.pdesc,c.pcode), c.price", cn);
                cm.Parameters.Add("@fromDate", SqlDbType.DateTime).Value = fromDate;
                cm.Parameters.Add("@toDate", SqlDbType.DateTime).Value = toDate;

                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    decimal rowPrice = Convert.ToDecimal(dr["price"]);
                    decimal rowDisc = dr["disc"] == DBNull.Value ? 0m : Convert.ToDecimal(dr["disc"]);
                    decimal rowTotal = dr["total"] == DBNull.Value ? 0m : Convert.ToDecimal(dr["total"]);
                    gross += rowTotal;

                    dgvSoldItems.Rows.Add(
                        i,
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

                decimal cancelled = GetCancelledTotal(fromDate, toDate);
                lblTotal.Text = BuildGrossCancelNetText(gross, cancelled);
            }
            catch (Exception ex)
            {
                if (dr != null && !dr.IsClosed)
                    dr.Close();

                if (cn.State == ConnectionState.Open)
                    cn.Close();

                MessageBox.Show(ex.Message);
            }
        }

        public void LoadCriticalItems()
        {
            try
            {
                dgvCriticalItems.Rows.Clear();
                int i = 0;
                cn.Open();
                cm = new SqlCommand("SELECT * FROM vwCriticalItems",cn);
                dr = cm.ExecuteReader();
                while(dr.Read())
                {
                    i++;
                    dgvCriticalItems.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString());

                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public void LoadInventoryList()
        {
            try
            {
                EnsureInventoryPricingColumns();
                dgvInventoryList.Rows.Clear();
                int i = 0;
                cn.Open();
                // Add costprice/markup from product master
                cm = new SqlCommand(@"SELECT v.*, p.costprice, p.markup FROM vwInventoryList v INNER JOIN tbProduct p ON p.pcode = v.pcode AND p.isActive=1 ORDER BY p.pdesc", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    // original 8 columns + cost + markup
                    string cost = dr[dr.FieldCount - 2] == DBNull.Value ? "0.00" : double.Parse(dr[dr.FieldCount - 2].ToString()).ToString("#,##0.00");
                    string markup = dr[dr.FieldCount - 1] == DBNull.Value ? "0" : double.Parse(dr[dr.FieldCount - 1].ToString()).ToString("#,##0.00");

                    dgvInventoryList.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), cost, markup);

                }
                dr.Close();
                cn.Close();
                ResponsiveLayoutHelper.Apply(this);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        public void LoadCancelItems()
        {
            try
            {
                int i = 0;
                dgvCancel.Rows.Clear();
                cn.Open();
                cm = new SqlCommand(@"
                    SELECT *
                    FROM vwCancelItems
                    WHERE sdate >= @fromDate
                      AND sdate <= @toDate
                    ORDER BY sdate DESC", cn);
                cm.Parameters.Add("@fromDate", SqlDbType.DateTime).Value = dtFromCancel.Value;
                cm.Parameters.Add("@toDate", SqlDbType.DateTime).Value = dtToCancel.Value;

                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    decimal price = dr[3] == DBNull.Value ? 0m : Convert.ToDecimal(dr[3]);
                    decimal total = dr[5] == DBNull.Value ? 0m : Convert.ToDecimal(dr[5]);
                    dgvCancel.Rows.Add(
                        i,
                        dr[0].ToString(),
                        dr[1].ToString(),
                        dr[2].ToString(),
                        price.ToString("#,##0.00"),
                        dr[4].ToString(),
                        total.ToString("#,##0.00"),
                        DateTime.Parse(dr[6].ToString()).ToString("dd/MM/yyyy hh:mm tt"),
                        dr[7].ToString(),
                        dr[8].ToString(),
                        dr[9].ToString(),
                        dr[10].ToString()
                    );
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

                MessageBox.Show(ex.Message);
            }
        }

        public void LoadStockInHist()
        {
            int i = 0;
            dgvStockIn.Rows.Clear();
            EnsureStockInPricingColumns();
            cn.Open();
            cm = new SqlCommand(@"
                SELECT
                    s.id,
                    s.refno,
                    s.pcode,
                    p.pdesc,
                    s.qty,
                    s.sdate,
                    s.stockinby,
                    s.status,
                    ISNULL(sup.supplier,'') AS supplier,
                    ISNULL(s.CostPrice, p.costprice) AS CostPrice,
                    ISNULL(s.Markup, p.markup) AS Markup,
                    ISNULL(s.Price, p.price) AS Price
                FROM tbStockIn s
                INNER JOIN tbProduct p ON p.pcode = s.pcode
                LEFT JOIN tbSupplier sup ON sup.id = s.supplierid
                WHERE cast(s.sdate AS date) BETWEEN @from AND @to AND s.status LIKE 'Done'
                ORDER BY s.sdate DESC, s.id DESC", cn);
            cm.Parameters.AddWithValue("@from", dtFromStockIn.Value.Date);
            cm.Parameters.AddWithValue("@to", dtToStockIn.Value.Date);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvStockIn.Rows.Add(
                    i,
                    dr["id"].ToString(),
                    dr["refno"].ToString(),
                    dr["pcode"].ToString(),
                    dr["pdesc"].ToString(),
                    dr["qty"].ToString(),
                    DateTime.Parse(dr["sdate"].ToString()).ToShortDateString(),
                    dr["stockinby"].ToString(),
                    dr["status"].ToString(),
                    dr["supplier"].ToString(),
                    Convert.ToDecimal(dr["CostPrice"]).ToString("#,##0.00"),
                    Convert.ToDecimal(dr["Markup"]).ToString("#,##0.00"),
                    Convert.ToDecimal(dr["Price"]).ToString("#,##0.00")
                );
            }
            dr.Close();
            cn.Close();
            ResponsiveLayoutHelper.Apply(this);
        }

        private void btnLoadTopSell_Click(object sender, EventArgs e)
        {
            if(cbTopSell.Text== "Select sort type")
            {
                MessageBox.Show("Please select sort type from the dropdown list.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbTopSell.Focus();
                return;
            }
            LoadTopSelling();
        }

        private void btnLoadSoldItems_Click(object sender, EventArgs e)
        {
            LoadSoldItems();
        }

        private void btnPrintSoldItems_Click(object sender, EventArgs e)
        {
            POSReport report = new POSReport();
            DateTime fromDate = dtFromSoldItems.Value;
            DateTime toDate = dtToSoldItems.Value;
            decimal gross = 0m;

            using (SqlConnection con = new SqlConnection(dbcon.myConnection()))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(SUM(total),0) FROM tbCart WHERE status = 'Sold' AND sdate >= @fromDate AND sdate <= @toDate", con))
                {
                    cmd.Parameters.Add("@fromDate", SqlDbType.DateTime).Value = fromDate;
                    cmd.Parameters.Add("@toDate", SqlDbType.DateTime).Value = toDate;
                    gross = Convert.ToDecimal(cmd.ExecuteScalar());
                }
            }

            decimal cancelled = GetCancelledTotal(fromDate, toDate);
            string param = "From : " + fromDate.ToString("dd/MM/yyyy hh:mm tt") +
                           " To : " + toDate.ToString("dd/MM/yyyy hh:mm tt") +
                           "    " + BuildGrossCancelNetText(gross, cancelled);

            string fromSql = fromDate.ToString("yyyy-MM-dd HH:mm:ss");
            string toSql = toDate.ToString("yyyy-MM-dd HH:mm:ss");

            report.LoadSoldItems(
                "SELECT c.pcode, COALESCE(NULLIF(c.itemDescription,''),p.pdesc,c.pcode) AS pdesc, c.price, sum(c.qty) as qty, SUM(c.disc) AS disc, SUM(c.total) AS total " +
                "FROM tbCart AS c LEFT JOIN tbProduct AS p ON c.pcode=p.pcode " +
                "WHERE c.status LIKE 'Sold' AND c.sdate >= '" + fromSql + "' AND c.sdate <= '" + toSql + "' " +
                "GROUP BY c.pcode, COALESCE(NULLIF(c.itemDescription,''),p.pdesc,c.pcode), c.price",
                param);
            report.ShowDialog();
        }

        private void btnLoadCancel_Click(object sender, EventArgs e)
        {
            LoadCancelItems();
        }

        private void btnLoadStockIn_Click(object sender, EventArgs e)
        {
            LoadStockInHist();
        }

        private void btnPrintTopSell_Click(object sender, EventArgs e)
        {
            POSReport report = new POSReport();
            string param = "From : " + dtFromTopSell.Value.ToString() + " To : " + dtToTopSell.Value.ToString();
            if (cbTopSell.Text == "Sort By Qty")
            {
                report.LoadTopSelling("SELECT TOP 10 pcode, pdesc, isnull(sum(qty),0) AS qty, ISNULL(SUM(total),0) AS total FROM vwTopSelling WHERE sdate BETWEEN '" + dtFromTopSell.Value.ToString() + "' AND '" + dtToTopSell.Value.ToString() + "' AND status LIKE 'Sold' GROUP BY pcode, pdesc ORDER BY qty DESC", param, "TOP SELLING ITEMS SORT BY QTY");
            }
            else if (cbTopSell.Text == "Sort By Total Amount")
            {
                report.LoadTopSelling("SELECT TOP 10 pcode, pdesc, isnull(sum(qty),0) AS qty, ISNULL(SUM(total),0) AS total FROM vwTopSelling WHERE sdate BETWEEN '" + dtFromTopSell.Value.ToString() + "' AND '" + dtToTopSell.Value.ToString() + "' AND status LIKE 'Sold' GROUP BY pcode, pdesc ORDER BY total DESC", param, "TOP SELLING ITEMS SORY BY TOTAL AMOUNT");
            }
            report.ShowDialog();
        }

        private void btnPrintInventoryList_Click(object sender, EventArgs e)
        {
            POSReport report = new POSReport();
            report.LoadInventory("SELECT * FROM vwInventoryList");
            report.ShowDialog();
        }

        private void btnPrintCancel_Click(object sender, EventArgs e)
        {
            POSReport report = new POSReport();
            string param = "From : " + dtFromCancel.Value.ToString() + " To : " + dtToCancel.Value.ToString();
            report.LoadCancelledOrder("SELECT * FROM vwCancelItems WHERE sdate BETWEEN '" + dtFromCancel.Value.ToString() + "' AND '" + dtToCancel.Value.ToString() + "'", param);
            report.ShowDialog();
        }

        private void btnPrintStockIn_Click(object sender, EventArgs e)
        {
            POSReport report = new POSReport();
            string param = "From : " + dtFromStockIn.Value.ToString() + " To : " + dtToStockIn.Value.ToString();
            report.LoadStockInHist("SELECT * FROM vwStockIn WHERE cast(sdate AS date) BETWEEN '" + dtFromStockIn.Value.ToString() + "' AND '" + dtToStockIn.Value.ToString() + "' AND status LIKE 'Done'", param);
            report.ShowDialog();
        }
    }
}
