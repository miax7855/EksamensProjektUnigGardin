using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Domainlayer;
namespace ApplicationLayer
{
    public class DBController
    {
        private Controller controller = new Controller();
        private static string connectionstring =
            "Server = den1.mssql8.gear.host; Database = uniggardin; User Id = uniggardin; Password = Iy71?B8skjQ_";
        public void SaveOrder(Order order)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("spSaveOrdersS", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add(new SqlParameter("@Customer_FirstName", order.FirstName));
                    cmd1.Parameters.Add(new SqlParameter("@Customer_SurName", order.LastName));
                    cmd1.Parameters.Add(new SqlParameter("@Country", order.Country));
                    cmd1.Parameters.Add(new SqlParameter("@Customer_Phone", order.PhoneNumber));
                    cmd1.Parameters.Add(new SqlParameter("@Customer_Mail", order.Email));
                    cmd1.Parameters.Add(new SqlParameter("@ZIP", order.Zip));
                    cmd1.Parameters.Add(new SqlParameter("@City", order.City));
                    cmd1.Parameters.Add(new SqlParameter("@Order_Date", DateTime.Now));
                    cmd1.Parameters.Add(new SqlParameter("@Order_Type", order.SampleType));
					cmd1.ExecuteNonQuery();
                }
                catch(SqlException e)
                {
                    Console.WriteLine("Shiiiit" + e);
                }
            }
        }

        
    }
}
