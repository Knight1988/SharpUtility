using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDomainTestRunner
{
    public abstract class ExporterBase : MarshalByRefObject
    {
        public string Name { get; private set; }
        public Version Version { get; set; }

        protected ExporterBase()
        {
            Name = GetType().FullName;
            Version = new Version(1, 0);
        }
    }
}
