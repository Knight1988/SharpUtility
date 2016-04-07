using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpUtility.Common;
using Telerik.JustMock;

namespace SharpUtility.Core.Tests
{
    [TestFixture]
    class JobCounterTests
    {
        [Test]
        public void TestJob()
        {
            /* Arrange */
            var jobCoutner = Mock.Create<JobCounter>(Behavior.CallOriginal, 100);
            string lastOutput = null;
            Mock.Arrange(() => jobCoutner.DisplayToConsole()).DoInstead(() =>
            {
                var value = jobCoutner.Value / jobCoutner.MaxValue;
                var output = $"{value:P}";
                lastOutput = output;
            });

            /* Act */
            jobCoutner.IncreaseValue().DisplayToConsole();

            /* Assert */
            Assert.AreEqual("1.00 %", lastOutput);
        }
    }
}
