# Invoice Barcode Selection

## Exclude products

Use the `Print Barcode` checkbox in each invoice row.

Unchecked products are excluded from both printing modes.

## Print one each

Press `Print 1 Each` to preview one label for every checked product.

## Print invoice quantities

Enable `After post: invoice QTY` before posting. Checked products print using
their invoice quantities.

Example:

```text
Product A   QTY 10   checked   → 1 with Print 1 Each, 10 after post
Product B   QTY 4    unchecked → 0 in both modes
```

The barcode size and layout remain exactly the Barcode-page settings.

No database update is required.
