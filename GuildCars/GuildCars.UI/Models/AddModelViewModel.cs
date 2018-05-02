using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models
{
    public class AddModelViewModel
    {
        public int CarMakeId { get; set; }
        public int CarModelId { get; set; }
        public string CarMakeName { get; set; }
        public string CarModelName { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserName { get; set; }       
        public IEnumerable<SelectListItem> Makes { get; set; }
        public List<CarModel> Models { get; set; }
        public CarModel NewModel { get; set; }
    }
}