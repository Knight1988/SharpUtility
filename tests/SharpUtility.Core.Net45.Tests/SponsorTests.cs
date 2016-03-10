using System;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpUtility.Runtime.Remoting;

namespace SharpUtility.Core.Tests
{
    [TestFixture()]
    public class SponsorTests : MarshalByRefObject
    {
        public static bool Called { get; set; }

        [Test()]
        public async Task SponsorTest()
        {
            Called = false;
            Activator.CreateInstance(typeof (SponsorImplement));
            GC.Collect();
            await Task.Delay(1000);
            GC.Collect();
            await Task.Delay(1000);
            Assert.True(Called);
        }
    }

    public class SponsorImplement : Sponsor
    {
        ~SponsorImplement()
        {
            SponsorTests.Called = true;
        }
    }
}