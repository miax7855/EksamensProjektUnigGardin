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
		List<string> sampleTypeList;
		OrderRepository orderRepo;
		IDictionary<int, IOrder> orders;
        List<IOrder> listOfOrders = new List<IOrder>();
        
		public void RegisterOrders(object fileNameObj)
		{
            string fileName = (string)fileNameObj;
            string relativePath = GetFilePath(fileName);

            FileStream fs = new FileStream(relativePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            using (StreamReader reader = new StreamReader(fs))
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    orderRepo = OrderRepository.GetOrderRepo();
                    orders = orderRepo.GetOrderDic();

                    if (!String.IsNullOrWhiteSpace(line))
                    {
                        orderItems = line.Split(';');
                        if (!orders.ContainsKey(Convert.ToInt32(orderItems[0])))
                        {
                            sampleTypeArray = orderItems[8].Split(',');
                            sampleTypeList = ConvertArrayToList(sampleTypeArray);
                            Order order = new Order(Convert.ToInt32(orderItems[0]), orderItems[1], orderItems[2], Convert.ToInt32(orderItems[3]), 
                                orderItems[4], orderItems[5], Convert.ToInt32(orderItems[6]), orderItems[7], sampleTypeList);

                            orderRepo.AddOrder(order);

                            OnOrderRegistered();C:\Users\Mia\source\repos\EksamensProjektUnigGardin\EksamensProjektUnigGardin\App.config
                        }
                    }
                }
            }
        }
        public void DeleteOrderItemEvent(object fileNameObj, IOrder io)
        {
            string fileName = (string)fileNameObj;
            FileStream filestream = new FileStream(fileName, FileMode.Truncate, FileAccess.ReadWrite);

            
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
