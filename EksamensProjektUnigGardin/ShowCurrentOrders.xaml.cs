using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using ApplicationLayer;
using library;

namespace EksamensProjektUnigGardin
{
    /// <summary>
    /// Interaction logic for ShowCurrentOrders.xaml
    /// </summary>
    public partial class ShowCurrentOrders : Page
    {
        Controller controller = new Controller();
        OrderRepository oRepo;
        List<IOrder> orders = new List<IOrder>();
        List<IOrder> listOfCurrentListViewItems = new List<IOrder>();

        public ShowCurrentOrders()
        {
            InitializeComponent();
            orders = AddFakes();
            ShowSamplesInListBox();
        }

        public void ShowOrdersInListView()
        {
            SelectedOrders.ItemsSource = orders;
        }
        
        private void ShowSamplesInListBox()
        {
            foreach (IOrder item in orders)
            {
                lstBox.Items.Add(item.OrderId);
            }
        }

        private void LstBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            int tal = 0;
            foreach (System.Int32 item in e.AddedItems)
            {
                tal = item;
            }
           
            IOrder result = orders.Find(x => x.OrderId == tal);
            listOfCurrentListViewItems.Add(result);
            SelectedOrders.Items.Clear();
            //SelectedOrders.ItemsSource = listOfCurrentListViewItems;
            
        }
        public List<IOrder> AddFakes()
        {
            oRepo = OrderRepository.GetOrderRepo();
            controller.ImportOrder("Orders.txt");
            
            return oRepo.ReturnOrdersAsList();
        }
    }
}
