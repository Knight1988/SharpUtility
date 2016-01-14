using System;
using AppDomainTestInterfaces;

namespace AppDomainTestLib2 {

	public class Import : MarshalByRefObject, IExport {

		public void InHere() {
			Console.WriteLine("I'm Library2");
		}
	}
}