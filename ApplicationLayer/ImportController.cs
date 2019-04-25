using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensProjektUnigGardin
{
    public class ImportController
    {
		string[] text;
		string[] orderItems;
		string[] sampleTypeArray;
		List<string> sampleTypeList;
		OrderRepository orderRepo;

		public void readLines()
        {
            string[] text = File.ReadAllLines("Orders.txt");

            
        }
		public void RegisterOrders()
		{
			foreach (string item in text)
			{
				string[] orderItems = item.Split(';');
				string[] sampleTypeArray = orderItems[7].Split(',');
				ConvertArrayToList(sampleTypeArray);
				Order order = new Order(orderItems[0], orderItems[1], Convert.ToInt32(orderItems[2]), orderItems[3], orderItems[4], Convert.ToInt32(orderItems[5]), orderItems[6], sampleTypeList);
				orderRepo.AddOrder(order);
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



	}
}
