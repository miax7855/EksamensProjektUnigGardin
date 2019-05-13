using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApplicationLayer;
using EksamensProjektUnigGardin;
using System.Collections.Generic;
using System.Threading;

namespace ImportControllerUnitTest
{
	[TestClass]
	public class ImportControllerTest
	{
		ImportController ICT;

		[TestInitialize]
		public void TestInitialize()
		{
			ICT = new ImportController();
		}
		[TestMethod]
		public void ImportReaderTest1()
		{
			//State based Test
			//ARRANGE
			string[] TestArray = { "hello", "goddag", "Nice" };

			//ACT
			List<string> TestList = ICT.ConvertArrayToList(TestArray);

			//ASSERT
			Assert.AreEqual(TestList[0], TestArray[0]);
		}
		[TestMethod]
		public void ImportReaderTest2()
		{
			//State based Test
			//ARRANGE
			string[] TestArray = { "hello", "goddag", "Nice" };

			//ACT
			List<string> TestList = ICT.ConvertArrayToList(TestArray);

			//ASSERT
			Assert.AreEqual(TestList[1], TestArray[1]);
		}
		[TestMethod]
		public void ImportReaderTest3()
		{
			//State based Test
			//ARRANGE
			string[] TestArray = { "hello", "goddag", "Nice" };

			//ACT
			List<string> TestList = ICT.ConvertArrayToList(TestArray);

			//ASSERT
			Assert.AreEqual(TestList[2], TestArray[2]);
		}

	}
}
