using System;
using System.IO;
using AppDomainTestRunner;

namespace AppDomainTest {

	internal class Program {
		private static AppDomain domain;

		[STAThread]
		private static void Main() {
			var cachePath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "ShadowCopyCache");
			var pluginPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Plugins");
			if (!Directory.Exists(cachePath)) {
				Directory.CreateDirectory(cachePath);
			}

			if (!Directory.Exists(pluginPath)) {
				Directory.CreateDirectory(pluginPath);
			}

			// This creates a ShadowCopy of the MEF DLL's (and any other DLL's in the ShadowCopyDirectories)
			var setup = new AppDomainSetup {
				CachePath = cachePath,
				ShadowCopyFiles = "true",
				ShadowCopyDirectories = pluginPath
			};

			// Create a new AppDomain then create an new instance of this application in the new AppDomain.
			// This bypasses the Main method as it's not executing it.
			domain = AppDomain.CreateDomain("Host_AppDomain", AppDomain.CurrentDomain.Evidence, setup);
			var runner = (Runner)domain.CreateInstanceAndUnwrap(typeof(Runner).Assembly.FullName, typeof(Runner).FullName);
		    runner.AutoRecompose = true;
		    runner.ExportUpdate += (sender, args) =>
		    {
		        var runner1 = (Runner) sender;
                runner1.DoSomething();
		    };
			Console.WriteLine("The main AppDomain is: {0}", AppDomain.CurrentDomain.FriendlyName);

			// We now have access to all the methods and properties of Program.
			runner.Initialize();
			runner.DoSomething();

			Console.WriteLine("Press any key when ready...");
			Console.ReadKey();

			// Clean up.
			AppDomain.Unload(domain);
		}
	}
}