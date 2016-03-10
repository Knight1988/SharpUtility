using System;
using SharpUtility.Runtime.Remoting;

namespace SharpUtility.MEF
{
    public abstract class ExporterBase : Sponsor, IExporterBase
    {
        public string Name { get; set; }
    }
    public abstract class ExporterBase<TEventHandler> : Sponsor, IExporterBase<TEventHandler> where TEventHandler : MarshalByRefObject
    {
        public string Name { get; set; }
        public abstract TEventHandler EventHandler { get; set; }
    }
}
