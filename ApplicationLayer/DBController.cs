using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Domainlayer;
using library;
namespace ApplicationLayer
{
	public class DBController
	{
		private ErrorController error = new ErrorController();
		private SqlConnection con;
        
		// connectionString som benyttes til at tilgå databasen. ((( NOT SAFE )))
        private static string connectionstring =
            "Server = den1.mssql8.gear.host; Database = uniggardin; User Id = uniggardin; Password = Iy71?B8skjQ_";

		// En metode der er subscribed til "OrderRegistered" tager objekt source, som parameter samt OrderRepository
		// OrderRepository kan benyttes som EventArgs input, da den arver fra eventArgs
        public void OnOrderRegistered(object source, OrderRepository e)
        {
			// kalder metoden SaveOrder med en liste fra OrderRepository som parameter via metoden "GetListOfOrdersToAdd"
            SaveOrder(e.GetListOfOrdersToAdd());
        }
		// Denne metode gemmer enhver ordre i listen den modtager på databasen
        public void SaveOrder(List<IOrder> order)
        {
            using (con = new SqlConnection(connectionstring))
            {
                try
                {
                    con.Open();
                    foreach (IOrder item in order)
                    {
                        SqlCommand cmd1 = new SqlCommand("spSaveOrder", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add(new SqlParameter("@Customer_FirstName", item.FirstName));
                        cmd1.Parameters.Add(new SqlParameter("@Customer_SurName", item.LastName));
                        cmd1.Parameters.Add(new SqlParameter("@Country", item.Country));
                        cmd1.Parameters.Add(new SqlParameter("@Customer_Phone", item.PhoneNumber));
                        cmd1.Parameters.Add(new SqlParameter("@Customer_Mail", item.Email));
                        cmd1.Parameters.Add(new SqlParameter("@ZIP", item.Zip));
                        cmd1.Parameters.Add(new SqlParameter("@Order_Date", item.TimeStamp));


						// tilføjer hver SampleType fra en ordre
                        for (int i = 0; i < item.SampleType.Count; i++)
                        {
                            int place = i + 1;
                            cmd1.Parameters.Add(new SqlParameter("@" + $"SampleType{place}", item.SampleType[i]));
                        }

                        cmd1.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
					// gemmer errors i en tekstfil (((Skulle være SQLException)))
                    error.SaveErrorLog(e.ToString());
                }
            }
        }
		// Tilføjer quantity til en nuværende SampleType
        public void InsertIntoStock(int Quantity, ErrorController error)
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
		// Fjerner ordrer fra "ORDERS" table i databasen
		public void FinishedOrder(IOrder order, ErrorController error)
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

		// Hvis en ordre bliver pakket, opdaterer denne metode databasen.
		// hardkodet til at ændre værdien med en, da det ikke giver mening at en ordre har mere end en
		// stofprøve per ordre.
		public void UpdateStock(IOrder order, ErrorController error)
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
				// gemmer fejlbesked der kommer fra SQL, hvis der opstod fejl.
				catch (SqlException e)
				{
					error.SaveErrorLog(e.ToString());
				}
			}
		}
        // Indlæser ordre fra databasen ind i programmet.
		public void GetOrdersFromDatabase(OrderRepository oRepo, ErrorController error)
		{
			using (con = new SqlConnection(connectionstring))
			{
				con.Open();
				SqlCommand cmd5 = new SqlCommand("SelectAllOrders", con);
				cmd5.CommandType = CommandType.StoredProcedure;
				
                using (SqlDataReader reader = cmd5.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
						// Reader.Read returnerer en boolean værdi om den kan læse den næste row i databasen. Så længe
						// der er data den kan læse, vil metoden fortsætte.
                        while (reader.Read())
                        {
                             string customerEmail = reader["Customer_Mail"].ToString();
                             string customerFirstname = reader["Customer_FirstName"].ToString();
                             string customerLastName = reader["Customer_SurName"].ToString();
                             int orderId = Convert.ToInt32(reader["Order_ID"].ToString());
                             int zip = Convert.ToInt32(reader["ZIP"].ToString());
                             string country = reader["Country"].ToString();
                             int phone = Convert.ToInt32(reader["Customer_Phone"].ToString());
                             DateTime timeStamp = Convert.ToDateTime(reader["Order_Date"].ToString());
                             List<string> sampletypelist = GetSampleTypesWithOrderID(orderId, error);
                              oRepo.AddOrder(new Order(orderId, customerFirstname, customerLastName, zip, country, phone, customerEmail, timeStamp, sampletypelist));
                            
                        }
                    }
                }
            }
		}
		// Henter alle SampleTypes for en specifik ordre ( int orderId )
		private List<string> GetSampleTypesWithOrderID(int orderId, ErrorController error)
		{
			List<string> sampleTypeList = new List<string>();
			using(con = new SqlConnection(connectionstring))
			{

				SqlCommand cmd6 = new SqlCommand("spGetOrderLinesWithOrderId", con);

				try
				{
					con.Open();
					cmd6.CommandType = CommandType.StoredProcedure;
					cmd6.Parameters.Add(new SqlParameter("@Orderid", orderId));
					cmd6.ExecuteNonQuery();
				}
				catch (SqlException e)
				{
					error.SaveErrorLog(e.ToString());
				}

				try
				{
					SqlDataReader reader = cmd6.ExecuteReader();

					if (reader.HasRows)
					{
						while (reader.Read())
						{
							string sampletype = reader["Sample_ID"].ToString();
							sampleTypeList.Add(sampletype);
						}
					}
				}
				catch (SqlException e)
				{
					error.SaveErrorLog(e.ToString());
				}
			}
			return sampleTypeList;
		}
		// kaldete stored procedure "spGetLowStockSampleTypes i databasen. Metoden læser alle fabricSampleTypes hvor 
		// quantity er under 10. alle læste linjer tilføjes til FabricSampleRepository.
		public bool GetLowStockSampleTypes(bool ran, FabricSampleRepository fRepo, ErrorController error)
		{
			using (con = new SqlConnection(connectionstring))
			{
				con.Open();
				SqlCommand cmd7 = new SqlCommand("spGetLowStockSampleTypes", con);
				cmd7.CommandType = CommandType.StoredProcedure;
				cmd7.ExecuteNonQuery();

				try
				{
					SqlDataReader reader = cmd7.ExecuteReader();

					if (reader.HasRows)
					{
						ran = true;
						while (reader.Read())
						{
							string sampleID = reader["Sample_ID"].ToString();
							int quantity = Convert.ToInt32(reader["Quantity"].ToString());
							string productName = reader["Product_Name"].ToString();
							if(productName == null)
							{
								FabricSample fs = new FabricSample(sampleID, quantity);
								fRepo.AddFabricSample(fs);
							}
							else
							{
								FabricSample fs = new FabricSample(sampleID, quantity, productName);
								fRepo.AddFabricSample(fs);
							}
						}
					}

				}

				catch (SqlException e)
				{
					error.SaveErrorLog(e.ToString());
				}
			}
			return ran;
		}
	}
}