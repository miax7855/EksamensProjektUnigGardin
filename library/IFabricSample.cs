using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
	public interface IFabricSample
	{
		string FabricSampleNumber { get; set; }
		int Quantity { get; set; }
		string ProductName { get; set; }
	}
}
