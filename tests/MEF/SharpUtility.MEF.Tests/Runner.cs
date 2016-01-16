using System.Linq;
using MEFInterface;

namespace SharpUtility.MEF.Tests
{
    public class Runner : RunnerBase<Export>
    {
        public string DoSomething()
        {
            // Here you can access Exports which contain Dictionary of plugin class
            // Tell our MEF parts to do something.
            return string.Join(" ", Exports.Select(p => p.Value.InHere()));
        }
    }
}
