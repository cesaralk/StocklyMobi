using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace StocklyMobi
{
    internal static class MobileShopDb
    {
        public const string ProductItem = "PRODUCT";
        public const string ServiceItem = "SERVICE";
        public const string FixItem = "FIX";

        public const string MobileRecharge = "Mobile Recharge";
        public const string TransferBalance = "Transfer Balance";
        public const string ValidityRecharge = "Validity Recharge";
        public const string InternetBundles = "Internet Bundles";
        public const string Apps = "Apps";

        public const string WishProviderCode = "WISH";
        public const string MtcLineProviderCode = "MTC_LINE";
        public const string AlfaLineProviderCode = "ALFA_LINE";

        private static string ConnectionString
        {
            get { return new DBConnect().myConnection(); }
        }

        public static TransactionDiscountInfo GetPendingTransactionDiscount(string transNo)
        {
            TransactionDiscountInfo info = new TransactionDiscountInfo();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(@"
SELECT TOP 1 amount,ISNULL(reason,'') reason
FROM dbo.tbTransactionDiscount
WHERE transno=@transno AND status='Pending'", con))
            {
                cmd.Parameters.Add("@transno", SqlDbType.VarChar, 50).Value = transNo ?? string.Empty;
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        info.Amount = Convert.ToDecimal(reader["amount"]);
                        info.Reason = reader["reason"].ToString();
                    }
                }
            }
            return info;
        }

        public static void SavePendingTransactionDiscount(string transNo, decimal amount, string reason, string username, decimal currentSubtotal)
        {
            if (string.IsNullOrWhiteSpace(transNo)) throw new ArgumentException("Transaction number is required.");
            if (amount <= 0m) throw new ArgumentException("Discount amount must be greater than zero.");
            if (currentSubtotal <= 0m || amount >= currentSubtotal)
                throw new ArgumentException("Discount amount must be less than the current cart subtotal.");

            using (SqlConnection con = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(@"
IF EXISTS(SELECT 1 FROM dbo.tbTransactionDiscount WHERE transno=@transno AND status='Pending')
BEGIN
    UPDATE dbo.tbTransactionDiscount
    SET amount=@amount,reason=@reason,appliedBy=@user,appliedAt=GETDATE()
    WHERE transno=@transno AND status='Pending';
END
ELSE
BEGIN
    INSERT INTO dbo.tbTransactionDiscount(transno,amount,reason,appliedBy,appliedAt,status)
    VALUES(@transno,@amount,@reason,@user,GETDATE(),'Pending');
END", con))
            {
                cmd.Parameters.Add("@transno", SqlDbType.VarChar, 50).Value = transNo.Trim();
                AddMoney(cmd, "@amount", amount);
                cmd.Parameters.Add("@reason", SqlDbType.NVarChar, 250).Value = string.IsNullOrWhiteSpace(reason) ? (object)DBNull.Value : reason.Trim();
                cmd.Parameters.Add("@user", SqlDbType.VarChar, 100).Value = string.IsNullOrWhiteSpace(username) ? "cashier" : username.Trim();
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void ClearPendingTransactionDiscount(string transNo)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM dbo.tbTransactionDiscount WHERE transno=@transno AND status='Pending'", con))
            {
                cmd.Parameters.Add("@transno", SqlDbType.VarChar, 50).Value = transNo ?? string.Empty;
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static bool IsKnownServiceType(string type)
        {
            return type == MobileRecharge || type == TransferBalance || type == ValidityRecharge ||
                   type == InternetBundles || type == Apps;
        }

        public static DataTable GetProviders(bool activeOnly)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(@"
SELECT id, providerName, ISNULL(providerCode,'') AS providerCode,
       ISNULL(providerType,'Wallet') AS providerType,
       currentBalance, isActive, createdAt
FROM dbo.tbServiceProvider
WHERE (@activeOnly=0 OR isActive=1)
ORDER BY ISNULL(displayOrder,999), providerName", con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.Add("@activeOnly", SqlDbType.Bit).Value = activeOnly;
                DataTable table = new DataTable();
                da.Fill(table);
                return table;
            }
        }

        public static DataTable GetServices(bool activeOnly)
        {
            return GetServices(activeOnly, string.Empty);
        }

        public static DataTable GetServices(bool activeOnly, string serviceType)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(@"
SELECT s.id, s.serviceName, s.serviceType, s.providerId, p.providerName,
       ISNULL(p.providerCode,'') AS providerCode,
       ISNULL(s.operatorName,'') AS operatorName,
       ISNULL(s.retakeEnabled,0) AS retakeEnabled,
       s.cost, ISNULL(s.deductedPrice,s.cost) AS deductedPrice,
       s.finalPrice, s.profit, s.isActive, p.currentBalance
FROM dbo.tbRechargeService s
INNER JOIN dbo.tbServiceProvider p ON p.id=s.providerId
WHERE (@activeOnly=0 OR (s.isActive=1 AND p.isActive=1))
  AND (@serviceType='' OR s.serviceType=@serviceType)
ORDER BY s.serviceType, s.serviceName, s.id", con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.Add("@activeOnly", SqlDbType.Bit).Value = activeOnly;
                cmd.Parameters.Add("@serviceType", SqlDbType.NVarChar, 40).Value = serviceType ?? string.Empty;
                DataTable table = new DataTable();
                da.Fill(table);
                return table;
            }
        }

        public static DataTable GetServiceDescriptions(string serviceType)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(@"
SELECT DISTINCT serviceName
FROM dbo.tbRechargeService
WHERE serviceType=@type AND serviceName<>''
ORDER BY serviceName", con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.Add("@type", SqlDbType.NVarChar, 40).Value = serviceType;
                DataTable table = new DataTable();
                da.Fill(table);
                return table;
            }
        }

        public static int SaveProvider(int id, string name, decimal openingBalance, bool isActive, string username)
        {
            string generatedCode = CreateProviderCode(name);
            return SaveProvider(id, name, generatedCode, "Wallet", openingBalance, isActive, username);
        }

        public static int SaveProvider(int id, string name, string code, string providerType,
            decimal openingBalance, bool isActive, string username)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Provider name is required.");
            if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("Provider code is required.");
            if (providerType != "Wallet" && providerType != "Mobile Line")
                throw new ArgumentException("Provider type must be Wallet or Mobile Line.");
            if (openingBalance < 0m) throw new ArgumentException("Opening balance cannot be negative.");

            string cleanCode = CreateProviderCode(code);
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlTransaction tr = con.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        if (id <= 0)
                        {
                            using (SqlCommand cmd = new SqlCommand(@"
INSERT INTO dbo.tbServiceProvider(providerName,providerCode,providerType,currentBalance,isActive,displayOrder,createdAt)
VALUES(@name,@code,@type,@balance,@active,999,GETDATE());
SELECT CAST(SCOPE_IDENTITY() AS INT);", con, tr))
                            {
                                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = name.Trim();
                                cmd.Parameters.Add("@code", SqlDbType.NVarChar, 30).Value = cleanCode;
                                cmd.Parameters.Add("@type", SqlDbType.NVarChar, 30).Value = providerType;
                                AddMoney(cmd, "@balance", openingBalance);
                                cmd.Parameters.Add("@active", SqlDbType.Bit).Value = isActive;
                                id = Convert.ToInt32(cmd.ExecuteScalar());
                            }

                            if (openingBalance > 0m)
                                InsertProviderLedger(con, tr, id, null, null, "OPENING_BALANCE",
                                    openingBalance, openingBalance, username, "Opening provider balance");
                        }
                        else
                        {
                            using (SqlCommand cmd = new SqlCommand(@"
UPDATE dbo.tbServiceProvider
SET providerName=@name,providerCode=@code,providerType=@type,isActive=@active
WHERE id=@id", con, tr))
                            {
                                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = name.Trim();
                                cmd.Parameters.Add("@code", SqlDbType.NVarChar, 30).Value = cleanCode;
                                cmd.Parameters.Add("@type", SqlDbType.NVarChar, 30).Value = providerType;
                                cmd.Parameters.Add("@active", SqlDbType.Bit).Value = isActive;
                                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                                if (cmd.ExecuteNonQuery() == 0) throw new InvalidOperationException("Provider was not found.");
                            }
                        }

                        tr.Commit();
                        return id;
                    }
                    catch
                    {
                        tr.Rollback();
                        throw;
                    }
                }
            }
        }

        public static void AdjustProviderBalance(int providerId, decimal amount, string username, string notes)
        {
            if (amount == 0m) throw new ArgumentException("Adjustment amount cannot be zero.");

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlTransaction tr = con.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        decimal oldBalance = ReadProviderBalance(con, tr, providerId);
                        decimal newBalance = oldBalance + amount;
                        if (newBalance < 0m) throw new InvalidOperationException("Provider balance cannot become negative.");
                        UpdateProviderBalance(con, tr, providerId, newBalance);
                        InsertProviderLedger(con, tr, providerId, null, null,
                            amount > 0m ? "BALANCE_ADDED" : "BALANCE_REMOVED",
                            amount, newBalance, username, notes);
                        tr.Commit();
                    }
                    catch
                    {
                        tr.Rollback();
                        throw;
                    }
                }
            }
        }

        public static int SaveService(int id, string name, string type, int providerId,
            decimal cost, decimal finalPrice, bool active)
        {
            return SaveService(id, name, type, providerId, string.Empty, cost, cost, finalPrice, active);
        }

        public static int SaveService(int id, string name, string type, int providerId, string operatorName,
            decimal cost, decimal finalPrice, bool active)
        {
            return SaveService(id, name, type, providerId, operatorName, cost, cost, finalPrice, active);
        }

        public static int SaveService(int id, string name, string type, int providerId, string operatorName,
            decimal cost, decimal deductedPrice, decimal finalPrice, bool active)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Service description is required.");
            if (!IsKnownServiceType(type)) throw new ArgumentException("Choose a valid service type.");
            if (providerId <= 0) throw new ArgumentException("Choose a provider.");
            if (cost < 0m || deductedPrice < 0m || finalPrice < 0m)
                throw new ArgumentException("Cost, deducted price, and final price cannot be negative.");

            string cleanOperator = NormalizeOperator(operatorName);
            if (type == ValidityRecharge && string.IsNullOrWhiteSpace(cleanOperator))
                throw new ArgumentException("Choose MTC or Alfa for validity recharge.");
            if (type == TransferBalance && string.IsNullOrWhiteSpace(cleanOperator))
                cleanOperator = GetOperatorFromProvider(providerId);
            if (type != ValidityRecharge && type != TransferBalance)
                cleanOperator = string.Empty;

            bool retakeEnabled = type == ValidityRecharge;
            decimal providerDeduction = type == ValidityRecharge ? deductedPrice : cost;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                ValidateServiceProvider(con, type, providerId);
                if (id <= 0)
                {
                    using (SqlCommand cmd = new SqlCommand(@"
INSERT INTO dbo.tbRechargeService
(serviceName,serviceType,providerId,operatorName,retakeEnabled,cost,deductedPrice,finalPrice,isActive,createdAt)
VALUES(@name,@type,@providerId,@operator,@retake,@cost,@deducted,@price,@active,GETDATE());
SELECT CAST(SCOPE_IDENTITY() AS INT);", con))
                    {
                        SetServiceParameters(cmd, name, type, providerId, cleanOperator, retakeEnabled,
                            cost, providerDeduction, finalPrice, active);
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }

                using (SqlCommand cmd = new SqlCommand(@"
UPDATE dbo.tbRechargeService
SET serviceName=@name,serviceType=@type,providerId=@providerId,
    operatorName=@operator,retakeEnabled=@retake,cost=@cost,deductedPrice=@deducted,
    finalPrice=@price,isActive=@active
WHERE id=@id", con))
                {
                    SetServiceParameters(cmd, name, type, providerId, cleanOperator, retakeEnabled,
                        cost, providerDeduction, finalPrice, active);
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    if (cmd.ExecuteNonQuery() == 0) throw new InvalidOperationException("Service was not found.");
                    return id;
                }
            }
        }

        private static void ValidateServiceProvider(SqlConnection con, string serviceType, int providerId)
        {
            string providerCode;
            bool isActive;
            using (SqlCommand cmd = new SqlCommand(@"
SELECT ISNULL(providerCode,''), isActive
FROM dbo.tbServiceProvider
WHERE id=@id", con))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = providerId;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                        throw new InvalidOperationException("The selected provider was not found.");
                    providerCode = Convert.ToString(reader[0]);
                    isActive = Convert.ToBoolean(reader[1]);
                }
            }

            if (!isActive)
                throw new InvalidOperationException("The selected provider is inactive.");

            if ((serviceType == MobileRecharge || serviceType == ValidityRecharge) &&
                providerCode != WishProviderCode)
            {
                throw new InvalidOperationException(serviceType + " must use the WishMoney provider.");
            }

            if (serviceType == TransferBalance &&
                providerCode != MtcLineProviderCode && providerCode != AlfaLineProviderCode)
            {
                throw new InvalidOperationException("Transfer Balance must use the MTC Line or Alfa Line provider.");
            }
        }

        private static void SetServiceParameters(SqlCommand cmd, string name, string type, int providerId,
            string operatorName, bool retakeEnabled, decimal cost, decimal deductedPrice,
            decimal finalPrice, bool active)
        {
            cmd.Parameters.Add("@name", SqlDbType.NVarChar, 150).Value = name.Trim();
            cmd.Parameters.Add("@type", SqlDbType.NVarChar, 40).Value = type;
            cmd.Parameters.Add("@providerId", SqlDbType.Int).Value = providerId;
            cmd.Parameters.Add("@operator", SqlDbType.NVarChar, 20).Value = string.IsNullOrWhiteSpace(operatorName) ? (object)DBNull.Value : operatorName;
            cmd.Parameters.Add("@retake", SqlDbType.Bit).Value = retakeEnabled;
            AddMoney(cmd, "@cost", cost);
            AddMoney(cmd, "@deducted", deductedPrice);
            AddMoney(cmd, "@price", finalPrice);
            cmd.Parameters.Add("@active", SqlDbType.Bit).Value = active;
        }

        public static void AddServiceToCart(string transNo, string cashier, int serviceId)
        {
            AddServiceToCart(transNo, cashier, serviceId, 0m);
        }

        public static void AddServiceToCart(string transNo, string cashier, int serviceId, decimal retakeAmount)
        {
            if (string.IsNullOrWhiteSpace(transNo)) throw new ArgumentException("Start a transaction first.");
            if (retakeAmount < 0m) throw new ArgumentException("Retake amount cannot be negative.");

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlTransaction tr = con.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        string name;
                        string type;
                        string operatorName;
                        decimal cost;
                        decimal deductedPrice;
                        decimal price;
                        int retakeProviderId = 0;

                        using (SqlCommand cmd = new SqlCommand(@"
SELECT s.serviceName,s.serviceType,ISNULL(s.operatorName,'') operatorName,
       ISNULL(s.retakeEnabled,0) retakeEnabled,s.cost,
       ISNULL(s.deductedPrice,s.cost) deductedPrice,s.finalPrice
FROM dbo.tbRechargeService s
INNER JOIN dbo.tbServiceProvider p ON p.id=s.providerId
WHERE s.id=@id AND s.isActive=1 AND p.isActive=1", con, tr))
                        {
                            cmd.Parameters.Add("@id", SqlDbType.Int).Value = serviceId;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (!reader.Read()) throw new InvalidOperationException("This service is inactive or no longer exists.");
                                name = reader["serviceName"].ToString();
                                type = reader["serviceType"].ToString();
                                operatorName = reader["operatorName"].ToString();
                                cost = Convert.ToDecimal(reader["cost"]);
                                deductedPrice = Convert.ToDecimal(reader["deductedPrice"]);
                                price = Convert.ToDecimal(reader["finalPrice"]);
                                bool retakeEnabled = Convert.ToBoolean(reader["retakeEnabled"]);
                                if (!retakeEnabled && retakeAmount != 0m)
                                    throw new InvalidOperationException("Retake amount is only used for validity recharge.");
                            }
                        }

                        if (type == ValidityRecharge)
                        {
                            if (string.IsNullOrWhiteSpace(operatorName))
                                throw new InvalidOperationException("This validity service has no MTC/Alfa operator configured.");
                            retakeProviderId = GetProviderIdByCode(con, tr,
                                operatorName == "MTC" ? MtcLineProviderCode : AlfaLineProviderCode);
                            if (retakeProviderId <= 0)
                                throw new InvalidOperationException(operatorName + " line provider was not found. Run the new database script.");
                        }
                        else
                        {
                            retakeAmount = 0m;
                        }

                        string description = name;
                        if (!string.IsNullOrWhiteSpace(operatorName)) description += " - " + operatorName;
                        if (retakeAmount > 0m) description += " | Retake $" + retakeAmount.ToString("0.00");

                        using (SqlCommand cmd = new SqlCommand(@"
INSERT INTO dbo.tbCart
(transno,pcode,price,qty,sdate,status,cashier,itemType,referenceId,itemDescription,costPrice,
 serviceDeductedPrice,serviceRetakeAmount,serviceRetakeProviderId,serviceOperator)
VALUES(@transno,@pcode,@price,1,GETDATE(),'Pending',@cashier,'SERVICE',@serviceId,@description,@cost,
 @deducted,@retake,@retakeProvider,@operator)", con, tr))
                        {
                            cmd.Parameters.Add("@transno", SqlDbType.VarChar, 50).Value = transNo;
                            cmd.Parameters.Add("@pcode", SqlDbType.VarChar, 50).Value = "SVC-" + serviceId.ToString("000000");
                            AddMoney(cmd, "@price", price);
                            cmd.Parameters.Add("@cashier", SqlDbType.VarChar, 100).Value = cashier;
                            cmd.Parameters.Add("@serviceId", SqlDbType.Int).Value = serviceId;
                            cmd.Parameters.Add("@description", SqlDbType.NVarChar, 250).Value = description;
                            AddMoney(cmd, "@cost", cost);
                            AddMoney(cmd, "@deducted", deductedPrice);
                            AddMoney(cmd, "@retake", retakeAmount);
                            cmd.Parameters.Add("@retakeProvider", SqlDbType.Int).Value = retakeProviderId > 0 ? (object)retakeProviderId : DBNull.Value;
                            cmd.Parameters.Add("@operator", SqlDbType.NVarChar, 20).Value = string.IsNullOrWhiteSpace(operatorName) ? (object)DBNull.Value : operatorName;
                            cmd.ExecuteNonQuery();
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
        }

        public static string CreateFix(string customerName, string phoneNumber, string phoneModel, string problem,
            decimal costPrice, decimal finalPrice, DateTime estimatedFinish, string receivedBy)
        {
            if (string.IsNullOrWhiteSpace(customerName)) throw new ArgumentException("Customer name is required.");
            if (string.IsNullOrWhiteSpace(phoneNumber)) throw new ArgumentException("Customer phone number is required.");
            if (string.IsNullOrWhiteSpace(phoneModel)) throw new ArgumentException("Phone model is required.");
            if (string.IsNullOrWhiteSpace(problem)) throw new ArgumentException("Problem description is required.");
            if (costPrice < 0m || finalPrice < 0m) throw new ArgumentException("Cost and final price cannot be negative.");

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlTransaction tr = con.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        string prefix = "FIX-" + DateTime.Now.ToString("yy") + "-";
                        int next;
                        using (SqlCommand cmd = new SqlCommand(@"
SELECT ISNULL(MAX(TRY_CONVERT(INT,SUBSTRING(fixCode,LEN(@prefix)+1,20))),-1)+1
FROM dbo.tbMobileFix WITH (UPDLOCK,HOLDLOCK)
WHERE fixCode LIKE @prefix+'%'", con, tr))
                        {
                            cmd.Parameters.Add("@prefix", SqlDbType.VarChar, 20).Value = prefix;
                            next = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        string code = prefix + next.ToString();
                        using (SqlCommand cmd = new SqlCommand(@"
INSERT INTO dbo.tbMobileFix
(fixCode,customerName,phoneNumber,phoneModel,problemDescription,costPrice,finalPrice,estimatedFinish,status,receivedBy,receivedAt)
VALUES(@code,@customer,@phone,@model,@problem,@cost,@price,@estimated,'Received',@receivedBy,GETDATE())", con, tr))
                        {
                            cmd.Parameters.Add("@code", SqlDbType.VarChar, 40).Value = code;
                            cmd.Parameters.Add("@customer", SqlDbType.NVarChar, 150).Value = customerName.Trim();
                            cmd.Parameters.Add("@phone", SqlDbType.NVarChar, 50).Value = phoneNumber.Trim();
                            cmd.Parameters.Add("@model", SqlDbType.NVarChar, 150).Value = phoneModel.Trim();
                            cmd.Parameters.Add("@problem", SqlDbType.NVarChar, 500).Value = problem.Trim();
                            AddMoney(cmd, "@cost", costPrice);
                            AddMoney(cmd, "@price", finalPrice);
                            cmd.Parameters.Add("@estimated", SqlDbType.DateTime).Value = estimatedFinish;
                            cmd.Parameters.Add("@receivedBy", SqlDbType.VarChar, 100).Value = string.IsNullOrWhiteSpace(receivedBy) ? "cashier" : receivedBy;
                            cmd.ExecuteNonQuery();
                        }

                        tr.Commit();
                        return code;
                    }
                    catch
                    {
                        tr.Rollback();
                        throw;
                    }
                }
            }
        }

        public static string NormalizeFixCodeInput(string input)
        {
            string value = (input ?? string.Empty).Trim().ToUpperInvariant().Replace(" ", string.Empty);
            if (value.Length == 0) return string.Empty;
            if (Regex.IsMatch(value, @"^FIX-\d{2}-\d+$")) return value;
            if (Regex.IsMatch(value, @"^FIX-\d+$")) return "FIX-" + DateTime.Now.ToString("yy") + "-" + value.Substring(4);
            if (Regex.IsMatch(value, @"^\d{2}-\d+$")) return "FIX-" + value;
            if (Regex.IsMatch(value, @"^\d+$")) return "FIX-" + DateTime.Now.ToString("yy") + "-" + value;
            return value;
        }

        public static DataTable GetFixes(string search)
        {
            string raw = (search ?? string.Empty).Trim();
            string exact = NormalizeFixCodeInput(raw);
            using (SqlConnection con = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(@"
SELECT id,fixCode AS [Fix ID],customerName AS [Customer],phoneNumber AS [Phone],
       phoneModel AS [Phone Model],problemDescription AS [Problem],costPrice AS [Cost],
       finalPrice AS [Final Price],profit AS [Profit],estimatedFinish AS [Estimated Finish],
       status AS [Status],receivedBy AS [Received By],receivedAt AS [Received At],
       deliveredAt AS [Delivered At],deliveredTransno AS [Invoice]
FROM dbo.tbMobileFix
WHERE @search='' OR fixCode=@exact OR fixCode LIKE @like OR customerName LIKE @like OR
      phoneNumber LIKE @like OR phoneModel LIKE @like
ORDER BY CASE WHEN fixCode=@exact THEN 0 ELSE 1 END,
         CASE status WHEN 'Ready' THEN 0 WHEN 'Received' THEN 1 WHEN 'In Progress' THEN 2 ELSE 3 END,
         receivedAt DESC", con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.Add("@search", SqlDbType.NVarChar, 150).Value = raw;
                cmd.Parameters.Add("@exact", SqlDbType.VarChar, 40).Value = exact;
                cmd.Parameters.Add("@like", SqlDbType.NVarChar, 170).Value = "%" + raw + "%";
                DataTable table = new DataTable();
                da.Fill(table);
                return table;
            }
        }

        public static DataRow GetFixByCode(string fixCode)
        {
            string normalized = NormalizeFixCodeInput(fixCode);
            using (SqlConnection con = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(@"
SELECT TOP 1 id,fixCode,customerName,phoneNumber,phoneModel,problemDescription,costPrice,finalPrice,
       estimatedFinish,status,receivedBy,receivedAt,deliveredAt,deliveredTransno
FROM dbo.tbMobileFix WHERE fixCode=@code", con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.Add("@code", SqlDbType.VarChar, 40).Value = normalized;
                DataTable table = new DataTable();
                da.Fill(table);
                return table.Rows.Count == 0 ? null : table.Rows[0];
            }
        }

        public static void UpdateFixDetails(
            int fixId,
            string problem,
            decimal costPrice,
            decimal finalPrice)
        {
            if (fixId <= 0)
                throw new ArgumentException("Select a fixing job first.");

            if (string.IsNullOrWhiteSpace(problem))
                throw new ArgumentException(
                    "Problem description is required.");

            if (costPrice < 0m || finalPrice < 0m)
                throw new ArgumentException(
                    "Cost and final price cannot be negative.");

            using (SqlConnection con =
                new SqlConnection(ConnectionString))
            {
                con.Open();

                using (SqlTransaction tr =
                    con.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(@"
UPDATE dbo.tbMobileFix
SET problemDescription=@problem,
    costPrice=@cost,
    finalPrice=@price
WHERE id=@id", con, tr))
                        {
                            cmd.Parameters.Add(
                                "@problem",
                                SqlDbType.NVarChar,
                                500).Value = problem.Trim();

                            AddMoney(cmd, "@cost", costPrice);
                            AddMoney(cmd, "@price", finalPrice);

                            cmd.Parameters.Add(
                                "@id",
                                SqlDbType.Int).Value = fixId;

                            if (cmd.ExecuteNonQuery() == 0)
                            {
                                throw new InvalidOperationException(
                                    "The selected fixing job was not found.");
                            }
                        }

                        // A repair may already be waiting inside an open
                        // cashier transaction. Keep that pending line aligned
                        // with the newly edited repair price and cost.
                        using (SqlCommand cmd = new SqlCommand(@"
UPDATE dbo.tbCart
SET price=@price,
    costPrice=@cost
WHERE status='Pending'
  AND itemType='FIX'
  AND referenceId=@id", con, tr))
                        {
                            AddMoney(cmd, "@price", finalPrice);
                            AddMoney(cmd, "@cost", costPrice);

                            cmd.Parameters.Add(
                                "@id",
                                SqlDbType.Int).Value = fixId;

                            cmd.ExecuteNonQuery();
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
        }

        public static void UpdateFixStatus(int fixId, string status)
        {
            if (status != "Received" && status != "In Progress" && status != "Ready" && status != "Cancelled")
                throw new ArgumentException("Invalid fixing status.");

            using (SqlConnection con = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(@"
UPDATE dbo.tbMobileFix SET status=@status
WHERE id=@id AND status<>'Delivered'", con))
            {
                cmd.Parameters.Add("@status", SqlDbType.NVarChar, 30).Value = status;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = fixId;
                con.Open();
                if (cmd.ExecuteNonQuery() == 0) throw new InvalidOperationException("Delivered fixing jobs cannot be changed.");
            }
        }

        public static void AddFixToCart(string transNo, string cashier, string fixCode)
        {
            string normalized = NormalizeFixCodeInput(fixCode);
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlTransaction tr = con.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        int fixId;
                        string customer;
                        string model;
                        string status;
                        decimal cost;
                        decimal price;

                        using (SqlCommand cmd = new SqlCommand(@"
SELECT id,customerName,phoneModel,status,costPrice,finalPrice
FROM dbo.tbMobileFix WITH (UPDLOCK,ROWLOCK)
WHERE fixCode=@code", con, tr))
                        {
                            cmd.Parameters.Add("@code", SqlDbType.VarChar, 40).Value = normalized;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (!reader.Read()) throw new InvalidOperationException("Fix ID was not found.");
                                fixId = Convert.ToInt32(reader["id"]);
                                customer = reader["customerName"].ToString();
                                model = reader["phoneModel"].ToString();
                                status = reader["status"].ToString();
                                cost = Convert.ToDecimal(reader["costPrice"]);
                                price = Convert.ToDecimal(reader["finalPrice"]);
                            }
                        }

                        if (status == "Delivered") throw new InvalidOperationException("This phone was already delivered.");
                        if (status == "Cancelled") throw new InvalidOperationException("This fixing job is cancelled.");
                        if (status != "Ready") throw new InvalidOperationException("This phone is not ready. Mark it Ready first.");

                        using (SqlCommand cmd = new SqlCommand(@"
SELECT COUNT(*) FROM dbo.tbCart
WHERE status='Pending' AND itemType='FIX' AND referenceId=@fixId", con, tr))
                        {
                            cmd.Parameters.Add("@fixId", SqlDbType.Int).Value = fixId;
                            if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                                throw new InvalidOperationException("This fixing job is already in a pending transaction.");
                        }

                        using (SqlCommand cmd = new SqlCommand(@"
INSERT INTO dbo.tbCart
(transno,pcode,price,qty,sdate,status,cashier,itemType,referenceId,itemDescription,costPrice)
VALUES(@transno,@pcode,@price,1,GETDATE(),'Pending',@cashier,'FIX',@fixId,@description,@cost)", con, tr))
                        {
                            cmd.Parameters.Add("@transno", SqlDbType.VarChar, 50).Value = transNo;
                            cmd.Parameters.Add("@pcode", SqlDbType.VarChar, 50).Value = normalized;
                            AddMoney(cmd, "@price", price);
                            cmd.Parameters.Add("@cashier", SqlDbType.VarChar, 100).Value = cashier;
                            cmd.Parameters.Add("@fixId", SqlDbType.Int).Value = fixId;
                            cmd.Parameters.Add("@description", SqlDbType.NVarChar, 250).Value = "Phone Fix - " + customer + " - " + model + " - " + normalized;
                            AddMoney(cmd, "@cost", cost);
                            cmd.ExecuteNonQuery();
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
        }

        public static string GetCartItemType(int cartId)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(itemType,'PRODUCT') FROM dbo.tbCart WHERE id=@id", con))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = cartId;
                con.Open();
                object value = cmd.ExecuteScalar();
                return value == null || value == DBNull.Value ? ProductItem : value.ToString();
            }
        }

        public static DataTable GetDailyProfit(DateTime from, DateTime to, string cashier)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(@"
SELECT q.id,q.[Invoice],q.[Date],q.[Cashier],q.[Category],q.[Description],q.[Qty],q.[Sales],q.[Cost],q.[Profit]
FROM
(
    SELECT c.id,c.transno AS [Invoice],c.sdate AS [Date],c.cashier AS [Cashier],
           CASE WHEN ISNULL(c.itemType,'PRODUCT')='PRODUCT' THEN 'Products'
                WHEN c.itemType='FIX' THEN 'Mobile Fixing'
                ELSE COALESCE(ss.serviceType,s.serviceType,'Services') END AS [Category],
           COALESCE(NULLIF(c.itemDescription,''),p.pdesc,c.pcode) AS [Description],
           c.qty AS [Qty],c.total AS [Sales],
           CONVERT(DECIMAL(18,4),ISNULL(c.costPrice,0)*c.qty) AS [Cost],
           CONVERT(DECIMAL(18,4),c.total-(ISNULL(c.costPrice,0)*c.qty)) AS [Profit]
    FROM dbo.tbCart c
    LEFT JOIN dbo.tbProduct p ON p.pcode=c.pcode
    LEFT JOIN dbo.tbRechargeService s ON c.itemType='SERVICE' AND s.id=c.referenceId
    LEFT JOIN dbo.tbServiceSale ss ON c.itemType='SERVICE' AND ss.cartId=c.id
    WHERE c.status='Sold' AND c.sdate>=@from AND c.sdate<@to
      AND (@cashier='' OR c.cashier=@cashier)

    UNION ALL

    SELECT -d.id,d.transno,ISNULL(d.soldAt,d.appliedAt),d.appliedBy,'Transaction Discount',
           CASE WHEN ISNULL(d.reason,'')='' THEN 'Transaction discount' ELSE d.reason END,
           1,CONVERT(DECIMAL(18,4),-d.amount),CONVERT(DECIMAL(18,4),0),CONVERT(DECIMAL(18,4),-d.amount)
    FROM dbo.tbTransactionDiscount d
    WHERE d.status='Sold' AND ISNULL(d.soldAt,d.appliedAt)>=@from AND ISNULL(d.soldAt,d.appliedAt)<@to
      AND (@cashier='' OR d.appliedBy=@cashier)
) q
ORDER BY q.[Date] DESC,q.id DESC", con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.Add("@from", SqlDbType.DateTime).Value = from;
                cmd.Parameters.Add("@to", SqlDbType.DateTime).Value = to;
                cmd.Parameters.Add("@cashier", SqlDbType.VarChar, 100).Value = cashier ?? string.Empty;
                DataTable table = new DataTable();
                da.Fill(table);
                return table;
            }
        }

        public static void CompleteTransaction(string transNo, string cashier, DateTime soldDateTime,
            decimal cashUsd, decimal cashLbp, decimal changeUsd, decimal changeLbp, decimal exchangeRate)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlTransaction tr = con.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        DataTable lines = new DataTable();
                        using (SqlCommand cmd = new SqlCommand(@"
SELECT id,pcode,qty,price,ISNULL(itemType,'PRODUCT') itemType,referenceId,
       ISNULL(itemDescription,'') itemDescription,ISNULL(costPrice,0) costPrice,
       ISNULL(serviceDeductedPrice,costPrice) serviceDeductedPrice,
       ISNULL(serviceRetakeAmount,0) serviceRetakeAmount,serviceRetakeProviderId,
       ISNULL(serviceOperator,'') serviceOperator
FROM dbo.tbCart WITH (UPDLOCK,HOLDLOCK)
WHERE transno=@transno AND status='Pending'
ORDER BY id", con, tr))
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            cmd.Parameters.Add("@transno", SqlDbType.VarChar, 50).Value = transNo;
                            da.Fill(lines);
                        }

                        if (lines.Rows.Count == 0) throw new InvalidOperationException("The transaction has no pending items.");

                        decimal grossSubtotal = 0m;
                        decimal lineDiscount = 0m;
                        decimal cartSubtotal = 0m;
                        using (SqlCommand subtotalCmd = new SqlCommand(@"
SELECT ISNULL(SUM(price*qty),0),ISNULL(SUM(disc),0),ISNULL(SUM(total),0)
FROM dbo.tbCart
WHERE transno=@transno AND status='Pending'", con, tr))
                        {
                            subtotalCmd.Parameters.Add("@transno", SqlDbType.VarChar, 50).Value = transNo;
                            using (SqlDataReader subtotalReader = subtotalCmd.ExecuteReader())
                            {
                                if (subtotalReader.Read())
                                {
                                    grossSubtotal = Convert.ToDecimal(subtotalReader[0]);
                                    lineDiscount = Convert.ToDecimal(subtotalReader[1]);
                                    cartSubtotal = Convert.ToDecimal(subtotalReader[2]);
                                }
                            }
                        }

                        decimal transactionDiscount = 0m;
                        string discountReason = string.Empty;
                        using (SqlCommand discountCmd = new SqlCommand(@"
SELECT TOP 1 amount,ISNULL(reason,'') reason
FROM dbo.tbTransactionDiscount WITH (UPDLOCK,HOLDLOCK)
WHERE transno=@transno AND status='Pending'", con, tr))
                        {
                            discountCmd.Parameters.Add("@transno", SqlDbType.VarChar, 50).Value = transNo;
                            using (SqlDataReader discountReader = discountCmd.ExecuteReader())
                            {
                                if (discountReader.Read())
                                {
                                    transactionDiscount = Convert.ToDecimal(discountReader["amount"]);
                                    discountReason = Convert.ToString(discountReader["reason"]);
                                }
                            }
                        }
                        if (transactionDiscount < 0m || transactionDiscount >= cartSubtotal)
                            throw new InvalidOperationException("The transaction discount is invalid for the current cart total. Re-open Transaction Discount and save it again.");
                        if (exchangeRate <= 0m) throw new InvalidOperationException("The exchange rate is invalid.");

                        foreach (DataRow row in lines.Rows)
                        {
                            int cartId = Convert.ToInt32(row["id"]);
                            string type = row["itemType"].ToString();
                            int quantity = Convert.ToInt32(row["qty"]);

                            if (type == ProductItem)
                            {
                                using (SqlCommand cmd = new SqlCommand(@"
UPDATE dbo.tbProduct SET qty=qty-@qty
WHERE pcode=@pcode AND qty>=@qty", con, tr))
                                {
                                    cmd.Parameters.Add("@qty", SqlDbType.Int).Value = quantity;
                                    cmd.Parameters.Add("@pcode", SqlDbType.VarChar, 50).Value = row["pcode"].ToString();
                                    if (cmd.ExecuteNonQuery() == 0)
                                        throw new InvalidOperationException("Not enough stock for product " + row["pcode"] + ".");
                                }
                            }
                            else if (type == ServiceItem)
                            {
                                CompleteServiceLine(con, tr, cartId, Convert.ToInt32(row["referenceId"]), quantity,
                                    Convert.ToDecimal(row["costPrice"]), Convert.ToDecimal(row["serviceDeductedPrice"]),
                                    Convert.ToDecimal(row["price"]), Convert.ToDecimal(row["serviceRetakeAmount"]),
                                    row["serviceRetakeProviderId"] == DBNull.Value ? 0 : Convert.ToInt32(row["serviceRetakeProviderId"]),
                                    row["serviceOperator"].ToString(), transNo, cashier, soldDateTime);
                            }
                            else if (type == FixItem)
                            {
                                using (SqlCommand cmd = new SqlCommand(@"
UPDATE dbo.tbMobileFix
SET status='Delivered',deliveredAt=@date,deliveredBy=@cashier,deliveredTransno=@transno
WHERE id=@fixId AND status='Ready'", con, tr))
                                {
                                    cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = soldDateTime;
                                    cmd.Parameters.Add("@cashier", SqlDbType.VarChar, 100).Value = cashier;
                                    cmd.Parameters.Add("@transno", SqlDbType.VarChar, 50).Value = transNo;
                                    cmd.Parameters.Add("@fixId", SqlDbType.Int).Value = Convert.ToInt32(row["referenceId"]);
                                    if (cmd.ExecuteNonQuery() == 0)
                                        throw new InvalidOperationException("The fixing job " + row["pcode"] + " cannot be delivered.");
                                }
                            }
                        }

                        SaveReceiptSnapshot(con, tr, transNo, cashier, soldDateTime,
                            grossSubtotal, lineDiscount, transactionDiscount, discountReason,
                            Math.Max(0m, cartSubtotal - transactionDiscount),
                            cashUsd, cashLbp, changeUsd, changeLbp, exchangeRate);

                        using (SqlCommand cmd = new SqlCommand(@"
UPDATE dbo.tbCart SET status='Sold',sdate=@date
WHERE transno=@transno AND status='Pending'", con, tr))
                        {
                            cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = soldDateTime;
                            cmd.Parameters.Add("@transno", SqlDbType.VarChar, 50).Value = transNo;
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = new SqlCommand(@"
UPDATE dbo.tbTransactionDiscount
SET status='Sold',soldAt=@date
WHERE transno=@transno AND status='Pending'", con, tr))
                        {
                            cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = soldDateTime;
                            cmd.Parameters.Add("@transno", SqlDbType.VarChar, 50).Value = transNo;
                            cmd.ExecuteNonQuery();
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
        }

        private static void SaveReceiptSnapshot(SqlConnection con, SqlTransaction tr, string transNo, string cashier, DateTime soldDateTime,
            decimal subtotal, decimal lineDiscount, decimal transactionDiscount, string discountReason, decimal netTotal,
            decimal cashUsd, decimal cashLbp, decimal changeUsd, decimal changeLbp, decimal exchangeRate)
        {
            using (SqlCommand exists = new SqlCommand("SELECT COUNT(*) FROM dbo.tbReceiptHeader WHERE transno=@transno", con, tr))
            {
                exists.Parameters.Add("@transno", SqlDbType.VarChar, 50).Value = transNo;
                if (Convert.ToInt32(exists.ExecuteScalar()) > 0)
                    throw new InvalidOperationException("This receipt number was already settled.");
            }

            using (SqlCommand header = new SqlCommand(@"
INSERT INTO dbo.tbReceiptHeader
(transno,soldAt,cashier,subtotal,lineDiscount,transactionDiscount,discountReason,netTotal,
 cashUSD,cashLBP,changeUSD,changeLBP,exchangeRate,paymentRecorded,status)
VALUES(@transno,@soldAt,@cashier,@subtotal,@lineDiscount,@transactionDiscount,@discountReason,@netTotal,
 @cashUSD,@cashLBP,@changeUSD,@changeLBP,@exchangeRate,1,'Sold')", con, tr))
            {
                header.Parameters.Add("@transno", SqlDbType.VarChar, 50).Value = transNo;
                header.Parameters.Add("@soldAt", SqlDbType.DateTime).Value = soldDateTime;
                header.Parameters.Add("@cashier", SqlDbType.VarChar, 100).Value = cashier;
                AddMoney(header, "@subtotal", subtotal);
                AddMoney(header, "@lineDiscount", lineDiscount);
                AddMoney(header, "@transactionDiscount", transactionDiscount);
                header.Parameters.Add("@discountReason", SqlDbType.NVarChar, 250).Value = string.IsNullOrWhiteSpace(discountReason) ? (object)DBNull.Value : discountReason;
                AddMoney(header, "@netTotal", netTotal);
                AddMoney(header, "@cashUSD", Math.Max(0m, cashUsd));
                AddMoney(header, "@cashLBP", Math.Max(0m, cashLbp));
                AddMoney(header, "@changeUSD", Math.Max(0m, changeUsd));
                AddMoney(header, "@changeLBP", Math.Max(0m, changeLbp));
                AddMoney(header, "@exchangeRate", exchangeRate);
                header.ExecuteNonQuery();
            }

            using (SqlCommand lines = new SqlCommand(@"
INSERT INTO dbo.tbReceiptLine
(transno,[lineNo],cartId,pcode,description,itemType,price,qty,lineDiscount,lineTotal)
SELECT @transno,ROW_NUMBER() OVER(ORDER BY c.id),c.id,c.pcode,
       COALESCE(NULLIF(c.itemDescription,''),p.pdesc,c.pcode),ISNULL(c.itemType,'PRODUCT'),
       c.price,c.qty,ISNULL(c.disc,0),c.total
FROM dbo.tbCart c
LEFT JOIN dbo.tbProduct p ON p.pcode=c.pcode
WHERE c.transno=@transno AND c.status='Pending'", con, tr))
            {
                lines.Parameters.Add("@transno", SqlDbType.VarChar, 50).Value = transNo;
                if (lines.ExecuteNonQuery() == 0) throw new InvalidOperationException("The receipt lines could not be saved.");
            }
        }

        private static void CompleteServiceLine(SqlConnection con, SqlTransaction tr, int cartId, int serviceId,
            int quantity, decimal unitCost, decimal unitDeductedPrice, decimal unitPrice,
            decimal retakeAmount, int retakeProviderId,
            string operatorName, string transNo, string cashier, DateTime soldDateTime)
        {
            int providerId;
            string serviceName;
            string serviceType;
            decimal providerBalance;

            using (SqlCommand cmd = new SqlCommand(@"
SELECT s.providerId,s.serviceName,s.serviceType,p.currentBalance
FROM dbo.tbRechargeService s
INNER JOIN dbo.tbServiceProvider p WITH (UPDLOCK,ROWLOCK) ON p.id=s.providerId
WHERE s.id=@id AND s.isActive=1 AND p.isActive=1", con, tr))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = serviceId;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read()) throw new InvalidOperationException("A service in the cart is inactive.");
                    providerId = Convert.ToInt32(reader["providerId"]);
                    serviceName = reader["serviceName"].ToString();
                    serviceType = reader["serviceType"].ToString();
                    providerBalance = Convert.ToDecimal(reader["currentBalance"]);
                }
            }

            decimal totalProviderDeduction = unitDeductedPrice * quantity;
            decimal newBalance = providerBalance - totalProviderDeduction;
            if (newBalance < 0m)
                throw new InvalidOperationException("Provider balance is not enough for " + serviceName +
                    ". Required provider deduction: $" + totalProviderDeduction.ToString("0.00") + ".");

            UpdateProviderBalance(con, tr, providerId, newBalance);
            InsertProviderLedger(con, tr, providerId, serviceId, cartId, "SERVICE_SALE",
                -totalProviderDeduction, newBalance, cashier, serviceName + " / Invoice " + transNo);

            decimal totalRetake = retakeAmount * quantity;
            decimal retakeBalanceAfter = 0m;
            if (totalRetake > 0m)
            {
                if (serviceType != ValidityRecharge || retakeProviderId <= 0)
                    throw new InvalidOperationException("The validity retake destination is missing.");
                decimal oldRetakeBalance = ReadProviderBalance(con, tr, retakeProviderId);
                retakeBalanceAfter = oldRetakeBalance + totalRetake;
                UpdateProviderBalance(con, tr, retakeProviderId, retakeBalanceAfter);
                InsertProviderLedger(con, tr, retakeProviderId, serviceId, cartId, "VALIDITY_RETAKE",
                    totalRetake, retakeBalanceAfter, cashier,
                    serviceName + " / " + operatorName + " / Invoice " + transNo);
            }

            using (SqlCommand cmd = new SqlCommand(@"
INSERT INTO dbo.tbServiceSale
(serviceId,providerId,cartId,transno,qty,unitCost,unitDeductedPrice,unitPrice,serviceType,operatorName,
 retakeAmount,retakeProviderId,cashier,sdate,status)
VALUES(@serviceId,@providerId,@cartId,@transno,@qty,@cost,@deducted,@price,@type,@operator,
 @retake,@retakeProvider,@cashier,@date,'Sold')", con, tr))
            {
                cmd.Parameters.Add("@serviceId", SqlDbType.Int).Value = serviceId;
                cmd.Parameters.Add("@providerId", SqlDbType.Int).Value = providerId;
                cmd.Parameters.Add("@cartId", SqlDbType.Int).Value = cartId;
                cmd.Parameters.Add("@transno", SqlDbType.VarChar, 50).Value = transNo;
                cmd.Parameters.Add("@qty", SqlDbType.Int).Value = quantity;
                AddMoney(cmd, "@cost", unitCost);
                AddMoney(cmd, "@deducted", unitDeductedPrice);
                AddMoney(cmd, "@price", unitPrice);
                cmd.Parameters.Add("@type", SqlDbType.NVarChar, 40).Value = serviceType;
                cmd.Parameters.Add("@operator", SqlDbType.NVarChar, 20).Value = string.IsNullOrWhiteSpace(operatorName) ? (object)DBNull.Value : operatorName;
                AddMoney(cmd, "@retake", retakeAmount);
                cmd.Parameters.Add("@retakeProvider", SqlDbType.Int).Value = retakeProviderId > 0 ? (object)retakeProviderId : DBNull.Value;
                cmd.Parameters.Add("@cashier", SqlDbType.VarChar, 100).Value = cashier;
                cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = soldDateTime;
                cmd.ExecuteNonQuery();
            }
        }

        private static decimal ReadProviderBalance(SqlConnection con, SqlTransaction tr, int providerId)
        {
            using (SqlCommand cmd = new SqlCommand(@"
SELECT currentBalance FROM dbo.tbServiceProvider WITH (UPDLOCK,ROWLOCK)
WHERE id=@id", con, tr))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = providerId;
                object value = cmd.ExecuteScalar();
                if (value == null || value == DBNull.Value) throw new InvalidOperationException("Provider account was not found.");
                return Convert.ToDecimal(value);
            }
        }

        private static void UpdateProviderBalance(SqlConnection con, SqlTransaction tr, int providerId, decimal balance)
        {
            using (SqlCommand cmd = new SqlCommand("UPDATE dbo.tbServiceProvider SET currentBalance=@balance WHERE id=@id", con, tr))
            {
                AddMoney(cmd, "@balance", balance);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = providerId;
                if (cmd.ExecuteNonQuery() == 0) throw new InvalidOperationException("Provider account was not found.");
            }
        }

        private static int GetProviderIdByCode(SqlConnection con, SqlTransaction tr, string code)
        {
            using (SqlCommand cmd = new SqlCommand(@"
SELECT TOP 1 id FROM dbo.tbServiceProvider
WHERE providerCode=@code AND isActive=1", con, tr))
            {
                cmd.Parameters.Add("@code", SqlDbType.NVarChar, 30).Value = code;
                object value = cmd.ExecuteScalar();
                return value == null || value == DBNull.Value ? 0 : Convert.ToInt32(value);
            }
        }

        private static string GetOperatorFromProvider(int providerId)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(providerCode,'') FROM dbo.tbServiceProvider WHERE id=@id", con))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = providerId;
                con.Open();
                string code = Convert.ToString(cmd.ExecuteScalar());
                if (code == MtcLineProviderCode) return "MTC";
                if (code == AlfaLineProviderCode) return "Alfa";
                return string.Empty;
            }
        }

        private static string NormalizeOperator(string value)
        {
            string op = (value ?? string.Empty).Trim();
            if (op.Equals("MTC", StringComparison.OrdinalIgnoreCase) || op.Equals("Touch", StringComparison.OrdinalIgnoreCase)) return "MTC";
            if (op.Equals("Alfa", StringComparison.OrdinalIgnoreCase)) return "Alfa";
            return string.Empty;
        }

        private static string CreateProviderCode(string value)
        {
            string code = Regex.Replace((value ?? string.Empty).Trim().ToUpperInvariant(), @"[^A-Z0-9]+", "_").Trim('_');
            if (code.Length == 0) code = "PROVIDER";
            if (code.Length > 30) code = code.Substring(0, 30);
            return code;
        }

        private static void InsertProviderLedger(SqlConnection con, SqlTransaction tr, int providerId,
            int? serviceId, int? cartId, string movementType, decimal amount, decimal balanceAfter,
            string username, string notes)
        {
            using (SqlCommand cmd = new SqlCommand(@"
INSERT INTO dbo.tbProviderLedger
(providerId,serviceId,cartId,movementType,amount,balanceAfter,username,sdate,notes)
VALUES(@providerId,@serviceId,@cartId,@movementType,@amount,@balanceAfter,@username,GETDATE(),@notes)", con, tr))
            {
                cmd.Parameters.Add("@providerId", SqlDbType.Int).Value = providerId;
                cmd.Parameters.Add("@serviceId", SqlDbType.Int).Value = serviceId.HasValue ? (object)serviceId.Value : DBNull.Value;
                cmd.Parameters.Add("@cartId", SqlDbType.Int).Value = cartId.HasValue ? (object)cartId.Value : DBNull.Value;
                cmd.Parameters.Add("@movementType", SqlDbType.NVarChar, 40).Value = movementType;
                AddMoney(cmd, "@amount", amount);
                AddMoney(cmd, "@balanceAfter", balanceAfter);
                cmd.Parameters.Add("@username", SqlDbType.VarChar, 100).Value = string.IsNullOrWhiteSpace(username) ? "system" : username;
                cmd.Parameters.Add("@notes", SqlDbType.NVarChar, 500).Value = string.IsNullOrWhiteSpace(notes) ? (object)DBNull.Value : notes;
                cmd.ExecuteNonQuery();
            }
        }

        private static void AddMoney(SqlCommand cmd, string name, decimal value)
        {
            SqlParameter parameter = cmd.Parameters.Add(name, SqlDbType.Decimal);
            parameter.Precision = 18;
            parameter.Scale = 4;
            parameter.Value = value;
        }
    }
}
