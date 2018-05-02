using GuildCars.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Models.Tables;
using System.Data.SqlClient;
using System.Data;

namespace GuildCars.Data.ADO
{
    public class MakeModelRepositoryADO : IMakeModelRepository
    {
        public void AddMake(CarMake carMake)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CarMakeInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@CarMakeId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);
                
                cmd.Parameters.AddWithValue("@CarMakeName", carMake.CarMakeName);                

                cn.Open();

                cmd.ExecuteNonQuery();

                carMake.CarMakeId = (int)param.Value;
            }
        }

        public void AddModel(CarModel carModel)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CarModelInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@CarModelId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@CarMakeName", carModel.CarMakeName);
                cmd.Parameters.AddWithValue("@CarModelName", carModel.CarModelName);

                cn.Open();

                cmd.ExecuteNonQuery();

                carModel.CarModelId = (int)param.Value;
            }
        }

        public List<CarMake> GetMakes()
        {
            List<CarMake> makes = new List<CarMake>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CarSelectMakes", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CarMake currentRow = new CarMake();
                        currentRow.CarMakeId = (int)dr["CarMakeId"];
                        currentRow.CarMakeName = dr["Make"].ToString();
                        currentRow.DateCreated = (DateTime)dr["Date Added"];

                        makes.Add(currentRow);
                    }
                }
            }

            return makes;
        }

        public List<CarModel> GetModels()
        {
            List<CarModel> models = new List<CarModel>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CarSelectModels", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CarModel currentRow = new CarModel();
                        currentRow.CarModelId = (int)dr["CarModelId"];
                        currentRow.CarMakeName = dr["Make"].ToString();
                        currentRow.CarModelName = dr["Model"].ToString();
                        currentRow.DateCreated = (DateTime)dr["Date Added"];

                        models.Add(currentRow);
                    }
                }
            }

            return models;
        }

        public List<CarModel> GetModelByMake(int carMakeId)
        {
            List<CarModel> carModels = new List<CarModel>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "SELECT cl.CarMakeId, CarModelId, CarModelName " +
                               "FROM CarModel cl " +
                                    "INNER JOIN CarMake cm ON cl.CarMakeId = cm.CarMakeId " +
                                    "WHERE cm.CarMakeId = @CarMakeId ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@CarMakeId", carMakeId);

                query += "ORDER BY CarModelName";
                cmd.CommandText = query;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CarModel row = new CarModel();

                        row.CarMakeId = (int)dr["CarMakeId"];
                        row.CarModelId = (int)dr["CarModelId"];
                        row.CarModelName = dr["CarModelName"].ToString();

                        carModels.Add(row);
                    }
                }
            }

            return carModels;
        }
    }
}
