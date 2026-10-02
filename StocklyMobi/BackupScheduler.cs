using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;

namespace StocklyMobi
{
    internal sealed class BackupScheduleResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    internal static class BackupScheduler
    {
        public const string TaskName = "StocklyMobi Daily Database Backup";

        public static BackupScheduleResult ApplyCurrentSettings()
        {
            return BackupConfiguration.AutomaticBackupEnabled
                ? CreateOrUpdate(BackupConfiguration.AutomaticBackupTime)
                : Delete();
        }

        public static BackupScheduleResult CreateOrUpdate(TimeSpan time)
        {
            try
            {
                string taskXmlPath = WriteTaskXml(time);
                string arguments =
                    "/Create /TN \"" + TaskName + "\" " +
                    "/XML \"" + taskXmlPath + "\" /F";

                ProcessResult result = RunSchtasks(arguments);
                if (result.ExitCode == 0)
                {
                    string message =
                        "Windows scheduled task is active for " +
                        DateTime.Today.Add(time).ToString("hh:mm tt") +
                        ". It runs under the current Windows user.";
                    BackupLog.Write("SCHEDULE UPDATED | " + message);
                    return new BackupScheduleResult { Success = true, Message = message };
                }

                string error = string.IsNullOrWhiteSpace(result.Error)
                    ? result.Output
                    : result.Error;
                string failedMessage =
                    "Windows could not create the automatic backup task. " +
                    (string.IsNullOrWhiteSpace(error)
                        ? "Unknown Task Scheduler error."
                        : error.Trim());
                BackupLog.Write("SCHEDULE FAILED | " + failedMessage);
                return new BackupScheduleResult { Success = false, Message = failedMessage };
            }
            catch (Exception ex)
            {
                string message = "Windows could not create the automatic backup task. " + ex.Message;
                BackupLog.Write("SCHEDULE FAILED | " + message);
                return new BackupScheduleResult { Success = false, Message = message };
            }
        }

        public static BackupScheduleResult Delete()
        {
            try
            {
                if (!TaskExists())
                {
                    return new BackupScheduleResult
                    {
                        Success = true,
                        Message = "Automatic database backup is disabled."
                    };
                }

                ProcessResult result = RunSchtasks(
                    "/Delete /TN \"" + TaskName + "\" /F");
                bool success = result.ExitCode == 0;
                string detail = string.IsNullOrWhiteSpace(result.Error)
                    ? result.Output
                    : result.Error;
                string message = success
                    ? "Automatic database backup is disabled."
                    : "The scheduled task could not be removed. " + detail.Trim();
                BackupLog.Write((success
                    ? "SCHEDULE DELETED | "
                    : "SCHEDULE DELETE FAILED | ") + message);
                return new BackupScheduleResult { Success = success, Message = message };
            }
            catch (Exception ex)
            {
                return new BackupScheduleResult
                {
                    Success = false,
                    Message = "The scheduled task could not be removed. " + ex.Message
                };
            }
        }

        public static bool TaskExists()
        {
            try
            {
                ProcessResult result = RunSchtasks(
                    "/Query /TN \"" + TaskName + "\" /FO LIST");
                return result.ExitCode == 0;
            }
            catch
            {
                return false;
            }
        }

        private static string WriteTaskXml(TimeSpan time)
        {
            string folder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "StocklyMobi",
                "BackupTask");
            Directory.CreateDirectory(folder);

            string taskXmlPath = Path.Combine(folder, "DatabaseBackupTask.xml");
            string executable = Application.ExecutablePath;
            string workingDirectory = Path.GetDirectoryName(executable) ?? string.Empty;
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            string userName = identity.User == null
                ? identity.Name
                : identity.User.Value;

            DateTime firstRun = DateTime.Today.Add(time);
            if (firstRun <= DateTime.Now)
                firstRun = firstRun.AddDays(1);

            string xml =
                "<?xml version=\"1.0\" encoding=\"UTF-16\"?>\r\n" +
                "<Task version=\"1.2\" xmlns=\"http://schemas.microsoft.com/windows/2004/02/mit/task\">\r\n" +
                "  <RegistrationInfo>\r\n" +
                "    <Description>Creates a verified full backup of the StocklyMobi SQL Server database.</Description>\r\n" +
                "  </RegistrationInfo>\r\n" +
                "  <Triggers>\r\n" +
                "    <CalendarTrigger>\r\n" +
                "      <StartBoundary>" + firstRun.ToString("yyyy-MM-dd'T'HH:mm:ss", CultureInfo.InvariantCulture) + "</StartBoundary>\r\n" +
                "      <Enabled>true</Enabled>\r\n" +
                "      <ScheduleByDay><DaysInterval>1</DaysInterval></ScheduleByDay>\r\n" +
                "    </CalendarTrigger>\r\n" +
                "  </Triggers>\r\n" +
                "  <Principals>\r\n" +
                "    <Principal id=\"Author\">\r\n" +
                "      <UserId>" + EscapeXml(userName) + "</UserId>\r\n" +
                "      <LogonType>InteractiveToken</LogonType>\r\n" +
                "      <RunLevel>LeastPrivilege</RunLevel>\r\n" +
                "    </Principal>\r\n" +
                "  </Principals>\r\n" +
                "  <Settings>\r\n" +
                "    <MultipleInstancesPolicy>IgnoreNew</MultipleInstancesPolicy>\r\n" +
                "    <DisallowStartIfOnBatteries>false</DisallowStartIfOnBatteries>\r\n" +
                "    <StopIfGoingOnBatteries>false</StopIfGoingOnBatteries>\r\n" +
                "    <StartWhenAvailable>true</StartWhenAvailable>\r\n" +
                "    <RunOnlyIfNetworkAvailable>false</RunOnlyIfNetworkAvailable>\r\n" +
                "    <AllowHardTerminate>true</AllowHardTerminate>\r\n" +
                "    <ExecutionTimeLimit>PT2H</ExecutionTimeLimit>\r\n" +
                "    <Priority>7</Priority>\r\n" +
                "    <Enabled>true</Enabled>\r\n" +
                "  </Settings>\r\n" +
                "  <Actions Context=\"Author\">\r\n" +
                "    <Exec>\r\n" +
                "      <Command>" + EscapeXml(executable) + "</Command>\r\n" +
                "      <Arguments>--automatic-database-backup</Arguments>\r\n" +
                "      <WorkingDirectory>" + EscapeXml(workingDirectory) + "</WorkingDirectory>\r\n" +
                "    </Exec>\r\n" +
                "  </Actions>\r\n" +
                "</Task>\r\n";

            File.WriteAllText(taskXmlPath, xml, Encoding.Unicode);
            return taskXmlPath;
        }

        private static string EscapeXml(string value)
        {
            return SecurityElement.Escape(value ?? string.Empty) ?? string.Empty;
        }

        private static ProcessResult RunSchtasks(string arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "schtasks.exe",
                Arguments = arguments,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            using (Process process = Process.Start(startInfo))
            {
                if (process == null)
                    throw new InvalidOperationException(
                        "Task Scheduler process could not be started.");

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                bool exited = process.WaitForExit(15000);
                if (!exited)
                {
                    try { process.Kill(); }
                    catch { }
                }

                return new ProcessResult
                {
                    ExitCode = exited ? process.ExitCode : -1,
                    Output = output ?? string.Empty,
                    Error = error ?? string.Empty
                };
            }
        }

        private sealed class ProcessResult
        {
            public int ExitCode { get; set; }
            public string Output { get; set; }
            public string Error { get; set; }
        }
    }
}
