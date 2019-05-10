﻿using System;
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
        private Controller controller = new Controller();
		private ErrorController error = new ErrorController();
		private SqlConnection con;
        public ImportController importController = new ImportController();
        
        private static string connectionstring =
            "Server = den1.mssql8.gear.host; Database = uniggardin; User Id = uniggardin; Password = Iy71?B8skjQ_";

        public void OnOrderRegistered(object source, OrderRepository e)
        {
            SaveOrder(e.listOfOrdersToAdd);
        }
        public void SaveOrder(List<IOrder> order)
        {
            using (con = new SqlConnection(connectionstring))
            {
                
                //try
                //{
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
                        cmd1.Parameters.Add(new SqlParameter("@City", item.City));
                        cmd1.Parameters.Add(new SqlParameter("@Order_Date", DateTime.Now));

                        cmd1.ExecuteNonQuery();

                        InsertIntoOrderLines(item, con);
                    }
                //}
                //catch (Exception e)
                //{
                //    error.SaveErrorLog(e.ToString());
                //}
            }
        }
        public void InsertIntoOrderLines(IOrder order, SqlConnection con)
        {
            SqlCommand cmd2 = new SqlCommand("spAddSamplesToOrder", con);
            cmd2.CommandType = CommandType.StoredProcedure;

            for (int i = 0; i < order.SampleType.Count; i++)
            {
                int place = i + 1;
                cmd2.Parameters.Add(new SqlParameter("@" + $"SampleType{place}", order.SampleType[i]));

            }
            cmd2.ExecuteNonQuery();
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
				catch(SqlException e)
				{
					error.SaveErrorLog(e.ToString());
				}
			}
		}

    }
}
