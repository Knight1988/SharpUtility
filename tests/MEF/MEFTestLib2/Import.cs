using MEFInterface;
using SharpUtility.Runtime.Remoting;

namespace MEFTestLib2
{
    public class Import : Sponsor, IExport
    {
        public string InHere()
        {
            return "Lib2";
        }

        public string Name { get; set; }
    }
}
