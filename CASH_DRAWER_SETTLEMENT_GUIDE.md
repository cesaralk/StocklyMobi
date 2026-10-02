# Cash Drawer After Settlement

## Setup

Open:

```text
Admin → Printer Settings
```

Enable:

```text
Open the cash drawer after a successful settlement
```

Select the connector:

```text
Pin 2 — most common
Pin 5
```

Press `Test Drawer` before completing a real sale.

## Settlement sequence

The drawer command is sent only after `CompleteTransaction` succeeds.

Therefore:

- Insufficient payment does not open the drawer.
- A database failure does not open the drawer.
- Drawer failure does not undo a completed sale.
- Receipt printing remains independent.

## Enter key

Pressing Enter in Settle performs the settlement once. A guard prevents repeated
Enter events from submitting the same transaction again.

## Connection

The cash drawer must be connected to the receipt printer's drawer port.

The program sends a standard ESC/POS RAW pulse through the configured receipt
printer.

No database update is required.
