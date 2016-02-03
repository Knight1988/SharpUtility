using System;
using System.Collections.Generic;
using System.IO;

namespace SharpUtility.MEF
{
    public static class RunnerManager
    {
        private static readonly Dictionary<string, DomainRunner> _domainRunners = new Dictionary<string, DomainRunner>();

        /// <summary>
        /// Create and initalize runner
        /// </summary>
        /// <typeparam name="TRunner"></typeparam>
        /// <typeparam name="TExporter"></typeparam>
        /// <param name="domainName">must be unique</param>
        /// <returns></returns>
        public static TRunner CreateRunner<TRunner, TExporter>(string domainName)
            where TRunner : RunnerBase<TExporter>
            where TExporter : IExporterBase
        {
            var pluginPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Plugins");
            return CreateRunner<TRunner, TExporter>(domainName, pluginPath);
        }
        /// <summary>
        /// Create and initalize runner
        /// </summary>
        /// <typeparam name="TRunner"></typeparam>
        /// <typeparam name="TExporter"></typeparam>
        /// <param name="domainName">must be unique</param>
        /// <param name="pluginPath">the location of plugin folder</param>
        /// <returns></returns>
        public static TRunner CreateRunner<TRunner, TExporter>(string domainName, string pluginPath)
            where TRunner : RunnerBase<TExporter>
            where TExporter : IExporterBase
        {
            var cachePath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "ShadowCopyCache");
            return CreateRunner<TRunner, TExporter>(domainName, pluginPath, cachePath);
        }

        /// <summary>
        /// Create and initalize runner
        /// </summary>
        /// <typeparam name="TRunner"></typeparam>
        /// <typeparam name="TExporter"></typeparam>
        /// <param name="domainName">must be unique</param>
        /// <param name="pluginPath">the location of plugin folder</param>
        /// <param name="cachePath">location to shadow copy the plugin</param>
        /// <returns></returns>
        public static TRunner CreateRunner<TRunner, TExporter>(string domainName, string pluginPath, string cachePath)
            where TRunner : RunnerBase<TExporter>
            where TExporter : IExporterBase
        {
            var basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            return CreateRunner<TRunner, TExporter>(domainName, pluginPath, cachePath, basePath);
        }

        /// <summary>
        /// Create and initalize runner
        /// </summary>
        /// <typeparam name="TRunner"></typeparam>
        /// <typeparam name="TExporter"></typeparam>
        /// <param name="domainName">must be unique</param>
        /// <param name="pluginPath">the location of plugin folder</param>
        /// <param name="cachePath">location to shadow copy the plugin</param>
        /// <param name="basePath">location for plugin to find it's reference. Should be same as exe folder or plugin folder</param>
        /// <returns></returns>
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

            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            // This creates a ShadowCopy of the MEF DLL's (and any other DLL's in the ShadowCopyDirectories)
            var setup = new AppDomainSetup
            {
                CachePath = cachePath,
                ShadowCopyFiles = "true",
                ShadowCopyDirectories = pluginPath,
                ApplicationBase = basePath,
                PrivateBinPath = pluginPath
            };

            // Create a new AppDomain then create an new instance of this application in the new AppDomain.
            // This bypasses the Main method as it's not executing it.
            var domain = AppDomain.CreateDomain(domainName, AppDomain.CurrentDomain.Evidence, setup);
            var runner = (TRunner)domain.CreateInstanceAndUnwrap(typeof(TRunner).Assembly.FullName, typeof(TRunner).FullName);
            runner.Initialize();

            // Add domain & runner to dictionary
            _domainRunners.Add(domainName, new DomainRunner
            {
                Domain = domain,
                Runner = runner
            });

            return runner;
        }

        /// <summary>
        /// Get the runner from domain name. return null if not found or incorrect type
        /// </summary>
        /// <typeparam name="TRunner"></typeparam>
        /// <typeparam name="TExporter"></typeparam>
        /// <param name="domainName"></param>
        /// <returns></returns>
        public static TRunner GetRunner<TRunner, TExporter>(string domainName)
            where TRunner : RunnerBase<TExporter>
            where TExporter : IExporterBase
        {
            if (!_domainRunners.ContainsKey(domainName)) return default(TRunner);

            return _domainRunners[domainName].Runner as TRunner;
        }

        /// <summary>
        /// Remove the runner
        /// </summary>
        /// <param name="domainName"></param>
        /// <returns></returns>
        public static bool RemoveRunner(string domainName)
        {
            if (!_domainRunners.ContainsKey(domainName)) return false;

            // Unload then remove domain from list.
            var domainRunner = _domainRunners[domainName];
            AppDomain.Unload(domainRunner.Domain);
            _domainRunners.Remove(domainName);
            return true;
        }
    }

    internal class DomainRunner
    {
        public AppDomain Domain { get; set; }
        public object Runner { get; set; }
    }
}
