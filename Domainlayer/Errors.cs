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
            using (StreamWriter sw = new StreamWriter(@"Source\Repos\EksamensProjektUnigGardin\Domainlayer\ErrorLog.txt")) 
                sw.WriteLine(message);
        }
    }
}
