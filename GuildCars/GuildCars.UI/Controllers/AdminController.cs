using GuildCars.Data.ADO;
using GuildCars.Data.Factories;
using GuildCars.Models.Tables;
using GuildCars.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GuildCars.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        ApplicationDbContext context;

        public AdminController()
        {
            context = new ApplicationDbContext();
        }

        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users()
        {
            var context = new ApplicationDbContext();

            var usersWithRoles = (from user in context.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      FirstName = user.FirstName,
                                      LastName = user.LastName,
                                      Email = user.Email,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in context.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new UserRoleViewModel()

                                  {
                                      UserId = p.UserId,
                                      FirstName = p.FirstName,
                                      LastName = p.LastName,
                                      Email = p.Email,
                                      Roles = string.Join(",", p.RoleNames)
                                  });


            return View(usersWithRoles);
        }

        [AllowAnonymous]
        public ActionResult AddUser()
        {
            var context = new ApplicationDbContext();

            ViewBag.Name = new SelectList(context.Roles.ToList(), "Name", "Name");

            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddUser(UserRoleViewModel model)
        {

            ViewBag.Name = new SelectList(context.Roles.ToList(), "Name", "Name");

            if (!String.IsNullOrEmpty(model.Password) && String.IsNullOrEmpty(model.ConfirmPassword))
            {
                ModelState.AddModelError("PasswordConfirm", "Please confirm the new password you entered.");
            }
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("PasswordConfirm", "Passwords must match.");
            }

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    await this.UserManager.AddToRoleAsync(user.Id, model.Roles);

                    return RedirectToAction("Index", "Home");
                }                

                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        public ActionResult EditUser(string id)
        {
            ApplicationUser appUser = new ApplicationUser();
            appUser = UserManager.FindById(id);
            UserRoleViewModel user = new UserRoleViewModel();
            user.FirstName = appUser.FirstName;
            user.LastName = appUser.LastName;
            user.Email = appUser.Email;
            ViewBag.Name = new SelectList(context.Roles.ToList(), "Name", "Name");

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(UserRoleViewModel model)
        {
            
            ViewBag.Name = new SelectList(context.Roles.ToList(), "Name", "Name");

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var manager = new UserManager<ApplicationUser>(store);
            var currentUser = manager.FindByEmail(model.Email);
            currentUser.FirstName = model.FirstName;
            currentUser.LastName = model.LastName;
            currentUser.Email = model.Email;
            var rolename = UserManager.GetRoles(currentUser.Id);

            foreach(var role in rolename)
            {
                IdentityResult deletionResult = await UserManager.RemoveFromRoleAsync(currentUser.Id, role);
            }
            
            await UserManager.AddToRoleAsync(currentUser.Id, model.Roles);

            if (!String.IsNullOrEmpty(model.Password) && String.IsNullOrEmpty(model.ConfirmPassword))
            {
                ModelState.AddModelError("PasswordConfirm", "Please confirm the new password you entered.");
            }
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("PasswordConfirm", "Passwords must match.");
            }
            if (model.Password != null)
            {
                var result = UserManager.ChangePasswordAsync(currentUser.Id, currentUser.PasswordHash, model.Password);
            }

            manager.Update(currentUser);

            TempData["Message"] = "Profile Changes Saved !";
            return RedirectToAction("Users");
        }

        public ActionResult Specials()
        {
            var model = new PromoViewModel();
            model = GetAdminSpecialsModel(model);

            return View(model);
        }

        private PromoViewModel GetAdminSpecialsModel(PromoViewModel model)
        {
            var repo = PromoRepositoryFactory.GetRepository();
            model.Promos = repo.GetAll();

            return model;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Specials(PromoViewModel promo)
        {
            if (Request.IsAuthenticated)
            {
                var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var user = userMgr.FindByName(User.Identity.Name);
                ViewBag.UserId = user.Id;
            }

            var repo = PromoRepositoryFactory.GetRepository();

            if (ModelState.IsValid)
            {
                try
                {
                    Promo newPromo = new Promo();
                    newPromo.PromotionName = promo.PromotionName;
                    newPromo.PromotionDescription = promo.PromotionDescription;
                    newPromo.ImageFileName = promo.ImageFileName;

                    repo.Insert(newPromo);

                    TempData["Message"] = "Promotion added.";
                    return RedirectToAction("Specials", "Admin");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                promo.Promos = repo.GetAll();
                return View(promo);
            }

        }


        [HttpPost]
        public ActionResult DeleteSpecial(int promoCodeId)
        {
            var repo = PromoRepositoryFactory.GetRepository();
            repo.Delete(promoCodeId);

            return RedirectToAction("Specials");
        }


        public ActionResult AddVehicle()
        {
            var model = new AddVehicleViewModel();

            model = GetAddVehicleModel(model);

            return View(model);
        }

        private AddVehicleViewModel GetAddVehicleModel(AddVehicleViewModel model)
        {
            var carRepo = CarRepositoryFactory.GetRepository();
            var makeModelRepo = MakeModelRepositoryFactory.GetRepository();

            model.Car = new Car();
            model.CarMakeName = new SelectList(makeModelRepo.GetMakes(), "CarMakeId", "CarMakeName");
            model.CarModelName = new SelectList(makeModelRepo.GetModels(), "CarModelId", "CarModelName");
            model.CategoryName = new SelectList(carRepo.GetCarCategory(), "CategoryId", "CategoryName");
            model.BodyStyle = new SelectList(carRepo.GetBodyStyle(), "BodyStyle", "BodyStyle");
            model.Transmission = new SelectList(carRepo.GetTransmission(), "Transmission", "Transmission");
            model.CarColor = new SelectList(carRepo.GetCarColor(), "CarColor", "CarColor");
            model.InteriorColor = new SelectList(carRepo.GetInteriorColor(), "InteriorColor", "InteriorColor");
            if (model.IsSold == null)
            {
                model.IsSold = false;
            }
            if (model.IsFeatured == null)
            {
                model.IsFeatured = false;
            }

            return model;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddVehicle(AddVehicleViewModel model)
        {
            var repo = CarRepositoryFactory.GetRepository();

            int carMileage;
            decimal carSalePrice;
            int carYear;
            decimal msrp;
            if (!Decimal.TryParse(model.Car.CarPrice.ToString(), out msrp))
            {
                ModelState.AddModelError("Car.CarPrice",
                    "Please enter a valid number.");
            }
            if (!Int32.TryParse(model.Car.Mileage.ToString(), out carMileage))
            {
                ModelState.AddModelError("Car.Mileage",
                    "Please enter a valid number.");
            }
            if (!Decimal.TryParse(model.Car.CarSalePrice.ToString(), out carSalePrice))
            {
                ModelState.AddModelError("Car.CarSalePrice",
                    "Please enter a valid number.");
            }
            if (!Int32.TryParse(model.Car.CarYear.ToString(), out carYear))
            {
                ModelState.AddModelError("Car.CarYear",
                    "Please enter a valid number.");
            }
            if (model.Car.CarYear.ToString().Length < 4)
            {
                ModelState.AddModelError("Car.CarYear",
                    "Please enter a four digit year.");
            }
            if (model.Car.CarYear < 2000 || model.Car.CarYear > DateTime.Now.Year + 1 )
            {
                ModelState.AddModelError("Car.CarYear",
                    "Car year can not be less than year 2000 or greater than next year.");
            }
            if (model.ImageUpload == null)
            {
                ModelState.AddModelError("Car.ImageFileName",
                    "Please upload an image of the vehicle.");
            }
            if ((model.Car.CategoryId == 2) && (model.Car.CarYear >= DateTime.Now.Year))
            {
                if(model.Car.Mileage > 1000)
                {
                    ModelState.AddModelError("Car.Mileage",
                        "Mileage must be below 1000 miles for new vehicles.");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (model.IsSold == null)
                    {
                        model.Car.IsSold = false;
                    }
                    if (model.IsFeatured == null)
                    {
                        model.IsFeatured = false;
                    }

                    repo.Insert(model.Car);

                    if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                    {
                        var savepath = Server.MapPath("~/Images");

                        string fileName = Path.GetFileNameWithoutExtension("inventory");
                        string extension = Path.GetExtension(model.ImageUpload.FileName);
                        if(extension != "png" || extension != "jpg" || extension != "jpeg")
                        {
                            ModelState.AddModelError("Car.ImageFileName",
                                "Please upload a png, jpg, or jpeg file.");
                        }
                        var filePath = Path.Combine(savepath, fileName + model.Car.CarId + extension);

                        model.ImageUpload.SaveAs(filePath);
                        model.Car.ImageFileName = Path.GetFileName(filePath);
                    }

                    repo.Update(model.Car);

                    TempData["Message"] = "New vehicle has been added.";
                    return RedirectToAction("EditVehicle", new { id = model.Car.CarId });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                var carRepo = CarRepositoryFactory.GetRepository();
                var makeModelRepo = MakeModelRepositoryFactory.GetRepository();

                model.Car = new Car();
                model.CarMakeName = new SelectList(makeModelRepo.GetMakes(), "CarMakeId", "CarMakeName");
                model.CarModelName = new SelectList(makeModelRepo.GetModels(), "CarModelId", "CarModelName");
                model.CategoryName = new SelectList(carRepo.GetCarCategory(), "CategoryId", "CategoryName");
                model.BodyStyle = new SelectList(carRepo.GetBodyStyle(), "BodyStyle", "BodyStyle");
                model.Transmission = new SelectList(carRepo.GetTransmission(), "Transmission", "Transmission");
                model.CarColor = new SelectList(carRepo.GetCarColor(), "CarColor", "CarColor");
                model.InteriorColor = new SelectList(carRepo.GetInteriorColor(), "InteriorColor", "InteriorColor");
                if (model.IsSold == null)
                {
                    model.IsSold = false;
                }
                if (model.IsFeatured == null)
                {
                    model.IsFeatured = false;
                }

                return View(model);
            }

        }


        public ActionResult EditVehicle(int id)
        {
            var model = new EditVehicleViewModel();

            var carRepo = CarRepositoryFactory.GetRepository();
            var makeModelRepo = MakeModelRepositoryFactory.GetRepository();

            model.Car = carRepo.GetById(id);
            model.CarMakeName = new SelectList(makeModelRepo.GetMakes(), "CarMakeId", "CarMakeName");
            model.CarModelName = new SelectList(makeModelRepo.GetModelByMake(model.Car.CarMakeId), "CarModelId", "CarModelName");
            model.CategoryName = new SelectList(carRepo.GetCarCategory(), "CategoryId", "CategoryName");
            model.BodyStyle = new SelectList(carRepo.GetBodyStyle(), "BodyStyle", "BodyStyle");
            model.Transmission = new SelectList(carRepo.GetTransmission(), "Transmission", "Transmission");
            model.CarColor = new SelectList(carRepo.GetCarColor(), "CarColor", "CarColor");
            model.InteriorColor = new SelectList(carRepo.GetInteriorColor(), "InteriorColor", "InteriorColor");
            if (model.IsSold == null)
            {
                model.IsSold = false;
            }
            if (model.IsFeatured == null)
            {
                model.IsFeatured = false;
            }
            else
            {
                model.IsFeatured = true;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditVehicle(EditVehicleViewModel model)
        {
            var repo = CarRepositoryFactory.GetRepository();

            int carMileage;
            decimal carSalePrice;
            int carYear;
            decimal msrp;
            if (!Decimal.TryParse(model.Car.CarPrice.ToString(), out msrp))
            {
                ModelState.AddModelError("Car.CarPrice",
                    "Please enter a valid number.");
            }
            if (!Int32.TryParse(model.Car.Mileage.ToString(), out carMileage))
            {
                ModelState.AddModelError("Car.Mileage",
                    "Please enter a valid number.");
            }
            if (!Decimal.TryParse(model.Car.CarSalePrice.ToString(), out carSalePrice))
            {
                ModelState.AddModelError("Car.CarSalePrice",
                    "Please enter a valid number.");
            }
            if (!Int32.TryParse(model.Car.CarYear.ToString(), out carYear))
            {
                ModelState.AddModelError("Car.CarYear",
                    "Please enter a valid number.");
            }
            if (model.Car.CarYear.ToString().Length < 4)
            {
                ModelState.AddModelError("Car.CarYear",
                    "Please enter a four digit year.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldCar = repo.GetById(model.Car.CarId);

                    if (model.IsSold == null)
                    {
                        model.Car.IsSold = false;
                    }

                    if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                    {
                        var savepath = Server.MapPath("~/Images");

                        string fileName = Path.GetFileNameWithoutExtension("inventory");
                        string extension = Path.GetExtension(model.ImageUpload.FileName);
                        if (extension != "png" || extension != "jpg" || extension != "jpeg")
                        {
                            ModelState.AddModelError("Car.ImageFileName",
                                "Please upload a png, jpg, or jpeg file.");
                        }

                        var filePath = Path.Combine(savepath, fileName + model.Car.CarId + extension);

                        model.ImageUpload.SaveAs(filePath);
                        model.Car.ImageFileName = Path.GetFileName(filePath);

                        // delete old file
                        var oldPath = Path.Combine(savepath, oldCar.ImageFileName);
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }
                    else
                    {
                        // they did not replace the old file, so keep the old file name
                        model.Car.ImageFileName = oldCar.ImageFileName;
                    }

                    repo.Update(model.Car);

                    TempData["Message"] = "Vehicle has been saved.";
                    return RedirectToAction("EditVehicle", new { id = model.Car.CarId });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                var carRepo = CarRepositoryFactory.GetRepository();
                var makeModelRepo = MakeModelRepositoryFactory.GetRepository();

                model.Car = carRepo.GetById(model.Car.CarId);
                model.CarMakeName = new SelectList(makeModelRepo.GetMakes(), "CarMakeId", "CarMakeName");
                model.CarModelName = new SelectList(makeModelRepo.GetModelByMake(model.Car.CarMakeId), "CarModelId", "CarModelName");
                model.CategoryName = new SelectList(carRepo.GetCarCategory(), "CategoryId", "CategoryName");
                model.BodyStyle = new SelectList(carRepo.GetBodyStyle(), "BodyStyle", "BodyStyle");
                model.Transmission = new SelectList(carRepo.GetTransmission(), "Transmission", "Transmission");
                model.CarColor = new SelectList(carRepo.GetCarColor(), "CarColor", "CarColor");
                model.InteriorColor = new SelectList(carRepo.GetInteriorColor(), "InteriorColor", "InteriorColor");
                if (model.IsSold == null)
                {
                    model.IsSold = false;
                }
                if (model.IsFeatured == null)
                {
                    model.IsFeatured = false;
                }
                else
                {
                    model.IsFeatured = true;
                }

                return View(model);
            }
            
        }

        public ActionResult Vehicles()
        {
            var repo = CarRepositoryFactory.GetRepository();

            return View(repo.GetDetails());
        }

        [HttpPost]
        public ActionResult DeleteVehicle(int carId)
        {
            var repo = CarRepositoryFactory.GetRepository();
            var vehicle = repo.GetById(carId);
            repo.Delete(carId);
            string path = Path.Combine(Server.MapPath("~/Images/"),
                          Path.GetFileName(vehicle.ImageFileName));
            System.IO.File.Delete(path);

            TempData["Message"] = "Vehicle and associated image have been removed.";
            return RedirectToAction("Vehicles");
        }


        public ActionResult Makes()
        {
            if (Request.IsAuthenticated)
            {
                var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                var user = userMgr.FindByName(User.Identity.Name);

                ViewBag.UserName = user.UserName;
            }

            AddMakeViewModel model = new AddMakeViewModel();
            var repo = MakeModelRepositoryFactory.GetRepository();

            model.Makes = repo.GetMakes();

            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Makes(AddMakeViewModel model)
        {
            var repo = MakeModelRepositoryFactory.GetRepository();

            if (string.IsNullOrEmpty(model.CarMakeName))
            {
                ModelState.AddModelError("CarMakeName",
                    "Please enter a car make name");
            }

            if (ModelState.IsValid)
            {               

                try
                {
                    model.NewMake = new CarMake();
                    model.NewMake.CarMakeId = model.CarMakeId;
                    model.NewMake.CarMakeName = model.CarMakeName;
                    model.NewMake.DateCreated = model.DateCreated;

                    var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                    var user = userMgr.FindByName(User.Identity.Name);

                    ViewBag.UserName = user.UserName;

                    repo.AddMake(model.NewMake);

                    return RedirectToAction("Makes", "Admin");
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                model.Makes = repo.GetMakes();

                return View(model);
            }
        }


        public ActionResult Models()
        {
            if (Request.IsAuthenticated)
            {
                var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                var user = userMgr.FindByName(User.Identity.Name);

                ViewBag.UserName = user.UserName;
            }

            AddModelViewModel model = new AddModelViewModel();
            var repo = MakeModelRepositoryFactory.GetRepository();

            model.Models = repo.GetModels();
            model.Makes = (from type in repo.GetMakes()
                                 select new SelectListItem()
                                 {
                                     Text = type.CarMakeName,
                                     Value = type.CarMakeName,
                                 }).ToList();

            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Models(AddModelViewModel model)
        {
            var repo = MakeModelRepositoryFactory.GetRepository();

            if (string.IsNullOrEmpty(model.CarModelName))
            {
                ModelState.AddModelError("CarModelName",
                    "Please enter a car model");
            }

            if (ModelState.IsValid)
            {

                try
                {
                    model.NewModel = new CarModel();
                    model.NewModel.CarMakeName = model.CarMakeName;
                    model.NewModel.CarModelName = model.CarModelName;
                    model.NewModel.DateCreated = model.DateCreated;

                    var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                    var user = userMgr.FindByName(User.Identity.Name);

                    ViewBag.UserName = user.UserName;

                    repo.AddModel(model.NewModel);

                    return RedirectToAction("Models", "Admin");
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                model.Models = repo.GetModels();
                model.Makes = (from type in repo.GetMakes()
                               select new SelectListItem()
                               {
                                   Text = type.CarMakeName,
                                   Value = type.CarMakeName,
                               }).ToList();

                return View(model);
            }
        }
    }
}