using System;

namespace SharpUtility.MEF
{
    public abstract class ExporterBase : MarshalByRefObject, IExporterBase
    {
        public string Name { get; private set; }
        public Version Version { get; set; }

        protected ExporterBase()
        {
            Name = GetType().FullName;
            Version = new Version(1, 0);
        }
    }
}
