using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domainlayer
{
    public class FabricSample
    {
        public string FabricSampleNumber { get; set; }
        public int Quantity { get; set; }
		public string ProductName { get; set; }

		public FabricSample(string fabricSampleNumber, int quantity)
		{
			FabricSampleNumber = fabricSampleNumber;
			Quantity = quantity;
		}
	}
}
