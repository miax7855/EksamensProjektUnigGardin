using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domainlayer;
using library;


namespace ApplicationLayer
{
    public class OrderRepository : EventArgs
    {
		private Dictionary<int, IOrder> orders = new Dictionary<int, IOrder>();
        private List<IOrder> orderAsList = new List<IOrder>();

        private List<IOrder> listOfOrdersToAdd = new List<IOrder>();

        static private OrderRepository orderRepository;
		public static OrderRepository GetOrderRepo()
		{
			if (orderRepository == null)
			{
				orderRepository = new OrderRepository();

				return orderRepository;
			}
			else
			{
				return orderRepository;
			}
		}
        public List<IOrder> GetListOfOrdersToAdd()
        {
            return listOfOrdersToAdd;
        }
		public Dictionary<int, IOrder> GetOrderDic()
		{
			return orders;
		}
        public List<IOrder> ReturnOrdersAsList()
        {
            orderAsList.Clear();
            foreach (KeyValuePair<int, IOrder> item in orders)
            {
                orderAsList.Add(item.Value);
            }
            return orderAsList;
        }
        public void AddOrder(IOrder o)
        {
			orders.Add(o.OrderId, o);
        }
        public void AddOrder(int key, IOrder o)
        {
            orders.Add(key, o);
        }
        public void RemoveOrder(IOrder o)
        {
			orders.Remove(o.OrderId);
        }
        
    }
}
