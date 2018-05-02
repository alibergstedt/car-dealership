using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class SalesReportSearchParameters
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string SalesPerson { get; set; }
        public int TotalVehicles { get; set; }
        public decimal TotalSales { get; set; }
        public DateTime? SaleDate { get; set; }
    }
}
