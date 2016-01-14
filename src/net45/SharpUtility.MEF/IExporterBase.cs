using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDomainTestRunner
{
    public interface IExporterBase
    {
        string Name { get; }
        Version Version { get; set; }
    }
}
