using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace StocklyMobi
{
    public sealed partial class ReceiptHistoryForm : Form
    {
        private readonly DBConnect _db = new DBConnect();
        private string _selectedTransNo = string.Empty;
        public ReceiptHistoryForm() { InitializeComponent(); }
        private void ReceiptHistoryForm_Load(object sender, EventArgs e)
        {
            _from.Value = DateTime.Today;
            _to.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
            LoadReceiptSuggestions();
            LoadReceipts();
            BeginInvoke((MethodInvoker)AdjustLayout);
        }
        private void ReceiptHistoryForm_Shown(object sender, EventArgs e) { AdjustLayout(); }
        private void ReceiptHistoryForm_Resize(object sender, EventArgs e) { AdjustLayout(); }
        private void ReceiptNo_KeyDown(object sender, KeyEventArgs e) { if(e.KeyCode==Keys.Enter){e.SuppressKeyPress=true;LoadReceipts();} }
        private void BtnSearch_Click(object sender, EventArgs e) { LoadReceipts(); }
        private void BtnToday_Click(object sender, EventArgs e) { _from.Value=DateTime.Today; _to.Value=DateTime.Today.AddDays(1).AddSeconds(-1); LoadReceipts(); }
        private void BtnAll_Click(object sender, EventArgs e) { _receiptNo.Clear(); _from.Value=new DateTime(2000,1,1); _to.Value=DateTime.Now.AddMinutes(1); LoadReceipts(); }
        private void Receipts_SelectionChanged(object sender, EventArgs e) { LoadSelectedReceipt(); }
        private void Receipts_DoubleClick(object sender, EventArgs e) { if(!string.IsNullOrWhiteSpace(_selectedTransNo)) HistoricalReceiptPrinter.ShowPreview(this,_selectedTransNo); }
        private void BtnPrint_Click(object sender, EventArgs e) { if(!string.IsNullOrWhiteSpace(_selectedTransNo)) HistoricalReceiptPrinter.Print(_selectedTransNo); }
        private void BtnPreview_Click(object sender, EventArgs e) { if(!string.IsNullOrWhiteSpace(_selectedTransNo)) HistoricalReceiptPrinter.ShowPreview(this,_selectedTransNo); }
        private void LoadReceiptSuggestions()
        {
            try
            {
                AutoCompleteStringCollection source = new AutoCompleteStringCollection();
                using (SqlConnection con = new SqlConnection(_db.myConnection()))
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1000 transno FROM dbo.tbReceiptHeader ORDER BY soldAt DESC", con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) source.Add(Convert.ToString(reader[0]));
                    }
                }
                _receiptNo.AutoCompleteCustomSource = source;
            }
            catch
            {
                // The search screen remains usable even if suggestions cannot be loaded.
            }
        }

        private void LoadReceipts()
        {
            try
            {
                if (_to.Value < _from.Value)
                    throw new InvalidOperationException("The end date/time must be after the start date/time.");

                string receipt = _receiptNo.Text.Trim();
                using (SqlConnection con = new SqlConnection(_db.myConnection()))
                using (SqlCommand cmd = new SqlCommand(@"
SELECT h.transno AS [Receipt Number],h.soldAt AS [Date / Time],h.cashier AS [Cashier],
       h.subtotal AS [Subtotal],h.lineDiscount AS [Line Discount],
       h.transactionDiscount AS [Transaction Discount],h.netTotal AS [Net Total],
       ISNULL(r.returnedTotal,0) AS [Returned],h.status AS [Status]
FROM dbo.tbReceiptHeader h
OUTER APPLY
(
    SELECT SUM(ISNULL(x.total,0)) returnedTotal
    FROM dbo.tbCancel x
    WHERE x.transno=h.transno
) r
WHERE (@receipt<>'' AND h.transno LIKE @like)
   OR (@receipt='' AND h.soldAt>=@from AND h.soldAt<=@to)
ORDER BY CASE WHEN h.transno=@receipt THEN 0 ELSE 1 END,h.soldAt DESC,h.transno DESC", con))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.Add("@from", SqlDbType.DateTime).Value = _from.Value;
                    cmd.Parameters.Add("@to", SqlDbType.DateTime).Value = _to.Value;
                    cmd.Parameters.Add("@receipt", SqlDbType.VarChar, 50).Value = receipt;
                    cmd.Parameters.Add("@like", SqlDbType.VarChar, 70).Value = "%" + receipt + "%";
                    DataTable table = new DataTable();
                    da.Fill(table);
                    _receipts.DataSource = table;
                    FormatMoney(_receipts, "Subtotal", "Line Discount", "Transaction Discount", "Net Total", "Returned");
                    if (_receipts.Columns.Contains("Receipt Number")) _receipts.Columns["Receipt Number"].FillWeight = 125;
                    if (_receipts.Columns.Contains("Date / Time")) _receipts.Columns["Date / Time"].DefaultCellStyle.Format = "dd/MM/yyyy hh:mm:ss tt";
                }
                LoadSelectedReceipt();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Receipt Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadSelectedReceipt()
        {
            _selectedTransNo = string.Empty;
            _lines.DataSource = null;
            _summary.Text = "Select a receipt";
            if (_receipts.CurrentRow == null || _receipts.CurrentRow.Cells["Receipt Number"].Value == null) return;

            try
            {
                _selectedTransNo = Convert.ToString(_receipts.CurrentRow.Cells["Receipt Number"].Value);
                using (SqlConnection con = new SqlConnection(_db.myConnection()))
                using (SqlCommand cmd = new SqlCommand(@"
SELECT [lineNo] AS [No],itemType AS [Type],pcode AS [Code],description AS [Description],
       price AS [Price],qty AS [Qty],lineDiscount AS [Line Discount],lineTotal AS [Line Total]
FROM dbo.tbReceiptLine
WHERE transno=@transno
ORDER BY [lineNo],id", con))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.Add("@transno", SqlDbType.VarChar, 50).Value = _selectedTransNo;
                    DataTable lines = new DataTable();
                    da.Fill(lines);
                    _lines.DataSource = lines;
                    FormatMoney(_lines, "Price", "Line Discount", "Line Total");
                    if (_lines.Columns.Contains("Description")) _lines.Columns["Description"].FillWeight = 190;
                }

                decimal subtotal = ToDecimal(_receipts.CurrentRow.Cells["Subtotal"].Value);
                decimal lineDiscount = ToDecimal(_receipts.CurrentRow.Cells["Line Discount"].Value);
                decimal transactionDiscount = ToDecimal(_receipts.CurrentRow.Cells["Transaction Discount"].Value);
                decimal netTotal = ToDecimal(_receipts.CurrentRow.Cells["Net Total"].Value);
                string status = Convert.ToString(_receipts.CurrentRow.Cells["Status"].Value);
                _summary.Text = "Receipt " + _selectedTransNo + "    Subtotal: $ " + subtotal.ToString("N2") +
                    "    Line discount: $ " + lineDiscount.ToString("N2") +
                    "    Transaction discount: $ " + transactionDiscount.ToString("N2") +
                    "    Net: $ " + netTotal.ToString("N2") + "    " + status;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Receipt Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AdjustLayout()
        {
            if (_split == null || tblFooter == null) return;

            bool compactHeight = ClientSize.Height < 720;
            lblHeader.Height = compactHeight ? 54 : 72;
            lblHeader.Padding = compactHeight
                ? new Padding(16, 7, 8, 2)
                : new Padding(18, 10, 10, 4);

            pnlFilters.Height = compactHeight ? 82 : 98;
            pnlFilters.Padding = compactHeight ? new Padding(10) : new Padding(14);

            tblFooter.Height = compactHeight ? 76 : 92;
            flowActions.Padding = new Padding(0, compactHeight ? 8 : 18, 0, 0);
            flowActions.WrapContents = false;
            flowActions.AutoScroll = true;

            int actionWidth = pnlDetails.ClientSize.Width < 620 ? 255 : 320;
            if (tblFooter.ColumnStyles.Count > 1)
                tblFooter.ColumnStyles[1].Width = actionWidth;

            bool sideBySide = ClientSize.Width >= 1040 && _split.ClientSize.Height >= 330;
            Orientation desired = sideBySide
                ? Orientation.Vertical
                : Orientation.Horizontal;

            if (_split.Orientation != desired)
            {
                _split.Panel1MinSize = 0;
                _split.Panel2MinSize = 0;
                _split.Orientation = desired;
            }

            int total = sideBySide
                ? _split.ClientSize.Width
                : _split.ClientSize.Height;

            int preferred = (int)(total * (sideBySide ? 0.44 : 0.40));
            int firstMinimum = sideBySide ? 340 : 180;
            int secondMinimum = sideBySide ? 390 : Math.Max(210, tblFooter.Height + 100);

            ResponsiveLayoutHelper.SetSafeSplitter(
                _split,
                preferred,
                firstMinimum,
                secondMinimum);

            tblFooter.BringToFront();
            btnPrint.BringToFront();
            btnPreview.BringToFront();
        }

        private static DateTimePicker DateTimeBox(DateTime value)
        {
            return new DateTimePicker
            {
                Value = value,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy hh:mm:ss tt",
                ShowUpDown = true,
                Font = new Font("Century Gothic", 9.5F),
                Dock = DockStyle.Fill
            };
        }

        private static Label Caption(string text)
        {
            Label label = MobileUi.Label(text);
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.MiddleLeft;
            return label;
        }

        private static void FormatMoney(DataGridView grid, params string[] columns)
        {
            foreach (string name in columns)
                if (grid.Columns.Contains(name)) grid.Columns[name].DefaultCellStyle.Format = "N2";
        }

        private static decimal ToDecimal(object value)
        {
            return value == null || value == DBNull.Value ? 0m : Convert.ToDecimal(value);
        }

    }
}
