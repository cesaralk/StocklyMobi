# Quantity Popup, Edit Product, and Delete Product

## Quantity popup

Use the single Search / Scan box.

### Barcode scanner

Scan a barcode normally. When the scanner sends Enter or Tab, the matching
product is found and a quantity window opens. Type the quantity and press Enter.

### Typed search

Type as many letters and words as needed. The product grid filters without
stealing keyboard focus.

- If one result remains, press Enter to open the quantity window.
- If several results remain, use Down Arrow and Enter, or double-click the row.
- Add Selected also opens the quantity window.

Canceling the quantity window does not add the product.

## Edit Product

Select a product and press **Edit Product**.

Editable fields:

- Barcode
- Description
- Brand
- Category
- Cost price
- Markup
- Selling price
- Reorder level

Changing selling price automatically recalculates markup when cost is greater
than zero. Changing cost or markup recalculates selling price.

Editing does not change stock quantity.

## Delete Product

Select a product and press **Delete Product**.

Permanent deletion is allowed only for an unused product with zero stock and no
historical references. This protects supplier invoices, sales, receipts,
returns, and reports.

When a product has history, use Edit Product instead of deleting it.

## Designer controls

Open `InventoryInvoiceForm.cs` with Shift+F7.

Designer-editable controls include:

- `_search`
- `_btnAddSelected`
- `_btnNewProduct`
- `_btnEditProduct`
- `_btnDeleteProduct`
- `_btnRefresh`
- `_productsGrid`

Open `NewProductQuickForm.cs` with Shift+F7 to edit the new/edit product dialog.

No database update is required.
