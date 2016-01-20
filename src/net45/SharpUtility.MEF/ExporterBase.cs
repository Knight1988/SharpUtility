using System;

namespace SharpUtility.MEF
{
    public abstract class ExporterBase : MarshalByRefObject, IExporterBase
    {
        public string Name { get; private set; }

        protected ExporterBase()
        {
            Name = GetType().FullName;
        }
    }
}
