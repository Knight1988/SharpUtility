using System.ComponentModel.Composition;
using MEFInterface;
using SharpUtility.Runtime.Remoting;

namespace MEFTestLib1
{
    [Export(typeof(IExport))]
    public class Import : Sponsor, IExport
    {
        public string InHere()
        {
            return "Lib1";
        }

        public string Name { get; set; }
    }
}
