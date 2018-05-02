USE GuildCars
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DbReset')
      DROP PROCEDURE DbReset
GO

CREATE PROCEDURE DbReset AS
BEGIN
	DELETE FROM SalesInvoice;
	DELETE FROM PurchaseType;
	DELETE FROM Car;
	DELETE FROM CarModel;
	DELETE FROM CarMake;
	DELETE FROM Promo;
	DELETE FROM States;
	DELETE FROM Category;
	DELETE FROM Contact;
	DELETE FROM AspNetUserRoles WHERE UserId in ('1473642b-98a4-4c11-8458-e161281c7fa5', '2137afc8-3133-4ced-9032-23701c4ea6f7',
	'3287179c-fe99-4b76-9697-0f39d7ea3fcc','62121730-3811-4588-8815-bcfdee5f189c','98eebcc9-95f5-46d4-88ac-3ce9afc753f6',
	'a5b45865-d689-4353-907d-1fbdc8b8073b')
	DELETE FROM AspNetUsers WHERE Id in ('1473642b-98a4-4c11-8458-e161281c7fa5', '2137afc8-3133-4ced-9032-23701c4ea6f7',
	'3287179c-fe99-4b76-9697-0f39d7ea3fcc','62121730-3811-4588-8815-bcfdee5f189c','98eebcc9-95f5-46d4-88ac-3ce9afc753f6',
	'a5b45865-d689-4353-907d-1fbdc8b8073b')

	DBCC CHECKIDENT ('car', RESEED, 1)
	DBCC CHECKIDENT ('contact', RESEED, 1)
	DBCC CHECKIDENT ('carmake', RESEED, 1)
	DBCC CHECKIDENT ('salesinvoice', RESEED, 1)

	SET IDENTITY_INSERT Contact ON;

	INSERT INTO Contact (ContactId, ContactName, EmailAddress, TelephoneNumber, ContactMessage)
	VALUES (1, 'Allison Bergstedt', 'allison@test.com', '419-706-2397', 'This is a test message'),
	(2, 'John Doe', 'john@test.com', '867-5309', 'Please contact me asap'),
	(3, 'Chris Stapleton', 'chris@test.com', '555-765-1234', 'I would like to buy a car')

	SET IDENTITY_INSERT Contact OFF;

	SET IDENTITY_INSERT Category ON;

	INSERT INTO Category (CategoryId, CategoryName)
	VALUES (1, 'Used'),
	(2, 'New')

	SET IDENTITY_INSERT Category OFF;

	SET IDENTITY_INSERT States ON;

	INSERT INTO States (StateId, [State])
	VALUES (1, 'OH'),
	(2, 'SC'),
	(3, 'CA'),
	(4, 'AL'),
	(5, 'AK'),
	(6, 'AR'),
	(7, 'CT'),
	(8, 'DE'),
	(9, 'FL'),
	(10, 'GA'),
	(11, 'IL'),
	(12, 'IN'),
	(13, 'KY'),
	(14, 'LA'),
	(15, 'MD'),
	(16, 'MA'),
	(17, 'ME'),
	(18, 'NV'),
	(19, 'NH'),
	(20, 'NJ'),
	(21, 'NY'),
	(22, 'OK'),
	(23, 'PA'),
	(24, 'TN'),
	(25, 'TX'),
	(26, 'VA'),
	(27, 'WA'),
	(28, 'WV')

	SET IDENTITY_INSERT States OFF;


	SET IDENTITY_INSERT Promo ON;

	INSERT INTO Promo (PromoCodeId, PromotionName, PromotionDescription,ImageFileName)
	VALUES (1, '$2,000 Off MSRP', 'For a limited time only, get $2,000 of suggested retail price.  Act fast! Offer ends soon.','stockpromo.jpg'),
	(2, '10% Off', 'For a limited time only, get 10% of suggested retail price.  Act fast! Offer ends soon.','stockpromo.jpg'),
	(3, '30% Off', 'For a limited time only, get 30% of suggested retail price.  Act fast! Offer ends soon.','stockpromo.jpg')

	SET IDENTITY_INSERT Promo OFF;


	SET IDENTITY_INSERT CarMake ON;

	INSERT INTO CarMake (CarMakeId, CarMakeName)
	VALUES (1, 'Ford'),
	(2, 'Chevrolet'),
	(3, 'Kia'),
	(4, 'Toyota'),
	(5, 'Dodge')

	SET IDENTITY_INSERT CarMake OFF;


	SET IDENTITY_INSERT CarModel ON;

	INSERT INTO CarModel (CarModelId, CarMakeId, CarModelName)
	VALUES (1, 1, 'Escape'),
	(2, 1, 'F150'),
	(3, 1, 'F250'),
	(4, 1, 'Explorer'),
	(5, 2, 'Silverado'),
	(6, 2, 'Corvette'),
	(7, 2, 'Volt'),
	(8, 2, 'Impala'),
	(9, 3, 'Sorento'),
	(10, 3, 'Optima'),
	(11, 3, 'Sedona'),
	(12, 3, 'Soul'),
	(13, 4, 'Corolla'),
	(14, 4, 'Highlander'),
	(15, 4, 'Prius'),
	(16, 4, 'Camry'),
	(17, 5, 'Charger'),
	(18, 5, 'Grand Caravan'),
	(19, 5, 'Ram'),
	(20, 5, 'Viper')


	SET IDENTITY_INSERT CarModel OFF;


	SET IDENTITY_INSERT Car ON;

	INSERT INTO Car (CarId, CategoryId, CarMakeId, CarModelId, BodyStyle, VinNumber, CarYear, Transmission, CarColor, InteriorColor,
	Mileage, CarDescription, CarPrice, CarSalePrice, ImageFileName, IsFeatured, IsSold)
	VALUES (1, 1, 1, 1, 'Truck', '5FNRL5H4XCB032138', 2010, 'Automatic', 'Black', 'Black',
	165000,'This is a blue car with black leather interior', 5200, null, 'stockcar.jpg', 0, 0),
	(2, 2, 2, 5, 'Car', 'JKAKZHG12CA058180', 2018, 'Manual', 'Blue', 'Gray',
	600,'This is a black car with tan leather interior', 23590, 21000, 'stockcar.jpg', 1, 0),
	(3, 1, 3, 10, 'SUV', 'WDDGF87X49F318475', 2012, 'Automatic', 'Gold', 'Tan',
	75000,'Fully loaded red car with black interior.  Clean!', 10504, 7500, 'stockcar.jpg', 1, 0),
	(4, 2, 4, 14, 'Van', '1FDWF3FY3AEA95306', 2018, 'Automatic', 'Silver', 'Brown',
	700,'Brand new! White car with black leather interior', 34607, 30000, 'stockcar.jpg', 0, 0),
	(5, 1, 5,  19, 'Truck', '1G2NE52M4TC760105', 2006, 'Manual', 'White', 'Beige',
	64567,'Clean car.  Silver with black interior. Fully loaded.', 9500, null, 'stockcar.jpg', 1, 0),
	(6, 2, 1, 2,  'Car', '2FWJAZAV86AV52956', 2018, 'Automatic', 'Black', 'Black',
	800,'Fully loaded Gold car with tan leather interior. Wood accents.', 23590, 20000, 'stockcar.jpg', 1, 0),
	(7, 1, 2, 7,  'SUV', '1FTCF10YXDPB12549', 2013, 'Automatic', 'Blue', 'Gray',
	70300,'Very nice used white car with tan interior.  Must see.', 6570, null,  'stockcar.jpg', 0, 0),
	(8, 2, 3, 11,  'Van', '3C6TR5NT7DG546452', 2018, 'Manual', 'Gold', 'Tan',
	800,'Luxury. Fast, fast, fast. Black on black details.', 22090, 14500, 'stockcar.jpg', 1, 0),
	(9, 1, 4, 16,  'Truck', '1FAHP36N39W270701', 2015, 'Automatic', 'Silver', 'Brown',
	43300,'This will not last long. Very clean.  Only 40K Miles.', 8670, null, 'stockcar.jpg', 1, 0),
	(10, 2, 5, 20, 'Car', '1GCNCPE07BZ126871', 2018, 'Automatic', 'White', 'Beige',
	800,'Fully loaded.  Green car with tan leather interior.', 32490, 26000, 'stockcar.jpg', 0, 0),
	(11, 2, 5, 20, 'SUV', '1GCNCPE07BZ126871', 2018, 'Automatic', 'Black', 'Tan',
	900,'Fully loaded.  Green car with tan leather interior.', 32490, 26000, 'stockcar.jpg', 0, 0),
	(12, 2, 5, 20, 'Van', '1GCNCPE07BZ126871', 2018, 'Automatic', 'Blue', 'Tan',
	1000,'Fully loaded.  Green car with tan leather interior.', 32490, 26000, 'stockcar.jpg', 0, 0),
	(13, 2, 5, 20, 'Truck', '1GCNCPE07BZ126871', 2018, 'Automatic', 'Gold', 'Tan',
	900,'Fully loaded.  Green car with tan leather interior.', 32490, 26000, 'stockcar.jpg', 0, 0),
	(14, 2, 5, 20, 'Car', '1GCNCPE07BZ126871', 2018, 'Automatic', 'Silver', 'Tan',
	800,'Fully loaded.  Green car with tan leather interior.', 32490, 26000, 'stockcar.jpg', 0, 0),
	(15, 2, 5, 20, 'SUV', '1GCNCPE07BZ126871', 2018, 'Automatic', 'White', 'Tan',
	800,'Fully loaded.  Green car with tan leather interior.', 32490, 26000, 'stockcar.jpg', 0, 0),
	(16, 2, 5, 20, 'Van', '1GCNCPE07BZ126871', 2018, 'Automatic', 'Black', 'Tan',
	700,'Fully loaded.  Green car with tan leather interior.', 32490, 26000, 'stockcar.jpg', 0, 0),
	(17, 2, 5, 20, 'Truck', '1GCNCPE07BZ126871', 2018, 'Automatic', 'Blue', 'Tan',
	700,'Fully loaded.  Green car with tan leather interior.', 32490, 26000, 'stockcar.jpg', 0, 0),
	(18, 2, 5, 20, 'Car', '1GCNCPE07BZ126871', 2018, 'Automatic', 'Gold', 'Tan',
	700,'Fully loaded.  Green car with tan leather interior.', 32490, 26000, 'stockcar.jpg', 0, 0),
	(19, 1, 3, 10, 'SUV', 'WDDGF87X49F318475', 2012, 'Automatic', 'Silver', 'Black',
	75000,'Fully loaded red car with black interior.  Clean!', 10504, 9700, 'stockcar.jpg', 1, 0),
	(20, 1, 3, 10, 'Van', 'WDDGF87X49F318475', 2012, 'Automatic', 'White', 'Black',
	75000,'Fully loaded red car with black interior.  Clean!', 10504, 9700, 'stockcar.jpg', 1, 0),
	(21, 1, 3, 10, 'Truck', 'WDDGF87X49F318475', 2012, 'Automatic', 'Black', 'Black',
	75000,'Fully loaded red car with black interior.  Clean!', 10504, 9700, 'stockcar.jpg', 1, 0),
	(22, 1, 3, 10, 'Car', 'WDDGF87X49F318475', 2012, 'Automatic', 'Blue', 'Black',
	75000,'Fully loaded red car with black interior.  Clean!', 10504, 9700, 'stockcar.jpg', 1, 0),
	(23, 2, 1, 3, 'SUV', '3C6TR5NT7DG546452', 2018, 'Manual', 'Gold', 'Black',
	900,'Luxury. Fast, fast, fast. Black on black details.', 22090, 14000, 'stockcar.jpg', 1, 0),
	(24, 2, 1, 3,  'Van', '3C6TR5NT7DG546452', 2018, 'Manual', 'Silver', 'Black',
	900,'Luxury. Fast, fast, fast. Black on black details.', 22090, 14000, 'stockcar.jpg', 1, 0),
	(25, 2, 1, 3, 'Truck', '3C6TR5NT7DG546452', 2018, 'Manual', 'White', 'Black',
	800,'Luxury. Fast, fast, fast. Black on black details.', 22090, 14000, 'stockcar.jpg', 1, 0),
	(26, 2, 1, 3, 'Car', '3C6TR5NT7DG546452', 2018, 'Manual', 'Black', 'Black',
	700,'Luxury. Fast, fast, fast. Black on black details.', 22090, 14000, 'stockcar.jpg', 1, 0),
	(27, 1, 2, 7, 'SUV', '1FTCF10YXDPB12549', 2013, 'Automatic', 'Blue', 'Tan',
	70300,'Very nice used white car with tan interior.  Must see.', 6570, null,  'stockcar.jpg', 0, 0),
	(28, 1, 2, 7, 'Van', '1FTCF10YXDPB12549', 2013, 'Automatic', 'Gold', 'Tan',
	70300,'Very nice used white car with tan interior.  Must see.', 6570, null,  'stockcar.jpg', 0, 0),
	(29, 1, 2, 7, 'Truck', '1FTCF10YXDPB12549', 2013, 'Automatic', 'Silver', 'Tan',
	70300,'Very nice used white car with tan interior.  Must see.', 6570, null,  'stockcar.jpg', 0, 0),
	(30, 1, 2, 7, 'Car', '1FTCF10YXDPB12549', 2013, 'Automatic', 'White', 'Tan',
	70300,'Very nice used white car with tan interior.  Must see.', 6570, null,  'stockcar.jpg', 0, 0)

	SET IDENTITY_INSERT Car OFF;


	SET IDENTITY_INSERT PurchaseType ON;

	INSERT INTO PurchaseType (PurchaseTypeId, PurchaseTypeName)
	VALUES (1, 'Dealer Finance'),
	(2, 'Cash'),
	(3, 'Bank Finance')

	SET IDENTITY_INSERT PurchaseType OFF;

	INSERT INTO AspNetUsers(Id, FirstName, LastName, Email, EmailConfirmed, PhoneNumberConfirmed, PasswordHash, SecurityStamp, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName)
	VALUES('1473642b-98a4-4c11-8458-e161281c7fa5', 'Eric', 'Wise', 'ewise@guildcars.com', 0, 0, 'AISZ+qBIkQia+o0aBx8sjv8+SNow7mqN70KRsKqUo275/acCbtpnhwRtsx7GLVcleA==', '2e313a77-4471-44ba-9f9b-c54f3607e712', 0, 1, 0, 'ewise@guildcars.com');

	INSERT INTO AspNetUsers(Id, FirstName, LastName, Email, EmailConfirmed, PhoneNumberConfirmed, PasswordHash, SecurityStamp, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName)
	VALUES('2137afc8-3133-4ced-9032-23701c4ea6f7', 'Corbin', 'March', 'cmarch@guildcars.com', 0, 0, 'ANfQ9YPeMafclULT8GloPfOqMMZEsKrpkHggZc0qVBQRHPqHZSIoY/g18osOUS1mfQ==', 'a046bf12-b9b4-475d-aeda-e40fb5b2b893', 0, 1, 0, 'cmarch@guildcars.com');

	INSERT INTO AspNetUsers(Id, FirstName, LastName, Email, EmailConfirmed, PhoneNumberConfirmed, PasswordHash, SecurityStamp, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName)
	VALUES('3287179c-fe99-4b76-9697-0f39d7ea3fcc', 'Eric', 'Ward', 'eward@guildcars.com', 0, 0, 'AJsRlr7n6B3mFheiLUmhegzV2CEwilvZWYZweApCewEA/9gVFROTApsTbZkppcRtTQ==', 'ff66ecb9-454e-4752-a256-407c7fc7a97a', 0, 1, 0, 'eward@guildcars.com');

	INSERT INTO AspNetUsers(Id, FirstName, LastName, Email, EmailConfirmed, PhoneNumberConfirmed, PasswordHash, SecurityStamp, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName)
	VALUES('62121730-3811-4588-8815-bcfdee5f189c', 'Victor', 'Pudelski', 'vpudelski@guildcars.com', 0, 0, 'AP4nkiGSzme1ZQ8t3MkZ5En+eJMN8uhjuDpwDP9x5wvCquY/AG0LGXHClMvVl32ufQ==', '98744eab-66fa-4f0f-9f9e-c80c1acaefae', 0, 1, 0, 'vpudelski@guildcars.com');

	INSERT INTO AspNetUsers(Id, FirstName, LastName, Email, EmailConfirmed, PhoneNumberConfirmed, PasswordHash, SecurityStamp, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName)
	VALUES('98eebcc9-95f5-46d4-88ac-3ce9afc753f6', 'Allison', 'Bergstedt', 'abergstedt@guildcars.com', 0, 0, 'AKJaylmNLqZgQHHuTK7tknXs5SjRIg98ZofxnlB08UeKbQ4FOhrdwop2zBU2sdZtuA==', 'accf2e77-570f-40bd-a411-e84b19c9f506', 0, 1, 0, 'abergstedt@guildcars.com');

	INSERT INTO AspNetUsers(Id, FirstName, LastName, Email, EmailConfirmed, PhoneNumberConfirmed, PasswordHash, SecurityStamp, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName)
	VALUES('a5b45865-d689-4353-907d-1fbdc8b8073b', 'Austyn', 'Hill', 'ahill@guildcars.com', 0, 0, 'AAR5Nic8rMuxM255xdqbx0LMH47jQc1cg7xeH49HUMi0E1IbBttrAYYXsGzoLtdRwQ==', 'ce8f5169-f15b-4fc2-a4c9-05087c6ee194', 0, 1, 0, 'ahill@guildcars.com');

	
	INSERT INTO AspNetUserRoles(UserId, RoleId)
	VALUES ('1473642b-98a4-4c11-8458-e161281c7fa5', '761aba7b-48a3-40d4-9a7a-2d5eab6d4737');

	INSERT INTO AspNetUserRoles(UserId, RoleId)
	VALUES ('2137afc8-3133-4ced-9032-23701c4ea6f7', '80f11668-0943-4bd2-9917-11db2d5e7898');

	INSERT INTO AspNetUserRoles(UserId, RoleId)
	VALUES ('3287179c-fe99-4b76-9697-0f39d7ea3fcc', '80f11668-0943-4bd2-9917-11db2d5e7898');

	INSERT INTO AspNetUserRoles(UserId, RoleId)
	VALUES ('62121730-3811-4588-8815-bcfdee5f189c', '80f11668-0943-4bd2-9917-11db2d5e7898');

	INSERT INTO AspNetUserRoles(UserId, RoleId)
	VALUES ('a5b45865-d689-4353-907d-1fbdc8b8073b', '80f11668-0943-4bd2-9917-11db2d5e7898');

	INSERT INTO AspNetUserRoles(UserId, RoleId)
	VALUES ('98eebcc9-95f5-46d4-88ac-3ce9afc753f6', '225fdf7e-7c34-4549-a712-f59baf1c652c');

	SET IDENTITY_INSERT SalesInvoice ON;

	INSERT INTO SalesInvoice (InvoiceId, CarId, StateId, PurchaseTypeId, SalesPerson, ZipCode, UserEmail, ContactName,
	TelephoneNumber, StreetAddress1, StreetAddress2, City, Total)
	VALUES (1, 2, 3, 1, 'Eric Ward', '90210', 'chris@test.com', 'Chris Stapleton', '555-867-5309', '123 Somewhere Ave', 'Suite 2', 'Hollywood', 37116),
	(2, 2, 2, 2, 'Austyn Hill', '29401', 'allison@test.com', 'Allison Bergstedt', '519-706-2397', '636 Old State Rd', null, 'North Fairfield', 23155),
	(3, 3, 1, 3, 'Corbin March', '90210', 'chris@test.com', 'Chris Stapleton', '555-867-5309', '123 Somewhere Ave', 'Suite 2', 'Hollywood', 2300),
	(4, 4, 3, 1, 'Victor Pudelski', '29401', 'allison@test.com', 'Allison Bergstedt', '519-706-2397', '636 Old State Rd', null, 'North Fairfield', 42350),
	(5, 5, 2, 2, 'Eric Ward', '90210', 'chris@test.com', 'Chris Stapleton', '555-867-5309', '123 Somewhere Ave', 'Suite 2', 'Hollywood', 37116),
	(6, 6, 1, 3, 'Austyn Hill', '29401', 'allison@test.com', 'Allison Bergstedt', '519-706-2397', '636 Old State Rd', null, 'North Fairfield', 23160),
	(7, 7, 3, 1, 'Corbin March', '90210', 'chris@test.com', 'Chris Stapleton', '555-867-5309', '123 Somewhere Ave', 'Suite 2', 'Hollywood', 37116),
	(8, 8, 2, 2, 'Austyn Hill', '29401', 'allison@test.com', 'Allison Bergstedt', '519-706-2397', '636 Old State Rd', null, 'North Fairfield', 23155),
	(9, 9, 1, 3, 'Eric Ward', '90210', 'chris@test.com', 'Chris Stapleton', '555-867-5309', '123 Somewhere Ave', 'Suite 2', 'Hollywood', 3450),
	(10, 10, 3, 1, 'Corbin March', '29401', 'allison@test.com', 'Allison Bergstedt', '519-706-2397', '636 Old State Rd', null, 'North Fairfield', 9670),
	(11, 11, 2, 2, 'Victor Pudelski', '90210', 'chris@test.com', 'Chris Stapleton', '555-867-5309', '123 Somewhere Ave', 'Suite 2', 'Hollywood', 37116),
	(12, 12, 1, 3, 'Austyn Hill', '29401', 'allison@test.com', 'Allison Bergstedt', '519-706-2397', '636 Old State Rd', null, 'North Fairfield', 24560),
	(13, 13, 3, 1, 'Eric Ward', '90210', 'chris@test.com', 'Chris Stapleton', '555-867-5309', '123 Somewhere Ave', 'Suite 2', 'Hollywood', 37116),
	(14, 14, 2, 2, 'Corbin March', '29401', 'allison@test.com', 'Allison Bergstedt', '519-706-2397', '636 Old State Rd', null, 'North Fairfield', 23155),
	(15, 15, 1, 3, 'Eric Ward', '90210', 'chris@test.com', 'Chris Stapleton', '555-867-5309', '123 Somewhere Ave', 'Suite 2', 'Hollywood', 35670),
	(16, 16, 3, 1, 'Victor Pudelski', '29401', 'allison@test.com', 'Allison Bergstedt', '519-706-2397', '636 Old State Rd', null, 'North Fairfield', 21234),
	(17, 17, 2, 2, 'Eric Ward', '90210', 'chris@test.com', 'Chris Stapleton', '555-867-5309', '123 Somewhere Ave', 'Suite 2', 'Hollywood', 15670),
	(18, 18, 1, 3, 'Corbin March', '29401', 'allison@test.com', 'Allison Bergstedt', '519-706-2397', '636 Old State Rd', null, 'North Fairfield', 13457),
	(19, 19, 3, 1, 'Eric Ward', '90210', 'chris@test.com', 'Chris Stapleton', '555-867-5309', '123 Somewhere Ave', 'Suite 2', 'Hollywood', 17680),
	(20, 20, 2, 2, 'Victor Pudelski', '29401', 'allison@test.com', 'Allison Bergstedt', '519-706-2397', '636 Old State Rd', null, 'North Fairfield', 17543),
	(21, 21, 1, 3, 'Victor Pudelski', '90210', 'chris@test.com', 'Chris Stapleton', '555-867-5309', '123 Somewhere Ave', 'Suite 2', 'Hollywood', 16534),
	(22, 22, 3, 1, 'Austyn Hill', '29401', 'allison@test.com', 'Allison Bergstedt', '519-706-2397', '636 Old State Rd', null, 'North Fairfield', 13645),
	(23, 23, 2, 2, 'Eric Ward', '90210', 'chris@test.com', 'Chris Stapleton', '555-867-5309', '123 Somewhere Ave', 'Suite 2', 'Hollywood', 12378),
	(24, 24, 1, 3, 'Corbin March', '29401', 'allison@test.com', 'Allison Bergstedt', '519-706-2397', '636 Old State Rd', null, 'North Fairfield', 23155.28),
	(25, 25, 3, 1, 'Eric Ward', '90210', 'chris@test.com', 'Chris Stapleton', '555-867-5309', '123 Somewhere Ave', 'Suite 2', 'Hollywood', 37116.01),
	(26, 26, 2, 2, 'Austyn Hill', '29401', 'allison@test.com', 'Allison Bergstedt', '519-706-2397', '636 Old State Rd', null, 'North Fairfield', 23155.28)

	SET IDENTITY_INSERT SalesInvoice OFF;

	
END