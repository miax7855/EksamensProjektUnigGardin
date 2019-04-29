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

		private DBController dbController;
		private ImportController iController;
		public bool programStillRunning = true;

        public void ExportOrder(Order order)
        {
            dbController.SaveOrder(order);
        }
        public List<Order> ReturnRepoList()
        {
            return oRepo.ReturnCurrentOrders();
        }

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
