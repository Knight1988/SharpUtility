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
        [Test]
        public async Task MouseClickTests()
        {
            /* Arrange */
            var handle = WindowFinder.FindWindow(null, "Genymotion for personal use - Google Galaxy Nexus - 4.2.2 - API 17 - 720x1280 v2.6 (720x1280, 320dpi) - 192.168.56.101");

            /* Atc */
            await MouseSimulator.ClickAsync(handle, MouseButton.Left, 50, 50);
        }
    }
}
