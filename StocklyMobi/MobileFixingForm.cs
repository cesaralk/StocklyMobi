using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace StocklyMobi
{
    public sealed partial class MobileFixingForm : Form
    {
        private string _cashier = "cashier";
        private DataRow _ticketRow;
        private int _editingFixId;
        private string _editingFixCode = string.Empty;

        public MobileFixingForm()
        {
            InitializeComponent();
        }

        public MobileFixingForm(string cashier) : this()
        {
            _cashier = string.IsNullOrWhiteSpace(cashier)
                ? "cashier"
                : cashier;
        }

        private void MobileFixingForm_Load(
            object sender,
            EventArgs e)
        {
            _estimated.Value = DateTime.Now.AddDays(1);
            SetEditMode(false);
            LoadFixes();
            BeginInvoke(
                (MethodInvoker)AdjustResponsiveLayout);
        }

        private void MobileFixingForm_Shown(
            object sender,
            EventArgs e)
        {
            AdjustResponsiveLayout();
        }

        private void MobileFixingForm_Resize(
            object sender,
            EventArgs e)
        {
            AdjustResponsiveLayout();
        }

        private void BtnClear_Click(
            object sender,
            EventArgs e)
        {
            ClearEntry();
        }

        private void BtnSave_Click(
            object sender,
            EventArgs e)
        {
            SaveFix(false, false);
        }

        private void BtnSavePreview_Click(
            object sender,
            EventArgs e)
        {
            SaveFix(true, false);
        }

        private void BtnSavePrint_Click(
            object sender,
            EventArgs e)
        {
            SaveFix(false, true);
        }

        private void Search_TextChanged(
            object sender,
            EventArgs e)
        {
            LoadFixes();
        }

        private void StatusFilter_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            ApplyStatusFilter();
        }

        private void BtnClearSearch_Click(
            object sender,
            EventArgs e)
        {
            _search.Clear();
            _statusFilter.SelectedIndex = 0;
        }

        private void BtnEditSelected_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                DataRow row = GetSelectedFullFix();

                _editingFixId =
                    Convert.ToInt32(row["id"]);

                _editingFixCode =
                    Convert.ToString(row["fixCode"]);

                _customerName.Text =
                    Convert.ToString(row["customerName"]);

                _phoneNumber.Text =
                    Convert.ToString(row["phoneNumber"]);

                _phoneModel.Text =
                    Convert.ToString(row["phoneModel"]);

                _problem.Text =
                    Convert.ToString(
                        row["problemDescription"]);

                SetNumericValue(
                    _costPrice,
                    Convert.ToDecimal(row["costPrice"]));

                SetNumericValue(
                    _finalPrice,
                    Convert.ToDecimal(row["finalPrice"]));

                _estimated.Value =
                    Convert.ToDateTime(
                        row["estimatedFinish"]);

                _ticketRow = row;
                SetEditMode(true);
                _problem.Focus();
                _problem.SelectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Edit Mobile Fix",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void BtnInProgress_Click(
            object sender,
            EventArgs e)
        {
            ChangeSelectedStatus("In Progress");
        }

        private void BtnReady_Click(
            object sender,
            EventArgs e)
        {
            ChangeSelectedStatus("Ready");
        }

        private void BtnCancelJob_Click(
            object sender,
            EventArgs e)
        {
            ChangeSelectedStatus("Cancelled");
        }

        private void PrintSelectedTicket_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                _ticketRow = GetSelectedFullFix();
                MobileFixTicketPrinter.PrintDirect(
                    _ticketRow);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.GetBaseException().Message,
                    "Print Fix Ticket",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void AdjustResponsiveLayout()
        {
            if (_split == null ||
                _split.ClientSize.Height <= 0)
            {
                return;
            }

            int available = _split.ClientSize.Height;

            int target = Math.Max(
                285,
                Math.Min(
                    365,
                    (int)(available * 0.46)));

            ResponsiveLayoutHelper.SetSafeSplitter(
                _split,
                target,
                270,
                240);
        }

        private void RecalculateProfit(
            object sender,
            EventArgs e)
        {
            decimal value =
                _finalPrice.Value -
                _costPrice.Value;

            _profit.Text =
                "$ " + value.ToString("N2");

            _profit.ForeColor =
                value >= 0m
                    ? MobileUi.Green
                    : MobileUi.Red;
        }

        private void SaveFix(
            bool preview,
            bool print)
        {
            try
            {
                bool editing = _editingFixId > 0;
                string code;

                if (editing)
                {
                    MobileShopDb.UpdateFixDetails(
                        _editingFixId,
                        _problem.Text,
                        _costPrice.Value,
                        _finalPrice.Value);

                    code = _editingFixCode;
                }
                else
                {
                    code = MobileShopDb.CreateFix(
                        _customerName.Text,
                        _phoneNumber.Text,
                        _phoneModel.Text,
                        _problem.Text,
                        _costPrice.Value,
                        _finalPrice.Value,
                        _estimated.Value,
                        _cashier);
                }

                _ticketRow =
                    MobileShopDb.GetFixByCode(code);

                if (_ticketRow == null)
                {
                    throw new InvalidOperationException(
                        "The fixing record could not be loaded.");
                }

                LoadFixes();

                if (preview)
                {
                    ShowPreview();
                }
                else if (print)
                {
                    MobileFixTicketPrinter.PrintDirect(
                        _ticketRow);
                }
                else
                {
                    MessageBox.Show(
                        editing
                            ? "Fixing job updated.\r\n\r\nFix ID: " + code
                            : "Fixing job saved.\r\n\r\nFix ID: " + code,
                        "Mobile Fixing",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                ClearEntry();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.GetBaseException().Message,
                    "Mobile Fixing",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void LoadFixes()
        {
            try
            {
                DataTable table =
                    MobileShopDb.GetFixes(
                        _search == null
                            ? string.Empty
                            : _search.Text);

                _grid.DataSource = table;

                if (_grid.Columns.Contains("id"))
                    _grid.Columns["id"].Visible = false;

                FormatMoney("Cost");
                FormatMoney("Final Price");
                FormatMoney("Profit");

                if (_grid.Columns.Contains("Problem"))
                    _grid.Columns["Problem"].FillWeight = 180;

                if (_grid.Columns.Contains("Fix ID"))
                    _grid.Columns["Fix ID"].FillWeight = 75;

                ApplyStatusFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Mobile Fixing",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ApplyStatusFilter()
        {
            DataTable table =
                _grid == null
                    ? null
                    : _grid.DataSource as DataTable;

            if (table == null ||
                _statusFilter == null)
            {
                return;
            }

            string status = _statusFilter.Text;

            table.DefaultView.RowFilter =
                status == "All Statuses" ||
                string.IsNullOrWhiteSpace(status)
                    ? string.Empty
                    : "[Status] = '" +
                      status.Replace("'", "''") +
                      "'";
        }

        private void FormatMoney(string name)
        {
            if (_grid.Columns.Contains(name))
            {
                _grid.Columns[name]
                    .DefaultCellStyle.Format = "N2";
            }
        }

        private void ChangeSelectedStatus(
            string status)
        {
            try
            {
                MobileShopDb.UpdateFixStatus(
                    GetSelectedFixId(),
                    status);

                LoadFixes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Mobile Fixing",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private int GetSelectedFixId()
        {
            DataRowView row = GetSelectedView();
            return Convert.ToInt32(row["id"]);
        }

        private DataRowView GetSelectedView()
        {
            if (_grid.CurrentRow == null)
            {
                throw new InvalidOperationException(
                    "Select a fixing job first.");
            }

            DataRowView row =
                _grid.CurrentRow.DataBoundItem
                as DataRowView;

            if (row == null)
            {
                throw new InvalidOperationException(
                    "Select a fixing job first.");
            }

            return row;
        }

        private DataRow GetSelectedFullFix()
        {
            DataRowView view = GetSelectedView();

            DataRow row =
                MobileShopDb.GetFixByCode(
                    Convert.ToString(view["Fix ID"]));

            if (row == null)
            {
                throw new InvalidOperationException(
                    "The selected fixing job was not found.");
            }

            return row;
        }

        private void PreviewSelectedTicket_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                _ticketRow = GetSelectedFullFix();
                ShowPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.GetBaseException().Message,
                    "Fix Ticket",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void ShowPreview()
        {
            if (_ticketRow == null)
            {
                throw new InvalidOperationException(
                    "No fixing ticket is available.");
            }

            MobileFixTicketPrinter.ShowPreview(
                this,
                _ticketRow);
        }

        private void SetEditMode(bool editing)
        {
            _customerName.ReadOnly = editing;
            _phoneNumber.ReadOnly = editing;
            _phoneModel.ReadOnly = editing;
            _estimated.Enabled = !editing;

            btnClear.Text =
                editing
                    ? "Cancel Edit / New"
                    : "Clear / New";

            btnSave.Text =
                editing
                    ? "Update"
                    : "Save";

            btnSavePreview.Text =
                editing
                    ? "Update + Preview"
                    : "Save + Preview";

            btnSavePrint.Text =
                editing
                    ? "Update + Print"
                    : "Save + Print";

            lblEditMode.Text =
                editing
                    ? "Editing " + _editingFixCode +
                      " — description and prices"
                    : "New fixing job";

            lblEditMode.ForeColor =
                editing
                    ? Color.FromArgb(124, 58, 237)
                    : Color.FromArgb(71, 85, 105);
        }

        private void ClearEntry()
        {
            _editingFixId = 0;
            _editingFixCode = string.Empty;
            _ticketRow = null;

            _customerName.Clear();
            _phoneNumber.Clear();
            _phoneModel.Clear();
            _problem.Clear();

            _costPrice.Value = 0m;
            _finalPrice.Value = 0m;
            _estimated.Value =
                DateTime.Now.AddDays(1);

            SetEditMode(false);
            _customerName.Focus();
        }

        private static void SetNumericValue(
            NumericUpDown control,
            decimal value)
        {
            if (value < control.Minimum)
                value = control.Minimum;

            if (value > control.Maximum)
                value = control.Maximum;

            control.Value = value;
        }
    }
}
