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
        [Test, Ignore("JustMock required.")]
        public void TestJob()
        {
            /* Arrange */
            var jobCoutner = Mock.Create<JobCounter>(Behavior.CallOriginal, 100);
            jobCoutner.DisplayRemainingTime = false;
            string lastOutput = null;
            Mock.Arrange(() => jobCoutner.WriteToConsole()).DoInstead(() =>
            {
                var output = jobCoutner.ToString();
                lastOutput = lastOutput?.Remove(0, jobCoutner.LastOutput.Length);
                lastOutput = output;
            });

            /* Act */
            jobCoutner.IncreaseValue().WriteToConsole();

            /* Assert */
            Assert.AreEqual("1.00 %", lastOutput);
        }
    }
}
