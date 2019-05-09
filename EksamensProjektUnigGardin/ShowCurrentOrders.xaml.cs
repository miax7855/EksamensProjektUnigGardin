using ApplicationLayer;
using library;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace EksamensProjektUnigGardin
{
    /// <summary>
    /// Interaction logic for ShowCurrentOrders.xaml
    /// </summary>
    
    public partial class ShowCurrentOrders : Page
    {
        Controller controller = new Controller();
        ImportController iController = new ImportController();
        private List<IOrder> ordersAsList = new List<IOrder>();
        private List<IOrder> ListOfCurrentListViewItems = new List<IOrder>();
        OrderRepository orderRepo = OrderRepository.GetOrderRepo();

        ObservableCollection<IOrder> ObsCollForListView = new ObservableCollection<IOrder>();
        //ObservableCollection<int> ObsCollForOrderIDs = new ObservableCollection<int>();

        //ObservableCollection<int> ObsCollForListBox = new ObservableCollection<int>();

        public ShowCurrentOrders()
        {
            InitializeComponent();
            iController.OrderRegistered += OnOrderRegistered;
            controller.ImportOrder("Orders.txt", iController);
        }
        
        public void ShowOrderIDsInListBox()
        {
            this.Dispatcher.Invoke(() =>
            {
                foreach (IOrder item in ordersAsList)
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
                listBox.Items.RemoveAt(listBox.Items.IndexOf(iOrder.OrderId));
                //SamplesListBox.ItemsSource = null;
                //SamplesListBox.Items.Clear();
                //SamplesListBox.ItemsSource = iOrder.SampleType;
            }
        }

        private void SamplesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OrderPackagedButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (SelectedOrders.HasItems)
            {
                IOrder orderToRemove = ordersAsList.Find((x) => SelectedOrders.SelectedValue.Equals(x));

                orderRepo.RemoveOrder(orderToRemove);
                ordersAsList = orderRepo.ReturnOrdersAsList();

                ObsCollForListView.Remove(orderToRemove);
  
                //listBox.Items.Clear();
                ShowOrderIDsInListBox();
                
            }

        }
        
    }
}
