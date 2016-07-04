using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.IO;
using System.Linq;

namespace SharpUtility.MEF
{
    [Serializable]
    public class RunnerBase<T> where T : IExporterBase
    {
        private CompositionContainer _container;
        private List<DirectoryCatalog> _directoryCatalogs;
        private Dictionary<string, T> _exports;

        public Dictionary<string, T> Exports
        {
            get
            {
                if (!IsInitialized) throw new Exception("Must call Initialize first.");
                return _exports;
            }
            private set { _exports = value; }
        }

        public string PluginPath { get; private set; }

        public void Initialize()
        {
            var pluginPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Plugins");
            Initialize(pluginPath);
        }

        public void Initialize(string pluginPath)
        {
            Initialize(pluginPath, "*.dll", "*.exe");
        }

        public void Initialize(string pluginPath, params string[] searchPatterns)
        {
            PluginPath = pluginPath;

            // Use RegistrationBuilder to set up our MEF parts.
            var regBuilder = new RegistrationBuilder();
            regBuilder.ForTypesDerivedFrom<T>().Export<T>();

            var catalog = new AggregateCatalog();
            _directoryCatalogs = new List<DirectoryCatalog>();
            foreach (var searchPattern in searchPatterns)
            {
                var directoryCatalog = new DirectoryCatalog(pluginPath, searchPattern, regBuilder);
                catalog.Catalogs.Add(directoryCatalog);
                _directoryCatalogs.Add(directoryCatalog);
            }

            _container = new CompositionContainer(catalog);
            _container.ComposeExportedValue(_container);

            // Get our exports available to the rest of Program.
            Exports = GetExportedValues();
            IsInitialized = true;
        }

        public bool IsInitialized { get; private set; }

        private Dictionary<string, T> GetExportedValues()
        {
            var values = _container.GetExportedValues<T>();

            return values.ToDictionary(p =>
            {
                if (string.IsNullOrWhiteSpace(p.Name)) p.Name = p.GetType().FullName;
                return p.Name;
            }, p => p);
        }

        public void Recompose()
        {
            if (!IsInitialized) throw new Exception("Must call Initialize first.");

            foreach (var directoryCatalog in _directoryCatalogs)
            {
                directoryCatalog.Refresh();
                _container.ComposeParts(directoryCatalog.Parts);
            }
            Exports = GetExportedValues();
        }
    }
}
