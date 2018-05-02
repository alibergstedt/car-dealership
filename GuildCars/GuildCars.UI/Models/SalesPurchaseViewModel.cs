using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models
{
    public class SalesPurchaseViewModel
    {
        public string State { get; set; }
        public string PurchaseTypeName { get; set; }
        public int StateId { get; set; }
        public int PurchaseTypeId { get; set; }
        public Car Car { get; set; }
        public IEnumerable<SelectListItem> PurchaseType { get; set; }
        public IEnumerable<SelectListItem> States { get; set; }
        public SalesInvoice SalesInvoice { get; set; }
    }
}