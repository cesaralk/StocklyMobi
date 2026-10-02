# Service Cost LBP and Two-Decimal USD

## Linked cost fields

Admin → Services now has:

```text
Cost price (USD)
Cost price (LBP)
```

The fields are linked using the rate saved in:

```text
Admin → Setting → Exchange Rate
```

Examples at 89,500 LBP:

```text
Cost USD 1.00   → Cost LBP 90,000
Cost LBP 100,000 → Cost USD 1.12
```

USD values are rounded to two decimal places.

## Final price

Final price USD/LBP conversion remains unchanged except that the USD result now
uses two decimals.

## Profit

Profit remains:

```text
Final price (USD) - Cost price (USD)
```

Validity's Deducted Price remains separate and is not changed by this update.

## Storage

The existing USD values remain the database values. Cost LBP and Final LBP are
conversion helpers and do not require new database columns.

No database update is required.
