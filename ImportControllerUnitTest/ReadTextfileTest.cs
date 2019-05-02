using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApplicationLayer;
using System.Collections.Generic;
using Domainlayer;
using System.IO;
using System.Threading;
using System.Linq;

namespace ImportControllerUnitTest
{
	[TestClass]
	public class ReadTextfileTest
	{
		[TestInitialize]
		public void Initialize()
		{
			

		}
		int TempInteger;

		[TestMethod]
		public void TestProperReadingOfSampleTypes()
		{
			ImportController ic = new ImportController();
			string fileName = "Orders.txt";
			string relativePath = ic.GetFilePath(fileName);
			Controller c = new Controller();
			OrderRepository or = OrderRepository.GetOrderRepo();

			List<string> testSampleType = new List<string> { "1", "2", "3" };

			Order o = new Order(1, "Julian", "Petersen", 52464, "schleswig", "deutschland", 123456789, "julian @gmail.com", testSampleType);

			c.RefreshOrders(fileName);
			Order o2 = or.GetOrderDic()[1];

			Assert.AreEqual(o.SampleType.ToString(), o2.SampleType.ToString());
		}

		[TestMethod]
		public void TestProperReadingOfOrder()
		{
			ImportController ic = new ImportController();
			Controller c = new Controller();
			string fileName = "Orders.txt";
			string relativePath = ic.GetFilePath(fileName);
			OrderRepository or = OrderRepository.GetOrderRepo();

			List<string> testSampleType = new List<string> { "U6542", "U7854" };

			Order o = new Order(3, "Assborn", "Larsen", 2464, "Bahnhof", "Danmark", 5648792, "Born @Ass.com", testSampleType);

			c.RefreshOrders(fileName);
			Order o2 = or.GetOrderDic()[3];

			Assert.AreEqual(o.PrintOrderInfo(o), o2.PrintOrderInfo(o2));
		}

		[TestMethod]
		public void TestProperReadingOfOrder2()
		{
			ImportController ic = new ImportController();
			Controller c = new Controller();
			string fileName = "Orders.txt";
			string relativePath = ic.GetFilePath(fileName);
			OrderRepository or = OrderRepository.GetOrderRepo();

			//dengang med et forkert testSampleType liste
			List<string> testSampleType = new List<string> { "U6542"};

			Order o = new Order(3, "Assborn", "Larsen", 2464, "Bahnhof", "Danmark", 5648792, "Born @Ass.com", testSampleType);

			c.RefreshOrders(fileName);
			Order o2 = or.GetOrderDic()[3];

			Assert.AreNotEqual(o.PrintOrderInfo(o), o2.PrintOrderInfo(o2));
		}
		[TestMethod]
		public void TestRefreshOrders()
		{
			ImportController ic = new ImportController();
			Controller c = new Controller();
			string fileName = "Orders.txt";
			string relativePath = ic.GetFilePath(fileName);
			c.RefreshOrders(fileName);
			OrderRepository or = OrderRepository.GetOrderRepo();
			using (StreamWriter Writer = new StreamWriter(relativePath, true))
			{
				Writer.WriteLine("1;Julian;Petersen;52464;Slesvig;deutschland;123456789;julian@gmail.com;1,2,3", true);
				Writer.WriteLine("2;Mia;Pars;56998;Odense;Danmark;98765432;mia.pars@camgirl.com;U4000,A6666,K6666,U4001", true);
				Writer.WriteLine("3;Assborn;Larsen;2464;Bahnhof;Danmark;5648792;Born@Ass.com;U6542,U7854", true);
				Writer.WriteLine("4;Anders;Weiskvist;5000;Bellinge;Danmark;6543214;An@ders.com;U5426", true);
				Writer.WriteLine("5;Jens;Jensen;5000;Bolbro;Danmark;588359;Bo@bronze.com;U3651,U8597,U8526,U4825,U9628,U6255,U6666,D6666,U1313,Z8542,A9999", true);
			}
			Thread.Sleep(5000);
			int Count = or.GetOrderDic().Count();
			Assert.AreEqual(5, Count);
		}
		public void ClearTxt()
		{
			ImportController ic = new ImportController();
			string fileName = "Orders.txt";
			string relativePath = ic.GetFilePath(fileName);
			using (StreamWriter Writer = new StreamWriter(relativePath))
			{

				File.WriteAllText(relativePath, String.Empty);
				File.Create(relativePath).Close();
			}
		}
	}
}
