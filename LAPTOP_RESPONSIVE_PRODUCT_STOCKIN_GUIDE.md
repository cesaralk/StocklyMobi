# Laptop-Responsive Products & Stock-In

## Layout breakpoints

The screen chooses between:

```text
Products | Invoice
```

and:

```text
Products
Invoice
```

based on the available content width and height.

## Product toolbar

The product toolbar automatically changes between:

```text
Search
Barcode controls | Product actions
```

and:

```text
Search
Barcode controls
Product actions
```

This prevents buttons from overlapping on laptop screens.

## Invoice footer

At normal laptop panel widths, all invoice actions remain on one compact row.

At narrower widths, the actions wrap cleanly into two rows while the footer
remains fixed at the bottom.

## Grid order

Essential columns are placed first and frozen in side-by-side mode.

Secondary columns remain available through horizontal scrolling.

## Screen fit

The form automatically fits inside the Windows working area with a small margin
for the taskbar and borders.

No database update is required.
