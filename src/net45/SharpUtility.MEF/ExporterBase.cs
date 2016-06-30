using System;

namespace SharpUtility.MEF
{
    [Serializable]
    public abstract class ExporterBase : IExporterBase
    {
        public string Name { get; set; }
    }

    [Serializable]
    public abstract class ExporterBase<TEventHandler> : IExporterBase<TEventHandler> where TEventHandler : MarshalByRefObject
    {
        public string Name { get; set; }
        public abstract TEventHandler EventHandler { get; set; }
    }
}
