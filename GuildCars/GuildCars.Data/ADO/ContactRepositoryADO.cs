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
    public class ContactRepositoryADO : IContactRepository
    {
        public void Insert(Contact contact)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ContactsInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@ContactId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@ContactName", contact.ContactName);
                cmd.Parameters.AddWithValue("@EmailAddress", contact.EmailAddress);
                cmd.Parameters.AddWithValue("@TelephoneNumber", contact.TelephoneNumber);

                if (string.IsNullOrEmpty(contact.ContactMessage.ToString()))
                {
                    cmd.Parameters.AddWithValue("@ContactMessage", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ContactMessage", contact.ContactMessage);
                }

                cn.Open();

                cmd.ExecuteNonQuery();

                contact.ContactId = (int)param.Value;
            }
        }

        public List<Contact> GetAll()
        {
            List<Contact> contacts = new List<Contact>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ContactsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Contact currentRow = new Contact();
                        currentRow.ContactId = (int)dr["ContactId"];
                        currentRow.ContactName = dr["ContactName"].ToString();
                        currentRow.EmailAddress = dr["EmailAddress"].ToString();
                        currentRow.TelephoneNumber = dr["TelephoneNumber"].ToString();
                        currentRow.ContactMessage = dr["ContactMessage"].ToString();

                        contacts.Add(currentRow);
                    }
                }
            }

            return contacts;
        }
    }
}
