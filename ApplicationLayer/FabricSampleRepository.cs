using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domainlayer;

namespace ApplicationLayer
{
    public class FabricSampleRepository
    {
        private List<FabricSample> fabricSamples = new List<FabricSample>();

        public void AddFabricSample(FabricSample sample)
        {
            fabricSamples.Add(sample);
        }

        public void RemoveFabricSample(FabricSample sample)
        {
            fabricSamples.Remove(sample);
        }
    }
}
