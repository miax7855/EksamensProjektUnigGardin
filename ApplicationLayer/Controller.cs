using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domainlayer;
using library;

namespace ApplicationLayer
{
    public class Controller
    {
        private OrderRepository oRepo;
        private DBController dbController;
		private ImportController iController = new ImportController();
        private Errors error = new Errors();
		public bool programStillRunning = true;
		object fileNameObj = "Orders.txt";


		public void ExportOrder(Order order)
        {
            dbController.SaveOrder(order);
        }
        

		public void ImportOrder(string fileName)
		{
			fileNameObj = fileName;
			iController.RegisterOrders(fileNameObj);
			RefreshOrders(fileName);
		}

		public void RefreshOrders(string fileName)
		{
			fileNameObj = fileName;
			//int i = 0;

			Thread thread = new Thread(iController.RegisterOrders);

			thread.Start(fileNameObj);

			//do
			//{
			//	Thread.Sleep(1000);
			//	i++;
			//}
			//while (i < 10);
		}

		public Dictionary<int, IOrder> ShowAllOrders()
		{
			oRepo = OrderRepository.GetOrderRepo();
			return oRepo.GetOrderDic();
		}
    }
}
