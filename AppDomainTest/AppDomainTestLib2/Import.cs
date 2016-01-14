using System;
using AppDomainTestInterfaces;

namespace AppDomainTestLib2 {

    public class Import : Export
    {
		public override void InHere() {
			Console.WriteLine("I'm Library2");
		}
	}
}