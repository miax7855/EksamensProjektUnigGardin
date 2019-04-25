using System;
using Domainlayer;

namespace ApplicationLayer
{
    public class Controller
    {
        private OrderRepository oRepo = new OrderRepository();
        private DBController dbController = new DBController();
        private FabricSampleRepository fsRepo = new FabricSampleRepository();
        private Errors error = new Errors();
        

        public void ExportOrder(Order order)
        {
            try
            {
                dbController.SaveOrder(order);
            }
            catch (Exception)
            {
                throw ;
            }
        }


    }
}
