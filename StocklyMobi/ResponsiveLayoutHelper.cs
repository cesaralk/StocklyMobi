using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace StocklyMobi
{
    internal static class ResponsiveLayoutHelper
    {
        public static void SetSafeSplitter(SplitContainer split, int preferredDistance, int panel1Minimum, int panel2Minimum)
        {
            if (split == null || split.IsDisposed) return;

            int total = split.Orientation == Orientation.Vertical
                ? split.ClientSize.Width
                : split.ClientSize.Height;

            if (total <= split.SplitterWidth + 2) return;

            panel1Minimum = Math.Max(0, panel1Minimum);
            panel2Minimum = Math.Max(0, panel2Minimum);

            // Clear minimums first. WinForms can otherwise throw while the control is
            // still being created and its temporary size is smaller than the final form.
            split.Panel1MinSize = 0;
            split.Panel2MinSize = 0;

            int maximumDistance = Math.Max(1, total - split.SplitterWidth - 1);
            int distance = Math.Max(1, Math.Min(preferredDistance, maximumDistance));
            bool canApplyMinimums = total >= panel1Minimum + panel2Minimum + split.SplitterWidth;

            if (canApplyMinimums)
            {
                int maximumWithSecondPanel = total - panel2Minimum - split.SplitterWidth;
                distance = Math.Max(panel1Minimum, Math.Min(distance, maximumWithSecondPanel));
            }

            split.SplitterDistance = distance;

            if (canApplyMinimums)
            {
                split.Panel1MinSize = panel1Minimum;
                split.Panel2MinSize = panel2Minimum;
            }
        }

        public static void Apply(Form form)
        {
            if (form == null) return;

            form.Shown -= HandleLayoutEvent;
            form.Resize -= HandleLayoutEvent;
            form.Shown += HandleLayoutEvent;
            form.Resize += HandleLayoutEvent;

            ApplyLayout(form);
        }

        private static void HandleLayoutEvent(object sender, EventArgs e)
        {
            Form form = sender as Form;
            if (form != null)
                ApplyLayout(form);
        }

        private static void ApplyLayout(Form form)
        {
            switch (form.GetType().Name)
            {
                case "MainForm":
                    LayoutMainForm(form);
                    break;
                case "Cashier":
                    LayoutCashier(form);
                    break;
                case "Dashboard":
                    LayoutDashboard(form);
                    break;
                case "Adjustments":
                    LayoutAdjustments(form);
                    break;
                case "StockIn":
                    LayoutStockIn(form);
                    break;
                case "UserAccount":
                    LayoutUserAccount(form);
                    break;
                case "Store":
                    LayoutStore(form);
                    break;
                case "Product":
                case "ProductStockIn":
                case "LookUpProduct":
                case "Barcode":
                case "Brand":
                case "Category":
                case "Supplier":
                    LayoutSearchGridFooter(form);
                    break;
                case "DailySale":
                    LayoutDailySale(form);
                    break;
                case "Record":
                    LayoutRecord(form);
                    break;
            }

            ConfigureDataGrids(form);
        }

        private static T Find<T>(Control root, string name) where T : Control
        {
            return root.Controls.Find(name, true).FirstOrDefault() as T;
        }

        private static IEnumerable<T> FindAll<T>(Control root) where T : Control
        {
            foreach (Control child in root.Controls)
            {
                if (child is T)
                    yield return (T)child;

                foreach (T nested in FindAll<T>(child))
                    yield return nested;
            }
        }

        private static void LayoutMainForm(Form form)
        {
            form.MinimumSize = new Size(1184, 700);
            form.FormBorderStyle = FormBorderStyle.Sizable;
            form.MaximizeBox = true;
        }

        private static void LayoutCashier(Form form)
        {
            form.MinimumSize = new Size(1184, 700);
            form.FormBorderStyle = FormBorderStyle.Sizable;
            form.MaximizeBox = true;
        }

        private static void LayoutDashboard(Form form)
        {
            form.MinimumSize = new Size(968, 582);

            Panel cardsHost = Find<Panel>(form, "panel1");
            Panel card1 = Find<Panel>(form, "panel2");
            Panel card2 = Find<Panel>(form, "panel4");
            Panel card3 = Find<Panel>(form, "panel8");
            Panel card4 = Find<Panel>(form, "panel6");

            if (cardsHost == null || card1 == null || card2 == null || card3 == null || card4 == null)
                return;

            cardsHost.Dock = DockStyle.Top;
            int spacing = 12;
            int y = 12;
            int minCardWidth = 180;
            int availableWidth = cardsHost.ClientSize.Width - (spacing * 5);
            int cardWidth = Math.Max(minCardWidth, availableWidth / 4);
            int totalNeeded = spacing + (cardWidth + spacing) * 4;

            if (totalNeeded > cardsHost.ClientSize.Width)
                cardWidth = Math.Max(150, (cardsHost.ClientSize.Width - (spacing * 5)) / 4);

            Panel[] cards = { card1, card2, card3, card4 };
            int x = spacing;
            foreach (Panel card in cards)
            {
                card.SetBounds(x, y, cardWidth, 114);
                card.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                x += cardWidth + spacing;
            }
        }

        private static void LayoutSearchGridFooter(Form form)
        {
            DockPanelIfExists(form, "panel1", DockStyle.Bottom);
            AnchorIfExists(form, "btnAdd", AnchorStyles.Top | AnchorStyles.Right);
            AnchorIfExists(form, "btnPrint", AnchorStyles.Top | AnchorStyles.Right);
            AnchorIfExists(form, "btnClose", AnchorStyles.Bottom | AnchorStyles.Right);

            Control footer = Find<Control>(form, "panel1");
            Control label = Find<Control>(form, "label1");
            Control search = Find<Control>(form, "txtSearch");
            Control grid = Find<Control>(form, "dgvProduct")
                           ?? Find<Control>(form, "dgvBarcode")
                           ?? Find<Control>(form, "dgvBrand")
                           ?? Find<Control>(form, "dgvCategory")
                           ?? Find<Control>(form, "dgvSupplier");

            if (grid != null)
                grid.Dock = DockStyle.Fill;

            if (footer != null && search != null)
            {
                search.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                int left = Math.Max(label != null ? label.Right + 20 : 180, 180);
                int rightReserve = 20;
                foreach (Control ctl in footer.Controls)
                {
                    if (ctl != search && ctl.Visible && ctl.Right > search.Left)
                        rightReserve = Math.Max(rightReserve, footer.ClientSize.Width - ctl.Left + 10);
                }
                search.SetBounds(left, search.Top, Math.Max(220, footer.ClientSize.Width - left - rightReserve), search.Height);
            }

            Control barcode = Find<Control>(form, "txtBarcode");
            if (footer != null && barcode != null)
            {
                barcode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                int rightReserve = 20;
                foreach (Control ctl in footer.Controls)
                {
                    if (ctl != barcode && ctl.Visible && ctl.Right > barcode.Left)
                        rightReserve = Math.Max(rightReserve, footer.ClientSize.Width - ctl.Left + 10);
                }
                barcode.SetBounds(barcode.Left, barcode.Top, Math.Max(220, footer.ClientSize.Width - barcode.Left - rightReserve), barcode.Height);
            }
        }

        private static void LayoutAdjustments(Form form)
        {
            form.MinimumSize = new Size(1035, 620);
            DockPanelIfExists(form, "panel2", DockStyle.Top);
            DockPanelIfExists(form, "panel1", DockStyle.Bottom);
            Control grid = Find<Control>(form, "dgvAdjustment");
            if (grid != null) grid.Dock = DockStyle.Fill;

            Control footer = Find<Control>(form, "panel1");
            Control label = Find<Control>(form, "label1");
            Control search = Find<Control>(form, "txtSearch");
            if (footer != null && search != null)
            {
                search.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                int left = Math.Max(label != null ? label.Right + 20 : 180, 180);
                int rightReserve = 100;
                Control save = Find<Control>(form, "btnSave");
                if (save != null)
                    rightReserve = Math.Max(rightReserve, footer.ClientSize.Width - save.Left + 12);
                search.SetBounds(left, search.Top, Math.Max(240, footer.ClientSize.Width - left - rightReserve), search.Height);
            }
        }

        private static void LayoutStockIn(Form form)
        {
            form.MinimumSize = new Size(984, 620);
            DockPanelIfExists(form, "panel1", DockStyle.Bottom);
            Control tabs = Find<Control>(form, "metroTabControl1");
            if (tabs != null) tabs.Dock = DockStyle.Fill;

            TabPage tab1 = Find<TabPage>(form, "tabPage1");
            Panel header = Find<Panel>(form, "panel2");
            DataGridView stockGrid = Find<DataGridView>(form, "dgvStockIn");
            Button entryButton = Find<Button>(form, "btnEntry");
            Button printButton = Find<Button>(form, "btnPrintStockLabel");
            Button generateButton = Find<Button>(form, "btnGenerateStockBarcode");
            if (tab1 != null && header != null && stockGrid != null && entryButton != null)
            {
                header.Dock = DockStyle.Top;
                int bottomY = tab1.ClientSize.Height - entryButton.Height - 8;
                int right = tab1.ClientSize.Width - 10;

                entryButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                entryButton.Location = new Point(right - entryButton.Width, bottomY);

                if (printButton != null)
                {
                    printButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                    printButton.Location = new Point(entryButton.Left - printButton.Width - 8, bottomY);
                }

                if (generateButton != null)
                {
                    generateButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                    int leftOf = printButton != null ? printButton.Left : entryButton.Left;
                    generateButton.Location = new Point(leftOf - generateButton.Width - 8, bottomY);
                }

                stockGrid.Dock = DockStyle.None;
                stockGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                int top = header.Bottom;
                int actionTop = generateButton != null ? generateButton.Top : (printButton != null ? printButton.Top : entryButton.Top);
                int height = Math.Max(140, actionTop - top - 8);
                stockGrid.SetBounds(0, top, tab1.ClientSize.Width, height);

                StretchToRight(tab1, "cbSupplier", 12);
                StretchToRight(tab1, "txtConPerson", 12);
                StretchToRight(tab1, "txtAddress", 12);
            }

            TabPage tab2 = Find<TabPage>(form, "tabPage2");
            Panel filter = Find<Panel>(form, "panel3");
            DataGridView historyGrid = Find<DataGridView>(form, "dgvInStockHistory");
            if (tab2 != null && filter != null && historyGrid != null)
            {
                filter.Dock = DockStyle.Top;
                historyGrid.Dock = DockStyle.None;
                historyGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                historyGrid.SetBounds(0, filter.Bottom, tab2.ClientSize.Width, Math.Max(140, tab2.ClientSize.Height - filter.Bottom));
            }
        }

        private static void LayoutUserAccount(Form form)
        {
            form.MinimumSize = new Size(984, 620);
            DockPanelIfExists(form, "panel1", DockStyle.Bottom);

            Control tabs = Find<Control>(form, "metroTabControl1");
            if (tabs != null)
            {
                tabs.Dock = DockStyle.None;
                tabs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                tabs.SetBounds(12, 12, form.ClientSize.Width - 24, form.ClientSize.Height - 12 - 80 - 12);
            }

            foreach (string name in new[] { "txtUsername", "txtPass", "txtRePass", "cbRole", "txtName", "txtCurPass", "txtNPass", "txtRePass2" })
                AnchorIfExists(form, name, AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);

            AnchorIfExists(form, "dgvUser", AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            AnchorIfExists(form, "gbUser", AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            AnchorIfExists(form, "btnProperties", AnchorStyles.Bottom | AnchorStyles.Right);
            AnchorIfExists(form, "btnRemove", AnchorStyles.Bottom | AnchorStyles.Right);
            AnchorIfExists(form, "btnResetPass", AnchorStyles.Top | AnchorStyles.Right);
        }

        private static void LayoutStore(Form form)
        {
            form.MinimumSize = new Size(708, 260);
            AnchorIfExists(form, "txtStName", AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            AnchorIfExists(form, "txtAddress", AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            AnchorIfExists(form, "btnSave", AnchorStyles.Bottom | AnchorStyles.Right);
            AnchorIfExists(form, "btnCancel", AnchorStyles.Bottom | AnchorStyles.Right);
        }

        private static void LayoutDailySale(Form form)
        {
            form.MinimumSize = new Size(1010, 620);
            DockPanelIfExists(form, "panel1", DockStyle.Top);
            DockPanelIfExists(form, "panel2", DockStyle.Bottom);

            Control grid = Find<Control>(form, "dgvSold");
            if (grid != null) grid.Dock = DockStyle.Fill;

            Panel footer = Find<Panel>(form, "panel2");
            Label fromLabel = Find<Label>(form, "label2");
            Label toLabel = Find<Label>(form, "label3");
            DateTimePicker fromDate = Find<DateTimePicker>(form, "dtFrom");
            DateTimePicker toDate = Find<DateTimePicker>(form, "dtTo");
            ComboBox cashier = Find<ComboBox>(form, "cboCashier");
            Button print = Find<Button>(form, "btnPrint");
            Label oldTotal = Find<Label>(form, "lblTotal");
            Label summary = Find<Label>(form, "lblSalesSummary");

            if (footer == null)
                return;

            footer.Height = Math.Max(90, footer.Height);

            if (oldTotal != null)
                oldTotal.Visible = false;

            int y = 14;
            int x = 10;
            int gap = 8;

            if (fromLabel != null)
            {
                fromLabel.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                fromLabel.Location = new Point(x, y + 4);
                x = fromLabel.Right + gap;
            }

            if (fromDate != null)
            {
                fromDate.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                fromDate.Width = 210;
                fromDate.Location = new Point(x, y);
                x = fromDate.Right + 20;
            }

            if (toLabel != null)
            {
                toLabel.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                toLabel.Location = new Point(x, y + 4);
                x = toLabel.Right + gap;
            }

            if (toDate != null)
            {
                toDate.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                toDate.Width = 210;
                toDate.Location = new Point(x, y);
                x = toDate.Right + 20;
            }

            if (cashier != null)
            {
                cashier.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                int printWidth = print != null ? print.Width : 42;
                int maxCashierWidth = footer.ClientSize.Width - x - printWidth - 30;
                cashier.Width = Math.Max(150, Math.Min(260, maxCashierWidth));
                cashier.Location = new Point(x, y);
                x = cashier.Right + 8;
            }

            if (print != null)
            {
                print.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                print.Location = new Point(x, y - 3);
            }

            if (summary != null)
            {
                summary.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
                summary.Location = new Point(10, footer.Height - 32);
                summary.Size = new Size(Math.Max(100, footer.ClientSize.Width - 20), 24);
                summary.TextAlign = ContentAlignment.MiddleRight;
            }
        }

        private static void LayoutRecord(Form form)
        {
            form.MinimumSize = new Size(984, 620);
            DockPanelIfExists(form, "panel1", DockStyle.Bottom);
            Control tabs = Find<Control>(form, "metroTabControl1");
            if (tabs != null) tabs.Dock = DockStyle.Fill;

            foreach (string buttonName in new[] { "btnPrintTopSell", "btnPrintSoldItems", "btnPrintInventoryList", "btnPrintCancel", "btnPrintStockIn", "btnLoadStockIn" })
                AnchorIfExists(form, buttonName, AnchorStyles.Top | AnchorStyles.Right);
        }

        private static void ConfigureDataGrids(Form form)
        {
            foreach (DataGridView grid in FindAll<DataGridView>(form))
                ConfigureGridBase(grid);

            switch (form.GetType().Name)
            {
                case "Product":
                    ConfigureProductGrid(Find<DataGridView>(form, "dgvProduct"));
                    break;
                case "LookUpProduct":
                    ConfigureLookupGrid(Find<DataGridView>(form, "dgvProduct"));
                    break;
                case "ProductStockIn":
                    ConfigureProductStockInGrid(Find<DataGridView>(form, "dgvProduct"));
                    break;
                case "Barcode":
                    ConfigureLookupGrid(Find<DataGridView>(form, "dgvBarcode"));
                    break;
                case "Adjustments":
                    ConfigureAdjustmentsGrid(Find<DataGridView>(form, "dgvAdjustment"));
                    break;
                case "Brand":
                    ConfigureBrandCategoryGrid(Find<DataGridView>(form, "dgvBrand"), "Brand");
                    break;
                case "Category":
                    ConfigureBrandCategoryGrid(Find<DataGridView>(form, "dgvCategory"), "Category");
                    break;
                case "Supplier":
                    ConfigureSupplierGrid(Find<DataGridView>(form, "dgvSupplier"));
                    break;
                case "StockIn":
                    ConfigureStockInGrid(Find<DataGridView>(form, "dgvStockIn"));
                    ConfigureStockInHistoryGrid(Find<DataGridView>(form, "dgvInStockHistory"));
                    break;
                case "Record":
                    ConfigureRecordGrids(form);
                    break;
            }
        }

        private static void ConfigureGridBase(DataGridView grid)
        {
            if (grid == null) return;

            grid.RowHeadersVisible = false;
            grid.AllowUserToResizeRows = false;
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.ScrollBars = ScrollBars.Both;

            foreach (DataGridViewColumn col in grid.Columns)
            {
                if (col is DataGridViewImageColumn)
                    ((DataGridViewImageColumn)col).ImageLayout = DataGridViewImageCellLayout.Zoom;
            }
        }

        private static void ConfigureProductGrid(DataGridView grid)
        {
            if (grid == null) return;

            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            SetFixed(grid, "Column1", 50);
            SetFixed(grid, "Column2", 90);
            SetFixed(grid, "Column3", 140);
            SetFill(grid, "Column4", 220, 220);
            SetFixed(grid, "Column5", 120);
            SetFixed(grid, "Column6", 130);
            SetFixed(grid, "Column9", 95, true);
            SetFixed(grid, "Column8", 85, true);
            SetFixed(grid, "Column7", 85, true);
            SetFixed(grid, "Column10", 80, true);
            SetButton(grid, "Edit");
            SetButton(grid, "Delete");
        }

        private static void ConfigureLookupGrid(DataGridView grid)
        {
            if (grid == null) return;

            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            SetFixed(grid, "Column1", 50);
            SetFixed(grid, "Column2", 90);
            SetFixed(grid, "Column3", 140);
            SetFill(grid, "Column4", 230, 240);
            SetFixed(grid, "Column5", 115);
            SetFixed(grid, "Column6", 125);
            SetFixed(grid, "Column7", 80, true);
            SetFixed(grid, "Column8", 65, true);
            SetButton(grid, "Select");
        }

        private static void ConfigureProductStockInGrid(DataGridView grid)
        {
            if (grid == null) return;

            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            SetFixed(grid, "Column1", 50);
            SetFixed(grid, "Column2", 90);
            SetFill(grid, "Column4", 280, 260);
            SetFixed(grid, "Column8", 70, true);
            SetButton(grid, "Select");
        }

        private static void ConfigureAdjustmentsGrid(DataGridView grid)
        {
            if (grid == null) return;

            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            SetFixed(grid, "Column1", 50);
            SetFixed(grid, "Column2", 90);
            SetFixed(grid, "Column3", 135);
            SetFill(grid, "Column4", 240, 240);
            SetFixed(grid, "Column5", 110);
            SetFixed(grid, "Column6", 120);
            SetFixed(grid, "Column7", 80, true);
            SetFixed(grid, "Column8", 90, true);
            SetButton(grid, "Select");
        }

        private static void ConfigureBrandCategoryGrid(DataGridView grid, string dataColumnName)
        {
            if (grid == null) return;

            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            SetFixed(grid, "Column1", 50);
            SetFixed(grid, "Column2", 60);
            SetFill(grid, "Column3", 200, 220);
            SetButton(grid, "Edit");
            SetButton(grid, "Delete");
        }

        private static void ConfigureSupplierGrid(DataGridView grid)
        {
            if (grid == null) return;

            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            SetFixed(grid, "Column1", 50);
            SetFixed(grid, "Column8", 60);
            SetFixed(grid, "Column2", 150);
            SetFill(grid, "Column3", 200, 220);
            SetFixed(grid, "Column4", 140);
            SetFixed(grid, "Column5", 110);
            SetFixed(grid, "Column6", 160);
            SetFixed(grid, "Column7", 100);
            SetButton(grid, "Edit");
            SetButton(grid, "Delete");
        }

        private static void ConfigureStockInGrid(DataGridView grid)
        {
            if (grid == null) return;

            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            SetFixed(grid, "Column1", 45);
            SetFixed(grid, "Column9", 55);
            SetFixed(grid, "Column10", 110);
            SetFixed(grid, "Column2", 90);
            SetFill(grid, "Column4", 190, 220);
            SetFixed(grid, "Column5", 60, true);
            SetFixed(grid, "colCostPrice", 90, true);
            SetFixed(grid, "colMarkup", 80, true);
            SetFixed(grid, "colPrice", 80, true);
            SetFixed(grid, "Column6", 115);
            SetFixed(grid, "Column7", 110);
            SetFixed(grid, "Column8", 140);
            SetButton(grid, "Delete");
        }

        private static void ConfigureStockInHistoryGrid(DataGridView grid)
        {
            if (grid == null) return;

            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            SetFixed(grid, "dataGridViewTextBoxColumn1", 45);
            SetFixed(grid, "dataGridViewTextBoxColumn2", 55);
            SetFixed(grid, "dataGridViewTextBoxColumn3", 110);
            SetFixed(grid, "dataGridViewTextBoxColumn4", 90);
            SetFill(grid, "dataGridViewTextBoxColumn5", 200, 220);
            SetFixed(grid, "dataGridViewTextBoxColumn6", 60, true);
            SetFixed(grid, "dataGridViewTextBoxColumnCostPrice", 90, true);
            SetFixed(grid, "dataGridViewTextBoxColumnMarkup", 80, true);
            SetFixed(grid, "dataGridViewTextBoxColumnPrice", 80, true);
            SetFixed(grid, "dataGridViewTextBoxColumn7", 115);
            SetFixed(grid, "dataGridViewTextBoxColumn8", 110);
            SetFixed(grid, "dataGridViewTextBoxColumn9", 140);
        }

        private static void ConfigureRecordGrids(Form form)
        {
            ConfigureRecordTopSelling(Find<DataGridView>(form, "dgvTopSelling"));
            ConfigureRecordSoldItems(Find<DataGridView>(form, "dgvSoldItems"));
            ConfigureRecordCritical(Find<DataGridView>(form, "dgvCriticalItems"));
            ConfigureRecordInventory(Find<DataGridView>(form, "dgvInventoryList"));
            ConfigureRecordCancel(Find<DataGridView>(form, "dgvCancel"));
            ConfigureRecordStockIn(Find<DataGridView>(form, "dgvStockIn"));
        }

        private static void ConfigureRecordTopSelling(DataGridView grid)
        {
            if (grid == null) return;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            SetFixed(grid, "Column1", 50);
            SetFixed(grid, "Column2", 90);
            SetFill(grid, "Column4", 250, 240);
            SetFixed(grid, "Column6", 70, true);
            SetFixed(grid, "Column7", 110, true);
        }

        private static void ConfigureRecordSoldItems(DataGridView grid)
        {
            if (grid == null) return;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            SetFixed(grid, "dataGridViewTextBoxColumn1", 50);
            SetFixed(grid, "dataGridViewTextBoxColumn2", 90);
            SetFill(grid, "dataGridViewTextBoxColumn3", 250, 240);
            SetFixed(grid, "Column16", 80, true);
            SetFixed(grid, "dataGridViewTextBoxColumn4", 65, true);
            SetFixed(grid, "Column3", 80, true);
            SetFixed(grid, "dataGridViewTextBoxColumn5", 95, true);
        }

        private static void ConfigureRecordCritical(DataGridView grid)
        {
            if (grid == null) return;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            SetFixed(grid, "dataGridViewTextBoxColumn6", 50);
            SetFixed(grid, "dataGridViewTextBoxColumn7", 90);
            SetFixed(grid, "dataGridViewTextBoxColumn8", 135);
            SetFill(grid, "dataGridViewTextBoxColumn9", 220, 220);
            SetFixed(grid, "Column5", 110);
            SetFixed(grid, "dataGridViewTextBoxColumn10", 120);
            SetFixed(grid, "dataGridViewTextBoxColumn11", 80, true);
            SetFixed(grid, "Column8", 75, true);
            SetFixed(grid, "Column9", 90, true);
        }

        private static void ConfigureRecordInventory(DataGridView grid)
        {
            if (grid == null) return;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            SetFixed(grid, "dataGridViewTextBoxColumn12", 50);
            SetFixed(grid, "dataGridViewTextBoxColumn13", 90);
            SetFixed(grid, "dataGridViewTextBoxColumn14", 135);
            SetFill(grid, "dataGridViewTextBoxColumn15", 220, 220);
            SetFixed(grid, "dataGridViewTextBoxColumn16", 110);
            SetFixed(grid, "dataGridViewTextBoxColumn17", 120);
            SetFixed(grid, "dataGridViewTextBoxColumn18", 80, true);
            SetFixed(grid, "dataGridViewTextBoxColumn20", 65, true);
            SetFixed(grid, "dataGridViewTextBoxColumn19", 80, true);
            SetFixedByNameIfExists(grid, "colInvCostPrice", 90, true);
            SetFixedByNameIfExists(grid, "colInvMarkup", 80, true);
        }

        private static void ConfigureRecordCancel(DataGridView grid)
        {
            if (grid == null) return;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            SetFixed(grid, "dataGridViewTextBoxColumn21", 50);
            SetFixed(grid, "dataGridViewTextBoxColumn22", 110);
            SetFixed(grid, "Column10", 90);
            SetFill(grid, "dataGridViewTextBoxColumn23", 220, 220);
            SetFixed(grid, "Column11", 80, true);
            SetFixed(grid, "dataGridViewTextBoxColumn24", 65, true);
            SetFixed(grid, "dataGridViewTextBoxColumn26", 95, true);
            SetFixed(grid, "dataGridViewTextBoxColumn25", 100);
            SetFixed(grid, "Column12", 120);
            SetFixed(grid, "Column13", 120);
            SetFixed(grid, "Column14", 150);
            SetFixed(grid, "Column15", 110);
        }

        private static void ConfigureRecordStockIn(DataGridView grid)
        {
            if (grid == null) return;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            SetFixed(grid, "dataGridViewTextBoxColumn27", 50);
            SetFixed(grid, "dataGridViewTextBoxColumn28", 55);
            SetFixed(grid, "dataGridViewTextBoxColumn31", 115);
            SetFixed(grid, "dataGridViewTextBoxColumn29", 90);
            SetFill(grid, "dataGridViewTextBoxColumn30", 220, 220);
            SetFixed(grid, "dataGridViewTextBoxColumn32", 65, true);
            SetFixed(grid, "dataGridViewTextBoxColumn33", 115);
            SetFixed(grid, "dataGridViewTextBoxColumn34", 110);
            SetFixed(grid, "Column17", 80);
            SetFixed(grid, "Column18", 140);
            SetFixedByNameIfExists(grid, "colStockInCostPrice", 90, true);
            SetFixedByNameIfExists(grid, "colStockInMarkup", 80, true);
            SetFixedByNameIfExists(grid, "colStockInPrice", 80, true);
        }

        private static void SetFixed(DataGridView grid, string columnName, int width)
        {
            SetFixed(grid, columnName, width, false);
        }

        private static void SetFixed(DataGridView grid, string columnName, int width, bool alignRight)
        {
            DataGridViewColumn col = grid.Columns.Contains(columnName) ? grid.Columns[columnName] : null;
            if (col == null) return;

            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            col.Width = width;
            col.MinimumWidth = Math.Min(width, 30) == width ? width : width;

            if (alignRight)
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private static void SetFixedByNameIfExists(DataGridView grid, string columnName, int width, bool alignRight)
        {
            if (grid == null || !grid.Columns.Contains(columnName)) return;
            SetFixed(grid, columnName, width, alignRight);
        }

        private static void SetFill(DataGridView grid, string columnName, int minWidth, float fillWeight)
        {
            DataGridViewColumn col = grid.Columns.Contains(columnName) ? grid.Columns[columnName] : null;
            if (col == null) return;

            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            col.MinimumWidth = minWidth;
            col.FillWeight = fillWeight;
        }

        private static void SetButton(DataGridView grid, string columnName)
        {
            DataGridViewColumn col = grid.Columns.Contains(columnName) ? grid.Columns[columnName] : null;
            if (col == null) return;

            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            col.Width = 36;
            col.MinimumWidth = 36;
        }

        private static void DockPanelIfExists(Control root, string name, DockStyle dock)
        {
            Control panel = Find<Control>(root, name);
            if (panel != null)
                panel.Dock = dock;
        }

        private static void AnchorIfExists(Control root, string name, AnchorStyles anchor)
        {
            Control ctl = Find<Control>(root, name);
            if (ctl != null)
                ctl.Anchor = anchor;
        }

        private static void StretchToRight(Control root, string name, int rightMargin)
        {
            Control ctl = Find<Control>(root, name);
            if (ctl == null) return;
            ctl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ctl.Width = Math.Max(220, root.ClientSize.Width - ctl.Left - rightMargin);
        }
    }
}
