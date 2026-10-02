using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StocklyMobi
{
    internal static class BackupRuntimeMonitor
    {
        private static Timer _timer;
        private static volatile bool _checkRunning;

        public static void Start()
        {
            if (_timer != null) return;

            _timer = new Timer();
            _timer.Interval = 60000;
            _timer.Tick += Timer_Tick;
            _timer.Start();

            Task.Run((Action)InitializeAndCatchUp);
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            if (_checkRunning || !BackupConfiguration.AutomaticBackupEnabled)
                return;

            if (!BackupConfiguration.IsAutomaticBackupDue(DateTime.Now))
                return;

            _checkRunning = true;
            Task.Run(() =>
            {
                try
                {
                    DatabaseBackupService.CreateBackup(
                        DatabaseBackupTrigger.Automatic,
                        false);
                }
                finally
                {
                    _checkRunning = false;
                }
            });
        }

        private static void InitializeAndCatchUp()
        {
            try
            {
                BackupScheduleResult schedule = BackupScheduler.ApplyCurrentSettings();
                BackupLog.Write("STARTUP SCHEDULE CHECK | " + schedule.Message);
            }
            catch (Exception ex)
            {
                BackupLog.Write("STARTUP SCHEDULE CHECK FAILED | " + ex.Message);
            }

            try
            {
                if (BackupConfiguration.AutomaticBackupEnabled &&
                    BackupConfiguration.IsAutomaticBackupDue(DateTime.Now))
                {
                    DatabaseBackupService.CreateBackup(
                        DatabaseBackupTrigger.Automatic,
                        false);
                }
            }
            catch (Exception ex)
            {
                BackupLog.Write("STARTUP CATCH-UP FAILED | " + ex.Message);
            }
        }
    }
}
