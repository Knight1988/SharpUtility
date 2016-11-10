using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SharpUtility.Core.Tests
{
    [TestFixture()]
    public class SponsorTests : MarshalByRefObject
    {
        public static bool Called { get; set; }

        [Test(), Ignore("Need better solution")]
        public async Task SponsorTest()
        {
            Called = false;
            Activator.CreateInstance(typeof (SponsorImplement));
            GC.Collect();
            //await Task.Delay(1000);
            await Task.Delay(5*61*1000);// 5m 1s
            GC.Collect();
            await Task.Delay(1000);
            Assert.True(Called);
        }
    }

    public class SponsorImplement
    {
        ~SponsorImplement()
        {
            SponsorTests.Called = true;
        }
    }
}