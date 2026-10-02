# StocklyMobi Designer Edition 2.18.2

This release keeps the 2.18 Product & Stock-In design, keeps the 2.18.1 cash
drawer backport, and adds safe stock-in invoice deletion from Invoice History.

See:

- `INVOICE_HISTORY_DELETE_GUIDE.md`
- `CASH_DRAWER_2_18_BACKPORT_GUIDE.md`

No SQL migration is required.

---

# StocklyMobi Designer Edition 2.18

This release adds per-product barcode selection and a one-label-per-product
invoice mode.

## Invoice row selection

Each invoice row now has a `Print Barcode` checkbox.

- Products with a barcode start checked.
- Products without a barcode start unchecked.
- Uncheck products that already arrive labeled.
- The checkbox controls both barcode modes.

## Print 1 Each

`Print 1 Each` opens the normal barcode preview with exactly one label for every
checked invoice product, regardless of invoice quantity.

## Existing invoice-quantity mode retained

`After post: invoice QTY` keeps the previous behavior: after posting, every
checked invoice product repeats according to its entered invoice quantity.

Both modes use the unchanged shared Barcode-page design: 177 × 98, zero
margins, Code 128, and the same number/description/price layout.

No SQL or database update is required.

# StocklyMobi Designer Edition 2.17

This release adds barcode label quantity printing to Products & Stock-In and
invoice-quantity barcode printing after posting a supplier invoice.

## Selected product barcode printing

Directly below Search / Scan in Products & Stock-In there is now:

```text
No. of labels
Print Barcode
```

1. Select a product in the product grid.
2. Enter the required number of labels.
3. Press Print Barcode.
4. The normal barcode print preview opens.

## Invoice barcode quantities

The Post Invoice footer now contains:

```text
Print all barcodes (invoice QTY)
```

When checked before posting:

- Every invoice line with a barcode is included.
- The number of labels equals that line's invoice quantity.
- A barcode preview opens after the invoice posts.
- Products without a barcode are reported and skipped.
- Closing or failing to open the barcode preview does not undo the posted
  invoice.

## Identical barcode page settings

All new barcode paths use the same shared label settings as the existing
Barcode page:

```text
Paper: Label
Size: 177 x 98 printer units
Approximate physical size: 4.5 cm x 2.5 cm
Margins: 0
Barcode: Code 128 with checksum
Barcode image: height 40, scale 1
Same barcode number, description and price layout
```

Each label is one printer page, so invoice quantities print the correct number
of physical labels.

## Database

No SQL or database update is required.

## Install

1. Close Visual Studio and StocklyMobi.
2. Extract the full 2.17 source into a new folder, or apply the patch to 2.16.
3. Delete `.vs`, `bin`, and `obj`.
4. Open `StocklyMobi.sln`.
5. Clean Solution.
6. Rebuild Solution.
7. Test one selected product with 3 labels.
8. Post a small test invoice with the barcode checkbox enabled and verify that
   each line repeats according to its invoice quantity.

# StocklyMobi Designer Edition 2.16

This release adds repair editing and moves Mobile Fixing tickets to the shared
receipt-printer setup.

## Edit any phone repair

Select any repair in Mobile Fixing and press:

```text
Edit Selected
```

You can update:

- Problem description
- Cost price
- Final price

Customer name, phone number, phone model, and estimated finish are protected
during edit mode so the selected repair identity cannot be changed accidentally.

During edit mode the buttons become:

```text
Update
Update + Preview
Update + Print
```

Double-clicking a repair also opens Edit mode.

The edit works for Received, In Progress, Ready, Delivered, and Cancelled
records.

When a repair is already inside a pending cashier transaction, its pending cart
cost and final price are updated automatically.

A delivered receipt remains a historical snapshot and is not rewritten.

## Receipt-style fixing ticket

Mobile Fixing tickets now use:

- The configured receipt printer
- 80 mm receipt paper
- Dynamic paper length
- The same receipt side padding
- The same top-feed correction
- The same printer hard-margin handling
- Two-decimal USD formatting

The ticket includes the Fix ID, customer, phone number, model, problem,
estimated finish, final customer price, and status.

Selected repairs now have both:

```text
Preview Ticket
Print Ticket
```

## Database

