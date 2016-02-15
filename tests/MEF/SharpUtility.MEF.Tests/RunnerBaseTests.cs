using System;
using System.IO;
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

            var runner = RunnerManager.CreateRunner<Runner, IExport>("Test");

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
            expected = "Lib1-v2 Lib2";

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
        }

        [Ignore("Not complete")]
        public void SwapDllEventTest()
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
            if (Directory.Exists(pluginPath))
            {
                Directory.Delete(pluginPath, true);
            }
            Directory.CreateDirectory(pluginPath);
            // Copy lib1
            File.Copy(lib1V1Path, lib1PluginPath);

            var runner = RunnerManager.CreateRunner<Runner, IExport>("Test", pluginPath, cachePath);
            runner.AutoRecompose = true;

            var actual = runner.DoSomething();
            var expected = "Lib1";

            Assert.AreEqual(expected, actual);

            // Copy lib2
            File.Copy(lib2Path, lib2PluginPath);
            runner.WaitForUpdate();
            actual = runner.DoSomething();
            expected = "Lib1 Lib2";

            Assert.AreEqual(expected, actual);
            // Replace lib1 with v2
            File.Delete(lib1PluginPath);
            runner.WaitForUpdate();
            File.Copy(lib1V2Path, lib1PluginPath, true);
            runner.WaitForUpdate();
            actual = runner.DoSomething();
            expected = "Lib1-v2 Lib2";

            Assert.AreEqual(expected, actual);

            // Delete Lib1
            File.Delete(lib1PluginPath);
            runner.WaitForUpdate();
            actual = runner.DoSomething();
            expected = "Lib2";

            Assert.AreEqual(expected, actual);

            // Delete Lib2
            File.Delete(lib2PluginPath);
            runner.WaitForUpdate();
            actual = runner.DoSomething();
            expected = string.Empty;

            Assert.AreEqual(expected, actual);
        }
    }
}
