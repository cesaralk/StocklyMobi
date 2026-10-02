using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace StocklyMobi
{
    internal sealed class StockInvoicePrinter
    {
        private readonly DBConnect _db = new DBConnect();
        private readonly int _invoiceId;
        private DataRow _header;
        private DataTable _lines;
        private int _lineIndex;

        private StockInvoicePrinter(int invoiceId)
        {
            _invoiceId = invoiceId;
            LoadData();
        }

        public static void ShowPreview(IWin32Window owner, int invoiceId)
        {
            try
            {
                StockInvoicePrinter printer = new StockInvoicePrinter(invoiceId);
                PrintDocument document = printer.CreateDocument();
                using (PrintPreviewDialog dialog = new PrintPreviewDialog())
                {
                    dialog.Document = document;
                    dialog.Width = 1100;
                    dialog.Height = 760;
                    dialog.ShowDialog(owner);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Invoice Print Preview", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public static void Print(int invoiceId)
        {
            try
            {
                StockInvoicePrinter printer = new StockInvoicePrinter(invoiceId);
                using (PrintDocument document = printer.CreateDocument()) document.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Print Invoice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private PrintDocument CreateDocument()
        {
            PrintDocument document = new PrintDocument();
            PrinterConfiguration.ApplyInvoicePrinter(document);
            document.DocumentName = "Stock-In Invoice " + Convert.ToString(_header["invoiceNo"]);
            document.DefaultPageSettings.Margins = new Margins(45, 45, 45, 45);
            document.BeginPrint += delegate { _lineIndex = 0; };
            document.PrintPage += PrintPage;
            return document;
        }

        private void LoadData()
        {
            using (SqlConnection con = new SqlConnection(_db.myConnection()))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(@"
SELECT i.id,i.invoiceNo,i.invoiceDate,i.createdAt,i.createdBy,i.totalCost,i.status,
       ISNULL(s.supplier,'') supplier,ISNULL(s.address,'') supplierAddress,ISNULL(s.phone,'') supplierPhone,
       ISNULL(st.store,'') store,ISNULL(st.address,'') storeAddress
FROM dbo.tbStockInvoice i
LEFT JOIN dbo.tbSupplier s ON s.id=i.supplierId
OUTER APPLY(SELECT TOP 1 store,address FROM dbo.tbStore ORDER BY id) st
WHERE i.id=@id", con))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = _invoiceId;
                    DataTable header = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd)) da.Fill(header);
                    if (header.Rows.Count == 0) throw new InvalidOperationException("The stock-in invoice was not found.");
                    _header = header.Rows[0];
                }

                using (SqlCommand cmd = new SqlCommand(@"
SELECT si.pcode,p.barcode,p.pdesc,si.qty,ISNULL(si.CostPrice,0) CostPrice,
       ISNULL(si.Markup,0) Markup,ISNULL(si.Price,0) Price,
       CONVERT(DECIMAL(18,4),si.qty*ISNULL(si.CostPrice,0)) LineCost
FROM dbo.tbStockIn si
LEFT JOIN dbo.tbProduct p ON p.pcode=si.pcode
WHERE si.invoiceId=@id OR (si.invoiceId IS NULL AND si.refno=(SELECT invoiceNo FROM dbo.tbStockInvoice WHERE id=@id))
ORDER BY si.id", con))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = _invoiceId;
                    _lines = new DataTable();
                    da.Fill(_lines);
                }
            }
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle area = e.MarginBounds;
            float y = area.Top;
            Font title = new Font("Arial", 16F, FontStyle.Bold);
            Font heading = new Font("Arial", 10F, FontStyle.Bold);
            Font normal = new Font("Arial", 9F);
            Font small = new Font("Arial", 8F);
            Pen linePen = Pens.Black;

            string store = Convert.ToString(_header["store"]);
            g.DrawString(string.IsNullOrWhiteSpace(store) ? "STOCK-IN INVOICE" : store, title, Brushes.Black, area.Left, y);
            y += 25;
            if (!string.IsNullOrWhiteSpace(Convert.ToString(_header["storeAddress"])))
            {
                g.DrawString(Convert.ToString(_header["storeAddress"]), small, Brushes.Black, area.Left, y);
                y += 17;
            }
            g.DrawString("STOCK-IN INVOICE", heading, Brushes.Black, area.Right - 150, area.Top);
            y += 10;
            g.DrawLine(linePen, area.Left, y, area.Right, y);
            y += 12;

            g.DrawString("Supplier Invoice No: " + Convert.ToString(_header["invoiceNo"]), heading, Brushes.Black, area.Left, y);
            g.DrawString("Invoice Date: " + Convert.ToDateTime(_header["invoiceDate"]).ToString("dd/MM/yyyy hh:mm tt"), normal, Brushes.Black, area.Left + 300, y);
            y += 22;
            g.DrawString("Supplier: " + Convert.ToString(_header["supplier"]), normal, Brushes.Black, area.Left, y);
            g.DrawString("Entered By: " + Convert.ToString(_header["createdBy"]), normal, Brushes.Black, area.Left + 300, y);
            y += 22;
            g.DrawString("Saved At: " + Convert.ToDateTime(_header["createdAt"]).ToString("dd/MM/yyyy hh:mm:ss tt"), normal, Brushes.Black, area.Left, y);
            y += 28;

            float xCode = area.Left;
            float xDescription = area.Left + 90;
            float xQty = area.Right - 245;
            float xCost = area.Right - 175;
            float xTotal = area.Right - 85;
            g.DrawString("Code", heading, Brushes.Black, xCode, y);
            g.DrawString("Description", heading, Brushes.Black, xDescription, y);
            g.DrawString("Qty", heading, Brushes.Black, xQty, y);
            g.DrawString("Cost", heading, Brushes.Black, xCost, y);
            g.DrawString("Total", heading, Brushes.Black, xTotal, y);
            y += 19;
            g.DrawLine(linePen, area.Left, y, area.Right, y);
            y += 7;

            while (_lineIndex < _lines.Rows.Count)
            {
                if (y > area.Bottom - 80)
                {
                    e.HasMorePages = true;
                    title.Dispose(); heading.Dispose(); normal.Dispose(); small.Dispose();
                    return;
                }

                DataRow row = _lines.Rows[_lineIndex];
                string description = Convert.ToString(row["pdesc"]);
                if (description.Length > 43) description = description.Substring(0, 43);
                g.DrawString(Convert.ToString(row["pcode"]), small, Brushes.Black, xCode, y);
                g.DrawString(description, small, Brushes.Black, xDescription, y);
                g.DrawString(Convert.ToInt32(row["qty"]).ToString(), small, Brushes.Black, xQty, y);
                g.DrawString(Convert.ToDecimal(row["CostPrice"]).ToString("N2"), small, Brushes.Black, xCost, y);
                g.DrawString(Convert.ToDecimal(row["LineCost"]).ToString("N2"), small, Brushes.Black, xTotal, y);
                y += 19;
                _lineIndex++;
            }

            y += 5;
            g.DrawLine(linePen, area.Left, y, area.Right, y);
            y += 12;
            g.DrawString("Invoice Total: $ " + Convert.ToDecimal(_header["totalCost"]).ToString("N2"), title, Brushes.Black, area.Right - 270, y);
            e.HasMorePages = false;
            title.Dispose(); heading.Dispose(); normal.Dispose(); small.Dispose();
        }
    }
}
