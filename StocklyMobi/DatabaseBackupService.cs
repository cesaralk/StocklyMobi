using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;

namespace StocklyMobi
{
    internal enum DatabaseBackupTrigger
    {
        Manual,
        Automatic
    }

    internal sealed class DatabaseBackupResult
    {
        public bool Success { get; set; }
        public bool Skipped { get; set; }
        public string FilePath { get; set; }
        public string Message { get; set; }
    }

    internal static class DatabaseBackupService
    {
        private const string BackupMutexName = @"Local\StocklyMobi.DatabaseBackup";

        public static DatabaseBackupResult CreateBackup(
            DatabaseBackupTrigger trigger,
            bool force)
        {
            using (Mutex mutex = new Mutex(false, BackupMutexName))
            {
                bool ownsMutex = false;
                try
                {
                    try
                    {
                        ownsMutex = mutex.WaitOne(TimeSpan.Zero, false);
                    }
                    catch (AbandonedMutexException)
                    {
                        ownsMutex = true;
                    }

                    if (!ownsMutex)
                    {
                        return new DatabaseBackupResult
                        {
                            Success = false,
                            Skipped = true,
                            Message = "Another database backup is already running."
                        };
                    }

                    if (trigger == DatabaseBackupTrigger.Automatic &&
                        !force &&
                        !BackupConfiguration.IsAutomaticBackupDue(DateTime.Now))
                    {
                        return new DatabaseBackupResult
                        {
                            Success = true,
                            Skipped = true,
                            Message = "The scheduled backup is already complete for the latest backup time."
                        };
                    }

                    return ExecuteBackup();
                }
                finally
                {
                    if (ownsMutex)
                    {
                        try { mutex.ReleaseMutex(); }
                        catch { }
                    }
                }
            }
        }

        public static string GetEffectiveBackupFolder()
        {
            string configured = BackupConfiguration.ConfiguredBackupFolder;
            if (!string.IsNullOrWhiteSpace(configured))
                return configured.Trim();

            return GetSqlServerDefaultBackupFolder();
        }

        public static string GetSqlServerDefaultBackupFolder()
        {
            using (SqlConnection connection = CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(
                    "SELECT CONVERT(nvarchar(4000), SERVERPROPERTY('InstanceDefaultBackupPath'));",
                    connection))
                {
                    object result = command.ExecuteScalar();
                    string path = result == null || result == DBNull.Value
                        ? string.Empty
                        : Convert.ToString(result);

                    if (!string.IsNullOrWhiteSpace(path))
                        return path.Trim().TrimEnd('\\');
                }

                try
                {
                    using (SqlCommand command = new SqlCommand(
                        "DECLARE @backupDirectory nvarchar(4000); " +
                        "EXEC master.dbo.xp_instance_regread " +
                        "N'HKEY_LOCAL_MACHINE', " +
                        "N'SOFTWARE\\Microsoft\\MSSQLServer\\MSSQLServer', " +
                        "N'BackupDirectory', @backupDirectory OUTPUT; " +
                        "SELECT @backupDirectory;",
                        connection))
                    {
                        object result = command.ExecuteScalar();
                        string path = result == null || result == DBNull.Value
                            ? string.Empty
                            : Convert.ToString(result);
                        if (!string.IsNullOrWhiteSpace(path))
                            return path.Trim().TrimEnd('\\');
                    }
                }
                catch (SqlException)
                {
                    // Older or restricted instances can still use the data-path fallback below.
                }

                using (SqlCommand command = new SqlCommand(
                    "SELECT TOP (1) physical_name " +
                    "FROM master.sys.master_files " +
                    "WHERE database_id = 1 AND file_id = 1;",
                    connection))
                {
                    object result = command.ExecuteScalar();
                    string masterFile = result == null || result == DBNull.Value
                        ? string.Empty
                        : Convert.ToString(result);

                    if (!string.IsNullOrWhiteSpace(masterFile))
                    {
                        string dataFolder = Path.GetDirectoryName(masterFile);
                        if (!string.IsNullOrWhiteSpace(dataFolder))
                        {
                            string instanceFolder = Path.GetDirectoryName(dataFolder);
                            if (!string.IsNullOrWhiteSpace(instanceFolder))
                                return Path.Combine(instanceFolder, "Backup");
                            return dataFolder;
                        }
                    }
                }
            }

            throw new InvalidOperationException(
                "SQL Server did not return a default backup folder. Choose a backup folder in Backup Settings.");
        }

