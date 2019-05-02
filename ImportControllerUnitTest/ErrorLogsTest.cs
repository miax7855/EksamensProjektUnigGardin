using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domainlayer;
using System.IO;

namespace ImportControllerUnitTest
{
	[TestClass]
	public class ErrorLogsTest
	{
		[TestMethod]
		public void TestSaveErrorLog()
		{
			string actualString;
			Errors errorsTest = new Errors();

			string expectedString = "THERE ARE OVER 9000 ERRORS!";
			errorsTest.SaveErrorLog(expectedString);
			actualString = File.ReadAllText("ErrorLog.txt");

			Assert.AreEqual(expectedString, actualString);
		}
	}
}
