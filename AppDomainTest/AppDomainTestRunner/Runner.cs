using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.IO;
using System.Linq;
using AppDomainTestInterfaces;

namespace AppDomainTestRunner {

	public class Runner : MarshalByRefObject {
		private CompositionContainer _container;
		private DirectoryCatalog _directoryCatalog;
		private IEnumerable<IExport> _exports;
		private static readonly string PluginPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Plugins");

		public void Initialize() {
			// Use RegistrationBuilder to set up our MEF parts.
			var regBuilder = new RegistrationBuilder();
			regBuilder.ForTypesDerivedFrom<IExport>().Export<IExport>();

			var catalog = new AggregateCatalog();
			catalog.Catalogs.Add(new AssemblyCatalog(typeof(Runner).Assembly, regBuilder));
			_directoryCatalog = new DirectoryCatalog(PluginPath, regBuilder);
			catalog.Catalogs.Add(_directoryCatalog);

			_container = new CompositionContainer(catalog);
			_container.ComposeExportedValue(_container);

			// Get our exports available to the rest of Program.
			_exports = _container.GetExportedValues<IExport>();
			Console.WriteLine("{0} exports in AppDomain {1}", _exports.Count(), AppDomain.CurrentDomain.FriendlyName);
		}

		public void Recompose() {
			// Gimme 3 steps...
			_directoryCatalog.Refresh();
			_container.ComposeParts(_directoryCatalog.Parts);
			_exports = _container.GetExportedValues<IExport>();
		}

		public void DoSomething() {
			// Tell our MEF parts to do something.
			_exports.ToList().ForEach(e => {
				e.InHere();
			});
		}
	}
}