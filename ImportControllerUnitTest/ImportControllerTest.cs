using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApplicationLayer;
using EksamensProjektUnigGardin;
using System.Collections.Generic;
using System.Threading;
using System.IO;

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
            Assert.AreEqual(typeof(List<string>), TestList.GetType());
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

		[TestMethod]
		public void TestDeleteFileContent()
		{
			//ARRANGE
			object fileNameObj = "TestText.txt";
			ICT.DeleteOrderItemEvent(fileNameObj);
			string relatvePath = ICT.GetFilePath("TestText.txt");

			StreamReader reader = new StreamReader(relatvePath);

			Assert.AreEqual(-1, reader.Peek());

		}

		[TestMethod]
		public void TestDeleteFileContent2()
		{
			//ARRANGE
			object fileNameObj = "TestText.txt";
			ICT.DeleteOrderItemEvent(fileNameObj);
			string relatvePath = ICT.GetFilePath("TestText.txt");

			StreamReader reader = new StreamReader(relatvePath);

			Assert.AreNotEqual(1, reader.Peek());

		}
	}
}
