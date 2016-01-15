using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpUtility.MEF
{
    public class RunnerManager
    {
        private static readonly Dictionary<string, DomainRunner> _domainRunners = new Dictionary<string, DomainRunner>();

        public static TRunner CreateRunner<TRunner, TExporter>(string domainName, string pluginPath, string cachePath)
            where TRunner : RunnerBase<TExporter>
            where TExporter : IExporterBase
        {
            var basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            return CreateRunner<TRunner, TExporter>(domainName, pluginPath, cachePath, basePath);
        }

        public static TRunner CreateRunner<TRunner, TExporter>(string domainName, string pluginPath, string cachePath, string basePath) 
            where TRunner : RunnerBase<TExporter>
            where TExporter : IExporterBase
        {
            if (!Directory.Exists(cachePath))
            {
                Directory.CreateDirectory(cachePath);
            }

            if (!Directory.Exists(pluginPath))
            {
                Directory.CreateDirectory(pluginPath);
            }

            // This creates a ShadowCopy of the MEF DLL's (and any other DLL's in the ShadowCopyDirectories)
            var setup = new AppDomainSetup
            {
                CachePath = cachePath,
                ShadowCopyFiles = "true",
                ShadowCopyDirectories = pluginPath,
                ApplicationBase = basePath
            };

            // Create a new AppDomain then create an new instance of this application in the new AppDomain.
            // This bypasses the Main method as it's not executing it.
            var domain = AppDomain.CreateDomain(domainName, AppDomain.CurrentDomain.Evidence, setup);
            var runner = (TRunner)domain.CreateInstanceAndUnwrap(typeof(TRunner).Assembly.FullName, typeof(TRunner).FullName);
            runner.Initialize();

            return runner;
        }
    }

    internal class DomainRunner
    {
        public AppDomain Domain { get; set; }
        public RunnerBase<IExporterBase> Runner { get; set; }
    }
}
