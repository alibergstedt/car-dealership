using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class PurchaseDetailsItem
    {
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string PurchaseType { get; set; }
        public decimal PurchasePrice { get; set; }
    }
}
