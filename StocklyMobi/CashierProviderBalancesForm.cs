using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace StocklyMobi
{
    public sealed partial class CashierProviderBalancesForm : Form
    {
        public CashierProviderBalancesForm()
        {
            InitializeComponent();
        }

        private void CashierProviderBalancesForm_Load(object sender, EventArgs e)
        {
            LoadBalances();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadBalances();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadBalances()
        {
            try
            {
                DataTable providers = MobileShopDb.GetProviders(true);
                dgvBalances.DataSource = providers;

                HideColumn("id");
                HideColumn("providerCode");
                HideColumn("isActive");
                HideColumn("createdAt");

                SetHeader("providerName", "Provider / Line");
                SetHeader("providerType", "Type");
                SetHeader("currentBalance", "Current Balance");

                if (dgvBalances.Columns.Contains("currentBalance"))
                {
                    dgvBalances.Columns["currentBalance"].DefaultCellStyle.Format = "$ #,##0.00";
                    dgvBalances.Columns["currentBalance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                lblUpdated.Text = "Updated: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Provider Balances", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void HideColumn(string name)
        {
            if (dgvBalances.Columns.Contains(name))
                dgvBalances.Columns[name].Visible = false;
        }

        private void SetHeader(string name, string header)
        {
            if (dgvBalances.Columns.Contains(name))
                dgvBalances.Columns[name].HeaderText = header;
        }
    }
}
