# Mobile Fix Editing and Receipt Ticket Printing

## Editing

1. Select a repair.
2. Press `Edit Selected`, or double-click the row.
3. Change the problem description, cost price, or final price.
4. Press `Update`, `Update + Preview`, or `Update + Print`.

The customer identity fields are read-only in Edit mode.

Press `Cancel Edit / New` to leave Edit mode.

## Pending cashier transaction

When the selected repair is already in a pending cashier cart, updating its
prices also updates that pending cart line. The cart trigger recalculates its
line total.

## Delivered repairs

A delivered repair record and its newly printed repair ticket can be updated.
The original completed sales receipt remains unchanged because it is a
historical snapshot.

## Ticket printer

`MobileFixTicketPrinter.cs` uses the same shared settings as receipts:

```text
PrinterConfiguration.ApplyReceiptPrinter
PrinterConfiguration.ApplyReceiptOriginCompensation
PrinterConfiguration.ReceiptSidePaddingHundredths
80 mm paper width
dynamic paper height
zero software margins
```

The old A4-style Mobile Fixing PrintDocument layout is no longer used.

## Ticket price

The customer-facing ticket prints Final Price only. Internal Cost Price is not
printed.

No database update is required.
