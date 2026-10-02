using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StocklyMobi
{
    public sealed partial class InvoiceHistoryForm : Form
    {
        private readonly DBConnect _db = new DBConnect();
        private int _selectedInvoiceId;
        private bool _layoutPending;
        private bool _adjustingLayout;

        public InvoiceHistoryForm() { InitializeComponent(); }

        //private void InvoiceHistoryForm_Load(object sender, EventArgs e)
        //{
        //    _from.Value = DateTime.Today.AddMonths(-1);
        //    _to.Value = DateTime.Today;
        //    LoadSearchSuggestions();
        //    LoadInvoices();
        //    BeginInvoke((MethodInvoker)AdjustLayout);
        //}
        //private void InvoiceHistoryForm_Shown(object sender, EventArgs e) { AdjustLayout(); }
        //private void InvoiceHistoryForm_Resize(object sender, EventArgs e) { AdjustLayout(); }
        private void InvoiceHistoryForm_Load(object sender, EventArgs e) {
            _from.Value = DateTime.Today.AddMonths(-1);
            _to.Value = DateTime.Today;

            LoadSearchSuggestions();
            LoadInvoices();

            QueueAdjustLayout();
        }

        private void InvoiceHistoryForm_Shown(object sender, EventArgs e) {
            QueueAdjustLayout();
        }

        private void InvoiceHistoryForm_Resize(object sender, EventArgs e) {
            QueueAdjustLayout();
        }

        private void QueueAdjustLayout() {
            if (_layoutPending ||
                IsDisposed ||
                Disposing ||
                !IsHandleCreated ||
                WindowState == FormWindowState.Minimized) {
                return;
            }

            _layoutPending = true;

            BeginInvoke((MethodInvoker)delegate
            {
                _layoutPending = false;

                if (!IsDisposed && !Disposing)
                    AdjustLayout();
            });
        }
        private void Search_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; LoadInvoices(); } }
        private void BtnFind_Click(object sender, EventArgs e) { LoadInvoices(); }
        private void BtnAll_Click(object sender, EventArgs e)
        {
            _search.Clear();
            _supplierSearch.Clear();
            _from.Value = new DateTime(2000, 1, 1);
            _to.Value = DateTime.Today;
            LoadInvoices();
        }
        private void Invoices_SelectionChanged(object sender, EventArgs e) { LoadSelectedInvoice(); }
        private void Invoices_DoubleClick(object sender, EventArgs e) { if (_selectedInvoiceId > 0) StockInvoicePrinter.ShowPreview(this,_selectedInvoiceId); }
        private void BtnPrint_Click(object sender, EventArgs e) { if (_selectedInvoiceId > 0) StockInvoicePrinter.Print(_selectedInvoiceId); }
        private void BtnPreview_Click(object sender, EventArgs e) { if (_selectedInvoiceId > 0) StockInvoicePrinter.ShowPreview(this,_selectedInvoiceId); }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedInvoiceId <= 0 || _invoices.CurrentRow == null)
            {
                MessageBox.Show(
                    "Select an invoice to delete.",
                    "Delete Invoice",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            string displayedInvoiceNo = Convert.ToString(
                _invoices.CurrentRow.Cells["Supplier Invoice No."].Value);

            string warning =
                "Permanently delete stock-in invoice " + displayedInvoiceNo + "?\r\n\r\n" +
                "This will subtract every quantity in this invoice from the current product stock, " +
                "then delete the invoice lines and header. This action cannot be undone.\r\n\r\n" +
                "The deletion will be blocked if it would make any product quantity negative.";

            if (MessageBox.Show(
                    warning,
                    "Delete Invoice",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            int deletedLineCount = 0;
            int affectedProductCount = 0;
            int reversedQuantity = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(_db.myConnection()))
                {
                    con.Open();

                    using (SqlTransaction tr = con.BeginTransaction(IsolationLevel.Serializable))
                    {
                        try
                        {
                            string invoiceNo;

                            using (SqlCommand headerCmd = new SqlCommand(@"
SELECT invoiceNo
FROM dbo.tbStockInvoice WITH (UPDLOCK,HOLDLOCK)
WHERE id=@id", con, tr))
                            {
                                headerCmd.Parameters.Add("@id", SqlDbType.Int).Value = _selectedInvoiceId;
                                object headerValue = headerCmd.ExecuteScalar();

                                if (headerValue == null || headerValue == DBNull.Value)
                                    throw new InvalidOperationException("The selected invoice no longer exists.");

                                invoiceNo = Convert.ToString(headerValue);
                            }

                            using (SqlCommand invalidStockCmd = new SqlCommand(@"
SELECT TOP 1
       p.pcode,
       p.pdesc,
       p.qty AS CurrentQty,
       x.InvoiceQty
FROM dbo.tbProduct p WITH (UPDLOCK,HOLDLOCK)
INNER JOIN
(
    SELECT pcode, SUM(ISNULL(qty,0)) AS InvoiceQty
    FROM dbo.tbStockIn WITH (UPDLOCK,HOLDLOCK)
    WHERE invoiceId=@id
       OR (invoiceId IS NULL AND refno=@invoiceNo)
    GROUP BY pcode
) x ON x.pcode=p.pcode
WHERE p.qty < x.InvoiceQty
ORDER BY p.pcode", con, tr))
                            {
                                invalidStockCmd.Parameters.Add("@id", SqlDbType.Int).Value = _selectedInvoiceId;
                                invalidStockCmd.Parameters.Add("@invoiceNo", SqlDbType.VarChar, 50).Value = invoiceNo;

                                using (SqlDataReader reader = invalidStockCmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        string product = Convert.ToString(reader["pdesc"]);
                                        if (string.IsNullOrWhiteSpace(product))
                                            product = Convert.ToString(reader["pcode"]);

                                        int currentQty = Convert.ToInt32(reader["CurrentQty"]);
                                        int invoiceQty = Convert.ToInt32(reader["InvoiceQty"]);

                                        throw new InvalidOperationException(
                                            "Invoice " + invoiceNo + " cannot be deleted because product " + product +
                                            " currently has quantity " + currentQty +
                                            ", while this invoice added " + invoiceQty + ".\r\n\r\n" +
                                            "Deleting it would create negative stock. Correct the stock or return the sold quantity first.");
                                    }
                                }
                            }

                            using (SqlCommand statsCmd = new SqlCommand(@"
SELECT COUNT(*) AS LineCount,
       COUNT(DISTINCT pcode) AS ProductCount,
       ISNULL(SUM(ISNULL(qty,0)),0) AS TotalQty
FROM dbo.tbStockIn WITH (UPDLOCK,HOLDLOCK)
WHERE invoiceId=@id
   OR (invoiceId IS NULL AND refno=@invoiceNo)", con, tr))
                            {
                                statsCmd.Parameters.Add("@id", SqlDbType.Int).Value = _selectedInvoiceId;
                                statsCmd.Parameters.Add("@invoiceNo", SqlDbType.VarChar, 50).Value = invoiceNo;

                                using (SqlDataReader reader = statsCmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        deletedLineCount = Convert.ToInt32(reader["LineCount"]);
                                        affectedProductCount = Convert.ToInt32(reader["ProductCount"]);
                                        reversedQuantity = Convert.ToInt32(reader["TotalQty"]);
                                    }
                                }
                            }

                            using (SqlCommand reverseCmd = new SqlCommand(@"
UPDATE p
SET p.qty=p.qty-x.InvoiceQty
FROM dbo.tbProduct p
INNER JOIN
(
    SELECT pcode, SUM(ISNULL(qty,0)) AS InvoiceQty
    FROM dbo.tbStockIn
    WHERE invoiceId=@id
       OR (invoiceId IS NULL AND refno=@invoiceNo)
    GROUP BY pcode
) x ON x.pcode=p.pcode", con, tr))
                            {
                                reverseCmd.Parameters.Add("@id", SqlDbType.Int).Value = _selectedInvoiceId;
                                reverseCmd.Parameters.Add("@invoiceNo", SqlDbType.VarChar, 50).Value = invoiceNo;
                                reverseCmd.ExecuteNonQuery();
                            }

                            using (SqlCommand deleteLinesCmd = new SqlCommand(@"
DELETE FROM dbo.tbStockIn
WHERE invoiceId=@id
   OR (invoiceId IS NULL AND refno=@invoiceNo)", con, tr))
                            {
                                deleteLinesCmd.Parameters.Add("@id", SqlDbType.Int).Value = _selectedInvoiceId;
                                deleteLinesCmd.Parameters.Add("@invoiceNo", SqlDbType.VarChar, 50).Value = invoiceNo;
                                int actualDeletedLines = deleteLinesCmd.ExecuteNonQuery();

                                if (actualDeletedLines != deletedLineCount)
                                    throw new InvalidOperationException("The invoice lines changed while the invoice was being deleted. Please try again.");
                            }

                            using (SqlCommand deleteHeaderCmd = new SqlCommand(
                                "DELETE FROM dbo.tbStockInvoice WHERE id=@id", con, tr))
                            {
                                deleteHeaderCmd.Parameters.Add("@id", SqlDbType.Int).Value = _selectedInvoiceId;

                                if (deleteHeaderCmd.ExecuteNonQuery() != 1)
                                    throw new InvalidOperationException("The invoice header could not be deleted.");
                            }

                            tr.Commit();
                        }
                        catch
                        {
                            tr.Rollback();
                            throw;
                        }
                    }
                }

                _selectedInvoiceId = 0;
                _lines.DataSource = null;
                _selectedInfo.Text = "Select an invoice";
                LoadSearchSuggestions();
                LoadInvoices();

                MessageBox.Show(
                    "Invoice deleted successfully.\r\n\r\n" +
                    "Deleted lines: " + deletedLineCount +
                    "\r\nAffected products: " + affectedProductCount +
                    "\r\nStock quantity reversed: " + reversedQuantity +
                    "\r\n\r\nA product can now be deleted only when its quantity is zero and it has no sales, receipt, adjustment, or cancellation history.",
                    "Delete Invoice",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.GetBaseException().Message,
                    "Delete Invoice",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        //private void AdjustLayout()
        //{
        //    if (_split == null) return;
        //    bool wide = ClientSize.Width >= 1080;
        //    Orientation desired = wide ? Orientation.Vertical : Orientation.Horizontal;
        //    if (_split.Orientation != desired) { _split.Panel1MinSize=0; _split.Panel2MinSize=0; _split.Orientation=desired; }
        //    int total = wide ? _split.ClientSize.Width : _split.ClientSize.Height;
        //    ResponsiveLayoutHelper.SetSafeSplitter(_split,(int)(total*(wide?0.43:0.45)),wide?340:220,wide?420:250);
        //}
        private void AdjustLayout() {
            if (_adjustingLayout ||
                _split == null ||
                _split.IsDisposed ||
                !_split.IsHandleCreated ||
                WindowState == FormWindowState.Minimized) {
                return;
            }

            int width = _split.ClientSize.Width;
            int height = _split.ClientSize.Height;

            // The SplitContainer can temporarily have a very small size
            // while the form is opening, minimizing or changing DPI.
            if (width <= _split.SplitterWidth + 4 ||
                height <= _split.SplitterWidth + 4) {
                return;
            }

            _adjustingLayout = true;
            _split.SuspendLayout();

            try {
                bool wide = ClientSize.Width >= 1080;

                Orientation desired = wide
                    ? Orientation.Vertical
                    : Orientation.Horizontal;

                // Remove minimum restrictions before changing orientation.
                _split.Panel1MinSize = 0;
                _split.Panel2MinSize = 0;

                if (_split.Orientation != desired) {
                    int currentTotal =
                        _split.Orientation == Orientation.Vertical
                            ? width
                            : height;

                    int targetTotal =
                        desired == Orientation.Vertical
                            ? width
                            : height;

                    // Make the current distance valid for both orientations
                    // before changing the orientation.
                    int transitionMaximum =
                        Math.Min(currentTotal, targetTotal)
                        - _split.SplitterWidth
                        - 1;

                    if (transitionMaximum < 1)
                        return;

                    int transitionDistance = _split.SplitterDistance;

                    if (transitionDistance < 1)
                        transitionDistance = 1;

                    if (transitionDistance > transitionMaximum)
                        transitionDistance = transitionMaximum;

                    _split.SplitterDistance = transitionDistance;
                    _split.Orientation = desired;
                }

                int total =
                    desired == Orientation.Vertical
                        ? _split.ClientSize.Width
                        : _split.ClientSize.Height;

                if (total <= _split.SplitterWidth + 2)
                    return;

                ResponsiveLayoutHelper.SetSafeSplitter(
                    _split,
                    (int)(total * (wide ? 0.43 : 0.45)),
                    wide ? 340 : 220,
                    wide ? 420 : 250);
            } catch (ArgumentOutOfRangeException) {
                // Ignore temporary invalid dimensions while Windows is resizing.
                // The next queued layout pass will apply the correct dimensions.
            } finally {
                _split.ResumeLayout();
                _adjustingLayout = false;
            }
        }
        private void LoadSearchSuggestions()
        {
            try
            {
                AutoCompleteStringCollection invoiceSource = new AutoCompleteStringCollection();
                AutoCompleteStringCollection supplierSource = new AutoCompleteStringCollection();

                using (SqlConnection con = new SqlConnection(_db.myConnection()))
                {
                    con.Open();

                    using (SqlCommand invoiceCmd = new SqlCommand(@"
SELECT TOP 500 CONVERT(NVARCHAR(150), i.invoiceNo)
FROM dbo.tbStockInvoice i
WHERE ISNULL(i.invoiceNo,'')<>''
GROUP BY i.invoiceNo
ORDER BY MAX(i.invoiceDate) DESC, CONVERT(NVARCHAR(150), i.invoiceNo)", con))
                    using (SqlDataReader reader = invoiceCmd.ExecuteReader())
                    {
                        while (reader.Read())
                            invoiceSource.Add(Convert.ToString(reader[0]));
                    }

                    using (SqlCommand supplierCmd = new SqlCommand(@"
SELECT TOP 500 s.supplier
FROM dbo.tbStockInvoice i
INNER JOIN dbo.tbSupplier s ON s.id=i.supplierId
WHERE ISNULL(s.supplier,N'')<>N''
GROUP BY s.supplier
ORDER BY MAX(i.invoiceDate) DESC, s.supplier", con))
                    using (SqlDataReader reader = supplierCmd.ExecuteReader())
                    {
                        while (reader.Read())
                            supplierSource.Add(Convert.ToString(reader[0]));
                    }
                }

                _search.AutoCompleteCustomSource = invoiceSource;
                _supplierSearch.AutoCompleteCustomSource = supplierSource;
            }
            catch
            {
                // Suggestions are optional; both searches still work if they cannot be loaded.
            }
        }

        private void LoadInvoices()
        {
            try
            {
                DateTime from = _from.Value.Date;
                DateTime to = _to.Value.Date.AddDays(1);
                string invoiceSearch = _search.Text.Trim();
                string supplierSearch = _supplierSearch.Text.Trim();

                using (SqlConnection con = new SqlConnection(_db.myConnection()))
                using (SqlCommand cmd = new SqlCommand(@"
SELECT i.id AS [Id],i.invoiceNo AS [Supplier Invoice No.],i.invoiceDate AS [Invoice Date],
       s.supplier AS [Supplier],i.totalCost AS [Total Cost],i.createdBy AS [Entered By],i.createdAt AS [Saved At],i.status AS [Status]
FROM dbo.tbStockInvoice i LEFT JOIN dbo.tbSupplier s ON s.id=i.supplierId
WHERE i.invoiceDate>=@from AND i.invoiceDate<@to
  AND (@invoiceSearch=N'' OR CONVERT(NVARCHAR(150),i.invoiceNo) LIKE @invoiceLike)
  AND (@supplierSearch=N'' OR ISNULL(s.supplier,N'') LIKE @supplierLike)
ORDER BY i.invoiceDate DESC,i.id DESC", con))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.Add("@from", SqlDbType.DateTime).Value = from;
                    cmd.Parameters.Add("@to", SqlDbType.DateTime).Value = to;
                    cmd.Parameters.Add("@invoiceSearch", SqlDbType.NVarChar, 150).Value = invoiceSearch;
                    cmd.Parameters.Add("@invoiceLike", SqlDbType.NVarChar, 170).Value = "%" + invoiceSearch + "%";
                    cmd.Parameters.Add("@supplierSearch", SqlDbType.NVarChar, 150).Value = supplierSearch;
                    cmd.Parameters.Add("@supplierLike", SqlDbType.NVarChar, 170).Value = "%" + supplierSearch + "%";

                    DataTable table = new DataTable();
                    da.Fill(table);
                    _invoices.DataSource = table;

                    if (_invoices.Columns.Contains("Id")) _invoices.Columns["Id"].Visible = false;
                    if (_invoices.Columns.Contains("Total Cost")) _invoices.Columns["Total Cost"].DefaultCellStyle.Format = "N2";
                    if (_invoices.Columns.Contains("Supplier Invoice No.")) _invoices.Columns["Supplier Invoice No."].FillWeight = 130;
                }

                LoadSelectedInvoice();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Invoice History", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadSelectedInvoice()
        {
            try
            {
                _selectedInvoiceId=0; _lines.DataSource=null; _selectedInfo.Text="Select an invoice"; btnDelete.Enabled=false;
                if(_invoices.CurrentRow==null || _invoices.CurrentRow.Cells["Id"].Value==null) return;
                _selectedInvoiceId=Convert.ToInt32(_invoices.CurrentRow.Cells["Id"].Value); btnDelete.Enabled=true;
                using(SqlConnection con=new SqlConnection(_db.myConnection()))
                using(SqlCommand cmd=new SqlCommand(@"
SELECT si.pcode AS [Code],p.barcode AS [Barcode],p.pdesc AS [Description],si.qty AS [Qty],
       ISNULL(si.CostPrice,0) AS [Cost],ISNULL(si.Markup,0) AS [Markup %],ISNULL(si.Price,0) AS [Selling Price],
       CONVERT(DECIMAL(18,4),si.qty*ISNULL(si.CostPrice,0)) AS [Line Cost]
FROM dbo.tbStockIn si LEFT JOIN dbo.tbProduct p ON p.pcode=si.pcode
WHERE si.invoiceId=@id OR (si.invoiceId IS NULL AND si.refno=(SELECT invoiceNo FROM dbo.tbStockInvoice WHERE id=@id))
ORDER BY si.id",con))
                using(SqlDataAdapter da=new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.Add("@id",SqlDbType.Int).Value=_selectedInvoiceId; DataTable lines=new DataTable(); da.Fill(lines); _lines.DataSource=lines;
                    foreach(string name in new[] { "Cost", "Markup %", "Selling Price", "Line Cost" }) if(_lines.Columns.Contains(name)) _lines.Columns[name].DefaultCellStyle.Format="N2";
                    if(_lines.Columns.Contains("Description")) _lines.Columns["Description"].FillWeight=180;
                }
                string invoiceNo=Convert.ToString(_invoices.CurrentRow.Cells["Supplier Invoice No."].Value);
                string total=Convert.ToDecimal(_invoices.CurrentRow.Cells["Total Cost"].Value).ToString("N2");
                _selectedInfo.Text="Supplier invoice "+invoiceNo+"     Total: $ "+total;
            }
            catch(Exception ex){ MessageBox.Show(ex.Message,"Invoice Details",MessageBoxButtons.OK,MessageBoxIcon.Warning); }
        }
    }
}
