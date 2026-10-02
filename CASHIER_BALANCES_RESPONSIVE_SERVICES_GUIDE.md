# Cashier Balances and Responsive Services

## Cashier balances

The cashier can press **Provider Balances** in the mobile-service action area.
The window is read-only and shows every active provider or mobile line with its
current balance.

Every cashier Service Picker also shows a read-only provider-balance strip.
Balances change only after settlement, because pending cart services do not
modify provider wallets. Press Refresh after a settlement if the picker was
already open.

## Responsive Admin Services

On a wide laptop screen, the configured-services list and service editor are
shown side by side. This gives the editor the full available height.

On narrow windows, they stack vertically and the editor panels scroll safely.
New Service, Save Service, Refresh, New Provider, Save Provider, Add Balance,
and Remove Balance use fixed action rows so they remain accessible.

All new balance controls and the new balance window are editable through the
WinForms Designer.

No SQL or database update is required.
