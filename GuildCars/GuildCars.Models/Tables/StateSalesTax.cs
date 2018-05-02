using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class StateSalesTax
    {
        public int TaxId { get; set; }
        public string State { get; set; }
        public decimal StateTax { get; set; }
    }
}
