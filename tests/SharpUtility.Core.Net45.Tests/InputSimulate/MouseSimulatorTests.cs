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
        public Task MouseClickTests()
        {
            /* Atc */
            await MouseSimulator.ClickAsync()
        }
    }
}
