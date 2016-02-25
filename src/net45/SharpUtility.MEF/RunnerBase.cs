using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.IO;
using System.Linq;
using System.Threading;
using SharpUtility.Runtime.Remoting;

namespace SharpUtility.MEF
{
    public class RunnerBase<T> : Sponsor where T : IExporterBase
    {
        private CompositionContainer _container;
        private DirectoryCatalog _directoryCatalog;
        private DirectoryCatalog _directoryCatalog2;
        public Dictionary<string, T> Exports { get; private set; }

        public string PluginPath { get; private set; }

        public void Initialize()
        {
            var pluginPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Plugins");
            Initialize(pluginPath);
        }

        public void Initialize(string pluginPath)
        {
            PluginPath = pluginPath;

            // Use RegistrationBuilder to set up our MEF parts.
            var regBuilder = new RegistrationBuilder();
            regBuilder.ForTypesDerivedFrom<T>().Export<T>();

            var catalog = new AggregateCatalog();
            //catalog.Catalogs.Add(new AssemblyCatalog(typeof (RunnerBase<T>).Assembly, regBuilder));
            _directoryCatalog2 = new DirectoryCatalog(pluginPath, "*.exe",regBuilder);
            _directoryCatalog = new DirectoryCatalog(pluginPath, regBuilder);
            catalog.Catalogs.Add(_directoryCatalog);
            catalog.Catalogs.Add(_directoryCatalog2);

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
            _directoryCatalog.Refresh();
            _container.ComposeParts(_directoryCatalog.Parts);

            _directoryCatalog2.Refresh();
            _container.ComposeParts(_directoryCatalog2.Parts);

            Exports = GetExportedValues();
        }
    }
}