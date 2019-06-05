using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ApplicationLayer;
using library;

namespace EksamensProjektUnigGardin
{
	/// <summary>
	/// Interaction logic for Alert.xaml
	/// </summary>
	/// 

	// arver fra Window
	public partial class Alert : Window
	{
		
		FabricSampleRepository Frepo;
		// ved start af dette window kaldes understående metoder.
		public Alert()
		{
			InitializeComponent();
			LowStockMessage();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{

			this.Close();
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
		// returnerer en string med besked om SampleTypes med lav beholdning
		private void LowStockMessage()
		{
			Frepo = FabricSampleRepository.GetFabricSampleRepo();
			string ErrorMessage = "";
			List<IFabricSample> LowStock = new List<IFabricSample>();
			LowStock = Frepo.ReturnLowStockSamples();
			foreach (IFabricSample item in LowStock)
			{
				ErrorMessage += item.FabricSampleNumber + " beholdning: " + item.Quantity + "\n";
			}
			LowStockMessages.Text = ErrorMessage;
		}
	}
}
