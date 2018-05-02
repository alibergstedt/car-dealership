using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class PromoViewModel
    {
        public Promo promo { get; set; }
        public IEnumerable<Promo> Promos { get; set; }

        [Required(ErrorMessage = "Please enter a title")]
        public string PromotionName { get; set; }

        [Required(ErrorMessage = "Please enter a descriptioin")]
        public string PromotionDescription { get; set; }

        public string ImageFileName { get; set; }
    }
}