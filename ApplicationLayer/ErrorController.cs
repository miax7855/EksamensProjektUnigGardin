using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer
{
	public class ErrorController
	{
		public void SaveErrorLog(string message)
		{
			ImportController ic = new ImportController();
			string fileName = "ErrorLog.txt";
			string relativePath = ic.GetFilePath(fileName);
			Controller c = new Controller();
			using (StreamWriter sw = new StreamWriter(relativePath))
				sw.WriteLine(DateTime.Now + "\n" + message + "\n///\n///\n///");
		}

		// ingen datetime.now så den kan testes
		public void TestSaveErrorLog(string message)
		{
			ImportController ic = new ImportController();
			string fileName = "ErrorLog.txt";
			string relativePath = ic.GetFilePath(fileName);
			Controller c = new Controller();
			using (StreamWriter sw = new StreamWriter(relativePath))
				sw.WriteLine(message + "\n///\n///\n///");
		}
	}
}
