using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domainlayer;

namespace ApplicationLayer
{
    public class Controller
    {
        private OrderRepository oRepo;
        private DBController dbController;
		private ImportController iController = new ImportController();
        private Errors error = new Errors();
		public bool programStillRunning = true;

        public void ExportOrder(Order order)
        {
            dbController.SaveOrder(order);
        }


		public void ImportOrder()
		{
			object fileNameObj = "TestText.txt";
			iController.RegisterOrders(fileNameObj);
			RefreshOrders();
		}

		public void RefreshOrders()
		{
			object fileNameObj = "TestText.txt";
			//int i = 0;

			//Thread thread = new Thread(iController.RegisterOrders);

			//thread.Start("TestText.txt");

			//do
			//{
			//	Thread.Sleep(1000);
			//	i++;
			//}
			//while (i < 1);
			iController.RegisterOrders(fileNameObj);
		}

		public IDictionary<int, Order> ShowAllOrders()
		{
			oRepo = new OrderRepository();
			return  oRepo.GetOrders();
		}
    }
}
