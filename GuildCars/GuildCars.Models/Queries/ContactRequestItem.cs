using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class ContactRequestItem
    {
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public string EmailAddress { get; set; }
        public string TelephoneNumber { get; set; }
        public string ContactMessage { get; set; }
    }
}
