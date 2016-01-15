using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEFInterface;

namespace MEFTestLib1
{
    public class Import : Export
    {
        public override string InHere()
        {
            return "Lib1";
        }
    }
}
