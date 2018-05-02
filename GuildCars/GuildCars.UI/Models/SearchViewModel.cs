using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models
{
    public class SearchViewModel
    {
        public IEnumerable<SelectListItem> MinPrice { get; set; }
        public IEnumerable<SelectListItem> MaxPrice { get; set; }
        public IEnumerable<SelectListItem> MinYear { get; set; }
        public IEnumerable<SelectListItem> MaxYear { get; set; }
        public CarDetailsItem car { get; set; }
    }
}