No SQL schema or database migration is required.

## Install

1. Close Visual Studio and StocklyMobi.
2. Extract the full 2.16 source into a new folder, or apply the patch to 2.15.
3. Delete `.vs`, `bin`, and `obj`.
4. Open `StocklyMobi.sln`.
5. Clean Solution.
6. Rebuild Solution.
7. Open Mobile Fixing and edit a test repair.
8. Print a ticket and confirm it uses the selected receipt printer and 80 mm
   receipt settings.

# StocklyMobi Designer Edition 2.15

This release applies a global two-decimal USD rule across the entire POS.

## Global USD rule

Every user-facing USD value now displays exactly two digits after the decimal
point:

```text
$ 0.00
$ 1.25
$ 100.00
```

This includes:

- Products and Product Search
- New/Edit Product
- Stock-In and Product History
- Supplier invoice entry and invoice history
- Mobile fixing cost, final price and profit
- Services and provider balances
- Cashier provider-balance views
- Validity Retake
- Daily Profit
- Sales, inventory and stock reports
- Barcode labels
- Cashier, Settle, Receipt Search and printed receipts
- Exchange-rate examples

## Entry precision

USD NumericUpDown controls use:

```text
DecimalPlaces = 2
Increment = 0.01
```

Product and stock pricing calculations shown in editable screens are rounded to
two decimals using AwayFromZero rounding.

## Database precision

Existing SQL decimal column definitions remain unchanged. This avoids a risky
database migration and preserves compatibility with existing data.

## Non-USD values

LBP remains displayed without decimal digits.

Dates, printer measurements and internal comparison tolerances are not currency
and were not changed.

## Database

No SQL or database update is required.

## Install

1. Close Visual Studio and StocklyMobi.
2. Extract the full 2.15 source into a new folder, or apply the patch to 2.14.
3. Delete `.vs`, `bin`, and `obj`.
4. Open `StocklyMobi.sln`.
5. Clean Solution.
6. Rebuild Solution.
7. Check Products, Stock-In, Mobile Fixing, Services and reports.
8. Print a barcode label and receipt to confirm two-decimal USD formatting.

# StocklyMobi Designer Edition 2.14

This release adds Cost Price in LBP and standardizes Admin Services USD amounts
to exactly two decimal places.

## Cost Price conversion

Admin → Services now contains:

```text
Cost price (USD)
Cost price (LBP)
```

- Changing USD updates LBP.
- Changing LBP updates USD.
- Both directions use the saved Admin exchange rate.
- USD is rounded to two decimals.
- LBP is rounded to the nearest 1,000 LBP.

Final price conversion remains available in both USD and LBP.

## USD precision

The Admin Services page now displays exactly two decimals for:

- Cost price
- Validity deducted price
- Final price
- Profit
- Provider opening balance
- Provider balance adjustments
- Provider balance grids
- Service cost/final/profit grids

Example:

```text
$ 1.10
$ 8.75
$ 100.00
```

## Database

No SQL or database update is required.

Only the USD values are stored in the existing service table. The LBP fields are
live conversion helpers.

## Install

1. Close Visual Studio and StocklyMobi.
2. Extract the full 2.14 source into a new folder, or apply the patch to 2.13.
3. Delete `.vs`, `bin`, and `obj`.
4. Open `StocklyMobi.sln`.
5. Clean Solution.
6. Rebuild Solution.
7. Test Cost USD → LBP and Cost LBP → USD.
8. Verify all Admin Services USD values show two decimals.

# StocklyMobi Designer Edition 2.13

This release adds linked USD/LBP service pricing and moves the system exchange
rate from Cashier to Admin Settings.

## Service pricing

Admin → Services now contains:

- Final price (USD)
- Final price (LBP)
- Current system rate

Typing or changing USD updates LBP. Typing or changing LBP updates USD.
Only the USD price is stored in the existing service table; LBP is a live
conversion helper, so no database column or migration is required.

## Exchange rate

The Cashier rate box was removed.

Admin can manage the rate from:

```text
Setting → Exchange Rate
```

The same saved rate is used by Cashier totals, Settle, and Admin Services.

## USD precision

Admin service prices, costs, deductions, profit, and provider balances now show
at most 3 decimal places. This keeps recharge accuracy without the old 4-digit
clutter. Cashier and receipts continue showing 2 decimal places.

