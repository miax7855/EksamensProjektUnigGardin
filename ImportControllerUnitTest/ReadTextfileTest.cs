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
			OrderRepository o = new OrderRepository();

			List<string> testList = new List<string>();

			testList.Add("1");
			testList.Add("2");
			testList.Add("3");

			c.RefreshOrders();
			IDictionary<int, Order> testOrders =  o.GetOrders();
			

			Assert.AreEqual(testList, testOrders.Values);
		}
	}
}
