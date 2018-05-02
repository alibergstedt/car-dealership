using GuildCars.Data.Factories;
using GuildCars.Models.Queries;
using GuildCars.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Inventory()
        {
            InventoryReportItem model = new InventoryReportItem();

            var repo = InventoryRepositoryFactory.GetRepository();

            model = GetReportsInventoryModel(model);

            return View(model);
        }

        private InventoryReportItem GetReportsInventoryModel(InventoryReportItem model)
        {
            var repo = InventoryRepositoryFactory.GetRepository();
            
            model.inventoryNew = repo.GetNewVehicles();
            model.inventoryUsed = repo.GetUsedVehicles();
            
            return model;
        }

        public ActionResult Sales()
        {
            SalesReportViewModel model = new SalesReportViewModel();
            var repo = InventoryRepositoryFactory.GetRepository();
            model.SalesPerson = new SelectList(repo.SalesPersonSelectAll(), "SalesPerson", "SalesPerson");

            return View(model);
        }
    }
}