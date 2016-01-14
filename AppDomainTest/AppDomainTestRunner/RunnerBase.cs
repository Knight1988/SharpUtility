using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.IO;
using System.Linq;

namespace AppDomainTestRunner
{
    public class RunnerBase<T> : MarshalByRefObject
    {
        private CompositionContainer _container;
        private DirectoryCatalog _directoryCatalog;
        public IEnumerable<T> Exports { get; private set; }
        private static readonly string PluginPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Plugins");

        public void Initialize()
        {
            // Use RegistrationBuilder to set up our MEF parts.
            var regBuilder = new RegistrationBuilder();
            regBuilder.ForTypesDerivedFrom<T>().Export<T>();

            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(RunnerBase<T>).Assembly, regBuilder));
            _directoryCatalog = new DirectoryCatalog(PluginPath, regBuilder);
            catalog.Catalogs.Add(_directoryCatalog);

            _container = new CompositionContainer(catalog);
            _container.ComposeExportedValue(_container);

            // Get our exports available to the rest of Program.
            Exports = _container.GetExportedValues<T>();
            Console.WriteLine("{0} exports in AppDomain {1}", Exports.Count(), AppDomain.CurrentDomain.FriendlyName);
        }

        public void Recompose()
        {
            // Gimme 3 steps...
            _directoryCatalog.Refresh();
            _container.ComposeParts(_directoryCatalog.Parts);
            Exports = _container.GetExportedValues<T>();
        }
    }
}
