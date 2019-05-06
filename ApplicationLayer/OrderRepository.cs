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
		public Dictionary<int, IOrder> GetOrderDic()
		{
			return orders;
		}
        public List<IOrder> ReturnOrdersAsList()
        {
            foreach (KeyValuePair<int, IOrder> item in orders)
            {
                orderAsList.Add(item.Value);
            }
            return orderAsList;
        }
        public void AddOrder(Order o)
        {
			orders.Add(o.OrderId, o);
        }
        public void RemoveOrder(Order o)
        {
			orders.Remove(o.OrderId);
        }
        

    }
}
