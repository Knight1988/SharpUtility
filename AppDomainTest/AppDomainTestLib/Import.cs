using System;
using AppDomainTestInterfaces;

namespace AppDomainTestLib {

	public class Import : MarshalByRefObject, IExport {

        public void InHere()
        {
            Console.WriteLine("I'm Library1");
        }
	}
}