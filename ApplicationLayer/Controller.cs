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
	public delegate void UpdateStockEventHandler<FabricSampleRepository>(object sender, EventArgs e);

    public class Controller
    {
		public event EventHandler<FabricSampleRepository> StockUpdated;
        private OrderRepository oRepo;
        private DBController dbController;
		private ImportController iController = new ImportController();
        private ErrorController error = new ErrorController();
		private FabricSampleRepository fRepo = new FabricSampleRepository();
		public bool programRunning = true;
		object fileNameObj = "Orders.txt";


		//public void ExportOrder(Order order)
  //      {
  //          dbController.SaveOrder(order);
  //      }
       
        public void ImportOrder(string fileName, ImportController importcontroller)
        {
            fileNameObj = fileName;
            RefreshOrders(fileName, importcontroller);
        }
        public void ImportOrder(string fileName, ImportController importcontroller, string orderToAdd)
        {
            fileNameObj = fileName;
            importcontroller.RegisterOrdersInGUI(fileNameObj, orderToAdd);
        }
        public void RefreshOrders(string fileName, ImportController importcontroller)
        {
            fileNameObj = fileName;

            Thread thread = new Thread(importcontroller.RegisterOrders);
            thread.IsBackground = true;
            thread.Start(fileNameObj);
        }

        public Dictionary<int, IOrder> ShowAllOrders()
		{
			oRepo = OrderRepository.GetOrderRepo();
			return oRepo.GetOrderDic();
		}

		public void OrderPacked(IOnStockUpdatedSubscriber subscriber, IOrder orderToRemove)
		{
			StockUpdated += subscriber.OnStockUpdated;
			dbController.UpdateStock(orderToRemove);
			dbController.GetLowStockSampleTypes();
			StockUpdated(this, fRepo);
		}

		public void DeleteOrderFromDatabase(IOrder orderToRemove)
		{
			dbController.FinishedOrder(orderToRemove);
		}
	}
}
