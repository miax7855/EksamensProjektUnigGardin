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
        private ErrorController error = new ErrorController();
		public bool programStillRunning = true;
		object fileNameObj = "Orders.txt";


		public void ExportOrder(Order order)
        {
            dbController.SaveOrder(order);
        }
        public List<IOrder> ReturnRepoList()
        {
            return oRepo.ReturnCurrentOrdersAsList();
        }

		public void ImportOrder(string filepath)
		{
			fileNameObj = filepath;
			iController.RegisterOrders(fileNameObj);
			RefreshOrders(filepath);
		}

		public void RefreshOrders(string filepath)
		{
			fileNameObj = filepath;
			int i = 0;

			Thread thread = new Thread(iController.RegisterOrders);

			thread.Start(fileNameObj);

			do
			{
				Thread.Sleep(1000);
				i++;
			}
			while (i < 10);
			iController.RegisterOrders(fileNameObj);
		}

		public Dictionary<int, IOrder> ShowAllOrders()
		{
			oRepo = OrderRepository.GetOrderRepo();
			return oRepo.GetOrderDic();
		}
    }
}
