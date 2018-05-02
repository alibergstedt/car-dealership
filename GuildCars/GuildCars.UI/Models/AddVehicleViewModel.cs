using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models
{
    public class AddVehicleViewModel
    {
        public Car Car { get; set; }

        
        public IEnumerable<SelectListItem> CarMakeName { get; set; }

        
        public IEnumerable<SelectListItem> CarModelName { get; set; }

        
        public IEnumerable<SelectListItem> CategoryName { get; set; }

        
        public IEnumerable<SelectListItem> BodyStyle { get; set; }

        
        public IEnumerable<SelectListItem> Transmission { get; set; }

        
        public IEnumerable<SelectListItem> CarColor { get; set; }

        
        public IEnumerable<SelectListItem> InteriorColor { get; set; }

        public HttpPostedFileBase ImageUpload { get; set; }
        public bool? IsSold { get; set; }
        public bool? IsFeatured { get; set; }
    }
}