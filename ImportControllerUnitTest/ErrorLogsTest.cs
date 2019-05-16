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
			//Interaction Based Testing
			//ARRANGE
			
			string actualString;
			ErrorController errorsTest = new ErrorController();
			string expectedString = "THERE ARE OVER 9000 ERRORS!\n///\n///\n///";

			//ACT
			errorsTest.TestSaveErrorLog(expectedString);
			actualString = File.ReadAllText("ErrorLog.txt");

			//ASSERT
			Assert.AreEqual(expectedString, actualString);
			//Assert.AreEqual(True, expectedString.Equals(actualString));
		}
        [TestMethod]
        public void TestSaveOrderCatch()
        {
			//Integration Testing
			//ARRANGE
            Order order = new Order();
           
            DBController dbc = new DBController();
            ErrorController errorstest = new ErrorController();
            order = null;

			//ACT
           // dbc.SaveOrder(order);
        }
	}
}
