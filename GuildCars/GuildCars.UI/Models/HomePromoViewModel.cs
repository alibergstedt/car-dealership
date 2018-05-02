using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class HomePromoViewModel
    {
        public IEnumerable<CarDetailsFeaturedItem> CarDetails { get; set; }
        public List<Promo> Promo { get; set; }
    }
}