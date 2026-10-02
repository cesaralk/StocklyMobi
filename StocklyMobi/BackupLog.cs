using System;
using System.IO;
using System.Text;

namespace StocklyMobi
{
    internal static class BackupLog
    {
        private static readonly object SyncRoot = new object();

        public static string LogFilePath
        {
            get
            {
                string folder = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "StocklyMobi",
                    "Logs");
                return Path.Combine(folder, "DatabaseBackup.log");
            }
        }

        public static void Write(string message)
        {
            try
            {
                lock (SyncRoot)
                {
                    string path = LogFilePath;
                    string folder = Path.GetDirectoryName(path);
                    if (!string.IsNullOrWhiteSpace(folder))
                        Directory.CreateDirectory(folder);

                    string line = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                        " | " + (message ?? string.Empty) + Environment.NewLine;
                    File.AppendAllText(path, line, Encoding.UTF8);
                }
            }
            catch
            {
                // Logging must never interrupt POS startup or settlement.
            }
        }
    }
}
