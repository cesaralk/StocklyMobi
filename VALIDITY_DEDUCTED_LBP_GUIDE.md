# Validity Recharge - Deducted Price in LBP

Baseline: `StocklyMobi_Custom_ProductDeleteFix_FullSource.zip`

## What changed

- Added **Deducted price (LBP)** to the Services editor.
- The field appears only when **Validity Recharge** is selected.
- Entering the deducted price in USD updates LBP automatically.
- Entering the deducted price in LBP updates USD automatically.
- Conversion uses the exchange rate configured in StocklyMobi Settings.
- The existing database field remains USD-based, so no database migration is required.
- Existing Validity Recharge records load both deducted USD and LBP values correctly.

## Small patch installation

1. Close StocklyMobi and Visual Studio.
2. Back up the current project folder.
3. Copy the `StocklyMobi` folder from the patch over the existing project folder.
4. Allow Windows to replace these two files:
   - `StocklyMobi/ServiceManagementForm.cs`
   - `StocklyMobi/ServiceManagementForm.Designer.cs`
5. Open `StocklyMobi.sln` in Visual Studio and rebuild.

No `.resx`, database, or unrelated business-logic files are changed.
