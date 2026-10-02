# StocklyMobi 2.18.2 — Invoice History Deletion

This release is based on **Designer Edition 2.18.1**. It keeps the preferred
2.18 Product & Stock-In design and all 2.18.1 cash-drawer functionality.

## New action

Open:

```text
Admin > Invoice History
```

Select a posted supplier invoice and press:

```text
Delete Invoice
```

## What deletion does

The deletion runs inside one SQL transaction:

1. Locks and rechecks the selected invoice.
2. Groups its stock-in quantities by product.
3. Blocks deletion if subtracting an invoice quantity would make any product
   stock negative.
4. Subtracts the invoice quantities from current product stock.
5. Deletes the matching `tbStockIn` invoice lines.
6. Deletes the `tbStockInvoice` header.
7. Commits everything together, or rolls everything back if any step fails.

Product cost, markup, and selling price are not rolled back because they may
have been edited after the invoice was posted.

## Product deletion afterward

Deleting a stock-in invoice removes that invoice's `tbStockIn` references. A
product can then be deleted only when:

- its current quantity is zero,
- it has no other stock-in invoice history,
- it has no adjustment history,
- it has no cart or sales history,
- it has no receipt history,
- it has no cancellation history.

This protects old sales and reports from becoming corrupted.

## Important safety behavior

- Deletion is permanent and requires confirmation.
- The invoice cannot be deleted when its quantity reversal would create
  negative stock.
- Invoice lines and the header are deleted only after the stock reversal is
  validated.
- A failure rolls the complete operation back.
- No database migration is required.

## Installation

1. Close StocklyMobi and Visual Studio.
2. Back up the project and database.
3. Extract the full 2.18.2 source into a new folder, or apply the focused patch
   only to Designer Edition 2.18.1.
4. Delete stale `.vs`, `bin`, and `obj` folders.
5. Open `StocklyMobi.sln`.
6. Clean Solution and Rebuild Solution.
7. Create a test product and post a small stock-in invoice.
8. Delete the invoice from Invoice History and confirm the product quantity is
   reversed.
9. Delete the now-unused zero-stock test product.
