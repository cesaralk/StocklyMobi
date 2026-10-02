# Receipt Side Margin Fix

## New receipt width behavior

The receipt now uses:

```text
approximately 3 mm left inner margin
approximately 3 mm right inner margin
```

These margins are added inside the printer's own physical printable area.

The program no longer translates receipt graphics left by `HardMarginX`.

## Main code locations

```text
PrinterConfiguration.cs
HistoricalReceiptPrinter.cs
```

The shared margin value is:

```csharp
PrinterConfiguration.ReceiptSidePaddingHundredths
```

Its default value is:

```csharp
12f
```

This is approximately 3 mm.

The content area also uses the printer driver's `PrintableArea.Width` and the
graphics `VisibleClipBounds.Width`, choosing the safer width.

## Applies to

- Settle
- Receipt Search Print
- Receipt Search Preview
- Test Receipt

The vertical feed correction from Designer Edition 2.11 remains unchanged.

No database update is required.
