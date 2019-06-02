using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domainlayer;
using library;

namespace ApplicationLayer
{
	public delegate void RegisterOrderEventHandler<OrderRepository>(object source, EventArgs e);
    

    public class ImportController
    {
        public event EventHandler<OrderRepository> OrderRegistered;

        private string[] orderItems;
		private string[] sampleTypeArray;
        private string[] dateTimeArray;
		private List<string> sampleTypeList;
        
        private OrderRepository orderRepo;
		

		public void RegisterOrders(object fileNameObj)
		{
            string fileName = (string)fileNameObj;
            string relativePath = GetFilePath(fileName);
            List<IOrder> newOrders = new List<IOrder>();
            orderRepo = OrderRepository.GetOrderRepo();

            FileStream fs = new FileStream(relativePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            using (StreamReader reader = new StreamReader(fs))
            {
                while (true)
                {
                    string line = string.Empty;
                    
                    while ((line = reader.ReadLine()) != null)
                    {
                        orderItems = line.Split(';');

                        dateTimeArray = orderItems[6].Split(',');
                        sampleTypeArray = orderItems[7].Split(',');
                        sampleTypeList = ConvertArrayToList(sampleTypeArray);
                        
                        DateTime timeStamp = new DateTime  (Convert.ToInt32(dateTimeArray[0]), Convert.ToInt32(dateTimeArray[1]), 
                                                            Convert.ToInt32(dateTimeArray[2]), Convert.ToInt32(dateTimeArray[3]), 
                                                            Convert.ToInt32(dateTimeArray[4]), Convert.ToInt32(dateTimeArray[5]));

                        Order order = new Order(orderItems[0], orderItems[1], Convert.ToInt32(orderItems[2]), orderItems[3], 
                                                 Convert.ToInt32(orderItems[4]), orderItems[5], timeStamp, sampleTypeList);
                        newOrders.Add(order);
                    }
                    if (newOrders.Count != 0)
                    {
                        orderRepo.GetListOfOrdersToAdd().Clear();

                        orderRepo.GetListOfOrdersToAdd().AddRange(newOrders.Except(orderRepo.ReturnOrdersAsList()));
                        
                        foreach (IOrder item in orderRepo.GetListOfOrdersToAdd())
                        {
                            if (orderRepo.GetOrderDic().Count != 0)
                            {
                                int id = orderRepo.GetOrderDic().Keys.Last() + 1;
                                item.OrderId = id;
                                orderRepo.AddOrder(id, item);
                            }
                            else
                            {
                                orderRepo.AddOrder(1000, item);
                            }
                        }
                        OnOrderRegistered();
                        newOrders.Clear();
                    }
                }
            }
        }



        //List<IOrder> Orders = orderRepo.ReturnOrdersAsList();


        //foreach (IOrder item in OrdersToAdd.ToList())
        //{
        //    foreach (IOrder i in Orders.ToList())
        //    {
        //        if (item.Email.Equals(i.Email) && item.TimeStamp == i.TimeStamp)
        //        {
        //            orderRepo.GetListOfOrdersToAdd().Remove(item);
        //        }
        //    }
        //    if (orderRepo.GetListOfOrdersToAdd().Contains(item))
        //    {
        //        if (orderRepo.GetOrderDic().Count == 0)
        //        {
        //            orderRepo.AddOrder(1000, item);
        //        }
        //        else
        //        {
        //            int id = orderRepo.GetOrderDic().Keys.Last() + 1;
        //            item.OrderId = id;
        //            orderRepo.AddOrder(id, item);
        //        }
        //    }
        //}

        public void DeleteOrderItemEvent(object fileNameObj)
        {
            string fileName = (string)fileNameObj;
			string relativePath = GetFilePath(fileName);
            FileStream filestream = new FileStream(relativePath, FileMode.Truncate, FileAccess.ReadWrite);
			filestream.Close();
        }

        public void RegisterOrdersInGUI(object fileNameObj, string orderLinesToAdd)
        {
            string filename = (string)fileNameObj;
            FileStream filestream = new FileStream(filename, FileMode.Append, FileAccess.Write);
            byte[] buffer = Encoding.Default.GetBytes(orderLinesToAdd);
            filestream.Write(buffer, 0, buffer.Length);
            filestream.Flush();
            filestream.Close();
        }

        public void OnOrderRegistered()
        {
            if (OrderRegistered != null)
            {
                OrderRegistered(this, OrderRepository.GetOrderRepo());
            }
        }
        
        public List<string> ConvertArrayToList(string[] c)
		{
			List<string> ListConvert = new List<string>();
			foreach (string item in c)
			{
				ListConvert.Add(item);
			}

			return ListConvert;
		}
		
		public List<string> GetSampleTypeList()
		{
			return sampleTypeList;
		}

		public string GetFilePath(string fileName)
		{
			string destination = Environment.CurrentDirectory;

			for (int i = 0; i < 3; i++)
			{
				destination = Directory.GetParent(destination).ToString();
			}

			return destination + "/Domainlayer/Content/" + fileName;
		}
	}
}
