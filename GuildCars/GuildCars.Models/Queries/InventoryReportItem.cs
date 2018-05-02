using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class InventoryReportItem
    {
        public IEnumerable<InventoryReportItem> inventoryNew { get; set; }
        public IEnumerable<InventoryReportItem> inventoryUsed { get; set; }
        public int CarYear { get; set; }
        public string CarMakeName { get; set; }
        public string CarModelName { get; set; }
        public int Count { get; set; }
        public decimal StockValue  { get; set; }
    }
}
