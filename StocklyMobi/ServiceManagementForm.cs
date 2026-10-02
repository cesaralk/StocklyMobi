using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace StocklyMobi
{
    public sealed partial class ServiceManagementForm : Form
    {
        private string _username = "admin";
        private readonly string[] _serviceTypes =
        {
            MobileShopDb.MobileRecharge, MobileShopDb.TransferBalance,
            MobileShopDb.ValidityRecharge, MobileShopDb.InternetBundles, MobileShopDb.Apps
        };
        private int _providerId;
        private int _serviceId;
        private string _selectedType = MobileShopDb.MobileRecharge;
        private bool _loading;
        private bool _syncingServiceCost;
        private bool _syncingServiceDeducted;
        private bool _syncingServicePrice;
        private bool _syncingProviderAdjustment;

        public ServiceManagementForm() { InitializeComponent(); }
        public ServiceManagementForm(string username) : this() { _username=string.IsNullOrWhiteSpace(username)?"admin":username; }
        private void ServiceManagementForm_Load(object sender, EventArgs e) { ReloadAll(); RefreshExchangeRateDisplay(); BeginInvoke((MethodInvoker)AdjustResponsiveLayout); }
        private void ServiceManagementForm_Shown(object sender, EventArgs e) { AdjustResponsiveLayout(); }
        private void ServiceManagementForm_Resize(object sender, EventArgs e) { AdjustResponsiveLayout(); }
        private void BtnNewService_Click(object sender, EventArgs e) { ClearService(); }
        private void BtnRefreshServices_Click(object sender, EventArgs e) { ReloadAll(); }
        private void BtnNewProvider_Click(object sender, EventArgs e) { ClearProvider(); }
        private void BtnAddBalance_Click(object sender, EventArgs e) { ApplyBalance(true); }
        private void BtnRemoveBalance_Click(object sender, EventArgs e) { ApplyBalance(false); }
        private void AdjustResponsiveLayout()
        {
            bool compactHeight = ClientSize.Height < 720;
            lblHeader.Height = compactHeight ? 56 : 72;
            _categoryBar.Height = compactHeight ? 54 : 64;

            foreach (Control control in _categoryBar.Controls)
            {
                Button button = control as Button;
                if (button != null) button.Height = compactHeight ? 38 : 44;
            }

            ConfigureResponsiveSplit(_servicesSplit, compactHeight);
            ConfigureResponsiveSplit(_providersSplit, compactHeight);
        }

        private static void ConfigureResponsiveSplit(SplitContainer split, bool compactHeight)
        {
            if (split == null || split.IsDisposed) return;

            bool sideBySide = split.ClientSize.Width >= 860;
            Orientation desired = sideBySide ? Orientation.Vertical : Orientation.Horizontal;
            if (split.Orientation != desired)
            {
                split.Panel1MinSize = 0;
                split.Panel2MinSize = 0;
                split.Orientation = desired;
            }

            if (desired == Orientation.Vertical)
            {
                int preferred = Math.Max(380, (int)(split.ClientSize.Width * 0.56));
                ResponsiveLayoutHelper.SetSafeSplitter(split, preferred, 340, 390);
            }
            else
            {
                int editorMinimum = compactHeight ? 260 : 300;
                int preferred = Math.Max(170, split.ClientSize.Height - editorMinimum - split.SplitterWidth);
                ResponsiveLayoutHelper.SetSafeSplitter(split, preferred, 150, editorMinimum);
            }
        }

        private static void AddEditorCell(TableLayoutPanel panel, int labelColumn, int row, string label, Control control)
        {
            Label caption = MobileUi.Label(label);
            caption.Dock = DockStyle.Fill;
            panel.Controls.Add(caption, labelColumn, row);
            control.Dock = DockStyle.Fill;
            panel.Controls.Add(control, labelColumn + 1, row);
        }

        private void ReloadAll()
        {
            try
            {
                _loading = true;
                LoadProviders();
                ConfigureServiceRules();
                LoadServices();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Services", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _loading = false;
            }

            LoadSelectedServiceIntoEditor();
            LoadSelectedProviderIntoEditor();
        }

        private void LoadProviders()
        {
            DataTable all = MobileShopDb.GetProviders(false);
            _providersGrid.DataSource = all;
            HideColumn(_providersGrid, "id");
            if (_providersGrid.Columns.Contains("currentBalance")) _providersGrid.Columns["currentBalance"].DefaultCellStyle.Format = "N2";

            DataTable active = MobileShopDb.GetProviders(true);
            _serviceProvider.DataSource = active;
            _serviceProvider.DisplayMember = "providerName";
            _serviceProvider.ValueMember = "id";
        }

        private void LoadServices()
        {
            _servicesGrid.DataSource = MobileShopDb.GetServices(false, _selectedType);
            HideColumn(_servicesGrid, "id");
            HideColumn(_servicesGrid, "providerId");
            HideColumn(_servicesGrid, "providerCode");
            HideColumn(_servicesGrid, "retakeEnabled");
            HideColumn(_servicesGrid, "currentBalance");
            FormatMoney(_servicesGrid, "cost");
            FormatMoney(_servicesGrid, "deductedPrice");
            if (_servicesGrid.Columns.Contains("deductedPrice"))
            {
                _servicesGrid.Columns["deductedPrice"].HeaderText = "Deducted Price";
                _servicesGrid.Columns["deductedPrice"].Visible = _selectedType == MobileShopDb.ValidityRecharge;
            }
            FormatMoney(_servicesGrid, "finalPrice");
            FormatMoney(_servicesGrid, "profit");
        }

        private void CategoryButton_Click(object sender, EventArgs e)
        {
            Button clicked = sender as Button;
            if (clicked == null) return;
            _selectedType = Convert.ToString(clicked.Tag);
            foreach (Control control in _categoryBar.Controls)
            {
                Button button = control as Button;
                if (button != null) button.BackColor = Convert.ToString(button.Tag) == _selectedType ? MobileUi.Blue : MobileUi.Slate;
            }
            _selectedCategoryLabel.Text = _selectedType.ToUpperInvariant();
            try
            {
                _loading = true;
                ClearService();
                ConfigureServiceRules();
                LoadServices();
            }
            finally
            {
                _loading = false;
            }
            LoadSelectedServiceIntoEditor();
        }

        private void ConfigureServiceRules()
        {
            if (_serviceProvider.DataSource == null) return;
            string previousDescription = _serviceDescription.Text;

            DataTable allProviders = MobileShopDb.GetProviders(true);
            DataView view = new DataView(allProviders);
            if (_selectedType == MobileShopDb.TransferBalance)
                view.RowFilter = "providerCode='MTC_LINE' OR providerCode='ALFA_LINE'";
            else if (_selectedType == MobileShopDb.MobileRecharge || _selectedType == MobileShopDb.ValidityRecharge)
                view.RowFilter = "providerCode='WISH'";

            _serviceProvider.DataSource = view;
            _serviceProvider.DisplayMember = "providerName";
            _serviceProvider.ValueMember = "id";

            bool validity = _selectedType == MobileShopDb.ValidityRecharge;
            _serviceOperator.Enabled = validity;
            lblServiceDeducted.Visible = validity;
            _serviceDeducted.Visible = validity;
            lblServiceDeductedLbp.Visible = validity;
            _serviceDeductedLbp.Visible = validity;
            if (!validity) SetServiceDeductedUsd(_serviceCost.Value);
            if (validity)
            {
                if (_serviceOperator.SelectedIndex < 0) _serviceOperator.SelectedIndex = 0;
                DataTable names = MobileShopDb.GetServiceDescriptions(MobileShopDb.MobileRecharge);
                _serviceDescription.DropDownStyle = ComboBoxStyle.DropDownList;
                _serviceDescription.Items.Clear();
                foreach (DataRow row in names.Rows) _serviceDescription.Items.Add(row["serviceName"].ToString());
                if (_serviceDescription.Items.Count > 0 && _serviceDescription.SelectedIndex < 0)
                    _serviceDescription.SelectedIndex = 0;
            }
            else
            {
                _serviceDescription.DropDownStyle = ComboBoxStyle.DropDown;
                _serviceDescription.Items.Clear();
                if (_selectedType != MobileShopDb.TransferBalance) _serviceOperator.SelectedIndex = -1;
            }

            if (!string.IsNullOrWhiteSpace(previousDescription)) _serviceDescription.Text = previousDescription;
            if (_selectedType == MobileShopDb.TransferBalance) UpdateTransferOperator();
        }

        private void ServiceProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loading) return;
            if (_selectedType == MobileShopDb.TransferBalance) UpdateTransferOperator();
        }

        private void UpdateTransferOperator()
        {
            DataRowView row = _serviceProvider.SelectedItem as DataRowView;
            if (row == null) return;
            string code = Convert.ToString(row["providerCode"]);
            _serviceOperator.Enabled = false;
            _serviceOperator.Text = code == MobileShopDb.MtcLineProviderCode ? "MTC" : code == MobileShopDb.AlfaLineProviderCode ? "Alfa" : string.Empty;
        }

        private void LoadSelectedServiceIntoEditor()
        {
            if (_servicesGrid == null || _servicesGrid.CurrentRow == null) return;
            ServicesGrid_SelectionChanged(_servicesGrid, EventArgs.Empty);
        }

        private void ServicesGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (_loading || _servicesGrid.CurrentRow == null) return;
            DataRowView row = _servicesGrid.CurrentRow.DataBoundItem as DataRowView;
            if (row == null) return;
            _serviceId = Convert.ToInt32(row["id"]);
            _serviceDescription.Text = row["serviceName"].ToString();
            _serviceProvider.SelectedValue = Convert.ToInt32(row["providerId"]);
            _serviceOperator.Text = row["operatorName"].ToString();
            SetServiceCostUsd(Convert.ToDecimal(row["cost"]));
            SetServiceDeductedUsd(Convert.ToDecimal(row["deductedPrice"]));
            SetServicePriceUsd(Convert.ToDecimal(row["finalPrice"]));
            _serviceActive.Checked = Convert.ToBoolean(row["isActive"]);
            RecalculateProfit(null, EventArgs.Empty);
        }

        private void LoadSelectedProviderIntoEditor()
        {
            if (_providersGrid == null || _providersGrid.CurrentRow == null) return;
            ProvidersGrid_SelectionChanged(_providersGrid, EventArgs.Empty);
        }

        private void ProvidersGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (_loading || _providersGrid.CurrentRow == null) return;
            DataRowView row = _providersGrid.CurrentRow.DataBoundItem as DataRowView;
            if (row == null) return;
            _providerId = Convert.ToInt32(row["id"]);
            _providerName.Text = row["providerName"].ToString();
            _providerCode.Text = row["providerCode"].ToString();
            _providerType.Text = row["providerType"].ToString();
            _providerBalance.Text = "$ " + Convert.ToDecimal(row["currentBalance"]).ToString("N2");
            SetProviderAdjustmentUsd(0m);
            _providerNotes.Clear();
            _providerActive.Checked = Convert.ToBoolean(row["isActive"]);
        }

        private void SaveService_Click(object sender, EventArgs e)
        {
            try
            {
                if (_serviceProvider.SelectedValue == null) throw new InvalidOperationException("Choose a provider.");
                string operatorName = _selectedType == MobileShopDb.ValidityRecharge || _selectedType == MobileShopDb.TransferBalance
                    ? _serviceOperator.Text : string.Empty;
                _serviceId = MobileShopDb.SaveService(_serviceId, _serviceDescription.Text, _selectedType,
                    Convert.ToInt32(_serviceProvider.SelectedValue), operatorName,
                    _serviceCost.Value, _serviceDeducted.Value,
                    _servicePrice.Value, _serviceActive.Checked);
                MessageBox.Show("Service saved. It is now available from the matching cashier button.", "Services", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadServices();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Service", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SaveProvider_Click(object sender, EventArgs e)
        {
            try
            {
                _providerId = MobileShopDb.SaveProvider(_providerId, _providerName.Text, _providerCode.Text,
                    _providerType.Text, 0m, _providerActive.Checked, _username);
                MessageBox.Show("Provider saved successfully.", "Provider", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReloadAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Provider", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ApplyBalance(bool add)
        {
            try
            {
                if (_providerId <= 0) throw new InvalidOperationException("Select a provider first.");
                if (_providerAdjustment.Value <= 0m) throw new InvalidOperationException("Enter a balance amount greater than zero.");
                decimal amount = add ? _providerAdjustment.Value : -_providerAdjustment.Value;
                MobileShopDb.AdjustProviderBalance(_providerId, amount, _username, _providerNotes.Text.Trim());
                SetProviderAdjustmentUsd(0m);
                _providerNotes.Clear();
                ReloadAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Provider Balance", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ProviderAdjustmentUsd_ValueChanged(object sender, EventArgs e)
        {
            if (_loading || _syncingProviderAdjustment) return;

            _syncingProviderAdjustment = true;
            try
            {
                _providerAdjustmentLbp.Value = ClampNumericValue(
                    _providerAdjustmentLbp,
                    ExchangeRateConfiguration.UsdToLbp(_providerAdjustment.Value));
            }
            finally
            {
                _syncingProviderAdjustment = false;
            }
        }

        private void ProviderAdjustmentLbp_ValueChanged(object sender, EventArgs e)
        {
            if (_loading || _syncingProviderAdjustment) return;

            _syncingProviderAdjustment = true;
            try
            {
                _providerAdjustment.Value = ClampNumericValue(
                    _providerAdjustment,
                    ExchangeRateConfiguration.LbpToUsd(_providerAdjustmentLbp.Value));
            }
            finally
            {
                _syncingProviderAdjustment = false;
            }
        }

        private void SetProviderAdjustmentUsd(decimal usd)
        {
            _syncingProviderAdjustment = true;
            try
            {
                _providerAdjustment.Value = ClampNumericValue(_providerAdjustment, usd);
                _providerAdjustmentLbp.Value = ClampNumericValue(
                    _providerAdjustmentLbp,
                    ExchangeRateConfiguration.UsdToLbp(_providerAdjustment.Value));
            }
            finally
            {
                _syncingProviderAdjustment = false;
            }
        }

        private void ServiceCostUsd_ValueChanged(object sender, EventArgs e)
        {
            if (!_loading && !_syncingServiceCost)
            {
                _syncingServiceCost = true;
                try
                {
                    _serviceCostLbp.Value = ClampNumericValue(
                        _serviceCostLbp,
                        ExchangeRateConfiguration.UsdToLbp(_serviceCost.Value));
                }
                finally
                {
                    _syncingServiceCost = false;
                }
            }

            RecalculateProfit(null, EventArgs.Empty);
        }

        private void ServiceCostLbp_ValueChanged(object sender, EventArgs e)
        {
            if (_loading || _syncingServiceCost) return;

            _syncingServiceCost = true;
            try
            {
                _serviceCost.Value = ClampNumericValue(
                    _serviceCost,
                    ExchangeRateConfiguration.LbpToUsd(_serviceCostLbp.Value));
            }
            finally
            {
                _syncingServiceCost = false;
            }

            RecalculateProfit(null, EventArgs.Empty);
        }

        private void SetServiceCostUsd(decimal usd)
        {
            _syncingServiceCost = true;
            try
            {
                _serviceCost.Value = ClampNumericValue(_serviceCost, usd);
                _serviceCostLbp.Value = ClampNumericValue(
                    _serviceCostLbp,
                    ExchangeRateConfiguration.UsdToLbp(_serviceCost.Value));
            }
            finally
            {
                _syncingServiceCost = false;
            }

            RecalculateProfit(null, EventArgs.Empty);
        }

        private void ServiceDeductedUsd_ValueChanged(object sender, EventArgs e)
        {
            if (_loading || _syncingServiceDeducted) return;

            _syncingServiceDeducted = true;
            try
            {
                _serviceDeductedLbp.Value = ClampNumericValue(
                    _serviceDeductedLbp,
                    ExchangeRateConfiguration.UsdToLbp(_serviceDeducted.Value));
            }
            finally
            {
                _syncingServiceDeducted = false;
            }
        }

        private void ServiceDeductedLbp_ValueChanged(object sender, EventArgs e)
        {
            if (_loading || _syncingServiceDeducted) return;

            _syncingServiceDeducted = true;
            try
            {
                _serviceDeducted.Value = ClampNumericValue(
                    _serviceDeducted,
                    ExchangeRateConfiguration.LbpToUsd(_serviceDeductedLbp.Value));
            }
            finally
            {
                _syncingServiceDeducted = false;
            }
        }

        private void SetServiceDeductedUsd(decimal usd)
        {
            _syncingServiceDeducted = true;
            try
            {
                _serviceDeducted.Value = ClampNumericValue(_serviceDeducted, usd);
                _serviceDeductedLbp.Value = ClampNumericValue(
                    _serviceDeductedLbp,
                    ExchangeRateConfiguration.UsdToLbp(_serviceDeducted.Value));
            }
            finally
            {
                _syncingServiceDeducted = false;
            }
        }

        private void ServicePriceUsd_ValueChanged(object sender, EventArgs e)
        {
            if (!_loading && !_syncingServicePrice)
            {
                _syncingServicePrice = true;
                try
                {
                    _servicePriceLbp.Value = ClampNumericValue(
                        _servicePriceLbp,
                        ExchangeRateConfiguration.UsdToLbp(_servicePrice.Value));
                }
                finally
                {
                    _syncingServicePrice = false;
                }
            }

            RecalculateProfit(null, EventArgs.Empty);
        }

        private void ServicePriceLbp_ValueChanged(object sender, EventArgs e)
        {
            if (_loading || _syncingServicePrice) return;

            _syncingServicePrice = true;
            try
            {
                _servicePrice.Value = ClampNumericValue(
                    _servicePrice,
                    ExchangeRateConfiguration.LbpToUsd(_servicePriceLbp.Value));
            }
            finally
            {
                _syncingServicePrice = false;
            }

            RecalculateProfit(null, EventArgs.Empty);
        }

        private void SetServicePriceUsd(decimal usd)
        {
            _syncingServicePrice = true;
            try
            {
                _servicePrice.Value = ClampNumericValue(_servicePrice, usd);
                _servicePriceLbp.Value = ClampNumericValue(
                    _servicePriceLbp,
                    ExchangeRateConfiguration.UsdToLbp(_servicePrice.Value));
            }
            finally
            {
                _syncingServicePrice = false;
            }

            RecalculateProfit(null, EventArgs.Empty);
        }

        private void RefreshExchangeRateDisplay()
        {
            decimal rate = ExchangeRateConfiguration.CurrentRate;
            lblServiceExchangeRate.Text = "Rate: 1 USD = " +
                ExchangeRateConfiguration.FormatRate(rate) + " LBP";
        }

        private static decimal ClampNumericValue(
            NumericUpDown control,
            decimal value)
        {
            if (value < control.Minimum) return control.Minimum;
            if (value > control.Maximum) return control.Maximum;
            return value;
        }

        private void RecalculateProfit(object sender, EventArgs e)
        {
            if (_selectedType != MobileShopDb.ValidityRecharge && _serviceDeducted != null)
                SetServiceDeductedUsd(_serviceCost.Value);

            decimal profit = _servicePrice.Value - _serviceCost.Value;
            _serviceProfit.Text = "$ " + profit.ToString("N2");
            _serviceProfit.ForeColor = profit >= 0m ? MobileUi.Green : MobileUi.Red;
            RefreshExchangeRateDisplay();
        }

        private void ClearService()
        {
            _serviceId = 0;
            _serviceDescription.Text = string.Empty;
            SetServiceCostUsd(0m);
            SetServiceDeductedUsd(0m);
            SetServicePriceUsd(0m);
            _serviceActive.Checked = true;
            if (_selectedType == MobileShopDb.ValidityRecharge) _serviceOperator.SelectedIndex = 0;
            RecalculateProfit(null, EventArgs.Empty);
            _serviceDescription.Focus();
        }

        private void ClearProvider()
        {
            _providerId = 0;
            _providerName.Clear();
            _providerCode.Clear();
            _providerType.SelectedIndex = 0;
            SetProviderAdjustmentUsd(0m);
            _providerNotes.Clear();
            _providerActive.Checked = true;
            _providerBalance.Text = "$ 0.00";
            _providerName.Focus();
        }

        private static void HideColumn(DataGridView grid, string name)
        {
            if (grid.Columns.Contains(name)) grid.Columns[name].Visible = false;
        }

        private static void FormatMoney(DataGridView grid, string name)
        {
            if (grid.Columns.Contains(name)) grid.Columns[name].DefaultCellStyle.Format = "N2";
        }

    }
}
