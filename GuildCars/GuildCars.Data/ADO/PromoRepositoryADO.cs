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
    public class PromoRepositoryADO : IPromoRepository
    {
        public void Delete(int promoCodeId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("PromoDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PromoCodeId", promoCodeId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public List<Promo> GetAll()
        {
            List<Promo> promos = new List<Promo>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("PromosSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Promo currentRow = new Promo();
                        currentRow.PromoCodeId = (int)dr["PromoCodeId"];
                        currentRow.PromotionName = dr["PromotionName"].ToString();
                        currentRow.PromotionDescription = dr["PromotionDescription"].ToString();                        

                        if (dr["ImageFileName"] != DBNull.Value)
                            currentRow.ImageFileName = dr["ImageFileName"].ToString();

                        promos.Add(currentRow);
                    }
                }
            }

            return promos;
        }

        public void Insert(Promo promo)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("PromoInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@PromoCodeId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@PromotionName", promo.PromotionName);
                cmd.Parameters.AddWithValue("@PromotionDescription", promo.PromotionDescription);
                cmd.Parameters.AddWithValue("@ImageFileName", "stockpromo.jpg");

                cn.Open();

                cmd.ExecuteNonQuery();

                promo.PromoCodeId = (int)param.Value;
            }
        }
    }
}
