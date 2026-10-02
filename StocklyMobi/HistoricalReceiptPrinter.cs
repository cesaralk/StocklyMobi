using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace StocklyMobi
{
    internal sealed class HistoricalReceiptPrinter
    {
        private readonly DBConnect _db = new DBConnect();
        private readonly string _transNo;
        private DataRow _header;
        private DataTable _lines;
        private string _store = string.Empty;
        private string _address = string.Empty;
        private int _receiptPaperHeight = 300;

        private HistoricalReceiptPrinter(string transNo)
        {
            _transNo = transNo;
            LoadData();
        }

        public static void ShowPreview(IWin32Window owner, string transNo)
        {
            try
            {
                HistoricalReceiptPrinter printer = new HistoricalReceiptPrinter(transNo);
                using (PrintDocument document = printer.CreateDocument())
                using (PrintPreviewDialog dialog = new PrintPreviewDialog())
                {
                    dialog.Document = document;
                    dialog.Width = 900;
                    dialog.Height = 760;
                    dialog.ShowDialog(owner);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Receipt Print Preview", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public static void Print(string transNo)
        {
            try
            {
                PrintDirect(transNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message, "Print Receipt",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public static void PrintDirect(string transNo)
        {
            if (string.IsNullOrWhiteSpace(transNo))
                throw new ArgumentException("The receipt number is missing.", "transNo");

            HistoricalReceiptPrinter printer = new HistoricalReceiptPrinter(transNo.Trim());
            using (PrintDocument document = printer.CreateDocument())
            {
                document.Print();
            }
        }

        private PrintDocument CreateDocument()
        {
            PrintDocument document = new PrintDocument();
            PrinterConfiguration.ApplyReceiptPrinter(document);

            // Apply a temporary short page first so the selected printer can
            // report its real printable width.
            _receiptPaperHeight = 300;
            ApplyReceiptPageSettings(document.DefaultPageSettings);
            ApplyReceiptPageSettings(document.PrinterSettings.DefaultPageSettings);

            float contentWidth = GetReceiptContentWidth(
                document.DefaultPageSettings);

            _receiptPaperHeight = CalculateReceiptPaperHeight(contentWidth);
            ApplyReceiptPageSettings(document.DefaultPageSettings);
            ApplyReceiptPageSettings(document.PrinterSettings.DefaultPageSettings);

            document.OriginAtMargins = false;
            document.DocumentName = "Receipt " + _transNo;
            document.QueryPageSettings += QueryPageSettings;
            document.PrintPage += PrintPage;
            return document;
        }

        private void QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
        {
            ApplyReceiptPageSettings(e.PageSettings);
        }

        private void ApplyReceiptPageSettings(PageSettings pageSettings)
        {
            if (pageSettings == null) return;

            pageSettings.Landscape = false;
            pageSettings.Margins = new Margins(0, 0, 0, 0);
            pageSettings.PaperSize = new PaperSize(
                "Receipt 80mm",
                315,
                _receiptPaperHeight);
        }

        private int CalculateReceiptPaperHeight(float contentWidth)
        {
            const float bottomFeed = 24f;
            float y = 0f;

            using (Bitmap bitmap = new Bitmap(32, 32))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            using (Font title = new Font("Arial", 12F, FontStyle.Bold))
            using (Font heading = new Font("Arial", 8.5F, FontStyle.Bold))
            using (Font normal = new Font("Arial", 8F))
            using (Font small = new Font("Arial", 7F))
            using (StringFormat center = new StringFormat())
            {
                graphics.PageUnit = GraphicsUnit.Inch;
                center.Alignment = StringAlignment.Center;

                y += 23f;

                if (!string.IsNullOrWhiteSpace(_address))
                {
                    y += MeasureWrappedHeight(
                        graphics,
                        _address,
                        small,
                        contentWidth,
                        center) + 2f;
                }

                y += 20f;
                y += 7f;

                // Receipt number, date, cashier and status.
                y += 60f;
                y += 7f;

                foreach (DataRow row in _lines.Rows)
                {
                    string description = Convert.ToString(row["description"]);
                    if (string.IsNullOrWhiteSpace(description))
                        description = Convert.ToString(row["pcode"]);

                    y += MeasureWrappedHeight(
                        graphics,
                        description,
                        heading,
                        contentWidth,
                        null);

                    y += 14f;

                    if (Convert.ToDecimal(row["lineDiscount"]) > 0m)
                        y += 13f;

                    y += 3f;
                }

                y += 7f;

                // Subtotal and net total.
                y += 15f;
                if (Convert.ToDecimal(_header["lineDiscount"]) > 0m)
                    y += 15f;

                decimal transactionDiscount =
                    Convert.ToDecimal(_header["transactionDiscount"]);

                if (transactionDiscount > 0m)
                {
                    y += 15f;

                    string reason =
                        Convert.ToString(_header["discountReason"]);

                    if (!string.IsNullOrWhiteSpace(reason))
                        y += 14f;
                }

                y += 15f;

                if (Convert.ToDecimal(_header["returnedTotal"]) > 0m)
                    y += 15f;

                y += 7f;

                bool paymentRecorded =
                    Convert.ToBoolean(_header["paymentRecorded"]);

                if (paymentRecorded)
                {
                    y += 15f;

                    if (Convert.ToDecimal(_header["cashLBP"]) > 0m)
                        y += 15f;

                    y += 15f;

                    if (Convert.ToDecimal(_header["changeLBP"]) > 0m)
                        y += 15f;

                    y += 15f;
                }
                else
                {
                    y += MeasureWrappedHeight(
                        graphics,
                        "Payment amounts were not stored for this older receipt.",
                        small,
                        contentWidth,
                        center);
                }

                y += 8f;
                y += 8f;
                y += 18f;
            }

            // Keep only a small trailing allowance. On printers that feed the
            // unused part before printing, this limits the leading blank to
            // approximately 6 mm instead of the previous 11 cm.
            int requiredHeight = (int)Math.Ceiling(y + bottomFeed);
            return Math.Max(260, requiredHeight);
        }

        private static float GetReceiptContentWidth(
            PageSettings pageSettings)
        {
            float printableWidth = 315f;

            if (pageSettings != null &&
                pageSettings.PrintableArea.Width > 0f &&
                pageSettings.PrintableArea.Width < printableWidth)
            {
                printableWidth = pageSettings.PrintableArea.Width;
            }

            float width = printableWidth -
                (PrinterConfiguration.ReceiptSidePaddingHundredths * 2f);

            // Keep enough width for two-column totals, even when a printer
            // driver reports an unusually narrow printable area.
            return Math.Max(210f, width);
        }

        private static float MeasureWrappedHeight(
            Graphics graphics,
            string text,
            Font font,
            float widthHundredths,
            StringFormat format)
        {
            if (string.IsNullOrEmpty(text)) return 0f;

            SizeF measured = graphics.MeasureString(
                text,
                font,
                new SizeF(widthHundredths / 100f, 100f),
                format ?? StringFormat.GenericDefault);

            return (measured.Height * 100f) + 2f;
        }

        private void LoadData()
        {
            using (SqlConnection con = new SqlConnection(_db.myConnection()))
            {
                con.Open();
                using (SqlCommand storeCmd = new SqlCommand("SELECT TOP 1 ISNULL(store,''),ISNULL(address,'') FROM dbo.tbStore ORDER BY id", con))
                using (SqlDataReader storeReader = storeCmd.ExecuteReader())
                {
                    if (storeReader.Read())
                    {
                        _store = Convert.ToString(storeReader[0]);
                        _address = Convert.ToString(storeReader[1]);
                    }
                }

                using (SqlCommand cmd = new SqlCommand(@"
SELECT h.transno,h.soldAt,h.cashier,h.subtotal,h.lineDiscount,h.transactionDiscount,
       h.discountReason,h.netTotal,h.cashUSD,h.cashLBP,h.changeUSD,h.changeLBP,
       h.exchangeRate,h.paymentRecorded,h.status,ISNULL(r.returnedTotal,0) returnedTotal
FROM dbo.tbReceiptHeader h
OUTER APPLY
(
    SELECT SUM(ISNULL(x.total,0)) returnedTotal
    FROM dbo.tbCancel x
    WHERE x.transno=h.transno
) r
WHERE h.transno=@transno", con))
                {
                    cmd.Parameters.Add("@transno", SqlDbType.VarChar, 50).Value = _transNo;
                    DataTable header = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd)) da.Fill(header);
                    if (header.Rows.Count == 0) throw new InvalidOperationException("The receipt was not found.");
                    _header = header.Rows[0];
                }

                using (SqlCommand cmd = new SqlCommand(@"
SELECT [lineNo],pcode,description,itemType,price,qty,lineDiscount,lineTotal
FROM dbo.tbReceiptLine
WHERE transno=@transno
ORDER BY [lineNo],id", con))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.Add("@transno", SqlDbType.VarChar, 50).Value = _transNo;
                    _lines = new DataTable();
                    da.Fill(_lines);
                }
            }
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            PrinterConfiguration.ApplyReceiptOriginCompensation(g, e.PageSettings);
            Rectangle area = CreatePrintableReceiptArea(e);
            float y = 0f;
            using (Font title = new Font("Arial", 12F, FontStyle.Bold))
            using (Font heading = new Font("Arial", 8.5F, FontStyle.Bold))
            using (Font normal = new Font("Arial", 8F))
            using (Font small = new Font("Arial", 7F))
            {
                StringFormat center = new StringFormat { Alignment = StringAlignment.Center };
                string storeTitle = string.IsNullOrWhiteSpace(_store) ? "STOCKLY MOBI" : _store;
                g.DrawString(storeTitle, title, Brushes.Black, new RectangleF(area.Left, y, area.Width, 22), center);
                y += 23;
                if (!string.IsNullOrWhiteSpace(_address))
                {
                    float addressHeight = DrawWrapped(g, _address, small, area.Left, y, area.Width, center);
                    y += addressHeight + 2;
                }
                g.DrawString("RECEIPT COPY", heading, Brushes.Black, new RectangleF(area.Left, y, area.Width, 18), center);
                y += 20;
                DrawRule(g, area, y);
                y += 7;

                DrawPair(g, normal, area, ref y, "Receipt:", Convert.ToString(_header["transno"]));
                DrawPair(g, normal, area, ref y, "Date:", Convert.ToDateTime(_header["soldAt"]).ToString("dd/MM/yyyy hh:mm:ss tt"));
                DrawPair(g, normal, area, ref y, "Cashier:", Convert.ToString(_header["cashier"]));
                DrawPair(g, normal, area, ref y, "Status:", Convert.ToString(_header["status"]));
                DrawRule(g, area, y);
                y += 7;

                foreach (DataRow row in _lines.Rows)
                {
                    string description = Convert.ToString(row["description"]);
                    if (string.IsNullOrWhiteSpace(description)) description = Convert.ToString(row["pcode"]);
                    float descriptionHeight = DrawWrapped(g, description, heading, area.Left, y, area.Width, null);
                    y += descriptionHeight;

                    int qty = Convert.ToInt32(row["qty"]);
                    decimal price = Convert.ToDecimal(row["price"]);
                    decimal lineDiscount = Convert.ToDecimal(row["lineDiscount"]);
                    decimal lineTotal = Convert.ToDecimal(row["lineTotal"]);
                    string left = qty.ToString() + " x $" + price.ToString("N2");
                    string right = "$" + lineTotal.ToString("N2");
                    DrawTwoColumns(g, small, area, y, left, right);
                    y += 14;
                    if (lineDiscount > 0m)
                    {
                        g.DrawString("Line discount: -$" + lineDiscount.ToString("N2"), small, Brushes.Black, area.Left + 10, y);
                        y += 13;
                    }
                    y += 3;
                }

                DrawRule(g, area, y);
                y += 7;
                DrawMoney(g, normal, area, ref y, "Subtotal", Convert.ToDecimal(_header["subtotal"]));
                decimal lineDisc = Convert.ToDecimal(_header["lineDiscount"]);
                if (lineDisc > 0m) DrawMoney(g, normal, area, ref y, "Line discounts", -lineDisc);
                decimal transDisc = Convert.ToDecimal(_header["transactionDiscount"]);
                if (transDisc > 0m)
                {
                    DrawMoney(g, normal, area, ref y, "Transaction discount", -transDisc);
                    string reason = Convert.ToString(_header["discountReason"]);
                    if (!string.IsNullOrWhiteSpace(reason))
                    {
                        g.DrawString("Reason: " + reason, small, Brushes.Black, area.Left, y);
                        y += 14;
                    }
                }
                DrawMoney(g, heading, area, ref y, "NET TOTAL", Convert.ToDecimal(_header["netTotal"]));

                decimal returned = Convert.ToDecimal(_header["returnedTotal"]);
                if (returned > 0m)
                {
                    DrawMoney(g, normal, area, ref y, "Returned later", -returned);
                }

                DrawRule(g, area, y);
                y += 7;
                bool paymentRecorded = Convert.ToBoolean(_header["paymentRecorded"]);
                if (paymentRecorded)
                {
                    DrawMoney(g, normal, area, ref y, "Cash USD", Convert.ToDecimal(_header["cashUSD"]));
                    decimal cashLbp = Convert.ToDecimal(_header["cashLBP"]);
                    if (cashLbp > 0m) DrawTextPair(g, normal, area, ref y, "Cash LBP", LbpFormatter.Format(cashLbp));
                    DrawMoney(g, normal, area, ref y, "Change USD", Convert.ToDecimal(_header["changeUSD"]));
                    decimal changeLbp = Convert.ToDecimal(_header["changeLBP"]);
                    if (changeLbp > 0m) DrawTextPair(g, normal, area, ref y, "Change LBP", LbpFormatter.Format(changeLbp));
                    DrawTextPair(g, small, area, ref y, "Rate", Convert.ToDecimal(_header["exchangeRate"]).ToString("N0") + " L.L / USD");
                }
                else
                {
                    y += DrawWrapped(g, "Payment amounts were not stored for this older receipt.", small, area.Left, y, area.Width, center);
                }

                y += 8;
                DrawRule(g, area, y);
                y += 8;

                g.DrawString(
                    "Thank you",
                    heading,
                    Brushes.Black,
                    new RectangleF(area.Left, y, area.Width, 18),
                    center);

                y += 20;

                g.DrawString(
                    "Powered by Cezar Elkhatib : 70864221",
                    small,
                    Brushes.Black,
                    new RectangleF(area.Left, y, area.Width, 14),
                    center);
            }
            e.HasMorePages = false;
        }

        private static Rectangle CreatePrintableReceiptArea(
            PrintPageEventArgs e)
        {
            int pageWidth =
                e.PageBounds.Width > 0 ? e.PageBounds.Width : 315;

            int pageHeight =
                e.PageBounds.Height > 0 ? e.PageBounds.Height : 300;

            float printableWidth = pageWidth;

            if (e.PageSettings != null &&
                e.PageSettings.PrintableArea.Width > 0f &&
                e.PageSettings.PrintableArea.Width < printableWidth)
            {
                printableWidth = e.PageSettings.PrintableArea.Width;
            }

            RectangleF visible = e.Graphics.VisibleClipBounds;

            if (visible.Width > 0f &&
                visible.Width < printableWidth)
            {
                printableWidth = visible.Width;
            }

            int sidePadding = (int)Math.Ceiling(
                PrinterConfiguration.ReceiptSidePaddingHundredths);

            int contentWidth = Math.Max(
                1,
                (int)Math.Floor(printableWidth) -
                (sidePadding * 2));

            return new Rectangle(
                sidePadding,
                0,
                contentWidth,
                pageHeight);
        }

        private static void DrawPair(Graphics g, Font font, Rectangle area, ref float y, string label, string value)
        {
            g.DrawString(label, font, Brushes.Black, area.Left, y);
            SizeF size = g.MeasureString(value, font);
            g.DrawString(value, font, Brushes.Black, area.Right - size.Width, y);
            y += 15;
        }

        private static void DrawTextPair(Graphics g, Font font, Rectangle area, ref float y, string label, string value)
        {
            DrawPair(g, font, area, ref y, label + ":", value);
        }

        private static void DrawMoney(Graphics g, Font font, Rectangle area, ref float y, string label, decimal amount)
        {
            string value = (amount < 0m ? "-$" : "$ ") + Math.Abs(amount).ToString("N2");
            DrawPair(g, font, area, ref y, label + ":", value);
        }

        private static void DrawTwoColumns(Graphics g, Font font, Rectangle area, float y, string left, string right)
        {
            g.DrawString(left, font, Brushes.Black, area.Left + 10, y);
            SizeF size = g.MeasureString(right, font);
            g.DrawString(right, font, Brushes.Black, area.Right - size.Width, y);
        }

        private static float DrawWrapped(Graphics g, string text, Font font, float x, float y, float width, StringFormat format)
        {
            if (string.IsNullOrEmpty(text)) return 0f;
            SizeF measured = g.MeasureString(text, font, new SizeF(width, 1000f), format ?? StringFormat.GenericDefault);
            g.DrawString(text, font, Brushes.Black, new RectangleF(x, y, width, measured.Height + 2f), format ?? StringFormat.GenericDefault);
            return measured.Height + 2f;
        }

        private static void DrawRule(Graphics g, Rectangle area, float y)
        {
            g.DrawLine(Pens.Black, area.Left, y, area.Right, y);
        }
    }
}
