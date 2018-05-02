using GuildCars.Data.Factories;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using GuildCars.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    [Authorize(Roles = "Sales")]
    public class SalesController : Controller
    {
       
        public ActionResult Index()
        {
            var repo = CarRepositoryFactory.GetRepository();

            return View(repo.GetDetails());
        }

        public ActionResult Purchase(int id)
        {
            if (Request.IsAuthenticated)
            {
                var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                var user = userMgr.FindByName(User.Identity.Name);

                ViewBag.UserName = user.UserName;
            }

            SalesPurchaseViewModel model = new SalesPurchaseViewModel();
            var carRepo = CarRepositoryFactory.GetRepository();
            var inventoryRepo = InventoryRepositoryFactory.GetRepository();

            model.Car = carRepo.GetById(id);
            model.SalesInvoice = new SalesInvoice();
            model.States = new SelectList(inventoryRepo.GetStates(), "StateId", "State");
            model.PurchaseType = new SelectList(inventoryRepo.GetPurchaseTypes(), "PurchaseTypeId", "PurchaseTypeName");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Purchase(SalesPurchaseViewModel model)
        {
            var inventoryRepo = InventoryRepositoryFactory.GetRepository();
            var carRepo = CarRepositoryFactory.GetRepository();

            decimal msrp;
            if (!Decimal.TryParse(model.SalesInvoice.Total.ToString(), out msrp))
            {
                ModelState.AddModelError("SalesInvoice.Total",
                    "Please enter a valid number.");
            }
            if (model.SalesInvoice.Total < model.Car.CarSalePrice * (decimal)0.95)
            {
                ModelState.AddModelError("SalesInvoice.Total",
                    "Purchase price cannot be less than 95% of sale price");
            }
            if (model.SalesInvoice.Total > model.Car.CarPrice)
            {
                ModelState.AddModelError("SalesInvoice.Total",
                    "Purchase price cannot be greater than MSRP");
            }
            if ((string.IsNullOrEmpty(model.SalesInvoice.UserEmail)) && (string.IsNullOrEmpty(model.SalesInvoice.TelephoneNumber)))
            {
                ModelState.AddModelError("SalesInvoice.UserEmail",
                    "Please enter an email address or phone number");
            }
            if (ModelState.IsValid)
            {

                try
                {
                    var preSale = carRepo.GetById(model.Car.CarId);

                    if (preSale.IsSold == false)
                    {
                        preSale.IsSold = true;
                    }

                    model.SalesInvoice.Car = preSale;
                    model.SalesInvoice.PurchaseTypeName = inventoryRepo.GetPurchaseTypeName(model.SalesInvoice.PurchaseTypeId);
                    model.SalesInvoice.State = inventoryRepo.GetStateName(model.SalesInvoice.StateId);


                    var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                    var user = userMgr.FindByName(User.Identity.Name);

                    model.SalesInvoice.SalesPerson = user.FirstName + " " + user.LastName;
                    model.SalesInvoice.CarId = model.SalesInvoice.Car.CarId;

                    carRepo.Update(model.SalesInvoice.Car);
                    inventoryRepo.Insert(model.SalesInvoice);
                                    

                    TempData["Message"] = "Thank you for your purchase.";
                    return RedirectToAction("Purchase");
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                if (Request.IsAuthenticated)
                {
                    var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                    var user = userMgr.FindByName(User.Identity.Name);

                    ViewBag.UserName = user.UserName;
                }

                model.Car = carRepo.GetById(model.Car.CarId);
                model.SalesInvoice = new SalesInvoice();
                model.States = new SelectList(inventoryRepo.GetStates(), "StateId", "State");
                model.PurchaseType = new SelectList(inventoryRepo.GetPurchaseTypes(), "PurchaseTypeId", "PurchaseTypeName");


                TempData["Message"] = "We were unable to complete your purchase.";
                return View(model);
            }
        }
    }
}