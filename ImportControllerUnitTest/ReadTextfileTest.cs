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
            DateTime date = new DateTime(2018, 11, 01, 02, 18, 11);
			Order o = new Order("Julian", "Petersen", 52464, "deutschland", 123456789, "julian @gmail.com", date, testSampleType);

			//ACT
			c.RefreshOrders(fileName);
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
            DateTime date = new DateTime(2018, 11, 01, 02, 18, 11);
            Order o = new Order("Asbjørn", "Larsen", 2464 ,"Danmark", 5648792, "Asbjorn@hotmail.com", date, testSampleType);

			//ACT
			c.RefreshOrders(fileName);
            Thread.Sleep(1000);
			Order o2 = (Order)or.GetOrderDic()[2];

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
            DateTime date = new DateTime(2018, 11, 01, 02, 18, 11);
            Order o = new Order("Asbjørn", "Larsen", 2464, "Danmark", 5648792, "Asbjorn@hotmail.com", date, testSampleType);

			//ACT
			c.RefreshOrders(fileName);
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
            string thing = string.Empty;

            using (StreamReader sr = new StreamReader(relativePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    thing += line + "\r\n";
                }
            }
            using (FileStream fs = new FileStream(relativePath, FileMode.Truncate, FileAccess.ReadWrite))
            {
                byte[] byteArray = System.Text.Encoding.ASCII.GetBytes("Julian;Petersen;52464;deutschland;123456789;julian@gmail.com;2018,11,01,02,18,11;1,2,3\r\n"+
                    "Mia;Pars;56998;Danmark;98765432;mia.pars@overslept.com;2018,11,01,02,18,11;U4000,A6666,K6666,U4001\r\n" +
                    "Asbjorn;Larsen;2464;Danmark;5648792;asbjorn@hotmail.com;2018,11,01,02,18,11;U6542,U7854\r\n" +
                    "Anders;Weiskvist;5000;Danmark;6543214;An@ders.com;2018,11,01,02,18,11;U5426\r\n" +
                    "Jens;Jensen;5000;Danmark;588359;Bo@bronze.com;2018,11,01,02,18,11;U3651,U8597,U8526,U4825,U9628,U6255,U6666,D6666,U1313,Z8542,A9999\r\n"
                    );
                fs.Write(byteArray, 0, byteArray.Length);
            }

            c.RefreshOrders(fileName);
            Thread.Sleep(5000);
            int Count = or.GetOrderDic().Count();
            Assert.AreEqual(5, Count);

            using (FileStream fs = new FileStream(relativePath, FileMode.Truncate, FileAccess.ReadWrite))
            {
                byte[] byteArray = System.Text.Encoding.ASCII.GetBytes(thing);
                fs.Write(byteArray, 0, thing.Length);
            }

            //ACT

            //ASSERT
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
