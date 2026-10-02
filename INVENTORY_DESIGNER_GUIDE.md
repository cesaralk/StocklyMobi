# Products & Stock-In — Visual Designer Pilot

This release converts only **Products & Stock-In** to a real WinForms Designer form.
It is based on the last working **Step 4.1 Responsive Search 1.2** source.

## Where the screenshot components live

The running screenshot contains two forms:

- `MainForm.Designer.cs`
  - Left navigation/sidebar
  - Admin name and role
  - Logout button
  - Main top title area
- `InventoryInvoiceForm.Designer.cs`
  - Supplier invoice number
  - Supplier and date
  - + Supplier, Invoice History, and New Invoice
  - Search box and clear button
  - Add Selected, New Product, and Refresh
  - Products grid and all ten product columns
  - Current invoice grid and all eight invoice columns
  - Remove Line and Post Stock-In Invoice
  - Lines, Total QTY, and Invoice cost

The sidebar and Products & Stock-In page will not appear together in one Designer tab because they belong to different forms.

## Open the page in Visual Studio

1. Open `StocklyMobi.sln`.
2. In Solution Explorer, expand `InventoryInvoiceForm.cs`.
3. Confirm these nested files exist:
   - `InventoryInvoiceForm.Designer.cs`
   - `InventoryInvoiceForm.resx`
4. Right-click `InventoryInvoiceForm.cs`.
5. Choose **View Designer**, or press **Shift+F7**.
6. Press **F7** to return to the page logic.
7. Press **F4** to edit the selected control's properties.

## Controls available for manual editing

Fixed controls are now real Designer fields, including:

- `_btnAddSupplier`
- `_btnInvoiceHistory`
- `_btnNewInvoice`
- `_btnClearSearch`
- `_btnAddSelected`
- `_btnNewProduct`
- `_btnRefresh`
- `_btnRemoveLine`
- `_btnPostInvoice`
- `_productsGrid`
- `_invoiceGrid`
- `_mainSplit`
- `_headerPanel`
- `_productCard`
- `_invoiceCard`

The DataGridView columns are also visible in **Edit Columns**:

- Product: Code, Barcode, Description, Brand, Category, Stock, Cost, Markup, Price, Reorder
- Invoice: Code, Barcode, Description, QTY, Cost, Markup %, Selling Price, Line Cost

## Runtime-only items

These do not appear as fixed controls because their contents come from SQL Server while the program runs:

- Product rows
- Supplier list values
- Search suggestion rows
- Current invoice rows
- Calculated totals

Their containers, grids, columns, and buttons are editable in the Designer.

## Safety

- No database script is included or required.
- Settlement, stock posting, quantity, invoice history, printing, and search logic were preserved.
- Only this one form was converted, so it can be verified before converting another page.
