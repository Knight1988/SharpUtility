using NUnit.Framework;
using SharpUtility.IO;

namespace SharpUtility.Core.Tests.IO
{
    [TestFixture]
    [Category("CloundIgnore")]
    public class FileTests
    {
        [Test]
        public void UnlockerLocationTest()
        {
            var location = File.UnlockerLocation;
            Assert.AreEqual("C:\\Program Files\\Unlocker\\Unlocker.exe", location);
            //Assert.AreEqual(string.Empty, location);
        }
    }
}
