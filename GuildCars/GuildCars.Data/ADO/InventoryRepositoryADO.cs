using GuildCars.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Models.Queries;
using System.Data.SqlClient;
using System.Data;
using GuildCars.Models.Tables;

namespace GuildCars.Data.ADO
{
    public class InventoryRepositoryADO : IInventoryReportRepository
    {
        public IEnumerable<InventoryReportItem> GetNewVehicles()
        {
            List<InventoryReportItem> cars = new List<InventoryReportItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CarSelectNewInventory", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        InventoryReportItem row = new InventoryReportItem();

                        row.CarMakeName = dr["CarMakeName"].ToString();
                        row.CarModelName = dr["CarModelName"].ToString();
                        row.CarYear = (int)dr["CarYear"];
                        row.Count = (int)dr["Count"];
                        row.StockValue = (decimal)dr["Stock Value"];

                        cars.Add(row);
                    }
                }
            }

            return cars;
        }

        public string GetPurchaseTypeName(int model)
        {
            string purchaseType = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetStateName", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StateId", model);

                cn.Open();

                string getValue = cmd.ExecuteScalar().ToString();
                if (getValue != null)
                {
                    purchaseType = getValue.ToString();
                }
            }

            return purchaseType;
        }

        public List<PurchaseType> GetPurchaseTypes()
        {
            List<PurchaseType> purchaseTypes = new List<PurchaseType>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("PurchaseTypeSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        PurchaseType currentRow = new PurchaseType();
                        currentRow.PurchaseTypeId = (int)dr["PurchaseTypeId"];
                        currentRow.PurchaseTypeName = dr["PurchaseTypeName"].ToString();

                        purchaseTypes.Add(currentRow);
                    }
                }
            }

            return purchaseTypes;
        }        

        public string GetStateName(int model)
        {
            string state = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetStateName", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StateId", model);

                cn.Open();

                string getValue = cmd.ExecuteScalar().ToString();
                if (getValue != null)
                {
                    state = getValue.ToString();
                }
            }

            return state;
        }

        public List<States> GetStates()
        {
            List<States> states = new List<States>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("StatesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        States currentRow = new States();
                        currentRow.StateId = (int)dr["StateId"];
                        currentRow.State = dr["State"].ToString();

                        states.Add(currentRow);
                    }
                }
            }

            return states;
        }

        public IEnumerable<InventoryReportItem> GetUsedVehicles()
        {
            List<InventoryReportItem> cars = new List<InventoryReportItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CarSelectUsedInventory", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        InventoryReportItem row = new InventoryReportItem();

                        row.CarMakeName = dr["CarMakeName"].ToString();
                        row.CarModelName = dr["CarModelName"].ToString();
                        row.CarYear = (int)dr["CarYear"];
                        row.Count = (int)dr["Count"];
                        row.StockValue = (decimal)dr["Stock Value"];

                        cars.Add(row);
                    }
                }
            }

            return cars;
        }

        public void Insert(SalesInvoice salesInvoice)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SalesInvoiceInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@InvoiceId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@CarId", salesInvoice.CarId);
                cmd.Parameters.AddWithValue("@StateId", salesInvoice.StateId);
                cmd.Parameters.AddWithValue("@PurchaseTypeId", salesInvoice.PurchaseTypeId);
                cmd.Parameters.AddWithValue("@SalesPerson", salesInvoice.SalesPerson);
                cmd.Parameters.AddWithValue("@UserEmail", salesInvoice.UserEmail);
                cmd.Parameters.AddWithValue("@ContactName", salesInvoice.ContactName);
                cmd.Parameters.AddWithValue("@TelephoneNumber", salesInvoice.TelephoneNumber);
                cmd.Parameters.AddWithValue("@StreetAddress1", salesInvoice.StreetAddress1);
                cmd.Parameters.AddWithValue("@ZipCode", salesInvoice.ZipCode);
                cmd.Parameters.AddWithValue("@City", salesInvoice.City);
                cmd.Parameters.AddWithValue("@Total", salesInvoice.Total);

                if (string.IsNullOrEmpty(salesInvoice.StreetAddress2))
                {
                    cmd.Parameters.AddWithValue("@StreetAddress2", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@StreetAddress2", salesInvoice.StreetAddress2);
                }

                cn.Open();

                cmd.ExecuteNonQuery();

                salesInvoice.InvoiceId = (int)param.Value;
            }
        }

        public List<SalesInvoice> SalesPersonSelectAll()
        {
            List<SalesInvoice> salesPerson = new List<SalesInvoice>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SalesPersonSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SalesInvoice currentRow = new SalesInvoice();
                        currentRow.SalesPerson = dr["SalesPerson"].ToString();

                        salesPerson.Add(currentRow);
                    }
                }
            }

            return salesPerson;
        }

        public IEnumerable<SalesReportSearchParameters> SearchSalesInvoices(SalesReportSearchParameters parameters)
        {
            List<SalesReportSearchParameters> sales = new List<SalesReportSearchParameters>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "SELECT SalesPerson, " +
                    "COUNT(CarId) AS [Total Vehicles], " +
                    "SUM(Total) AS [Total Sales] " +
                    "FROM SalesInvoice " +
                    "WHERE 1 = 1 ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if (!string.IsNullOrEmpty(parameters.SalesPerson))
                {
                    query += "AND SalesPerson = @SalesPerson ";
                    cmd.Parameters.AddWithValue("@SalesPerson", parameters.SalesPerson);
                }

                if (!string.IsNullOrEmpty(parameters.FromDate.ToString()))
                {
                    query += "AND SaleDate >= @FromDate ";
                    cmd.Parameters.AddWithValue("@FromDate", parameters.FromDate);
                }

                if (!string.IsNullOrEmpty(parameters.ToDate.ToString()))
                {
                    query += "AND SaleDate <= @ToDate ";
                    cmd.Parameters.AddWithValue("@ToDate", parameters.ToDate);
                }

                query += "GROUP BY SalesPerson";
                cmd.CommandText = query;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SalesReportSearchParameters row = new SalesReportSearchParameters();

                        row.TotalSales = (decimal)dr["Total Sales"];
                        row.TotalVehicles = (int)dr["Total Vehicles"];

                        if (dr["SalesPerson"] != DBNull.Value)
                            row.SalesPerson = dr["SalesPerson"].ToString();

                        sales.Add(row);
                    }
                }
            }

            return sales;
        }
    }
}
