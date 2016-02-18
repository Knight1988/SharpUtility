using System;

namespace SharpUtility.MEF
{
    public interface IExporterBase
    {
        string Name { get; set; }
    }

    public interface IExporterBase<TEventHandler> : IExporterBase where TEventHandler : MarshalByRefObject
    {
        TEventHandler EventHandler { get; set; }
    }
}
