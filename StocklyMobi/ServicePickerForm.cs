using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace StocklyMobi
{
    public sealed partial class ServicePickerForm : Form
    {
        private Cashier _cashier;
        private string _serviceType = MobileShopDb.MobileRecharge;
        private DataTable _services;

        public ServicePickerForm()
        {
            InitializeComponent();
            ApplyServiceTypeText();
        }
        public ServicePickerForm(Cashier cashier) : this(cashier, MobileShopDb.MobileRecharge) { }
        public ServicePickerForm(Cashier cashier, string serviceType) : this()
        {
            _cashier = cashier;
            _serviceType = MobileShopDb.IsKnownServiceType(serviceType) ? serviceType : MobileShopDb.MobileRecharge;
            ApplyServiceTypeText();
        }
        private void ApplyServiceTypeText()
        {
            Text = _serviceType;
            string subtitle = _serviceType == MobileShopDb.ValidityRecharge
                ? "Choose a validity package, then enter the Retake $ amount. The sale is added to the current transaction."
                : "Choose a service. It is added to the current transaction and processed only after settlement.";
            lblHeader.Text = _serviceType + "\r\n" + subtitle;
        }
        private void ServicePickerForm_Load(object sender, EventArgs e) { LoadProviderBalances(); LoadServices(); }
        private void ServicePickerForm_Resize(object sender, EventArgs e) { ResizeCards(); }
        private void Search_TextChanged(object sender, EventArgs e) { RenderButtons(); }
        private void BtnRefreshBalances_Click(object sender, EventArgs e) { LoadProviderBalances(); }

        private void LoadProviderBalances()
        {
            if (_balancesHost == null) return;

            _balancesHost.SuspendLayout();
            _balancesHost.Controls.Clear();
            try
            {
                DataTable providers = MobileShopDb.GetProviders(true);
                foreach (DataRow row in providers.Rows)
                {
                    Label balance = new Label();
                    balance.AutoSize = false;
                    balance.BackColor = lblBalanceTemplate.BackColor;
                    balance.BorderStyle = lblBalanceTemplate.BorderStyle;
                    balance.Font = lblBalanceTemplate.Font;
                    balance.ForeColor = lblBalanceTemplate.ForeColor;
                    balance.Margin = lblBalanceTemplate.Margin;
                    balance.Padding = lblBalanceTemplate.Padding;
                    balance.Size = lblBalanceTemplate.Size;
                    balance.TextAlign = lblBalanceTemplate.TextAlign;
                    balance.Text = Convert.ToString(row["providerName"]) + ": $" +
                        Convert.ToDecimal(row["currentBalance"]).ToString("N2");
                    _balancesHost.Controls.Add(balance);
                }

                if (_balancesHost.Controls.Count == 0)
                {
                    Label empty = new Label();
                    empty.AutoSize = true;
                    empty.ForeColor = MobileUi.Text;
                    empty.Margin = new Padding(8, 10, 8, 8);
                    empty.Text = "No active provider balances are configured.";
                    _balancesHost.Controls.Add(empty);
                }
                lblBalancesUpdated.Text = "Balances update after settlement • " + DateTime.Now.ToString("HH:mm:ss");
            }
            catch (Exception ex)
            {
                lblBalancesUpdated.Text = "Could not load balances";
                ToolTip tip = new ToolTip();
                tip.SetToolTip(lblBalancesUpdated, ex.Message);
            }
            finally
            {
                _balancesHost.ResumeLayout();
            }
        }

        private void LoadServices()
        {
            try { _services = MobileShopDb.GetServices(true, _serviceType); RenderButtons(); }
            catch (Exception ex) { MessageBox.Show(ex.Message, _serviceType, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void RenderButtons()
        {
            _buttonsHost.SuspendLayout();
            _buttonsHost.Controls.Clear();
            string filter = (_search.Text ?? string.Empty).Trim().ToLowerInvariant();
            if (_services != null)
            {
                foreach (DataRow row in _services.Rows)
                {
                    string name = row["serviceName"].ToString();
                    string provider = row["providerName"].ToString();
                    string operatorName = row["operatorName"].ToString();
                    string haystack = (name + " " + provider + " " + operatorName).ToLowerInvariant();
                    if (filter.Length > 0 && !haystack.Contains(filter)) continue;
                    decimal price = Convert.ToDecimal(row["finalPrice"]);
                    Button card = new Button();
                    card.Width = _serviceButtonTemplate.Width;
                    card.Height = _serviceButtonTemplate.Height;
                    card.Margin = _serviceButtonTemplate.Margin;
                    card.Padding = _serviceButtonTemplate.Padding;
                    card.TextAlign = _serviceButtonTemplate.TextAlign;
                    card.FlatStyle = _serviceButtonTemplate.FlatStyle;
                    card.BackColor = _serviceButtonTemplate.BackColor;
                    card.ForeColor = _serviceButtonTemplate.ForeColor;
                    card.Cursor = _serviceButtonTemplate.Cursor;
                    card.Font = _serviceButtonTemplate.Font;
                    card.FlatAppearance.BorderColor = _serviceButtonTemplate.FlatAppearance.BorderColor;
                    card.FlatAppearance.BorderSize = _serviceButtonTemplate.FlatAppearance.BorderSize;
                    card.Tag = row;
                    string operatorLine = string.IsNullOrWhiteSpace(operatorName) ? string.Empty : "\r\nOperator: " + operatorName;
                    card.Text = name + operatorLine + "\r\nPrice: $" + price.ToString("N2") + "\r\nClick to add to transaction";
                    card.Click += ServiceButton_Click;
                    _buttonsHost.Controls.Add(card);
                }
            }
            if (_buttonsHost.Controls.Count == 0)
            {
                Label empty = new Label();
                empty.Text = "No active " + _serviceType + " services are configured. Open Admin > Services and create one first.";
                empty.AutoSize = true; empty.MaximumSize = new Size(720,0); empty.ForeColor = MobileUi.Text;
                empty.Font = new Font("Century Gothic",11F); empty.Margin = new Padding(20); _buttonsHost.Controls.Add(empty);
            }
            _buttonsHost.ResumeLayout();
            ResizeCards();
        }

        private void ResizeCards()
        {
            if (_buttonsHost == null || _buttonsHost.ClientSize.Width <= 0) return;
            int usable = Math.Max(280, _buttonsHost.ClientSize.Width - 50);
            int columns = usable >= 900 ? 3 : usable >= 580 ? 2 : 1;
            int width = Math.Max(250, (usable / columns) - 18);
            foreach (Control control in _buttonsHost.Controls) { Button button = control as Button; if (button != null) button.Width = width; }
        }

        private void ServiceButton_Click(object sender, EventArgs e)
        {
            try
            {
                Button button = sender as Button; DataRow row = button == null ? null : button.Tag as DataRow; if (row == null) return;
                if (_cashier == null) throw new InvalidOperationException("Cashier transaction is not available.");
                decimal retake = 0m;
                if (_serviceType == MobileShopDb.ValidityRecharge)
                {
                    using (ValidityRetakeDialog dialog = new ValidityRetakeDialog(row["serviceName"].ToString(), row["operatorName"].ToString(), Convert.ToDecimal(row["finalPrice"])))
                    { if (dialog.ShowDialog(this) != DialogResult.OK) return; retake = dialog.RetakeAmount; }
                }
                MobileShopDb.AddServiceToCart(_cashier.lblTranNo.Text, _cashier.lblUsername.Text, Convert.ToInt32(row["id"]), retake);
                _cashier.LoadCart(); _cashier.FocusBarcode();
                MessageBox.Show("Service added to the current transaction.", _serviceType, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, _serviceType, MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
    }
}
