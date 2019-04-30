using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApplicationLayer;
using System.Collections.Generic;
using Domainlayer;

namespace ImportControllerUnitTest
{
	[TestClass]
	public class ReadTextfileTest
	{
		[TestInitialize]
		public void Initialize()
		{
			

		}


		[TestMethod]
		public void TestProperReadingOfSampleTypes()
		{
			ImportController ic = new ImportController();
			Controller c = new Controller();
			OrderRepository or = OrderRepository.GetOrderRepo();
			//Dictionary<int, Order> orderTests = new Dictionary<int, Order>();

			List<string> testSampleType = new List<string> { "1", "2", "3" };

			Order o = new Order(1, "Julian", "Petersen", 52464, "schleswig", "deutschland", 123456789, "julian @gmail.com", testSampleType);

			//orderTests.Add(1, o);


			c.RefreshOrders();
			Order o2 = or.GetOrderDic()[1];

			
			//Dictionary<int, Order> testOrders = or.GetOrderDic();



			Assert.AreEqual(o.SampleType.ToString(), o2.SampleType.ToString());
		}
	}
}