        private static DatabaseBackupResult ExecuteBackup()
        {
            string filePath = string.Empty;
            try
            {
                string connectionString = GetConnectionString();
                SqlConnectionStringBuilder builder =
                    new SqlConnectionStringBuilder(connectionString);

                string databaseName = builder.InitialCatalog;
                if (string.IsNullOrWhiteSpace(databaseName))
                    throw new InvalidOperationException(
                        "The POSDB connection string does not contain an Initial Catalog database name.");

                string folder = GetEffectiveBackupFolder();
                if (string.IsNullOrWhiteSpace(folder))
                    throw new InvalidOperationException("A database backup folder is not configured.");

                bool usingCustomFolder =
                    !string.IsNullOrWhiteSpace(BackupConfiguration.ConfiguredBackupFolder);
                if (usingCustomFolder && !Directory.Exists(folder))
                {
                    try { Directory.CreateDirectory(folder); }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException(
                            "The custom backup folder does not exist and could not be created: " + folder,
                            ex);
                    }
                }

                string safeDatabaseName = MakeSafeFileName(databaseName);
                string fileName = safeDatabaseName + "_" +
                    DateTime.Now.ToString("yyyy-MM-dd_HHmmss") + ".bak";
                filePath = Path.Combine(folder, fileName);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string quotedDatabase = QuoteSqlIdentifier(databaseName);
                    string backupSql =
                        "DECLARE @backupPath nvarchar(4000) = @path; " +
                        "BACKUP DATABASE " + quotedDatabase + " " +
                        "TO DISK = @backupPath " +
                        "WITH COPY_ONLY, INIT, CHECKSUM, " +
                        "NAME = @backupName, DESCRIPTION = @description;";

                    using (SqlCommand command = new SqlCommand(backupSql, connection))
                    {
                        command.CommandTimeout = 0;
                        command.Parameters.Add("@path", SqlDbType.NVarChar, 4000).Value = filePath;
                        command.Parameters.Add("@backupName", SqlDbType.NVarChar, 128).Value =
                            databaseName + " Full Database Backup";
                        command.Parameters.Add("@description", SqlDbType.NVarChar, 255).Value =
                            "StocklyMobi full database backup created " +
                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        command.ExecuteNonQuery();
                    }

                    using (SqlCommand verify = new SqlCommand(
                        "DECLARE @backupPath nvarchar(4000) = @path; " +
                        "RESTORE VERIFYONLY FROM DISK = @backupPath WITH CHECKSUM;",
                        connection))
                    {
                        verify.CommandTimeout = 0;
                        verify.Parameters.Add("@path", SqlDbType.NVarChar, 4000).Value = filePath;
                        verify.ExecuteNonQuery();
                    }
                }

                string successMessage =
                    "Database backup completed and SQL Server verification passed.";
                string statusMessage = successMessage;
                try
                {
                    BackupConfiguration.RecordSuccess(filePath, successMessage);
                }
                catch (Exception settingsException)
                {
                    statusMessage += Environment.NewLine + Environment.NewLine +
                        "The backup file is valid, but StocklyMobi could not save the last-backup status: " +
                        settingsException.Message;
                    BackupLog.Write("STATUS SAVE FAILED | " + settingsException.Message);
                }

                BackupLog.Write("SUCCESS | " + filePath);

                return new DatabaseBackupResult
                {
                    Success = true,
                    Skipped = false,
                    FilePath = filePath,
                    Message = statusMessage
                };
            }
            catch (Exception ex)
            {
                string message = BuildFriendlyError(ex, filePath);
                try { BackupConfiguration.RecordFailure(message); }
                catch { }
                BackupLog.Write("FAILED | " + message);

                return new DatabaseBackupResult
                {
                    Success = false,
                    Skipped = false,
                    FilePath = filePath,
                    Message = message
                };
            }
        }

        private static SqlConnection CreateConnection()
        {
            return new SqlConnection(GetConnectionString());
        }

        private static string GetConnectionString()
        {
            ConnectionStringSettings settings =
                ConfigurationManager.ConnectionStrings["POSDB"];
            if (settings == null || string.IsNullOrWhiteSpace(settings.ConnectionString))
                throw new InvalidOperationException(
                    "The POSDB connection string was not found in App.config.");
            return settings.ConnectionString;
        }

        private static string QuoteSqlIdentifier(string value)
        {
            return "[" + (value ?? string.Empty).Replace("]", "]]" ) + "]";
        }

        private static string MakeSafeFileName(string value)
        {
            string safe = value ?? "StocklyMobi";
            foreach (char invalid in Path.GetInvalidFileNameChars())
                safe = safe.Replace(invalid, '_');
            return string.IsNullOrWhiteSpace(safe) ? "StocklyMobi" : safe;
        }

        private static string BuildFriendlyError(Exception exception, string filePath)
        {
            string message = exception == null
                ? "The database backup failed."
                : exception.Message;

            SqlException sqlException = exception as SqlException;
            if (sqlException != null &&
                message.IndexOf("Operating system error 5", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                message += Environment.NewLine + Environment.NewLine +
                    "SQL Server cannot write to the selected folder. Choose the SQL Server default backup folder, " +
                    "or grant the SQL Server service permission to that folder.";
            }

            if (!string.IsNullOrWhiteSpace(filePath))
                message += Environment.NewLine + Environment.NewLine + "Backup file: " + filePath;

            return message;
        }
    }
}
