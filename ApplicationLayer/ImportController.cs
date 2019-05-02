using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domainlayer;






namespace ApplicationLayer
{
	

	public class ImportController
    {
		string[] orderItems;
		string[] sampleTypeArray;
		List<string> sampleTypeList;
		OrderRepository orderRepo;
		IDictionary<int, Order> orders;

		public string[] ReadLines(string fileName)
        {
			string parentDirectory = GetFilePath();
            string[] text = File.ReadAllLines(parentDirectory + "/Domainlayer/Content/" + fileName);
			return text;
        }
		public void RegisterOrders(object fileNameObj)
		{
			
			string fileName = (string)fileNameObj;
			orderRepo = OrderRepository.GetOrderRepo();
			orders = orderRepo.GetOrderDic();
			string[] text = ReadLines(fileName);

			foreach (string item in text)
			{
				orderItems = item.Split(';');
				if (!orders.ContainsKey(Convert.ToInt32(orderItems[0])))
				{
					sampleTypeArray = orderItems[8].Split(',');
					sampleTypeList = ConvertArrayToList(sampleTypeArray);
					Order order = new Order(Convert.ToInt32(orderItems[0]), orderItems[1], orderItems[2], Convert.ToInt32(orderItems[3]), orderItems[4], orderItems[5], Convert.ToInt32(orderItems[6]), orderItems[7], sampleTypeList);
					orderRepo.AddOrder(order);
				}
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

		public string GetFilePath()
		{
			string destination = Environment.CurrentDirectory;

			for (int i = 0; i < 3; i++)
			{
				destination = Directory.GetParent(destination).ToString();
			}

			return destination;
		}
	}
}
