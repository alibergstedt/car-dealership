using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class PromoDetailsItem
    {
        public int PromoCodeId { get; set; }
        public string PromotionName { get; set; }
        public string PromotionDescription { get; set; }
        public string ImageFileName { get; set; }
    }
}
