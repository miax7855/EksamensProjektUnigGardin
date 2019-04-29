using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Domainlayer
{
    public class Errors
    {
        public void SaveErrorLog(string message)
        {
			using (StreamWriter sw = new StreamWriter("ErrorLog.txt"))
				sw.WriteLine(DateTime.Now + "\n" + message + "\n///\n///\n///\n");
		}
    }
}
