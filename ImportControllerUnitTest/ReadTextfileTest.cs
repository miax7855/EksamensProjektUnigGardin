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
			Controller c = new Controller();
			OrderRepository or = OrderRepository.GetOrderRepo();

			List<string> testSampleType = new List<string> { "1", "2", "3" };

			Order o = new Order(1, "Julian", "Petersen", 52464, "schleswig", "deutschland", 123456789, "julian @gmail.com", testSampleType);

			c.RefreshOrders();
			Order o2 = or.GetOrderDic()[1];

			Assert.AreEqual(o.SampleType.ToString(), o2.SampleType.ToString());
		}

		[TestMethod]
		public void TestProperReadingOfOrder()
		{
			Controller c = new Controller();
			OrderRepository or = OrderRepository.GetOrderRepo();

			List<string> testSampleType = new List<string> { "U6542", "U7854" };

			Order o = new Order(3, "Assborn", "Larsen", 2464, "Bahnhof", "Danmark", 5648792, "Born @Ass.com", testSampleType);

			c.RefreshOrders();
			Order o2 = or.GetOrderDic()[3];

			Assert.AreEqual(o.PrintOrderInfo(o), o2.PrintOrderInfo(o2));
		}

		[TestMethod]
		public void TestProperReadingOfOrder2()
		{
			Controller c = new Controller();
			OrderRepository or = OrderRepository.GetOrderRepo();

			//dengang med et forkert testSampleType liste
			List<string> testSampleType = new List<string> { "U6542"};

			Order o = new Order(3, "Assborn", "Larsen", 2464, "Bahnhof", "Danmark", 5648792, "Born @Ass.com", testSampleType);

			c.RefreshOrders();
			Order o2 = or.GetOrderDic()[3];

			Assert.AreNotEqual(o.PrintOrderInfo(o), o2.PrintOrderInfo(o2));
		}
	}
}
