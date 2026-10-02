using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace StocklyMobi
{
    public sealed partial class ProfitReportForm : Form
    {
        private readonly DBConnect _db = new DBConnect();
        private DataTable _data;
        public ProfitReportForm() { InitializeComponent(); }
        private void ProfitReportForm_Load(object sender, EventArgs e)
        {
            _from.Value = DateTime.Today;
            _to.Value = DateTime.Today;
            LoadCashiers();
            LoadReport();
        }
        private void BtnLoad_Click(object sender, EventArgs e) { LoadReport(); }
        private void BtnToday_Click(object sender, EventArgs e) { _from.Value=DateTime.Today; _to.Value=DateTime.Today; LoadReport(); }
        private void Category_SelectedIndexChanged(object sender, EventArgs e) { ApplyCategoryFilter(); }
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

        private void LoadReport()
        {
            try
            {
                DateTime from = _from.Value.Date;
                DateTime to = _to.Value.Date.AddDays(1);
                if (to <= from) throw new InvalidOperationException("The To date must be the same as or after the From date.");
                string cashier = _cashier.SelectedValue == null ? string.Empty : Convert.ToString(_cashier.SelectedValue);
                _data = MobileShopDb.GetDailyProfit(from, to, cashier);
                _grid.DataSource = _data;
                if (_grid.Columns.Contains("id")) _grid.Columns["id"].Visible = false;
                FormatMoney("Sales");
                FormatMoney("Cost");
                FormatMoney("Profit");
                if (_grid.Columns.Contains("Description")) _grid.Columns["Description"].FillWeight = 180;
                ApplyCategoryFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Daily Profit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ApplyCategoryFilter()
        {
            if (_data == null) return;
            string category = _category == null ? "All Categories" : _category.Text;
            _data.DefaultView.RowFilter = category == "All Categories" || string.IsNullOrWhiteSpace(category)
                ? string.Empty
                : "[Category] = '" + category.Replace("'", "''") + "'";
            UpdateSummary(_data.DefaultView);
        }

        private void UpdateSummary(DataView view)
        {
            decimal sales = 0m;
            decimal cost = 0m;
            decimal profit = 0m;
            foreach (DataRowView row in view)
            {
                sales += Convert.ToDecimal(row["Sales"]);
                cost += Convert.ToDecimal(row["Cost"]);
                profit += Convert.ToDecimal(row["Profit"]);
            }
            _sales.Text = "$ " + sales.ToString("N2");
            _cost.Text = "$ " + cost.ToString("N2");
            _profit.Text = "$ " + profit.ToString("N2");
            _profit.ForeColor = profit >= 0m ? MobileUi.Green : MobileUi.Red;
        }

        private void FormatMoney(string name)
        {
            if (_grid.Columns.Contains(name)) _grid.Columns[name].DefaultCellStyle.Format = "N2";
        }

    }
}
