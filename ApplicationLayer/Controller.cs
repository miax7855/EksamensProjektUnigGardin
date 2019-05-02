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

		private DBController dbController;
		private ImportController iController;
        OrderRepository oRepo = new OrderRepository();

        public bool programStillRunning = true;

        public void ExportOrder(Order order)
        {
            dbController.SaveOrder(order);
        }
        List<IOrder> listOfOrders = new List<IOrder>();
        public List<IOrder> ReturnRepoList()
        {
            foreach (KeyValuePair<int, IOrder> pair in oRepo.ReturnOrders())
            {
                listOfOrders.Add(pair.Value);
            }
            return listOfOrders;
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
