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
			bool True = true;
			string actualString;
			ErrorController errorsTest = new ErrorController();
			string expectedString = "THERE ARE OVER 9000 ERRORS!\n///\n///\n///";
			errorsTest.TestSaveErrorLog(expectedString);
			actualString = File.ReadAllText("ErrorLog.txt");

			Assert.AreEqual(expectedString, actualString);
			//Assert.AreEqual(True, expectedString.Equals(actualString));
		}
	}
}
