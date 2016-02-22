using System.Runtime.Remoting.Lifetime;
using MEFInterface;
using SharpUtility.Runtime.Remoting;

namespace MEFTestLib1
{
    public class Import : Sponsor, IExport
    {
        public string InHere()
        {
            return "Exe1";
        }

        public string Name { get; set; }
    }
}
