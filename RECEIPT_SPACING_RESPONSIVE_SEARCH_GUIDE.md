# Receipt Spacing and Responsive Receipt Search

## Receipt top spacing

The receipt starts at the first printable position instead of adding another
software top margin.

A receipt printer can still have a small physical hard margin that software
cannot remove safely. The additional blank area previously created by the POS
has been removed.

## Receipt Search

Receipt Search opens maximized and adapts to the available height and width.

On shorter laptop displays:

- The title area becomes shorter.
- The filter area becomes shorter.
- The details footer becomes shorter.
- Print and Print Preview remain visible.
- The receipt list and details can stack when side-by-side space is insufficient.

## Designer controls

The responsive controls remain editable in:

```text
ReceiptHistoryForm.Designer.cs
```

Important controls:

```text
tblFooter
flowActions
btnPrint
btnPreview
_split
pnlFilters
lblHeader
```

No database update is required.
