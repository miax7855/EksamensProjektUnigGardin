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
        private List<Order> orders = new List<Order>();

        public void AddOrder(Order o)
        {
            orders.Add(o);
        }
        public void RemoveOrder(Order o)
        {
            orders.Remove(o);
        }
        
    }
    
}
