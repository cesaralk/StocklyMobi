using System;
using System.Globalization;

namespace StocklyMobi
{
    internal static class BackupConfiguration
    {
        public static readonly TimeSpan DefaultAutomaticTime = new TimeSpan(23, 0, 0);

        public static bool AutomaticBackupEnabled
        {
            get
            {
                try { return Properties.Settings.Default.AutomaticDatabaseBackupEnabled; }
                catch { return true; }
            }
        }

        public static TimeSpan AutomaticBackupTime
        {
            get
            {
                string value = string.Empty;
                try { value = Properties.Settings.Default.AutomaticDatabaseBackupTime; }
                catch { }

                TimeSpan parsed;
                return TimeSpan.TryParseExact(
                    value,
                    @"hh\:mm",
                    CultureInfo.InvariantCulture,
                    out parsed)
                    ? parsed
                    : DefaultAutomaticTime;
            }
        }

        public static string ConfiguredBackupFolder
        {
            get
            {
                try { return Properties.Settings.Default.DatabaseBackupFolder ?? string.Empty; }
                catch { return string.Empty; }
            }
        }

        public static DateTime? LastSuccessfulBackupLocal
        {
            get
            {
                string value = string.Empty;
                try { value = Properties.Settings.Default.LastDatabaseBackupUtc; }
                catch { }

                DateTime parsed;
                if (DateTime.TryParse(
                    value,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.RoundtripKind,
                    out parsed))
                {
                    if (parsed.Kind == DateTimeKind.Unspecified)
                        parsed = DateTime.SpecifyKind(parsed, DateTimeKind.Utc);
                    return parsed.ToLocalTime();
                }

                return null;
            }
        }

        public static string LastBackupFile
        {
            get
            {
                try { return Properties.Settings.Default.LastDatabaseBackupFile ?? string.Empty; }
                catch { return string.Empty; }
            }
        }

        public static string LastBackupStatus
        {
            get
            {
                try { return Properties.Settings.Default.LastDatabaseBackupStatus ?? "Never"; }
                catch { return "Never"; }
            }
        }

        public static string LastBackupMessage
        {
            get
            {
                try { return Properties.Settings.Default.LastDatabaseBackupMessage ?? string.Empty; }
                catch { return string.Empty; }
            }
        }

        public static DateTime? LastBackupAttemptLocal
        {
            get
            {
                string value = string.Empty;
                try { value = Properties.Settings.Default.LastDatabaseBackupAttemptUtc; }
                catch { }

                DateTime parsed;
                if (DateTime.TryParse(
                    value,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.RoundtripKind,
                    out parsed))
                {
                    if (parsed.Kind == DateTimeKind.Unspecified)
                        parsed = DateTime.SpecifyKind(parsed, DateTimeKind.Utc);
                    return parsed.ToLocalTime();
                }

                return null;
            }
        }

        public static void SaveSchedule(bool enabled, TimeSpan time, string folder)
        {
            if (time < TimeSpan.Zero || time >= TimeSpan.FromDays(1))
                throw new ArgumentOutOfRangeException("time", "Backup time must be within one day.");

            Properties.Settings.Default.AutomaticDatabaseBackupEnabled = enabled;
            Properties.Settings.Default.AutomaticDatabaseBackupTime =
                time.ToString(@"hh\:mm", CultureInfo.InvariantCulture);
            Properties.Settings.Default.DatabaseBackupFolder = folder ?? string.Empty;
            Properties.Settings.Default.Save();
        }

        public static bool IsAutomaticBackupDue(DateTime nowLocal)
        {
            if (!AutomaticBackupEnabled) return false;

            DateTime scheduledToday = nowLocal.Date.Add(AutomaticBackupTime);
            DateTime latestScheduledOccurrence =
                nowLocal >= scheduledToday
                    ? scheduledToday
                    : scheduledToday.AddDays(-1);

            DateTime? lastSuccess = LastSuccessfulBackupLocal;
            return !lastSuccess.HasValue || lastSuccess.Value < latestScheduledOccurrence;
        }

        public static void RecordSuccess(string filePath, string message)
        {
            DateTime utcNow = DateTime.UtcNow;
            Properties.Settings.Default.LastDatabaseBackupUtc =
                utcNow.ToString("o", CultureInfo.InvariantCulture);
            Properties.Settings.Default.LastDatabaseBackupAttemptUtc =
                utcNow.ToString("o", CultureInfo.InvariantCulture);
            Properties.Settings.Default.LastDatabaseBackupFile = filePath ?? string.Empty;
            Properties.Settings.Default.LastDatabaseBackupStatus = "Success";
            Properties.Settings.Default.LastDatabaseBackupMessage = message ?? string.Empty;
            Properties.Settings.Default.Save();
        }

        public static void RecordFailure(string message)
        {
            Properties.Settings.Default.LastDatabaseBackupAttemptUtc =
                DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture);
            Properties.Settings.Default.LastDatabaseBackupStatus = "Failed";
            Properties.Settings.Default.LastDatabaseBackupMessage = message ?? string.Empty;
            Properties.Settings.Default.Save();
        }
    }
}
