using System;
using AppDomainTestInterfaces;

namespace AppDomainTestLib {

	public class Import : Export {

        public override void InHere()
        {
            Console.WriteLine("I'm Library1");
        }
	}
}