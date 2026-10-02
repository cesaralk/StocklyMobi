using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace StocklyMobi
{
    public sealed partial class DeliverFixForm : Form
    {
        private Cashier _cashier;
        private DataRow _row;

        public DeliverFixForm()
        {
            InitializeComponent();
        }

        public DeliverFixForm(Cashier cashier) : this()
        {
            _cashier = cashier;
        }

        private void DeliverFixForm_Load(object sender, EventArgs e)
        {
            _normalizedCode.Text = "FIX-" + DateTime.Now.ToString("yy") + "-";
            _fixCode.Focus();
        }

        private void FixCode_TextChanged(object sender, EventArgs e)
        {
            _normalizedCode.Text = MobileShopDb.NormalizeFixCodeInput(_fixCode.Text);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            SearchFix();
        }

        private void FixCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SearchFix();
            }
        }

        private void SearchFix()
        {
            try
            {
                string code = MobileShopDb.NormalizeFixCodeInput(_fixCode.Text);
                if (string.IsNullOrWhiteSpace(code)) throw new InvalidOperationException("Enter the Fix number, for example 3.");
                _row = MobileShopDb.GetFixByCode(code);
                if (_row == null) throw new InvalidOperationException("Fix ID " + code + " was not found.");
                _fixCode.Text = _row["fixCode"].ToString();
                _normalizedCode.Text = _row["fixCode"].ToString();
                _customer.Text = _row["customerName"].ToString();
                _phone.Text = _row["phoneModel"].ToString();
                _problem.Text = _row["problemDescription"].ToString();
                _status.Text = _row["status"].ToString();
                _status.ForeColor = _status.Text == "Ready" ? MobileUi.Green : MobileUi.Amber;
                _price.Text = "$ " + Convert.ToDecimal(_row["finalPrice"]).ToString("N2");
            }
            catch (Exception ex)
            {
                _row = null;
                ClearDetails();
                MessageBox.Show(ex.Message, "Deliver Fixed Phone", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (_row == null) SearchFix();
                if (_row == null) return;
                if (_cashier == null) throw new InvalidOperationException("Cashier transaction is not available.");

                string status = _row["status"].ToString();
                if (status != "Ready")
                {
                    MessageBox.Show("This fixing job is currently '" + status + "'. Mark it Ready first.",
                        "Deliver Fixed Phone", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                MobileShopDb.AddFixToCart(_cashier.lblTranNo.Text, _cashier.lblUsername.Text, _row["fixCode"].ToString());
                _cashier.LoadCart();
                _cashier.FocusBarcode();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Deliver Fixed Phone", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ClearDetails()
        {
            _customer.Text = string.Empty;
            _phone.Text = string.Empty;
            _problem.Text = string.Empty;
            _status.Text = string.Empty;
            _price.Text = string.Empty;
        }
    }
}
