﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEFInterface;
using SharpUtility.MEF;

namespace MEFTestLib2
{
    public class Import : Export
    {
        public override string InHere()
        {
            return "Lib2";
        }
    }
}
