using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace StocklyMobi
{
    internal sealed class MobileFixTicketPrinter
    {
        private readonly string _fixCode;
        private readonly string _customerName;
        private readonly string _phoneNumber;
        private readonly string _phoneModel;
        private readonly string _problemDescription;
        private readonly string _status;
        private readonly DateTime _estimatedFinish;
        private readonly decimal _finalPrice;
        private int _paperHeight = 260;

        private MobileFixTicketPrinter(DataRow row)
        {
            if (row == null)
                throw new ArgumentNullException("row");

            _fixCode = ReadText(row, "fixCode");
            _customerName = ReadText(row, "customerName");
            _phoneNumber = ReadText(row, "phoneNumber");
            _phoneModel = ReadText(row, "phoneModel");
            _problemDescription = ReadText(row, "problemDescription");
            _status = ReadText(row, "status");
            _estimatedFinish = ReadDate(row, "estimatedFinish");
            _finalPrice = ReadDecimal(row, "finalPrice");
        }

        public static void ShowPreview(IWin32Window owner, DataRow row)
        {
            MobileFixTicketPrinter printer =
                new MobileFixTicketPrinter(row);

            using (PrintDocument document = printer.CreateDocument())
            using (PrintPreviewDialog preview = new PrintPreviewDialog())
            {
                preview.Document = document;
                preview.Width = 900;
                preview.Height = 760;
                preview.ShowDialog(owner);
            }
        }

        public static void PrintDirect(DataRow row)
        {
            MobileFixTicketPrinter printer =
                new MobileFixTicketPrinter(row);

            using (PrintDocument document = printer.CreateDocument())
            {
                document.Print();
            }
        }

        private PrintDocument CreateDocument()
        {
            PrintDocument document = new PrintDocument();
            PrinterConfiguration.ApplyReceiptPrinter(document);

            _paperHeight = 260;
            ApplyPageSettings(document.DefaultPageSettings);
            ApplyPageSettings(
                document.PrinterSettings.DefaultPageSettings);

            float contentWidth = GetContentWidth(
                document.DefaultPageSettings);

            _paperHeight = CalculatePaperHeight(contentWidth);
            ApplyPageSettings(document.DefaultPageSettings);
            ApplyPageSettings(
                document.PrinterSettings.DefaultPageSettings);

            document.DocumentName =
                "Mobile Fixing Ticket " + _fixCode;

            document.OriginAtMargins = false;
            document.QueryPageSettings += QueryPageSettings;
            document.PrintPage += PrintPage;
            return document;
        }

        private void QueryPageSettings(
            object sender,
            QueryPageSettingsEventArgs e)
        {
            ApplyPageSettings(e.PageSettings);
        }

        private void ApplyPageSettings(PageSettings pageSettings)
        {
            if (pageSettings == null) return;

            pageSettings.Landscape = false;
            pageSettings.Margins = new Margins(0, 0, 0, 0);
            pageSettings.PaperSize = new PaperSize(
                "Receipt 80mm",
                315,
                _paperHeight);
        }

        private int CalculatePaperHeight(float contentWidth)
        {
            const float bottomFeed = 24f;
            const float labelWidth = 74f;
            float valueWidth = Math.Max(
                120f,
                contentWidth - labelWidth - 5f);

            float y = 8f;

            using (Bitmap bitmap = new Bitmap(32, 32))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            using (Font title = new Font(
                "Arial", 11F, FontStyle.Bold))
            using (Font label = new Font(
                "Arial", 7.5F, FontStyle.Bold))
            using (Font value = new Font("Arial", 8F))
            using (Font small = new Font("Arial", 7F))
            using (StringFormat center = CreateCenterFormat())
            {
                graphics.PageUnit = GraphicsUnit.Inch;

                y += MeasureHeight(
                    graphics,
                    "MOBILE FIXING TICKET",
                    title,
                    contentWidth,
                    center) + 7f;

                y += 6f;

                y += MeasureFieldHeight(
                    graphics,
                    "Fix ID",
                    _fixCode,
                    label,
                    value,
                    labelWidth,
                    valueWidth);

                y += MeasureFieldHeight(
                    graphics,
                    "Customer",
                    _customerName,
                    label,
                    value,
                    labelWidth,
                    valueWidth);

                y += MeasureFieldHeight(
                    graphics,
                    "Phone",
                    _phoneNumber,
                    label,
                    value,
                    labelWidth,
                    valueWidth);

                y += MeasureFieldHeight(
                    graphics,
                    "Model",
                    _phoneModel,
                    label,
                    value,
                    labelWidth,
                    valueWidth);

                y += MeasureFieldHeight(
                    graphics,
                    "Problem",
                    _problemDescription,
                    label,
                    value,
                    labelWidth,
                    valueWidth);

                y += MeasureFieldHeight(
                    graphics,
                    "Estimated",
                    _estimatedFinish.ToString(
                        "dd/MM/yyyy hh:mm tt"),
                    label,
                    value,
                    labelWidth,
                    valueWidth);

                y += MeasureFieldHeight(
                    graphics,
                    "Price",
                    "$ " + _finalPrice.ToString("N2"),
                    label,
                    value,
                    labelWidth,
                    valueWidth);

                y += MeasureFieldHeight(
                    graphics,
                    "Status",
                    _status,
                    label,
                    value,
                    labelWidth,
                    valueWidth);

                y += 8f;

                y += MeasureHeight(
                    graphics,
                    "Please keep this ticket and present the Fix ID when collecting the phone.",
                    small,
                    contentWidth,
                    center);

                y += 4f;
            }

            int required =
                (int)Math.Ceiling(y + bottomFeed);

            return Math.Max(220, required);
        }

        private void PrintPage(
            object sender,
            PrintPageEventArgs e)
        {
            PrinterConfiguration.ApplyReceiptOriginCompensation(
                e.Graphics,
                e.PageSettings);

            RectangleF area = CreatePrintableArea(e);
            float y = 8f;
            const float labelWidth = 74f;

            using (Font title = new Font(
                "Arial", 11F, FontStyle.Bold))
            using (Font label = new Font(
                "Arial", 7.5F, FontStyle.Bold))
            using (Font value = new Font("Arial", 8F))
            using (Font small = new Font("Arial", 7F))
            using (Pen line = new Pen(Color.Black, 1F))
            using (StringFormat center = CreateCenterFormat())
            {
                float titleHeight = MeasureDisplayHeight(
                    e.Graphics,
                    "MOBILE FIXING TICKET",
                    title,
                    area.Width,
                    center);

                e.Graphics.DrawString(
                    "MOBILE FIXING TICKET",
                    title,
                    Brushes.Black,
                    new RectangleF(
                        area.Left,
                        y,
                        area.Width,
                        titleHeight),
                    center);

                y += titleHeight + 7f;

                e.Graphics.DrawLine(
                    line,
                    area.Left,
                    y,
                    area.Right,
                    y);

                y += 6f;

                DrawField(
                    e.Graphics,
                    label,
                    value,
                    area,
                    labelWidth,
                    ref y,
                    "Fix ID",
                    _fixCode);

                DrawField(
                    e.Graphics,
                    label,
                    value,
                    area,
                    labelWidth,
                    ref y,
                    "Customer",
                    _customerName);

                DrawField(
                    e.Graphics,
                    label,
                    value,
                    area,
                    labelWidth,
                    ref y,
                    "Phone",
                    _phoneNumber);

                DrawField(
                    e.Graphics,
                    label,
                    value,
                    area,
                    labelWidth,
                    ref y,
                    "Model",
                    _phoneModel);

                DrawField(
                    e.Graphics,
                    label,
                    value,
                    area,
                    labelWidth,
                    ref y,
                    "Problem",
                    _problemDescription);

                DrawField(
                    e.Graphics,
                    label,
                    value,
                    area,
                    labelWidth,
                    ref y,
                    "Estimated",
                    _estimatedFinish.ToString(
                        "dd/MM/yyyy hh:mm tt"));


                DrawField(
                    e.Graphics,
                    label,
                    value,
                    area,
                    labelWidth,
                    ref y,
                    "Status",
                    _status);

                y += 4f;

                e.Graphics.DrawLine(
                    line,
                    area.Left,
                    y,
                    area.Right,
                    y);

                y += 8f;

                float footerHeight = MeasureDisplayHeight(
                    e.Graphics,
                    "Please keep this ticket and present the Fix ID when collecting the phone.",
                    small,
                    area.Width,
                    center);

                e.Graphics.DrawString(
                    "Please keep this ticket and present the Fix ID when collecting the phone.",
                    small,
                    Brushes.Black,
                    new RectangleF(
                        area.Left,
                        y,
                        area.Width,
                        footerHeight),
                    center);
            }

            e.HasMorePages = false;
        }

        private static RectangleF CreatePrintableArea(
            PrintPageEventArgs e)
        {
            float pageWidth =
                e.PageBounds.Width > 0
                    ? e.PageBounds.Width
                    : 315f;

            float pageHeight =
                e.PageBounds.Height > 0
                    ? e.PageBounds.Height
                    : 260f;

            float printableWidth = pageWidth;

            if (e.PageSettings != null &&
                e.PageSettings.PrintableArea.Width > 0f &&
                e.PageSettings.PrintableArea.Width <
                printableWidth)
            {
                printableWidth =
                    e.PageSettings.PrintableArea.Width;
            }

            RectangleF visible =
                e.Graphics.VisibleClipBounds;

            if (visible.Width > 0f &&
                visible.Width < printableWidth)
            {
                printableWidth = visible.Width;
            }

            float sidePadding =
                PrinterConfiguration
                    .ReceiptSidePaddingHundredths;

            float width = Math.Max(
                1f,
                printableWidth - (sidePadding * 2f));

            return new RectangleF(
                sidePadding,
                0f,
                width,
                pageHeight);
        }

        private static float GetContentWidth(
            PageSettings pageSettings)
        {
            float printableWidth = 315f;

            if (pageSettings != null &&
                pageSettings.PrintableArea.Width > 0f &&
                pageSettings.PrintableArea.Width <
                printableWidth)
            {
                printableWidth =
                    pageSettings.PrintableArea.Width;
            }

            float width = printableWidth -
                (PrinterConfiguration
                    .ReceiptSidePaddingHundredths * 2f);

            return Math.Max(210f, width);
        }

        private static float MeasureFieldHeight(
            Graphics graphics,
            string labelText,
            string valueText,
            Font labelFont,
            Font valueFont,
            float labelWidth,
            float valueWidth)
        {
            float labelHeight = MeasureHeight(
                graphics,
                labelText + ":",
                labelFont,
                labelWidth,
                null);

            float valueHeight = MeasureHeight(
                graphics,
                valueText,
                valueFont,
                valueWidth,
                null);

            return Math.Max(
                13f,
                Math.Max(labelHeight, valueHeight)) + 3f;
        }

        private static float MeasureHeight(
            Graphics graphics,
            string text,
            Font font,
            float widthHundredths,
            StringFormat format)
        {
            string safeText = text ?? string.Empty;

            SizeF size = graphics.MeasureString(
                safeText,
                font,
                new SizeF(
                    widthHundredths / 100f,
                    100f),
                format ??
                    StringFormat.GenericDefault);

            return (size.Height * 100f) + 2f;
        }

        private static float MeasureDisplayHeight(
            Graphics graphics,
            string text,
            Font font,
            float width,
            StringFormat format)
        {
            SizeF size = graphics.MeasureString(
                text ?? string.Empty,
                font,
                new SizeF(width, 1000f),
                format ??
                    StringFormat.GenericDefault);

            return Math.Max(
                font.GetHeight(graphics) + 1f,
                size.Height + 1f);
        }

        private static void DrawField(
            Graphics graphics,
            Font labelFont,
            Font valueFont,
            RectangleF area,
            float labelWidth,
            ref float y,
            string labelText,
            string valueText)
        {
            float valueWidth = Math.Max(
                1f,
                area.Width - labelWidth - 5f);

            float labelHeight = MeasureDisplayHeight(
                graphics,
                labelText + ":",
                labelFont,
                labelWidth,
                null);

            float valueHeight = MeasureDisplayHeight(
                graphics,
                valueText,
                valueFont,
                valueWidth,
                null);

            float height = Math.Max(
                13f,
                Math.Max(labelHeight, valueHeight));

            graphics.DrawString(
                labelText + ":",
                labelFont,
                Brushes.Black,
                new RectangleF(
                    area.Left,
                    y,
                    labelWidth,
                    height));

            graphics.DrawString(
                valueText ?? string.Empty,
                valueFont,
                Brushes.Black,
                new RectangleF(
                    area.Left + labelWidth + 5f,
                    y,
                    valueWidth,
                    height));

            y += height + 3f;
        }

        private static StringFormat CreateCenterFormat()
        {
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Near;
            format.Trimming = StringTrimming.Word;
            return format;
        }

        private static string ReadText(
            DataRow row,
            string columnName)
        {
            if (!row.Table.Columns.Contains(columnName) ||
                row[columnName] == DBNull.Value)
            {
                return string.Empty;
            }

            return Convert.ToString(row[columnName]);
        }

        private static DateTime ReadDate(
            DataRow row,
            string columnName)
        {
            if (!row.Table.Columns.Contains(columnName) ||
                row[columnName] == DBNull.Value)
            {
                return DateTime.Now;
            }

            return Convert.ToDateTime(row[columnName]);
        }

        private static decimal ReadDecimal(
            DataRow row,
            string columnName)
        {
            if (!row.Table.Columns.Contains(columnName) ||
                row[columnName] == DBNull.Value)
            {
                return 0m;
            }

            return Convert.ToDecimal(row[columnName]);
        }
    }
}
