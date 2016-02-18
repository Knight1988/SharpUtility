using System;

namespace SharpUtility.MEF
{
    internal class DomainRunner
    {
        public AppDomain Domain { get; set; }
        public object Runner { get; set; }
    }
}