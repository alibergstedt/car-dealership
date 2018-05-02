USE GuildCars
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ContactsSelectAll')
      DROP PROCEDURE ContactsSelectAll
GO

CREATE PROCEDURE ContactsSelectAll AS
BEGIN
	SELECT ContactId, ContactName, EmailAddress, TelephoneNumber, ContactMessage
	FROM Contact
END

GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ContactsInsert')
      DROP PROCEDURE ContactsInsert
GO

CREATE PROCEDURE ContactsInsert (
	@ContactId int output,
	@ContactName varchar(75),
	@EmailAddress varchar(100),
	@TelephoneNumber varchar(25),
	@ContactMessage varchar(255)
) AS
BEGIN
	INSERT INTO Contact(ContactName, EmailAddress, TelephoneNumber, ContactMessage)
	VALUES (@ContactName, @EmailAddress, @TelephoneNumber, @ContactMessage)

	SET @ContactId = SCOPE_IDENTITY();
END
GO



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'PromosSelectAll')
      DROP PROCEDURE PromosSelectAll
GO

CREATE PROCEDURE PromosSelectAll AS
BEGIN
	SELECT PromoCodeId, PromotionName, PromotionDescription,ImageFileName
	FROM Promo
END

GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'PromoInsert')
      DROP PROCEDURE PromoInsert
GO

CREATE PROCEDURE PromoInsert (
	@PromoCodeId int output,
	@PromotionName varchar(30),
	@PromotionDescription varchar(255),
	@ImageFileName varchar(50)
) AS
BEGIN
	INSERT INTO Promo (PromotionName, PromotionDescription, ImageFileName)
	VALUES (@PromotionName, @PromotionDescription, @ImageFileName)

	SET @PromoCodeId = SCOPE_IDENTITY();
END

GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'PromoDelete')
      DROP PROCEDURE PromoDelete
GO

CREATE PROCEDURE PromoDelete (
	@PromoCodeId int
) AS
BEGIN
	BEGIN TRANSACTION

	DELETE FROM Promo WHERE PromoCodeId = @PromoCodeId;

	COMMIT TRANSACTION
END
GO



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SalesInvoiceInsert')
      DROP PROCEDURE SalesInvoiceInsert
GO

CREATE PROCEDURE SalesInvoiceInsert (
	@InvoiceId int output,
	@CarId int,
	@StateId int,
	@PurchaseTypeId int,
	@SalesPerson varchar(75),
	@ZipCode char(5),
	@UserEmail varchar(128),
	@ContactName varchar(75),
	@TelephoneNumber varchar(75),
	@StreetAddress1 varchar(75),
	@StreetAddress2 varchar(35),
	@City varchar(35),
	@Total decimal(10,2)
) AS
BEGIN
	INSERT INTO SalesInvoice (CarId, StateId, PurchaseTypeId, SalesPerson, ZipCode, UserEmail, ContactName,
	TelephoneNumber, StreetAddress1, StreetAddress2,City, Total)
	VALUES (@CarId, @StateId, @PurchaseTypeId, @SalesPerson, @ZipCode, @UserEmail, @ContactName,
	@TelephoneNumber, @StreetAddress1, @StreetAddress2,	@City,@Total)

	SET @InvoiceId = SCOPE_IDENTITY();
END

GO




IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'CarInsert')
      DROP PROCEDURE CarInsert
GO

CREATE PROCEDURE CarInsert (
	@CarId int output,
	@CategoryId int,
	@CarMakeId int,
	@CarModelId int,
	@BodyStyle varchar(75),
	@VinNumber char(17),
	@CarYear smallint,
	@Transmission varchar(40),
	@CarColor varchar(25),
	@InteriorColor varchar(25),
	@Mileage int,
	@CarDescription varchar(500),
	@CarPrice decimal(10,2),
	@CarSalePrice decimal(10,2),
	@ImageFileName varchar(50),
	@IsFeatured bit,
	@IsSold bit
) AS
BEGIN
	INSERT INTO Car (CategoryId, CarMakeId, CarModelId, BodyStyle, VinNumber, CarYear, Transmission, CarColor, InteriorColor,
	Mileage, CarDescription, CarPrice, CarSalePrice, ImageFileName, IsFeatured, IsSold)
	VALUES (@CategoryId, @CarMakeId, @CarModelId, @BodyStyle, @VinNumber, @CarYear, @Transmission, @CarColor, @InteriorColor,
	@Mileage, @CarDescription, @CarPrice, @CarSalePrice, @ImageFileName, @IsFeatured, @IsSold)

	SET @CarId = SCOPE_IDENTITY();
END

GO



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'CarMakeInsert')
      DROP PROCEDURE CarMakeInsert
GO

CREATE PROCEDURE CarMakeInsert (
	@CarMakeId int output,
	@CarMakeName varchar(35)
) AS
BEGIN
	INSERT INTO CarMake (CarMakeName)
	VALUES (@CarMakeName)

	SET @CarMakeId = SCOPE_IDENTITY();
