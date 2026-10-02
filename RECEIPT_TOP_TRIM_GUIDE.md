# Receipt Top Trim

## What changed

Receipt printing now compensates for the Windows printer driver's reported hard
left and top margins.

A manual top-trim value is also available because some thermal-printer drivers
feed extra paper that does not match their reported margin.

## Adjusting it

Open:

```text
Settings → Printers
```

Set:

```text
Receipt top trim (mm)
```

Begin at 0.0 mm and print a test receipt.

Increase gradually:

```text
0.5
1.0
1.5
2.0
```

Stop when the receipt title starts close to the top.

Do not use a larger value than necessary. If the first line is clipped, lower
the setting.

## Scope

The value applies to every snapshot receipt:

- Settle
- Receipt Search Print
- Receipt Search Preview
- Test Receipt

A4 stock-in and invoice printing is not affected.

No database update is required.
