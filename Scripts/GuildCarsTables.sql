USE GuildCars
GO


IF EXISTS(SELECT * FROM sys.tables WHERE name='SalesInvoice')
	DROP TABLE SalesInvoice
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='PurchaseType')
	DROP TABLE PurchaseType
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Car')
	DROP TABLE Car
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='CarModel')
	DROP TABLE CarModel
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='CarMake')
	DROP TABLE CarMake
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Promo')
	DROP TABLE Promo
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='States')
	DROP TABLE States
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Category')
	DROP TABLE Category
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Contact')
	DROP TABLE Contact
GO

CREATE TABLE Contact (
	ContactId int identity(1,1) not null primary key,
	ContactName varchar(75) not null,
	EmailAddress varchar(100) not null,
	TelephoneNumber varchar(25) not null,
	ContactMessage varchar(255) null
)

CREATE TABLE Category (
	CategoryId int identity(1,1) not null primary key,
	CategoryName varchar(75) not null,
)

CREATE TABLE States (
    StateId int identity(1,1) not null primary key,
    [State] varchar(2) not null
)

CREATE TABLE Promo (
   PromoCodeId int identity(1,1) not null primary key,
   PromotionName varchar(30) not null,
   PromotionDescription varchar(255) not null,
   ImageFileName varchar(50) null
)

CREATE TABLE CarMake (
    CarMakeId int identity(1,1) not null primary key,
	CarMakeName varchar(35) not null,
	DateCreated datetime2 not null default(getdate())
)

CREATE TABLE CarModel (
    CarModelId int identity(1,1) not null primary key,
	CarMakeId int not null foreign key references CarMake(CarMakeId),
	CarModelName varchar(35) not null,
	DateCreated datetime2 not null default(getdate())
)

CREATE TABLE Car (
	CarId int identity(1,1) not null primary key,
	CategoryId int not null foreign key references Category(CategoryId),
	CarMakeId int not null foreign key references CarMake(CarMakeId),
	CarModelId int not null foreign key references CarModel(CarModelId),	
	BodyStyle varchar(75) not null,
	VinNumber char(17) not null,
	CarYear int not null,
	Transmission varchar(40) not null,
	CarColor varchar(25) not null,
	InteriorColor varchar(25) not null,
	Mileage int not null,
	CarDescription varchar(500) null,
	CarPrice decimal(10,2) not null,
	CarSalePrice decimal(10,2) null,
	ImageFileName varchar(50) null,
	IsFeatured bit not null,
	IsSold bit not null
)

CREATE TABLE PurchaseType (
	PurchaseTypeId int identity(1,1) not null primary key,	
	PurchaseTypeName varchar(35) not null,
)

CREATE TABLE SalesInvoice (
	InvoiceId int identity(1,1) not null primary key,
	CarId int not null foreign key references Car(CarId),
	StateId int not null foreign key references States(StateId),
	PurchaseTypeId int not null foreign key references PurchaseType(PurchaseTypeId),
	SalesPerson varchar(75) not null,
	UserEmail nvarchar(128) not null,
	ContactName varchar(75) not null,
	TelephoneNumber varchar(25) not null,
	StreetAddress1 varchar(75) not null,
	StreetAddress2 varchar(35) null,
	ZipCode char(5) not null,
	City varchar(35) not null,
	SaleDate datetime2 not null default(getdate()),
	Total decimal(10,2) not null
)






