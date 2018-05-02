using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class Car
    {
        public int CarId { get; set; }
        
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please select a car make")]
        public int CarMakeId { get; set; }

        [Required(ErrorMessage = "Please select a car model")]
        public int CarModelId { get; set; }

        
        public string CarMakeName { get; set; }

        
        public string CarModelName { get; set; }

        [Required(ErrorMessage = "Please select a body style")]
        public string BodyStyle { get; set; }

        [Required(ErrorMessage = "Please enter a VinNumber")]
        [StringLength(17, MinimumLength = 17, ErrorMessage = "Please enter a 17 digit VinNumber")]
        public string VinNumber { get; set; }

        [Required(ErrorMessage = "Please enter a four digit car year")]
        public int? CarYear { get; set; }

        [Required(ErrorMessage = "Please select a transmission")]
        public string Transmission { get; set; }

        [Required(ErrorMessage = "Please select a car color")]
        public string CarColor { get; set; }

        [Required(ErrorMessage = "Please select an interior color")]
        public string InteriorColor { get; set; }

        [Required(ErrorMessage = "Please enter car mileage")]
        public int? Mileage { get; set; }

        [Required(ErrorMessage = "Please enter car description")]
        public string CarDescription { get; set; }

        [Required(ErrorMessage = "Please enter MSRP")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public decimal? CarPrice { get; set; }

        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public decimal? CarSalePrice { get; set; }


        public string ImageFileName { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsSold { get; set; }
    }
}
