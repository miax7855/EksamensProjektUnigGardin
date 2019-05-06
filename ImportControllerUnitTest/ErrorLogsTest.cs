using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domainlayer;
using System.IO;
using ApplicationLayer;


namespace ImportControllerUnitTest
{
	[TestClass]
	public class ErrorLogsTest
	{
		[TestMethod]
		public void TestSaveErrorLog()
		{
			string actualString;
			ErrorController errorsTest = new ErrorController();

			string expectedString = "THERE ARE OVER 9000 ERRORS!";
			errorsTest.SaveErrorLog(expectedString);
			actualString = File.ReadAllText("ErrorLog.txt");

			Assert.AreEqual(expectedString, actualString);
		}
        [TestMethod]
        public void TestSaveOrderCatch()
        {
            Order order = new Order();
           
            DBController dbc = new DBController();
            ErrorController errorstest = new ErrorController();
            order = null;
            dbc.SaveOrder(order);
        }
	}
}
