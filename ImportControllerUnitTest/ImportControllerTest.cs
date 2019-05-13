﻿using System;
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
		[TestMethod]
		public void ImportReaderTest1()
		{
			//State based Test
			//ARRANGE
			string[] TestArray = { "hello", "goddag", "Nice" };

			ImportController ICT = new ImportController();

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

			ImportController ICT = new ImportController();

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

			ImportController ICT = new ImportController();

			//ACT
			List<string> TestList = ICT.ConvertArrayToList(TestArray);

			//ASSERT
			Assert.AreEqual(TestList[2], TestArray[2]);
		}

	}
}
