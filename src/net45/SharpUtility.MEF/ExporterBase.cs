using System;

namespace SharpUtility.MEF
{
    [Serializable]
    public abstract class ExporterBase : IExporterBase
    {
        public string Name { get; set; }
    }
}
