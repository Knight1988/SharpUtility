using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExportInterface;

namespace ExportV1
{
    public class Class1 : MarshalByRefObject, IExport
    {
        public string Test()
        {
            return "V1";
        }
    }
}
