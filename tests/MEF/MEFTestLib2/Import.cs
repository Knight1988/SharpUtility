using System;
using MEFInterface;

namespace MEFTestLib2
{
    [Serializable]
    public class Import : IExport
    {
        public string InHere()
        {
            return "Lib2";
        }

        public string Name { get; set; }
    }
}
