using NUnit.Framework;
using SharpUtility.WindowService;

namespace SharpUtility.Core.Tests.WindowService
{
    [TestFixture]
    public class WindowServiceManagerTests
    {
        [Test, Category("CloudIgnore")]
        public void InstallTest()
        {
            WindowServiceManager.Install("TestService", "TestService", "TestService");

            var isInstalled = WindowServiceManager.IsIntalled("TestService");

            Assert.True(isInstalled);
        }

        [Test, Category("CloudIgnore")]
        public void UninstallTest()
        {
            WindowServiceManager.Uninstall("TestService");

            var isInstalled = WindowServiceManager.IsIntalled("TestService");

            Assert.False(isInstalled);
        }
    }
}