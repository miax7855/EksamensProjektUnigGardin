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
        private OrderRepository oRepo = new OrderRepository();
        private DBController dbController = new DBController();
		private ImportController iController = new ImportController();
        private Errors error = new Errors();
		public bool programStillRunning = true;
        


		public void RefreshOrders()
		{
			Thread thread = new Thread(iController.RegisterOrders);

			do
			{
				Thread.Sleep(5000);
				thread.Start();
			}
			while (programStillRunning);
		}

    }
}
