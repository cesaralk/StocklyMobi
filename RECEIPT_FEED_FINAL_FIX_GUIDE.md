# Final Receipt Feed Fix

## Fixed behavior

Receipt paper height is no longer forced to 700 units.

The program measures the actual receipt and creates a page only slightly taller
than the printed content. This prevents thermal drivers from feeding a large
unused page section before the first line.

Expected spare feed for a normal short receipt:

```text
approximately 6 mm
```

The target is below 1 cm.

## Applies to

- Settle receipt printing
- Receipt Search printing
- Receipt Search preview

All three use `HistoricalReceiptPrinter`.

## Fine adjustment

Settings → Printers still has:

```text
Fine top trim (mm)
```

Keep it at 0.0 first. It is now limited to 0–2 mm.

Large old values are reset automatically.

## Main code location

The dynamic page calculation is in:

```text
HistoricalReceiptPrinter.cs
CalculateReceiptPaperHeight()
```

The previous problem was this fixed minimum:

```csharp
Math.Max(700, ...)
```

That fixed 700-unit minimum has been removed.

No database update is required.
