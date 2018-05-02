using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class Promo
    {
        public int PromoCodeId { get; set; }

        [Required(ErrorMessage = "Please enter a title")]
        public string PromotionName { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        public string PromotionDescription { get; set; }

        public string ImageFileName { get; set; }
    }
}
