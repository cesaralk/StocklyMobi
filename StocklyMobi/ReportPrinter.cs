using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Globalization;
using Microsoft.Reporting.WinForms;

namespace StocklyMobi
{
    public static class LocalReportExtensions
    {
        public static void Print(this LocalReport report)
        {
            Print(report, string.Empty);
        }

        public static void Print(this LocalReport report, string printerName)
        {
            PageSettings pageSettings = new PageSettings();
            pageSettings.PaperSize = report.GetDefaultPageSettings().PaperSize;
            pageSettings.Landscape = report.GetDefaultPageSettings().IsLandscape;
            pageSettings.Margins = report.GetDefaultPageSettings().Margins;
            Print(report, pageSettings, printerName);
        }

        public static void Print(this LocalReport report, PageSettings pageSettings)
        {
            Print(report, pageSettings, string.Empty);
        }

        private static string ToInches(int hundredthsOfInch)
        {
            return (hundredthsOfInch / 100.0).ToString("0.###", CultureInfo.InvariantCulture);
        }

        public static void Print(this LocalReport report, PageSettings pageSettings, string printerName)
        {
            if (report == null) throw new ArgumentNullException("report");
            if (pageSettings == null) throw new ArgumentNullException("pageSettings");

            string deviceInfo =
                $@"<DeviceInfo>
                    <OutputFormat>EMF</OutputFormat>
                    <PageWidth>{ToInches(pageSettings.PaperSize.Width)}in</PageWidth>
                    <PageHeight>{ToInches(pageSettings.PaperSize.Height)}in</PageHeight>
                    <MarginTop>{ToInches(pageSettings.Margins.Top)}in</MarginTop>
                    <MarginLeft>{ToInches(pageSettings.Margins.Left)}in</MarginLeft>
                    <MarginRight>{ToInches(pageSettings.Margins.Right)}in</MarginRight>
                    <MarginBottom>{ToInches(pageSettings.Margins.Bottom)}in</MarginBottom>
                </DeviceInfo>";

            Warning[] warnings;
            List<Stream> streams = new List<Stream>();
            int currentPageIndex = 0;

            report.Render("Image", deviceInfo,
                delegate(string name, string fileNameExtension, System.Text.Encoding encoding, string mimeType, bool willSeek)
                {
                    MemoryStream stream = new MemoryStream();
                    streams.Add(stream);
                    return stream;
                }, out warnings);

            foreach (Stream stream in streams)
                stream.Position = 0;

            if (streams.Count == 0)
                throw new Exception("Error: no stream to print.");

            PrintDocument printDocument = new PrintDocument();
            if (!string.IsNullOrWhiteSpace(printerName))
                printDocument.PrinterSettings.PrinterName = printerName;

            if (!printDocument.PrinterSettings.IsValid)
            {
                foreach (Stream stream in streams) stream.Dispose();
                printDocument.Dispose();
                throw new Exception("The selected printer is not available. Open Printer Settings and choose an installed printer.");
            }

            printDocument.DefaultPageSettings.PaperSize = pageSettings.PaperSize;
            printDocument.DefaultPageSettings.Landscape = pageSettings.Landscape;
            printDocument.DefaultPageSettings.Margins = pageSettings.Margins;

            printDocument.PrintPage += delegate(object sender, PrintPageEventArgs e)
            {
                using (Metafile pageImage = new Metafile(streams[currentPageIndex]))
                {
                    Rectangle adjustedRect = new Rectangle(
                        e.PageBounds.Left - (int)e.PageSettings.HardMarginX,
                        e.PageBounds.Top - (int)e.PageSettings.HardMarginY,
                        e.PageBounds.Width,
                        e.PageBounds.Height);
                    e.Graphics.FillRectangle(Brushes.White, adjustedRect);
                    e.Graphics.DrawImage(pageImage, adjustedRect);
                }

                currentPageIndex++;
                e.HasMorePages = currentPageIndex < streams.Count;
            };

            printDocument.EndPrint += delegate
            {
                foreach (Stream stream in streams) stream.Dispose();
                streams.Clear();
                printDocument.Dispose();
            };

            printDocument.Print();
        }
    }
}
