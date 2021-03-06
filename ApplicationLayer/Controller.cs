﻿using System;
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
        private DBController dbController = new DBController();
		private ImportController iController = new ImportController();
        private ErrorController error = new ErrorController();
		private FabricSampleRepository fRepo = FabricSampleRepository.GetFabricSampleRepo();
		private bool programRunning = true;
		private object fileNameObj = "Orders.txt";
		private bool ran = false;
        
        public void SubscribersOrderRegistered(ISubscribersOrderRegistered subscriber)
        {
            iController.OrderRegistered += subscriber.OnOrderRegistered;
            iController.OrderRegistered += dbController.OnOrderRegistered;
        }

        public void ImportOrder(string fileName)
        {
            fileNameObj = fileName;
            RefreshOrders(fileName);
        }
        
        public void RefreshOrders(string fileName)
        {
            fileNameObj = fileName;

            Thread thread = new Thread(iController.RegisterOrders);
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
			//StockUpdated += subscriber.OnStockUpdated;
			dbController.UpdateStock(orderToRemove, error);
			ran = dbController.GetLowStockSampleTypes(ran, fRepo, error);

			if (ran)
			{
				StockUpdated(this, fRepo);
                ran = false;
            }
			
			fRepo.CLearSampleTypeList();
		}

		public void DeleteOrderFromDatabase(IOrder orderToRemove)
		{
			dbController.FinishedOrder(orderToRemove, error);
		}
		public void ConGetOrdersFromDataBase()
		{
			dbController.GetOrdersFromDatabase(oRepo, error);
		}
		public OrderRepository ReturnOrderRepository()
		{
			oRepo = OrderRepository.GetOrderRepo();
			return oRepo;
		}

		public FabricSampleRepository ReturnFabricSampleRepository()
		{
			fRepo = FabricSampleRepository.GetFabricSampleRepo();
			return fRepo;
		}
	}
}
