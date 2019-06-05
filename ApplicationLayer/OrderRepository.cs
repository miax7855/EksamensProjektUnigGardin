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
		// Singleton
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
		//oprettes til DCD
		//public List<Order> ReturnOrdersWithoutIDs()
		//{
		//    List<Order> noIds = new List<Order>();
		//    foreach (KeyValuePair<int, IOrder> item in orders)
		//    {
		//        noIds.Add(new Order(item.Value.OrderId = 0, item.Value.FirstName, item.Value.LastName, item.Value.Zip, item.Value.Country, 
		//                            item.Value.PhoneNumber, item.Value.Email, item.Value.TimeStamp, item.Value.SampleType));
		//    }
		//    return noIds;
		//}


		// Tilføjer IOrder til Dictionary
		public void AddOrder(IOrder o)
        {
			orders.Add(o.OrderId, o);
        }
		// Overload af "AddOrder" her indsættes key value i metoden, og kræves som parameter.
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
