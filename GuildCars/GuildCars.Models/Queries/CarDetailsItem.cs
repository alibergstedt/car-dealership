using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class CarDetailsItem
    {
        public int CarId { get; set; }
        public int CategoryId { get; set; }
        public int CarMakeId { get; set; }
        public int CarModelId { get; set; }
        public string CarMakeName { get; set; }
        public string CarModelName { get; set; }
        public string BodyStyle { get; set; }
        public string VinNumber { get; set; }
        public int CarYear { get; set; }
        public string Transmission { get; set; }
        public string CarColor { get; set; }
        public string InteriorColor { get; set; }
        public int Mileage { get; set; }
        public string CarDescription { get; set; }
        public decimal CarPrice { get; set; }
        public decimal CarSalePrice { get; set; }
        public string ImageFileName { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsSold { get; set; }

    }
}
