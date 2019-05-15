using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using library;

namespace Domainlayer
{
    public class FabricSample: IFabricSample
    {
        public string FabricSampleNumber { get; set; }
        public int Quantity { get; set; }
		public string ProductName { get; set; }

		public FabricSample(string fabricSampleNumber, int quantity, string productName)
		{
			FabricSampleNumber = fabricSampleNumber;
			Quantity = quantity;
			ProductName = productName;
		}
	}
}
