using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface IInventoryReportRepository
    {
        IEnumerable<InventoryReportItem> GetNewVehicles();
        IEnumerable<InventoryReportItem> GetUsedVehicles();
        void Insert(SalesInvoice salesInvoice);
        List<States> GetStates();
        List<PurchaseType> GetPurchaseTypes();
        List<SalesInvoice> SalesPersonSelectAll();
        string GetStateName(int model);
        string GetPurchaseTypeName(int model);
        IEnumerable<SalesReportSearchParameters> SearchSalesInvoices(SalesReportSearchParameters parameters);
    }
}
