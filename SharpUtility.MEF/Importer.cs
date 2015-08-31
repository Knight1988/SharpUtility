﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;

namespace SharpUtility.MEF
{
    public class Importer<T>
    {
        public Importer(string pluginPath)
        {
            PluginPath = pluginPath;
        }

        public string PluginPath { get; set; }

        public int AvailableNumberOfOperation
        {
            get { return Operations != null ? Operations.Count() : 0; }
        }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public IEnumerable<Lazy<T>> Operations { get; private set; }

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