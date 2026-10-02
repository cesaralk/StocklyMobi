# StocklyMobi 2.18.1 — Cash Drawer Backport

This release keeps the Product & Stock-In design from StocklyMobi 2.18 and adds only the cash-drawer functionality from 2.20.1.

## Preserved from 2.18

- Product & Stock-In layout and behavior
- Invoice barcode row selection
- Invoice quantity barcode printing
- Existing receipt, A4 invoice, service, repair, reports, and database behavior

No Product & Stock-In files were taken from 2.19 or 2.20.1.

## Added from 2.20.1

- Receipt-printer cash-drawer configuration
- Pin 2 / Pin 5 selection
- Six ESC/POS compatibility profiles
- Test Drawer button
- Automatic drawer opening only after a transaction is successfully committed in the Settle form
- Duplicate-settlement protection when Enter is pressed more than once

## Required printer configuration

For the XP-T80Q Windows printer driver:

- Cash Drawer Before Printing: Off / Disabled
- Cash Drawer After Printing: Off / Disabled

Inside StocklyMobi > Admin > Printer Settings:

- Receipt printer: XP-T80Q
- Open the cash drawer after a successful settlement: Enabled
- Drawer connector: Pin 2
- Command profile: any tested working profile; Profile 1 is a safe default

The Windows driver must not open the drawer automatically. StocklyMobi sends its own drawer command only after a successful Settle operation. Receipt reprints, printer tests, barcode labels, and A4 invoices do not call the drawer controller.

## Database

No SQL migration is required.

## Validation scope

This package was statically checked in Linux. Visual Studio compilation and physical printer/drawer testing must be completed on Windows.
