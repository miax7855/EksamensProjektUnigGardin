using ApplicationLayer;
using library;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows;

namespace EksamensProjektUnigGardin
{
    /// <summary>
    /// Interaction logic for ShowCurrentOrders.xaml
    /// </summary>
	/// 

    // Arver fra Page, og implementerer 2 interfaces. 
    public partial class ShowCurrentOrders : Page, IOnStockUpdatedSubscriber, ISubscribersOrderRegistered
    {
        Controller controller = new Controller();
        private List<IOrder> ordersAsList = new List<IOrder>();
		// en collection der opdaterer alt der benytter kollektionen (ved ændring) fx listbox eller lign.
        ObservableCollection<IOrder> ObsCollForListView = new ObservableCollection<IOrder>();

		// 
        public ShowCurrentOrders()
        {
            InitializeComponent();

			controller.StockUpdated += OnStockUpdated;
            if (controller.ReturnOrderRepository().GetOrderDic().Count == 0)
            {
                controller.ConGetOrdersFromDataBase();
            }
            controller.SubscribersOrderRegistered(this);
            controller.ImportOrder("Orders.txt");
			OrderPackagedButton.IsEnabled = false;
            
        }
		// sætter Ordrer i listBox
        private void ShowOrderIDsInListBox()
        {
            listBox.Dispatcher.Invoke(() =>
            {
                listBox.Items.Clear();
                foreach (IOrder item in this.ordersAsList)
                {
                    listBox.Items.Add(item.OrderId);
                }
            });
        }
		// sætter source for "SelectedOders"
        public void ShowSamplesInListBox()
        {
            SelectedOrders.ItemsSource = ordersAsList;
        }
		// Starter ved Ændring, valgte emne bliver sat til item
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int OrderIdSelected = 0;
			// INT
            foreach (System.Int32 item in e.AddedItems)
            {
                OrderIdSelected = item;
            }
            // lambda funktionen fungerer som et For each her, hvor den søger efter x.OrderID som er det samme som parameteren
			// "OrderIdSelected" som er = interger værdien item.
            IOrder result = ordersAsList.Find(x => x.OrderId == OrderIdSelected);
			// Hvis Observable collection allerede indeholder resultatet, bliver den ikke tilføjet igen.
			if (!ObsCollForListView.Contains(result))
            {
				// burde være i intialize eller et sted hvor den kører en gang
				SelectedOrders.ItemsSource = null;
				SelectedOrders.Items.Clear();
				// ItemSource bliver sat til ObservableCollection
				SelectedOrders.ItemsSource = ObsCollForListView;
				// opdateres automatisk pga. source nu er en obersable collection
                ObsCollForListView.Add(result);
            }
        }
		// EventArgs castes til OrderRepository, henter data fra OrderRepository og tilføjer det til klassens liste
		// "this.ordersAsList"

		// Vi har en interface i vores library for en subscriber denne subscriber har parameteren "eventArgs"
		// Grunden til OrderRepository ikke kan indsættes som eventArgs er fordi library ikke har reference til programmet.
		// Derfor bruges der casting.
        public void OnOrderRegistered(object source, EventArgs e)
        {
            this.ordersAsList.Clear();
            OrderRepository o = (OrderRepository)e;
            this.ordersAsList.AddRange(o.ReturnOrdersAsList());
			// nedenstående metode bliver kaldt med opdaterede liste.
            ShowOrderIDsInListBox();
        }

		// Hver gang der trykkes på noget i listview "SelectedOrders" skal dens tilhørende sampletypes sættes ind i
		// SamplesListBox
		private void SelectedOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			// Det element der er valgt, skal laves om til et "IOrder Objekt" Dette udføres vha. "selectedValue"
			// SelectedValue kendes pga. e er med som parameter.
			IOrder iOrder = (IOrder)SelectedOrders.SelectedValue;
            
            if (iOrder != null)
            {
				// cleares og source sættes på ny pga. det ikke er en observable collection
                SamplesListBox.ItemsSource = null;
                SamplesListBox.Items.Clear();
                SamplesListBox.ItemsSource = iOrder.SampleType;
				OrderPackagedButton.IsEnabled = true;
			}
        }
        // Håndterer alt der forekommer ved godkendelse af pakning
        private void OrderPackagedButton_Click(object sender, RoutedEventArgs e)
        {
			// returnerer en bool
			bool confirmation = ShowPopUpBox();
			// if true
			if (confirmation)
			{
				// hvis "SelectedOrders" har noget i sig
				if (SelectedOrders.HasItems)
				{
					// finder ordren der er blevet valgt 
					IOrder orderToRemove = ordersAsList.Find((x) => SelectedOrders.SelectedValue.Equals(x));
					// fjernes fra diverse collections
					ObsCollForListView.Remove(orderToRemove);

					controller.ReturnOrderRepository().RemoveOrder(orderToRemove);
					// ordersAsList sættes til listen fra OrderRepo
                    ordersAsList = controller.ReturnOrderRepository().ReturnOrdersAsList();

					// Den kalder OrderPacked, som kaldet et event 
                    controller.OrderPacked(this, orderToRemove);
					// Skulle have været subscribed til eventet
					controller.DeleteOrderFromDatabase(orderToRemove);

					listBox.Items.Clear();
                    ShowOrderIDsInListBox();
                }
			}

			OrderPackagedButton.IsEnabled = false;

		}

		//Viser en popUpBox med 2 forskellige svar muligheder.
		private bool ShowPopUpBox()
		{
			bool confirmation = false;
			MessageBoxResult result = MessageBox.Show("Er du sikker at pakken er færdig?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Information);
			switch (result)
			{
				case MessageBoxResult.Yes:
					confirmation = true;
					break;
					
				case MessageBoxResult.No:
					MessageBox.Show("Kann så ikke bearbejde pakken...", "Confirmation");
					confirmation = false;
					break;
			}

			return confirmation;
		}

		// Henter MainWindow og dens indhold sættes til at være "ManageStock"
		private void GoToManageStock(object sender, RoutedEventArgs e)
		{
			Application.Current.MainWindow.Content = new ManageStock();
		}
		// et SUbscriberEvent
		public void OnStockUpdated(object sender, EventArgs e)
		{
			// EventArgs e bliver castet til FabricSamplyRepository
			FabricSampleRepository e2 = (FabricSampleRepository)e;

				string sampleTypesWithLowStock = string.Empty;
			// Alle lowStockSampleTypes ProductName bliver sat til string SamplyTypesWithLowStock
				foreach (IFabricSample fabricSample in e2.ReturnLowStockSamples())
				{
					sampleTypesWithLowStock += fabricSample.ProductName;
				}

				MessageBoxResult result = MessageBox.Show("Der er Stofprøver under en kritisk mængde", "Hovsa", MessageBoxButton.OK, MessageBoxImage.Warning);
				switch (result)
				{
					case MessageBoxResult.OK:
						MessageBox.Show(sampleTypesWithLowStock);
						break;
				}
		}

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void SamplesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
