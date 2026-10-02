namespace StocklyMobi
{
    partial class BackupSettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.TableLayoutPanel tblContent;
        private System.Windows.Forms.Panel pnlManualCard;
        private System.Windows.Forms.Label lblManualTitle;
        private System.Windows.Forms.Label lblManualHelp;
        private System.Windows.Forms.Button btnBackupNow;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Panel pnlScheduleCard;
        private System.Windows.Forms.TableLayoutPanel tblSchedule;
        private System.Windows.Forms.Label lblScheduleTitle;
        private System.Windows.Forms.CheckBox chkAutomaticBackup;
        private System.Windows.Forms.Label lblAutomaticTime;
        private System.Windows.Forms.DateTimePicker dtpAutomaticTime;
        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.TextBox txtBackupFolder;
        private System.Windows.Forms.FlowLayoutPanel pnlFolderActions;
        private System.Windows.Forms.Button btnChooseFolder;
        private System.Windows.Forms.Button btnUseSqlDefault;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.Label lblFolderMode;
        private System.Windows.Forms.FlowLayoutPanel pnlScheduleActions;
        private System.Windows.Forms.Button btnSaveSchedule;
        private System.Windows.Forms.Button btnOpenLog;
        private System.Windows.Forms.Label lblSchedulerStatus;
        private System.Windows.Forms.Panel pnlStatusCard;
        private System.Windows.Forms.TableLayoutPanel tblStatus;
        private System.Windows.Forms.Label lblStatusTitle;
        private System.Windows.Forms.Label lblLastSuccessful;
        private System.Windows.Forms.Label lblLastSuccessfulValue;
        private System.Windows.Forms.Label lblLastFile;
        private System.Windows.Forms.Label lblLastFileValue;
        private System.Windows.Forms.Label lblLastResult;
        private System.Windows.Forms.Label lblLastResultValue;
        private System.Windows.Forms.Label lblPermissionHelp;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.tblContent = new System.Windows.Forms.TableLayoutPanel();
            this.pnlManualCard = new System.Windows.Forms.Panel();
            this.lblProgress = new System.Windows.Forms.Label();
            this.btnBackupNow = new System.Windows.Forms.Button();
            this.lblManualHelp = new System.Windows.Forms.Label();
            this.lblManualTitle = new System.Windows.Forms.Label();
            this.pnlScheduleCard = new System.Windows.Forms.Panel();
            this.tblSchedule = new System.Windows.Forms.TableLayoutPanel();
            this.lblScheduleTitle = new System.Windows.Forms.Label();
            this.chkAutomaticBackup = new System.Windows.Forms.CheckBox();
            this.lblAutomaticTime = new System.Windows.Forms.Label();
            this.dtpAutomaticTime = new System.Windows.Forms.DateTimePicker();
            this.lblFolder = new System.Windows.Forms.Label();
            this.txtBackupFolder = new System.Windows.Forms.TextBox();
            this.pnlFolderActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnChooseFolder = new System.Windows.Forms.Button();
            this.btnUseSqlDefault = new System.Windows.Forms.Button();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.lblFolderMode = new System.Windows.Forms.Label();
            this.pnlScheduleActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSaveSchedule = new System.Windows.Forms.Button();
            this.btnOpenLog = new System.Windows.Forms.Button();
            this.lblSchedulerStatus = new System.Windows.Forms.Label();
            this.lblPermissionHelp = new System.Windows.Forms.Label();
            this.pnlStatusCard = new System.Windows.Forms.Panel();
            this.tblStatus = new System.Windows.Forms.TableLayoutPanel();
            this.lblStatusTitle = new System.Windows.Forms.Label();
            this.lblLastSuccessful = new System.Windows.Forms.Label();
            this.lblLastSuccessfulValue = new System.Windows.Forms.Label();
            this.lblLastFile = new System.Windows.Forms.Label();
            this.lblLastFileValue = new System.Windows.Forms.Label();
            this.lblLastResult = new System.Windows.Forms.Label();
            this.lblLastResultValue = new System.Windows.Forms.Label();
            this.pnlContent.SuspendLayout();
            this.tblContent.SuspendLayout();
            this.pnlManualCard.SuspendLayout();
            this.pnlScheduleCard.SuspendLayout();
            this.tblSchedule.SuspendLayout();
            this.pnlFolderActions.SuspendLayout();
            this.pnlScheduleActions.SuspendLayout();
            this.pnlStatusCard.SuspendLayout();
            this.tblStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Century Gothic", 15F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Padding = new System.Windows.Forms.Padding(22, 10, 10, 4);
            this.lblHeader.Size = new System.Drawing.Size(1040, 76);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Database Backup\r\nCreate a full SQL Server backup now or schedule a verified daily" +
    " backup.";
            // 
            // pnlContent
            // 
            this.pnlContent.AutoScroll = true;
            this.pnlContent.Controls.Add(this.tblContent);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 76);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(22);
            this.pnlContent.Size = new System.Drawing.Size(1040, 604);
            this.pnlContent.TabIndex = 1;
            // 
            // tblContent
            // 
            this.tblContent.AutoSize = true;
            this.tblContent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblContent.ColumnCount = 1;
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContent.Controls.Add(this.pnlManualCard, 0, 0);
            this.tblContent.Controls.Add(this.pnlScheduleCard, 0, 1);
            this.tblContent.Controls.Add(this.pnlStatusCard, 0, 2);
            this.tblContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblContent.Location = new System.Drawing.Point(22, 22);
            this.tblContent.Name = "tblContent";
            this.tblContent.RowCount = 3;
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 146F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 380F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 248F));
            this.tblContent.Size = new System.Drawing.Size(979, 774);
            this.tblContent.TabIndex = 0;
            // 
            // pnlManualCard
            // 
            this.pnlManualCard.BackColor = System.Drawing.Color.White;
            this.pnlManualCard.Controls.Add(this.lblProgress);
            this.pnlManualCard.Controls.Add(this.btnBackupNow);
            this.pnlManualCard.Controls.Add(this.lblManualHelp);
            this.pnlManualCard.Controls.Add(this.lblManualTitle);
            this.pnlManualCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlManualCard.Location = new System.Drawing.Point(0, 0);
            this.pnlManualCard.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.pnlManualCard.Name = "pnlManualCard";
            this.pnlManualCard.Padding = new System.Windows.Forms.Padding(22);
            this.pnlManualCard.Size = new System.Drawing.Size(979, 132);
            this.pnlManualCard.TabIndex = 0;
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProgress.Font = new System.Drawing.Font("Century Gothic", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblProgress.Location = new System.Drawing.Point(24, 94);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(700, 24);
            this.lblProgress.TabIndex = 3;
            // 
            // btnBackupNow
            // 
            this.btnBackupNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBackupNow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnBackupNow.FlatAppearance.BorderSize = 0;
            this.btnBackupNow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackupNow.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.btnBackupNow.ForeColor = System.Drawing.Color.White;
            this.btnBackupNow.Location = new System.Drawing.Point(765, 45);
            this.btnBackupNow.Name = "btnBackupNow";
            this.btnBackupNow.Size = new System.Drawing.Size(188, 50);
            this.btnBackupNow.TabIndex = 2;
            this.btnBackupNow.Text = "Backup Now";
            this.btnBackupNow.UseVisualStyleBackColor = false;
            this.btnBackupNow.Click += new System.EventHandler(this.BtnBackupNow_Click);
            // 
            // lblManualHelp
            // 
            this.lblManualHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblManualHelp.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.lblManualHelp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblManualHelp.Location = new System.Drawing.Point(24, 52);
            this.lblManualHelp.Name = "lblManualHelp";
            this.lblManualHelp.Size = new System.Drawing.Size(700, 40);
            this.lblManualHelp.TabIndex = 1;
            this.lblManualHelp.Text = "Creates a complete .bak file and runs RESTORE VERIFYONLY before reporting success" +
    ".";
            // 
            // lblManualTitle
            // 
            this.lblManualTitle.AutoSize = true;
            this.lblManualTitle.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.lblManualTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblManualTitle.Location = new System.Drawing.Point(22, 18);
            this.lblManualTitle.Name = "lblManualTitle";
            this.lblManualTitle.Size = new System.Drawing.Size(133, 19);
            this.lblManualTitle.TabIndex = 0;
            this.lblManualTitle.Text = "Manual Backup";
            // 
            // pnlScheduleCard
            // 
            this.pnlScheduleCard.BackColor = System.Drawing.Color.White;
            this.pnlScheduleCard.Controls.Add(this.tblSchedule);
            this.pnlScheduleCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScheduleCard.Location = new System.Drawing.Point(0, 146);
            this.pnlScheduleCard.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.pnlScheduleCard.Name = "pnlScheduleCard";
            this.pnlScheduleCard.Padding = new System.Windows.Forms.Padding(22);
            this.pnlScheduleCard.Size = new System.Drawing.Size(979, 366);
            this.pnlScheduleCard.TabIndex = 1;
            // 
            // tblSchedule
            // 
            this.tblSchedule.ColumnCount = 3;
            this.tblSchedule.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 215F));
            this.tblSchedule.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblSchedule.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 280F));
            this.tblSchedule.Controls.Add(this.lblScheduleTitle, 0, 0);
            this.tblSchedule.Controls.Add(this.chkAutomaticBackup, 0, 1);
            this.tblSchedule.Controls.Add(this.lblAutomaticTime, 0, 2);
            this.tblSchedule.Controls.Add(this.dtpAutomaticTime, 1, 2);
            this.tblSchedule.Controls.Add(this.lblFolder, 0, 3);
            this.tblSchedule.Controls.Add(this.txtBackupFolder, 1, 3);
            this.tblSchedule.Controls.Add(this.pnlFolderActions, 2, 3);
            this.tblSchedule.Controls.Add(this.lblFolderMode, 1, 4);
            this.tblSchedule.Controls.Add(this.pnlScheduleActions, 0, 5);
            this.tblSchedule.Controls.Add(this.lblSchedulerStatus, 0, 6);
            this.tblSchedule.Controls.Add(this.lblPermissionHelp, 0, 7);
            this.tblSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblSchedule.Location = new System.Drawing.Point(22, 22);
            this.tblSchedule.Name = "tblSchedule";
            this.tblSchedule.RowCount = 8;
            this.tblSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tblSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tblSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tblSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tblSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tblSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tblSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblSchedule.Size = new System.Drawing.Size(935, 322);
            this.tblSchedule.TabIndex = 0;
            // 
            // lblScheduleTitle
            // 
            this.lblScheduleTitle.AutoSize = true;
            this.tblSchedule.SetColumnSpan(this.lblScheduleTitle, 3);
            this.lblScheduleTitle.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.lblScheduleTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblScheduleTitle.Location = new System.Drawing.Point(3, 0);
            this.lblScheduleTitle.Name = "lblScheduleTitle";
            this.lblScheduleTitle.Size = new System.Drawing.Size(154, 19);
            this.lblScheduleTitle.TabIndex = 0;
            this.lblScheduleTitle.Text = "Automatic Backup";
            // 
            // chkAutomaticBackup
            // 
            this.chkAutomaticBackup.AutoSize = true;
            this.tblSchedule.SetColumnSpan(this.chkAutomaticBackup, 3);
            this.chkAutomaticBackup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkAutomaticBackup.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.chkAutomaticBackup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.chkAutomaticBackup.Location = new System.Drawing.Point(3, 41);
            this.chkAutomaticBackup.Name = "chkAutomaticBackup";
            this.chkAutomaticBackup.Size = new System.Drawing.Size(929, 36);
            this.chkAutomaticBackup.TabIndex = 1;
            this.chkAutomaticBackup.Text = "Enable one automatic full database backup every day";
            this.chkAutomaticBackup.UseVisualStyleBackColor = true;
            this.chkAutomaticBackup.CheckedChanged += new System.EventHandler(this.ChkAutomaticBackup_CheckedChanged);
            // 
            // lblAutomaticTime
            // 
            this.lblAutomaticTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAutomaticTime.Font = new System.Drawing.Font("Century Gothic", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblAutomaticTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblAutomaticTime.Location = new System.Drawing.Point(3, 80);
            this.lblAutomaticTime.Name = "lblAutomaticTime";
            this.lblAutomaticTime.Size = new System.Drawing.Size(209, 48);
            this.lblAutomaticTime.TabIndex = 2;
            this.lblAutomaticTime.Text = "Daily backup time";
            this.lblAutomaticTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpAutomaticTime
            // 
            this.dtpAutomaticTime.CustomFormat = "hh:mm tt";
            this.dtpAutomaticTime.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.dtpAutomaticTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpAutomaticTime.Location = new System.Drawing.Point(218, 90);
            this.dtpAutomaticTime.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.dtpAutomaticTime.Name = "dtpAutomaticTime";
            this.dtpAutomaticTime.ShowUpDown = true;
            this.dtpAutomaticTime.Size = new System.Drawing.Size(190, 24);
            this.dtpAutomaticTime.TabIndex = 3;
            // 
            // lblFolder
            // 
            this.lblFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFolder.Font = new System.Drawing.Font("Century Gothic", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblFolder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblFolder.Location = new System.Drawing.Point(3, 128);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(209, 50);
            this.lblFolder.TabIndex = 4;
            this.lblFolder.Text = "Backup folder";
            this.lblFolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBackupFolder
            // 
            this.txtBackupFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBackupFolder.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.txtBackupFolder.Location = new System.Drawing.Point(218, 139);
            this.txtBackupFolder.Margin = new System.Windows.Forms.Padding(3, 11, 8, 8);
            this.txtBackupFolder.Name = "txtBackupFolder";
            this.txtBackupFolder.ReadOnly = true;
            this.txtBackupFolder.Size = new System.Drawing.Size(429, 22);
            this.txtBackupFolder.TabIndex = 5;
            // 
            // pnlFolderActions
            // 
            this.pnlFolderActions.Controls.Add(this.btnChooseFolder);
            this.pnlFolderActions.Controls.Add(this.btnUseSqlDefault);
            this.pnlFolderActions.Controls.Add(this.btnOpenFolder);
            this.pnlFolderActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFolderActions.Location = new System.Drawing.Point(658, 131);
            this.pnlFolderActions.Name = "pnlFolderActions";
            this.pnlFolderActions.Size = new System.Drawing.Size(274, 44);
            this.pnlFolderActions.TabIndex = 6;
            // 
            // btnChooseFolder
            // 
            this.btnChooseFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(165)))), ((int)(((byte)(233)))));
            this.btnChooseFolder.FlatAppearance.BorderSize = 0;
            this.btnChooseFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChooseFolder.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
            this.btnChooseFolder.ForeColor = System.Drawing.Color.White;
            this.btnChooseFolder.Location = new System.Drawing.Point(3, 3);
            this.btnChooseFolder.Name = "btnChooseFolder";
            this.btnChooseFolder.Size = new System.Drawing.Size(85, 36);
            this.btnChooseFolder.TabIndex = 0;
            this.btnChooseFolder.Text = "Change";
            this.btnChooseFolder.UseVisualStyleBackColor = false;
            this.btnChooseFolder.Click += new System.EventHandler(this.BtnChooseFolder_Click);
            // 
            // btnUseSqlDefault
            // 
            this.btnUseSqlDefault.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnUseSqlDefault.FlatAppearance.BorderSize = 0;
            this.btnUseSqlDefault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUseSqlDefault.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
            this.btnUseSqlDefault.ForeColor = System.Drawing.Color.White;
            this.btnUseSqlDefault.Location = new System.Drawing.Point(94, 3);
            this.btnUseSqlDefault.Name = "btnUseSqlDefault";
            this.btnUseSqlDefault.Size = new System.Drawing.Size(95, 36);
            this.btnUseSqlDefault.TabIndex = 1;
            this.btnUseSqlDefault.Text = "SQL Default";
            this.btnUseSqlDefault.UseVisualStyleBackColor = false;
            this.btnUseSqlDefault.Click += new System.EventHandler(this.BtnUseSqlDefault_Click);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(58)))), ((int)(((byte)(237)))));
            this.btnOpenFolder.FlatAppearance.BorderSize = 0;
            this.btnOpenFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenFolder.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
            this.btnOpenFolder.ForeColor = System.Drawing.Color.White;
            this.btnOpenFolder.Location = new System.Drawing.Point(195, 3);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(76, 36);
            this.btnOpenFolder.TabIndex = 2;
            this.btnOpenFolder.Text = "Open";
            this.btnOpenFolder.UseVisualStyleBackColor = false;
            this.btnOpenFolder.Click += new System.EventHandler(this.BtnOpenFolder_Click);
            // 
            // lblFolderMode
            // 
            this.lblFolderMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFolderMode.Font = new System.Drawing.Font("Century Gothic", 8F);
            this.lblFolderMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblFolderMode.Location = new System.Drawing.Point(218, 178);
            this.lblFolderMode.Name = "lblFolderMode";
            this.lblFolderMode.Size = new System.Drawing.Size(434, 28);
            this.lblFolderMode.TabIndex = 7;
            this.lblFolderMode.Text = "SQL Server default folder — recommended";
            // 
            // pnlScheduleActions
            // 
            this.tblSchedule.SetColumnSpan(this.pnlScheduleActions, 3);
            this.pnlScheduleActions.Controls.Add(this.btnSaveSchedule);
            this.pnlScheduleActions.Controls.Add(this.btnOpenLog);
            this.pnlScheduleActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScheduleActions.Location = new System.Drawing.Point(3, 209);
            this.pnlScheduleActions.Name = "pnlScheduleActions";
            this.pnlScheduleActions.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.pnlScheduleActions.Size = new System.Drawing.Size(929, 54);
            this.pnlScheduleActions.TabIndex = 8;
            // 
            // btnSaveSchedule
            // 
            this.btnSaveSchedule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnSaveSchedule.FlatAppearance.BorderSize = 0;
            this.btnSaveSchedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveSchedule.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnSaveSchedule.ForeColor = System.Drawing.Color.White;
            this.btnSaveSchedule.Location = new System.Drawing.Point(3, 10);
            this.btnSaveSchedule.Name = "btnSaveSchedule";
            this.btnSaveSchedule.Size = new System.Drawing.Size(170, 40);
            this.btnSaveSchedule.TabIndex = 0;
            this.btnSaveSchedule.Text = "Save Schedule";
            this.btnSaveSchedule.UseVisualStyleBackColor = false;
            this.btnSaveSchedule.Click += new System.EventHandler(this.BtnSaveSchedule_Click);
            // 
            // btnOpenLog
            // 
            this.btnOpenLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnOpenLog.FlatAppearance.BorderSize = 0;
            this.btnOpenLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenLog.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnOpenLog.ForeColor = System.Drawing.Color.White;
            this.btnOpenLog.Location = new System.Drawing.Point(179, 10);
            this.btnOpenLog.Name = "btnOpenLog";
            this.btnOpenLog.Size = new System.Drawing.Size(130, 40);
            this.btnOpenLog.TabIndex = 1;
            this.btnOpenLog.Text = "Open Log";
            this.btnOpenLog.UseVisualStyleBackColor = false;
            this.btnOpenLog.Click += new System.EventHandler(this.BtnOpenLog_Click);
            // 
            // lblSchedulerStatus
            // 
            this.tblSchedule.SetColumnSpan(this.lblSchedulerStatus, 3);
            this.lblSchedulerStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSchedulerStatus.Font = new System.Drawing.Font("Century Gothic", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblSchedulerStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSchedulerStatus.Location = new System.Drawing.Point(3, 266);
            this.lblSchedulerStatus.Name = "lblSchedulerStatus";
            this.lblSchedulerStatus.Size = new System.Drawing.Size(929, 38);
            this.lblSchedulerStatus.TabIndex = 9;
            this.lblSchedulerStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPermissionHelp
            // 
            this.tblSchedule.SetColumnSpan(this.lblPermissionHelp, 3);
            this.lblPermissionHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPermissionHelp.Font = new System.Drawing.Font("Century Gothic", 8F);
            this.lblPermissionHelp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblPermissionHelp.Location = new System.Drawing.Point(3, 304);
            this.lblPermissionHelp.Name = "lblPermissionHelp";
            this.lblPermissionHelp.Size = new System.Drawing.Size(929, 18);
            this.lblPermissionHelp.TabIndex = 10;
            this.lblPermissionHelp.Text = "A custom folder must allow the SQL Server service to write files. The SQL default" +
    " folder is the safest choice.";
            // 
            // pnlStatusCard
            // 
            this.pnlStatusCard.BackColor = System.Drawing.Color.White;
            this.pnlStatusCard.Controls.Add(this.tblStatus);
            this.pnlStatusCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStatusCard.Location = new System.Drawing.Point(0, 526);
            this.pnlStatusCard.Margin = new System.Windows.Forms.Padding(0);
            this.pnlStatusCard.Name = "pnlStatusCard";
            this.pnlStatusCard.Padding = new System.Windows.Forms.Padding(22);
            this.pnlStatusCard.Size = new System.Drawing.Size(979, 248);
            this.pnlStatusCard.TabIndex = 2;
            // 
            // tblStatus
            // 
            this.tblStatus.ColumnCount = 2;
            this.tblStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 215F));
            this.tblStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblStatus.Controls.Add(this.lblStatusTitle, 0, 0);
            this.tblStatus.Controls.Add(this.lblLastSuccessful, 0, 1);
            this.tblStatus.Controls.Add(this.lblLastSuccessfulValue, 1, 1);
            this.tblStatus.Controls.Add(this.lblLastFile, 0, 2);
            this.tblStatus.Controls.Add(this.lblLastFileValue, 1, 2);
            this.tblStatus.Controls.Add(this.lblLastResult, 0, 3);
            this.tblStatus.Controls.Add(this.lblLastResultValue, 1, 3);
            this.tblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblStatus.Location = new System.Drawing.Point(22, 22);
            this.tblStatus.Name = "tblStatus";
            this.tblStatus.RowCount = 4;
            this.tblStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tblStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tblStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tblStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblStatus.Size = new System.Drawing.Size(935, 204);
            this.tblStatus.TabIndex = 0;
            // 
            // lblStatusTitle
            // 
            this.lblStatusTitle.AutoSize = true;
            this.tblStatus.SetColumnSpan(this.lblStatusTitle, 2);
            this.lblStatusTitle.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.lblStatusTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStatusTitle.Location = new System.Drawing.Point(3, 0);
            this.lblStatusTitle.Name = "lblStatusTitle";
            this.lblStatusTitle.Size = new System.Drawing.Size(116, 19);
            this.lblStatusTitle.TabIndex = 0;
            this.lblStatusTitle.Text = "Backup Status";
            // 
            // lblLastSuccessful
            // 
            this.lblLastSuccessful.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLastSuccessful.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblLastSuccessful.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblLastSuccessful.Location = new System.Drawing.Point(3, 38);
            this.lblLastSuccessful.Name = "lblLastSuccessful";
            this.lblLastSuccessful.Size = new System.Drawing.Size(209, 48);
            this.lblLastSuccessful.TabIndex = 1;
            this.lblLastSuccessful.Text = "Last successful backup";
            this.lblLastSuccessful.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLastSuccessfulValue
            // 
            this.lblLastSuccessfulValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLastSuccessfulValue.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.lblLastSuccessfulValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.lblLastSuccessfulValue.Location = new System.Drawing.Point(218, 38);
            this.lblLastSuccessfulValue.Name = "lblLastSuccessfulValue";
            this.lblLastSuccessfulValue.Size = new System.Drawing.Size(714, 48);
            this.lblLastSuccessfulValue.TabIndex = 2;
            this.lblLastSuccessfulValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLastFile
            // 
            this.lblLastFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLastFile.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblLastFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblLastFile.Location = new System.Drawing.Point(3, 86);
            this.lblLastFile.Name = "lblLastFile";
            this.lblLastFile.Size = new System.Drawing.Size(209, 62);
            this.lblLastFile.TabIndex = 3;
            this.lblLastFile.Text = "Last backup file";
            this.lblLastFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLastFileValue
            // 
            this.lblLastFileValue.AutoEllipsis = true;
            this.lblLastFileValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLastFileValue.Font = new System.Drawing.Font("Century Gothic", 8.5F);
            this.lblLastFileValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblLastFileValue.Location = new System.Drawing.Point(218, 86);
            this.lblLastFileValue.Name = "lblLastFileValue";
            this.lblLastFileValue.Size = new System.Drawing.Size(714, 62);
            this.lblLastFileValue.TabIndex = 4;
            this.lblLastFileValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLastResult
            // 
            this.lblLastResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLastResult.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblLastResult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblLastResult.Location = new System.Drawing.Point(3, 148);
            this.lblLastResult.Name = "lblLastResult";
            this.lblLastResult.Size = new System.Drawing.Size(209, 56);
            this.lblLastResult.TabIndex = 5;
            this.lblLastResult.Text = "Last attempt result";
            this.lblLastResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLastResultValue
            // 
            this.lblLastResultValue.AutoEllipsis = true;
            this.lblLastResultValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLastResultValue.Font = new System.Drawing.Font("Century Gothic", 8.5F);
            this.lblLastResultValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblLastResultValue.Location = new System.Drawing.Point(218, 148);
            this.lblLastResultValue.Name = "lblLastResultValue";
            this.lblLastResultValue.Size = new System.Drawing.Size(714, 56);
            this.lblLastResultValue.TabIndex = 6;
            this.lblLastResultValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BackupSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1040, 680);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(760, 560);
            this.Name = "BackupSettingsForm";
            this.Text = "Database Backup";
            this.Load += new System.EventHandler(this.BackupSettingsForm_Load);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.tblContent.ResumeLayout(false);
            this.pnlManualCard.ResumeLayout(false);
            this.pnlManualCard.PerformLayout();
            this.pnlScheduleCard.ResumeLayout(false);
            this.tblSchedule.ResumeLayout(false);
            this.tblSchedule.PerformLayout();
            this.pnlFolderActions.ResumeLayout(false);
            this.pnlScheduleActions.ResumeLayout(false);
            this.pnlStatusCard.ResumeLayout(false);
            this.tblStatus.ResumeLayout(false);
            this.tblStatus.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
