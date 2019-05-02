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
        List<IOrder> orders = new List<IOrder>();

        public ShowCurrentOrders()
        {
            InitializeComponent();
            ShowSamplesInListBox();
        }

        public void ShowOrdersInListView()
        {
            orders = controller.ReturnRepoList();
            SelectedOrders.ItemsSource = orders;
        }
        
        private void ShowSamplesInListBox()
        {
            orders = controller.ReturnRepoList();
            foreach (IOrder item in orders)
            {
                lstBox.Items.Add(item.OrderId);
            }
        }
        List<IOrder> newList = new List<IOrder>();

        private void LstBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int tal = 0;
            foreach (System.Int32 item in e.AddedItems)
            {
                tal = item;
            }
            //string test = this.lstBox.SelectedItem.ToString();
            //SelectedOrders.ItemsSource =
            //string IDSelected = e.AddedItems.ToString();
            
            IOrder result = orders.Find(x => x.OrderId == tal);
            newList.Add(result);
            SelectedOrders.Items.Clear();
            SelectedOrders.ItemsSource = newList;

            //orders.Find(x => x.OrderId == e.AddedItems);
            //SelectedOrders.ItemsSource = e.AddedItems;
            //Type t = Type.GetType("IOrder");
            //PropertyInfo p = 
        }
    }
}
