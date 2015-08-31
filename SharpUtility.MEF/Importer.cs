using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;

namespace SharpUtility.MEF
{
    public class Importer<T>
    {
#pragma warning disable 649
        private IEnumerable<Lazy<T>> _operations;
#pragma warning restore 649

        public Importer(string pluginPath)
        {
            PluginPath = pluginPath;
        }

        public string PluginPath { get; set; }

        public int AvailableNumberOfOperation
        {
            get { return _operations != null ? _operations.Count() : 0; }
        }

        public void DoImport()
        {
            //An aggregate catalog that combines multiple catalogs
            var catalog = new AggregateCatalog();

            //Add all the parts found in all assemblies in
            //the same directory as the executing program
            catalog.Catalogs.Add(new DirectoryCatalog(PluginPath));

            //Create the CompositionContainer with the parts in the catalog.
            var container = new CompositionContainer(catalog);

            //Fill the imports of this object
            container.ComposeParts(this);
        }
    }
}