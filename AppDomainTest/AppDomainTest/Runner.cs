using System.Linq;
using AppDomainTestInterfaces;
using AppDomainTestRunner;

namespace AppDomainTest {

	public class Runner : RunnerBase<Export>
	{
		public void DoSomething() {
			// Tell our MEF parts to do something.
		    foreach (var pair in Exports)
		    {
                pair.Value.InHere();
		    }
		}
	}
}