# Service LBP Pricing and Admin Exchange Rate

## Recommended USD precision

Three decimals are used in Admin Services. This is the best balance for mobile
recharge pricing because values such as 0.729 remain accurate, while the page no
longer shows four or more distracting decimals.

Cashier totals and printed receipts remain at two decimals.

## Linked final price

In Admin → Services:

```text
Final price (USD)
Final price (LBP)
```

Changing either field recalculates the other using the saved system rate.

LBP is rounded to the nearest whole Lebanese pound. USD calculated from LBP is
rounded to three decimals.

## Rate location

The exchange rate is no longer editable in Cashier.

Open:

```text
Admin → Setting → Exchange Rate
```

The rate is saved in the existing `ExchangeRateLBP` user setting and is shared
by Cashier, Settle, and Services.

## Database

No database change is required. The LBP field is a conversion helper and the
service continues saving its final price in USD.
