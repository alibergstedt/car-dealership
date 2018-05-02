using GuildCars.Data.Factories;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using GuildCars.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GuildCars.UI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CarAPIController : ApiController
    {
        [Route("api/inventory/new")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SearchNew(decimal? minPrice, decimal? maxPrice, int? carYear, int? minYear,
            int? maxYear, string carMakeName, string carModelName)
        {
            var repo = CarRepositoryFactory.GetRepository();

            try
            {
                var parameters = new CarSearchParameters()
                {
                    MinPrice = minPrice,
                    MaxPrice = maxPrice,
                    CarYear = carYear,
                    MinYear = minYear,
                    MaxYear = maxYear,
                    CarMakeName = carMakeName,
                    CarModelName = carModelName
                };

                var result = repo.SearchNewInventory(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [Route("api/sales/index")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SearchSalesInventory(decimal? minPrice, decimal? maxPrice, int? carYear, int? minYear,
            int? maxYear, string carMakeName, string carModelName)
        {
            var repo = CarRepositoryFactory.GetRepository();

            try
            {
                var parameters = new CarSearchParameters()
                {
                    MinPrice = minPrice,
                    MaxPrice = maxPrice,
                    CarYear = carYear,
                    MinYear = minYear,
                    MaxYear = maxYear,
                    CarMakeName = carMakeName,
                    CarModelName = carModelName
                };

                var result = repo.SearchAll(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [Route("api/admin/vehicles")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SearchAdminInventory(decimal? minPrice, decimal? maxPrice, int? carYear, int? minYear,
            int? maxYear, string carMakeName, string carModelName)
        {
            var repo = CarRepositoryFactory.GetRepository();

            try
            {
                var parameters = new CarSearchParameters()
                {
                    MinPrice = minPrice,
                    MaxPrice = maxPrice,
                    CarYear = carYear,
                    MinYear = minYear,
                    MaxYear = maxYear,
                    CarMakeName = carMakeName,
                    CarModelName = carModelName
                };

                var result = repo.SearchAll(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/inventory/used")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SearchUsed(decimal? minPrice, decimal? maxPrice, int? carYear, int? minYear,
            int? maxYear, string carMakeName, string carModelName)
        {
            var repo = CarRepositoryFactory.GetRepository();

            try
            {
                var parameters = new CarSearchParameters()
                {
                    MinPrice = minPrice,
                    MaxPrice = maxPrice,
                    CarYear = carYear,
                    MinYear = minYear,
                    MaxYear = maxYear,
                    CarMakeName = carMakeName,
                    CarModelName = carModelName
                };

                var result = repo.SearchUsedInventory(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("api/reports/sales")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SearchSalesReport(string salesPerson, DateTime? fromDate, DateTime? toDate)
        {
            var repo = InventoryRepositoryFactory.GetRepository();

            try
            {
                var parameters = new SalesReportSearchParameters()
                {
                    SalesPerson = salesPerson,
                    FromDate = fromDate,
                    ToDate = toDate
                };

                var result = repo.SearchSalesInvoices(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("api/admin/addvehicle")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetModelsAssociatedWithMake(int carMakeId)
        {
            var repo = MakeModelRepositoryFactory.GetRepository();

            var model = repo.GetModelByMake(carMakeId);
            return Ok(model);
        }


        [Route("api/admin/editvehicle")]
        [AcceptVerbs("GET")]
        public IHttpActionResult EditModelsAssociatedWithMake(int carMakeId)
        {
            var repo = MakeModelRepositoryFactory.GetRepository();

            var model = repo.GetModelByMake(carMakeId);
            return Ok(model);
        }

    }
}
