using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StocklyMobi
{
    public sealed partial class DiscountHistoryForm : Form
    {
        private readonly DBConnect _db = new DBConnect();

        public DiscountHistoryForm()
        {
            InitializeComponent();
        }

        private void DiscountHistoryForm_Load(object sender, EventArgs e)
        {
            _from.Value = DateTime.Today;
            _to.Value = DateTime.Today;
            LoadCashiers();
            LoadDiscounts();
        }

        private void Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                LoadDiscounts();
            }
        }

        private void BtnLoad_Click(object sender, EventArgs e) { LoadDiscounts(); }
        private void BtnToday_Click(object sender, EventArgs e)
        {
            _from.Value = DateTime.Today;
            _to.Value = DateTime.Today;
            LoadDiscounts();
        }

        private void LoadCashiers()
        {
            using (SqlConnection con = new SqlConnection(_db.myConnection()))
            using (SqlDataAdapter da = new SqlDataAdapter("SELECT '' username UNION ALL SELECT username FROM dbo.tbUser ORDER BY username", con))
            {
                DataTable table = new DataTable();
                da.Fill(table);
                _cashier.DataSource = table;
                _cashier.DisplayMember = "username";
                _cashier.ValueMember = "username";
            }
        }

        private void LoadDiscounts()
        {
            try
            {
                DateTime from = _from.Value.Date;
                DateTime to = _to.Value.Date.AddDays(1);
                string cashier = _cashier.SelectedValue == null ? string.Empty : Convert.ToString(_cashier.SelectedValue);
                string status = _status.Text == "All" ? string.Empty : _status.Text;
                string search = _search.Text.Trim();

                using (SqlConnection con = new SqlConnection(_db.myConnection()))
                using (SqlCommand cmd = new SqlCommand(@"
SELECT [Type],[Invoice],[Date],[Cashier],[Description],[Amount],[Status],[Reason]
FROM
(
    SELECT 'Transaction' AS [Type],d.transno AS [Invoice],
           CASE WHEN d.status='Sold' THEN ISNULL(d.soldAt,d.appliedAt) ELSE ISNULL(d.cancelledAt,d.appliedAt) END AS [Date],
           d.appliedBy AS [Cashier],'Complete transaction' AS [Description],d.amount AS [Amount],d.status AS [Status],ISNULL(d.reason,'') AS [Reason]
    FROM dbo.tbTransactionDiscount d
    UNION ALL
    SELECT 'Product line' AS [Type],c.transno AS [Invoice],c.sdate AS [Date],c.cashier AS [Cashier],
           COALESCE(NULLIF(c.itemDescription,''),p.pdesc,c.pcode) AS [Description],c.disc AS [Amount],c.status AS [Status],'' AS [Reason]
    FROM dbo.tbCart c
    LEFT JOIN dbo.tbProduct p ON p.pcode=c.pcode
    WHERE ISNULL(c.disc,0)>0
) x
WHERE x.[Date]>=@from AND x.[Date]<@to
  AND (@cashier='' OR x.[Cashier]=@cashier)
  AND (@status='' OR x.[Status]=@status)
  AND (@search='' OR x.[Invoice] LIKE @like OR x.[Description] LIKE @like OR x.[Reason] LIKE @like)
ORDER BY x.[Date] DESC,x.[Invoice] DESC", con))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.Add("@from", SqlDbType.DateTime).Value = from;
                    cmd.Parameters.Add("@to", SqlDbType.DateTime).Value = to;
                    cmd.Parameters.Add("@cashier", SqlDbType.VarChar, 100).Value = cashier;
                    cmd.Parameters.Add("@status", SqlDbType.VarChar, 20).Value = status;
                    cmd.Parameters.Add("@search", SqlDbType.NVarChar, 250).Value = search;
                    cmd.Parameters.Add("@like", SqlDbType.NVarChar, 270).Value = "%" + search + "%";
                    DataTable table = new DataTable();
                    da.Fill(table);
                    _grid.DataSource = table;
                    if (_grid.Columns.Contains("Amount")) _grid.Columns["Amount"].DefaultCellStyle.Format = "N2";
                    if (_grid.Columns.Contains("Description")) _grid.Columns["Description"].FillWeight = 160;
                    if (_grid.Columns.Contains("Reason")) _grid.Columns["Reason"].FillWeight = 150;

                    decimal total = 0m;
                    foreach (DataRow row in table.Rows)
                        if (Convert.ToString(row["Status"]) == "Sold") total += Convert.ToDecimal(row["Amount"]);
                    _total.Text = "Sold discount total: $ " + total.ToString("N2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Discount History", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
