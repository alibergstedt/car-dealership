using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class CarDetailsFeaturedItem
    {
        public int CarId { get; set; }
        public string CarMakeName { get; set; }
        public string CarModelName { get; set; }
        public int CarYear { get; set; }
        public decimal CarPrice { get; set; }
        public string ImageFileName { get; set; }
    }
}