END

GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'CarModelInsert')
      DROP PROCEDURE CarModelInsert
GO

CREATE PROCEDURE CarModelInsert (
	@CarModelId int output,
	@CarMakeName varchar(35),
	@CarModelName varchar(35)
) AS
BEGIN
	DECLARE
	@CarMakeId int

	SELECT @CarMakeId = mk.CarMakeId FROM CarMake mk WHERE mk.CarMakeName = @CarMakeName
	INSERT INTO CarModel (CarMakeId, CarModelName)
	VALUES (@CarMakeId, @CarModelName)

	SET @CarModelId = SCOPE_IDENTITY();
END

GO



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'CarUpdate')
      DROP PROCEDURE CarUpdate
GO

CREATE PROCEDURE CarUpdate (
	@CarId int,
	@CategoryId int,
	@CarMakeId int,
	@CarModelId int,
	@BodyStyle varchar(75),
	@VinNumber char(17),
	@CarYear smallint,
	@Transmission varchar(40),
	@CarColor varchar(25),
	@InteriorColor varchar(25),
	@Mileage int,
	@CarDescription varchar(500),
	@CarPrice decimal(10,2),
	@CarSalePrice decimal(10,2),
	@ImageFileName varchar(50),
	@IsFeatured bit,
	@IsSold bit
) AS
BEGIN
	UPDATE Car SET 
		CategoryId = @CategoryId,
		CarMakeId = @CarMakeId,
		CarModelId = @CarModelId,
		BodyStyle = @BodyStyle,
		VinNumber = @VinNumber,
		CarYear = @CarYear,
		Transmission = @Transmission,
		CarColor = @CarColor,
		InteriorColor = @InteriorColor,
		Mileage = @Mileage,
		CarDescription = @CarDescription,
		CarPrice = @CarPrice,
		CarSalePrice = @CarSalePrice,
		ImageFileName = @ImageFileName,
		IsFeatured = @IsFeatured,
		IsSold = @IsSold
	WHERE CarId = @CarId
END

GO




IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'CarDelete')
      DROP PROCEDURE CarDelete
GO

CREATE PROCEDURE CarDelete (
	@CarId int
) AS
BEGIN
	BEGIN TRANSACTION

	DELETE FROM Car WHERE CarId = @CarId;

	COMMIT TRANSACTION
END
GO



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'CarGetById')
      DROP PROCEDURE CarGetById
GO

CREATE PROCEDURE CarGetById (
	@CarId int
) AS
BEGIN
	SELECT CarId, c.CarMakeId, c.CarModelId, cg.CategoryId, CarMakeName, CarModelName, BodyStyle, VinNumber,
	CarYear, Transmission, CarColor, InteriorColor, Mileage, CarDescription, CarPrice, CarSalePrice, ImageFileName
	FROM Car c
		INNER JOIN Category cg ON c.CategoryId = cg.CategoryId
		INNER JOIN CarModel cd ON c.CarModelId = cd.CarModelId
		INNER JOIN CarMake cm ON cd.CarMakeId = cm.CarMakeId
	WHERE CarId = @CarId
END
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'CarSelectFeatured')
      DROP PROCEDURE CarSelectFeatured
GO

CREATE PROCEDURE CarSelectFeatured AS
BEGIN
	SELECT TOP 8 CarId, CarMakeName, CarModelName, CarYear, CarPrice, ImageFileName
	FROM Car c
		INNER JOIN CarModel cd ON c.CarModelId = cd.CarModelId
		INNER JOIN CarMake cm ON cd.CarMakeId = cm.CarMakeId
	WHERE IsFeatured = 1
END

GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'CarSelectDetails')
      DROP PROCEDURE CarSelectDetails
GO

CREATE PROCEDURE CarSelectDetails AS 
BEGIN
	SELECT CarId, cm.CarMakeId, IsSold, cd.CarModelId, CarMakeName, CarModelName, VinNumber, BodyStyle, CarYear, Transmission,
	CarColor, InteriorColor, Mileage, CarDescription, CarPrice, CarSalePrice, ImageFileName
	FROM Car c
		INNER JOIN CarMake cm ON c.CarMakeId = cm.CarMakeId
		INNER JOIN CarModel cd ON cm.CarMakeId = cd.CarMakeId
END

GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'CarSelectUsedInventory')
      DROP PROCEDURE CarSelectUsedInventory
GO

CREATE PROCEDURE CarSelectUsedInventory AS
BEGIN
	SELECT CarYear, CarMakeName, CarModelName,
		COUNT(CarId) AS [Count],
		SUM(CarPrice) AS [Stock Value]
	FROM Car c
		INNER JOIN CarModel cd ON c.CarModelId = cd.CarModelId
		INNER JOIN CarMake cm ON cd.CarMakeId = cm.CarMakeId
		INNER JOIN Category ct ON c.CategoryId = ct.CategoryId
	WHERE CategoryName = 'Used' AND IsSold = 0
	GROUP BY CarYear,CarMakeName,CarModelName
