using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MEFInterface;
using NUnit.Framework;
using SharpUtility.MEF;
using SharpUtility.MEF.Tests;

// ReSharper disable PossibleNullReferenceException

namespace AppDomainTestRunner.Tests
{
    [TestFixture]
    public class RunnerBaseTests
    {
        [Test]
        public async Task TestSponsor()
        {
            var pluginPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Plugins");
            var currentDir = new DirectoryInfo(AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
            var rootPath = currentDir.Parent.Parent.Parent;
            var lib1V1Path = Path.Combine(rootPath.FullName, @"bin\MEFTestLib1.dll");
            var lib1PluginPath = Path.Combine(pluginPath, "MEFTestLib1.dll");

            // Clear plugin folder
            if (Directory.Exists(pluginPath))
            {
                Directory.Delete(pluginPath, true);
            }
            Directory.CreateDirectory(pluginPath);
            // Copy lib1
            File.Copy(lib1V1Path, lib1PluginPath);

            var runner = RunnerManager.CreateRunner<Runner, IExport>("Plugins");

            var actual = runner.Exports.First().Value;
            Assert.AreEqual("MEFTestLib1.Import", actual.Name);

            await Task.Delay(new TimeSpan(0, 5, 10));

            actual = runner.Exports.First().Value;
            Assert.AreEqual("MEFTestLib1.Import", actual.Name);

            Assert.True(RunnerManager.RemoveRunner("Plugins"));
        }

        [Test]
        public void ChangePluginFolderTest()
        {
            var currentDir = new DirectoryInfo(AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
            var pluginPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var rootPath = currentDir.Parent.Parent.Parent;
            var lib1V1Path = Path.Combine(rootPath.FullName, @"bin\MEFTestLib1.dll");
            var lib1PluginPath = Path.Combine(pluginPath, "MEFTestLib1.dll");

            // Copy lib1
            File.Copy(lib1V1Path, lib1PluginPath);

            var runner = RunnerManager.CreateRunner<Runner, IExport>("Plugins", pluginPath);

            var actual = runner.DoSomething();
            var expected = "Lib1";

            Assert.AreEqual(expected, actual);

            Assert.True(RunnerManager.RemoveRunner("Plugins"));
        }

        [Test]
        public void SwapDllTest()
        {
            var pluginPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Plugins");
            var currentDir = new DirectoryInfo(AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
            var rootPath = currentDir.Parent.Parent.Parent;
            var lib1V1Path = Path.Combine(rootPath.FullName, @"bin\MEFTestLib1.dll");
            var lib1V2Path = Path.Combine(rootPath.FullName, @"bin\MEFTestLib1-v2.dll");
            var lib2Path = Path.Combine(rootPath.FullName, @"bin\MEFTestLib2.dll");
            var lib1PluginPath = Path.Combine(pluginPath, "MEFTestLib1.dll");
            var lib2PluginPath = Path.Combine(pluginPath, "MEFTestLib2.dll");

            // Clear plugin folder
            if (Directory.Exists(pluginPath))
            {
                Directory.Delete(pluginPath, true);
            }
            Directory.CreateDirectory(pluginPath);
            // Copy lib1
            File.Copy(lib1V1Path, lib1PluginPath);

            var runner = RunnerManager.CreateRunner<Runner, IExport>("Plugins");

            var actual = runner.DoSomething();
            var expected = "Lib1";

            Assert.AreEqual(expected, actual);

            // Copy lib2
            File.Copy(lib2Path, lib2PluginPath);
            runner.Recompose();
            actual = runner.DoSomething();
            expected = "Lib1 Lib2";

            Assert.AreEqual(expected, actual);
            // Replace lib1 with v2
            File.Delete(lib1PluginPath);
            runner.Recompose();
            File.Copy(lib1V2Path, lib1PluginPath, true);
            runner.Recompose();
            actual = runner.DoSomething();
            expected = "Lib2 Lib1-v2";

            Assert.AreEqual(expected, actual);

            // Delete Lib1
            File.Delete(lib1PluginPath);
            runner.Recompose(); 
            actual = runner.DoSomething();
            expected = "Lib2";

            Assert.AreEqual(expected, actual);

            // Delete Lib2
            File.Delete(lib2PluginPath);
            runner.Recompose(); 
            actual = runner.DoSomething();
            expected = string.Empty;

            Assert.AreEqual(expected, actual);
            Assert.True(RunnerManager.RemoveRunner("Plugins"));
        }
    }
}
