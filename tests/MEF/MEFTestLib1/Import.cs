﻿using MEFInterface;

namespace MEFTestLib1
{
    public class Import : IExport
    {
        public string InHere()
        {
            return "Lib1";
        }

        public string Name { get; set; }
    }
}