## Database

No SQL or database update is required.

## Install

1. Close Visual Studio and StocklyMobi.
2. Extract the full 2.13 source into a new folder, or apply the patch to 2.12.
3. Delete `.vs`, `bin`, and `obj`.
4. Open `StocklyMobi.sln`.
5. Clean Solution.
6. Rebuild Solution.
7. Open Setting → Exchange Rate and save the rate.
8. Open Services and test USD → LBP and LBP → USD conversion.

# StocklyMobi Designer Edition 2.12

This release fixes text being clipped horizontally on thermal receipts.

## Cause

The receipt code was subtracting the printer driver's `HardMarginX`, moving
the drawing area left into the printer's non-printable zone. The remaining
software side padding was only about 1.5 mm.

## Correction

- Horizontal hard-margin compensation was removed.
- Vertical hard-margin compensation remains active.
- A safe inner margin of approximately 3 mm is applied on both sides.
- The actual printer-reported printable width is used.
- The same width is used when measuring wrapped receipt content.
- Receipt height/feed behavior from Designer Edition 2.11 is unchanged.

## Applies to

- Settle receipt printing
- Receipt Search printing
- Receipt Search preview
- Test Receipt

## Database

No SQL or database update is required.

## Install

1. Close Visual Studio and StocklyMobi.
2. Extract the full 2.12 source into a new folder, or apply the patch to 2.11.
3. Delete `.vs`, `bin`, and `obj`.
4. Open `StocklyMobi.sln`.
5. Clean Solution.
6. Rebuild Solution.
7. Print a Test Receipt.
8. Verify that the first and last characters are fully visible.

# StocklyMobi Designer Edition 2.11

This release fixes the approximately 11 cm of blank paper before receipt text.

## Root cause fixed

The receipt printer was always given a minimum page height of 700 printer
units, approximately 17.8 cm.

The actual content of a small receipt is often only about 6–8 cm. Some thermal
printer drivers feed the unused part of that page before printing, which caused
the large blank section.

The page height is now calculated from the real receipt content:

- Store and address
- Receipt information
- Number of products/services
- Wrapped descriptions
- Discounts
- Payments and change
- Footer

Only a small trailing allowance is kept. On a normal receipt this should keep
the blank feed near 6 mm and below the requested 1 cm.

## Old trim values

Large Receipt Top Trim values from the previous workaround are automatically
reset to 0.

The remaining Fine Top Trim control is limited to 0–2 mm and is optional.

## Database

No SQL or database update is required.

## Install

1. Close Visual Studio and StocklyMobi.
2. Extract the full 2.11 source into a new folder, or apply the patch to 2.10.
3. Delete `.vs`, `bin`, and `obj`.
4. Open `StocklyMobi.sln`.
5. Clean Solution.
6. Rebuild Solution.
7. Open Settings → Printers once.
8. Confirm Fine Top Trim shows 0.0 mm.
9. Print one short receipt from Settle and one from Receipt Search.

# StocklyMobi Designer Edition 2.10

This release addresses receipt paper wasted before the first printed line.

## Automatic hard-margin compensation

The receipt graphics are now moved upward by the exact hard top margin reported
by the selected Windows receipt-printer driver.

This removes the driver offset that was still being added before receipt data.

## Receipt Top Trim

Settings → Printers now contains:

```text
Receipt top trim (mm)
```

Use it only when the printer still feeds extra blank paper after automatic
compensation.

Recommended procedure:

1. Start at `0.0`.
2. Press Test Receipt.
3. Increase by `0.5` or `1.0` mm.
4. Stop when the first title is close to the paper edge.
5. Reduce the value if the title becomes clipped.

The allowed range is 0–30 mm.

The same adjustment is used by:

- Settlement receipt printing
- Receipt Search printing
- Receipt Search preview
- Test Receipt

## Database

No SQL or database update is required.

## Install

1. Close Visual Studio and StocklyMobi.
2. Extract the full 2.10 source into a new folder, or apply the patch to 2.9.
3. Delete `.vs`, `bin`, and `obj`.
4. Open the solution.
5. Clean Solution.
6. Rebuild Solution.
7. Open Settings → Printers.
8. Save the printer settings.
9. Test at 0.0 mm, then adjust only as needed.

