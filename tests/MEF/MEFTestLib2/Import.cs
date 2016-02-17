using MEFInterface;
using SharpUtility.MEF;

namespace MEFTestLib2
{
    public class Import : IExport
    {
        public string InHere()
        {
            return "Lib2";
        }

        public string Name { get; set; }
        public ExporterEventHandler ExporterEventHandler { get; set; }
    }
}
