using System.Linq;
using AppDomainTestInterfaces;
using AppDomainTestRunner;

namespace AppDomainTest {

	public class Runner : RunnerBase<Export>
	{
		public void DoSomething() {
			// Tell our MEF parts to do something.
			Exports.ToList().ForEach(e => {
				e.InHere();
			});
		}
	}
}