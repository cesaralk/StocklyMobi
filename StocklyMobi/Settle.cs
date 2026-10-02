using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StocklyMobi
{
    public partial class Settle : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();        
        Cashier cashier;
       double exchangeRate = 89500;


        double totalUSD = 0;
        private bool _settlementInProgress;

        public Settle(Cashier cash)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            this.KeyPreview = true;
            chkPrintReceipt.Checked = PrinterConfiguration.AutoPrintReceipt;
            UpdateReceiptPrintToggle();
            cashier = cash;
            // Use the current rate from Cashier (editable on cashier screen)
            try { exchangeRate = cashier.ExchangeRateLBP; } catch { }
            // Fallback: if cashier rate not available for any reason, use saved setting
            if (exchangeRate <= 0)
            {
                try { exchangeRate = Properties.Settings.Default.ExchangeRateLBP; } catch { }
            }
            if (exchangeRate <= 0) exchangeRate = 89500;
            totalUSD = Convert.ToDouble(cashier.CurrentSaleUSD);

            // DISPLAY ONLY
            txtSale.Text = "$ " + totalUSD.ToString("#,##0.00");
            txtSaleLBP.Text = LbpFormatter.Format(totalUSD * exchangeRate);

            // IMPORTANT: store numeric sale in TAG (hidden, safe)
            txtSale.Tag = totalUSD;

           

            // display USD and LBP
            txtSale.Text = "$ " + totalUSD.ToString("#,##0.00");
            txtSaleLBP.Text = LbpFormatter.Format(totalUSD * exchangeRate);

            // store numeric value safely
            txtSale.Tag = totalUSD;

            // initialize change display
            txtChange.Text = "$ 0.00";
            txtChangeLBP.Text = LbpFormatter.Format(0d);
        

        }


        private string FormatUsdForReceipt(string value, bool includeDollarSign)
        {
            string clean = (value ?? string.Empty).Replace("$", string.Empty).Trim();
            double amount;

            if (!double.TryParse(clean, NumberStyles.Any, CultureInfo.CurrentCulture, out amount) &&
                !double.TryParse(clean, NumberStyles.Any, CultureInfo.InvariantCulture, out amount))
            {
                amount = 0;
            }

            return (includeDollarSign ? "$ " : string.Empty) + amount.ToString("#,##0.00");
        }

        private double ParseNumberForCalculation(string value)
        {
            string clean = (value ?? string.Empty)
                .Replace("$", string.Empty)
                .Replace("L.L", string.Empty)
                .Replace("LL", string.Empty)
                .Replace("LBP", string.Empty)
                .Replace(",", string.Empty)
                .Trim();

            double amount;
            if (double.TryParse(clean, NumberStyles.Any, CultureInfo.CurrentCulture, out amount))
                return amount;

            if (double.TryParse(clean, NumberStyles.Any, CultureInfo.InvariantCulture, out amount))
                return amount;

            return 0;
        }



        private void btnOne_Click(object sender, EventArgs e)
        {
            txtCash.Text += btnOne.Text;
        }

        private void btnTwo_Click(object sender, EventArgs e)
        {
            txtCash.Text += btnTwo.Text;
        }

        private void btnThree_Click(object sender, EventArgs e)
        {
            txtCash.Text += btnThree.Text;
        }

        private void btnFour_Click(object sender, EventArgs e)
        {
            txtCash.Text += btnFour.Text;
        }

        private void btnFive_Click(object sender, EventArgs e)
        {
            txtCash.Text += btnFive.Text;
        }

        private void btnSix_Click(object sender, EventArgs e)
        {
            txtCash.Text += btnSix.Text;
        }

        private void btnSeven_Click(object sender, EventArgs e)
        {
            txtCash.Text += btnSeven.Text;
        }

        private void btnEight_Click(object sender, EventArgs e)
        {
            txtCash.Text += btnEight.Text;
        }

        private void btnNine_Click(object sender, EventArgs e)
        {
            txtCash.Text += btnNine.Text;
        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            txtCash.Text += btnZero.Text;
        }

        private void btnDZero_Click(object sender, EventArgs e)
        {
            txtCash.Text += btnDZero.Text;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCash.Clear();
            txtCash.Focus();
        }
        private void btnEnter_Click(
            object sender,
            EventArgs e)
        {
            if (_settlementInProgress)
                return;

            _settlementInProgress = true;
            btnEnter.Enabled = false;

            double cashUsd =
                ParseNumberForCalculation(
                    txtCash.Text);

            double cashLbpUsd =
                Convert.ToDouble(
                    LbpFormatter.ParseAmount(
                        textCashLBP.Text))
                / exchangeRate;

            double changeUSD =
                cashUsd +
                cashLbpUsd -
                totalUSD;

            if (changeUSD < -0.0001d)
            {
                _settlementInProgress = false;
                btnEnter.Enabled = true;

                MessageBox.Show(
                    "Insufficient amount. Please enter the correct amount.",
                    "Settlement",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            decimal cashUsdAmount =
                Convert.ToDecimal(cashUsd);

            decimal cashLbpAmount =
                LbpFormatter.ParseAmount(
                    textCashLBP.Text);

            decimal changeUsdAmount =
                Convert.ToDecimal(
                    Math.Max(
                        0d,
                        changeUSD));

            decimal changeLbpAmount =
                Math.Round(
                    changeUsdAmount *
                    Convert.ToDecimal(
                        exchangeRate),
                    0,
                    MidpointRounding.AwayFromZero);

            string transactionNo =
                cashier.lblTranNo.Text;

            DateTime soldDateTime =
                DateTime.Now;

            try
            {
                MobileShopDb.CompleteTransaction(
                    transactionNo,
                    cashier.lblUsername.Text,
                    soldDateTime,
                    cashUsdAmount,
                    cashLbpAmount,
                    changeUsdAmount,
                    changeLbpAmount,
                    Convert.ToDecimal(
                        exchangeRate));
            }
            catch (Exception ex)
            {
                _settlementInProgress = false;
                btnEnter.Enabled = true;

                MessageBox.Show(
                    "The transaction was not saved. No stock, provider balance, or fixing status was changed.\r\n\r\n" +
                    ex.Message,
                    "Settlement",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // The drawer pulse is deliberately sent only after the sale has
            // committed. A drawer/printer problem must never reverse payment.
            if (PrinterConfiguration.AutoOpenCashDrawer)
            {
                try
                {
                    CashDrawerController
                        .OpenConfiguredDrawer();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Payment was saved successfully, but the cash drawer could not open automatically.\r\n\r\n" +
                        ex.GetBaseException().Message +
                        "\r\n\r\nOpen it manually and check Printer Settings.",
                        "Cash Drawer",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }

            if (chkPrintReceipt.Checked)
            {
                try
                {
                    HistoricalReceiptPrinter
                        .PrintDirect(
                            transactionNo);
                }
                catch (Exception ex)
                {
                    Exception root =
                        ex.GetBaseException();

                    MessageBox.Show(
                        "Payment was saved successfully, but the receipt could not be printed.\r\n\r\n" +
                        root.Message +
                        "\r\n\r\nYou can reprint it later from Receipt Search.",
                        "Receipt",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }

            cashier.GetTranNo();
            cashier.LoadCart();
            cashier.FocusBarcode();
            Close();
        }

        //private void txtCash_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        double sale = double.Parse(txtSale.Text);
        //        double cash = double.Parse(txtCash.Text);
        //        double charge = cash - sale;
        //        txtChange.Text = charge.ToString("#,##0.00");
        //    }
        //    catch (Exception)
        //    {
        //        txtChange.Text = "0.00";
        //    }
        //}
        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double saleUSD = (double)txtSale.Tag;

                double cashUSD = 0;
                double cashLBP = 0;

                if (!string.IsNullOrEmpty(txtCash.Text))
                    cashUSD = ParseNumberForCalculation(txtCash.Text);

                if (!string.IsNullOrEmpty(textCashLBP.Text))
                    cashLBP = Convert.ToDouble(LbpFormatter.ParseAmount(textCashLBP.Text)) / exchangeRate;

                double totalCashUSD = cashUSD + cashLBP;
                double changeUSD = totalCashUSD - saleUSD;

                txtChange.Text = "$ " + changeUSD.ToString("#,##0.00");
                txtChangeLBP.Text = LbpFormatter.Format(changeUSD * exchangeRate);
            }
            catch
            {
                txtChange.Text = "$ 0.00";
                txtChangeLBP.Text = LbpFormatter.Format(0d);
            }
        }


        private void chkPrintReceipt_CheckedChanged(object sender, EventArgs e)
        {
            UpdateReceiptPrintToggle();
        }

        private void UpdateReceiptPrintToggle()
        {
            if (chkPrintReceipt == null) return;

            chkPrintReceipt.Text = chkPrintReceipt.Checked
                ? "Print Receipt: ON"
                : "Print Receipt: OFF";

            chkPrintReceipt.BackColor = chkPrintReceipt.Checked
                ? Color.FromArgb(22, 163, 74)
                : Color.FromArgb(100, 116, 139);

            chkPrintReceipt.ForeColor = Color.White;
        }

        private void Settle_KeyDown(
            object sender,
            KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                if (!_settlementInProgress)
                    Dispose();

                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                if (!_settlementInProgress)
                    btnEnter.PerformClick();
            }
        }
    }
}
