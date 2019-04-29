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


		public void RefreshOrders()
		{
			int i = 0;

			Thread thread = new Thread(iController.RegisterOrders);

			thread.Start("TestText.txt");

			do
			{
				Thread.Sleep(1000);
				i++;
			}
			while (i < 1);
		}

    }
}
