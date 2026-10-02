# Barcode Quantity Printing

## Product & Stock-In

Below the Search / Scan box:

```text
No. of labels: [1]
[Print Barcode]
```

The selected product's barcode, description and current selling price are used.

The quantity field accepts 1–9,999 labels.

## Post Invoice

Enable:

```text
Print all barcodes (invoice QTY)
```

before pressing `Post Stock-In Invoice`.

Example:

```text
Phone Case    Invoice QTY 5  → 5 labels
USB Cable     Invoice QTY 3  → 3 labels
```

The barcode list is captured before the invoice screen resets.

## Products without barcodes

Lines without a barcode are skipped. The program tells the user how many lines
will be skipped before posting.

## Shared barcode layout

All labels use `BarcodeLabelHelper`:

```text
177 x 98
zero margins
Code 128 with checksum
40-pixel barcode height
scale 1
same number/description/price fonts and positions
```

This matches the existing Barcode page.

## Printing behavior

The button opens Print Preview, matching the Barcode page. The user can then
select Print from the preview.

No database update is required.
