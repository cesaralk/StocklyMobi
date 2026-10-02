using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace StocklyMobi
{
    internal static class CashDrawerController
    {
        private const byte Escape = 0x1B;
        private const byte InitializePrinter = 0x40;
        private const byte DrawerPulseCommand = 0x70;

        public const int GenericAsciiProfile = 0;
        public const int StandardBinaryProfile = 1;
        public const int StandardAsciiProfile = 2;
        public const int ShortBinaryProfile = 3;
        public const int ShortAsciiProfile = 4;
        public const int LegacyBinaryProfile = 5;

        [StructLayout(
            LayoutKind.Sequential,
            CharSet = CharSet.Unicode)]
        private sealed class DocInfo1
        {
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pDocName;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string pOutputFile;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string pDataType;
        }

        [DllImport(
            "winspool.drv",
            EntryPoint = "OpenPrinterW",
            SetLastError = true,
            CharSet = CharSet.Unicode)]
        private static extern bool OpenPrinter(
            string printerName,
            out IntPtr printerHandle,
            IntPtr printerDefaults);

        [DllImport(
            "winspool.drv",
            SetLastError = true)]
        private static extern bool ClosePrinter(
            IntPtr printerHandle);

        [DllImport(
            "winspool.drv",
            EntryPoint = "StartDocPrinterW",
            SetLastError = true,
            CharSet = CharSet.Unicode)]
        private static extern int StartDocPrinter(
            IntPtr printerHandle,
            int level,
            [In] DocInfo1 documentInfo);

        [DllImport(
            "winspool.drv",
            SetLastError = true)]
        private static extern bool EndDocPrinter(
            IntPtr printerHandle);

        [DllImport(
            "winspool.drv",
            SetLastError = true)]
        private static extern bool StartPagePrinter(
            IntPtr printerHandle);

        [DllImport(
            "winspool.drv",
            SetLastError = true)]
        private static extern bool EndPagePrinter(
            IntPtr printerHandle);

        [DllImport(
            "winspool.drv",
            SetLastError = true)]
        private static extern bool WritePrinter(
            IntPtr printerHandle,
            IntPtr bytes,
            int byteCount,
            out int bytesWritten);

        public static void OpenConfiguredDrawer()
        {
            Open(
                PrinterConfiguration.ReceiptPrinterName,
                PrinterConfiguration.CashDrawerPin,
                PrinterConfiguration.CashDrawerProfile);
        }

        public static void Open(
            string printerName,
            int drawerPin,
            int profile)
        {
            if (string.IsNullOrWhiteSpace(printerName))
            {
                throw new InvalidOperationException(
                    "No receipt printer is selected. Open Printer Settings and select the printer connected to the cash drawer.");
            }

            if (!PrinterConfiguration.IsInstalled(printerName))
            {
                throw new InvalidOperationException(
                    "The selected receipt printer is not installed or available.");
            }

            byte[] command =
                BuildCommand(
                    drawerPin,
                    profile);

            SendRaw(
                printerName,
                command);
        }

        public static string GetProfileName(
            int profile)
        {
            switch (NormalizeProfile(profile))
            {
                case StandardBinaryProfile:
                    return "Standard binary 0/1";

                case StandardAsciiProfile:
                    return "Standard ASCII 48/49";

                case ShortBinaryProfile:
                    return "Short pulse binary 0/1";

                case ShortAsciiProfile:
                    return "Short pulse ASCII 48/49";

                case LegacyBinaryProfile:
                    return "Legacy binary without initialize";

                default:
                    return "Generic ASCII 48/49 — recommended";
            }
        }

        public static int NormalizeProfile(
            int profile)
        {
            if (profile < GenericAsciiProfile ||
                profile > LegacyBinaryProfile)
            {
                return GenericAsciiProfile;
            }

            return profile;
        }

        private static byte[] BuildCommand(
            int drawerPin,
            int profile)
        {
            int normalized =
                NormalizeProfile(profile);

            bool pinFive =
                drawerPin == 5;

            byte binaryConnector =
                pinFive
                    ? (byte)1
                    : (byte)0;

            byte asciiConnector =
                pinFive
                    ? (byte)49
                    : (byte)48;

            switch (normalized)
            {
                case StandardBinaryProfile:
                    return BuildInitializedPulse(
                        binaryConnector,
                        25,
                        250);

                case StandardAsciiProfile:
                    return BuildInitializedPulse(
                        asciiConnector,
                        25,
                        250);

                case ShortBinaryProfile:
                    return BuildInitializedPulse(
                        binaryConnector,
                        50,
                        100);

                case ShortAsciiProfile:
                    return BuildInitializedPulse(
                        asciiConnector,
                        50,
                        100);

                case LegacyBinaryProfile:
                    return new byte[]
                    {
                        Escape,
                        DrawerPulseCommand,
                        binaryConnector,
                        25,
                        250
                    };

                default:
                    // Many generic 80 mm printers ignore m=0/1 but accept
                    // ASCII '0'/'1'. A medium pulse also works more reliably
                    // than the previous long-off profile on inexpensive units.
                    return BuildInitializedPulse(
                        asciiConnector,
                        60,
                        120);
            }
        }

        private static byte[] BuildInitializedPulse(
            byte connector,
            byte onTime,
            byte offTime)
        {
            return new byte[]
            {
                Escape,
                InitializePrinter,
                Escape,
                DrawerPulseCommand,
                connector,
                onTime,
                offTime
            };
        }

        private static void SendRaw(
            string printerName,
            byte[] command)
        {
            IntPtr printerHandle = IntPtr.Zero;
            IntPtr unmanagedBytes = IntPtr.Zero;
            bool documentStarted = false;
            bool pageStarted = false;

            try
            {
                if (!OpenPrinter(
                    printerName,
                    out printerHandle,
                    IntPtr.Zero))
                {
                    ThrowLastWin32Error(
                        "Windows could not open the receipt printer.");
                }

                DocInfo1 documentInfo =
                    new DocInfo1
                    {
                        pDocName =
                            "StocklyMobi Cash Drawer",
                        pOutputFile = null,
                        pDataType = "RAW"
                    };

                if (StartDocPrinter(
                    printerHandle,
                    1,
                    documentInfo) == 0)
                {
                    ThrowLastWin32Error(
                        "Windows could not start the drawer command.");
                }

                documentStarted = true;

                if (!StartPagePrinter(printerHandle))
                {
                    ThrowLastWin32Error(
                        "Windows could not start the drawer pulse page.");
                }

                pageStarted = true;

                unmanagedBytes =
                    Marshal.AllocCoTaskMem(
                        command.Length);

                Marshal.Copy(
                    command,
                    0,
                    unmanagedBytes,
                    command.Length);

                int written;

                if (!WritePrinter(
                    printerHandle,
                    unmanagedBytes,
                    command.Length,
                    out written))
                {
                    ThrowLastWin32Error(
                        "Windows could not send the drawer pulse.");
                }

                if (written != command.Length)
                {
                    throw new InvalidOperationException(
                        "The receipt printer accepted only part of the cash-drawer command.");
                }
            }
            finally
            {
                if (unmanagedBytes != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(
                        unmanagedBytes);
                }

                if (pageStarted &&
                    printerHandle != IntPtr.Zero)
                {
                    EndPagePrinter(
                        printerHandle);
                }

                if (documentStarted &&
                    printerHandle != IntPtr.Zero)
                {
                    EndDocPrinter(
                        printerHandle);
                }

                if (printerHandle != IntPtr.Zero)
                {
                    ClosePrinter(
                        printerHandle);
                }
            }
        }

        private static void ThrowLastWin32Error(
            string message)
        {
            int error =
                Marshal.GetLastWin32Error();

            throw new Win32Exception(
                error,
                message);
        }
    }
}
