using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.IO;
using System.Linq;
using ExportInterface;

namespace SharpUtility.MEF
{
    public class Runner : MarshalByRefObject
    {
        private CompositionContainer container;
        private DirectoryCatalog directoryCatalog;
        private static string pluginPath = Path.Combine
        (AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
        "Plugins");

        public IEnumerable<IExport> Exports { get; private set; }

        public void DoWorkInShadowCopiedDomain()
        {
            string currentFilePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            string currentFileName = Path.GetFileNameWithoutExtension(currentFilePath);
            pluginPath = Path.GetTempPath() + currentFileName + "\\Plugins";

            // Use RegistrationBuilder to set up our MEF parts.
            var regBuilder = new RegistrationBuilder();
            regBuilder.ForTypesDerivedFrom
            <IExport>().Export<IExport>();

            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog
            (typeof(Runner).Assembly, regBuilder));
            directoryCatalog = new DirectoryCatalog(pluginPath, regBuilder);
            catalog.Catalogs.Add(directoryCatalog);

            container = new CompositionContainer(catalog);
            container.ComposeExportedValue(container);

            // Get our exports available to the rest of Program.
            Exports = container.GetExportedValues<IExport>();
            Console.WriteLine("{0} exports in AppDomain {1}",
            Exports.Count(), AppDomain.CurrentDomain.FriendlyName);
        }

        public void Recompose()
        {
            // Gimme 3 steps...
            directoryCatalog.Refresh();
            container.ComposeParts(directoryCatalog.Parts);
            Exports = container.GetExportedValues<IExport>();
        }
    }
}
