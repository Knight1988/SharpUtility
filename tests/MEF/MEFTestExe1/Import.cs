using System;
using MEFInterface;

namespace MEFTestExe1
{
    [Serializable]
    public class Import : IExport
    {
        public string InHere()
        {
            return "Exe1";
        }

        public string Name { get; set; }
    }
}
