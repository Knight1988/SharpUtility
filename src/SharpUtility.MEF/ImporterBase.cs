using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;

namespace SharpUtility.MEF
{
    public class ImporterBase<T> 
    {
        private readonly FileSystemWatcher _fileWatcher;

        public bool ReloadOnChanged
        {
            get { return _fileWatcher.EnableRaisingEvents; }
            set { _fileWatcher.EnableRaisingEvents = value; }
        }

        public ImporterBase(string pluginPath)
        {
            PluginPath = pluginPath;
            _fileWatcher = new FileSystemWatcher
            {
                Path = PluginPath,
                Filter = "*.dll",
                NotifyFilter =
                    NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.CreationTime | NotifyFilters.Size
            };
            _fileWatcher.Changed += FileWatcherOnChanged;
            _fileWatcher.Created += FileWatcherOnChanged;
            _fileWatcher.Deleted += FileWatcherOnChanged;
            _fileWatcher.Renamed += FileWatcherOnRenamed;
        }

        private void FileWatcherOnRenamed(object sender, RenamedEventArgs args)
        {
            DoImport();
        }

        private void FileWatcherOnChanged(object sender, FileSystemEventArgs args)
        {
            DoImport();
        }

        public string PluginPath { get; set; }

        public int AvailableNumberOfOperation
        {
            get { return Operations != null ? Operations.Count() : 0; }
        }

        [ImportMany]
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