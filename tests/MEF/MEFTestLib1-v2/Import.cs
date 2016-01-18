using System;
using MEFInterface;

namespace MEFTestLib1
{
    public class Import : Export
    {
        public Import()
        {
            Version = new Version(1, 1);
        }

        public override string InHere()
        {
            return "Lib1-v2";
        }
    }
}
