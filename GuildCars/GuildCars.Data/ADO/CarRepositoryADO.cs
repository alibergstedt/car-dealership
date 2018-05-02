using GuildCars.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System.Data.SqlClient;
using System.Data;

namespace GuildCars.Data.ADO
{
    public class CarRepositoryADO : ICarRepository
    {
        public void Delete(int carId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CarDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CarId", carId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public List<Car> GetBodyStyle()
        {
            List<Car> bodyStyles = new List<Car>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("BodyStyleSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Car currentRow = new Car();
                        currentRow.BodyStyle = dr["BodyStyle"].ToString();

                        bodyStyles.Add(currentRow);
                    }
                }
            }

            return bodyStyles;
        }

        public Car GetById(int carId)
        {
            Car car = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CarGetById", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CarId", carId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        car = new Car();
                        car.CarId = (int)dr["CarId"];
                        car.CarYear = (int)dr["CarYear"];
                        car.BodyStyle = dr["BodyStyle"].ToString();
                        car.CarMakeId = (int)dr["CarMakeId"];
                        car.CarModelId = (int)dr["CarModelId"];
                        car.CategoryId = (int)dr["CategoryId"];
                        car.CarMakeName = dr["CarMakeName"].ToString();
                        car.CarModelName = dr["CarModelName"].ToString();
                        car.BodyStyle = dr["BodyStyle"].ToString();
                        car.InteriorColor = dr["InteriorColor"].ToString();
                        car.VinNumber = dr["VinNumber"].ToString();
                        car.Transmission = dr["Transmission"].ToString();
                        car.CarColor = dr["CarColor"].ToString();
                        car.Mileage = (int)dr["Mileage"];
                        car.CarPrice = (decimal)dr["CarPrice"];
                        
                        if (dr["CarSalePrice"] != DBNull.Value)
                            car.CarSalePrice = (decimal)dr["CarSalePrice"];                        

                        if (dr["CarDescription"] != DBNull.Value)
                            car.CarDescription = dr["CarDescription"].ToString();

                        if (dr["ImageFileName"] != DBNull.Value)
                            car.ImageFileName = dr["ImageFileName"].ToString();
                    }
                }
            }

            return car;
        }

        public List<Category> GetCarCategory()
        {
            List<Category> category = new List<Category>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CategorySelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Category currentRow = new Category();
                        currentRow.CategoryId = (int)dr["CategoryId"];
                        currentRow.CategoryName = dr["CategoryName"].ToString();

                        category.Add(currentRow);
                    }
                }
            }

            return category;
        }

        public List<Car> GetCarColor()
        {
            List<Car> carColor = new List<Car>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CarColorSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Car currentRow = new Car();
                        currentRow.CarColor = dr["CarColor"].ToString();

                        carColor.Add(currentRow);
                    }
                }
            }

            return carColor;
        }

        public IEnumerable<CarDetailsItem> GetDetails()
        {
            List<CarDetailsItem> cars = new List<CarDetailsItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CarSelectDetails", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CarDetailsItem row = new CarDetailsItem();

                        row.CarId = (int)dr["CarId"];
                        row.CarMakeId = (int)dr["CarMakeId"];
                        row.CarModelId = (int)dr["CarModelId"];
                        row.CarMakeName = dr["CarMakeName"].ToString();
                        row.CarModelName = dr["CarModelName"].ToString();
                        row.CarYear = (int)dr["CarYear"];
                        row.BodyStyle = dr["BodyStyle"].ToString();
                        row.InteriorColor = dr["InteriorColor"].ToString();
                        row.VinNumber = dr["VinNumber"].ToString();
                        row.Transmission = dr["Transmission"].ToString();
                        row.CarColor = dr["CarColor"].ToString();
                        row.Mileage = (int)dr["Mileage"];
                        row.CarPrice = (decimal)dr["CarPrice"];
                        row.IsSold = (bool)dr["IsSold"];

                        if (dr["ImageFileName"] != DBNull.Value)
                            row.ImageFileName = dr["ImageFileName"].ToString();

                        if (dr["CarDescription"] != DBNull.Value)
                            row.CarDescription = dr["CarDescription"].ToString();

                        if (dr["CarSalePrice"] != DBNull.Value)
                            row.CarSalePrice = (decimal)dr["CarSalePrice"];

                        cars.Add(row);
                    }
                }
            }

            return cars;
        }

        public IEnumerable<CarDetailsFeaturedItem> GetFeatured()
        {
            List<CarDetailsFeaturedItem> cars = new List<CarDetailsFeaturedItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CarSelectFeatured", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CarDetailsFeaturedItem row = new CarDetailsFeaturedItem();

                        row.CarId = (int)dr["CarId"];
                        row.CarMakeName = dr["CarMakeName"].ToString();
                        row.CarModelName = dr["CarModelName"].ToString();
                        row.CarYear = (int)dr["CarYear"];
                        row.CarPrice = (decimal)dr["CarPrice"];

                        if (dr["ImageFileName"] != DBNull.Value)
                            row.ImageFileName = dr["ImageFileName"].ToString();

                        cars.Add(row);
                    }
                }
            }

            return cars;
        }

        public List<Car> GetInteriorColor()
        {
            List<Car> interiorColor = new List<Car>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("InteriorColorSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Car currentRow = new Car();
                        currentRow.InteriorColor = dr["InteriorColor"].ToString();

                        interiorColor.Add(currentRow);
                    }
                }
            }

            return interiorColor;
        }

        public List<Car> GetTransmission()
        {
            List<Car> transmission = new List<Car>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("TransmissionSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Car currentRow = new Car();
                        currentRow.Transmission = dr["Transmission"].ToString();

                        transmission.Add(currentRow);
                    }
                }
            }

            return transmission;
        }

        public void Insert(Car car)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CarInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@CarId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);
                
                cmd.Parameters.AddWithValue("@CategoryId", car.CategoryId);
                cmd.Parameters.AddWithValue("@CarMakeId", car.CarMakeId);
                cmd.Parameters.AddWithValue("@CarModelId", car.CarModelId);
                cmd.Parameters.AddWithValue("@BodyStyle", car.BodyStyle);
                cmd.Parameters.AddWithValue("@VinNumber", car.VinNumber);
                cmd.Parameters.AddWithValue("@CarYear", car.CarYear);
                cmd.Parameters.AddWithValue("@Transmission", car.Transmission);
                cmd.Parameters.AddWithValue("@CarColor", car.CarColor);
                cmd.Parameters.AddWithValue("@InteriorColor", car.InteriorColor);
                cmd.Parameters.AddWithValue("@CarPrice", car.CarPrice);
                cmd.Parameters.AddWithValue("@Mileage", car.Mileage);
                cmd.Parameters.AddWithValue("@IsFeatured", car.IsFeatured);
                cmd.Parameters.AddWithValue("@IsSold", car.IsSold);
                
                if (string.IsNullOrEmpty(car.CarSalePrice.ToString()))
                {
                    cmd.Parameters.AddWithValue("@CarSalePrice", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CarSalePrice", car.CarSalePrice);
                }                

                if (string.IsNullOrEmpty(car.CarDescription))
                {
                    cmd.Parameters.AddWithValue("@CarDescription", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CarDescription", car.CarDescription);
                }

                if (string.IsNullOrEmpty(car.ImageFileName))
                {
                    cmd.Parameters.AddWithValue("@ImageFileName", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ImageFileName", car.ImageFileName);
                }

                cn.Open();

                cmd.ExecuteNonQuery();

                car.CarId = (int)param.Value;
            }
        }

        public IEnumerable<CarDetailsItem> SearchAll(CarSearchParameters parameters)
        {
            List<CarDetailsItem> cars = new List<CarDetailsItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "SELECT TOP 20 CarId, IsSold, CarMakeName, CarModelName, BodyStyle, VinNumber, CarYear, Transmission, " +
                    "CarColor, InteriorColor, Mileage, CarPrice, CarSalePrice, ImageFileName " +
                    "FROM Car c INNER JOIN CarModel cd ON c.CarModelId = cd.CarModelId INNER JOIN CarMake cm ON cd.CarMakeId = cm.CarMakeId " +
                    "WHERE 1 = 1 AND IsSold = 0 ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if (parameters.MinPrice.HasValue)
                {
                    query += "AND ((CarSalePrice >= @MinPrice) OR (CarPrice >= @MinPrice)) ";
                    cmd.Parameters.AddWithValue("@MinPrice", parameters.MinPrice.Value);
                }

                if (parameters.MaxPrice.HasValue)
                {
                    query += "AND ((CarSalePrice <= @MaxPrice) OR (CarPrice <= @MaxPrice)) ";
                    cmd.Parameters.AddWithValue("@MaxPrice", parameters.MaxPrice.Value);
                }

                if (parameters.MinYear.HasValue)
                {
                    query += "AND CarYear >= @MinYear ";
                    cmd.Parameters.AddWithValue("@MinYear", parameters.MinYear.Value);
                }

                if (parameters.MaxYear.HasValue)
                {
                    query += "AND CarYear <= @MaxYear ";
                    cmd.Parameters.AddWithValue("@MaxYear", parameters.MaxYear.Value);
                }

                if (!string.IsNullOrEmpty(parameters.CarMakeName))
                {
                    query += "AND CarMakeName LIKE @CarMakeName ";
                    cmd.Parameters.AddWithValue("@CarMakeName", parameters.CarMakeName + '%');
                }

                if (!string.IsNullOrEmpty(parameters.CarModelName))
                {
                    query += "OR CarModelName LIKE @CarModelName ";
                    cmd.Parameters.AddWithValue("@CarModelName", parameters.CarModelName + '%');
                }

                if (!string.IsNullOrEmpty(parameters.CarYear.ToString()))
                {
                    query += "OR CarYear LIKE @CarYear ";
                    cmd.Parameters.AddWithValue("@CarYear", parameters.CarYear + '%');
                }

                query += "ORDER BY CarPrice DESC";
                cmd.CommandText = query;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CarDetailsItem row = new CarDetailsItem();

                        row.CarId = (int)dr["CarId"];
                        row.CarYear = (int)dr["CarYear"];
                        row.CarMakeName = dr["CarMakeName"].ToString();
                        row.CarModelName = dr["CarModelName"].ToString();
                        row.BodyStyle = dr["BodyStyle"].ToString();
                        row.InteriorColor = dr["InteriorColor"].ToString();
                        row.VinNumber = dr["VinNumber"].ToString();
                        row.Transmission = dr["Transmission"].ToString();
                        row.CarColor = dr["CarColor"].ToString();
                        row.Mileage = (int)dr["Mileage"];
                        row.CarPrice = (decimal)dr["CarPrice"];
                        row.IsSold = (bool)dr["IsSold"];
                        
                        if (dr["CarSalePrice"] != DBNull.Value)
                            row.CarSalePrice = (decimal)dr["CarSalePrice"];

                        if (dr["ImageFileName"] != DBNull.Value)
                            row.ImageFileName = dr["ImageFileName"].ToString();

                        cars.Add(row);
                    }
                }
            }

            return cars;
        }

        public IEnumerable<CarDetailsItem> SearchNewInventory(CarSearchParameters parameters)
        {
            List<CarDetailsItem> cars = new List<CarDetailsItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "SELECT TOP 20 CarId, CarMakeName, CarModelName, BodyStyle, VinNumber, CarYear, Transmission, " +
                    "CarColor, InteriorColor, Mileage, CarPrice, CarSalePrice, ImageFileName " +
                    "FROM Car c INNER JOIN CarModel cd ON c.CarModelId = cd.CarModelId INNER JOIN CarMake cm ON cd.CarMakeId = cm.CarMakeId " +
                    "WHERE 1 = 1 AND CategoryId = 2 ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if(parameters.MinPrice.HasValue)
                {
                    query += "AND ((CarSalePrice >= @MinPrice) OR (CarPrice >= @MinPrice)) ";
                    cmd.Parameters.AddWithValue("@MinPrice", parameters.MinPrice.Value);
                }

                if (parameters.MaxPrice.HasValue)
                {
                    query += "AND ((CarPrice <= @MaxPrice) OR (CarSalePrice <= @MaxPrice)) ";
                    cmd.Parameters.AddWithValue("@MaxPrice", parameters.MaxPrice.Value);
                }

                if (parameters.MinYear.HasValue)
                {
                    query += "AND CarYear >= @MinYear ";
                    cmd.Parameters.AddWithValue("@MinYear", parameters.MinYear.Value);
                }

                if (parameters.MaxYear.HasValue)
                {
                    query += "AND CarYear <= @MaxYear ";
                    cmd.Parameters.AddWithValue("@MaxYear", parameters.MaxYear.Value);
                }

                if (!string.IsNullOrEmpty(parameters.CarMakeName))
                {
                    query += "AND CarMakeName LIKE @CarMakeName ";
                    cmd.Parameters.AddWithValue("@CarMakeName", parameters.CarMakeName + '%');
                }

                if (!string.IsNullOrEmpty(parameters.CarModelName))
                {
                    query += "OR CarModelName LIKE @CarModelName ";
                    cmd.Parameters.AddWithValue("@CarModelName", parameters.CarModelName + '%');
                }

                if (!string.IsNullOrEmpty(parameters.CarYear.ToString()))
                {
                    query += "OR CarYear LIKE @CarYear ";
                    cmd.Parameters.AddWithValue("@CarYear", parameters.CarYear + '%');
                }

                query += "ORDER BY CarPrice DESC";
                cmd.CommandText = query;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CarDetailsItem row = new CarDetailsItem();

                        row.CarId = (int)dr["CarId"];
                        row.CarYear = (int)dr["CarYear"];
                        row.CarMakeName = dr["CarMakeName"].ToString();
                        row.CarModelName = dr["CarModelName"].ToString();
                        row.BodyStyle = dr["BodyStyle"].ToString();
                        row.InteriorColor = dr["InteriorColor"].ToString();
                        row.VinNumber = dr["VinNumber"].ToString();
                        row.Transmission = dr["Transmission"].ToString();
                        row.CarColor = dr["CarColor"].ToString();
                        row.Mileage = (int)dr["Mileage"];
                        row.CarPrice = (decimal)dr["CarPrice"];

                        if (dr["CarSalePrice"] != DBNull.Value)
                            row.CarSalePrice = (decimal)dr["CarSalePrice"];

                        if (dr["ImageFileName"] != DBNull.Value)
                            row.ImageFileName = dr["ImageFileName"].ToString();

                        cars.Add(row);
                    }
                }
            }

            return cars;
        }

        public IEnumerable<CarDetailsItem> SearchUsedInventory(CarSearchParameters parameters)
        {
            List<CarDetailsItem> cars = new List<CarDetailsItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "SELECT TOP 20 CarId, CarMakeName, CarModelName, BodyStyle, VinNumber, CarYear, Transmission, " +
                    "CarColor, InteriorColor, Mileage, CarPrice, CarSalePrice, ImageFileName " +
                    "FROM Car c INNER JOIN CarModel cd ON c.CarModelId = cd.CarModelId INNER JOIN CarMake cm ON cd.CarMakeId = cm.CarMakeId " +
                    "WHERE 1 = 1 AND CategoryId = 1 ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if (parameters.MinPrice.HasValue)
                {
                    query += "AND ((CarSalePrice >= @MinPrice) OR (CarPrice >= @MinPrice)) ";
                    cmd.Parameters.AddWithValue("@MinPrice", parameters.MinPrice.Value);
                }

                if (parameters.MaxPrice.HasValue)
                {
                    query += "AND ((CarPrice <= @MaxPrice) OR (CarSalePrice <= @MaxPrice)) ";
                    cmd.Parameters.AddWithValue("@MaxPrice", parameters.MaxPrice.Value);
                }

                if (parameters.MinYear.HasValue)
                {
                    query += "AND CarYear >= @MinYear ";
                    cmd.Parameters.AddWithValue("@MinYear", parameters.MinYear.Value);
                }

                if (parameters.MaxYear.HasValue)
                {
                    query += "AND CarYear <= @MaxYear ";
                    cmd.Parameters.AddWithValue("@MaxYear", parameters.MaxYear.Value);
                }

                if (!string.IsNullOrEmpty(parameters.CarMakeName))
                {
                    query += "AND CarMakeName LIKE @CarMakeName ";
                    cmd.Parameters.AddWithValue("@CarMakeName", parameters.CarMakeName + '%');
                }

                if (!string.IsNullOrEmpty(parameters.CarModelName))
                {
                    query += "OR CarModelName LIKE @CarModelName ";
                    cmd.Parameters.AddWithValue("@CarModelName", parameters.CarModelName + '%');
                }

                if (!string.IsNullOrEmpty(parameters.CarYear.ToString()))
                {
                    query += "OR CarYear LIKE @CarYear ";
                    cmd.Parameters.AddWithValue("@CarYear", parameters.CarYear + '%');
                }

                query += "ORDER BY CarPrice DESC";
                cmd.CommandText = query;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CarDetailsItem row = new CarDetailsItem();

                        row.CarId = (int)dr["CarId"];
                        row.CarYear = (int)dr["CarYear"];
                        row.CarMakeName = dr["CarMakeName"].ToString();
                        row.CarModelName = dr["CarModelName"].ToString();
                        row.BodyStyle = dr["BodyStyle"].ToString();
                        row.InteriorColor = dr["InteriorColor"].ToString();
                        row.VinNumber = dr["VinNumber"].ToString();
                        row.Transmission = dr["Transmission"].ToString();
                        row.CarColor = dr["CarColor"].ToString();
                        row.Mileage = (int)dr["Mileage"];
                        row.CarPrice = (decimal)dr["CarPrice"];

                        if (dr["CarSalePrice"] != DBNull.Value)
                            row.CarSalePrice = (decimal)dr["CarSalePrice"];

                        if (dr["ImageFileName"] != DBNull.Value)
                            row.ImageFileName = dr["ImageFileName"].ToString();

                        cars.Add(row);
                    }
                }
            }

            return cars;
        }

        public void Update(Car car)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CarUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CarId", car.CarId);           
                cmd.Parameters.AddWithValue("@CarColor", car.CarColor);
                cmd.Parameters.AddWithValue("@CarMakeId", car.CarMakeId);
                cmd.Parameters.AddWithValue("@CarModelId", car.CarModelId);
                cmd.Parameters.AddWithValue("@BodyStyle", car.BodyStyle);
                cmd.Parameters.AddWithValue("@CarPrice", car.CarPrice);
                cmd.Parameters.AddWithValue("@CarYear", car.CarYear);
                cmd.Parameters.AddWithValue("@CategoryId", car.CategoryId);
                cmd.Parameters.AddWithValue("@InteriorColor", car.InteriorColor);
                cmd.Parameters.AddWithValue("@IsFeatured", car.IsFeatured);
                cmd.Parameters.AddWithValue("@Mileage", car.Mileage);
                cmd.Parameters.AddWithValue("@Transmission", car.Transmission);
                cmd.Parameters.AddWithValue("@VinNumber", car.VinNumber);
                cmd.Parameters.AddWithValue("@IsSold", car.IsSold);
                
                if (string.IsNullOrEmpty(car.CarSalePrice.ToString()))
                {
                    cmd.Parameters.AddWithValue("@CarSalePrice", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CarSalePrice", car.CarSalePrice);
                }

                if (string.IsNullOrEmpty(car.CarDescription))
                {
                    cmd.Parameters.AddWithValue("@CarDescription", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CarDescription", car.CarDescription);
                }

                if (string.IsNullOrEmpty(car.ImageFileName))
                {
                    cmd.Parameters.AddWithValue("@ImageFileName", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ImageFileName", car.ImageFileName);
                }

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}
