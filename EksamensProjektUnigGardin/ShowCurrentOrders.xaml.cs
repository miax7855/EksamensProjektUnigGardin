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
using ApplicationLayer;

namespace EksamensProjektUnigGardin
{
    /// <summary>
    /// Interaction logic for ShowCurrentOrders.xaml
    /// </summary>
    public partial class ShowCurrentOrders : Page
    {
        Controller controller = new Controller();
        public ShowCurrentOrders()
        {
            InitializeComponent();
            ShowOrders();
            
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ShowOrders()
        {
            lstOrders.ItemsSource 
        }
    }
}
