using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class SalesReportItem
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string UserId { get; set; }
        public int InvoiceId { get; set; }
        public decimal Total { get; set; }
    }
}
