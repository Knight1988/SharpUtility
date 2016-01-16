using System;
using System.IO;
using MEFInterface;
using NUnit.Framework;
using SharpUtility.MEF;
using SharpUtility.MEF.Tests;
// ReSharper disable PossibleNullReferenceException

namespace AppDomainTestRunner.Tests
{
    [TestFixture()]
    public class RunnerBaseTests
    {
        [Test()]
        public void SwapDllTest()
        {
            var cachePath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "ShadowCopyCache");
            var pluginPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Plugins");
            var currentDir = new DirectoryInfo(Environment.CurrentDirectory);
            var rootPath = currentDir.Parent.Parent.Parent;
            var lib1V1Path = Path.Combine(rootPath.FullName, @"MEFTestLib1\bin\Release\MEFTestLib1.dll");
            var lib1V2Path = Path.Combine(rootPath.FullName, @"MEFTestLib1-v2\bin\Release\MEFTestLib1-v2.dll");
            var lib2Path = Path.Combine(rootPath.FullName, @"MEFTestLib2\bin\Release\MEFTestLib2.dll");
            var lib1PluginPath = Path.Combine(pluginPath, "MEFTestLib1.dll");
            var lib2PluginPath = Path.Combine(pluginPath, "MEFTestLib2.dll");

            // Clear plugin folder
            Directory.Delete(pluginPath, true);

            var runner = RunnerManager.CreateRunner<Runner, Export>("Test", pluginPath, cachePath);

            var actual = runner.DoSomething();
            var expected = string.Empty;

            Assert.AreEqual(expected, actual);

            // Copy lib1
            File.Copy(lib1V1Path, lib1PluginPath);
            runner.Recompose();
            actual = runner.DoSomething();
            expected = "Lib1";

            Assert.AreEqual(expected, actual);

            // Copy lib2
            File.Copy(lib2Path, lib2PluginPath);
            runner.Recompose();
            actual = runner.DoSomething();
            expected = "Lib1 Lib2";

            Assert.AreEqual(expected, actual);
            // Replace lib1 with v2
            //File.Delete(lib1PluginPath);
            //runner.Recompose();
            File.Copy(lib1V2Path, lib1PluginPath, true);
            runner.Recompose();
            actual = runner.DoSomething();
            expected = "Lib1-v2 Lib2";

            Assert.AreEqual(expected, actual);

            // Delete Lib1
            File.Delete(lib1PluginPath);
            runner.Recompose(); actual = runner.DoSomething();
            expected = "Lib2";

            Assert.AreEqual(expected, actual);

            // Delete Lib2
            File.Delete(lib2PluginPath);
            runner.Recompose(); 
            actual = runner.DoSomething();
            expected = string.Empty;

            Assert.AreEqual(expected, actual);
        }

        [Test()]
        public void Test()
        {
            var cachePath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "ShadowCopyCache");
            var pluginPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Plugins");
            var basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
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
            var domain = AppDomain.CreateDomain("Host_AppDomain", AppDomain.CurrentDomain.Evidence, setup);
            var runner = (Runner)domain.CreateInstanceAndUnwrap(typeof(Runner).Assembly.FullName, typeof(Runner).FullName);

            // We now have access to all the methods and properties of Program.
            runner.Initialize();

            var actual = runner.DoSomething();
            var expected = string.Empty;

            Assert.AreEqual(expected, actual);
        }
    }
}
