using NUnit.Framework;
using SharpUtility.Core.IO;

namespace SharpUtility.Core.Tests.IO
{
    [TestFixture()]
    public class FileTests
    {
        [Test()]
        public void UnlockerLocationTest()
        {
            var location = File.UnlockerLocation;
            Assert.AreEqual("C:\\Program Files\\Unlocker\\Unlocker.exe", location);
        }
    }
}
