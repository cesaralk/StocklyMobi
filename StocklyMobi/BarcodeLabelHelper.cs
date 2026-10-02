using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using Zen.Barcode;

namespace StocklyMobi
{
    internal sealed class BarcodeLabelItem
    {
        public BarcodeLabelItem(
            string barcode,
            string description,
            decimal price,
            int quantity)
        {
            Barcode = (barcode ?? string.Empty).Trim();
            Description = (description ?? string.Empty).Trim();
            Price = price;
            Quantity = Math.Max(0, quantity);
        }

        public string Barcode { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
    }

    internal static class BarcodeLabelHelper
    {
        // These values intentionally match the existing Barcode page exactly:
        // 4.5 cm x 2.5 cm at 100 printer units per inch.
        public const int LabelWidth = 177;
        public const int LabelHeight = 98;
        public const int BarcodeImageHeight = 40;
        public const int BarcodeScale = 1;

        public static string GenerateNextInternalBarcode(
            string connectionString)
        {
            string prefix =
                "29" + DateTime.Now.ToString("yyMMdd");

            int nextSequence = 1;

            using (SqlConnection cn =
                new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(@"
SELECT barcode
FROM tbProduct
WHERE isActive=1 AND barcode LIKE @pattern
  AND LEN(barcode) = 13
ORDER BY barcode DESC", cn))
            {
                cmd.Parameters.AddWithValue(
                    "@pattern",
                    prefix + "%");

                cn.Open();

                using (SqlDataReader dr =
                    cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string value =
                            dr["barcode"] == DBNull.Value
                                ? string.Empty
                                : dr["barcode"].ToString();

                        if (string.IsNullOrWhiteSpace(value) ||
                            value.Length != 13)
                        {
                            continue;
                        }

                        string sequencePart =
                            value.Substring(prefix.Length);

                        int parsed;

                        if (sequencePart.All(char.IsDigit) &&
                            int.TryParse(
                                sequencePart,
                                out parsed))
                        {
                            nextSequence = parsed + 1;
                            break;
                        }
                    }
                }
            }

            if (nextSequence > 99999)
                nextSequence = 1;

            return prefix +
                nextSequence.ToString("D5");
        }

        public static Image BuildBarcodeImage(
            string barcodeValue,
            int height = BarcodeImageHeight,
            int scale = BarcodeScale)
        {
            if (string.IsNullOrWhiteSpace(barcodeValue))
                return null;

            Code128BarcodeDraw barcode =
                BarcodeDrawFactory.Code128WithChecksum;

            return barcode.Draw(
                barcodeValue.Trim(),
                height,
                scale);
        }

        public static void ShowPrintPreview(
            IWin32Window owner,
            string barcodeValue,
            string description,
            decimal price)
        {
            ShowPrintPreview(
                owner,
                barcodeValue,
                description,
                price,
                1);
        }

        public static void ShowPrintPreview(
            IWin32Window owner,
            string barcodeValue,
            string description,
            decimal price,
            int quantity)
        {
            BarcodeLabelItem item =
                new BarcodeLabelItem(
                    barcodeValue,
                    description,
                    price,
                    quantity);

            ShowBatchPrintPreview(
                owner,
                new[] { item });
        }

        public static void ShowBatchPrintPreview(
            IWin32Window owner,
            IEnumerable<BarcodeLabelItem> items)
        {
            List<BarcodeLabelItem> printableItems =
                (items ?? Enumerable.Empty<BarcodeLabelItem>())
                .Where(
                    item =>
                        item != null &&
                        item.Quantity > 0 &&
                        !string.IsNullOrWhiteSpace(
                            item.Barcode))
                .ToList();

            if (printableItems.Count == 0)
            {
                MessageBox.Show(
                    owner,
                    "There are no barcode labels to print.",
                    "Barcode Labels",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            using (BarcodePrintSession session =
                new BarcodePrintSession(
                    printableItems))
            using (PrintPreviewDialog preview =
                new PrintPreviewDialog())
            {
                preview.Document = session.Document;
                preview.Width = 900;
                preview.Height = 700;
                preview.ShowDialog(owner);
            }
        }

        public static void DrawLabel(
            Graphics graphics,
            int labelWidth,
            Image barcodeImage,
            string barcodeValue,
            string description,
            string priceText)
        {
            if (graphics == null ||
                barcodeImage == null)
            {
                return;
            }

            const int margin = 2;
            int currentY = margin;

            int barcodeX =
                margin +
                (labelWidth - barcodeImage.Width) / 2;

            graphics.DrawImage(
                barcodeImage,
                barcodeX,
                currentY,
                barcodeImage.Width,
                barcodeImage.Height);

            currentY += barcodeImage.Height + 2;

            using (Font numberFont =
                new Font(
                    "Arial",
                    6F,
                    FontStyle.Bold))
            using (Font descriptionFont =
                new Font(
                    "Arial",
                    7F,
                    FontStyle.Bold))
            using (Font priceFont =
                new Font(
                    "Arial",
                    6F,
                    FontStyle.Bold))
            {
                SizeF numberSize =
                    graphics.MeasureString(
                        barcodeValue,
                        numberFont);

                graphics.DrawString(
                    barcodeValue,
                    numberFont,
                    Brushes.Black,
                    margin +
                    (labelWidth - numberSize.Width) / 2F,
                    currentY);

                currentY += 8;

                RectangleF descriptionRectangle =
                    new RectangleF(
                        margin,
                        currentY,
                        labelWidth,
                        14);

                graphics.DrawString(
                    description,
                    descriptionFont,
                    Brushes.Black,
                    descriptionRectangle);

                currentY += 14;

                string finalPriceText =
                    "Price: " + priceText;

                SizeF priceSize =
                    graphics.MeasureString(
                        finalPriceText,
                        priceFont);

                graphics.DrawString(
                    finalPriceText,
                    priceFont,
                    Brushes.Black,
                    margin +
                    (labelWidth - priceSize.Width) / 2F,
                    currentY);
            }
        }

        private sealed class BarcodePrintSession :
            IDisposable
        {
            private readonly IList<BarcodeLabelItem> _items;
            private int _itemIndex;
            private int _remainingQuantity;

            public BarcodePrintSession(
                IList<BarcodeLabelItem> items)
            {
                _items = items;

                Document = new PrintDocument();
                ApplyLabelSettings(
                    Document.DefaultPageSettings);

                ApplyLabelSettings(
                    Document.PrinterSettings
                        .DefaultPageSettings);

                Document.DocumentName =
                    "StocklyMobi Barcode Labels";

                Document.BeginPrint +=
                    BeginPrint;

                Document.QueryPageSettings +=
                    QueryPageSettings;

                Document.PrintPage +=
                    PrintPage;
            }

            public PrintDocument Document
            {
                get;
                private set;
            }

            private void BeginPrint(
                object sender,
                PrintEventArgs e)
            {
                _itemIndex = 0;
                _remainingQuantity =
                    _items.Count == 0
                        ? 0
                        : _items[0].Quantity;
            }

            private void QueryPageSettings(
                object sender,
                QueryPageSettingsEventArgs e)
            {
                ApplyLabelSettings(
                    e.PageSettings);
            }

            private void PrintPage(
                object sender,
                PrintPageEventArgs e)
            {
                if (_itemIndex >= _items.Count)
                {
                    e.HasMorePages = false;
                    return;
                }

                BarcodeLabelItem item =
                    _items[_itemIndex];

                using (Image barcodeImage =
                    BuildBarcodeImage(
                        item.Barcode,
                        BarcodeImageHeight,
                        BarcodeScale))
                {
                    if (barcodeImage == null)
                    {
                        Advance();
                        e.HasMorePages =
                            _itemIndex < _items.Count;
                        return;
                    }

                    DrawLabel(
                        e.Graphics,
                        LabelWidth,
                        barcodeImage,
                        item.Barcode,
                        item.Description,
                        item.Price.ToString("0.00"));
                }

                Advance();

                e.HasMorePages =
                    _itemIndex < _items.Count;
            }

            private void Advance()
            {
                _remainingQuantity--;

                if (_remainingQuantity > 0)
                    return;

                _itemIndex++;

                if (_itemIndex < _items.Count)
                {
                    _remainingQuantity =
                        _items[_itemIndex].Quantity;
                }
            }

            private static void ApplyLabelSettings(
                PageSettings pageSettings)
            {
                if (pageSettings == null)
                    return;

                pageSettings.Landscape = false;

                pageSettings.PaperSize =
                    new PaperSize(
                        "Label",
                        LabelWidth,
                        LabelHeight);

                pageSettings.Margins =
                    new Margins(0, 0, 0, 0);
            }

            public void Dispose()
            {
                if (Document == null)
                    return;

                Document.BeginPrint -=
                    BeginPrint;

                Document.QueryPageSettings -=
                    QueryPageSettings;

                Document.PrintPage -=
                    PrintPage;

                Document.Dispose();
                Document = null;
            }
        }
    }
}
