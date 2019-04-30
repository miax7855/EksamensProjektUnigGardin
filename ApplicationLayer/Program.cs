using Domainlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer
{
    class Program
    {
        static void Main(string[] args)
        {
			Controller con = new Controller();

			con.RefreshOrders();
			Dictionary<int, Order> orders = con.ShowAllOrders();

			foreach(Order o in orders.Values)
			{
				Console.WriteLine(o);
			}
			Console.Read();
		}
    }
}
