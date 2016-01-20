using System;
using MEFInterface;

namespace MEFTestLib1
{
    public class Import : Export
    {
        public override string InHere()
        {
            return "Lib1-v2";
        }
    }
}