# StocklyMobi Designer Edition 2.9

This release reduces the blank space before receipt data and makes Receipt
Search fit laptop screens.

## Receipt top spacing

The snapshot receipt printer no longer starts from `MarginBounds`.

- Software margins are set to zero.
- Printing starts at the first printable row.
- A small 6-unit left/right safety padding remains.
- The printer's unavoidable physical non-printable area is still respected.

This affects settlement receipts, Receipt Search printing, and Receipt Search
preview because they use the same snapshot printer.

## Responsive Receipt Search

Receipt Search now:

- Opens maximized inside the Windows working area.
- Uses a smaller minimum size.
- Compacts its header, filters, and footer on short screens.
- Keeps Print and Print Preview inside a fixed visible footer.
- Switches between side-by-side and stacked layouts based on available space.
- Uses a scroll-safe, non-wrapping print action area.

## Database

No SQL or database update is required.

## Install

1. Close Visual Studio and StocklyMobi.
2. Extract the full 2.9 source into a new folder, or apply the patch to 2.8.
3. Delete `.vs`, `bin`, and `obj`.
4. Open `StocklyMobi.sln`.
5. Clean Solution.
6. Rebuild Solution.
7. Print one receipt and verify the top feed.
8. Open Receipt Search on the laptop and verify Print and Print Preview.

# StocklyMobi Designer Edition 2.8

This release fixes receipt printing during settlement and adds a per-transaction
Print Receipt ON/OFF control.

## Receipt printing fix

Settlement no longer uses the old RDLC LocalReport renderer.

After the transaction is saved, the receipt is printed from the permanent
receipt snapshot in `tbReceiptHeader` and `tbReceiptLine`. This is the same
source used by Receipt Search and includes products, services, mobile fixing,
discounts, payment amounts, and receipt status.

If printing fails, payment remains saved and the receipt can be reprinted from
Receipt Search.

## Settle ON/OFF control

The Settle form now has:

- Green `Print Receipt: ON`
- Gray `Print Receipt: OFF`

Its initial value comes from Settings → Printers. The cashier can change it for
the current transaction without changing the saved global default.

## Database

No SQL or database update is required.

## Install

1. Close Visual Studio and StocklyMobi.
2. Extract the full 2.8 ZIP into a new folder, or apply the patch to 2.7.1.
3. Delete `.vs`, `bin`, and `obj`.
4. Open `StocklyMobi.sln`.
5. Clean Solution.
6. Rebuild Solution.
7. Select the receipt printer in Settings → Printers.
8. Test one settlement with printing ON and one with printing OFF.

# StocklyMobi Designer Edition 2.7.1

This is a syntax-only correction for Designer Edition 2.7.

## Fixed

`CashierProviderBalancesForm.Designer.cs` contained a normal C# string split
across two physical source lines. That one malformed string generated around
93 cascading compiler errors.

It now uses:

```csharp
this.lblHeader.Text =
    "Provider Balances\r\nRead-only balances for cashier services.";
```

## Scope

Only `CashierProviderBalancesForm.Designer.cs` changed.

No database, SQL, settlement, services, balances, printing, stock, repair,
receipt, or responsive-layout logic was changed.

## Install

1. Close Visual Studio.
2. Use the full 2.7.1 source in a new folder, or replace only
   `CashierProviderBalancesForm.Designer.cs` in Designer Edition 2.7.
3. Delete `.vs`, `bin`, and `obj`.
4. Open the solution.
5. Clean Solution.
6. Rebuild Solution.

# StocklyMobi Designer Edition 2.7

Based on Designer Edition 2.6.

## New

- Read-only Provider Balances button on the cashier side.
- Provider balance strip inside cashier service selection.
- Responsive Admin Services page for small laptop screens.
- Service and provider action buttons remain visible.

No SQL or database update is required.

# StocklyMobi Designer Edition 2.6

This release is based on Designer Edition 2.5.

## Validity Recharge change

Validity Recharge now has a separate **Deducted Price**.

- WishMoney is reduced by Deducted Price at settlement.
- Cost Price remains for accounting and profit only.
- Profit remains Final Price minus Cost Price.
- Cancellation restores Deducted Price to WishMoney.
- Retake $ behavior remains unchanged.
- Other service types still deduct Cost Price.

