using MEFInterface;
using SharpUtility.Runtime.Remoting;

namespace MEFTestExe1
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
