using System;

namespace SharpUtility.MEF
{
    public interface IExporterBase
    {
        string Name { get; }
        Version Version { get; set; }
    }
}
