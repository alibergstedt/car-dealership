using GuildCars.Data.Factories;
using GuildCars.Models.Tables;
using GuildCars.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomePromoViewModel model = new HomePromoViewModel();

            model.CarDetails = CarRepositoryFactory.GetRepository().GetFeatured();
            model.Promo = PromoRepositoryFactory.GetRepository().GetAll();

            if (model != null)
            {
                return View(model);
            }

            return View();
        }

        public ActionResult Contact(string vinNumber)
        {
            ViewBag.Message = "Your contact page.";
            Contact model = new Contact();
            model.ContactMessage = vinNumber;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(Contact contact)
        {
            var repo = ContactRepositoryFactory.GetRepository();

            if ((string.IsNullOrEmpty(contact.EmailAddress)) && (string.IsNullOrEmpty(contact.TelephoneNumber)))
            {
                ModelState.AddModelError("EmailAddress",
                    "Please enter an email address or phone number");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Contact newContact = new Contact();
                    newContact.ContactId = contact.ContactId;
                    newContact.ContactName = contact.ContactName;
                    newContact.EmailAddress = contact.EmailAddress;
                    newContact.TelephoneNumber = contact.TelephoneNumber;
                    newContact.ContactMessage = contact.ContactMessage;

                    repo.Insert(newContact);

                    TempData["Message"] = "Thank you! Your message has been received. Please allow one business day for a response.";
                    return RedirectToAction("Contact");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                ViewBag.Message = "Your contact page.";

                return View();
            }


        }

        public ActionResult Specials()
        {
            var model = PromoRepositoryFactory.GetRepository().GetAll();
            return View(model);
        }
    }
}