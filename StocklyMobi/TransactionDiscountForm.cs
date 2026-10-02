using System;
using System.Windows.Forms;
namespace StocklyMobi
{
    public sealed partial class TransactionDiscountForm : Form
    {
        private Cashier _cashier; private string _transNo=string.Empty; private string _username="cashier";
        public TransactionDiscountForm() { InitializeComponent(); }
        public TransactionDiscountForm(Cashier cashier,string transNo,string username):this() { _cashier=cashier; _transNo=transNo??string.Empty; _username=string.IsNullOrWhiteSpace(username)?"cashier":username; }
        private void Amount_ValueChanged(object sender, EventArgs e) { RefreshNetTotal(); }
        private void BtnClose_Click(object sender, EventArgs e) { Close(); }
        private void TransactionDiscountForm_Load(object sender, EventArgs e)
        {
            decimal subtotal = _cashier == null ? 0m : _cashier.CurrentCartSubtotalUSD;
            _subtotal.Text = "$ " + subtotal.ToString("N2");
            _amount.Maximum = Math.Max(0m, subtotal);

            TransactionDiscountInfo info = MobileShopDb.GetPendingTransactionDiscount(_transNo);
            decimal existing = Math.Min(_amount.Maximum, Math.Max(0m, info.Amount));
            _amount.Value = existing;
            _reason.Text = info.Reason ?? string.Empty;
            RefreshNetTotal();
            _amount.Focus();
        }

        private void RefreshNetTotal()
        {
            decimal subtotal = _cashier == null ? 0m : _cashier.CurrentCartSubtotalUSD;
            decimal net = Math.Max(0m, subtotal - _amount.Value);
            _netTotal.Text = "$ " + net.ToString("N2");
        }

        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                decimal subtotal = _cashier == null ? 0m : _cashier.CurrentCartSubtotalUSD;
                decimal amount = _amount.Value;
                if (subtotal <= 0m) throw new InvalidOperationException("Add at least one item before applying a transaction discount.");
                if (amount <= 0m) throw new InvalidOperationException("Enter a discount amount greater than zero, or use Remove Discount.");
                if (amount >= subtotal) throw new InvalidOperationException("The transaction discount must be less than the cart subtotal.");

                MobileShopDb.SavePendingTransactionDiscount(_transNo, amount, _reason.Text.Trim(), _username, subtotal);
                if (_cashier != null) _cashier.LoadCart();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Transaction Discount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            try
            {
                MobileShopDb.ClearPendingTransactionDiscount(_transNo);
                if (_cashier != null) _cashier.LoadCart();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Transaction Discount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }


    public sealed class TransactionDiscountInfo
    {
        public decimal Amount { get; set; }
        public string Reason { get; set; }
    }
}
