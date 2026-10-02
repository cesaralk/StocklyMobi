# Database Backup — Open Folder Fix

This update changes only the Backup Settings folder-opening behavior.

## Fixes

- Uses `explorer.exe` explicitly instead of asking Windows to open a raw path.
- Selects the most recent `.bak` file when it exists in the displayed folder.
- Falls back to the effective configured backup folder when the textbox is empty.
- Shows the exact folder path and a clearer permission message if Windows cannot open the SQL Server default folder.

## Important

The SQL Server default backup directory can be writable by the SQL Server service but restricted for the signed-in Windows user. In that case, choose a custom shared folder and grant the SQL Server service account write permission.

No database migration is required.
