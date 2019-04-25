using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensProjektUnigGardin
{
    public class OrderRepository
    {
        public List<Order> orders = new List<Order>();

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
