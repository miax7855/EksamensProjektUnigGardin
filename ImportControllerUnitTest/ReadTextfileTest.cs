using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApplicationLayer;
using System.Collections.Generic;
using Domainlayer;
using System.IO;
using System.Threading;
using System.Linq;
using library;

namespace ImportControllerUnitTest
{
	[TestClass]
	public class ReadTextfileTest
	{
		ImportController ic;
		Controller c;
		OrderRepository or;

		[TestInitialize]
		public void Initialize()
		{
			ic = new ImportController();
			c = new Controller();
			or = OrderRepository.GetOrderRepo();
		}

		[TestMethod]
		public void TestProperReadingOfSampleTypes()
		{
			//Interaction based Test
			//ARRANGE
			string fileName = "Orders.txt";
			string relativePath = ic.GetFilePath(fileName);

			List<string> testSampleType = new List<string> { "1", "2", "3" };

			Order o = new Order(1, "Julian", "Petersen", 52464, "schleswig", "deutschland", 123456789, "julian @gmail.com", testSampleType);

			//ACT
			c.RefreshOrders(fileName, ic);
			// venter 1 sekund pga. den anden thread ikke har tilføjet data til orderRepo endnu
			Thread.Sleep(1000);
			Order o2 = (Order)or.GetOrderDic()[1];

			//ASSERT
			Assert.AreEqual(o.SampleType.ToString(), o2.SampleType.ToString());
		}

		[TestMethod]
		public void TestProperReadingOfOrder()
		{
			//Interaction based Test
			//ARRANGE
			string fileName = "Orders.txt";
			string relativePath = ic.GetFilePath(fileName);

			List<string> testSampleType = new List<string> { "U6542", "U7854" };

			Order o = new Order(3, "Assborn", "Larsen", 2464, "Bahnhof", "Danmark", 5648792, "Born @Ass.com", testSampleType);

			//ACT
			c.RefreshOrders(fileName, ic);
			Order o2 = (Order)or.GetOrderDic()[3];

			//ASSERT
			Assert.AreEqual(o.PrintOrderInfo(o), o2.PrintOrderInfo(o2));
		}

		[TestMethod]
		public void TestProperReadingOfOrder2()
		{
			//Interaction based Test
			//ARRANGE
			string fileName = "Orders.txt";
			string relativePath = ic.GetFilePath(fileName);

			//dengang med et forkert testSampleType liste
			List<string> testSampleType = new List<string> { "U6542"};

			Order o = new Order(3, "Assborn", "Larsen", 2464, "Bahnhof", "Danmark", 5648792, "Born @Ass.com", testSampleType);

			//ACT
			c.RefreshOrders(fileName, ic);
			//da refreshorders er en threat
			Thread.Sleep(1000);
			Order o2 = (Order)or.GetOrderDic()[3];

			//ASSERT
			Assert.AreNotEqual(o.PrintOrderInfo(o), o2.PrintOrderInfo(o2));
		}
		[TestMethod]
		public void TestRefreshOrders()
		{
			//Interaction based Test
			//ARRANGE
			string fileName = "Orders.txt";
			string relativePath = ic.GetFilePath(fileName);
			c.RefreshOrders(fileName, ic);

			using (StreamWriter Writer = new StreamWriter(relativePath, true))
			{
				Writer.WriteLine("1;Julian;Petersen;52464;Slesvig;deutschland;123456789;julian@gmail.com;1,2,3", true);
				Writer.WriteLine("2;Mia;Pars;56998;Odense;Danmark;98765432;mia.pars@camgirl.com;U4000,A6666,K6666,U4001", true);
				Writer.WriteLine("3;Assborn;Larsen;2464;Bahnhof;Danmark;5648792;Born@Ass.com;U6542,U7854", true);
				Writer.WriteLine("4;Anders;Weiskvist;5000;Bellinge;Danmark;6543214;An@ders.com;U5426", true);
				Writer.WriteLine("5;Jens;Jensen;5000;Bolbro;Danmark;588359;Bo@bronze.com;U3651,U8597,U8526,U4825,U9628,U6255,U6666,D6666,U1313,Z8542,A9999", true);
			}
			Thread.Sleep(5000);

			//ACT
			int Count = or.GetOrderDic().Count();

			//ASSERT
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
