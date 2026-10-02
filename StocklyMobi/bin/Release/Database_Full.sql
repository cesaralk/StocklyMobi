/*
    StocklyMobi Mobile Shop - Step 4.1 Full Database Installation / Upgrade Script
    Technology: SQL Server / SQL Server Express
    Database: Stocklymobi

    This script is idempotent. It can create a fresh database or upgrade the
    original StocklyMobi database while preserving its core POS data.
*/
USE [master];
GO

IF DB_ID(N'Stocklymobi') IS NULL
BEGIN
    CREATE DATABASE [Stocklymobi];
END;
GO

USE [Stocklymobi];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

/* =========================
   CORE LOOKUP TABLES
   ========================= */
IF OBJECT_ID(N'dbo.tbBrand', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tbBrand
    (
        id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_tbBrand PRIMARY KEY,
        brand NVARCHAR(100) NOT NULL
    );
END;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.tbBrand') AND name=N'UX_tbBrand_brand')
   AND NOT EXISTS (SELECT brand FROM dbo.tbBrand GROUP BY brand HAVING COUNT(*)>1)
    CREATE UNIQUE INDEX UX_tbBrand_brand ON dbo.tbBrand(brand);
GO

IF OBJECT_ID(N'dbo.tbCategory', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tbCategory
    (
        id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_tbCategory PRIMARY KEY,
        category NVARCHAR(100) NOT NULL
    );
END;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.tbCategory') AND name=N'UX_tbCategory_category')
   AND NOT EXISTS (SELECT category FROM dbo.tbCategory GROUP BY category HAVING COUNT(*)>1)
    CREATE UNIQUE INDEX UX_tbCategory_category ON dbo.tbCategory(category);
GO

IF OBJECT_ID(N'dbo.tbSupplier', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tbSupplier
    (
        id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_tbSupplier PRIMARY KEY,
        supplier NVARCHAR(150) NOT NULL,
        address NVARCHAR(250) NULL,
        contactperson NVARCHAR(150) NULL,
        phone NVARCHAR(50) NULL,
        email NVARCHAR(150) NULL,
        fax NVARCHAR(50) NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.tbStore', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tbStore
    (
        id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_tbStore PRIMARY KEY,
        store NVARCHAR(150) NOT NULL,
        address NVARCHAR(300) NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.tbUser', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tbUser
    (
        username VARCHAR(50) NOT NULL CONSTRAINT PK_tbUser PRIMARY KEY,
        password VARCHAR(100) NOT NULL,
        role VARCHAR(30) NOT NULL,
        name NVARCHAR(150) NOT NULL,
        isactivate BIT NOT NULL CONSTRAINT DF_tbUser_isactivate DEFAULT(1)
    );
END;
GO
IF COL_LENGTH(N'dbo.tbUser', N'isactivate') IS NULL
    ALTER TABLE dbo.tbUser ADD isactivate BIT NOT NULL CONSTRAINT DF_tbUser_isactivate_upgrade DEFAULT(1) WITH VALUES;
GO

/* =========================
   PRODUCT / INVENTORY TABLES
   ========================= */
IF OBJECT_ID(N'dbo.tbProduct', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tbProduct
    (
        id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_tbProduct PRIMARY KEY,
        pcode VARCHAR(50) NOT NULL,
        barcode VARCHAR(100) NOT NULL,
        pdesc NVARCHAR(300) NOT NULL,
        bid INT NOT NULL,
        cid INT NOT NULL,
        CostPrice DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbProduct_CostPrice DEFAULT(0),
        Markup DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbProduct_Markup DEFAULT(0),
        price DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbProduct_price DEFAULT(0),
        qty INT NOT NULL CONSTRAINT DF_tbProduct_qty DEFAULT(0),
        reorder INT NOT NULL CONSTRAINT DF_tbProduct_reorder DEFAULT(0),
        isActive BIT NOT NULL CONSTRAINT DF_tbProduct_isActive DEFAULT(1)
    );
END;
GO
IF COL_LENGTH(N'dbo.tbProduct', N'CostPrice') IS NULL
    ALTER TABLE dbo.tbProduct ADD CostPrice DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbProduct_CostPrice_upgrade DEFAULT(0) WITH VALUES;
IF COL_LENGTH(N'dbo.tbProduct', N'Markup') IS NULL
    ALTER TABLE dbo.tbProduct ADD Markup DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbProduct_Markup_upgrade DEFAULT(0) WITH VALUES;
IF COL_LENGTH(N'dbo.tbProduct', N'qty') IS NULL
    ALTER TABLE dbo.tbProduct ADD qty INT NOT NULL CONSTRAINT DF_tbProduct_qty_upgrade DEFAULT(0) WITH VALUES;
IF COL_LENGTH(N'dbo.tbProduct', N'reorder') IS NULL
    ALTER TABLE dbo.tbProduct ADD reorder INT NOT NULL CONSTRAINT DF_tbProduct_reorder_upgrade DEFAULT(0) WITH VALUES;
IF COL_LENGTH(N'dbo.tbProduct', N'isActive') IS NULL
    ALTER TABLE dbo.tbProduct ADD isActive BIT NOT NULL CONSTRAINT DF_tbProduct_isActive_upgrade DEFAULT(1) WITH VALUES;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.tbProduct') AND name=N'UX_tbProduct_pcode')
   AND NOT EXISTS (SELECT pcode FROM dbo.tbProduct GROUP BY pcode HAVING COUNT(*)>1)
    CREATE UNIQUE INDEX UX_tbProduct_pcode ON dbo.tbProduct(pcode);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.tbProduct') AND name=N'UX_tbProduct_barcode')
   AND NOT EXISTS (SELECT barcode FROM dbo.tbProduct GROUP BY barcode HAVING COUNT(*)>1)
    CREATE UNIQUE INDEX UX_tbProduct_barcode ON dbo.tbProduct(barcode);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.tbProduct') AND name=N'IX_tbProduct_description')
    CREATE INDEX IX_tbProduct_description ON dbo.tbProduct(pdesc) INCLUDE(pcode,barcode,price,qty,CostPrice,Markup);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.tbProduct') AND name=N'IX_tbProduct_active_description')
    CREATE INDEX IX_tbProduct_active_description ON dbo.tbProduct(isActive,pdesc) INCLUDE(pcode,barcode,price,qty,CostPrice,Markup,reorder);
GO

IF OBJECT_ID(N'dbo.tbStockIn', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tbStockIn
    (
        id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_tbStockIn PRIMARY KEY,
        refno VARCHAR(50) NOT NULL,
        pcode VARCHAR(50) NOT NULL,
        qty INT NOT NULL,
        sdate DATETIME NOT NULL CONSTRAINT DF_tbStockIn_sdate DEFAULT(GETDATE()),
        stockinby VARCHAR(100) NULL,
        supplierid INT NULL,
        status VARCHAR(30) NOT NULL CONSTRAINT DF_tbStockIn_status DEFAULT('Pending'),
        CostPrice DECIMAL(18,4) NULL,
        Markup DECIMAL(18,4) NULL,
        Price DECIMAL(18,4) NULL,
        invoiceId INT NULL
    );
END;
GO
IF COL_LENGTH(N'dbo.tbStockIn', N'CostPrice') IS NULL ALTER TABLE dbo.tbStockIn ADD CostPrice DECIMAL(18,4) NULL;
IF COL_LENGTH(N'dbo.tbStockIn', N'Markup') IS NULL ALTER TABLE dbo.tbStockIn ADD Markup DECIMAL(18,4) NULL;
IF COL_LENGTH(N'dbo.tbStockIn', N'Price') IS NULL ALTER TABLE dbo.tbStockIn ADD Price DECIMAL(18,4) NULL;
IF COL_LENGTH(N'dbo.tbStockIn', N'invoiceId') IS NULL ALTER TABLE dbo.tbStockIn ADD invoiceId INT NULL;
UPDATE dbo.tbStockIn SET refno='LEGACY-'+CONVERT(VARCHAR(20),id) WHERE refno IS NULL OR LTRIM(RTRIM(refno))='';
GO

IF OBJECT_ID(N'dbo.tbStockInvoice', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tbStockInvoice
    (
        id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_tbStockInvoice PRIMARY KEY,
        invoiceNo VARCHAR(50) NOT NULL,
        supplierId INT NULL,
        invoiceDate DATETIME NOT NULL,
        createdAt DATETIME NOT NULL CONSTRAINT DF_tbStockInvoice_createdAt DEFAULT(GETDATE()),
        createdBy VARCHAR(100) NULL,
        totalCost DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbStockInvoice_totalCost DEFAULT(0),
        status VARCHAR(20) NOT NULL CONSTRAINT DF_tbStockInvoice_status DEFAULT('Posted'),
        notes NVARCHAR(500) NULL
    );
END;
GO
IF COL_LENGTH(N'dbo.tbStockInvoice', N'invoiceNo') IS NULL ALTER TABLE dbo.tbStockInvoice ADD invoiceNo VARCHAR(50) NULL;
IF COL_LENGTH(N'dbo.tbStockInvoice', N'supplierId') IS NULL ALTER TABLE dbo.tbStockInvoice ADD supplierId INT NULL;
IF COL_LENGTH(N'dbo.tbStockInvoice', N'invoiceDate') IS NULL ALTER TABLE dbo.tbStockInvoice ADD invoiceDate DATETIME NULL;
IF COL_LENGTH(N'dbo.tbStockInvoice', N'createdAt') IS NULL ALTER TABLE dbo.tbStockInvoice ADD createdAt DATETIME NULL;
IF COL_LENGTH(N'dbo.tbStockInvoice', N'createdBy') IS NULL ALTER TABLE dbo.tbStockInvoice ADD createdBy VARCHAR(100) NULL;
IF COL_LENGTH(N'dbo.tbStockInvoice', N'totalCost') IS NULL ALTER TABLE dbo.tbStockInvoice ADD totalCost DECIMAL(18,4) NULL;
IF COL_LENGTH(N'dbo.tbStockInvoice', N'status') IS NULL ALTER TABLE dbo.tbStockInvoice ADD status VARCHAR(20) NULL;
IF COL_LENGTH(N'dbo.tbStockInvoice', N'notes') IS NULL ALTER TABLE dbo.tbStockInvoice ADD notes NVARCHAR(500) NULL;
UPDATE dbo.tbStockInvoice
SET invoiceNo=COALESCE(NULLIF(LTRIM(RTRIM(invoiceNo)),''),'LEGACY-INVOICE-'+CONVERT(VARCHAR(20),id)),
    invoiceDate=ISNULL(invoiceDate,GETDATE()),createdAt=ISNULL(createdAt,ISNULL(invoiceDate,GETDATE())),
    totalCost=ISNULL(totalCost,0),status=COALESCE(NULLIF(status,''),'Posted');
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.tbStockInvoice') AND name=N'invoiceNo' AND is_nullable=1)
    ALTER TABLE dbo.tbStockInvoice ALTER COLUMN invoiceNo VARCHAR(50) NOT NULL;
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.tbStockInvoice') AND name=N'invoiceDate' AND is_nullable=1)
    ALTER TABLE dbo.tbStockInvoice ALTER COLUMN invoiceDate DATETIME NOT NULL;
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.tbStockInvoice') AND name=N'createdAt' AND is_nullable=1)
    ALTER TABLE dbo.tbStockInvoice ALTER COLUMN createdAt DATETIME NOT NULL;
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.tbStockInvoice') AND name=N'totalCost' AND is_nullable=1)
    ALTER TABLE dbo.tbStockInvoice ALTER COLUMN totalCost DECIMAL(18,4) NOT NULL;
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.tbStockInvoice') AND name=N'status' AND is_nullable=1)
    ALTER TABLE dbo.tbStockInvoice ALTER COLUMN status VARCHAR(20) NOT NULL;
GO

INSERT INTO dbo.tbStockInvoice(invoiceNo,supplierId,invoiceDate,createdAt,createdBy,totalCost,status)
SELECT si.refno,MAX(si.supplierid),MIN(si.sdate),MIN(si.sdate),MAX(si.stockinby),
       SUM(CONVERT(DECIMAL(18,4),ISNULL(si.qty,0))*ISNULL(si.CostPrice,0)),'Posted'
FROM dbo.tbStockIn si
WHERE si.status='Done' AND ISNULL(si.refno,'')<>''
  AND NOT EXISTS(SELECT 1 FROM dbo.tbStockInvoice i WHERE i.invoiceNo=si.refno)
GROUP BY si.refno;
GO
UPDATE si SET invoiceId=i.id
FROM dbo.tbStockIn si
INNER JOIN dbo.tbStockInvoice i ON i.invoiceNo=si.refno
WHERE si.invoiceId IS NULL;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.tbStockInvoice') AND name=N'UX_tbStockInvoice_invoiceNo')
   AND NOT EXISTS (SELECT invoiceNo FROM dbo.tbStockInvoice GROUP BY invoiceNo HAVING COUNT(*)>1)
    CREATE UNIQUE INDEX UX_tbStockInvoice_invoiceNo ON dbo.tbStockInvoice(invoiceNo);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.tbStockInvoice') AND name=N'IX_tbStockInvoice_date')
    CREATE INDEX IX_tbStockInvoice_date ON dbo.tbStockInvoice(invoiceDate DESC) INCLUDE(invoiceNo,supplierId,totalCost,createdBy,status);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.tbStockIn') AND name=N'IX_tbStockIn_refno_status')
    CREATE INDEX IX_tbStockIn_refno_status ON dbo.tbStockIn(refno,status) INCLUDE(pcode,qty,sdate,supplierid,CostPrice,Markup,Price,invoiceId);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.tbStockIn') AND name=N'IX_tbStockIn_invoiceId')
    CREATE INDEX IX_tbStockIn_invoiceId ON dbo.tbStockIn(invoiceId) INCLUDE(pcode,qty,CostPrice,Markup,Price);
GO

IF OBJECT_ID(N'dbo.tbAdjustment', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tbAdjustment
    (
        id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_tbAdjustment PRIMARY KEY,
        referenceno VARCHAR(50) NULL,
        pcode VARCHAR(50) NOT NULL,
        qty INT NOT NULL,
        action VARCHAR(50) NOT NULL,
        remarks NVARCHAR(500) NULL,
        sdate DATETIME NOT NULL CONSTRAINT DF_tbAdjustment_sdate DEFAULT(GETDATE()),
        [user] VARCHAR(100) NULL
    );
END;
GO

/* =========================
   CART / SALES / RETURNS
   ========================= */
IF OBJECT_ID(N'dbo.tbCart', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tbCart
    (
        id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_tbCart PRIMARY KEY,
        transno VARCHAR(50) NOT NULL,
        pcode VARCHAR(50) NOT NULL,
        price DECIMAL(18,4) NOT NULL,
        qty INT NOT NULL,
        disc_percent DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbCart_disc_percent DEFAULT(0),
        disc DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbCart_disc DEFAULT(0),
        total DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbCart_total DEFAULT(0),
        sdate DATETIME NOT NULL CONSTRAINT DF_tbCart_sdate DEFAULT(GETDATE()),
        status VARCHAR(30) NOT NULL CONSTRAINT DF_tbCart_status DEFAULT('Pending'),
        cashier VARCHAR(100) NULL,
        total_ll DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbCart_total_ll DEFAULT(0),
        itemType VARCHAR(20) NOT NULL CONSTRAINT DF_tbCart_itemType DEFAULT('PRODUCT'),
        referenceId INT NULL,
        itemDescription NVARCHAR(250) NULL,
        costPrice DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbCart_costPrice DEFAULT(0),
        serviceDeductedPrice DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbCart_serviceDeductedPrice DEFAULT(0),
        serviceRetakeAmount DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbCart_serviceRetakeAmount DEFAULT(0),
        serviceRetakeProviderId INT NULL,
        serviceOperator NVARCHAR(20) NULL
    );
END;
GO
IF COL_LENGTH(N'dbo.tbCart', N'disc_percent') IS NULL ALTER TABLE dbo.tbCart ADD disc_percent DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbCart_disc_percent_upgrade DEFAULT(0) WITH VALUES;
IF COL_LENGTH(N'dbo.tbCart', N'disc') IS NULL ALTER TABLE dbo.tbCart ADD disc DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbCart_disc_upgrade DEFAULT(0) WITH VALUES;
IF COL_LENGTH(N'dbo.tbCart', N'total') IS NULL ALTER TABLE dbo.tbCart ADD total DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbCart_total_upgrade DEFAULT(0) WITH VALUES;
IF COL_LENGTH(N'dbo.tbCart', N'total_ll') IS NULL ALTER TABLE dbo.tbCart ADD total_ll DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbCart_total_ll_upgrade DEFAULT(0) WITH VALUES;
IF COL_LENGTH(N'dbo.tbCart', N'itemType') IS NULL ALTER TABLE dbo.tbCart ADD itemType VARCHAR(20) NOT NULL CONSTRAINT DF_tbCart_itemType_upgrade DEFAULT('PRODUCT') WITH VALUES;
IF COL_LENGTH(N'dbo.tbCart', N'referenceId') IS NULL ALTER TABLE dbo.tbCart ADD referenceId INT NULL;
IF COL_LENGTH(N'dbo.tbCart', N'itemDescription') IS NULL ALTER TABLE dbo.tbCart ADD itemDescription NVARCHAR(250) NULL;
IF COL_LENGTH(N'dbo.tbCart', N'costPrice') IS NULL ALTER TABLE dbo.tbCart ADD costPrice DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbCart_costPrice_upgrade DEFAULT(0) WITH VALUES;
IF COL_LENGTH(N'dbo.tbCart', N'serviceDeductedPrice') IS NULL
BEGIN
    ALTER TABLE dbo.tbCart ADD serviceDeductedPrice DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbCart_serviceDeductedPrice_upgrade DEFAULT(0) WITH VALUES;
    EXEC(N'UPDATE dbo.tbCart SET serviceDeductedPrice=costPrice WHERE itemType=''SERVICE'';');
END;
IF COL_LENGTH(N'dbo.tbCart', N'serviceRetakeAmount') IS NULL ALTER TABLE dbo.tbCart ADD serviceRetakeAmount DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbCart_serviceRetakeAmount_upgrade DEFAULT(0) WITH VALUES;
IF COL_LENGTH(N'dbo.tbCart', N'serviceRetakeProviderId') IS NULL ALTER TABLE dbo.tbCart ADD serviceRetakeProviderId INT NULL;
IF COL_LENGTH(N'dbo.tbCart', N'serviceOperator') IS NULL ALTER TABLE dbo.tbCart ADD serviceOperator NVARCHAR(20) NULL;
GO

UPDATE c
SET itemType = ISNULL(NULLIF(itemType,''),'PRODUCT'),
    itemDescription = COALESCE(NULLIF(itemDescription,''), p.pdesc, c.pcode),
    costPrice = CASE WHEN ISNULL(c.costPrice,0)=0 THEN ISNULL(p.CostPrice,0) ELSE c.costPrice END
FROM dbo.tbCart c
LEFT JOIN dbo.tbProduct p ON p.pcode=c.pcode;
GO

IF OBJECT_ID(N'dbo.trg_tbCart_Recalculate', N'TR') IS NOT NULL
    DROP TRIGGER dbo.trg_tbCart_Recalculate;
GO
CREATE TRIGGER dbo.trg_tbCart_Recalculate
ON dbo.tbCart
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF TRIGGER_NESTLEVEL() > 1 RETURN;

    UPDATE c
       SET disc = ROUND((c.price * c.qty) * (ISNULL(c.disc_percent,0) / 100.0), 4),
           total = ROUND((c.price * c.qty) - ((c.price * c.qty) * (ISNULL(c.disc_percent,0) / 100.0)), 4)
    FROM dbo.tbCart c
    INNER JOIN inserted i ON i.id=c.id;
END;
GO

UPDATE dbo.tbCart
SET disc = ROUND((price*qty)*(ISNULL(disc_percent,0)/100.0),4),
    total = ROUND((price*qty)-((price*qty)*(ISNULL(disc_percent,0)/100.0)),4);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.tbCart') AND name=N'IX_tbCart_transno_status')
    CREATE INDEX IX_tbCart_transno_status ON dbo.tbCart(transno,status) INCLUDE(id,pcode,price,qty,disc,total,sdate,cashier,itemType,referenceId,itemDescription,costPrice);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.tbCart') AND name=N'IX_tbCart_pcode_status')
    CREATE INDEX IX_tbCart_pcode_status ON dbo.tbCart(pcode,status) INCLUDE(qty,transno,sdate);
GO

IF OBJECT_ID(N'dbo.tbTransactionDiscount', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tbTransactionDiscount
    (
        id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_tbTransactionDiscount PRIMARY KEY,
        transno VARCHAR(50) NOT NULL,
        amount DECIMAL(18,4) NOT NULL,
        reason NVARCHAR(250) NULL,
        appliedBy VARCHAR(100) NULL,
        appliedAt DATETIME NOT NULL CONSTRAINT DF_tbTransactionDiscount_appliedAt DEFAULT(GETDATE()),
        status VARCHAR(20) NOT NULL CONSTRAINT DF_tbTransactionDiscount_status DEFAULT('Pending'),
        soldAt DATETIME NULL,
        cancelledAt DATETIME NULL,
        cancelledBy VARCHAR(100) NULL
    );
END;
GO
IF COL_LENGTH(N'dbo.tbTransactionDiscount', N'transno') IS NULL ALTER TABLE dbo.tbTransactionDiscount ADD transno VARCHAR(50) NULL;
IF COL_LENGTH(N'dbo.tbTransactionDiscount', N'amount') IS NULL ALTER TABLE dbo.tbTransactionDiscount ADD amount DECIMAL(18,4) NULL;
IF COL_LENGTH(N'dbo.tbTransactionDiscount', N'reason') IS NULL ALTER TABLE dbo.tbTransactionDiscount ADD reason NVARCHAR(250) NULL;
IF COL_LENGTH(N'dbo.tbTransactionDiscount', N'appliedBy') IS NULL ALTER TABLE dbo.tbTransactionDiscount ADD appliedBy VARCHAR(100) NULL;
IF COL_LENGTH(N'dbo.tbTransactionDiscount', N'appliedAt') IS NULL ALTER TABLE dbo.tbTransactionDiscount ADD appliedAt DATETIME NULL;
IF COL_LENGTH(N'dbo.tbTransactionDiscount', N'status') IS NULL ALTER TABLE dbo.tbTransactionDiscount ADD status VARCHAR(20) NULL;
IF COL_LENGTH(N'dbo.tbTransactionDiscount', N'soldAt') IS NULL ALTER TABLE dbo.tbTransactionDiscount ADD soldAt DATETIME NULL;
IF COL_LENGTH(N'dbo.tbTransactionDiscount', N'cancelledAt') IS NULL ALTER TABLE dbo.tbTransactionDiscount ADD cancelledAt DATETIME NULL;
IF COL_LENGTH(N'dbo.tbTransactionDiscount', N'cancelledBy') IS NULL ALTER TABLE dbo.tbTransactionDiscount ADD cancelledBy VARCHAR(100) NULL;
UPDATE dbo.tbTransactionDiscount
SET transno=COALESCE(NULLIF(LTRIM(RTRIM(transno)),''),'LEGACY-DISCOUNT-'+CONVERT(VARCHAR(20),id)),
    amount=ISNULL(amount,0),appliedAt=ISNULL(appliedAt,GETDATE()),status=COALESCE(NULLIF(status,''),'Pending');
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.tbTransactionDiscount') AND name=N'transno' AND is_nullable=1)
    ALTER TABLE dbo.tbTransactionDiscount ALTER COLUMN transno VARCHAR(50) NOT NULL;
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.tbTransactionDiscount') AND name=N'amount' AND is_nullable=1)
    ALTER TABLE dbo.tbTransactionDiscount ALTER COLUMN amount DECIMAL(18,4) NOT NULL;
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.tbTransactionDiscount') AND name=N'appliedAt' AND is_nullable=1)
    ALTER TABLE dbo.tbTransactionDiscount ALTER COLUMN appliedAt DATETIME NOT NULL;
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.tbTransactionDiscount') AND name=N'status' AND is_nullable=1)
    ALTER TABLE dbo.tbTransactionDiscount ALTER COLUMN status VARCHAR(20) NOT NULL;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.tbTransactionDiscount') AND name=N'UX_tbTransactionDiscount_transno')
   AND NOT EXISTS (SELECT transno FROM dbo.tbTransactionDiscount GROUP BY transno HAVING COUNT(*)>1)
    CREATE UNIQUE INDEX UX_tbTransactionDiscount_transno ON dbo.tbTransactionDiscount(transno);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.tbTransactionDiscount') AND name=N'IX_tbTransactionDiscount_status_date')
    CREATE INDEX IX_tbTransactionDiscount_status_date ON dbo.tbTransactionDiscount(status,appliedAt DESC) INCLUDE(transno,amount,appliedBy,soldAt,cancelledAt);
GO


/* =========================
   RECEIPT SNAPSHOTS / REPRINTING
   ========================= */
IF OBJECT_ID(N'dbo.tbReceiptHeader', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tbReceiptHeader
    (
        transno VARCHAR(50) NOT NULL CONSTRAINT PK_tbReceiptHeader PRIMARY KEY,
        soldAt DATETIME NOT NULL,
        cashier VARCHAR(100) NULL,
        subtotal DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbReceiptHeader_subtotal DEFAULT(0),
        lineDiscount DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbReceiptHeader_lineDiscount DEFAULT(0),
        transactionDiscount DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbReceiptHeader_transactionDiscount DEFAULT(0),
        discountReason NVARCHAR(250) NULL,
        netTotal DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbReceiptHeader_netTotal DEFAULT(0),
        cashUSD DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbReceiptHeader_cashUSD DEFAULT(0),
        cashLBP DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbReceiptHeader_cashLBP DEFAULT(0),
        changeUSD DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbReceiptHeader_changeUSD DEFAULT(0),
        changeLBP DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbReceiptHeader_changeLBP DEFAULT(0),
        exchangeRate DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbReceiptHeader_exchangeRate DEFAULT(89500),
        paymentRecorded BIT NOT NULL CONSTRAINT DF_tbReceiptHeader_paymentRecorded DEFAULT(0),
        status VARCHAR(30) NOT NULL CONSTRAINT DF_tbReceiptHeader_status DEFAULT('Sold')
    );
END;
GO
IF COL_LENGTH(N'dbo.tbReceiptHeader', N'soldAt') IS NULL ALTER TABLE dbo.tbReceiptHeader ADD soldAt DATETIME NULL;
IF COL_LENGTH(N'dbo.tbReceiptHeader', N'cashier') IS NULL ALTER TABLE dbo.tbReceiptHeader ADD cashier VARCHAR(100) NULL;
IF COL_LENGTH(N'dbo.tbReceiptHeader', N'subtotal') IS NULL ALTER TABLE dbo.tbReceiptHeader ADD subtotal DECIMAL(18,4) NULL;
IF COL_LENGTH(N'dbo.tbReceiptHeader', N'lineDiscount') IS NULL ALTER TABLE dbo.tbReceiptHeader ADD lineDiscount DECIMAL(18,4) NULL;
IF COL_LENGTH(N'dbo.tbReceiptHeader', N'transactionDiscount') IS NULL ALTER TABLE dbo.tbReceiptHeader ADD transactionDiscount DECIMAL(18,4) NULL;
IF COL_LENGTH(N'dbo.tbReceiptHeader', N'discountReason') IS NULL ALTER TABLE dbo.tbReceiptHeader ADD discountReason NVARCHAR(250) NULL;
IF COL_LENGTH(N'dbo.tbReceiptHeader', N'netTotal') IS NULL ALTER TABLE dbo.tbReceiptHeader ADD netTotal DECIMAL(18,4) NULL;
IF COL_LENGTH(N'dbo.tbReceiptHeader', N'cashUSD') IS NULL ALTER TABLE dbo.tbReceiptHeader ADD cashUSD DECIMAL(18,4) NULL;
IF COL_LENGTH(N'dbo.tbReceiptHeader', N'cashLBP') IS NULL ALTER TABLE dbo.tbReceiptHeader ADD cashLBP DECIMAL(18,4) NULL;
IF COL_LENGTH(N'dbo.tbReceiptHeader', N'changeUSD') IS NULL ALTER TABLE dbo.tbReceiptHeader ADD changeUSD DECIMAL(18,4) NULL;
IF COL_LENGTH(N'dbo.tbReceiptHeader', N'changeLBP') IS NULL ALTER TABLE dbo.tbReceiptHeader ADD changeLBP DECIMAL(18,4) NULL;
IF COL_LENGTH(N'dbo.tbReceiptHeader', N'exchangeRate') IS NULL ALTER TABLE dbo.tbReceiptHeader ADD exchangeRate DECIMAL(18,4) NULL;
IF COL_LENGTH(N'dbo.tbReceiptHeader', N'paymentRecorded') IS NULL ALTER TABLE dbo.tbReceiptHeader ADD paymentRecorded BIT NULL;
IF COL_LENGTH(N'dbo.tbReceiptHeader', N'status') IS NULL ALTER TABLE dbo.tbReceiptHeader ADD status VARCHAR(30) NULL;
UPDATE dbo.tbReceiptHeader
SET soldAt=ISNULL(soldAt,GETDATE()),subtotal=ISNULL(subtotal,0),lineDiscount=ISNULL(lineDiscount,0),
    transactionDiscount=ISNULL(transactionDiscount,0),netTotal=ISNULL(netTotal,0),cashUSD=ISNULL(cashUSD,0),
    cashLBP=ISNULL(cashLBP,0),changeUSD=ISNULL(changeUSD,0),changeLBP=ISNULL(changeLBP,0),
    exchangeRate=CASE WHEN ISNULL(exchangeRate,0)<=0 THEN 89500 ELSE exchangeRate END,
    paymentRecorded=ISNULL(paymentRecorded,0),status=COALESCE(NULLIF(status,''),'Sold');
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.tbReceiptHeader') AND name=N'soldAt' AND is_nullable=1)
    ALTER TABLE dbo.tbReceiptHeader ALTER COLUMN soldAt DATETIME NOT NULL;
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.tbReceiptHeader') AND name=N'subtotal' AND is_nullable=1)
    ALTER TABLE dbo.tbReceiptHeader ALTER COLUMN subtotal DECIMAL(18,4) NOT NULL;
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.tbReceiptHeader') AND name=N'lineDiscount' AND is_nullable=1)
    ALTER TABLE dbo.tbReceiptHeader ALTER COLUMN lineDiscount DECIMAL(18,4) NOT NULL;
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.tbReceiptHeader') AND name=N'transactionDiscount' AND is_nullable=1)
    ALTER TABLE dbo.tbReceiptHeader ALTER COLUMN transactionDiscount DECIMAL(18,4) NOT NULL;
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.tbReceiptHeader') AND name=N'netTotal' AND is_nullable=1)
    ALTER TABLE dbo.tbReceiptHeader ALTER COLUMN netTotal DECIMAL(18,4) NOT NULL;
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.tbReceiptHeader') AND name=N'cashUSD' AND is_nullable=1)
    ALTER TABLE dbo.tbReceiptHeader ALTER COLUMN cashUSD DECIMAL(18,4) NOT NULL;
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.tbReceiptHeader') AND name=N'cashLBP' AND is_nullable=1)
    ALTER TABLE dbo.tbReceiptHeader ALTER COLUMN cashLBP DECIMAL(18,4) NOT NULL;
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.tbReceiptHeader') AND name=N'changeUSD' AND is_nullable=1)
    ALTER TABLE dbo.tbReceiptHeader ALTER COLUMN changeUSD DECIMAL(18,4) NOT NULL;
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.tbReceiptHeader') AND name=N'changeLBP' AND is_nullable=1)
    ALTER TABLE dbo.tbReceiptHeader ALTER COLUMN changeLBP DECIMAL(18,4) NOT NULL;
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.tbReceiptHeader') AND name=N'exchangeRate' AND is_nullable=1)
    ALTER TABLE dbo.tbReceiptHeader ALTER COLUMN exchangeRate DECIMAL(18,4) NOT NULL;
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.tbReceiptHeader') AND name=N'paymentRecorded' AND is_nullable=1)
    ALTER TABLE dbo.tbReceiptHeader ALTER COLUMN paymentRecorded BIT NOT NULL;
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.tbReceiptHeader') AND name=N'status' AND is_nullable=1)
    ALTER TABLE dbo.tbReceiptHeader ALTER COLUMN status VARCHAR(30) NOT NULL;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.tbReceiptHeader') AND name=N'IX_tbReceiptHeader_soldAt')
    CREATE INDEX IX_tbReceiptHeader_soldAt ON dbo.tbReceiptHeader(soldAt DESC) INCLUDE(cashier,subtotal,lineDiscount,transactionDiscount,netTotal,status);
GO

IF OBJECT_ID(N'dbo.tbReceiptLine', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tbReceiptLine
    (
        id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_tbReceiptLine PRIMARY KEY,
        transno VARCHAR(50) NOT NULL,
        [lineNo] INT NOT NULL,
        cartId INT NULL,
        pcode VARCHAR(50) NOT NULL,
        description NVARCHAR(250) NOT NULL,
        itemType VARCHAR(20) NOT NULL,
        price DECIMAL(18,4) NOT NULL,
        qty INT NOT NULL,
        lineDiscount DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbReceiptLine_discount DEFAULT(0),
        lineTotal DECIMAL(18,4) NOT NULL
    );
END;
GO
IF COL_LENGTH(N'dbo.tbReceiptLine', N'lineNo') IS NULL ALTER TABLE dbo.tbReceiptLine ADD [lineNo] INT NULL;
IF COL_LENGTH(N'dbo.tbReceiptLine', N'cartId') IS NULL ALTER TABLE dbo.tbReceiptLine ADD cartId INT NULL;
IF COL_LENGTH(N'dbo.tbReceiptLine', N'pcode') IS NULL ALTER TABLE dbo.tbReceiptLine ADD pcode VARCHAR(50) NULL;
IF COL_LENGTH(N'dbo.tbReceiptLine', N'description') IS NULL ALTER TABLE dbo.tbReceiptLine ADD description NVARCHAR(250) NULL;
IF COL_LENGTH(N'dbo.tbReceiptLine', N'itemType') IS NULL ALTER TABLE dbo.tbReceiptLine ADD itemType VARCHAR(20) NULL;
IF COL_LENGTH(N'dbo.tbReceiptLine', N'price') IS NULL ALTER TABLE dbo.tbReceiptLine ADD price DECIMAL(18,4) NULL;
IF COL_LENGTH(N'dbo.tbReceiptLine', N'qty') IS NULL ALTER TABLE dbo.tbReceiptLine ADD qty INT NULL;
IF COL_LENGTH(N'dbo.tbReceiptLine', N'lineDiscount') IS NULL ALTER TABLE dbo.tbReceiptLine ADD lineDiscount DECIMAL(18,4) NULL;
IF COL_LENGTH(N'dbo.tbReceiptLine', N'lineTotal') IS NULL ALTER TABLE dbo.tbReceiptLine ADD lineTotal DECIMAL(18,4) NULL;
UPDATE dbo.tbReceiptLine
SET [lineNo]=ISNULL([lineNo],id),pcode=COALESCE(NULLIF(pcode,''),'UNKNOWN'),description=COALESCE(NULLIF(description,''),pcode,'Unknown item'),
    itemType=COALESCE(NULLIF(itemType,''),'PRODUCT'),price=ISNULL(price,0),qty=ISNULL(qty,0),lineDiscount=ISNULL(lineDiscount,0),lineTotal=ISNULL(lineTotal,0);
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.tbReceiptLine') AND name=N'lineNo' AND is_nullable=1)
    ALTER TABLE dbo.tbReceiptLine ALTER COLUMN [lineNo] INT NOT NULL;
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.tbReceiptLine') AND name=N'pcode' AND is_nullable=1)
    ALTER TABLE dbo.tbReceiptLine ALTER COLUMN pcode VARCHAR(50) NOT NULL;
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.tbReceiptLine') AND name=N'description' AND is_nullable=1)
    ALTER TABLE dbo.tbReceiptLine ALTER COLUMN description NVARCHAR(250) NOT NULL;
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.tbReceiptLine') AND name=N'itemType' AND is_nullable=1)
    ALTER TABLE dbo.tbReceiptLine ALTER COLUMN itemType VARCHAR(20) NOT NULL;
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.tbReceiptLine') AND name=N'price' AND is_nullable=1)
    ALTER TABLE dbo.tbReceiptLine ALTER COLUMN price DECIMAL(18,4) NOT NULL;
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.tbReceiptLine') AND name=N'qty' AND is_nullable=1)
    ALTER TABLE dbo.tbReceiptLine ALTER COLUMN qty INT NOT NULL;
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.tbReceiptLine') AND name=N'lineDiscount' AND is_nullable=1)
    ALTER TABLE dbo.tbReceiptLine ALTER COLUMN lineDiscount DECIMAL(18,4) NOT NULL;
IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.tbReceiptLine') AND name=N'lineTotal' AND is_nullable=1)
    ALTER TABLE dbo.tbReceiptLine ALTER COLUMN lineTotal DECIMAL(18,4) NOT NULL;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.tbReceiptLine') AND name=N'UX_tbReceiptLine_transno_lineNo')
   AND NOT EXISTS (SELECT transno,[lineNo] FROM dbo.tbReceiptLine GROUP BY transno,[lineNo] HAVING COUNT(*)>1)
    CREATE UNIQUE INDEX UX_tbReceiptLine_transno_lineNo ON dbo.tbReceiptLine(transno,[lineNo]);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.tbReceiptLine') AND name=N'UX_tbReceiptLine_cartId')
   AND NOT EXISTS (SELECT cartId FROM dbo.tbReceiptLine WHERE cartId IS NOT NULL GROUP BY cartId HAVING COUNT(*)>1)
    CREATE UNIQUE INDEX UX_tbReceiptLine_cartId ON dbo.tbReceiptLine(cartId) WHERE cartId IS NOT NULL;
GO

/* Best-effort migration of older sold transactions into searchable receipts. */
INSERT INTO dbo.tbReceiptHeader
(transno,soldAt,cashier,subtotal,lineDiscount,transactionDiscount,discountReason,netTotal,
 cashUSD,cashLBP,changeUSD,changeLBP,exchangeRate,paymentRecorded,status)
SELECT c.transno,MIN(c.sdate),MAX(c.cashier),
       SUM(CONVERT(DECIMAL(18,4),c.price*c.qty)),SUM(ISNULL(c.disc,0)),ISNULL(MAX(d.amount),0),MAX(d.reason),
       CASE WHEN SUM(ISNULL(c.total,0))-ISNULL(MAX(d.amount),0)<0 THEN 0 ELSE SUM(ISNULL(c.total,0))-ISNULL(MAX(d.amount),0) END,
       0,0,0,0,89500,0,
       CASE WHEN SUM(CASE WHEN c.status='Sold' AND c.qty>0 THEN 1 ELSE 0 END)=0 THEN 'Cancelled'
            WHEN SUM(CASE WHEN c.status='Cancelled' OR c.qty=0 THEN 1 ELSE 0 END)>0 THEN 'Partially Returned'
            ELSE 'Sold' END
FROM dbo.tbCart c
OUTER APPLY
(
    SELECT TOP 1 amount,reason
    FROM dbo.tbTransactionDiscount td
    WHERE td.transno=c.transno AND td.status IN ('Sold','Cancelled')
    ORDER BY ISNULL(td.soldAt,td.appliedAt) DESC,td.id DESC
) d
WHERE c.status IN ('Sold','Cancelled') AND ISNULL(c.transno,'')<>''
  AND NOT EXISTS(SELECT 1 FROM dbo.tbReceiptHeader h WHERE h.transno=c.transno)
GROUP BY c.transno;
GO

;WITH sourceLines AS
(
    SELECT c.transno,ROW_NUMBER() OVER(PARTITION BY c.transno ORDER BY c.id) AS [lineNo],c.id cartId,c.pcode,
           COALESCE(NULLIF(c.itemDescription,''),p.pdesc,c.pcode) description,ISNULL(c.itemType,'PRODUCT') itemType,
           c.price,c.qty,ISNULL(c.disc,0) lineDiscount,c.total lineTotal
    FROM dbo.tbCart c
    LEFT JOIN dbo.tbProduct p ON p.pcode=c.pcode
    WHERE c.status IN ('Sold','Cancelled') AND ISNULL(c.transno,'')<>''
)
INSERT INTO dbo.tbReceiptLine(transno,[lineNo],cartId,pcode,description,itemType,price,qty,lineDiscount,lineTotal)
SELECT s.transno,s.[lineNo],s.cartId,s.pcode,s.description,s.itemType,s.price,s.qty,s.lineDiscount,s.lineTotal
FROM sourceLines s
WHERE NOT EXISTS(SELECT 1 FROM dbo.tbReceiptLine r WHERE r.cartId=s.cartId);
GO

IF OBJECT_ID(N'dbo.tbCancel', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tbCancel
    (
        id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_tbCancel PRIMARY KEY,
        transno VARCHAR(50) NOT NULL,
        pcode VARCHAR(50) NOT NULL,
        price DECIMAL(18,4) NOT NULL,
        qty INT NOT NULL,
        total DECIMAL(18,4) NOT NULL,
        sdate DATETIME NOT NULL CONSTRAINT DF_tbCancel_sdate DEFAULT(GETDATE()),
        voidby VARCHAR(100) NULL,
        cancelledby VARCHAR(100) NULL,
        reason NVARCHAR(500) NULL,
        action VARCHAR(50) NULL
    );
END;
GO

/* =========================
   SERVICE PROVIDERS / MOBILE LINES
   ========================= */
IF OBJECT_ID(N'dbo.tbServiceProvider', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tbServiceProvider
    (
        id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_tbServiceProvider PRIMARY KEY,
        providerName NVARCHAR(100) NOT NULL,
        providerCode NVARCHAR(30) NULL,
        providerType NVARCHAR(30) NOT NULL CONSTRAINT DF_tbServiceProvider_type DEFAULT(N'Wallet'),
        currentBalance DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbServiceProvider_balance DEFAULT(0),
        isActive BIT NOT NULL CONSTRAINT DF_tbServiceProvider_active DEFAULT(1),
        displayOrder INT NOT NULL CONSTRAINT DF_tbServiceProvider_order DEFAULT(999),
        createdAt DATETIME NOT NULL CONSTRAINT DF_tbServiceProvider_created DEFAULT(GETDATE())
    );
END;
GO
IF COL_LENGTH(N'dbo.tbServiceProvider', N'providerCode') IS NULL ALTER TABLE dbo.tbServiceProvider ADD providerCode NVARCHAR(30) NULL;
IF COL_LENGTH(N'dbo.tbServiceProvider', N'providerType') IS NULL ALTER TABLE dbo.tbServiceProvider ADD providerType NVARCHAR(30) NOT NULL CONSTRAINT DF_tbServiceProvider_type_upgrade DEFAULT(N'Wallet') WITH VALUES;
IF COL_LENGTH(N'dbo.tbServiceProvider', N'currentBalance') IS NULL ALTER TABLE dbo.tbServiceProvider ADD currentBalance DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbServiceProvider_balance_upgrade DEFAULT(0) WITH VALUES;
IF COL_LENGTH(N'dbo.tbServiceProvider', N'isActive') IS NULL ALTER TABLE dbo.tbServiceProvider ADD isActive BIT NOT NULL CONSTRAINT DF_tbServiceProvider_active_upgrade DEFAULT(1) WITH VALUES;
IF COL_LENGTH(N'dbo.tbServiceProvider', N'displayOrder') IS NULL ALTER TABLE dbo.tbServiceProvider ADD displayOrder INT NOT NULL CONSTRAINT DF_tbServiceProvider_order_upgrade DEFAULT(999) WITH VALUES;
IF COL_LENGTH(N'dbo.tbServiceProvider', N'createdAt') IS NULL ALTER TABLE dbo.tbServiceProvider ADD createdAt DATETIME NOT NULL CONSTRAINT DF_tbServiceProvider_created_upgrade DEFAULT(GETDATE()) WITH VALUES;
GO

UPDATE dbo.tbServiceProvider SET providerCode=N'WISH', providerType=N'Wallet', displayOrder=1 WHERE providerName=N'WishMoney' AND (providerCode IS NULL OR providerCode='');
UPDATE dbo.tbServiceProvider SET providerCode=N'MTC_LINE', providerType=N'Mobile Line', displayOrder=2 WHERE providerName IN (N'MTC Line',N'Touch Line') AND (providerCode IS NULL OR providerCode='');
UPDATE dbo.tbServiceProvider SET providerCode=N'ALFA_LINE', providerType=N'Mobile Line', displayOrder=3 WHERE providerName=N'Alfa Line' AND (providerCode IS NULL OR providerCode='');
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.tbServiceProvider') AND name=N'UX_tbServiceProvider_name')
   AND NOT EXISTS (SELECT providerName FROM dbo.tbServiceProvider GROUP BY providerName HAVING COUNT(*)>1)
    CREATE UNIQUE INDEX UX_tbServiceProvider_name ON dbo.tbServiceProvider(providerName);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.tbServiceProvider') AND name=N'UX_tbServiceProvider_code')
   AND NOT EXISTS (SELECT providerCode FROM dbo.tbServiceProvider WHERE providerCode IS NOT NULL GROUP BY providerCode HAVING COUNT(*)>1)
    CREATE UNIQUE INDEX UX_tbServiceProvider_code ON dbo.tbServiceProvider(providerCode) WHERE providerCode IS NOT NULL;
GO

/* =========================
   FIVE SERVICE TYPES
   ========================= */
IF OBJECT_ID(N'dbo.tbRechargeService', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tbRechargeService
    (
        id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_tbRechargeService PRIMARY KEY,
        serviceName NVARCHAR(150) NOT NULL,
        serviceType NVARCHAR(40) NOT NULL,
        providerId INT NOT NULL,
        operatorName NVARCHAR(20) NULL,
        retakeEnabled BIT NOT NULL CONSTRAINT DF_tbRechargeService_retake DEFAULT(0),
        cost DECIMAL(18,4) NOT NULL,
        deductedPrice DECIMAL(18,4) NOT NULL,
        finalPrice DECIMAL(18,4) NOT NULL,
        profit AS (CONVERT(DECIMAL(18,4),finalPrice-cost)) PERSISTED,
        isActive BIT NOT NULL CONSTRAINT DF_tbRechargeService_active DEFAULT(1),
        createdAt DATETIME NOT NULL CONSTRAINT DF_tbRechargeService_created DEFAULT(GETDATE()),
        CONSTRAINT CK_tbRechargeService_values CHECK(cost>=0 AND deductedPrice>=0 AND finalPrice>=0),
        CONSTRAINT FK_tbRechargeService_provider FOREIGN KEY(providerId) REFERENCES dbo.tbServiceProvider(id)
    );
END;
GO
IF COL_LENGTH(N'dbo.tbRechargeService', N'operatorName') IS NULL ALTER TABLE dbo.tbRechargeService ADD operatorName NVARCHAR(20) NULL;
IF COL_LENGTH(N'dbo.tbRechargeService', N'retakeEnabled') IS NULL ALTER TABLE dbo.tbRechargeService ADD retakeEnabled BIT NOT NULL CONSTRAINT DF_tbRechargeService_retake_upgrade DEFAULT(0) WITH VALUES;
IF COL_LENGTH(N'dbo.tbRechargeService', N'deductedPrice') IS NULL
BEGIN
    ALTER TABLE dbo.tbRechargeService ADD deductedPrice DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbRechargeService_deductedPrice_upgrade DEFAULT(0) WITH VALUES;
    EXEC(N'UPDATE dbo.tbRechargeService SET deductedPrice=cost;');
END;
GO
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE parent_object_id=OBJECT_ID(N'dbo.tbRechargeService') AND name=N'CK_tbRechargeService_deductedPrice')
    ALTER TABLE dbo.tbRechargeService ADD CONSTRAINT CK_tbRechargeService_deductedPrice CHECK(deductedPrice>=0);
GO
UPDATE dbo.tbRechargeService SET retakeEnabled=CASE WHEN serviceType=N'Validity Recharge' THEN 1 ELSE 0 END;
GO
DECLARE @dropServiceTypeChecks NVARCHAR(MAX)=N'';
SELECT @dropServiceTypeChecks=@dropServiceTypeChecks+N'ALTER TABLE dbo.tbRechargeService DROP CONSTRAINT '+QUOTENAME(name)+N';'
FROM sys.check_constraints
WHERE parent_object_id=OBJECT_ID(N'dbo.tbRechargeService') AND definition LIKE N'%serviceType%';
IF @dropServiceTypeChecks<>N'' EXEC sys.sp_executesql @dropServiceTypeChecks;
GO
UPDATE dbo.tbRechargeService SET serviceType=N'Apps' WHERE serviceType=N'App Recharge';
UPDATE dbo.tbRechargeService SET serviceType=N'Internet Bundles' WHERE serviceType=N'Ushare';
GO
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE parent_object_id=OBJECT_ID(N'dbo.tbRechargeService') AND name=N'CK_tbRechargeService_type_v2')
    ALTER TABLE dbo.tbRechargeService ADD CONSTRAINT CK_tbRechargeService_type_v2
    CHECK(serviceType IN (N'Mobile Recharge',N'Transfer Balance',N'Validity Recharge',N'Internet Bundles',N'Apps'));
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.tbRechargeService') AND name=N'IX_tbRechargeService_active_type')
    CREATE INDEX IX_tbRechargeService_active_type ON dbo.tbRechargeService(isActive,serviceType) INCLUDE(serviceName,providerId,operatorName,retakeEnabled,cost,deductedPrice,finalPrice);
GO

IF OBJECT_ID(N'dbo.tbServiceSale', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tbServiceSale
    (
        id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_tbServiceSale PRIMARY KEY,
        serviceId INT NOT NULL,
        providerId INT NOT NULL,
        cartId INT NOT NULL,
        transno VARCHAR(50) NOT NULL,
        qty INT NOT NULL,
        unitCost DECIMAL(18,4) NOT NULL,
        unitDeductedPrice DECIMAL(18,4) NOT NULL,
        unitPrice DECIMAL(18,4) NOT NULL,
        serviceType NVARCHAR(40) NULL,
        operatorName NVARCHAR(20) NULL,
        retakeAmount DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbServiceSale_retake DEFAULT(0),
        retakeProviderId INT NULL,
        profit AS (CONVERT(DECIMAL(18,4),(unitPrice-unitCost)*qty)) PERSISTED,
        cashier VARCHAR(100) NULL,
        sdate DATETIME NOT NULL,
        status VARCHAR(30) NOT NULL CONSTRAINT DF_tbServiceSale_status DEFAULT('Sold'),
        CONSTRAINT FK_tbServiceSale_service FOREIGN KEY(serviceId) REFERENCES dbo.tbRechargeService(id),
        CONSTRAINT FK_tbServiceSale_provider FOREIGN KEY(providerId) REFERENCES dbo.tbServiceProvider(id)
    );
END;
GO
IF COL_LENGTH(N'dbo.tbServiceSale', N'serviceType') IS NULL ALTER TABLE dbo.tbServiceSale ADD serviceType NVARCHAR(40) NULL;
IF COL_LENGTH(N'dbo.tbServiceSale', N'unitDeductedPrice') IS NULL
BEGIN
    ALTER TABLE dbo.tbServiceSale ADD unitDeductedPrice DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbServiceSale_unitDeductedPrice_upgrade DEFAULT(0) WITH VALUES;
    EXEC(N'UPDATE dbo.tbServiceSale SET unitDeductedPrice=unitCost;');
END;
IF COL_LENGTH(N'dbo.tbServiceSale', N'operatorName') IS NULL ALTER TABLE dbo.tbServiceSale ADD operatorName NVARCHAR(20) NULL;
IF COL_LENGTH(N'dbo.tbServiceSale', N'retakeAmount') IS NULL ALTER TABLE dbo.tbServiceSale ADD retakeAmount DECIMAL(18,4) NOT NULL CONSTRAINT DF_tbServiceSale_retake_upgrade DEFAULT(0) WITH VALUES;
IF COL_LENGTH(N'dbo.tbServiceSale', N'retakeProviderId') IS NULL ALTER TABLE dbo.tbServiceSale ADD retakeProviderId INT NULL;
GO
UPDATE dbo.tbServiceSale SET serviceType=N'Internet Bundles' WHERE serviceType=N'Ushare';
UPDATE ss SET serviceType=s.serviceType,operatorName=s.operatorName
FROM dbo.tbServiceSale ss INNER JOIN dbo.tbRechargeService s ON s.id=ss.serviceId
WHERE ss.serviceType IS NULL;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.tbServiceSale') AND name=N'UX_tbServiceSale_cartId')
    CREATE UNIQUE INDEX UX_tbServiceSale_cartId ON dbo.tbServiceSale(cartId);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.tbServiceSale') AND name=N'IX_tbServiceSale_date')
    CREATE INDEX IX_tbServiceSale_date ON dbo.tbServiceSale(sdate,status) INCLUDE(transno,serviceId,providerId,qty,unitCost,unitDeductedPrice,unitPrice,profit,cashier,serviceType,operatorName,retakeAmount,retakeProviderId);
GO

IF OBJECT_ID(N'dbo.tbProviderLedger', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tbProviderLedger
    (
        id BIGINT IDENTITY(1,1) NOT NULL CONSTRAINT PK_tbProviderLedger PRIMARY KEY,
        providerId INT NOT NULL,
        serviceId INT NULL,
        cartId INT NULL,
        movementType NVARCHAR(40) NOT NULL,
        amount DECIMAL(18,4) NOT NULL,
        balanceAfter DECIMAL(18,4) NOT NULL,
        username VARCHAR(100) NULL,
        sdate DATETIME NOT NULL CONSTRAINT DF_tbProviderLedger_sdate DEFAULT(GETDATE()),
        notes NVARCHAR(500) NULL,
        CONSTRAINT FK_tbProviderLedger_provider FOREIGN KEY(providerId) REFERENCES dbo.tbServiceProvider(id),
        CONSTRAINT FK_tbProviderLedger_service FOREIGN KEY(serviceId) REFERENCES dbo.tbRechargeService(id)
    );
END;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.tbProviderLedger') AND name=N'IX_tbProviderLedger_provider_date')
    CREATE INDEX IX_tbProviderLedger_provider_date ON dbo.tbProviderLedger(providerId,sdate DESC) INCLUDE(serviceId,cartId,movementType,amount,balanceAfter,username,notes);
GO

/* =========================
   MOBILE FIXING
   ========================= */
IF OBJECT_ID(N'dbo.tbMobileFix', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tbMobileFix
    (
        id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_tbMobileFix PRIMARY KEY,
        fixCode VARCHAR(40) NOT NULL,
        customerName NVARCHAR(150) NOT NULL,
        phoneNumber NVARCHAR(50) NOT NULL,
        phoneModel NVARCHAR(150) NOT NULL,
        problemDescription NVARCHAR(500) NOT NULL,
        costPrice DECIMAL(18,4) NOT NULL,
        finalPrice DECIMAL(18,4) NOT NULL,
        profit AS (CONVERT(DECIMAL(18,4), finalPrice-costPrice)) PERSISTED,
        estimatedFinish DATETIME NOT NULL,
        status NVARCHAR(30) NOT NULL CONSTRAINT DF_tbMobileFix_status DEFAULT(N'Received'),
        receivedBy VARCHAR(100) NULL,
        receivedAt DATETIME NOT NULL CONSTRAINT DF_tbMobileFix_receivedAt DEFAULT(GETDATE()),
        deliveredBy VARCHAR(100) NULL,
        deliveredAt DATETIME NULL,
        deliveredTransno VARCHAR(50) NULL,
        CONSTRAINT CK_tbMobileFix_status CHECK(status IN (N'Received',N'In Progress',N'Ready',N'Delivered',N'Cancelled')),
        CONSTRAINT CK_tbMobileFix_prices CHECK(costPrice>=0 AND finalPrice>=0)
    );
END;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.tbMobileFix') AND name=N'UX_tbMobileFix_fixCode')
    CREATE UNIQUE INDEX UX_tbMobileFix_fixCode ON dbo.tbMobileFix(fixCode);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.tbMobileFix') AND name=N'IX_tbMobileFix_status_received')
    CREATE INDEX IX_tbMobileFix_status_received ON dbo.tbMobileFix(status,receivedAt DESC) INCLUDE(fixCode,customerName,phoneNumber,phoneModel,estimatedFinish,finalPrice);
GO

/* =========================
   SEED ESSENTIAL DATA
   ========================= */
IF NOT EXISTS(SELECT 1 FROM dbo.tbBrand WHERE brand=N'General') INSERT dbo.tbBrand(brand) VALUES(N'General');
IF NOT EXISTS(SELECT 1 FROM dbo.tbCategory WHERE category=N'General') INSERT dbo.tbCategory(category) VALUES(N'General');
IF NOT EXISTS(SELECT 1 FROM dbo.tbSupplier WHERE supplier=N'General Supplier')
    INSERT dbo.tbSupplier(supplier,address,contactperson,phone,email,fax) VALUES(N'General Supplier',N'',N'',N'',N'',N'');
IF NOT EXISTS(SELECT 1 FROM dbo.tbStore)
    INSERT dbo.tbStore(store,address) VALUES(N'Stockly Mobile Shop',N'');
IF NOT EXISTS(SELECT 1 FROM dbo.tbUser WHERE username='admin')
    INSERT dbo.tbUser(username,password,role,name,isactivate) VALUES('admin','admin123','Administrator',N'Administrator',1);
IF NOT EXISTS(SELECT 1 FROM dbo.tbUser WHERE username='cashier')
    INSERT dbo.tbUser(username,password,role,name,isactivate) VALUES('cashier','cashier123','Cashier',N'Cashier',1);
IF NOT EXISTS(SELECT 1 FROM dbo.tbServiceProvider WHERE providerCode=N'WISH' OR providerName=N'WishMoney')
    INSERT dbo.tbServiceProvider(providerName,providerCode,providerType,currentBalance,isActive,displayOrder,createdAt) VALUES(N'WishMoney',N'WISH',N'Wallet',0,1,1,GETDATE());
ELSE
    UPDATE dbo.tbServiceProvider SET providerName=N'WishMoney',providerCode=N'WISH',providerType=N'Wallet',displayOrder=1 WHERE providerCode=N'WISH' OR providerName=N'WishMoney';
IF NOT EXISTS(SELECT 1 FROM dbo.tbServiceProvider WHERE providerCode=N'MTC_LINE')
    INSERT dbo.tbServiceProvider(providerName,providerCode,providerType,currentBalance,isActive,displayOrder,createdAt) VALUES(N'MTC Line',N'MTC_LINE',N'Mobile Line',0,1,2,GETDATE());
IF NOT EXISTS(SELECT 1 FROM dbo.tbServiceProvider WHERE providerCode=N'ALFA_LINE')
    INSERT dbo.tbServiceProvider(providerName,providerCode,providerType,currentBalance,isActive,displayOrder,createdAt) VALUES(N'Alfa Line',N'ALFA_LINE',N'Mobile Line',0,1,3,GETDATE());
GO
DECLARE @wishServiceProviderId INT=(SELECT TOP 1 id FROM dbo.tbServiceProvider WHERE providerCode=N'WISH');
IF @wishServiceProviderId IS NOT NULL
BEGIN
    UPDATE dbo.tbRechargeService
    SET providerId=@wishServiceProviderId
    WHERE serviceType IN (N'Mobile Recharge',N'Validity Recharge')
      AND providerId<>@wishServiceProviderId;
END;
GO

/* =========================
   REPORTING VIEWS EXPECTED BY THE ORIGINAL POS
   ========================= */
IF OBJECT_ID(N'dbo.vwCriticalItems', N'V') IS NOT NULL DROP VIEW dbo.vwCriticalItems;
GO
CREATE VIEW dbo.vwCriticalItems
AS
SELECT p.pcode,
       p.barcode,
       p.pdesc,
       b.brand,
       c.category,
       p.price,
       p.reorder,
       p.qty
FROM dbo.tbProduct p
INNER JOIN dbo.tbBrand b ON b.id=p.bid
INNER JOIN dbo.tbCategory c ON c.id=p.cid
WHERE p.isActive=1 AND p.qty<=p.reorder;
GO

IF OBJECT_ID(N'dbo.vwInventoryList', N'V') IS NOT NULL DROP VIEW dbo.vwInventoryList;
GO
CREATE VIEW dbo.vwInventoryList
AS
SELECT p.pcode,
       p.barcode,
       p.pdesc,
       b.brand,
       c.category,
       p.price,
       p.reorder,
       p.qty
FROM dbo.tbProduct p
INNER JOIN dbo.tbBrand b ON b.id=p.bid
INNER JOIN dbo.tbCategory c ON c.id=p.cid
WHERE p.isActive=1;
GO

IF OBJECT_ID(N'dbo.vwTopSelling', N'V') IS NOT NULL DROP VIEW dbo.vwTopSelling;
GO
CREATE VIEW dbo.vwTopSelling
AS
SELECT c.pcode,
       COALESCE(NULLIF(c.itemDescription,N''),p.pdesc,c.pcode) AS pdesc,
       c.qty,
       c.total,
       c.sdate,
       c.status
FROM dbo.tbCart c
LEFT JOIN dbo.tbProduct p ON p.pcode=c.pcode
WHERE ISNULL(c.itemType,'PRODUCT')='PRODUCT';
GO

IF OBJECT_ID(N'dbo.vwCancelItems', N'V') IS NOT NULL DROP VIEW dbo.vwCancelItems;
GO
CREATE VIEW dbo.vwCancelItems
AS
SELECT x.transno,
       x.pcode,
       COALESCE(p.pdesc,x.pcode) AS pdesc,
       x.price,
       x.qty,
       x.total,
       x.sdate,
       x.voidby,
       x.cancelledby,
       x.reason,
       x.action
FROM dbo.tbCancel x
LEFT JOIN dbo.tbProduct p ON p.pcode=x.pcode;
GO

IF OBJECT_ID(N'dbo.vwStockIn', N'V') IS NOT NULL DROP VIEW dbo.vwStockIn;
GO
CREATE VIEW dbo.vwStockIn
AS
SELECT s.id,
       s.refno,
       s.pcode,
       p.pdesc,
       s.qty,
       s.sdate,
       s.stockinby,
       s.status,
       ISNULL(sup.supplier,N'') AS supplier,
       ISNULL(s.CostPrice,p.CostPrice) AS CostPrice,
       ISNULL(s.Markup,p.Markup) AS Markup,
       ISNULL(s.Price,p.price) AS Price
FROM dbo.tbStockIn s
INNER JOIN dbo.tbProduct p ON p.pcode=s.pcode
LEFT JOIN dbo.tbSupplier sup ON sup.id=s.supplierid;
GO

IF OBJECT_ID(N'dbo.vwDailySystemProfit', N'V') IS NOT NULL DROP VIEW dbo.vwDailySystemProfit;
GO
CREATE VIEW dbo.vwDailySystemProfit
AS
SELECT c.id,c.transno,c.sdate,c.cashier,
       CASE WHEN ISNULL(c.itemType,'PRODUCT')='PRODUCT' THEN N'Products'
            WHEN c.itemType='FIX' THEN N'Mobile Fixing'
            ELSE COALESCE(ss.serviceType,s.serviceType,N'Services') END AS category,
       COALESCE(NULLIF(c.itemDescription,N''),p.pdesc,c.pcode) AS itemDescription,
       c.qty,c.total AS sales,
       CONVERT(DECIMAL(18,4),ISNULL(c.costPrice,0)*c.qty) AS cost,
       CONVERT(DECIMAL(18,4),c.total-(ISNULL(c.costPrice,0)*c.qty)) AS profit
FROM dbo.tbCart c
LEFT JOIN dbo.tbProduct p ON p.pcode=c.pcode
LEFT JOIN dbo.tbRechargeService s ON c.itemType='SERVICE' AND s.id=c.referenceId
LEFT JOIN dbo.tbServiceSale ss ON c.itemType='SERVICE' AND ss.cartId=c.id
WHERE c.status='Sold';
GO

PRINT 'Stocklymobi database installation / upgrade completed successfully.';
GO
