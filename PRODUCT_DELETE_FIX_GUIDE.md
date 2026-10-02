# StocklyMobi Product Delete Fix

## What changed

The Products & Stock-In page can now delete products that already have stock or historical transactions.

Deletion is implemented as a safe catalog archive:

- The deleted product disappears from product lists, cashier barcode scans, barcode printing, stock-in selection, adjustments, inventory reports, dashboard product count, and stock totals.
- Existing sales, receipts, cancellation records, adjustments, and stock-in invoice history remain readable.
- The original barcode is released so it can be used for a new product later.
- The product quantity and reorder level are reset to zero.
- Deletion is blocked only when the product is currently used in a pending cashier transaction or pending stock-in entry.

## Database migration

`Database_Full.sql` now adds `tbProduct.isActive` automatically with a default value of `1`. StocklyMobi runs this schema script at startup, so existing products remain active and no manual database recreation is required.

If the application cannot run the startup migration because of SQL permissions, run the included `StocklyMobi/Database_Full.sql` once in SQL Server Management Studio using the application's database.

## Patch application

Copy the files from the patch ZIP over the same relative paths in the previous `StocklyMobi_Custom_ServiceRules_FullSource` version. Keep a backup of the working source and database before replacing files.

## Validation

- 53 focused static checks passed.
- Product and inventory Designer `.Designer.cs` and `.resx` files are unchanged.
- ZIP integrity and SHA-256 hashes are provided separately.
- A final build and test should be performed in Visual Studio on Windows because this workspace does not include the .NET Framework 4.8 Windows build toolchain.
