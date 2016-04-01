using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SharpUtility.Core.Tests
{
    [TestFixture]
    class JobCounterTests
    {
        [Test]
        public void TestJob()
        {
            /* Arrange */
            var jobCoutner = new JobCounter(100);

            /* Act */
            jobCoutner.IncreaseValue().DisplayToConsole();

            /* Assert */
            Assert.AreEqual("1.00%", jobCoutner.LastOutput);
        }
    }
}
