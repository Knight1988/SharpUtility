using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpUtility.MEF;

namespace MEFInterface
{
    public interface IExport : IExporterBase
    {
        string InHere();
    }
}
