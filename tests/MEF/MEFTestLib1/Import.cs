using System;
using System.ComponentModel.Composition;
using MEFInterface;

namespace MEFTestLib1
{
    [Export(typeof(IExport))]
    [Serializable]
    public class Import : IExport
    {
        public string InHere()
        {
            return "Lib1";
        }

        public string Name { get; set; }
    }
}
