using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StocklyMobi
{
    public partial class BackupSettingsForm : Form
    {
        private bool _usingSqlDefaultFolder;
        private bool _loading;

        public BackupSettingsForm()
        {
            InitializeComponent();
        }

        private void BackupSettingsForm_Load(object sender, EventArgs e)
        {
            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            _loading = true;
            try
            {
                chkAutomaticBackup.Checked = BackupConfiguration.AutomaticBackupEnabled;
                dtpAutomaticTime.Value = DateTime.Today.Add(
                    BackupConfiguration.AutomaticBackupTime);

                string configured = BackupConfiguration.ConfiguredBackupFolder;
                _usingSqlDefaultFolder = string.IsNullOrWhiteSpace(configured);
                txtBackupFolder.Text = _usingSqlDefaultFolder
                    ? ResolveSqlDefaultFolderForDisplay()
                    : configured;

                UpdateAutomaticControls();
                RefreshStatus();
            }
            finally
            {
                _loading = false;
            }
        }

        private async void BtnBackupNow_Click(object sender, EventArgs e)
        {
            BackupScheduleResult scheduleResult;
            try
            {
                scheduleResult = SaveCurrentConfiguration(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Backup Settings",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            SetBusy(true, "Creating and verifying the full database backup...");
            try
            {
                DatabaseBackupResult result = await Task.Run(() =>
                    DatabaseBackupService.CreateBackup(
                        DatabaseBackupTrigger.Manual,
                        true));

                RefreshStatus();

                if (result.Success)
                {
                    MessageBox.Show(
                        result.Message + Environment.NewLine + Environment.NewLine +
                        result.FilePath +
                        (scheduleResult.Success
                            ? string.Empty
                            : Environment.NewLine + Environment.NewLine + scheduleResult.Message),
                        "Database Backup",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        result.Message,
                        "Database Backup Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Database Backup Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            finally
            {
                SetBusy(false, string.Empty);
            }
        }

        private void BtnSaveSchedule_Click(object sender, EventArgs e)
        {
            try
            {
                BackupScheduleResult result = SaveCurrentConfiguration(true);
                RefreshStatus();

                MessageBox.Show(
                    result.Message,
                    result.Success ? "Backup Settings" : "Backup Schedule Warning",
                    MessageBoxButtons.OK,
                    result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Backup Settings",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void BtnChooseFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description =
                    "Choose a folder that the SQL Server service is allowed to write to.";
                dialog.ShowNewFolderButton = true;

                if (Directory.Exists(txtBackupFolder.Text))
                    dialog.SelectedPath = txtBackupFolder.Text;

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    _usingSqlDefaultFolder = false;
                    txtBackupFolder.Text = dialog.SelectedPath;
                    lblFolderMode.Text = "Custom folder";
                }
            }
        }

        private void BtnUseSqlDefault_Click(object sender, EventArgs e)
        {
            _usingSqlDefaultFolder = true;
            txtBackupFolder.Text = ResolveSqlDefaultFolderForDisplay();
            lblFolderMode.Text = "SQL Server default folder — recommended";
        }

        private void BtnOpenFolder_Click(object sender, EventArgs e)
        {
            OpenBackupFolderInExplorer();
        }

        private void BtnOpenLog_Click(object sender, EventArgs e)
        {
            string logPath = BackupLog.LogFilePath;
            if (!File.Exists(logPath))
            {
                MessageBox.Show(
                    "No database-backup log has been created yet.",
                    "Backup Log",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            OpenPath(logPath, "The backup log could not be opened.");
        }

        private void ChkAutomaticBackup_CheckedChanged(object sender, EventArgs e)
        {
            if (!_loading) UpdateAutomaticControls();
        }

        private BackupScheduleResult SaveCurrentConfiguration(bool validateFolder)
        {
            string folderToSave = _usingSqlDefaultFolder
                ? string.Empty
                : (txtBackupFolder.Text ?? string.Empty).Trim();

            if (validateFolder &&
                !_usingSqlDefaultFolder &&
                string.IsNullOrWhiteSpace(folderToSave))
            {
                throw new InvalidOperationException(
                    "Choose a backup folder or use the SQL Server default folder.");
            }

            BackupConfiguration.SaveSchedule(
                chkAutomaticBackup.Checked,
                dtpAutomaticTime.Value.TimeOfDay,
                folderToSave);

            BackupScheduleResult result = BackupScheduler.ApplyCurrentSettings();
            lblSchedulerStatus.Text = result.Message;
            lblSchedulerStatus.ForeColor = result.Success
                ? System.Drawing.Color.FromArgb(22, 101, 52)
                : System.Drawing.Color.FromArgb(185, 28, 28);
            return result;
        }

        private string ResolveSqlDefaultFolderForDisplay()
        {
            try
            {
                lblFolderMode.Text = "SQL Server default folder — recommended";
                return DatabaseBackupService.GetSqlServerDefaultBackupFolder();
            }
            catch (Exception ex)
            {
                lblFolderMode.Text = "SQL Server default folder could not be detected";
                lblFolderMode.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
                BackupLog.Write("DEFAULT BACKUP FOLDER LOOKUP FAILED | " + ex.Message);
                return string.Empty;
            }
        }

        private void RefreshStatus()
        {
            DateTime? lastSuccess = BackupConfiguration.LastSuccessfulBackupLocal;
            lblLastSuccessfulValue.Text = lastSuccess.HasValue
                ? lastSuccess.Value.ToString("yyyy-MM-dd  hh:mm:ss tt")
                : "No successful backup recorded";

            string lastFile = BackupConfiguration.LastBackupFile;
            lblLastFileValue.Text = string.IsNullOrWhiteSpace(lastFile)
                ? "—"
                : lastFile;

            DateTime? attempt = BackupConfiguration.LastBackupAttemptLocal;
            string status = BackupConfiguration.LastBackupStatus;
            string message = BackupConfiguration.LastBackupMessage;
            lblLastResultValue.Text =
                (attempt.HasValue
                    ? attempt.Value.ToString("yyyy-MM-dd  hh:mm:ss tt") + " — "
                    : string.Empty) +
                status +
                (string.IsNullOrWhiteSpace(message) ? string.Empty : ": " + message);

            bool taskExists = BackupScheduler.TaskExists();
            if (!chkAutomaticBackup.Checked)
            {
                lblSchedulerStatus.Text = "Automatic database backup is disabled.";
                lblSchedulerStatus.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            }
            else if (taskExists)
            {
                lblSchedulerStatus.Text =
                    "Windows task is installed. Next daily time: " +
                    dtpAutomaticTime.Value.ToString("hh:mm tt") +
                    ". Missed backups are checked again when StocklyMobi starts.";
                lblSchedulerStatus.ForeColor = System.Drawing.Color.FromArgb(22, 101, 52);
            }
            else
            {
                lblSchedulerStatus.Text =
                    "The Windows task is not installed yet. Press Save Schedule.";
                lblSchedulerStatus.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
            }

            lblFolderMode.Text = _usingSqlDefaultFolder
                ? "SQL Server default folder — recommended"
                : "Custom folder";
            lblFolderMode.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
        }

        private void UpdateAutomaticControls()
        {
            dtpAutomaticTime.Enabled = chkAutomaticBackup.Checked;
            lblAutomaticTime.Enabled = chkAutomaticBackup.Checked;
        }

        private void SetBusy(bool busy, string message)
        {
            btnBackupNow.Enabled = !busy;
            btnSaveSchedule.Enabled = !busy;
            btnChooseFolder.Enabled = !busy;
            btnUseSqlDefault.Enabled = !busy;
            UseWaitCursor = busy;
            lblProgress.Text = message ?? string.Empty;
            Application.DoEvents();
        }

        private void OpenBackupFolderInExplorer()
        {
            string folder = (txtBackupFolder.Text ?? string.Empty).Trim();
            string lastBackupFile = BackupConfiguration.LastBackupFile;

            try
            {
                if (string.IsNullOrWhiteSpace(folder))
                    folder = DatabaseBackupService.GetEffectiveBackupFolder();

                if (string.IsNullOrWhiteSpace(folder))
                    throw new DirectoryNotFoundException(
                        "No backup folder is currently configured.");

                string lastBackupFolder = string.IsNullOrWhiteSpace(lastBackupFile)
                    ? string.Empty
                    : Path.GetDirectoryName(lastBackupFile);

                bool selectLastBackup =
                    !string.IsNullOrWhiteSpace(lastBackupFile) &&
                    File.Exists(lastBackupFile) &&
                    string.Equals(
                        NormalizeFolder(folder),
                        NormalizeFolder(lastBackupFolder),
                        StringComparison.OrdinalIgnoreCase);

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "explorer.exe",
                    UseShellExecute = true,
                    Arguments = selectLastBackup
                        ? "/select,\"" + lastBackupFile + "\""
                        : "\"" + folder + "\""
                };

                Process process = Process.Start(startInfo);
                if (process == null)
                    throw new InvalidOperationException(
                        "Windows Explorer did not start.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "The backup folder could not be opened." +
                    Environment.NewLine + Environment.NewLine +
                    "Folder:" + Environment.NewLine + folder +
                    Environment.NewLine + Environment.NewLine +
                    ex.Message +
                    Environment.NewLine + Environment.NewLine +
                    "When SQL Default is selected, Windows may block access to the SQL Server service folder. " +
                    "Use Change to select a shared folder that both Windows and SQL Server can access.",
                    "Database Backup",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private static string NormalizeFolder(string folder)
        {
            if (string.IsNullOrWhiteSpace(folder))
                return string.Empty;

            try
            {
                return Path.GetFullPath(folder.Trim())
                    .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            }
            catch
            {
                return folder.Trim()
                    .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            }
        }

        private void OpenPath(string path, string errorMessage)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(path) ||
                    (!Directory.Exists(path) && !File.Exists(path)))
                {
                    throw new FileNotFoundException(errorMessage);
                }

                Process.Start(new ProcessStartInfo
                {
                    FileName = path,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    errorMessage + Environment.NewLine + Environment.NewLine + ex.Message,
                    "Database Backup",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
    }
}
