using MEFInterface;
using SharpUtility.Runtime.Remoting;

namespace MEFTestLib1
{
    public class Import : Sponsor, IExport
    {
        public string InHere()
        {
            return "Lib1";
        }

        public string Name { get; set; }
    }
}
