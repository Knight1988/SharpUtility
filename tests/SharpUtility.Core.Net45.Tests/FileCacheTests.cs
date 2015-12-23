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
    }
}