## Database

This release includes a database upgrade. Back up the database first. The application can apply `Database_Full.sql` automatically, or the standalone full SQL script can be run manually.

# StocklyMobi Designer Edition 2.5

This release adds fast suggestions for Supplier, Brand, and Category.

Only the Products & Stock-In supplier selector and the New/Edit Product brand/category selectors were changed. No SQL update is required.

# StocklyMobi Designer Edition 2.4.1

Compile-only correction for Designer Edition 2.4.

## Fixed

`InventoryInvoiceForm.cs` called `ApplySearchCueText()` but the method was
missing. The method has been restored using the existing Windows cue-banner
API already declared in the same form.

## Scope

Only `StocklyMobi/InventoryInvoiceForm.cs` changed.

No database, SQL, Designer layout, product management, printer, scanner,
settlement, service, repair, receipt, or report change is included.

## Install

1. Close Visual Studio.
2. Use the full 2.4.1 source in a new folder, or copy the patched
   `InventoryInvoiceForm.cs` over Designer Edition 2.4.
3. Delete stale `.vs`, `bin`, and `obj` folders.
4. Open the solution.
5. Clean Solution.
6. Rebuild Solution.

# StocklyMobi Designer Edition 2.4

This release is based on Designer Edition 2.3.

## Products & Stock-In changes

### Quantity after selection

The separate Qty-first field was removed.

The workflow is now:

1. Scan an exact barcode, or search and choose a product.
2. A quantity window opens immediately.
3. Type the required quantity and press Enter.
4. The product is added to the current supplier invoice.

The quantity window appears after:

- an exact barcode scan,
- an exact product code plus Enter or Tab,
- Enter when only one search result remains,
- Enter on a selected product row,
- double-clicking a product,
- pressing Add Selected,
- creating a new product before adding it to the invoice.

After a successful add, Search / Scan is cleared and focused for the next item.
The QTY cell in the current invoice remains editable.

### Product editing

Products & Stock-In now includes an **Edit Product** button.

It opens the same Designer-based product form and allows the administrator to
change:

- barcode,
- description,
- brand,
- category,
- cost price,
- markup,
- selling price,
- reorder level.

Current stock quantity is not changed by Edit Product. Stock quantity remains
controlled by stock-in invoices and stock adjustments.

If the edited product already exists in the current unposted invoice, its
barcode, description, cost, markup, and selling price are refreshed there too.

### Safe product deletion

Products & Stock-In now includes a **Delete Product** button.

A product can be permanently deleted only when:

- current stock is zero,
- it is not in the current unposted invoice,
- it has no stock-in history,
- it has no adjustment history,
- it has no cart or sales history,
- it has no receipt history,
- it has no cancellation history.

Products with history are protected so old receipts, reports, and invoices do
not become corrupted. Use Edit Product to change their details or price.

## Designer

The following new buttons are visible in `InventoryInvoiceForm.Designer.cs`:

- `_btnEditProduct`
- `_btnDeleteProduct`

`NewProductQuickForm` remains a normal Designer form and is reused for editing.

## Database

No SQL or database update is required.

## Installation

1. Close Visual Studio and StocklyMobi.
2. Extract the full source ZIP into a new folder, or apply the patch only to
   Designer Edition 2.3.
3. Delete stale `.vs`, `bin`, and `obj` folders.
4. Open `StocklyMobi.sln`.
5. Clean Solution.
6. Rebuild Solution.
7. Test one barcode scan, one typed search, one price edit, and deletion of one
   unused zero-stock test product.

---

## 2.18.1 Cash Drawer Backport

This package preserves the 2.18 Product & Stock-In design and adds the 2.20.1 cash-drawer settings and successful-settlement trigger. See `CASH_DRAWER_2_18_BACKPORT_GUIDE.md`.

---

## 2.18.2 Invoice History Deletion

This package is based on 2.18.1. It adds safe deletion of posted stock-in
invoices from Invoice History. Deletion reverses the invoice quantities and
removes its stock-in lines and header in one transaction. It is blocked if any
product would become negative. See `INVOICE_HISTORY_DELETE_GUIDE.md`.
