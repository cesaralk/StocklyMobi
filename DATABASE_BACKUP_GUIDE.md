# StocklyMobi database backup

## Added feature

Open **Settings > Database Backup**.

- **Backup Now** creates a complete SQL Server `.bak` file.
- Every backup is checked with `RESTORE VERIFYONLY` before success is recorded.
- Automatic backup is enabled by default at **11:00 PM**.
- The time can be changed and saved from the same screen.
- A Windows Task Scheduler task named **StocklyMobi Daily Database Backup** launches the application in backup-only mode.
- StocklyMobi also checks for a missed scheduled backup while the program is running and again when it starts.
- Automatic scheduling can be disabled from the screen.

## Recommended backup folder

Use **SQL Default** unless there is a specific reason to use another folder. SQL Server writes the `.bak` file, so its Windows service account must have permission to the selected folder.

## Log

The **Open Log** button opens:

`%LOCALAPPDATA%\StocklyMobi\Logs\DatabaseBackup.log`

## Important deployment note

After moving or publishing the application to another folder, start StocklyMobi once. Startup refreshes the scheduled task so it points to the current executable path.
