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

        string[] orderItems;
		string[] sampleTypeArray;
        string[] dateTimeArray;
		List<string> sampleTypeList;
        
        OrderRepository orderRepo;
		

		public void RegisterOrders(object fileNameObj)
		{
            string fileName = (string)fileNameObj;
            string relativePath = GetFilePath(fileName);

            orderRepo = OrderRepository.GetOrderRepo();

            FileStream fs = new FileStream(relativePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            using (StreamReader reader = new StreamReader(fs))
            {
                while (true)
                {
                    string line = string.Empty;

                    orderRepo.GetListOfOrdersToAdd().Clear();
                    
                    while ((line = reader.ReadLine()) != null)
                    {
                        orderItems = line.Split(';');

                        dateTimeArray = orderItems[7].Split(',');
                        sampleTypeArray = orderItems[8].Split(',');
                        sampleTypeList = ConvertArrayToList(sampleTypeArray);
                        
                        DateTime timeStamp = new DateTime  (Convert.ToInt32(dateTimeArray[0]), Convert.ToInt32(dateTimeArray[1]), 
                                                            Convert.ToInt32(dateTimeArray[2]), Convert.ToInt32(dateTimeArray[3]), 
                                                            Convert.ToInt32(dateTimeArray[4]), Convert.ToInt32(dateTimeArray[5]));

                        Order order = new Order(orderItems[0], orderItems[1], Convert.ToInt32(orderItems[2]), orderItems[3], 
                                                 Convert.ToInt32(orderItems[5]), orderItems[6], timeStamp, sampleTypeList);

                        orderRepo.GetListOfOrdersToAdd().Add(order);
                    }
                    if (orderRepo.GetListOfOrdersToAdd().Count != 0)
                    {
                        //List<IOrder> lst = orderRepo.listOfOrdersToAdd;
                        foreach (IOrder item in orderRepo.GetListOfOrdersToAdd().ToList())
                        {
                            foreach (IOrder thing in orderRepo.ReturnOrdersAsList())
                            {
                                if (item.Email.Equals(thing.Email) && item.TimeStamp == thing.TimeStamp)
                                {
                                    orderRepo.GetListOfOrdersToAdd().Remove(item);
                                }
                            }
                            //orderRepo.listOfOrdersToAdd.Remove(orderRepo.ReturnOrdersAsList().Find(x => x.Email.Equals(item.Email) && x.TimeStamp == item.TimeStamp));
                        }

                        foreach (IOrder item in orderRepo.GetListOfOrdersToAdd())
                        {
                            if (orderRepo.GetOrderDic().Count == 0)
                            {
                                orderRepo.AddOrder(0, item);
                            }
                            else
                            {
                                orderRepo.AddOrder(orderRepo.GetOrderDic().Keys.Last() + 1, item);
                            }
                        }
                        OnOrderRegistered();
                    }
                }
            }
        }
        
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
