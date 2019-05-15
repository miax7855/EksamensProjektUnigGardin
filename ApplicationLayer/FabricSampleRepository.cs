using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domainlayer;
using library;

namespace ApplicationLayer
{
    public class FabricSampleRepository
    {
        private List<IFabricSample> fabricSamples = new List<IFabricSample>();

        public void AddFabricSample(IFabricSample sample)
        {
            fabricSamples.Add(sample);
        }

        public void RemoveFabricSample(IFabricSample sample)
        {
            fabricSamples.Remove(sample);
        }
		public List<IFabricSample> ReturnLowStockSamples()
		{
			List<IFabricSample> LowStockList = new List<IFabricSample>();
			foreach (IFabricSample item in fabricSamples)
			{
				if (item.Quantity <= 10)
				{
					LowStockList.Add(item);
				}
			}
			return LowStockList;
		}
		public List<IFabricSample> ReturnStock()
		{
			List<IFabricSample> Stock = new List<IFabricSample>();
			foreach (IFabricSample item in fabricSamples)
			{
				Stock.Add(item);
			}
			return Stock;
		}

		public void AddTestSamples()
		{
			FabricSample Test1 = new FabricSample("U5500", 8, "Grøn Persienne");
			FabricSample Test2 = new FabricSample("U7000", 12, "Sort Persienne");
			FabricSample Test3 = new FabricSample("A2300", 55, "Hvid slør");
			FabricSample Test4 = new FabricSample("Y6700", 67, "GitGud Gardin");
			FabricSample Test5 = new FabricSample("U4000", 2, "Sort veil");
			FabricSample Test6 = new FabricSample("U2100", 34, "Rød Julian");
			FabricSample Test7 = new FabricSample("Y8900", 112, "halv Grøn Persienne");

			fabricSamples.Add((IFabricSample)Test1);
			fabricSamples.Add((IFabricSample)Test2);
			fabricSamples.Add((IFabricSample)Test3);
			fabricSamples.Add((IFabricSample)Test4);
			fabricSamples.Add((IFabricSample)Test5);
			fabricSamples.Add((IFabricSample)Test6);
			fabricSamples.Add((IFabricSample)Test7);

		}
	}

}
