using NUnit.Framework;
using System;
using System.Threading.Tasks;
using SharpUtility.Runtime.Remoting;

namespace SharpUtility.Tests
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