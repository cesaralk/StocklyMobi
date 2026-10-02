using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace StocklyMobi
{
    public sealed partial class InventoryInvoiceForm : Form
    {
        private readonly DBConnect _db = new DBConnect();
        private readonly string _username;
        private DataTable _invoiceLines;
        private bool _editingLine;
        private bool _searchReloading;
        private bool _layoutBusy;
        private bool _lastSideBySide;
        private Timer _searchTimer;
        private int _lastPostedInvoiceId;
        private bool _searchSubmitting;

        private const int EmSetCueBanner = 0x1501;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);

        public InventoryInvoiceForm()
            : this("admin")
        {
        }

        public InventoryInvoiceForm(string username)
        {
            _username = string.IsNullOrWhiteSpace(username) ? "admin" : username;
            BuildInvoiceTable();
            InitializeComponent();
            _invoiceGrid.DataSource = _invoiceLines;

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            _searchTimer = new Timer { Interval = 280 };
            _searchTimer.Tick += SearchTimer_Tick;
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            _searchTimer.Stop();
            LoadProducts(false);
        }

        private void InventoryInvoiceForm_Load(object sender, EventArgs e)
        {
            LoadSuppliers();
            LoadProducts(false);
            StartNewInvoice();
            BeginInvoke((MethodInvoker)AdjustResponsiveLayout);
        }

        private void InventoryInvoiceForm_Shown(object sender, EventArgs e)
        {
            AdjustResponsiveLayout();
            if (_search != null) _search.Focus();
        }

        private void InventoryInvoiceForm_Resize(object sender, EventArgs e)
        {
            AdjustResponsiveLayout();
        }

        private void InventoryInvoiceForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_searchTimer != null) _searchTimer.Dispose();
        }

        private void Search_HandleCreated(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                ApplySearchCueText();
        }

        private void BtnNewInvoice_Click(object sender, EventArgs e)
        {
            ConfirmStartNewInvoice();
        }

        private void BtnInvoiceHistory_Click(object sender, EventArgs e)
        {
            using (InvoiceHistoryForm form = new InvoiceHistoryForm())
                form.ShowDialog(this);
        }

        private void BtnClearSearch_Click(object sender, EventArgs e)
        {
            _searchReloading = true;
            _search.Clear();
            _searchReloading = false;
            if (_searchTimer != null) _searchTimer.Stop();
            LoadProducts(false);
            _search.Focus();
        }

        private void BtnAddSelected_Click(object sender, EventArgs e)
        {
            if (AddSelectedProduct())
                ResetUnifiedEntry(true);
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            if (_searchTimer != null) _searchTimer.Stop();
            LoadProducts(false);
        }

        private void BtnPrintBarcode_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                if (_productsGrid.CurrentRow == null)
                {
                    throw new InvalidOperationException(
                        "Select a product first.");
                }

                string barcode =
                    Convert.ToString(
                        _productsGrid.CurrentRow
                            .Cells["Barcode"].Value);

                string description =
                    Convert.ToString(
                        _productsGrid.CurrentRow
                            .Cells["Description"].Value);

                decimal price =
                    ToDecimal(
                        _productsGrid.CurrentRow
                            .Cells["Price"].Value);

                int quantity =
                    Convert.ToInt32(
                        _barcodeLabelCount.Value);

                if (string.IsNullOrWhiteSpace(barcode))
                {
                    throw new InvalidOperationException(
                        "The selected product does not have a barcode.");
                }

                BarcodeLabelHelper.ShowPrintPreview(
                    this,
                    barcode,
                    description,
                    price,
                    quantity);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Print Barcode",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void ProductsGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && AddSelectedProduct())
                ResetUnifiedEntry(true);
        }

        private void ProductsGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && _productsGrid.CurrentRow != null)
            {
                e.SuppressKeyPress = true;
                if (AddSelectedProduct())
                    ResetUnifiedEntry(true);
            }
        }

        private void InvoiceGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void InvoiceGrid_CurrentCellDirtyStateChanged(
            object sender,
            EventArgs e)
        {
            if (_invoiceGrid == null ||
                !_invoiceGrid.IsCurrentCellDirty ||
                !(_invoiceGrid.CurrentCell is DataGridViewCheckBoxCell))
            {
                return;
            }

            _invoiceGrid.CommitEdit(
                DataGridViewDataErrorContexts.Commit);
        }

        private void BuildInvoiceTable()
        {
            _invoiceLines = new DataTable();
            _invoiceLines.Columns.Add("pcode", typeof(string));
            _invoiceLines.Columns.Add("barcode", typeof(string));
            _invoiceLines.Columns.Add("description", typeof(string));

            DataColumn printBarcodeColumn =
                _invoiceLines.Columns.Add(
                    "printBarcode",
                    typeof(bool));

            printBarcodeColumn.AllowDBNull = false;
            printBarcodeColumn.DefaultValue = true;

            _invoiceLines.Columns.Add("qty", typeof(int));
            _invoiceLines.Columns.Add("cost", typeof(decimal));
            _invoiceLines.Columns.Add("markup", typeof(decimal));
            _invoiceLines.Columns.Add("price", typeof(decimal));
            _invoiceLines.Columns.Add("lineCost", typeof(decimal));
        }

        private void AdjustResponsiveLayout()
        {
            if (_layoutBusy || _mainSplit == null || _mainSplit.ClientSize.Width <= 0 || _mainSplit.ClientSize.Height <= 0) return;

            _layoutBusy = true;
            try
            {
                bool compactHeight = ClientSize.Height < 720;
                if (_pageHeader != null) _pageHeader.Height = compactHeight ? 58 : 72;
                if (_headerPanel != null) _headerPanel.Height = compactHeight ? 106 : 118;
                if (_productTools != null) _productTools.Height = compactHeight ? 148 : 154;
                if (_invoiceHelp != null) _invoiceHelp.Height = compactHeight ? 30 : 34;
                if (_invoiceFooter != null) _invoiceFooter.Height = compactHeight ? 84 : 92;

                int availableWidth = _mainSplit.ClientSize.Width;
                int availableHeight = _mainSplit.ClientSize.Height;

                // A 1366x768 laptop has a wide but short content area after the admin sidebar.
                // Side-by-side gives both grids the full available height instead of showing one row each.
                bool sideBySide = availableWidth >= 1000 && (availableHeight < 620 || availableWidth >= 1320);
                Orientation desired = sideBySide ? Orientation.Vertical : Orientation.Horizontal;

                if (_mainSplit.Orientation != desired)
                {
                    _mainSplit.Panel1MinSize = 0;
                    _mainSplit.Panel2MinSize = 0;
                    _mainSplit.Orientation = desired;
                }

                int total = sideBySide ? availableWidth : availableHeight;
                int preferred = (int)(total * (sideBySide ? 0.50 : 0.48));
                int minFirst = sideBySide ? 410 : 170;
                int minSecond = sideBySide ? 410 : 205;
                ResponsiveLayoutHelper.SetSafeSplitter(_mainSplit, preferred, minFirst, minSecond);

                _lastSideBySide = sideBySide;
                ConfigureProductGrid(sideBySide);
                ConfigureInvoiceGrid(sideBySide);
            }
            finally
            {
                _layoutBusy = false;
            }
        }

        private void InventoryInvoiceForm_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Control && (e.KeyCode == Keys.F || e.KeyCode == Keys.B)) ||
                e.KeyCode == Keys.F3)
            {
                e.SuppressKeyPress = true;
                FocusSearch();
            }
        }

        private void Search_TextChanged(object sender, EventArgs e)
        {
            if (_searchReloading || _searchSubmitting || _searchTimer == null) return;

            // Keep keyboard focus in the textbox and filter the product grid only.
            // No popup takes focus, so the user can type a full description.
            _searchTimer.Stop();
            _searchTimer.Start();
        }

        private void Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                FocusProductResults();
                return;
            }

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                e.SuppressKeyPress = true;
                SubmitUnifiedSearch();
                return;
            }

            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                BtnClearSearch_Click(sender, EventArgs.Empty);
            }
        }

        private void ApplySearchCueText()
        {
            if (_search != null && _search.IsHandleCreated)
            {
                SendMessage(
                    _search.Handle,
                    EmSetCueBanner,
                    new IntPtr(1),
                    "Type description, product code, or scan barcode...");
            }
        }

        private void FocusSearch()
        {
            if (_search == null) return;
            _search.Focus();
            _search.SelectAll();
        }


        private void FocusProductResults()
        {
            if (_productsGrid == null || _productsGrid.Rows.Count == 0) return;

            if (_productsGrid.CurrentRow == null)
            {
                _productsGrid.ClearSelection();
                _productsGrid.Rows[0].Selected = true;
                _productsGrid.CurrentCell = _productsGrid.Rows[0].Cells["Description"];
            }

            _productsGrid.Focus();
        }

        private void SubmitUnifiedSearch()
        {
            if (_searchSubmitting || _search == null) return;

            string raw = (_search.Text ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(raw))
            {
                FocusSearch();
                return;
            }

            _searchSubmitting = true;
            try
            {
                if (_searchTimer != null) _searchTimer.Stop();

                ProductSelection exactProduct;
                if (TryFindProductByExactIdentifier(raw, out exactProduct))
                {
                    if (PromptAndAddProduct(exactProduct))
                        ResetUnifiedEntry(true);
                    else
                        FocusSearch();
                    return;
                }

                // Normal keyboard search: keep the complete phrase, filter the grid,
                // and never interrupt typing with a popup after the first letter.
                LoadProducts(false);

                if (_productsGrid == null || _productsGrid.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No product matches \"" + raw + "\".",
                        "Product Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FocusSearch();
                    return;
                }

                if (_productsGrid.Rows.Count == 1)
                {
                    if (AddSelectedProduct())
                        ResetUnifiedEntry(true);
                    else
                        FocusSearch();
                    return;
                }

                // More than one match: keep the search text and let the user choose
                // from the filtered grid using arrows + Enter.
                FocusProductResults();
                if (_productCount != null)
                    _productCount.Text = _productsGrid.Rows.Count.ToString("N0") +
                        " matches • keep typing to narrow, or use arrows + Enter";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Product Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FocusSearch();
            }
            finally
            {
                _searchSubmitting = false;
            }
        }

        private void ResetUnifiedEntry(bool reloadProducts)
        {
            _searchReloading = true;
            try
            {
                if (_search != null) _search.Clear();
            }
            finally
            {
                _searchReloading = false;
            }

            if (reloadProducts) LoadProducts(false);
            if (_search != null) _search.Focus();
        }

        private bool TryFindProductByExactIdentifier(string identifier, out ProductSelection product)
        {
            product = null;
            if (string.IsNullOrWhiteSpace(identifier)) return false;

            using (SqlConnection con = new SqlConnection(_db.myConnection()))
            using (SqlCommand cmd = new SqlCommand(@"
SELECT TOP 1 p.pcode,p.barcode,p.pdesc,
       ISNULL(p.CostPrice,0) CostPrice,ISNULL(p.Markup,0) Markup,ISNULL(p.price,0) Price
FROM dbo.tbProduct p
WHERE p.isActive=1 AND (p.barcode=@identifier OR p.pcode=@identifier)
ORDER BY CASE WHEN p.barcode=@identifier THEN 0 ELSE 1 END", con))
            {
                cmd.Parameters.Add("@identifier", SqlDbType.VarChar, 100).Value = identifier.Trim();
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read()) return false;
                    product = new ProductSelection(
                        Convert.ToString(reader["pcode"]),
                        Convert.ToString(reader["barcode"]),
                        Convert.ToString(reader["pdesc"]),
                        Convert.ToDecimal(reader["CostPrice"]),
                        Convert.ToDecimal(reader["Markup"]),
                        Convert.ToDecimal(reader["Price"]));
                    return true;
                }
            }
        }

        private bool PromptAndAddProduct(ProductSelection product)
        {
            if (product == null) return false;
            int quantity = QuantityPromptForm.ShowQuantity(product.Description, 1);
            if (quantity <= 0) return false;

            AddProductLine(product.Code, product.Barcode, product.Description,
                product.Cost, product.Markup, product.Price, quantity);
            return true;
        }

        private sealed class ProductSelection
        {
            public string Code { get; private set; }
            public string Barcode { get; private set; }
            public string Description { get; private set; }
            public decimal Cost { get; private set; }
            public decimal Markup { get; private set; }
            public decimal Price { get; private set; }

            public ProductSelection(string code, string barcode, string description,
                decimal cost, decimal markup, decimal price)
            {
                Code = code;
                Barcode = barcode;
                Description = description;
                Cost = cost;
                Markup = markup;
                Price = price;
            }
        }

        private void LoadSuppliers()
        {
            object previousValue = _supplier == null ? null : _supplier.SelectedValue;
            string previousText = _supplier == null ? string.Empty : (_supplier.Text ?? string.Empty).Trim();

            using (SqlConnection con = new SqlConnection(_db.myConnection()))
            using (SqlDataAdapter da = new SqlDataAdapter(
                "SELECT id,supplier FROM dbo.tbSupplier ORDER BY supplier", con))
            {
                DataTable data = new DataTable();
                da.Fill(data);
                _supplier.DataSource = data;
                _supplier.DisplayMember = "supplier";
                _supplier.ValueMember = "id";

                if (previousValue != null && previousValue != DBNull.Value)
                    _supplier.SelectedValue = previousValue;
                else if (!string.IsNullOrWhiteSpace(previousText))
                    SelectSupplierByName(previousText);
            }
        }

        private bool SelectSupplierByName(string supplierName)
        {
            if (_supplier == null || string.IsNullOrWhiteSpace(supplierName)) return false;

            DataTable data = _supplier.DataSource as DataTable;
            if (data == null) return false;

            foreach (DataRow row in data.Rows)
            {
                if (string.Equals(Convert.ToString(row["supplier"]), supplierName.Trim(),
                    StringComparison.CurrentCultureIgnoreCase))
                {
                    _supplier.SelectedValue = row["id"];
                    return true;
                }
            }

            return false;
        }

        private void LoadProducts(bool showSuggestions = false)
        {
            try
            {
                string search = _search == null ? string.Empty : (_search.Text ?? string.Empty).Trim();
                string[] tokens = search.Split(
                    new[] { ' ' },
                    StringSplitOptions.RemoveEmptyEntries);

                string tokenFilter = string.Empty;
                for (int i = 0; i < tokens.Length; i++)
                {
                    tokenFilter += @"
  AND (p.pcode LIKE @token" + i +
                        " OR p.barcode LIKE @token" + i +
                        " OR p.pdesc LIKE @token" + i +
                        " OR b.brand LIKE @token" + i +
                        " OR c.category LIKE @token" + i + ")";
                }

                string sql = @"
SELECT p.pcode AS [Code],p.barcode AS [Barcode],p.pdesc AS [Description],
       b.brand AS [Brand],c.category AS [Category],p.qty AS [Stock],
       ISNULL(p.CostPrice,0) AS [Cost],ISNULL(p.Markup,0) AS [Markup],
       p.price AS [Price],p.reorder AS [Reorder]
FROM dbo.tbProduct p
INNER JOIN dbo.tbBrand b ON b.id=p.bid
INNER JOIN dbo.tbCategory c ON c.id=p.cid
WHERE p.isActive=1" + tokenFilter + @"
ORDER BY CASE
           WHEN @search='' THEN 3
           WHEN p.pcode=@search OR p.barcode=@search THEN 0
           WHEN p.pcode LIKE @prefix OR p.barcode LIKE @prefix OR
                p.pdesc LIKE @prefix OR b.brand LIKE @prefix OR c.category LIKE @prefix THEN 1
           ELSE 2
         END,
         p.pdesc";

                using (SqlConnection con = new SqlConnection(_db.myConnection()))
                using (SqlCommand cmd = new SqlCommand(sql, con))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.Add("@search", SqlDbType.VarChar, 150).Value = search;
                    cmd.Parameters.Add("@prefix", SqlDbType.VarChar, 170).Value = search + "%";

                    for (int i = 0; i < tokens.Length; i++)
                    {
                        cmd.Parameters.Add("@token" + i, SqlDbType.VarChar, 170)
                            .Value = "%" + tokens[i] + "%";
                    }

                    DataTable data = new DataTable();
                    da.Fill(data);
                    _productsGrid.DataSource = data;
                    ConfigureProductGrid(_lastSideBySide);
                    FormatGridMoney(_productsGrid, "Cost", "Markup", "Price");

                    if (_productCount != null)
                    {
                        string resultText = data.Rows.Count == 1
                            ? "1 product"
                            : data.Rows.Count.ToString("N0") + " products";

                        _productCount.Text = resultText +
                            (string.IsNullOrWhiteSpace(search)
                                ? " • type full words or scan a barcode • quantity is requested after selection"
                                : " • keep typing any words • Enter adds when one result remains");
                    }

                    if (data.Rows.Count > 0)
                    {
                        _productsGrid.ClearSelection();
                        _productsGrid.Rows[0].Selected = true;
                        _productsGrid.CurrentCell = _productsGrid.Rows[0].Cells["Description"];
                    }

                    // The filtered grid itself is the suggestion list. It never
                    // steals focus, so the user can type complete descriptions.
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Products", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool AddSelectedProduct()
        {
            try
            {
                if (_productsGrid.CurrentRow == null)
                    throw new InvalidOperationException("Select a product first.");

                DataRowView row = _productsGrid.CurrentRow.DataBoundItem as DataRowView;
                if (row == null)
                    throw new InvalidOperationException("Select a product first.");

                ProductSelection product = new ProductSelection(
                    Convert.ToString(row["Code"]),
                    Convert.ToString(row["Barcode"]),
                    Convert.ToString(row["Description"]),
                    Convert.ToDecimal(row["Cost"]),
                    Convert.ToDecimal(row["Markup"]),
                    Convert.ToDecimal(row["Price"]));

                return PromptAndAddProduct(product);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Stock-In Invoice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private void AddProductLine(string pcode, string barcode, string description, decimal cost, decimal markup, decimal price, int quantity)
        {
            if (quantity < 0) return;
            DataRow[] existing = _invoiceLines.Select("pcode='" + pcode.Replace("'", "''") + "'");
            if (existing.Length > 0)
            {
                existing[0]["qty"] = Convert.ToInt32(existing[0]["qty"]) + quantity;
                RecalculateRow(existing[0], true);
            }
            else
            {
                DataRow line = _invoiceLines.NewRow();
                line["pcode"] = pcode;
                line["barcode"] = barcode;
                line["description"] = description;
                line["printBarcode"] =
                    !string.IsNullOrWhiteSpace(barcode);
                line["qty"] = quantity;
                line["cost"] = cost;
                line["markup"] = markup;
                line["price"] = price;
                line["lineCost"] = cost * quantity;
                _invoiceLines.Rows.Add(line);
            }
            ConfigureInvoiceGrid();
            UpdateInvoiceTotal();
        }

        private void ConfigureProductGrid(bool sideBySide)
        {
            if (_productsGrid == null || _productsGrid.Columns.Count == 0) return;

            _productsGrid.AutoSizeColumnsMode = sideBySide
                ? DataGridViewAutoSizeColumnsMode.None
                : DataGridViewAutoSizeColumnsMode.Fill;

            ConfigureProductColumn("Code", 82, 70, 78, 0);
            ConfigureProductColumn("Barcode", 108, 85, 95, 1);
            ConfigureProductColumn("Description", sideBySide ? 205 : 260, 150, 250, 2);
            ConfigureProductColumn("Stock", 64, 55, 62, 3);
            ConfigureProductColumn("Cost", 78, 65, 74, 4);
            ConfigureProductColumn("Price", 78, 65, 74, 5);
            ConfigureProductColumn("Brand", 92, 75, 88, 6);
            ConfigureProductColumn("Category", 92, 75, 88, 7);
            ConfigureProductColumn("Markup", 78, 65, 72, 8);
            ConfigureProductColumn("Reorder", 72, 60, 68, 9);

            if (_productsGrid.Columns.Contains("Description"))
                _productsGrid.Columns["Description"].DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            if (_productsGrid.Columns.Contains("Stock"))
            {
                _productsGrid.Columns["Stock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                _productsGrid.Columns["Stock"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void ConfigureProductColumn(string name, int width, int minimumWidth, float fillWeight, int displayIndex)
        {
            if (!_productsGrid.Columns.Contains(name)) return;
            DataGridViewColumn column = _productsGrid.Columns[name];
            column.MinimumWidth = minimumWidth;
            column.Width = width;
            column.FillWeight = fillWeight;
            column.DisplayIndex = Math.Min(displayIndex, _productsGrid.Columns.Count - 1);
            column.Visible = true;
        }

        private void ConfigureInvoiceGrid()
        {
            ConfigureInvoiceGrid(_lastSideBySide);
        }

        private void ConfigureInvoiceGrid(bool sideBySide)
        {
            if (_invoiceGrid == null || _invoiceGrid.Columns.Count == 0) return;

            _invoiceGrid.AutoSizeColumnsMode = sideBySide
                ? DataGridViewAutoSizeColumnsMode.None
                : DataGridViewAutoSizeColumnsMode.Fill;

            SetReadOnly("pcode", true, "Code");
            SetReadOnly("barcode", true, "Barcode");
            SetReadOnly("description", true, "Description");
            SetReadOnly("printBarcode", false, "Print Barcode");
            SetReadOnly("qty", false, "QTY");
            SetReadOnly("cost", false, "Cost");
            SetReadOnly("markup", false, "Markup %");
            SetReadOnly("price", false, "Selling Price");
            SetReadOnly("lineCost", true, "Line Cost");

            ConfigureInvoiceColumn("pcode", 82, 70, 75, 0);
            ConfigureInvoiceColumn("barcode", 105, 85, 90, 1);
            ConfigureInvoiceColumn("description", sideBySide ? 185 : 220, 140, 200, 2);
            ConfigureInvoiceColumn("printBarcode", 86, 78, 82, 3);
            ConfigureInvoiceColumn("qty", 66, 62, 62, 4);
            ConfigureInvoiceColumn("cost", 78, 68, 74, 5);
            ConfigureInvoiceColumn("markup", 82, 70, 74, 6);
            ConfigureInvoiceColumn("price", 92, 76, 82, 7);
            ConfigureInvoiceColumn("lineCost", 92, 76, 82, 8);

            DataGridViewColumn printColumn =
                FindInvoiceColumn("printBarcode");

            if (printColumn != null)
            {
                printColumn.DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
                printColumn.HeaderCell.Style.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
                printColumn.DefaultCellStyle.BackColor =
                    Color.FromArgb(240, 253, 244);

                DataGridViewCheckBoxColumn checkColumn =
                    printColumn as DataGridViewCheckBoxColumn;

                if (checkColumn != null)
                {
                    checkColumn.ThreeState = false;
                    checkColumn.TrueValue = true;
                    checkColumn.FalseValue = false;
                    checkColumn.IndeterminateValue = false;
                }
            }

            DataGridViewColumn qtyColumn = FindInvoiceColumn("qty");
            qtyColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            qtyColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            qtyColumn.DefaultCellStyle.BackColor = Color.FromArgb(255, 248, 220);
            qtyColumn.DefaultCellStyle.Font = new Font("Century Gothic", 9F, FontStyle.Bold);

            FormatGridMoney(_invoiceGrid, "cost", "markup", "price", "lineCost");
        }

        private DataGridViewColumn FindInvoiceColumn(string dataPropertyName)
        {
            foreach (DataGridViewColumn column in _invoiceGrid.Columns)
                if (string.Equals(column.DataPropertyName, dataPropertyName, StringComparison.OrdinalIgnoreCase)) return column;
            return null;
        }

        private void ConfigureInvoiceColumn(string name, int width, int minimumWidth, float fillWeight, int displayIndex)
        {
            DataGridViewColumn column = FindInvoiceColumn(name);
            if (column == null) return;
            column.MinimumWidth = minimumWidth; column.Width = width; column.FillWeight = fillWeight;
            column.DisplayIndex = Math.Min(displayIndex, _invoiceGrid.Columns.Count - 1); column.Visible = true;
        }

        private void SetReadOnly(string columnName, bool readOnly, string header)
        {
            DataGridViewColumn column = FindInvoiceColumn(columnName);
            if (column == null) return;
            column.ReadOnly = readOnly; column.HeaderText = header;
        }

        private void FormatGridMoney(DataGridView grid, params string[] columns)
        {
            foreach (string name in columns)
            {
                DataGridViewColumn column = grid == _invoiceGrid ? FindInvoiceColumn(name) : (grid.Columns.Contains(name) ? grid.Columns[name] : null);
                if (column != null) column.DefaultCellStyle.Format = "N2";
            }
        }

        private void InvoiceGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (_editingLine || e.RowIndex < 0) return;
            DataRowView view = _invoiceGrid.Rows[e.RowIndex].DataBoundItem as DataRowView;
            if (view == null) return;
            try
            {
                _editingLine = true;
                string column =
                    _invoiceGrid.Columns[e.ColumnIndex]
                        .DataPropertyName;

                if (string.Equals(
                    column,
                    "printBarcode",
                    StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

                RecalculateRow(
                    view.Row,
                    column != "price");

                UpdateInvoiceTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Invoice Line", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { _editingLine = false; }
        }

        private static void RecalculateRow(DataRow row, bool calculatePriceFromMarkup)
        {
            int qty = Math.Max(0, ToInt(row["qty"]));
            decimal cost = Math.Max(0m, ToDecimal(row["cost"]));
            decimal markup = Math.Max(0m, ToDecimal(row["markup"]));
            decimal price = Math.Max(0m, ToDecimal(row["price"]));

            if (calculatePriceFromMarkup)
                price = Math.Round(cost * (1m + markup / 100m), 2, MidpointRounding.AwayFromZero);
            else if (cost > 0m)
                markup = Math.Round(((price - cost) / cost) * 100m, 2, MidpointRounding.AwayFromZero);

            row["qty"] = qty;
            row["cost"] = cost;
            row["markup"] = markup;
            row["price"] = price;
            row["lineCost"] = cost * qty;
        }

        private static decimal ToDecimal(object value)
        {
            decimal result;
            if (decimal.TryParse(Convert.ToString(value), NumberStyles.Any, CultureInfo.CurrentCulture, out result)) return result;
            if (decimal.TryParse(Convert.ToString(value), NumberStyles.Any, CultureInfo.InvariantCulture, out result)) return result;
            return 0m;
        }

        private static int ToInt(object value)
        {
            int result;
            return int.TryParse(Convert.ToString(value), out result) ? result : 0;
        }

        private decimal GetInvoiceCost()
        {
            decimal total = 0m;
            foreach (DataRow row in _invoiceLines.Rows)
            {
                RecalculateRow(row, false);
                total += ToDecimal(row["lineCost"]);
            }
            return total;
        }

        private void UpdateInvoiceTotal()
        {
            decimal total = 0m;
            int totalQuantity = 0;
            foreach (DataRow row in _invoiceLines.Rows)
            {
                total += ToDecimal(row["lineCost"]);
                totalQuantity += Math.Max(0, ToInt(row["qty"]));
            }
            _invoiceTotal.Text = "Lines: " + _invoiceLines.Rows.Count + "   Total QTY: " + totalQuantity + "   Invoice cost: $ " + total.ToString("N2");
        }

        private void NewProduct_Click(object sender, EventArgs e)
        {
            using (NewProductQuickForm form = new NewProductQuickForm())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    LoadProducts();
                    SelectProductRow(form.CreatedProductCode);
                    if (AddSelectedProduct())
                        ResetUnifiedEntry(true);
                }
            }
        }

        private void BtnEditProduct_Click(object sender, EventArgs e)
        {
            string pcode = GetSelectedProductCode();
            if (string.IsNullOrWhiteSpace(pcode))
            {
                MessageBox.Show("Select a product to edit.", "Edit Product",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (NewProductQuickForm form = new NewProductQuickForm(pcode))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    LoadProducts();
                    SelectProductRow(pcode);
                    RefreshPendingInvoiceProduct(pcode);
                }
            }
        }

        private void BtnDeleteProduct_Click(object sender, EventArgs e)
        {
            string pcode = GetSelectedProductCode();
            if (string.IsNullOrWhiteSpace(pcode))
            {
                MessageBox.Show("Select a product to delete.", "Delete Product",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataRow[] pendingLines = _invoiceLines.Select("pcode='" + pcode.Replace("'", "''") + "'");
            if (pendingLines.Length > 0)
            {
                MessageBox.Show("Remove this product from the current invoice before deleting it.",
                    "Delete Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string description = Convert.ToString(_productsGrid.CurrentRow.Cells["Description"].Value);
            int stock = Convert.ToInt32(_productsGrid.CurrentRow.Cells["Stock"].Value);
            if (MessageBox.Show(
                "Delete product " + description + "?\r\n\r\n" +
                "Current stock: " + stock + ". The product will disappear from inventory, barcode search, and the cashier. " +
                "Past sales, receipts, adjustments, and stock-in invoices will remain available.",
                "Delete Product", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                using (SqlConnection con = new SqlConnection(_db.myConnection()))
                {
                    con.Open();
                    using (SqlTransaction tr = con.BeginTransaction(IsolationLevel.Serializable))
                    {
                        try
                        {
                            int pendingReferences;
                            using (SqlCommand pendingCmd = new SqlCommand(@"
SELECT
    (SELECT COUNT(*) FROM dbo.tbCart WHERE pcode=@pcode AND status='Pending') +
    (SELECT COUNT(*) FROM dbo.tbStockIn WHERE pcode=@pcode AND status='Pending')", con, tr))
                            {
                                pendingCmd.Parameters.Add("@pcode", SqlDbType.VarChar, 50).Value = pcode;
                                pendingReferences = Convert.ToInt32(pendingCmd.ExecuteScalar());
                            }

                            if (pendingReferences > 0)
                                throw new InvalidOperationException(
                                    "This product is currently used in a pending cashier transaction or stock-in entry. " +
                                    "Remove it from the pending transaction first, then delete it.");

                            using (SqlCommand deleteCmd = new SqlCommand(@"
UPDATE dbo.tbProduct
SET isActive=0,
    qty=0,
    reorder=0,
    barcode=LEFT('DELETED-' + CONVERT(VARCHAR(20),id) + '-' +
                 REPLACE(CONVERT(VARCHAR(36),NEWID()),'-',''),100)
WHERE pcode=@pcode AND isActive=1", con, tr))
                            {
                                deleteCmd.Parameters.Add("@pcode", SqlDbType.VarChar, 50).Value = pcode;
                                if (deleteCmd.ExecuteNonQuery() != 1)
                                    throw new InvalidOperationException("The product no longer exists or was already deleted.");
                            }

                            tr.Commit();
                        }
                        catch
                        {
                            tr.Rollback();
                            throw;
                        }
                    }
                }

                LoadProducts(false);
                MessageBox.Show("Product deleted. Previous transaction history was preserved.", "Delete Product",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Product",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string GetSelectedProductCode()
        {
            if (_productsGrid == null || _productsGrid.CurrentRow == null) return string.Empty;
            return Convert.ToString(_productsGrid.CurrentRow.Cells["Code"].Value);
        }

        private void SelectProductRow(string pcode)
        {
            if (string.IsNullOrWhiteSpace(pcode) || _productsGrid == null) return;
            foreach (DataGridViewRow gridRow in _productsGrid.Rows)
            {
                if (string.Equals(Convert.ToString(gridRow.Cells["Code"].Value), pcode,
                    StringComparison.OrdinalIgnoreCase))
                {
                    _productsGrid.ClearSelection();
                    gridRow.Selected = true;
                    _productsGrid.CurrentCell = gridRow.Cells["Description"];
                    if (!gridRow.Displayed) _productsGrid.FirstDisplayedScrollingRowIndex = gridRow.Index;
                    return;
                }
            }
        }

        private void RefreshPendingInvoiceProduct(string pcode)
        {
            DataRow[] lines = _invoiceLines.Select("pcode='" + pcode.Replace("'", "''") + "'");
            if (lines.Length == 0) return;

            using (SqlConnection con = new SqlConnection(_db.myConnection()))
            using (SqlCommand cmd = new SqlCommand(@"
SELECT p.barcode,p.pdesc,ISNULL(p.CostPrice,0) CostPrice,
       ISNULL(p.Markup,0) Markup,ISNULL(p.price,0) Price
FROM dbo.tbProduct p WHERE p.pcode=@pcode", con))
            {
                cmd.Parameters.Add("@pcode", SqlDbType.VarChar, 50).Value = pcode;
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read()) return;
                    foreach (DataRow line in lines)
                    {
                        line["barcode"] = Convert.ToString(reader["barcode"]);
                        line["description"] = Convert.ToString(reader["pdesc"]);
                        line["cost"] = Convert.ToDecimal(reader["CostPrice"]);
                        line["markup"] = Convert.ToDecimal(reader["Markup"]);
                        line["price"] = Convert.ToDecimal(reader["Price"]);
                        RecalculateRow(line, false);
                    }
                }
            }

            UpdateInvoiceTotal();
            _invoiceGrid.Refresh();
        }

        private void RemoveLine_Click(object sender, EventArgs e)
        {
            if (_invoiceGrid.CurrentRow == null) return;
            DataRowView view = _invoiceGrid.CurrentRow.DataBoundItem as DataRowView;
            if (view == null) return;
            view.Row.Delete();
            UpdateInvoiceTotal();
        }

        private void AddSupplier_Click(object sender, EventArgs e)
        {
            string name = TextEntryDialog.Show("Supplier name", "Add Supplier");
            if (string.IsNullOrWhiteSpace(name)) return;
            try
            {
                using (SqlConnection con = new SqlConnection(_db.myConnection()))
                using (SqlCommand cmd = new SqlCommand(@"
IF NOT EXISTS(SELECT 1 FROM dbo.tbSupplier WHERE supplier=@name)
    INSERT INTO dbo.tbSupplier(supplier,address,contactperson,phone,email,fax) VALUES(@name,'','','','','')", con))
                {
                    cmd.Parameters.Add("@name", SqlDbType.VarChar, 100).Value = name.Trim();
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadSuppliers();
                SelectSupplierByName(name.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Supplier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CommitInvoiceGridEdits()
        {
            if (_invoiceGrid == null)
                return;

            if (_invoiceGrid.IsCurrentCellDirty)
            {
                _invoiceGrid.CommitEdit(
                    DataGridViewDataErrorContexts.Commit);
            }

            _invoiceGrid.EndEdit();

            CurrencyManager manager =
                BindingContext[_invoiceGrid.DataSource]
                as CurrencyManager;

            if (manager != null)
                manager.EndCurrentEdit();
        }

        private static bool ShouldPrintInvoiceBarcode(
            DataRow row)
        {
            if (row == null ||
                !row.Table.Columns.Contains("printBarcode") ||
                row["printBarcode"] == DBNull.Value)
            {
                return true;
            }

            return Convert.ToBoolean(row["printBarcode"]);
        }

        private List<BarcodeLabelItem>
            BuildInvoiceBarcodeItems(
                bool oneEach,
                out int missingBarcodeLines,
                out int excludedLines,
                out long totalLabelCount)
        {
            CommitInvoiceGridEdits();

            List<BarcodeLabelItem> items =
                new List<BarcodeLabelItem>();

            missingBarcodeLines = 0;
            excludedLines = 0;
            totalLabelCount = 0L;

            foreach (DataRow row in _invoiceLines.Rows)
            {
                if (!ShouldPrintInvoiceBarcode(row))
                {
                    excludedLines++;
                    continue;
                }

                int invoiceQuantity =
                    Math.Max(0, ToInt(row["qty"]));

                int labelQuantity =
                    oneEach ? 1 : invoiceQuantity;

                if (labelQuantity <= 0)
                    continue;

                string barcode =
                    Convert.ToString(row["barcode"]);

                if (string.IsNullOrWhiteSpace(barcode))
                {
                    missingBarcodeLines++;
                    continue;
                }

                items.Add(
                    new BarcodeLabelItem(
                        barcode,
                        Convert.ToString(row["description"]),
                        ToDecimal(row["price"]),
                        labelQuantity));

                totalLabelCount += labelQuantity;
            }

            return items;
        }

        private void BtnPrintOneEach_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                if (_invoiceLines.Rows.Count == 0)
                {
                    throw new InvalidOperationException(
                        "Add products to the invoice first.");
                }

                int missingBarcodeLines;
                int excludedLines;
                long totalLabelCount;

                List<BarcodeLabelItem> items =
                    BuildInvoiceBarcodeItems(
                        true,
                        out missingBarcodeLines,
                        out excludedLines,
                        out totalLabelCount);

                if (items.Count == 0)
                {
                    throw new InvalidOperationException(
                        "No invoice products are selected for barcode printing.\r\n\r\n" +
                        "Check the Print Barcode boxes for the products you want.");
                }

                if (missingBarcodeLines > 0)
                {
                    DialogResult choice =
                        MessageBox.Show(
                            missingBarcodeLines.ToString("N0") +
                            " selected invoice line(s) do not have a barcode and will be skipped.\r\n\r\n" +
                            "Continue with one label for each remaining product?",
                            "Print 1 Each",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);

                    if (choice != DialogResult.Yes)
                        return;
                }

                BarcodeLabelHelper.ShowBatchPrintPreview(
                    this,
                    items);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Print 1 Each",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void PostInvoice_Click(object sender, EventArgs e)
        {
            List<BarcodeLabelItem> invoiceBarcodeItems = null;
            bool printInvoiceBarcodes =
                _chkPrintInvoiceBarcodes.Checked;
            long invoiceLabelCount = 0L;

            try
            {
                if (_invoiceLines.Rows.Count == 0)
                {
                    throw new InvalidOperationException(
                        "Add at least one product to the invoice.");
                }

                if (_supplier.SelectedValue == null &&
                    !SelectSupplierByName(_supplier.Text))
                {
                    throw new InvalidOperationException(
                        "Select an existing supplier from the suggestions, or use + Supplier to create it.");
                }

                if (string.IsNullOrWhiteSpace(
                    _invoiceNo.Text))
                {
                    throw new InvalidOperationException(
                        "Enter the supplier invoice number printed on the supplier invoice.");
                }

                if (printInvoiceBarcodes)
                {
                    int missingBarcodeLines;
                    int excludedLines;

                    invoiceBarcodeItems =
                        BuildInvoiceBarcodeItems(
                            false,
                            out missingBarcodeLines,
                            out excludedLines,
                            out invoiceLabelCount);

                    if (invoiceBarcodeItems.Count == 0)
                    {
                        throw new InvalidOperationException(
                            "Barcode printing is selected, but no invoice lines are checked with a printable barcode.\r\n\r\n" +
                            "Check the Print Barcode boxes for the products you want.");
                    }

                    if (missingBarcodeLines > 0)
                    {
                        DialogResult missingChoice =
                            MessageBox.Show(
                                missingBarcodeLines.ToString("N0") +
                                " invoice line(s) do not have a barcode and will be skipped.\r\n\r\n" +
                                "Continue posting the invoice?",
                                "Invoice Barcode Labels",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning);

                        if (missingChoice !=
                            DialogResult.Yes)
                        {
                            return;
                        }
                    }
                }

                string enteredInvoiceNo =
                    _invoiceNo.Text.Trim();

                string confirmation =
                    "Post invoice " +
                    enteredInvoiceNo +
                    " and update stock now?";

                if (printInvoiceBarcodes)
                {
                    confirmation +=
                        "\r\n\r\nA barcode preview will open for " +
                        invoiceLabelCount.ToString("N0") +
                        " label(s), using each invoice quantity.";
                }

                if (MessageBox.Show(
                    confirmation,
                    "Stock-In Invoice",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) !=
                    DialogResult.Yes)
                {
                    return;
                }

                _lastPostedInvoiceId = 0;

                using (SqlConnection con =
                    new SqlConnection(
                        _db.myConnection()))
                {
                    con.Open();

                    using (SqlTransaction tr =
                        con.BeginTransaction(
                            IsolationLevel.Serializable))
                    {
                        try
                        {
                            using (SqlCommand check =
                                new SqlCommand(@"
SELECT CASE
    WHEN EXISTS(
        SELECT 1
        FROM dbo.tbStockInvoice
        WHERE invoiceNo=@refno)
      OR EXISTS(
        SELECT 1
        FROM dbo.tbStockIn
        WHERE refno=@refno
          AND status='Done')
    THEN 1
    ELSE 0
END", con, tr))
                            {
                                check.Parameters.Add(
                                    "@refno",
                                    SqlDbType.VarChar,
                                    50).Value =
                                    enteredInvoiceNo;

                                if (Convert.ToInt32(
                                    check.ExecuteScalar()) > 0)
                                {
                                    throw new InvalidOperationException(
                                        "This invoice number was already posted.");
                                }
                            }

                            decimal invoiceCost =
                                GetInvoiceCost();

                            using (SqlCommand headerInsert =
                                new SqlCommand(@"
INSERT INTO dbo.tbStockInvoice
(
    invoiceNo,
    supplierId,
    invoiceDate,
    createdAt,
    createdBy,
    totalCost,
    status
)
VALUES
(
    @invoiceNo,
    @supplier,
    @invoiceDate,
    GETDATE(),
    @user,
    @total,
    'Posted'
);
SELECT CAST(SCOPE_IDENTITY() AS INT);",
                                    con,
                                    tr))
                            {
                                headerInsert.Parameters.Add(
                                    "@invoiceNo",
                                    SqlDbType.VarChar,
                                    50).Value =
                                    enteredInvoiceNo;

                                headerInsert.Parameters.Add(
                                    "@supplier",
                                    SqlDbType.Int).Value =
                                    Convert.ToInt32(
                                        _supplier.SelectedValue);

                                headerInsert.Parameters.Add(
                                    "@invoiceDate",
                                    SqlDbType.DateTime).Value =
                                    _invoiceDate.Value;

                                headerInsert.Parameters.Add(
                                    "@user",
                                    SqlDbType.VarChar,
                                    100).Value =
                                    _username;

                                AddMoney(
                                    headerInsert,
                                    "@total",
                                    invoiceCost);

                                _lastPostedInvoiceId =
                                    Convert.ToInt32(
                                        headerInsert.ExecuteScalar());
                            }

                            foreach (DataRow row in
                                _invoiceLines.Rows)
                            {
                                RecalculateRow(
                                    row,
                                    false);

                                int quantity =
                                    ToInt(row["qty"]);

                                decimal cost =
                                    ToDecimal(row["cost"]);

                                decimal markup =
                                    ToDecimal(row["markup"]);

                                decimal price =
                                    ToDecimal(row["price"]);

                                using (SqlCommand update =
                                    new SqlCommand(@"
UPDATE dbo.tbProduct
SET qty=qty+@qty,
    CostPrice=@cost,
    Markup=@markup,
    price=@price
WHERE pcode=@pcode",
                                        con,
                                        tr))
                                {
                                    update.Parameters.Add(
                                        "@qty",
                                        SqlDbType.Int).Value =
                                        quantity;

                                    AddMoney(
                                        update,
                                        "@cost",
                                        cost);

                                    AddMoney(
                                        update,
                                        "@markup",
                                        markup);

                                    AddMoney(
                                        update,
                                        "@price",
                                        price);

                                    update.Parameters.Add(
                                        "@pcode",
                                        SqlDbType.VarChar,
                                        50).Value =
                                        row["pcode"].ToString();

                                    if (update.ExecuteNonQuery() == 0)
                                    {
                                        throw new InvalidOperationException(
                                            "Product " +
                                            row["pcode"] +
                                            " no longer exists.");
                                    }
                                }

                                using (SqlCommand insert =
                                    new SqlCommand(@"
INSERT INTO dbo.tbStockIn
(
    refno,
    pcode,
    qty,
    sdate,
    stockinby,
    supplierid,
    status,
    CostPrice,
    Markup,
    Price,
    invoiceId
)
VALUES
(
    @refno,
    @pcode,
    @qty,
    @date,
    @user,
    @supplier,
    'Done',
    @cost,
    @markup,
    @price,
    @invoiceId
)",
                                        con,
                                        tr))
                                {
                                    insert.Parameters.Add(
                                        "@refno",
                                        SqlDbType.VarChar,
                                        50).Value =
                                        enteredInvoiceNo;

                                    insert.Parameters.Add(
                                        "@invoiceId",
                                        SqlDbType.Int).Value =
                                        _lastPostedInvoiceId;

                                    insert.Parameters.Add(
                                        "@pcode",
                                        SqlDbType.VarChar,
                                        50).Value =
                                        row["pcode"].ToString();

                                    insert.Parameters.Add(
                                        "@qty",
                                        SqlDbType.Int).Value =
                                        quantity;

                                    insert.Parameters.Add(
                                        "@date",
                                        SqlDbType.DateTime).Value =
                                        _invoiceDate.Value;

                                    insert.Parameters.Add(
                                        "@user",
                                        SqlDbType.VarChar,
                                        100).Value =
                                        _username;

                                    insert.Parameters.Add(
                                        "@supplier",
                                        SqlDbType.Int).Value =
                                        Convert.ToInt32(
                                            _supplier.SelectedValue);

                                    AddMoney(
                                        insert,
                                        "@cost",
                                        cost);

                                    AddMoney(
                                        insert,
                                        "@markup",
                                        markup);

                                    AddMoney(
                                        insert,
                                        "@price",
                                        price);

                                    insert.ExecuteNonQuery();
                                }
                            }

                            tr.Commit();
                        }
                        catch
                        {
                            tr.Rollback();
                            throw;
                        }
                    }
                }

                DialogResult invoicePreviewChoice =
                    MessageBox.Show(
                        "Stock-in invoice posted successfully.\r\n\r\n" +
                        "Open the A4 invoice print preview now?",
                        "Stock-In Invoice",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);

                int postedInvoiceId =
                    _lastPostedInvoiceId;

                LoadProducts();
                StartNewInvoice();

                if (invoicePreviewChoice ==
                        DialogResult.Yes &&
                    postedInvoiceId > 0)
                {
                    try
                    {
                        StockInvoicePrinter.ShowPreview(
                            this,
                            postedInvoiceId);
                    }
                    catch (Exception printException)
                    {
                        MessageBox.Show(
                            "The invoice was posted, but its print preview could not open.\r\n\r\n" +
                            printException.GetBaseException().Message,
                            "Invoice Print Preview",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }

                if (printInvoiceBarcodes &&
                    invoiceBarcodeItems != null)
                {
                    try
                    {
                        BarcodeLabelHelper
                            .ShowBatchPrintPreview(
                                this,
                                invoiceBarcodeItems);
                    }
                    catch (Exception barcodeException)
                    {
                        MessageBox.Show(
                            "The invoice was posted, but the barcode preview could not open.\r\n\r\n" +
                            barcodeException.GetBaseException().Message,
                            "Invoice Barcode Labels",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Stock-In Invoice",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void ConfirmStartNewInvoice()
        {
            if (_invoiceLines.Rows.Count > 0 && MessageBox.Show("Discard the current unposted invoice?", "New Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            StartNewInvoice();
        }

        private void StartNewInvoice()
        {
            _invoiceLines.Clear();
            _invoiceNo.Clear();
            _invoiceDate.Value = DateTime.Now;
            _lastPostedInvoiceId = 0;
            if (_chkPrintInvoiceBarcodes != null)
                _chkPrintInvoiceBarcodes.Checked = false;
            _searchReloading = true;
            if (_search != null) _search.Clear();
            _searchReloading = false;
            UpdateInvoiceTotal();
            ConfigureInvoiceGrid();
        }

        private static void AddMoney(SqlCommand cmd, string name, decimal value)
        {
            SqlParameter p = cmd.Parameters.Add(name, SqlDbType.Decimal);
            p.Precision = 18;
            p.Scale = 4;
            p.Value = value;
        }
    }
}
