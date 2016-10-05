using NUnit.Framework;
using SharpUtility.WindowService;

namespace SharpUtility.Core.Tests.WindowService
{
    [TestFixture()]
    public class WindowServiceManagerTests
    {
        [Test()]
        public void InstallTest()
        {
            WindowServiceManager.Install("TestService", "TestService", "TestService", "C:\\");

            var isInstalled = WindowServiceManager.IsIntalled("TestService");

            Assert.True(isInstalled);
        }

        [Test()]
        public void UninstallTest()
        {
            WindowServiceManager.Uninstall("TestService");

            var isInstalled = WindowServiceManager.IsIntalled("TestService");

            Assert.False(isInstalled);
        }
    }
}