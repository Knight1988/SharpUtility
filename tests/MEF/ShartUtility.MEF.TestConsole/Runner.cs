using System.Linq;
using MEFInterface;
using SharpUtility.MEF;

namespace ShartUtility.MEF.TestConsole
{
    public class Runner : RunnerBase<Export>
    {
        public string DoSomething()
        {
            // Tell our MEF parts to do something.
            return string.Join(" ", Exports.Select(p => p.Value.InHere()));
        }
    }
}
