using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SharpUtility.MEF;

namespace MEFInterface
{
    public class InterfaceExport : MarshalByRefObject, IExporterBase
    {
        public string Name { get; private set; }
        public Version Version { get; set; }

        public InterfaceExport()
        {
            Name = GetType().FullName;
            Version = new Version(1, 0);
        }
    }
}
