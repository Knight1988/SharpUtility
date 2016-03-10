using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.IO;
using System.Linq;
using SharpUtility.Runtime.Remoting;

namespace SharpUtility.MEF
{
    public class RunnerBase<T> : Sponsor where T : IExporterBase
    {
        private CompositionContainer _container;
        private readonly List<DirectoryCatalog> _directoryCatalogs = new List<DirectoryCatalog>();
        public Dictionary<string, T> Exports { get; private set; }

        public string PluginPath { get; private set; }

        public void Initialize()
        {
            var pluginPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Plugins");
            Initialize(pluginPath, "*.dll", "*.exe");
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
            //catalog.Catalogs.Add(new AssemblyCatalog(typeof (RunnerBase<T>).Assembly, regBuilder));
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
        }

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
            foreach (var directoryCatalog in _directoryCatalogs)
            {
                directoryCatalog.Refresh();
                _container.ComposeParts(directoryCatalog.Parts);
            }
            Exports = GetExportedValues();
        }
    }
}