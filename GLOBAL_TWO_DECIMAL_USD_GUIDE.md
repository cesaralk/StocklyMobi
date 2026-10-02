# Global Two-Decimal USD Rule

## Display format

All USD values throughout StocklyMobi now use exactly:

```text
0.00
```

Examples:

```text
0     → 0.00
1.2   → 1.20
1.236 → 1.24
```

## Areas covered

- Product cost and selling price
- Product grids and lookup
- Stock-In cost and price
- Stock-In history
- Supplier invoice lines and history
- Mobile fixing
- Services, profit and provider balances
- Cashier service balance cards
- Validity Retake
- Daily Profit and reports
- Barcode labels
- Exchange-rate preview
- Cashier, Settle and receipts

## Rounding

Editable product and stock pricing calculations use two-decimal
`MidpointRounding.AwayFromZero`.

## Database

SQL columns retain their existing precision. No migration is required.

## Not currency

These remain unchanged because they are not USD:

- LBP values
- Date/time values
- Printer dimensions
- Internal floating-point comparison tolerances
