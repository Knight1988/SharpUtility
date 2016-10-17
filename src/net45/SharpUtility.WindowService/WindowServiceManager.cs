using System.Collections.Specialized;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;

namespace SharpUtility.WindowService
{
    /// <summary>
    /// Allow Install/Uninstall a window service. Need to run as Administrator right.
    /// </summary>
    public static class WindowServiceManager
    {
        /// <summary>
        /// Uninstall a service
        /// </summary>
        /// <param name="serviceName">Service name</param>
        /// <param name="logPath">(optional) Log path</param>
        public static void Uninstall(string serviceName, string logPath = null)
        {
            var serviceInstallerObj = new ServiceInstaller();
            var context = new InstallContext(logPath, null);
            serviceInstallerObj.Context = context;
            serviceInstallerObj.ServiceName = serviceName;
            serviceInstallerObj.Uninstall(null);
        }

        /// <summary>
        /// Install a service
        /// </summary>
        /// <param name="serviceName">Service name</param>
        /// <param name="displayName">Display name</param>
        /// <param name="description">Description</param>
        /// <param name="fileName">file name</param>
        /// <param name="settings">(optional) more settings</param>
        public static void Install(string serviceName, string displayName, string description, string fileName = null, ServiceSettings settings = null)
        {
            if (fileName == null) fileName = Assembly.GetExecutingAssembly().Location;

            if (settings == null)
            {
                settings = new ServiceSettings
                {
                    ServiceName = serviceName,
                    DisplayName = displayName,
                    Description = description,
                    FileName = fileName
                };
            }
            Install(settings);
        }

        /// <summary>
        /// Install a service
        /// </summary>
        /// <param name="settings">more settings</param>
        public static void Install(ServiceSettings settings)
        {
            var procesServiceInstaller = new ServiceProcessInstaller
            {
                Account = settings.Account,
                Username = settings.Username,
                Password = settings.Password
            };

            var serviceInstallerObj = new ServiceInstaller();
            var path = $"/assemblypath={settings.FileName}";
            string[] cmdline = {path};

            serviceInstallerObj.Context = new InstallContext(null, cmdline);
            serviceInstallerObj.DisplayName = settings.DisplayName;
            serviceInstallerObj.Description = settings.Description;
            serviceInstallerObj.ServiceName = settings.ServiceName;
            serviceInstallerObj.StartType = settings.StartMode;
            serviceInstallerObj.Parent = procesServiceInstaller;

            var state = new ListDictionary();
            serviceInstallerObj.Install(state);
        }

        /// <summary>
        /// Check if service is installed
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public static bool IsIntalled(string serviceName)
        {
            return ServiceController.GetServices().Any(s => s.ServiceName == serviceName);
        }
    }
}