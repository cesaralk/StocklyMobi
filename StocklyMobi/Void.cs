using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StocklyMobi
{
    public partial class Void : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        CancelOrder cancelOrder;

        public Void(CancelOrder cancel)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            cancelOrder = cancel;
        }

        private void btnVoid_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim().Length == 0 || txtPass.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter the void username and password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtUsername.Text.Trim().Equals(cancelOrder.txtCancelBy.Text.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Void by name and cancelled by name are same! Please void by another person.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.myConnection()))
                {
                    con.Open();
                    using (SqlTransaction tran = con.BeginTransaction())
                    {
                        string voidUser = AuthenticateVoidUser(con, tran);
                        if (string.IsNullOrWhiteSpace(voidUser))
                        {
                            tran.Rollback();
                            MessageBox.Show("Invalid username or password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        CartCancelInfo info = GetCartCancelInfo(con, tran);
                        int cancelQty = Convert.ToInt32(cancelOrder.udCancelQty.Value);

                        if (cancelQty <= 0)
                        {
                            tran.Rollback();
                            MessageBox.Show("Cancel quantity must be greater than zero.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (cancelQty > info.CurrentQty)
                        {
                            tran.Rollback();
                            MessageBox.Show("Unable to cancel. Remaining sold quantity is " + info.CurrentQty + ".", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        decimal cancelDisc = CalculateProportionalAmount(info.CurrentDisc, info.CurrentQty, cancelQty);
                        decimal cancelTotal = CalculateProportionalAmount(info.CurrentTotal, info.CurrentQty, cancelQty);
                        decimal cancelTotalLL = CalculateProportionalAmount(info.CurrentTotalLL, info.CurrentQty, cancelQty);

                        SaveCancelOrder(con, tran, voidUser, info.Price, cancelQty, cancelTotal);

                        if (info.ItemType == MobileShopDb.ProductItem)
                        {
                            if (ShouldReturnToInventory())
                            {
                                using (SqlCommand updateProduct = new SqlCommand("UPDATE tbProduct SET qty = qty + @qty WHERE pcode = @pcode", con, tran))
                                {
                                    updateProduct.Parameters.Add("@qty", SqlDbType.Int).Value = cancelQty;
                                    updateProduct.Parameters.Add("@pcode", SqlDbType.VarChar).Value = info.PCode;
                                    updateProduct.ExecuteNonQuery();
                                }
                            }
                        }
                        else if (info.ItemType == MobileShopDb.ServiceItem)
                        {
                            ReverseServiceSale(con, tran, info, cancelQty, voidUser);
                        }
                        else if (info.ItemType == MobileShopDb.FixItem)
                        {
                            if (cancelQty != info.CurrentQty)
                                throw new InvalidOperationException("A fixed-phone delivery must be cancelled as a complete line.");
                            ReverseFixDelivery(con, tran, info);
                        }

                        UpdateSoldRowAfterReturn(con, tran, info, cancelQty, cancelDisc, cancelTotal, cancelTotalLL);
                        CancelTransactionDiscountIfFullyReturned(con, tran, info.TransNo, voidUser);

                        tran.Commit();
                    }
                }

                MessageBox.Show("Returned/cancelled quantity saved successfully.", "Cancel Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
                cancelOrder.ReloadSoldList();
                cancelOrder.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string AuthenticateVoidUser(SqlConnection con, SqlTransaction tran)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 username FROM tbUser WHERE username = @username AND password = @password", con, tran))
            {
                cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = txtUsername.Text.Trim();
                cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = txtPass.Text.Trim();
                object result = cmd.ExecuteScalar();
                return result == null || result == DBNull.Value ? string.Empty : result.ToString();
            }
        }

        private CartCancelInfo GetCartCancelInfo(SqlConnection con, SqlTransaction tran)
        {
            CartCancelInfo info = new CartCancelInfo();

            using (SqlCommand cmd = new SqlCommand(@"
                SELECT TOP 1
                    id,
                    transno,
                    pcode,
                    price,
                    qty,
                    ISNULL(disc, 0) AS disc,
                    ISNULL(total, 0) AS total,
                    ISNULL(itemType, 'PRODUCT') AS itemType,
                    referenceId,
                    ISNULL(costPrice, 0) AS costPrice
                FROM tbCart
                WHERE id = @id
                  AND status = 'Sold'", con, tran))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(cancelOrder.txtId.Text);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                        throw new Exception("Sold item was not found, or it is already fully cancelled/returned.");

                    info.Id = Convert.ToInt32(reader["id"]);
                    info.TransNo = reader["transno"].ToString();
                    info.PCode = reader["pcode"].ToString();
                    info.Price = Convert.ToDecimal(reader["price"]);
                    info.CurrentQty = Convert.ToInt32(reader["qty"]);
                    info.CurrentDisc = Convert.ToDecimal(reader["disc"]);
                    info.CurrentTotal = Convert.ToDecimal(reader["total"]);
                    info.ItemType = reader["itemType"].ToString();
                    info.ReferenceId = reader["referenceId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["referenceId"]);
                    info.CostPrice = Convert.ToDecimal(reader["costPrice"]);
                    info.CurrentTotalLL = 0m;
                }
            }

            if (info.CurrentQty <= 0)
                throw new Exception("This sold item has no remaining quantity to return/cancel.");

            info.CurrentTotalLL = GetCartTotalLLIfColumnExists(con, tran, info.Id);

            return info;
        }

        private decimal GetCartTotalLLIfColumnExists(SqlConnection con, SqlTransaction tran, int cartId)
        {
            using (SqlCommand check = new SqlCommand("SELECT CASE WHEN COL_LENGTH('dbo.tbCart', 'total_ll') IS NULL THEN 0 ELSE 1 END", con, tran))
            {
                bool hasColumn = Convert.ToInt32(check.ExecuteScalar()) == 1;
                if (!hasColumn)
                    return 0m;
            }

            using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(total_ll, 0) FROM tbCart WHERE id = @id", con, tran))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = cartId;
                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }

        private void ReverseServiceSale(SqlConnection con, SqlTransaction tran, CartCancelInfo info, int cancelQty, string username)
        {
            int providerId;
            int serviceId;
            int soldQty;
            decimal unitDeductedPrice;
            decimal providerBalance;
            decimal unitRetake;
            int retakeProviderId;

            using (SqlCommand cmd = new SqlCommand(@"
SELECT TOP 1 ss.providerId,ss.serviceId,ss.qty,
       ISNULL(ss.unitDeductedPrice,ss.unitCost) unitDeductedPrice,
       ISNULL(ss.retakeAmount,0) retakeAmount,ISNULL(ss.retakeProviderId,0) retakeProviderId,
       p.currentBalance
FROM dbo.tbServiceSale ss
INNER JOIN dbo.tbServiceProvider p WITH (UPDLOCK,ROWLOCK) ON p.id=ss.providerId
WHERE ss.cartId=@cartId AND ss.status='Sold'", con, tran))
            {
                cmd.Parameters.Add("@cartId", SqlDbType.Int).Value = info.Id;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read()) throw new InvalidOperationException("The provider transaction for this service sale was not found.");
                    providerId = Convert.ToInt32(reader["providerId"]);
                    serviceId = Convert.ToInt32(reader["serviceId"]);
                    soldQty = Convert.ToInt32(reader["qty"]);
                    unitDeductedPrice = Convert.ToDecimal(reader["unitDeductedPrice"]);
                    unitRetake = Convert.ToDecimal(reader["retakeAmount"]);
                    retakeProviderId = Convert.ToInt32(reader["retakeProviderId"]);
                    providerBalance = Convert.ToDecimal(reader["currentBalance"]);
                }
            }

            if (cancelQty > soldQty) throw new InvalidOperationException("The service return quantity is larger than the recorded sale.");

            decimal restoreAmount = unitDeductedPrice * cancelQty;
            decimal newBalance = providerBalance + restoreAmount;
            using (SqlCommand cmd = new SqlCommand("UPDATE dbo.tbServiceProvider SET currentBalance=@balance WHERE id=@id", con, tran))
            {
                AddMoney(cmd, "@balance", newBalance);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = providerId;
                cmd.ExecuteNonQuery();
            }

            using (SqlCommand cmd = new SqlCommand(@"
INSERT INTO dbo.tbProviderLedger(providerId,serviceId,cartId,movementType,amount,balanceAfter,username,sdate,notes)
VALUES(@providerId,@serviceId,@cartId,'SERVICE_RETURN',@amount,@balance,@username,GETDATE(),@notes)", con, tran))
            {
                cmd.Parameters.Add("@providerId", SqlDbType.Int).Value = providerId;
                cmd.Parameters.Add("@serviceId", SqlDbType.Int).Value = serviceId;
                cmd.Parameters.Add("@cartId", SqlDbType.Int).Value = info.Id;
                AddMoney(cmd, "@amount", restoreAmount);
                AddMoney(cmd, "@balance", newBalance);
                cmd.Parameters.Add("@username", SqlDbType.VarChar, 100).Value = username;
                cmd.Parameters.Add("@notes", SqlDbType.NVarChar, 500).Value = "Service return / invoice " + info.TransNo;
                cmd.ExecuteNonQuery();
            }

            decimal retakeToReverse = unitRetake * cancelQty;
            if (retakeToReverse > 0m && retakeProviderId > 0)
            {
                decimal retakeBalance;
                using (SqlCommand cmd = new SqlCommand(@"
SELECT currentBalance FROM dbo.tbServiceProvider WITH (UPDLOCK,ROWLOCK)
WHERE id=@id", con, tran))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = retakeProviderId;
                    object value = cmd.ExecuteScalar();
                    if (value == null || value == DBNull.Value) throw new InvalidOperationException("The validity retake line was not found.");
                    retakeBalance = Convert.ToDecimal(value);
                }

                if (retakeBalance < retakeToReverse)
                    throw new InvalidOperationException("The validity Retake $ amount has already been used from the mobile line. Add enough balance before cancelling this sale.");

                decimal retakeAfter = retakeBalance - retakeToReverse;
                using (SqlCommand cmd = new SqlCommand("UPDATE dbo.tbServiceProvider SET currentBalance=@balance WHERE id=@id", con, tran))
                {
                    AddMoney(cmd, "@balance", retakeAfter);
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = retakeProviderId;
                    cmd.ExecuteNonQuery();
                }

                using (SqlCommand cmd = new SqlCommand(@"
INSERT INTO dbo.tbProviderLedger(providerId,serviceId,cartId,movementType,amount,balanceAfter,username,sdate,notes)
VALUES(@providerId,@serviceId,@cartId,'VALIDITY_RETAKE_RETURN',@amount,@balance,@username,GETDATE(),@notes)", con, tran))
                {
                    cmd.Parameters.Add("@providerId", SqlDbType.Int).Value = retakeProviderId;
                    cmd.Parameters.Add("@serviceId", SqlDbType.Int).Value = serviceId;
                    cmd.Parameters.Add("@cartId", SqlDbType.Int).Value = info.Id;
                    AddMoney(cmd, "@amount", -retakeToReverse);
                    AddMoney(cmd, "@balance", retakeAfter);
                    cmd.Parameters.Add("@username", SqlDbType.VarChar, 100).Value = username;
                    cmd.Parameters.Add("@notes", SqlDbType.NVarChar, 500).Value = "Validity retake reversal / invoice " + info.TransNo;
                    cmd.ExecuteNonQuery();
                }
            }

            using (SqlCommand cmd = new SqlCommand(@"
UPDATE dbo.tbServiceSale
SET qty=qty-@qty,
    status=CASE WHEN qty-@qty<=0 THEN 'Cancelled' ELSE 'Sold' END
WHERE cartId=@cartId AND status='Sold'", con, tran))
            {
                cmd.Parameters.Add("@qty", SqlDbType.Int).Value = cancelQty;
                cmd.Parameters.Add("@cartId", SqlDbType.Int).Value = info.Id;
                cmd.ExecuteNonQuery();
            }
        }

        private void ReverseFixDelivery(SqlConnection con, SqlTransaction tran, CartCancelInfo info)
        {
            using (SqlCommand cmd = new SqlCommand(@"
UPDATE dbo.tbMobileFix
SET status='Ready', deliveredAt=NULL, deliveredBy=NULL, deliveredTransno=NULL
WHERE id=@fixId AND deliveredTransno=@transno", con, tran))
            {
                cmd.Parameters.Add("@fixId", SqlDbType.Int).Value = info.ReferenceId;
                cmd.Parameters.Add("@transno", SqlDbType.VarChar, 50).Value = info.TransNo;
                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException("The fixing delivery record could not be restored.");
            }
        }

        private static void AddMoney(SqlCommand cmd, string name, decimal value)
        {
            SqlParameter p = cmd.Parameters.Add(name, SqlDbType.Decimal);
            p.Precision = 18;
            p.Scale = 4;
            p.Value = value;
        }

        private void SaveCancelOrder(SqlConnection con, SqlTransaction tran, string user, decimal price, int cancelQty, decimal cancelTotal)
        {
            using (SqlCommand cmd = new SqlCommand(@"
                INSERT INTO tbCancel
                    (transno, pcode, price, qty, total, sdate, voidby, cancelledby, reason, action)
                VALUES
                    (@transno, @pcode, @price, @qty, @total, @sdate, @voidby, @cancelledby, @reason, @action)", con, tran))
            {
                cmd.Parameters.Add("@transno", SqlDbType.VarChar).Value = cancelOrder.txtTransno.Text.Trim();
                cmd.Parameters.Add("@pcode", SqlDbType.VarChar).Value = cancelOrder.txtPcode.Text.Trim();

                SqlParameter pPrice = cmd.Parameters.Add("@price", SqlDbType.Decimal);
                pPrice.Precision = 18;
                pPrice.Scale = 4;
                pPrice.Value = price;

                cmd.Parameters.Add("@qty", SqlDbType.Int).Value = cancelQty;

                SqlParameter pTotal = cmd.Parameters.Add("@total", SqlDbType.Decimal);
                pTotal.Precision = 18;
                pTotal.Scale = 4;
                pTotal.Value = cancelTotal;

                cmd.Parameters.Add("@sdate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@voidby", SqlDbType.VarChar).Value = user;
                cmd.Parameters.Add("@cancelledby", SqlDbType.VarChar).Value = cancelOrder.txtCancelBy.Text.Trim();
                cmd.Parameters.Add("@reason", SqlDbType.VarChar).Value = cancelOrder.txtReason.Text.Trim();
                cmd.Parameters.Add("@action", SqlDbType.VarChar).Value = "Yes";
                cmd.ExecuteNonQuery();
            }
        }

        private void UpdateSoldRowAfterReturn(SqlConnection con, SqlTransaction tran, CartCancelInfo info, int cancelQty, decimal cancelDisc, decimal cancelTotal, decimal cancelTotalLL)
        {
            int remainingQty = info.CurrentQty - cancelQty;
            decimal remainingDisc = info.CurrentDisc - cancelDisc;
            decimal remainingTotal = info.CurrentTotal - cancelTotal;
            decimal remainingTotalLL = info.CurrentTotalLL - cancelTotalLL;

            if (remainingQty > 0)
            {
                using (SqlCommand cmd = new SqlCommand(@"
                    UPDATE tbCart
                    SET qty = @qty,
                        disc = @disc,
                        total = @total
                    WHERE id = @id", con, tran))
                {
                    cmd.Parameters.Add("@qty", SqlDbType.Int).Value = remainingQty;

                    SqlParameter pDisc = cmd.Parameters.Add("@disc", SqlDbType.Decimal);
                    pDisc.Precision = 18;
                    pDisc.Scale = 4;
                    pDisc.Value = remainingDisc;

                    SqlParameter pTotal = cmd.Parameters.Add("@total", SqlDbType.Decimal);
                    pTotal.Precision = 18;
                    pTotal.Scale = 4;
                    pTotal.Value = remainingTotal;

                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = info.Id;
                    cmd.ExecuteNonQuery();
                }

                UpdateCartTotalLLIfColumnExists(con, tran, info.Id, remainingTotalLL);
            }
            else
            {
                using (SqlCommand cmd = new SqlCommand(@"
                    UPDATE tbCart
                    SET qty = 0,
                        disc = 0,
                        total = 0,
                        status = 'Cancelled'
                    WHERE id = @id", con, tran))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = info.Id;
                    cmd.ExecuteNonQuery();
                }

                UpdateCartTotalLLIfColumnExists(con, tran, info.Id, 0m);
            }
        }

        private static void CancelTransactionDiscountIfFullyReturned(SqlConnection con, SqlTransaction tran, string transNo, string username)
        {
            int remainingCount;
            using (SqlCommand remaining = new SqlCommand("SELECT COUNT(*) FROM dbo.tbCart WHERE transno=@transno AND status='Sold' AND qty>0", con, tran))
            {
                remaining.Parameters.Add("@transno", SqlDbType.VarChar, 50).Value = transNo;
                remainingCount = Convert.ToInt32(remaining.ExecuteScalar());
            }

            using (SqlCommand receipt = new SqlCommand(@"
UPDATE dbo.tbReceiptHeader
SET status=@status
WHERE transno=@transno", con, tran))
            {
                receipt.Parameters.Add("@status", SqlDbType.VarChar, 30).Value = remainingCount > 0 ? "Partially Returned" : "Cancelled";
                receipt.Parameters.Add("@transno", SqlDbType.VarChar, 50).Value = transNo;
                receipt.ExecuteNonQuery();
            }

            if (remainingCount > 0) return;

            using (SqlCommand cmd = new SqlCommand(@"
UPDATE dbo.tbTransactionDiscount
SET status='Cancelled',cancelledAt=GETDATE(),cancelledBy=@user
WHERE transno=@transno AND status='Sold'", con, tran))
            {
                cmd.Parameters.Add("@transno", SqlDbType.VarChar, 50).Value = transNo;
                cmd.Parameters.Add("@user", SqlDbType.VarChar, 100).Value = username;
                cmd.ExecuteNonQuery();
            }
        }

        private void UpdateCartTotalLLIfColumnExists(SqlConnection con, SqlTransaction tran, int cartId, decimal totalLL)
        {
            using (SqlCommand check = new SqlCommand("SELECT CASE WHEN COL_LENGTH('dbo.tbCart', 'total_ll') IS NULL THEN 0 ELSE 1 END", con, tran))
            {
                bool hasColumn = Convert.ToInt32(check.ExecuteScalar()) == 1;
                if (!hasColumn)
                    return;
            }

            using (SqlCommand cmd = new SqlCommand("UPDATE tbCart SET total_ll = @total_ll WHERE id = @id", con, tran))
            {
                SqlParameter pTotalLL = cmd.Parameters.Add("@total_ll", SqlDbType.Decimal);
                pTotalLL.Precision = 18;
                pTotalLL.Scale = 4;
                pTotalLL.Value = totalLL;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = cartId;
                cmd.ExecuteNonQuery();
            }
        }

        private bool ShouldReturnToInventory()
        {
            // The current POS behavior requested by the owner is that every returned/cancelled quantity
            // is added back to product stock.
            return true;
        }

        private decimal CalculateProportionalAmount(decimal currentAmount, int currentQty, int cancelQty)
        {
            if (currentQty <= 0)
                return 0m;

            return Math.Round(currentAmount * cancelQty / currentQty, 4, MidpointRounding.AwayFromZero);
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Void_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }

        private class CartCancelInfo
        {
            public int Id { get; set; }
            public string TransNo { get; set; }
            public string PCode { get; set; }
            public decimal Price { get; set; }
            public int CurrentQty { get; set; }
            public decimal CurrentDisc { get; set; }
            public decimal CurrentTotal { get; set; }
            public decimal CurrentTotalLL { get; set; }
            public string ItemType { get; set; }
            public int ReferenceId { get; set; }
            public decimal CostPrice { get; set; }
        }
    }
}
