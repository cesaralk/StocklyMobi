using System;
using System.Drawing;
using System.Drawing.Printing;

namespace StocklyMobi
{
    internal static class PrinterConfiguration
    {
        // 12 hundredths of an inch is approximately 3 mm.
        // This is an inner software margin in addition to the printer's
        // physical non-printable area.
        public const float ReceiptSidePaddingHundredths = 12f;
        public static string ReceiptPrinterName
        {
            get { return ResolvePrinterName(Properties.Settings.Default.ReceiptPrinterName); }
        }

        public static string InvoicePrinterName
        {
            get { return ResolvePrinterName(Properties.Settings.Default.InvoicePrinterName); }
        }

        public static bool AutoPrintReceipt
        {
            get { return Properties.Settings.Default.AutoPrintReceipt; }
        }

        public static bool AutoOpenCashDrawer
        {
            get
            {
                return Properties.Settings.Default.AutoOpenCashDrawer;
            }
        }

        public static int CashDrawerPin
        {
            get
            {
                return Properties.Settings.Default.CashDrawerPin == 5
                    ? 5
                    : 2;
            }
        }

        public static int CashDrawerProfile
        {
            get
            {
                return CashDrawerController.NormalizeProfile(
                    Properties.Settings.Default.CashDrawerProfile);
            }
        }

        public static decimal ReceiptTopTrimMm
        {
            get
            {
                decimal value = Properties.Settings.Default.ReceiptTopTrimMm;

                // Values above 2 mm belonged to the old fixed-page workaround.
                // Ignore them so an old saved value cannot cut off the receipt.
                if (value < 0m || value > 2m) return 0m;

                return value;
            }
        }

        public static float ReceiptTopTrimHundredths
        {
            get
            {
                return (float)(ReceiptTopTrimMm * 100m / 25.4m);
            }
        }

        public static string DefaultPrinterName
        {
            get
            {
                try
                {
                    PrinterSettings settings = new PrinterSettings();
                    return settings.PrinterName ?? string.Empty;
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

        public static bool IsInstalled(string printerName)
        {
            if (string.IsNullOrWhiteSpace(printerName)) return false;
            try
            {
                foreach (string installed in PrinterSettings.InstalledPrinters)
                {
                    if (string.Equals(installed, printerName, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        public static string ResolvePrinterName(string configuredName)
        {
            if (IsInstalled(configuredName)) return configuredName;
            return DefaultPrinterName;
        }

        public static void ApplyReceiptPrinter(PrintDocument document)
        {
            if (document == null) throw new ArgumentNullException("document");
            ApplyPrinter(document, ReceiptPrinterName);
        }

        public static void ApplyInvoicePrinter(PrintDocument document)
        {
            if (document == null) throw new ArgumentNullException("document");
            ApplyPrinter(document, InvoicePrinterName);
            document.DefaultPageSettings.Landscape = false;
            document.DefaultPageSettings.PaperSize = FindA4PaperSize(document.PrinterSettings);
        }

        public static void ApplyReceiptOriginCompensation(Graphics graphics, PageSettings pageSettings)
        {
            if (graphics == null) throw new ArgumentNullException("graphics");
            if (pageSettings == null) throw new ArgumentNullException("pageSettings");

            float hardMarginY = Math.Max(0f, pageSettings.HardMarginY);
            float additionalTrim = ReceiptTopTrimHundredths;

            // Do not subtract HardMarginX. The Graphics origin is already
            // inside the printer's horizontal printable area on common
            // thermal-printer drivers. Moving it left clips the first and last
            // characters. Only compensate vertically.
            graphics.TranslateTransform(
                0f,
                -(hardMarginY + additionalTrim));
        }

        public static PaperSize FindA4PaperSize(PrinterSettings printerSettings)
        {
            if (printerSettings != null)
            {
                foreach (PaperSize size in printerSettings.PaperSizes)
                {
                    if (size.Kind == PaperKind.A4 ||
                        (!string.IsNullOrWhiteSpace(size.PaperName) &&
                         size.PaperName.IndexOf("A4", StringComparison.OrdinalIgnoreCase) >= 0))
                        return size;
                }
            }

            return new PaperSize("A4", 827, 1169);
        }

        private static void ApplyPrinter(PrintDocument document, string printerName)
        {
            if (!string.IsNullOrWhiteSpace(printerName))
                document.PrinterSettings.PrinterName = printerName;

            if (!document.PrinterSettings.IsValid)
                throw new InvalidOperationException("The selected printer is not available. Open Printer Settings and choose an installed printer.");
        }
    }
}
