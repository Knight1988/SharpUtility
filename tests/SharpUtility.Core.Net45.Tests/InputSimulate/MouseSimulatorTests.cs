using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using NUnit.Framework;
using SharpUtility.InputSimulate;

namespace SharpUtility.Core.Tests.InputSimulate
{
    [TestFixture]
    class MouseSimulatorTests
    {
        private string _windowTitle =
            "Genymotion for personal use - Google Nexus 5 - 6.0.0 - API 23 - 1080x1920 (1080x1920, 480dpi) - 192.168.56.101";

        [Test]
        public async Task MouseClickTests()
        {
            /* Arrange */
            var handle = WindowFinder.FindWindow(null, _windowTitle);

            /* Atc */
            await MouseSimulator.ClickAsync(handle, MouseButton.Left, 190, 400, 1, 250);
        }

        [Test]
        public async Task MouseClickDownDelayTests()
        {
            /* Arrange */
            var handle = WindowFinder.FindWindow(null, _windowTitle);

            /* Atc */
            await MouseSimulator.ClickAsync(handle, MouseButton.Left, 50, 50, 1, 10, 1000);
        }

        [Test]
        public async Task MouseClickRepeatClickTests()
        {
            /* Arrange */
            var handle = WindowFinder.FindWindow(null, _windowTitle);

            /* Atc */
            await MouseSimulator.ClickAsync(handle, MouseButton.Left, 50, 500, 1, 10);
        }
    }
}
