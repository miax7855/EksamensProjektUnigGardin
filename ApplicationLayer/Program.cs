using Domainlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using library;

namespace ApplicationLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Controller con = new Controller();
            ImportController ic = new ImportController();
            Program programme = new Program();
            ic.OrderRegistered += programme.OnOrderRegistered;
            //con.ImportOrder(@"C:\Users\Mia\source\repos\EksamensProjektUnigGardin\Domainlayer\Content\Orders.txt");
            //con.RefreshOrders(@"C:\Users\Mia\source\repos\EksamensProjektUnigGardin\Domainlayer\Content\Orders.txt");
            Dictionary<int, IOrder> orders = con.ShowAllOrders();

            foreach (Order o in orders.Values)
            {
                //Console.WriteLine("an order");
                Console.WriteLine(o.PrintOrderInfo(o));
            }
            Console.ReadKey();
            foreach (Order o in orders.Values)
            {
                //Console.WriteLine("an order");
                Console.WriteLine(o.PrintOrderInfo(o));
            }
        }
        public void OnOrderRegistered(object source, EventArgs e)
        {
            Console.WriteLine("order registered");
        }
    }
}
