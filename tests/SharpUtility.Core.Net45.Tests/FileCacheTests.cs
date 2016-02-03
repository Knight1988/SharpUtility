using System;
using NUnit.Framework;
namespace SharpUtility.Runtime.Caching.Tests
{
    [TestFixture()]
    public class FileCacheTests
    {
        [Test()]
        public void FileCacheTest()
        {
            var fileCache = new SimpleFileCache("Test");
            fileCache.Add("Test", 1, DateTime.Now + new TimeSpan(1, 0, 0));

            var objActual = fileCache.Get("Test");
            Assert.AreEqual(1, objActual);

            fileCache.Remove("Test");
            objActual = fileCache.Get("Test");
            Assert.AreEqual(null, objActual);
        }

        [Test]
        public void ByteTest()
        {
            var fileCache = new SimpleFileCache("Test");

            var expected = new byte[]
            {
                1, 2, 3, 4
            };
            fileCache.Add("Test byte", expected, DateTime.Now + new TimeSpan(1, 0, 0));
            var objActual = fileCache.Get<byte[]>("Test byte");
            Assert.AreEqual(expected, objActual);
        }
    }
}
