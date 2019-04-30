using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domainlayer;


namespace ApplicationLayer
{
    public class OrderRepository
    {
		private Dictionary<int, Order> orders = new Dictionary<int, Order>();
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
		public Dictionary<int, Order> GetOrderDic()
		{
			return orders;
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
