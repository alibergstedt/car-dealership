using GuildCars.Data.ADO;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Test.IntegrationTests
{
    [TestFixture]
    public class AdoTests
    {
        [SetUp]
        public void Init()
        {
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "DbReset";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Connection = cn;
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        [Test]
        public void CanInsertSalesInvoice()
        {
            SalesInvoice newInvoice = new SalesInvoice();
            var repo = new InventoryRepositoryADO();

            newInvoice.CarId = 23;
            newInvoice.StateId = 1;
            newInvoice.ZipCode = "44857";
            newInvoice.PurchaseTypeId = 1;
            newInvoice.UserEmail = "ewise@guildcars.com";
            newInvoice.ContactName = "Amy Samsonite";
            newInvoice.SalesPerson = "Austyn Hill";
            newInvoice.TelephoneNumber = "356-555-4321";
            newInvoice.StreetAddress1 = "123 Somewhere Street";
            newInvoice.StreetAddress2 = "Apt 2";
            newInvoice.City = "Norwalk";
            newInvoice.PurchaseTypeId = 1;
            newInvoice.Total = 15050M;

            repo.Insert(newInvoice);

            Assert.AreEqual(27, newInvoice.InvoiceId);
        }

        [Test]
        public void CanGetListOfStates()
        {
            var repo = new InventoryRepositoryADO();

            var states = repo.GetStates();

            Assert.AreEqual(28, states.Count);

            Assert.AreEqual("AK", states[0].State);
            Assert.AreEqual(5, states[0].StateId);
        }

        [Test]
        public void CanGetListOfPurchaseTypes()
        {
            var repo = new InventoryRepositoryADO();

            var types = repo.GetPurchaseTypes();

            Assert.AreEqual(3, types.Count);

            Assert.AreEqual("Bank Finance", types[0].PurchaseTypeName);
        }

        [Test]
        public void CanLoadContacts()
        {
            var repo = new ContactRepositoryADO();

            var contacts = repo.GetAll();

            Assert.AreEqual(3, contacts.Count);

            Assert.AreEqual("Allison Bergstedt", contacts[0].ContactName);
            Assert.AreEqual(1, contacts[0].ContactId);
            Assert.AreEqual("This is a test message", contacts[0].ContactMessage);
            Assert.AreEqual("419-706-2397", contacts[0].TelephoneNumber);
            Assert.AreEqual("allison@test.com", contacts[0].EmailAddress);
        }

        [Test]
        public void CanAddContact()
        {
            Contact contactToAdd = new Contact();
            var repo = new ContactRepositoryADO();
            
            contactToAdd.ContactName = "Jane Austin";
            contactToAdd.EmailAddress = "jane@test.com";
            contactToAdd.TelephoneNumber = "419-577-1358";
            contactToAdd.ContactMessage = "This is a test contact";

            repo.Insert(contactToAdd);
            
            Assert.AreEqual(4, contactToAdd.ContactId);
            Assert.AreEqual("Jane Austin", contactToAdd.ContactName);
            Assert.AreEqual("jane@test.com", contactToAdd.EmailAddress);
            Assert.AreEqual("419-577-1358", contactToAdd.TelephoneNumber);
            Assert.AreEqual("This is a test contact", contactToAdd.ContactMessage);
            
        }


        [Test]
        public void CanLoadPromos()
        {
            var repo = new PromoRepositoryADO();

            var promos = repo.GetAll();

            Assert.AreEqual(3, promos.Count);

            Assert.AreEqual(2, promos[1].PromoCodeId);
            Assert.AreEqual("10% Off", promos[1].PromotionName);
            Assert.AreEqual("For a limited time only, get 10% of suggested retail price.  Act fast! Offer ends soon.", promos[1].PromotionDescription);
            Assert.AreEqual("stockpromo.jpg", promos[1].ImageFileName);

        }

        [Test]
        public void CanLoadCarById()
        {
            var repo = new CarRepositoryADO();

            var car = repo.GetById(1);

            Assert.IsNotNull(car);

            Assert.AreEqual(1, car.CarId);
            Assert.AreEqual("5FNRL5H4XCB032138", car.VinNumber);
            Assert.AreEqual(2010, car.CarYear);
            Assert.AreEqual("Automatic", car.Transmission);
            Assert.AreEqual("Black", car.CarColor);
            Assert.AreEqual("Black", car.InteriorColor);
            Assert.AreEqual(165000, car.Mileage);
            Assert.AreEqual("This is a blue car with black leather interior", car.CarDescription);
            Assert.AreEqual(5200M, car.CarPrice);
            Assert.AreEqual("stockcar.jpg", car.ImageFileName);

        }


        [Test]
        public void NotFoundCarReturnsNull()
        {
            var repo = new CarRepositoryADO();

            var car = repo.GetById(100000);

            Assert.IsNull(car);
        }

        [Test]
        public void CanAddMake()
        {
            CarMake carMakeToAdd = new CarMake();
            var repo = new MakeModelRepositoryADO();
            
            carMakeToAdd.CarMakeName = "Nissan";

            repo.AddMake(carMakeToAdd);

            Assert.AreEqual(6, carMakeToAdd.CarMakeId);
        }

        [Test]
        public void CanAddCar()
        {
            Car carToAdd = new Car();
            var repo = new CarRepositoryADO();

            carToAdd.CarColor = "Blue";
            carToAdd.CarDescription = "This is a test car";
            carToAdd.CarMakeId = 1;
            carToAdd.CarModelId = 4;
            carToAdd.BodyStyle = "Sedan";
            carToAdd.CarPrice = 3500M;
            carToAdd.CarSalePrice = 2500M;
            carToAdd.CarYear = 2009;
            carToAdd.CategoryId = 1;
            carToAdd.InteriorColor = "Black";
            carToAdd.IsFeatured = true;
            carToAdd.Mileage = 135000;
            carToAdd.Transmission = "Automatic";
            carToAdd.VinNumber = "1G4AH5346S6460154";
            carToAdd.ImageFileName = "stockcar.jpg";
            carToAdd.IsSold = false;

            repo.Insert(carToAdd);

            Assert.AreEqual(31, carToAdd.CarId);
                        
        }

        [Test]
        public void CanUpdateCar()
        {
            Car carToAdd = new Car();
            var repo = new CarRepositoryADO();

            carToAdd.CarColor = "Blue";
            carToAdd.CarDescription = "This is a test car";
            carToAdd.CarMakeId = 1;
            carToAdd.CarModelId = 4;
            carToAdd.BodyStyle = "Sedan";
            carToAdd.CarPrice = 3500M;
            carToAdd.CarSalePrice = 2500M;
            carToAdd.CarYear = 2009;
            carToAdd.CategoryId = 1;
            carToAdd.InteriorColor = "Black";
            carToAdd.IsFeatured = true;
            carToAdd.Mileage = 135000;
            carToAdd.Transmission = "Automatic";
            carToAdd.VinNumber = "1G4AH5346S6460154";
            carToAdd.ImageFileName = "stockcar.jpg";
            carToAdd.IsSold = false;

            repo.Insert(carToAdd);

            carToAdd.CarColor = "Red";
            carToAdd.InteriorColor = "Tan";
            carToAdd.CarDescription = "This is an updated car";
            carToAdd.IsFeatured = false;

            repo.Update(carToAdd);
            
            var updatedCar = repo.GetById(31);

            Assert.AreEqual("Red", updatedCar.CarColor);
            Assert.AreEqual("Tan", updatedCar.InteriorColor);
            Assert.AreEqual("This is an updated car", updatedCar.CarDescription);
            Assert.AreEqual(false, updatedCar.IsFeatured);
            
        }

        [Test]
        public void CanDeleteCar()
        {
            Car carToAdd = new Car();
            var repo = new CarRepositoryADO();

            carToAdd.CarColor = "Blue";
            carToAdd.CarDescription = "This is a test car";
            carToAdd.CarMakeId = 1;
            carToAdd.CarModelId = 4;
            carToAdd.BodyStyle = "Sedan";
            carToAdd.CarPrice = 3500M;
            carToAdd.CarSalePrice = 2500M;
            carToAdd.CarYear = 2009;
            carToAdd.CategoryId = 1;
            carToAdd.InteriorColor = "Black";
            carToAdd.IsFeatured = true;
            carToAdd.Mileage = 135000;
            carToAdd.Transmission = "Automatic";
            carToAdd.VinNumber = "1G4AH5346S6460154";
            carToAdd.ImageFileName = "stockcar.jpg";
            carToAdd.IsSold = false;

            repo.Insert(carToAdd);

            var loaded = repo.GetById(31);
            Assert.IsNotNull(loaded);

            repo.Delete(31);
            loaded = repo.GetById(31);

            Assert.IsNull(loaded);

        }

        [Test]
        public void CanLoadFavoriteCars()
        {
            var repo = new CarRepositoryADO();
            List<CarDetailsFeaturedItem> cars = repo.GetFeatured().ToList();

            Assert.AreEqual(8, cars.Count());

            Assert.AreEqual(3, cars[1].CarId);
            Assert.AreEqual("Kia", cars[1].CarMakeName);
            Assert.AreEqual("Optima", cars[1].CarModelName);
            Assert.AreEqual(10504M, cars[1].CarPrice);
            Assert.AreEqual(2012, cars[1].CarYear);
            Assert.AreEqual("stockcar.jpg", cars[1].ImageFileName);
        }


        [Test]
        public void CanLoadCarDetails()
        {
            var repo = new CarRepositoryADO();

            List<CarDetailsItem> car = repo.GetDetails().ToList();

            Assert.IsNotNull(car);

            Assert.AreEqual(1, car[0].CarId);
            Assert.AreEqual("Ford", car[0].CarMakeName);
            Assert.AreEqual("Escape", car[0].CarModelName);
            Assert.AreEqual("5FNRL5H4XCB032138", car[0].VinNumber);
            Assert.AreEqual(2010, car[0].CarYear);
            Assert.AreEqual("Automatic", car[0].Transmission);
            Assert.AreEqual("Black", car[0].CarColor);
            Assert.AreEqual("Black", car[0].InteriorColor);
            Assert.AreEqual(165000, car[0].Mileage);
            Assert.AreEqual("This is a blue car with black leather interior", car[0].CarDescription);
            Assert.AreEqual(5200M, car[0].CarPrice);
            Assert.AreEqual("stockcar.jpg", car[0].ImageFileName);

        }


        [Test]
        public void CanLoadUsedIventory()
        {
            var repo = new InventoryRepositoryADO();
            List<InventoryReportItem> cars = repo.GetUsedVehicles().ToList();

            Assert.AreEqual(5, cars.Count());
            
            Assert.AreEqual("Dodge", cars[0].CarMakeName);
            Assert.AreEqual("Ram", cars[0].CarModelName);
            Assert.AreEqual(2006, cars[0].CarYear);
            Assert.AreEqual(1, cars[0].Count);
            Assert.AreEqual(9500M, cars[0].StockValue);

        }


        [Test]
        public void CanLoadNewIventory()
        {
            var repo = new InventoryRepositoryADO();
            List<InventoryReportItem> cars = repo.GetNewVehicles().ToList();

            Assert.AreEqual(6, cars.Count());

            Assert.AreEqual("Chevrolet", cars[0].CarMakeName);
            Assert.AreEqual("Silverado", cars[0].CarModelName);
            Assert.AreEqual(2018, cars[0].CarYear);
            Assert.AreEqual(1, cars[0].Count);
            Assert.AreEqual(23590M, cars[0].StockValue);

        }

        [Test]
        public void CanSearchNewInventoryOnMinPrice()
        {
            var repo = new CarRepositoryADO();

            var found = repo.SearchNewInventory(new CarSearchParameters { MinPrice = 8000M });

            Assert.AreEqual(17, found.Count());
        }

        [Test]
        public void CanSearchNewInventoryOnMaxPrice()
        {
            var repo = new CarRepositoryADO();

            var found = repo.SearchNewInventory(new CarSearchParameters { MaxPrice = 17000M });

            Assert.AreEqual(5, found.Count());
        }

        [Test]
        public void CanSearchUsedInventoryOnMaxPrice()
        {
            var repo = new CarRepositoryADO();

            var found = repo.SearchUsedInventory(new CarSearchParameters { MaxPrice = 9000M });

            Assert.AreEqual(8, found.Count());
        }

        [Test]
        public void CanSearchOnPriceRange()
        {
            var repo = new CarRepositoryADO();

            var found = repo.SearchAll(new CarSearchParameters { MaxPrice = 20000M, MinPrice = 8000M });

            Assert.AreEqual(13, found.Count());
        }

        [Test]
        public void CanSearchNewInventoryOnCarMakeName()
        {
            var repo = new CarRepositoryADO();

            var found = repo.SearchNewInventory(new CarSearchParameters { CarMakeName = "Toyota" });

            Assert.AreEqual(1, found.Count());
        }

        [Test]
        public void CanSearchSalesInvoiceBySalesPerson()
        {
            var repo = new InventoryRepositoryADO();

            var found = repo.SearchSalesInvoices(new SalesReportSearchParameters { SalesPerson = "Eric Ward"});

            Assert.AreEqual(1, found.Count());
        }

        [Test]
        public void GetCarModelByMake()
        {
            var repo = new MakeModelRepositoryADO();

            var found = repo.GetModelByMake(2);

            Assert.AreEqual(4, found.Count());
        }

        [Test]
        public void CanLoadCategory()
        {
            var repo = new CarRepositoryADO();

            var category = repo.GetCarCategory();

            Assert.AreEqual(2, category.Count);

            Assert.AreEqual(2, category[0].CategoryId);
            Assert.AreEqual("New", category[0].CategoryName);
        }


        [Test]
        public void CanLoadBodyStyle()
        {
            var repo = new CarRepositoryADO();

            var bodyStyle = repo.GetBodyStyle();

            Assert.AreEqual(4, bodyStyle.Count);

            Assert.AreEqual("Car", bodyStyle[0].BodyStyle);
            Assert.AreEqual("SUV", bodyStyle[1].BodyStyle);
        }

        [Test]
        public void CanLoadTransmission()
        {
            var repo = new CarRepositoryADO();

            var transmission = repo.GetTransmission();

            Assert.AreEqual(2, transmission.Count);

            Assert.AreEqual("Automatic", transmission[0].Transmission);
            Assert.AreEqual("Manual", transmission[1].Transmission);
        }

        [Test]
        public void CanLoadCarColor()
        {
            var repo = new CarRepositoryADO();

            var carColor = repo.GetCarColor();

            Assert.AreEqual(5, carColor.Count);

            Assert.AreEqual("Black", carColor[0].CarColor);
            Assert.AreEqual("Blue", carColor[1].CarColor);
        }

        [Test]
        public void CanLoadInteriorColor()
        {
            var repo = new CarRepositoryADO();

            var interiorColor = repo.GetInteriorColor();

            Assert.AreEqual(5, interiorColor.Count);

            Assert.AreEqual("Beige", interiorColor[0].InteriorColor);
            Assert.AreEqual("Black", interiorColor[1].InteriorColor);
        }

        [Test]
        public void CanLoadCarMake()
        {
            var repo = new MakeModelRepositoryADO();

            var carMake = repo.GetMakes();

            Assert.AreEqual(5, carMake.Count);

            Assert.AreEqual("Chevrolet", carMake[0].CarMakeName);
            Assert.AreEqual(2, carMake[0].CarMakeId);
            Assert.AreEqual("Dodge", carMake[1].CarMakeName);
            Assert.AreEqual(5, carMake[1].CarMakeId);
        }

        [Test]
        public void CanLoadCarModel()
        {
            var repo = new MakeModelRepositoryADO();

            var carModel = repo.GetModels();

            Assert.AreEqual(20, carModel.Count);

            Assert.AreEqual("Corvette", carModel[0].CarModelName);
            Assert.AreEqual("Impala", carModel[1].CarModelName);
        }
    }
}
