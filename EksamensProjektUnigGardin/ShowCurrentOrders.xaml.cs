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
    
    public partial class ShowCurrentOrders : Page, IOnStockUpdatedSubscriber
    {
        Controller controller = new Controller();
        ImportController iController = new ImportController();
        private List<IOrder> ordersAsList = new List<IOrder>();
        private List<IOrder> ListOfCurrentListViewItems = new List<IOrder>();
        OrderRepository orderRepo = OrderRepository.GetOrderRepo();
        DBController databaseController = new DBController();

        ObservableCollection<IOrder> ObsCollForListView = new ObservableCollection<IOrder>();
        //ObservableCollection<int> ObsCollForOrderIDs = new ObservableCollection<int>();

        //ObservableCollection<int> ObsCollForListBox = new ObservableCollection<int>();

        public ShowCurrentOrders()
        {
            InitializeComponent();
            iController.OrderRegistered += OnOrderRegistered;
			// iController.OrderRegistered += databaseController.OnOrderRegistered;
			controller.StockUpdated += OnStockUpdated;
            controller.ImportOrder("Orders.txt", iController);

			OrderPackagedButton.IsEnabled = false;
            ShowOrderIDsInListBox();

        }

        private void ShowOrderIDsInListBox()
        {
           Dispatcher.Invoke(() => {
                listBox.Items.Clear();

                foreach ( IOrder item in this.ordersAsList)
                {
                    listBox.Items.Add(item.OrderId);
                }

           });
        }
        public void ShowSamplesInListBox()
        {
            SelectedOrders.ItemsSource = ordersAsList;
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int tal = 0;
            foreach (System.Int32 item in e.AddedItems)
            {
                tal = item;
            }

            IOrder result = ordersAsList.Find(x => x.OrderId == tal);

            SelectedOrders.ItemsSource = null;
            SelectedOrders.Items.Clear();

            SelectedOrders.ItemsSource = ObsCollForListView;
            ObsCollForListView.Add(result);

            //SelectedOrders.Items.Clear();
            //SelectedOrders.ItemsSource = ListOfCurrentListViewItems;
        }

        public void OnOrderRegistered(object source, OrderRepository e)
        {
            this.ordersAsList.Clear();
            this.ordersAsList = e.ReturnOrdersAsList();
            ShowOrderIDsInListBox();
        }

        private void InsertOrderButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (InsertOrderTextBox.Text != null)
            {
                controller.ImportOrder(iController.GetFilePath("Orders.txt"), iController, InsertOrderTextBox.Text);
            }
        }

        private void SelectedOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IOrder iOrder = (IOrder)SelectedOrders.SelectedValue;
            
            if (iOrder != null)
            {
                SamplesListBox.ItemsSource = null;
                SamplesListBox.Items.Clear();
                SamplesListBox.ItemsSource = iOrder.SampleType;
				OrderPackagedButton.IsEnabled = true;
			}
        }

        private void SamplesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OrderPackagedButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
			bool confirmation = ShowPopUpBox();
			if (confirmation)
			{
				if (SelectedOrders.HasItems)
				{

					IOrder orderToRemove = ordersAsList.Find((x) => SelectedOrders.SelectedValue.Equals(x));
					ListOfCurrentListViewItems.Remove(orderToRemove);

					orderRepo.RemoveOrder(orderToRemove);
					
					ordersAsList = orderRepo.ReturnOrdersAsList();

					controller.OrderPacked(this, orderToRemove);
					controller.DeleteOrderFromDatabase(orderToRemove);

					SelectedOrders.ItemsSource = null;
					SelectedOrders.Items.Clear();
					//SelectedOrders.Items.Remove(orderToRemove);
					SelectedOrders.ItemsSource = ListOfCurrentListViewItems;

					listBox.Items.Clear();
					ShowOrderIDsInListBox();


					//SelectedOrders.Items.Remove(SelectedOrders.FindResource(orderToRemove));
					//SelectedOrders.Items.Clear();
					//SelectedOrders.ItemsSource = ordersAsList;
					//SelectedOrders.SelectedValue
				}
			}

			OrderPackagedButton.IsEnabled = false;
			
		}

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

		private void Button_Click(object sender, RoutedEventArgs e)
		{
		//	Application.Current.MainWindow.Content = new ShowCurrentOrders();
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			Application.Current.MainWindow.Content = new ManageStock();
		}

		public void OnStockUpdated(object sender, EventArgs e)
		{
			FabricSampleRepository e2 = (FabricSampleRepository)e;
			MessageBoxResult result = MessageBox.Show("Der er Stofprøver under en kritisk mængde", "Hovsa", MessageBoxButton.OK, MessageBoxImage.Warning);
			switch (result)
			{
				case MessageBoxResult.OK:
					MessageBox.Show(e2.ReturnLowStockSamples().ToString());
					break;
			}
		}
	}
}
