using System.ServiceProcess;

namespace SharpUtility.WindowService
{
    public class ServiceSettings
    {
        /// <summary>
        /// Window username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Window password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Full path to file you want to install as service
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Service's display name
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// Service's description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Service's name
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// Create a log file after installing service
        /// </summary>
        public string LogPath { get; set; }
        /// <summary>
        /// Set the service password
        /// </summary>
        public ServiceStartMode StartMode { get; set; } = ServiceStartMode.Automatic;
        /// <summary>
        /// Service account which the service will run
        /// </summary>
        public ServiceAccount Account { get; set; } = ServiceAccount.LocalSystem;
    }
}