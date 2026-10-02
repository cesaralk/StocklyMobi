# StocklyMobi Service Provider Rules Update

This update is based on the custom StocklyMobi source that already contains the database-backup and Open Backup Folder fixes.

## Service rules

- **Mobile Recharge**: WishMoney only.
- **Transfer Balance**: MTC Line or Alfa Line only.
- **Validity Recharge**: WishMoney only (existing behavior preserved).
- **Internet Bundles**: renamed from Ushare; any active provider can be selected from the provider dropdown.
- **Apps**: any active provider can be selected from the provider dropdown.

## Automatic database upgrade

On the first application start after rebuilding:

- Existing `Ushare` service definitions are renamed to `Internet Bundles`.
- Existing historical service-sale categories named `Ushare` are renamed to `Internet Bundles`.
- Existing Mobile Recharge and Validity Recharge service definitions are assigned to WishMoney.
- The service-type database constraint is upgraded automatically.

No separate manual SQL migration is required. Keep `Database_Full.sql` beside the application executable, as in the existing project setup.

## Files changed

- `StocklyMobi/MobileShopDb.cs`
- `StocklyMobi/ServiceManagementForm.cs`
- `StocklyMobi/ServiceManagementForm.Designer.cs`
- `StocklyMobi/Cashier.cs`
- `StocklyMobi/ProfitReportForm.Designer.cs`
- `StocklyMobi/Database_Full.sql`
