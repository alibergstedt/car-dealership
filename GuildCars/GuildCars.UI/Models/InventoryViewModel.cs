using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class InventoryViewModel
    {
        public IEnumerable<InventoryViewModel> inventoryNew { get; set; }
        public IEnumerable<InventoryViewModel> inventoryUsed { get; set; }
        public int CarYear { get; set; }
        public string CarMakeName { get; set; }
        public string CarModelName { get; set; }
        public int Count { get; set; }
        public decimal StockValue { get; set; }
    }
}