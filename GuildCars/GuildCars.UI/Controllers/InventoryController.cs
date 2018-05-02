using GuildCars.Data.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class InventoryController : Controller
    {
        // GET: Inventory
        public ActionResult Details(int id)
        {
            var repo = CarRepositoryFactory.GetRepository();
            var model = repo.GetById(id);

            return View(model);
        }

        public ActionResult New()
        {
            var repo = CarRepositoryFactory.GetRepository();

            return View(repo.GetDetails());
        }

        public ActionResult Used()
        {
            var repo = CarRepositoryFactory.GetRepository();

            return View(repo.GetDetails());
        }
    }
}