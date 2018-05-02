using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class CarSearchParameters
    {
        public int? CarId { get; set; }
        public int? CategoryId { get; set; }
        public string CarMakeName { get; set; }
        public string CarModelName { get; set; }
        public int? CarYear { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinYear { get; set; }
        public int? MaxYear { get; set; }
        public int? CarSalePrice { get; set; }
    }
}
