using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApplicationLayer;
using System.Collections.Generic;
using Domainlayer;
using System.Linq;
using System.IO;
using System.Threading;

namespace ImportControllerUnitTest
{
	[TestClass]
	public class ReadTextfileTest
	{
		[TestInitialize]
		public void Initialize()
		{
			

		}
		string TempData1;
		string[] TempArray1;
		string[] TempArray2;
		int TempInteger;

		[TestMethod]
		public void TestProperReadingOfSampleTypes()
		{
			Controller c = new Controller();
			OrderRepository or = OrderRepository.GetOrderRepo();

			List<string> testSampleType = new List<string> { "1", "2", "3" };

			Order o = new Order(1, "Julian", "Petersen", 52464, "schleswig", "deutschland", 123456789, "julian @gmail.com", testSampleType);

			c.RefreshOrders("Orders.txt");
			Order o2 = or.GetOrderDic()[1];

			Assert.AreEqual(o.SampleType.ToString(), o2.SampleType.ToString());
		}
		[TestMethod]
		public void TestRefreshOrders()
		{
			
			string Filepath = @"C:\Users\chocobams\source\repos\EksamensProjektUnigGardin2\Domainlayer\Orders.txt";
			ClearTxt();
			Controller c = new Controller();
			c.RefreshOrders(Filepath);
			OrderRepository or = OrderRepository.GetOrderRepo();
			int Count = or.GetOrderDic().Count();
			using (StreamWriter Writer = new StreamWriter(Filepath, true))
			{
				Writer.WriteLine(""+ TempInteger+"6;Julian;Petersen;52464;Slesvig;deutschland;123456789;julian@gmail.com;1,2,3", true);
				Writer.WriteLine("" + TempInteger +1+ "7;Mia;Pars;56998;Odense;Danmark;98765432;mia.pars@camgirl.com;U4000,A6666,K6666,U4001", true);
				Writer.WriteLine("" + TempInteger +2+ "8;Assborn;Larsen;2464;Bahnhof;Danmark;5648792;Born@Ass.com;U6542,U7854", true);
				Writer.WriteLine("" + TempInteger +3+ "9;Anders;Weiskvist;5000;Bellinge;Danmark;6543214;An@ders.com;U5426", true);
				Writer.WriteLine("" + TempInteger +4+ "10;Jens;Jensen;5000;Bolbro;Danmark;588359;Bo@bronze.com;U3651,U8597,U8526,U4825,U9628,U6255,U6666,D6666,U1313,Z8542,A9999", true);
			}
			Thread.Sleep(5000);
			Count = or.GetOrderDic().Count();	
			Assert.AreEqual(5, Count);
		}
		public void ClearTxt()
		{
			string Filepath = @"C:\Users\chocobams\source\repos\EksamensProjektUnigGardin2\Domainlayer\Orders.txt";
			using (StreamWriter Writer = new StreamWriter(Filepath))
			{

				File.WriteAllText(Filepath, String.Empty);
				File.Create(Filepath).Close();
			}
		}
		
		




	}
}
