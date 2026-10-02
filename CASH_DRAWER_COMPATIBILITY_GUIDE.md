# Cash Drawer Compatibility Profiles

## Recommended test order

Use Pin 2 first and test profiles in the displayed order.

The Test Drawer dialog asks whether the drawer physically opened. Answering No
selects the next profile automatically.

## Profiles

1. Generic ASCII 48/49 with `ESC @`, pulse 60/120
2. Standard binary 0/1 with `ESC @`, pulse 25/250
3. Standard ASCII 48/49 with `ESC @`, pulse 25/250
4. Short binary 0/1 with `ESC @`, pulse 50/100
5. Short ASCII 48/49 with `ESC @`, pulse 50/100
6. Legacy binary without `ESC @`

## When none opens the drawer

Check:

- Drawer cable is connected to the printer's DK / Cash Drawer port
- The drawer is not connected directly to the computer
- Printer driver is the correct manufacturer driver
- Printer self-test or Windows driver has a Cash Drawer option
- Drawer voltage matches the printer port
- The exact printer brand and model

No database update is required.
