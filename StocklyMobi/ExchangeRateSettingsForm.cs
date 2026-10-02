using System;
using System.Windows.Forms;

namespace StocklyMobi
{
    public partial class ExchangeRateSettingsForm : Form
    {
        private bool _loading;

        public ExchangeRateSettingsForm()
        {
            InitializeComponent();
        }

        private void ExchangeRateSettingsForm_Load(object sender, EventArgs e)
        {
            LoadRate();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ExchangeRateConfiguration.Save(numExchangeRate.Value);
                lblSaved.Text = "Saved " + DateTime.Now.ToString("hh:mm:ss tt");
                UpdatePreview();
                MessageBox.Show(
                    "The system exchange rate was saved.",
                    "Exchange Rate",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Exchange Rate",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadRate();
        }

        private void NumExchangeRate_ValueChanged(object sender, EventArgs e)
        {
            if (!_loading) UpdatePreview();
        }

        private void LoadRate()
        {
            _loading = true;
            try
            {
                decimal rate = ExchangeRateConfiguration.CurrentRate;
                if (rate < numExchangeRate.Minimum)
                    rate = numExchangeRate.Minimum;
                if (rate > numExchangeRate.Maximum)
                    rate = numExchangeRate.Maximum;

                numExchangeRate.Value = rate;
                UpdatePreview();
            }
            finally
            {
                _loading = false;
            }
        }

        private void UpdatePreview()
        {
            decimal rate = numExchangeRate.Value;
            if (rate <= 0m) rate = ExchangeRateConfiguration.DefaultRate;

            lblUsdPreview.Text = "$1.00 = " +
                ExchangeRateConfiguration.FormatRate(rate) + " LBP";

            decimal usdForExample = decimal.Round(
                100000m / rate,
                3,
                MidpointRounding.AwayFromZero);

            lblLbpPreview.Text = "100,000 LBP = $" +
                usdForExample.ToString("N2");
        }
    }
}
