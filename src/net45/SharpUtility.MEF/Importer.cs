using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpUtility.MEF
{
    public class Importer
    {
        private AppDomain domain;
        private Runner runner;

        public Importer()
        {
            string currentFilePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            string currentFileName = Path.GetFileNameWithoutExtension(currentFilePath);
            string pluginPath = Path.GetTempPath() + currentFileName + "\\Plugins";
            string cachePath = Path.GetTempPath() + currentFileName + "\\Cache";

            if (!Directory.Exists(cachePath))
            {
                Directory.CreateDirectory(cachePath);
            }

            if (!Directory.Exists(pluginPath))
            {
                Directory.CreateDirectory(pluginPath);
            }

            // This creates a ShadowCopy of the MEF DLL's 
            // (and any other DLL's in the ShadowCopyDirectories)
            var setup = new AppDomainSetup
            {
                CachePath = cachePath,
                ShadowCopyFiles = "true",
                ShadowCopyDirectories = pluginPath
            };

            // Create a new AppDomain then create a new instance 
            // of this application in the new AppDomain.            
            domain = AppDomain.CreateDomain("Host_AppDomain",
            AppDomain.CurrentDomain.Evidence, setup);
            runner = (Runner)domain.CreateInstanceAndUnwrap
            (typeof(Runner).Assembly.FullName, typeof(Runner).FullName);

            runner.DoWorkInShadowCopiedDomain();
        }

        public void Recompose()
        {
            runner.Recompose();
        }
    }
}
