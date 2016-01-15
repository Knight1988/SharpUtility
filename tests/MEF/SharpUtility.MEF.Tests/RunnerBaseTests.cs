﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDomainTestRunner;
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
            var rootPath = currentDir.Parent.Parent;
            var runner = RunnerManager.CreateRunner<Runner, Export>("Test", pluginPath, cachePath);

            var actual = runner.DoSomething();
            var expected = string.Empty;

            Assert.AreEqual(expected, actual);
        }

        [Test()]
        public void Test()
        {
            var cachePath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "ShadowCopyCache");
            var pluginPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Plugins");
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
                ShadowCopyDirectories = pluginPath
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
