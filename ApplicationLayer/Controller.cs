using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domainlayer;
using library;

namespace ApplicationLayer
{
	// overflødig, delegate, som tager en FabricSampleRepo-type som input i EventArgs, og har 2 parametre
	public delegate void UpdateStockEventHandler<FabricSampleRepository>(object sender, EventArgs e);

    public class Controller
    {
		// creater en eventhandler som modtager en FabricSampleRepository objekt
		public event EventHandler<FabricSampleRepository> StockUpdated;
        private OrderRepository oRepo;
        private DBController dbController = new DBController();
		private ImportController iController = new ImportController();
        private ErrorController error = new ErrorController();
		// Henter singleton
		private FabricSampleRepository fRepo = FabricSampleRepository.GetFabricSampleRepo();
		private bool programRunning = true;
		private object fileNameObj = "Orders.txt";
		// bool ran sættes kun til true ved metoden "GetLowStockSamples" og fandt LowStockSamples. 
		private bool ran = false;
        
		// Har ISubscriber som input, importcontrollerens event attacher parameterens metode, som kaldes af eventhandleren
		// burde ske i ImportController
        public void SubscribersOrderRegistered(ISubscribersOrderRegistered subscriber)
        {
            iController.OrderRegistered += subscriber.OnOrderRegistered;
            iController.OrderRegistered += dbController.OnOrderRegistered;
        }
		// Tager en string som parameter, som benyttes til kald af RefreshOrders
        public void ImportOrder(string fileName)
        {
            fileNameObj = fileName;
            RefreshOrders(fileName);
        }
        // Tager en string som parameter, laver en thread der får en reference af "RegisterOrders" fra ImportController
        public void RefreshOrders(string fileName)
        {
            fileNameObj = fileName;

            Thread thread = new Thread(iController.RegisterOrders);
            thread.IsBackground = true;
            thread.Start(fileNameObj);
        }
		// Returnerer en kollektion af typen dictionary (int, Iorder) ((( FORKERT NAVN )))) BAD BAD BAD
        public Dictionary<int, IOrder> ShowAllOrders()
		{
			oRepo = OrderRepository.GetOrderRepo();
			return oRepo.GetOrderDic();
		}
		// Metode der køres ved bekræftelse af ordre (pakket) input en Subscriber af typen "IOnStockUpdatedSubscriber"
		// og en "IOrder" som den skal fjerne.
		public void OrderPacked(IOnStockUpdatedSubscriber subscriber, IOrder orderToRemove)
		{
			// Subscriberen attaches til "StockUpdated" som er en eventhandler
			StockUpdated += subscriber.OnStockUpdated;
			// Metoden "UpdateStock" kaldes som opdaterer lageret på databasen.
			dbController.UpdateStock(orderToRemove, error);
			// Der kaldes metoden "GetLowStockSampleTypes" metoden returnerer en bool som benyttes iforhold til If statement
			ran = dbController.GetLowStockSampleTypes(ran, fRepo, error);
			// hvis ran er true kaldes "StockUpdated" Eventhandler
			if (ran)
			{
				// 2 parametre, this som er "objectSender" + en FabricSampleRepository som kan bruges af metoderne der ligger
				// som er attached til eventhandleren
				StockUpdated(this, fRepo);
			}
			ran = false;
			fRepo.CLearSampleTypeList();
		}
		// Deleter en ordre fra databasen ved kald af metoden "FinishedOrders i DbController.
		public void DeleteOrderFromDatabase(IOrder orderToRemove)
		{
			dbController.FinishedOrder(orderToRemove, error);
		}
		// Kalder metoden "GetOrdersFromDatabase via DbController
		public void ConGetOrdersFromDataBase()
		{
			dbController.GetOrdersFromDatabase(oRepo, error);
		}
		// returnerer en OrderRepository instansen
		public OrderRepository ReturnOrderRepository()
		{
			oRepo = OrderRepository.GetOrderRepo();
			return oRepo;
		}
		// returnerer en FabricSampleRepository instansen
		public FabricSampleRepository ReturnFabricSampleRepository()
		{
			fRepo = FabricSampleRepository.GetFabricSampleRepo();
			return fRepo;
		}
	}
}
