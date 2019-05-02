using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domainlayer;
using library;

namespace ApplicationLayer
{
    public class OrderRepository
    {
		private Dictionary<int, IOrder> orders = new Dictionary<int, IOrder>();


		public void AddOrder(Order o)
        {
			orders.Add(o.OrderId, o);
        }
        public void RemoveOrder(Order o)
        {
			orders.Remove(o.OrderId);
        }
		public IDictionary<int, IOrder> GetOrders()
		{
			return orders;
		}
        public Dictionary<int, IOrder> ReturnOrders()
        {
            FakeOrders();
            return orders;
        }
        List<string> sampletypeTest = new List<string>();
        public void FakeOrders()
        {
            sampletypeTest.Add("1");
            sampletypeTest.Add("2");
            sampletypeTest.Add("3");
            sampletypeTest.Add("4");
            sampletypeTest.Add("5");

            orders.Add(1, new Order(1, "tom", "last", 123, "dk", "dk", 123, "kns", sampletypeTest));
            orders.Add(2, new Order(2, "tom", "last", 4773, "dk", "dk", 65, "kns", sampletypeTest));
            orders.Add(3, new Order(3, "tom", "last", 43, "dk", "dk", 65, "kns", sampletypeTest));
            orders.Add(4, new Order(4, "tom", "last", 65, "dk", "dk", 65, "kns", sampletypeTest));

        }
    }
}
