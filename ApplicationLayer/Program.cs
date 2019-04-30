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
			Console.WriteLine(con.ShowAllOrders()); 

		}
    }
}
