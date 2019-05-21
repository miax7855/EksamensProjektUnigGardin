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
using library;


namespace EksamensProjektUnigGardin
{
	/// <summary>
	/// Interaction logic for ManageStock.xaml
	/// </summary>
	public partial class ManageStock : Page
	{
		Controller con = new Controller();
		FabricSampleRepository fRepo;
		List<IFabricSample> fabricSamples = new List<IFabricSample>();


		public ManageStock()
		{
			fRepo = con.ReturnFabricSampleRepository();
			InitializeComponent();
			fRepo.AddTestSamples();
			ShowSamplesAllStock();
			ShowLowStackSamples();
			AlertOnLowStock();
		}
		private void ShowSamplesAllStock()
		{

			foreach (IFabricSample item in fRepo.ReturnStock())
			{
				AllStock.Items.Add(item);
			}
		}
		private void ShowLowStackSamples()
		{
			foreach (IFabricSample item in fRepo.ReturnLowStockSamples())
			{
				LowStock.Items.Add(item);
			}
		}
		public void OnOrderRegistered(object source, FabricSampleRepository e)
		{
			this.fabricSamples.Clear();
			this.fabricSamples = e.ReturnLowStockSamples();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.MainWindow.Content = new ShowCurrentOrders();
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
		//	Application.Current.MainWindow.Content = new ManageStock();
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{

		}

		private void Button_Click_3(object sender, RoutedEventArgs e)
		{

		}
		private void AlertOnLowStock()
		{
			if (fRepo.ReturnLowStockSamples() != null)
			{
				Alert AlertWindow = new Alert();
				AlertWindow.Show();
			}
		}
	}
}
