# Validity Recharge — Deducted Price

Designer Edition 2.6 changes only Validity Recharge provider deduction logic.

## Admin setup

Open **Admin → Services → Validity Recharge**.

The editor now contains:

- Cost price — used for accounting and profit.
- Deducted price — amount removed from WishMoney when the sale is settled.
- Final price — amount charged to the customer.
- Profit — remains Final price minus Cost price.

Example:

- Cost price: $8.00
- Deducted price: $7.50
- Final price: $9.00
- Profit: $1.00

After settlement, WishMoney decreases by $7.50, not $8.00.

## Retake $

Retake behavior is unchanged. The entered Retake $ is credited to the selected MTC or Alfa line after settlement.

## Cancellation

Cancelling the validity sale restores the same Deducted Price to WishMoney and reverses the Retake $ credit from the mobile line.

## Other services

Mobile Recharge, Transfer Balance, Ushare, and Apps continue deducting their Cost Price from their provider.
