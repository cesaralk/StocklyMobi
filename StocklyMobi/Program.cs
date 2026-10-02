using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StocklyMobi
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (IsAutomaticBackupCommand(args))
            {
                RunAutomaticDatabaseBackup();
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                DatabaseBootstrapper.EnsureDatabaseAndSchema();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "The StocklyMobi database could not be created or upgraded.\n\n" + ex.Message +
                    "\n\nYou can also run Database_Full.sql manually in SQL Server Management Studio.",
                    "Database Setup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            BackupRuntimeMonitor.Start();
            Application.Run(new Login());
        }

        private static bool IsAutomaticBackupCommand(string[] args)
        {
            return args != null && args.Any(argument =>
                string.Equals(
                    argument,
                    "--automatic-database-backup",
                    StringComparison.OrdinalIgnoreCase));
        }

        private static void RunAutomaticDatabaseBackup()
        {
            try
            {
                DatabaseBackupResult result = DatabaseBackupService.CreateBackup(
                    DatabaseBackupTrigger.Automatic,
                    false);
                BackupLog.Write(
                    "SCHEDULED PROCESS | " + result.Message +
                    (string.IsNullOrWhiteSpace(result.FilePath)
                        ? string.Empty
                        : " | " + result.FilePath));
                Environment.ExitCode = result.Success || result.Skipped ? 0 : 1;
            }
            catch (Exception ex)
            {
                try { BackupConfiguration.RecordFailure(ex.Message); }
                catch { }
                BackupLog.Write("SCHEDULED PROCESS FAILED | " + ex);
                Environment.ExitCode = 1;
            }
        }
    }
}
