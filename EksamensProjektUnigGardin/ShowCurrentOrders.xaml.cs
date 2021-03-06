﻿using ApplicationLayer;
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
    
    public partial class ShowCurrentOrders : Page, IOnStockUpdatedSubscriber, ISubscribersOrderRegistered
    {
        Controller controller = new Controller();
        private List<IOrder> ordersAsList = new List<IOrder>();
        ObservableCollection<IOrder> ObsCollForListView = new ObservableCollection<IOrder>();

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
        public void ShowSamplesInListBox()
        {
            SelectedOrders.ItemsSource = ordersAsList;
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int OrderIdSelected = 0;
            foreach (System.Int32 item in e.AddedItems)
            {
                OrderIdSelected = item;
            }
            
            IOrder result = ordersAsList.Find(x => x.OrderId == OrderIdSelected);
            if (!ObsCollForListView.Contains(result))
            {
                SelectedOrders.ItemsSource = null;
                SelectedOrders.Items.Clear();
                SelectedOrders.ItemsSource = ObsCollForListView;

                ObsCollForListView.Add(result);
            }
        }

        public void OnOrderRegistered(object source, EventArgs e)
        {
            this.ordersAsList.Clear();
            OrderRepository o = (OrderRepository)e;
            this.ordersAsList.AddRange(o.ReturnOrdersAsList());
            ShowOrderIDsInListBox();
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
        
        private void OrderPackagedButton_Click(object sender, RoutedEventArgs e)
        {

			bool confirmation = ShowPopUpBox();
			if (confirmation)
			{
				if (SelectedOrders.HasItems)
				{
					IOrder orderToRemove = ordersAsList.Find((x) => SelectedOrders.SelectedValue.Equals(x));
                    ObsCollForListView.Remove(orderToRemove);

					controller.ReturnOrderRepository().RemoveOrder(orderToRemove);

                    ordersAsList = controller.ReturnOrderRepository().ReturnOrdersAsList();

                    controller.OrderPacked(this, orderToRemove);
					controller.DeleteOrderFromDatabase(orderToRemove);

					listBox.Items.Clear();
                    ShowOrderIDsInListBox();
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
        
		private void GoToManageStock(object sender, RoutedEventArgs e)
		{
			Application.Current.MainWindow.Content = new ManageStock();
		}

		public void OnStockUpdated(object sender, EventArgs e)
		{
			FabricSampleRepository e2 = (FabricSampleRepository)e;

				string sampleTypesWithLowStock = string.Empty;

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
