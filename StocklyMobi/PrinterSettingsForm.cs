using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace StocklyMobi
{
    public partial class PrinterSettingsForm : Form
    {
        public PrinterSettingsForm()
        {
            InitializeComponent();
        }

        private void PrinterSettingsForm_Load(object sender, EventArgs e)
        {
            LoadPrinters();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadPrinters();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string receipt = Convert.ToString(cboReceiptPrinter.SelectedItem);
            string invoice = Convert.ToString(cboInvoicePrinter.SelectedItem);

            if (string.IsNullOrWhiteSpace(receipt))
            {
                MessageBox.Show("Select the cashier receipt printer.", "Printer Settings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(invoice))
            {
                MessageBox.Show("Select the A4 stock-in invoice printer.", "Printer Settings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Properties.Settings.Default.ReceiptPrinterName = receipt;
            Properties.Settings.Default.InvoicePrinterName = invoice;
            Properties.Settings.Default.AutoPrintReceipt = chkAutoPrintReceipt.Checked;
            Properties.Settings.Default.AutoOpenCashDrawer = chkAutoOpenCashDrawer.Checked;
            Properties.Settings.Default.CashDrawerPin = GetSelectedDrawerPin();
            Properties.Settings.Default.CashDrawerProfile = GetSelectedDrawerProfile();
            Properties.Settings.Default.ReceiptTopTrimMm = numReceiptTopTrim.Value;
            Properties.Settings.Default.Save();

            lblSaved.Text = "Saved " + DateTime.Now.ToString("hh:mm:ss tt");
            MessageBox.Show("Printer settings were saved.", "Printer Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnTestReceipt_Click(object sender, EventArgs e)
        {
            PrintTest(Convert.ToString(cboReceiptPrinter.SelectedItem), false);
        }

        private void BtnTestInvoice_Click(object sender, EventArgs e)
        {
            PrintTest(Convert.ToString(cboInvoicePrinter.SelectedItem), true);
        }

        private void BtnTestDrawer_Click(
            object sender,
            EventArgs e)
        {
            string printerName =
                Convert.ToString(
                    cboReceiptPrinter.SelectedItem);

            if (string.IsNullOrWhiteSpace(printerName))
            {
                MessageBox.Show(
                    "Select the receipt printer connected to the cash drawer.",
                    "Cash Drawer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int pin =
                GetSelectedDrawerPin();

            int profile =
                GetSelectedDrawerProfile();

            try
            {
                CashDrawerController.Open(
                    printerName,
                    pin,
                    profile);

                DialogResult opened =
                    MessageBox.Show(
                        "Windows accepted the drawer command.\r\n\r\n" +
                        "Profile: " +
                        CashDrawerController.GetProfileName(profile) +
                        "\r\nConnector: Pin " +
                        pin +
                        "\r\n\r\nDid the drawer physically open?",
                        "Cash Drawer Test",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                if (opened == DialogResult.Yes)
                {
                    Properties.Settings.Default.ReceiptPrinterName =
                        printerName;

                    Properties.Settings.Default.AutoOpenCashDrawer =
                        true;

                    Properties.Settings.Default.CashDrawerPin =
                        pin;

                    Properties.Settings.Default.CashDrawerProfile =
                        profile;

                    Properties.Settings.Default.Save();

                    chkAutoOpenCashDrawer.Checked =
                        true;

                    lblSaved.Text =
                        "Working drawer profile saved " +
                        DateTime.Now.ToString(
                            "hh:mm:ss tt");

                    MessageBox.Show(
                        "The working drawer profile was saved and will be used after Settle.",
                        "Cash Drawer",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                int nextProfile =
                    profile + 1;

                if (nextProfile <
                    cboDrawerProfile.Items.Count)
                {
                    cboDrawerProfile.SelectedIndex =
                        nextProfile;

                    MessageBox.Show(
                        "The next compatibility profile is selected:\r\n\r\n" +
                        Convert.ToString(
                            cboDrawerProfile.SelectedItem) +
                        "\r\n\r\nPress Test Drawer again.",
                        "Cash Drawer",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        "All available command profiles have been tested.\r\n\r\n" +
                        "Check that the drawer cable is connected to the printer's DK / Cash Drawer port, then check the printer driver for a cash-drawer option.\r\n\r\n" +
                        "The exact printer brand and model will be needed for a manufacturer-specific command.",
                        "Cash Drawer",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.GetBaseException().Message,
                    "Cash Drawer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void LoadPrinters()
        {
            string selectedReceipt = Properties.Settings.Default.ReceiptPrinterName;
            string selectedInvoice = Properties.Settings.Default.InvoicePrinterName;
            string defaultPrinter = PrinterConfiguration.DefaultPrinterName;

            cboReceiptPrinter.BeginUpdate();
            cboInvoicePrinter.BeginUpdate();
            try
            {
                cboReceiptPrinter.Items.Clear();
                cboInvoicePrinter.Items.Clear();

                foreach (string printer in PrinterSettings.InstalledPrinters)
                {
                    cboReceiptPrinter.Items.Add(printer);
                    cboInvoicePrinter.Items.Add(printer);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Windows could not load the installed printers.\r\n\r\n" + ex.Message,
                    "Printer Settings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                cboReceiptPrinter.EndUpdate();
                cboInvoicePrinter.EndUpdate();
            }

            SelectPrinter(cboReceiptPrinter, selectedReceipt, defaultPrinter);
            SelectPrinter(cboInvoicePrinter, selectedInvoice, defaultPrinter);
            chkAutoPrintReceipt.Checked = Properties.Settings.Default.AutoPrintReceipt;
            chkAutoOpenCashDrawer.Checked = Properties.Settings.Default.AutoOpenCashDrawer;
            SelectDrawerPin(Properties.Settings.Default.CashDrawerPin);
            SelectDrawerProfile(Properties.Settings.Default.CashDrawerProfile);

            decimal savedTrim = Properties.Settings.Default.ReceiptTopTrimMm;

            if (savedTrim < numReceiptTopTrim.Minimum ||
                savedTrim > numReceiptTopTrim.Maximum)
            {
                savedTrim = 0m;
                Properties.Settings.Default.ReceiptTopTrimMm = 0m;
                Properties.Settings.Default.Save();
            }

            numReceiptTopTrim.Value = savedTrim;

            lblDefaultPrinter.Text = string.IsNullOrWhiteSpace(defaultPrinter)
                ? "Windows default printer: none"
                : "Windows default printer: " + defaultPrinter;
        }

        private int GetSelectedDrawerPin()
        {
            return cboDrawerPin.SelectedIndex == 1
                ? 5
                : 2;
        }

        private void SelectDrawerPin(
            int drawerPin)
        {
            cboDrawerPin.SelectedIndex =
                drawerPin == 5
                    ? 1
                    : 0;
        }

        private int GetSelectedDrawerProfile()
        {
            return CashDrawerController.NormalizeProfile(
                cboDrawerProfile.SelectedIndex);
        }

        private void SelectDrawerProfile(
            int drawerProfile)
        {
            int normalized =
                CashDrawerController.NormalizeProfile(
                    drawerProfile);

            if (cboDrawerProfile.Items.Count == 0)
                return;

            cboDrawerProfile.SelectedIndex =
                Math.Min(
                    normalized,
                    cboDrawerProfile.Items.Count - 1);
        }

        private static void SelectPrinter(ComboBox comboBox, string savedPrinter, string fallbackPrinter)
        {
            int index = FindPrinterIndex(comboBox, savedPrinter);
            if (index < 0) index = FindPrinterIndex(comboBox, fallbackPrinter);
            if (index < 0 && comboBox.Items.Count > 0) index = 0;
            comboBox.SelectedIndex = index;
        }

        private static int FindPrinterIndex(ComboBox comboBox, string printerName)
        {
            if (string.IsNullOrWhiteSpace(printerName)) return -1;
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if (string.Equals(Convert.ToString(comboBox.Items[i]), printerName, StringComparison.OrdinalIgnoreCase))
                    return i;
            }
            return -1;
        }

        private void PrintTest(string printerName, bool a4)
        {
            if (string.IsNullOrWhiteSpace(printerName))
            {
                MessageBox.Show("Select a printer first.", "Printer Test", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (PrintDocument document = new PrintDocument())
                {
                    document.PrinterSettings.PrinterName = printerName;
                    if (!document.PrinterSettings.IsValid)
                        throw new InvalidOperationException("The selected printer is not available.");

                    if (a4)
                    {
                        document.DocumentName = "StocklyMobi A4 Printer Test";
                        document.DefaultPageSettings.PaperSize = PrinterConfiguration.FindA4PaperSize(document.PrinterSettings);
                        document.DefaultPageSettings.Landscape = false;
                        document.DefaultPageSettings.Margins = new Margins(45, 45, 45, 45);
                    }
                    else
                    {
                        document.DocumentName = "StocklyMobi Receipt Printer Test";
                        document.DefaultPageSettings.PaperSize = new PaperSize("Receipt 80mm", 315, 120);
                        document.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                        document.OriginAtMargins = false;
                    }

                    document.PrintPage += delegate(object sender, PrintPageEventArgs args)
                    {
                        using (Font title = new Font("Arial", a4 ? 18F : 12F, FontStyle.Bold))
                        using (Font normal = new Font("Arial", a4 ? 11F : 9F))
                        {
                            float x;
                            float y;

                            if (a4)
                            {
                                x = args.MarginBounds.Left;
                                y = args.MarginBounds.Top;
                            }
                            else
                            {
                                PrinterConfiguration.ApplyReceiptOriginCompensation(
                                    args.Graphics,
                                    args.PageSettings);
                                x = PrinterConfiguration
                                    .ReceiptSidePaddingHundredths;
                                y = 0f;
                            }

                            args.Graphics.DrawString("StocklyMobi Printer Test", title, Brushes.Black, x, y);
                            y += a4 ? 38 : 28;
                            args.Graphics.DrawString(a4 ? "Section: Stock-In / Invoice History (A4)" : "Section: Cashier Receipt (80mm)", normal, Brushes.Black, x, y);
                            y += a4 ? 28 : 22;
                            args.Graphics.DrawString("Printer: " + printerName, normal, Brushes.Black, x, y);
                            y += a4 ? 28 : 22;
                            args.Graphics.DrawString("Printed: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), normal, Brushes.Black, x, y);
                        }
                        args.HasMorePages = false;
                    };

                    document.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Printer Test", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
