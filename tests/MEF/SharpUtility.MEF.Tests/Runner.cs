using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEFInterface;

namespace SharpUtility.MEF.Tests
{
    public class Runner : SharpUtility.MEF.RunnerBase<Export>
    {
        public string DoSomething()
        {
            // Tell our MEF parts to do something.
            return string.Join(" ", Exports.Select(p => p.Value.InHere()));
        }
    }
}
