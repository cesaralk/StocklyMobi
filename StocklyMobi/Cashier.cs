using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StocklyMobi
{
    public partial class Cashier : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;

        int qty;
        string id;
        string price;

        string stitle = "StocklyMobi POS";
        private const string MASTER_BARCODE = "9999999999999";
        // The rate is managed from Admin > Setting > Exchange Rate.
        public double ExchangeRateLBP
        {
            get { return Convert.ToDouble(ExchangeRateConfiguration.CurrentRate); }
        }

        private double GetExchangeRateLBP()
        {
            return Convert.ToDouble(ExchangeRateConfiguration.CurrentRate);
        }
        private decimal ParseDecimalValue(object value)
        {
            decimal.TryParse(Convert.ToString(value), out decimal result);
            return result;
        }
        private void dgvCash_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCash.CurrentRow == null) return;
            if (dgvCash.CurrentRow.Index < 0) return;

            int i = dgvCash.CurrentRow.Index;

            if (dgvCash.Rows[i].Cells[1].Value != null)
                id = dgvCash.Rows[i].Cells[1].Value.ToString();
            else
                id = "";

            if (dgvCash.Rows[i].Cells[7].Value != null)
                price = dgvCash.Rows[i].Cells[7].Value.ToString();
            else
                price = "0";
        }
        public decimal CurrentSaleUSD = 0m;
        public decimal CurrentCartSubtotalUSD { get; private set; }
        public decimal CurrentTransactionDiscountUSD { get; private set; }
        private decimal CurrentDiscountUSD = 0m;

        public Cashier()
        {
            InitializeComponent();
            ResponsiveLayoutHelper.Apply(this);
            InitializeMobileShopActions();
            btnDiscount.Text = "Transaction Discount";
            cn = new SqlConnection(dbcon.myConnection());
            GetTranNo();
            lblDate.Text = DateTime.Now.ToShortDateString();
            txtBarcode.KeyDown += txtBarcode_KeyDown;


            // Some barcode scanners send CR (KeyPress '\r') or TAB instead of Enter.
            // Handle KeyPress too so "qty + scan" works reliably.
            txtBarcode.KeyPress += txtBarcode_KeyPress;

            // Qty helper: allow quick multiply (e.g., set Qty then scan)
            txtQty.KeyPress += TxtQty_KeyPress;
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Handle scanners that send carriage return (\r) as the suffix.
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                HandleBarcodeSubmit();
            }
        }

        private void HandleBarcodeSubmit()
        {
            var raw = txtBarcode.Text.Trim();
            if (string.IsNullOrWhiteSpace(raw))
            {
                txtBarcode.Clear();
                return;
            }

            // Support quick multiplication:
            //  - Type Qty in txtQty (e.g., 5) then scan barcode
            //  - Or type: 5*BARCODE  (or BARCODE*5) then press Enter
            if (TryParseQtyAndBarcode(raw, out int qtyToAdd, out string barcode))
            {
                ProcessBarcode(barcode, qtyToAdd);
            }
            else
            {
                ProcessBarcode(raw, GetScanQtyFallback());
            }

            txtBarcode.Clear();

            // Reset qty back to 1 for next scan (common POS behavior)
            txtQty.Text = "1";
            txtQty.SelectionStart = 0;
            txtQty.SelectionLength = txtQty.Text.Length;
        }

        private void TxtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Digits + control keys only
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            // Many scanners send Enter, some send Tab.
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                e.SuppressKeyPress = true; // prevents beep sound
                HandleBarcodeSubmit();
            }
        }

        private int GetScanQtyFallback()
        {
            if (int.TryParse(txtQty.Text.Trim(), out int q) && q > 0) return q;
            return 1;
        }

        private bool TryParseQtyAndBarcode(string input, out int qtyToAdd, out string barcode)
        {
            qtyToAdd = 1;
            barcode = input;

            if (string.IsNullOrWhiteSpace(input)) return false;
            var s = input.Replace(" ", "");

            // Accept both '*' and 'x' as multiplier separators
            char sep = s.Contains('*') ? '*' : (s.IndexOf('x') >= 0 ? 'x' : (s.IndexOf('X') >= 0 ? 'X' : '\0'));
            if (sep == '\0') return false;

            var parts = s.Split(new[] { sep }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2) return false;

            // Pattern: qty*barcode
            if (int.TryParse(parts[0], out int q1) && q1 > 0)
            {
                qtyToAdd = q1;
                barcode = parts[1];
                return !string.IsNullOrWhiteSpace(barcode);
            }

            // Pattern: barcode*qty
            if (int.TryParse(parts[1], out int q2) && q2 > 0)
            {
                qtyToAdd = q2;
                barcode = parts[0];
                return !string.IsNullOrWhiteSpace(barcode);
            }

            return false;
        }

        private decimal PromptForOpenPrice()
        {
            using (Form prompt = new Form())
            {
                prompt.Width = 300;
                prompt.Height = 170;
                prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
                prompt.Text = "Open Price";
                prompt.StartPosition = FormStartPosition.CenterScreen;
                prompt.MinimizeBox = false;
                prompt.MaximizeBox = false;

                Label textLabel = new Label()
                {
                    Left = 15,
                    Top = 20,
                    AutoSize = true,
                    Text = "Enter unit price:"
                };

                TextBox inputBox = new TextBox()
                {
                    Left = 15,
                    Top = 50,
                    Width = 250
                };

                Button confirmation = new Button()
                {
                    Text = "OK",
                    Left = 185,
                    Width = 80,
                    Top = 85,
                    DialogResult = DialogResult.OK
                };

                prompt.Controls.Add(textLabel);
                prompt.Controls.Add(inputBox);
                prompt.Controls.Add(confirmation);
                prompt.AcceptButton = confirmation;

                if (prompt.ShowDialog() == DialogResult.OK)
                {
                    string s = inputBox.Text.Trim().Replace(",", "");
                    if (decimal.TryParse(s, out decimal enteredPrice) && enteredPrice > 0)
                        return enteredPrice;
                }

                return 0m;
            }
        }
        //private void ProcessBarcode(string barcode, int qtyToAdd)
        //{
        //    if (string.IsNullOrEmpty(barcode)) return;
        //    if (qtyToAdd <= 0) qtyToAdd = 1;

        //    try
        //    {
        //        cn.Open();
        //        cm = new SqlCommand("SELECT * FROM tbProduct WHERE barcode=@barcode", cn);
        //        cm.Parameters.AddWithValue("@barcode", barcode);
        //        dr = cm.ExecuteReader();

        //        if (dr.Read())
        //        {
        //            string _pcode = dr["pcode"].ToString();
        //            double _price = double.Parse(dr["price"].ToString());

        //            qty = int.Parse(dr["qty"].ToString()); // ✅ SET STOCK ON HAND
        //            int _qty = qtyToAdd; // ✅ scanned qty (supports multiplication)

        //            dr.Close();
        //            cn.Close();

        //            AddToCart(_pcode, _price, _qty);
        //        }

        //        else
        //        {
        //            dr.Close();
        //            cn.Close();
        //            MessageBox.Show("Product not found!", "StocklyMobi POS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        cn.Close();
        //        MessageBox.Show(ex.Message, "StocklyMobi POS", MessageBoxButtons.OK, MessageBoxIcon.Warning);


        //    }
        //}

        private void ProcessBarcode(string barcode, int qtyToAdd)
        {
            if (string.IsNullOrEmpty(barcode)) return;
            if (qtyToAdd <= 0) qtyToAdd = 1;

            string _pcode = "";
            decimal _price = 0m;
            int availableQty = 0;
            bool found = false;
            bool isOpenPrice = barcode == MASTER_BARCODE;

            try
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 pcode, price, qty FROM tbProduct WHERE barcode=@barcode AND isActive=1", cn))
                {
                    cmd.Parameters.AddWithValue("@barcode", barcode);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            found = true;
                            _pcode = reader["pcode"].ToString();

                            if (!isOpenPrice)
                            {
                                _price = ParseDecimalValue(reader["price"]);
                                int.TryParse(reader["qty"].ToString(), out availableQty);
                            }
                        }
                    }
                }
                cn.Close();

                if (!found)
                {
                    MessageBox.Show("Product not found!", "StocklyMobi POS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (isOpenPrice)
                {
                    _price = PromptForOpenPrice();
                    if (_price <= 0)
                        return;

                    // bypass stock blocking for the special open-price item
                    qty = int.MaxValue;

                    // false = do not merge with an existing row automatically
                    AddToCart(_pcode, _price, qtyToAdd, false);
                }
                else
                {
                    qty = availableQty;
                    AddToCart(_pcode, _price, qtyToAdd, true);
                }
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();

                MessageBox.Show(ex.Message, "StocklyMobi POS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit Application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void InitializeMobileShopActions()
        {
            panel4.Height = 174;

            FlowLayoutPanel mobileActions = new FlowLayoutPanel();
            mobileActions.Name = "panelMobileActions";
            mobileActions.Dock = DockStyle.Bottom;
            mobileActions.Height = 124;
            mobileActions.Padding = new Padding(8, 7, 8, 5);
            mobileActions.WrapContents = true;
            mobileActions.AutoScroll = true;
            mobileActions.BackColor = Color.FromArgb(248, 250, 252);

            Button fixingButton = MobileUi.Button("Mobile Fixing", MobileUi.Blue);
            fixingButton.Width = 125;
            fixingButton.Click += delegate
            {
                using (MobileFixingForm form = new MobileFixingForm(lblUsername.Text))
                    form.ShowDialog(this);
                FocusBarcode();
            };

            mobileActions.Controls.Add(fixingButton);

            Button balancesButton = MobileUi.Button("Provider Balances", MobileUi.Amber);
            balancesButton.Width = 150;
            balancesButton.Click += delegate
            {
                using (CashierProviderBalancesForm form = new CashierProviderBalancesForm())
                    form.ShowDialog(this);
                FocusBarcode();
            };
            mobileActions.Controls.Add(balancesButton);

            mobileActions.Controls.Add(CreateServiceAction("Mobile Recharge", MobileShopDb.MobileRecharge, MobileUi.Sky));
            mobileActions.Controls.Add(CreateServiceAction("Transfer Balance", MobileShopDb.TransferBalance, MobileUi.Sky));
            mobileActions.Controls.Add(CreateServiceAction("Validity", MobileShopDb.ValidityRecharge, MobileUi.Amber));
            Button internetBundlesButton = CreateServiceAction("Internet Bundles", MobileShopDb.InternetBundles, MobileUi.Sky);
            internetBundlesButton.Width = 155;
            mobileActions.Controls.Add(internetBundlesButton);
            mobileActions.Controls.Add(CreateServiceAction("Apps", MobileShopDb.Apps, MobileUi.Sky));

            Button deliverButton = MobileUi.Button("Deliver Fixed Phone", MobileUi.Green);
            deliverButton.Width = 155;
            deliverButton.Click += delegate
            {
                using (DeliverFixForm form = new DeliverFixForm(this))
                    form.ShowDialog(this);
                FocusBarcode();
            };
            mobileActions.Controls.Add(deliverButton);

            panel4.Controls.Add(mobileActions);
            mobileActions.BringToFront();
        }

        private Button CreateServiceAction(string text, string serviceType, Color color)
        {
            Button button = MobileUi.Button(text, color);
            button.Width = 135;
            button.Tag = serviceType;
            button.Click += delegate
            {
                using (ServicePickerForm form = new ServicePickerForm(this, serviceType))
                    form.ShowDialog(this);
                FocusBarcode();
            };
            return button;
        }

        public void FocusBarcode()
        {
            if (txtBarcode == null) return;
            txtBarcode.Focus();
            txtBarcode.SelectAll();
        }

        public void slide(Button button)
        {
            panelSlide.BackColor = Color.White;
            panelSlide.Height = button.Height;
            panelSlide.Top = button.Top;
        }
        #region button
        private void btnNTran_Click(object sender, EventArgs e)
        {
            slide(btnNTran);
            GetTranNo();

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            slide(btnSearch);
            LookUpProduct lookUp = new LookUpProduct(this);
            lookUp.LoadProduct();
            lookUp.ShowDialog();
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            slide(btnDiscount);
            if (dgvCash.Rows.Count == 0 || CurrentCartSubtotalUSD <= 0m)
            {
                MessageBox.Show("Add at least one item before applying a transaction discount.", "Transaction Discount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (TransactionDiscountForm discount = new TransactionDiscountForm(this, lblTranNo.Text, lblUsername.Text))
                discount.ShowDialog(this);
            FocusBarcode();
        }

        private void btnSettle_Click(object sender, EventArgs e)
        {
            slide(btnSettle);
            if (dgvCash.Rows.Count == 0 || CurrentSaleUSD <= 0m)
            {
                MessageBox.Show("Add at least one item before settlement.", "Settlement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (Settle settle = new Settle(this))
                settle.ShowDialog();
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            slide(btnClear);
            if (MessageBox.Show("Remove all items from cart?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cn.Open();
                cm = new SqlCommand("DELETE FROM tbCart WHERE transno=@transno AND status='Pending'", cn);
                cm.Parameters.Add("@transno", SqlDbType.VarChar, 50).Value = lblTranNo.Text;
                cm.ExecuteNonQuery();
                cn.Close();
                MobileShopDb.ClearPendingTransactionDiscount(lblTranNo.Text);
                MessageBox.Show("All items and the pending transaction discount were removed.", "Remove item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCart();
            }
        }

        private void btnReceiptSearch_Click(object sender, EventArgs e)
        {
            slide(btnReceiptSearch);
            using (ReceiptHistoryForm receiptHistory = new ReceiptHistoryForm())
            {
                receiptHistory.ShowDialog(this);
            }
            FocusBarcode();
        }

        private void btnDSales_Click(object sender, EventArgs e)
        {
            slide(btnDSales);
            DailySale dailySale = new DailySale(new MainForm());
            dailySale.solduser = lblUsername.Text;
            dailySale.dtFrom.Enabled = false;
            dailySale.dtTo.Enabled = false;
            dailySale.cboCashier.Enabled = false;
            dailySale.cboCashier.Text = lblUsername.Text;
            dailySale.LoadSold();
            dailySale.picClose.Visible = true;
            dailySale.lblTitle.Visible = true;
            dailySale.ShowDialog();
        }

        private void btnPass_Click(object sender, EventArgs e)
        {
            slide(btnPass);
            ChangePassword change = new ChangePassword(this);
            change.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            slide(btnLogout);
            if (dgvCash.Rows.Count > 0)
            {
                MessageBox.Show("Unable to logout. Please cancel the transaction.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Logout Application?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                Login login = new Login();
                login.ShowDialog();
            }
        }
        #endregion button

        public void LoadCart()
        {
            try
            {
                Boolean hascart = false;
                int i = 0;
                decimal netTotal = 0m;
                decimal grossTotal = 0m;
                decimal discount = 0m;
                dgvCash.Rows.Clear();
                cn.Open();
                cm = new SqlCommand(@"
SELECT c.id, c.pcode,
       COALESCE(NULLIF(c.itemDescription,''), p.pdesc, c.pcode) AS pdesc,
       c.price, c.qty, c.disc, c.total
FROM tbCart AS c
LEFT JOIN tbProduct AS p ON c.pcode=p.pcode
WHERE c.transno = @transno AND c.status = 'Pending'
ORDER BY c.id", cn);
                cm.Parameters.AddWithValue("@transno", lblTranNo.Text);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {

                    i++;
                    decimal rowPrice = ParseDecimalValue(dr["price"]);
                    decimal rowDisc = ParseDecimalValue(dr["disc"]);
                    decimal rowTotal = ParseDecimalValue(dr["total"]);
                    netTotal += rowTotal;
                    discount += rowDisc;
                    grossTotal = netTotal + discount;
                    dgvCash.Rows.Add(i, dr["id"].ToString(), dr["pcode"].ToString(), dr["pdesc"].ToString(), rowPrice.ToString("0.00"), dr["qty"].ToString(), rowDisc.ToString("0.00"), rowTotal.ToString("#,##0.00"));//
                    hascart = true;
                }
                dr.Close();
                cn.Close();
                TransactionDiscountInfo transactionDiscount = MobileShopDb.GetPendingTransactionDiscount(lblTranNo.Text);
                decimal transactionDiscountAmount = Math.Max(0m, transactionDiscount.Amount);
                if (transactionDiscountAmount >= netTotal && netTotal > 0m)
                {
                    MobileShopDb.ClearPendingTransactionDiscount(lblTranNo.Text);
                    transactionDiscountAmount = 0m;
                }

                CurrentCartSubtotalUSD = netTotal;
                CurrentTransactionDiscountUSD = transactionDiscountAmount;
                CurrentSaleUSD = Math.Max(0m, netTotal - transactionDiscountAmount);
                CurrentDiscountUSD = discount + transactionDiscountAmount;
                lblSaleTotal.Text = "$ " + grossTotal.ToString("#,##0.00");
                lblDiscount.Text = CurrentDiscountUSD.ToString("#,##0.00");
                GetCartTotal();

                double saleTotalLBP = Convert.ToDouble(CurrentSaleUSD) * GetExchangeRateLBP();
                lblSaleTotalLBP.Text = LbpFormatter.Format(saleTotalLBP);



                if (!hascart)
                {
                    CurrentCartSubtotalUSD = 0m;
                    CurrentTransactionDiscountUSD = 0m;
                    CurrentSaleUSD = 0m;
                    CurrentDiscountUSD = 0m;
                    lblDiscount.Text = "0.00";
                }

                if (hascart) { btnClear.Enabled = true; btnSettle.Enabled = true; btnDiscount.Enabled = true; }
                else { btnClear.Enabled = false; btnSettle.Enabled = false; btnDiscount.Enabled = false; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, stitle);
            }

        }

        public void GetCartTotal()
        {
            decimal sales = CurrentSaleUSD;

            // VAT/Tax logic removed: totals are VAT-free.
            // Keep the existing labels/parameters populated with safe values so reports don't break.
            decimal vat = 0m;
            decimal vatable = 0m;

            lblVat.Text = vat.ToString("#,##0.00");
            lblVatable.Text = vatable.ToString("#,##0.00");
            lblDisplayTotal.Text = "$ " + sales.ToString("#,##0.00");

            double totalLBP = Convert.ToDouble(sales) * GetExchangeRateLBP();
            lblDisplayTotalLBP.Text = LbpFormatter.FormatSuffix(totalLBP);


        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimer.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        public void GetTranNo()
        {
            try
            {
                string sdate = DateTime.Now.ToString("yyyyMMdd");
                int count;
                string transno;
                cn.Open();
                cm = new SqlCommand("SELECT TOP 1 transno FROM tbCart WHERE transno LIKE '" + sdate + "%' ORDER BY id desc", cn);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    transno = dr[0].ToString();
                    count = int.Parse(transno.Substring(8, 4));
                    lblTranNo.Text = sdate + (count + 1);
                }
                else
                {
                    transno = sdate + "1001";
                    lblTranNo.Text = transno;
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {

                cn.Close();
                MessageBox.Show(ex.Message, stitle);

            }

        }

        // private void txtBarcode_TextChanged(object sender, EventArgs e)
        //{
        //try
        //{
        //    if (txtBarcode.Text == string.Empty) return;
        //    else
        //    {
        //        string _pcode;
        //        double _price;
        //        int _qty;
        //        cn.Open();
        //        cm = new SqlCommand("SELECT * FROM tbProduct WHERE barcode LIKE '" + txtBarcode.Text + "'", cn);
        //        dr = cm.ExecuteReader();
        //        dr.Read();
        //        if (dr.HasRows)
        //        {
        //            qty = int.Parse(dr["qty"].ToString());
        //            _pcode = dr["pcode"].ToString();
        //            _price = double.Parse(dr["price"].ToString());
        //            _qty = int.Parse(txtQty.Text);

        //            dr.Close();
        //            cn.Close();
        //            //insert to tbCart
        //            AddToCart(_pcode, _price, _qty);
        //        }
        //        dr.Close();
        //        cn.Close();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    cn.Close();
        //    MessageBox.Show(ex.Message, stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //}
        //  }

        //public void AddToCart(string _pcode, double _price, int _qty)
        //{
        //    try
        //    {
        //        string id = "";
        //        int cart_qty = 0;
        //        bool found = false;
        //        cn.Open();
        //        cm = new SqlCommand("Select * from tbCart Where transno = @transno and pcode = @pcode", cn);
        //        cm.Parameters.AddWithValue("@transno", lblTranNo.Text);
        //        cm.Parameters.AddWithValue("@pcode", _pcode);
        //        dr = cm.ExecuteReader();
        //        dr.Read();
        //        if (dr.HasRows)
        //        {
        //            id = dr["id"].ToString();
        //            cart_qty = int.Parse(dr["qty"].ToString());
        //            found = true;
        //        }
        //        else found = false;
        //        dr.Close();
        //        cn.Close();

        //        if (found)
        //        {
        //            // if (qty < (int.Parse(txtQty.Text) + cart_qty))
        //            if (qty < (_qty + cart_qty))

        //            {
        //                MessageBox.Show("Unable to procced. Remaining quantity on hand is " + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                return;
        //            }
        //            cn.Open();
        //            cm = new SqlCommand("Update tbCart set qty = (qty + " + _qty + ")Where id= '" + id + "'", cn);
        //            cm.ExecuteReader();
        //            cn.Close();
        //            txtBarcode.SelectionStart = 0;
        //            txtBarcode.SelectionLength = txtBarcode.Text.Length;
        //            LoadCart();
        //        }
        //        else
        //        {
        //            //if (qty < (int.Parse(txtQty.Text) + cart_qty))
        //            if (qty < (_qty + cart_qty))

        //            {
        //                MessageBox.Show("Unable to procced. Remaining qty on hand is" + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                return;
        //            }
        //            cn.Open();
        //            cm = new SqlCommand("INSERT INTO tbCart(transno, pcode, price, qty, sdate, cashier)VALUES(@transno, @pcode, @price, @qty, @sdate, @cashier)", cn);
        //            cm.Parameters.AddWithValue("@transno", lblTranNo.Text);
        //            cm.Parameters.AddWithValue("@pcode", _pcode);
        //            cm.Parameters.AddWithValue("@price", _price);
        //            cm.Parameters.AddWithValue("@qty", _qty);
        //            cm.Parameters.AddWithValue("@sdate", DateTime.Now);
        //            cm.Parameters.AddWithValue("@cashier", lblUsername.Text);
        //            cm.ExecuteNonQuery();
        //            cn.Close();
        //            LoadCart();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, stitle);
        //    }
        //}

        //private void dgvCash_SelectionChanged(object sender, EventArgs e)
        //{
        //    int i = dgvCash.CurrentRow.Index;
        //    id = dgvCash[1, i].Value.ToString();
        //    price = dgvCash[7, i].Value.ToString();
        //}

        public void AddToCart(string _pcode, decimal _price, int _qty, bool mergeExisting = true)
        {
            try
            {
                string cartId = "";
                int cartQty = 0;
                bool found = false;

                if (mergeExisting)
                {
                    cn.Open();
                    using (SqlCommand checkCmd = new SqlCommand(
                        @"SELECT TOP 1 id, qty
                  FROM tbCart
                  WHERE transno=@transno AND pcode=@pcode AND price=@price AND status='Pending'
                    AND ISNULL(itemType,'PRODUCT')='PRODUCT'", cn))
                    {
                        checkCmd.Parameters.AddWithValue("@transno", lblTranNo.Text);
                        checkCmd.Parameters.AddWithValue("@pcode", _pcode);
                        checkCmd.Parameters.AddWithValue("@price", _price);

                        using (SqlDataReader rdr = checkCmd.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                cartId = rdr["id"].ToString();
                                cartQty = Convert.ToInt32(rdr["qty"]);
                                found = true;
                            }
                        }
                    }
                    cn.Close();
                }

                if (qty < (_qty + cartQty))
                {
                    MessageBox.Show("Unable to proceed. Remaining quantity on hand is " + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                cn.Open();

                if (found)
                {
                    using (SqlCommand updateCmd = new SqlCommand("UPDATE tbCart SET qty = qty + @qty WHERE id=@id", cn))
                    {
                        updateCmd.Parameters.AddWithValue("@qty", _qty);
                        updateCmd.Parameters.AddWithValue("@id", cartId);
                        updateCmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    using (SqlCommand insertCmd = new SqlCommand(
                        @"INSERT INTO tbCart(transno, pcode, price, qty, sdate, cashier, itemType, itemDescription, costPrice)
                  SELECT @transno, pcode, @price, @qty, @sdate, @cashier, 'PRODUCT', pdesc, ISNULL(CostPrice,0)
                  FROM tbProduct WHERE pcode=@pcode AND isActive=1", cn))
                    {
                        insertCmd.Parameters.AddWithValue("@transno", lblTranNo.Text);
                        insertCmd.Parameters.AddWithValue("@pcode", _pcode);
                        SqlParameter pPrice = insertCmd.Parameters.Add("@price", SqlDbType.Decimal);
                        pPrice.Precision = 18;
                        pPrice.Scale = 4;
                        pPrice.Value = _price;
                        insertCmd.Parameters.AddWithValue("@qty", _qty);
                        insertCmd.Parameters.AddWithValue("@sdate", DateTime.Now);
                        insertCmd.Parameters.AddWithValue("@cashier", lblUsername.Text);
                        insertCmd.ExecuteNonQuery();
                    }
                }

                cn.Close();
                txtBarcode.SelectionStart = 0;
                txtBarcode.SelectionLength = txtBarcode.Text.Length;
                LoadCart();
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();

                MessageBox.Show(ex.Message, stitle);
            }
        }

        //private void dgvCash_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    string colName = dgvCash.Columns[e.ColumnIndex].Name;


        //    if (colName == "Delete")
        //    {
        //        if (MessageBox.Show("Remove this item", "Remove item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //        {
        //            dbcon.ExecuteQuery("Delete from tbCart where id like'" + dgvCash.Rows[e.RowIndex].Cells[1].Value.ToString() + "'");
        //            MessageBox.Show("Items has been successfully remove", "Remove item", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            LoadCart();
        //        }
        //    }
        //    else if (colName == "colAdd")
        //    {
        //        int i = 0;
        //        cn.Open();
        //        cm = new SqlCommand("SELECT SUM(qty) as qty FROM tbProduct WHERE pcode LIKE'" + dgvCash.Rows[e.RowIndex].Cells[2].Value.ToString() + "' GROUP BY pcode", cn);
        //        i = int.Parse(cm.ExecuteScalar().ToString());
        //        cn.Close();
        //        if (int.Parse(dgvCash.Rows[e.RowIndex].Cells[5].Value.ToString()) < i)
        //        {
        //            dbcon.ExecuteQuery("UPDATE tbCart SET qty = qty + " + int.Parse(txtQty.Text) + " WHERE transno LIKE '" + lblTranNo.Text + "'  AND pcode LIKE '" + dgvCash.Rows[e.RowIndex].Cells[2].Value.ToString() + "'");
        //            LoadCart();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Remaining qty on hand is " + i + "!", "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }
        //    }
        //    else if (colName == "colReduce")
        //    {
        //        int i = 0;
        //        cn.Open();
        //        cm = new SqlCommand("SELECT SUM(qty) as qty FROM tbCart WHERE pcode LIKE'" + dgvCash.Rows[e.RowIndex].Cells[2].Value.ToString() + "' GROUP BY pcode", cn);
        //        i = int.Parse(cm.ExecuteScalar().ToString());
        //        cn.Close();
        //        if (i > 1)
        //        {
        //            dbcon.ExecuteQuery("UPDATE tbCart SET qty = qty - " + int.Parse(txtQty.Text) + " WHERE transno LIKE '" + lblTranNo.Text + "'  AND pcode LIKE '" + dgvCash.Rows[e.RowIndex].Cells[2].Value.ToString() + "'");
        //            LoadCart();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Remaining qty on cart is " + i + "!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }
        //    }
        //}
        private void dgvCash_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string colName = dgvCash.Columns[e.ColumnIndex].Name;
            int cartId = Convert.ToInt32(dgvCash.Rows[e.RowIndex].Cells[1].Value);
            string itemType = MobileShopDb.GetCartItemType(cartId);

            if (colName == "Delete")
            {
                if (MessageBox.Show("Remove this item?", "Remove Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                using (SqlConnection con = new SqlConnection(dbcon.myConnection()))
                using (SqlCommand cmd = new SqlCommand("DELETE FROM dbo.tbCart WHERE id=@id AND status='Pending'", con))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = cartId;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadCart();
                return;
            }

            if (itemType != MobileShopDb.ProductItem && (colName == "colAdd" || colName == "colReduce"))
            {
                MessageBox.Show("Services and fixed-phone delivery use quantity 1. Add another service button if needed.", "Transaction", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (colName == "colAdd")
            {
                int currentRowQty = Convert.ToInt32(dgvCash.Rows[e.RowIndex].Cells[5].Value);
                int qtyToChange = GetScanQtyFallback();

                if (itemType == MobileShopDb.ProductItem)
                {
                    string pcode = dgvCash.Rows[e.RowIndex].Cells[2].Value.ToString();
                    int stockQty = 0;
                    string productBarcode = string.Empty;

                    using (SqlConnection con = new SqlConnection(dbcon.myConnection()))
                    using (SqlCommand cmd = new SqlCommand("SELECT barcode,qty FROM dbo.tbProduct WHERE pcode=@pcode", con))
                    {
                        cmd.Parameters.Add("@pcode", SqlDbType.VarChar, 50).Value = pcode;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                productBarcode = reader["barcode"].ToString();
                                stockQty = Convert.ToInt32(reader["qty"]);
                            }
                        }
                    }

                    bool openPrice = productBarcode == MASTER_BARCODE;
                    if (!openPrice && currentRowQty + qtyToChange > stockQty)
                    {
                        MessageBox.Show("Remaining quantity on hand is " + stockQty + ".", "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                using (SqlConnection con = new SqlConnection(dbcon.myConnection()))
                using (SqlCommand cmd = new SqlCommand("UPDATE dbo.tbCart SET qty=qty+@qty WHERE id=@id AND status='Pending'", con))
                {
                    cmd.Parameters.Add("@qty", SqlDbType.Int).Value = qtyToChange;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = cartId;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadCart();
            }
            else if (colName == "colReduce")
            {
                int currentRowQty = Convert.ToInt32(dgvCash.Rows[e.RowIndex].Cells[5].Value);
                int qtyToChange = GetScanQtyFallback();
                if (currentRowQty - qtyToChange < 1)
                {
                    MessageBox.Show("Use the remove button to delete the line.", "Transaction", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (SqlConnection con = new SqlConnection(dbcon.myConnection()))
                using (SqlCommand cmd = new SqlCommand("UPDATE dbo.tbCart SET qty=qty-@qty WHERE id=@id AND status='Pending'", con))
                {
                    cmd.Parameters.Add("@qty", SqlDbType.Int).Value = qtyToChange;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = cartId;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadCart();
            }
        }

        public void Noti()
        {
            int i = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM vwCriticalItems", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                Alert alert = new Alert(new MainForm());
                alert.lblPcode.Text = dr["pcode"].ToString();
                alert.showAlert(i + ". " + dr["pdesc"].ToString() + " - " + dr["qty"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void Cashier_Load(object sender, EventArgs e)
        {
            Noti();
            txtBarcode.Focus();

            // VAT removed: hide VAT/Vatable UI elements (values remain available as 0.00 for report parameters).
            try
            {
                label9.Visible = false;   // "VAT :"
                label10.Visible = false;  // "VATable :"
                lblVat.Visible = false;
                lblVatable.Visible = false;
            }
            catch { /* ignore if designer names change */ }

        }

        private void lblTotalLBP_Click(object sender, EventArgs e)
        {

        }
    }
}


