using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class SalesInvoice
    {
        public int InvoiceId { get; set; }
        public int CarId { get; set; }
        public string SalesPerson { get; set; }
        
        public int StateId { get; set; }        
        public int PurchaseTypeId { get; set; }        
        
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string UserEmail { get; set; }
        
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid phone no.")]
        public string TelephoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "Please enter a street address")]
        public string StreetAddress1 { get; set; }

        public string StreetAddress2 { get; set; }

        [Required(ErrorMessage = "Please enter a city")]
        public string City { get; set; }
        
        public string State { get; set; }

        [Required(ErrorMessage = "Please enter a zipcode")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Please enter a 5 digit Zipcode")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Purchase price is required")]
        public decimal? Total { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string PurchaseTypeName { get; set; }
        public Car Car { get; set; }
    }
}
