# Settlement Receipt Printing

## Fixed error

The old settlement print route used `rptRecept.rdlc`, which produced:

```text
An error occurred during local report processing.
```

Settlement now prints from the saved receipt snapshot with `PrintDocument`, so
the RDLC LocalReport renderer is no longer used when Enter is pressed.

## ON/OFF button

- Green `Print Receipt: ON`: print after settlement.
- Gray `Print Receipt: OFF`: save without printing.

The global Printer Settings option decides the initial state each time Settle
opens. The Settle button changes only the current transaction.

## Reprint safety

Whether printing is OFF or a printer error occurs, the completed receipt remains
available in Receipt Search.

## Designer

Open `Settle.cs` with Shift+F7. The new control is:

```text
chkPrintReceipt
```

No database update is required.
