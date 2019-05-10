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
		private ErrorController error = new ErrorController();
		private SqlConnection con;

		private static string connectionstring =
			"Server = den1.mssql8.gear.host; Database = uniggardin; User Id = uniggardin; Password = Iy71?B8skjQ_";
		public void SaveOrder(Order order)
		{
			using (con = new SqlConnection(connectionstring))
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
				catch (Exception e)
				{
					error.SaveErrorLog(e.ToString());
				}
			}
		}
		public void InsertIntoOrderLines(Order order)
		{
			using (con = new SqlConnection(connectionstring))
			{
				try
				{
					con.Open();
					SqlCommand cmd2 = new SqlCommand("spAddSamplesToOrder", con);
					cmd2.CommandType = CommandType.StoredProcedure;
					cmd2.Parameters.Add(new SqlParameter("@OrderID", order.OrderId));
					cmd2.Parameters.Add(new SqlParameter("@SampleType1", order.SampleType[0]));
					cmd2.Parameters.Add(new SqlParameter("@SampleType2", order.SampleType[1]));
					cmd2.Parameters.Add(new SqlParameter("@SampleType3", order.SampleType[2]));
					cmd2.Parameters.Add(new SqlParameter("@SampleType4", order.SampleType[3]));
					cmd2.Parameters.Add(new SqlParameter("@SampleType5", order.SampleType[4]));
					cmd2.Parameters.Add(new SqlParameter("@SampleType6", order.SampleType[5]));
					cmd2.Parameters.Add(new SqlParameter("@SampleType7", order.SampleType[6]));
					cmd2.Parameters.Add(new SqlParameter("@SampleType8", order.SampleType[7]));
					cmd2.Parameters.Add(new SqlParameter("@SampleType9", order.SampleType[8]));
					cmd2.Parameters.Add(new SqlParameter("@SampleType10", order.SampleType[9]));

					cmd2.ExecuteNonQuery();
				}
				catch (SqlException e)
				{
					error.SaveErrorLog(e.ToString());
				}
			}
		}
		public void InsertIntoStock(int Quantity)
		{
			using (con = new SqlConnection(connectionstring))
			{
				try
				{
					con.Open();
					SqlCommand cmd3 = new SqlCommand("spInsertIntoStock", con);
					cmd3.CommandType = CommandType.StoredProcedure;
					cmd3.Parameters.Add(new SqlParameter("@Quantity", Quantity));

					cmd3.ExecuteNonQuery();
				}
				catch (Exception e)
				{

					error.SaveErrorLog(e.ToString());
				}
			}
		}
		public void FinishedOrder(Order order)
		{
			using (SqlConnection con3 = new SqlConnection(connectionstring))
			{
				try
				{
					con3.Open();
					SqlCommand cmd3 = new SqlCommand("spRemoveFinishedOrder", con3);
					cmd3.CommandType = CommandType.StoredProcedure;
					cmd3.Parameters.Add(new SqlParameter("@Order_ID", order.OrderId));
					cmd3.ExecuteNonQuery();
				}
				catch (Exception e)
				{

					error.SaveErrorLog(e.ToString());
				}
			}
		}


		public void UpdateStock(Order order)
		{
			using (con = new SqlConnection(connectionstring))
			{
				try
				{
					con.Open();


					foreach (string s in order.SampleType)
					{
						SqlCommand cmd4 = new SqlCommand("spUpdateStock", con);
						cmd4.CommandType = CommandType.StoredProcedure;
						cmd4.Parameters.Add(new SqlParameter("@Quantity", 1));
						cmd4.Parameters.Add(new SqlParameter("@SampleType", s));

						cmd4.ExecuteNonQuery();
					}


				}
				catch (SqlException e)
				{
					error.SaveErrorLog(e.ToString());
				}
			}
		}


		public void GetOrdersFromDatabase()
		{
			using (con = new SqlConnection(connectionstring))
			{
				con.Open();
				SqlCommand cmd5 = new SqlCommand("SelectAllOrders", con);
				cmd5.CommandType = CommandType.StoredProcedure;

				SqlDataReader reader = cmd5.ExecuteReader();
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						string customerEmail = reader["Customer_Mail"].ToString();
						string customerFirstname = reader["Customer_FirstName"].ToString();
						string customerLastName = reader["Customer_SurName"].ToString();
						int orderId = Convert.ToInt32(reader["Order_ID"].ToString());
						int zip = Convert.ToInt32(reader["ZIP"].ToString());
						string city = reader["Customer_Mail"].ToString();
						string country = reader["Customer_Mail"].ToString();
						int phone = Convert.ToInt32(reader["Customer_Phone"].ToString());

					}
				}
			}
		}
	}
}