END

GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'CarSelectNewInventory')
      DROP PROCEDURE CarSelectNewInventory
GO

CREATE PROCEDURE CarSelectNewInventory AS
BEGIN
	SELECT CarYear, CarMakeName, CarModelName,
		COUNT(CarId) AS [Count],
		SUM(CarPrice) AS [Stock Value]
	FROM Car c
		INNER JOIN CarModel cd ON c.CarModelId = cd.CarModelId
		INNER JOIN CarMake cm ON cd.CarMakeId = cm.CarMakeId
		INNER JOIN Category ct ON c.CategoryId = ct.CategoryId
	WHERE CategoryName = 'New' AND IsSold = 0
	GROUP BY CarYear,CarMakeName,CarModelName
END

GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SearchSalesReport')
      DROP PROCEDURE SearchSalesReport
GO

CREATE PROCEDURE SearchSalesReport AS
BEGIN
	SELECT SalesPerson,
		COUNT(CarId) AS [Total Vehicles],
		SUM(Total) AS [Total Sales]
	FROM SalesInvoice
	GROUP BY SalesPerson
END

GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'CarSelectModels')
      DROP PROCEDURE CarSelectModels
GO

CREATE PROCEDURE CarSelectModels AS
BEGIN
	SELECT CarModelId, CarMakeName AS [Make], CarModelName AS [Model], cd.DateCreated AS [Date Added]
	FROM CarMake cm
		INNER JOIN CarModel cd ON cm.CarMakeId = cd.CarMakeId
	GROUP BY CarModelId,CarMakeName,CarModelName,cd.DateCreated
	ORDER BY CarMakeName, CarModelName
END

GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'CarSelectMakes')
      DROP PROCEDURE CarSelectMakes
GO

CREATE PROCEDURE CarSelectMakes AS
BEGIN
	SELECT CarMakeId, CarMakeName AS [Make], DateCreated AS [Date Added]
	FROM CarMake
	GROUP BY CarMakeId,CarMakeName,DateCreated
	ORDER BY CarMakeName
END

GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'CategorySelectAll')
      DROP PROCEDURE CategorySelectAll
GO

CREATE PROCEDURE CategorySelectAll AS
BEGIN
	SELECT CategoryId, CategoryName
	FROM Category
	ORDER BY CategoryName
END

GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'BodyStyleSelectAll')
      DROP PROCEDURE BodyStyleSelectAll
GO

CREATE PROCEDURE BodyStyleSelectAll AS
BEGIN
	SELECT DISTINCT BodyStyle
	FROM Car
	ORDER BY BodyStyle
END

GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'TransmissionSelectAll')
      DROP PROCEDURE TransmissionSelectAll
GO

CREATE PROCEDURE TransmissionSelectAll AS
BEGIN
	SELECT DISTINCT Transmission
	FROM Car
	ORDER BY Transmission
END

GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'CarColorSelectAll')
      DROP PROCEDURE CarColorSelectAll
GO

CREATE PROCEDURE CarColorSelectAll AS
BEGIN
	SELECT DISTINCT CarColor
	FROM Car
	ORDER BY CarColor
END

GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'InteriorColorSelectAll')
      DROP PROCEDURE InteriorColorSelectAll
GO

CREATE PROCEDURE InteriorColorSelectAll AS
BEGIN
	SELECT DISTINCT InteriorColor
	FROM Car
	ORDER BY InteriorColor
END

GO



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'StatesSelectAll')
      DROP PROCEDURE StatesSelectAll
GO

CREATE PROCEDURE StatesSelectAll AS
BEGIN
	SELECT DISTINCT [State], StateId
	FROM States
	ORDER BY [State]
END

GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'PurchaseTypeSelectAll')
      DROP PROCEDURE PurchaseTypeSelectAll
GO

CREATE PROCEDURE PurchaseTypeSelectAll AS
BEGIN
	SELECT DISTINCT PurchaseTypeId, PurchaseTypeName
	FROM PurchaseType
	ORDER BY PurchaseTypeName
END

GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'GetPurchaseTypeName')
      DROP PROCEDURE GetPurchaseTypeName
GO

CREATE PROCEDURE GetPurchaseTypeName (
	@PurchaseTypeId int
) AS
BEGIN
	SELECT DISTINCT PurchaseTypeName
	FROM PurchaseType
	WHERE PurchaseTypeId = @PurchaseTypeId
END

GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'GetStateName')
      DROP PROCEDURE GetStateName
GO

CREATE PROCEDURE GetStateName (
	@StateId int
) AS
BEGIN
	SELECT DISTINCT [State]
	FROM States
	WHERE StateId = @StateId
END

GO



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SalesPersonSelectAll')
      DROP PROCEDURE SalesPersonSelectAll
GO

CREATE PROCEDURE SalesPersonSelectAll AS
BEGIN
	SELECT DISTINCT SalesPerson
	FROM SalesInvoice
	ORDER BY SalesPerson
END

GO
