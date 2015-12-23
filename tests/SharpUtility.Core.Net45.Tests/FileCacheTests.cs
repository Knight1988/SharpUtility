using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpUtility.Runtime.Caching;
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
            fileCache.Add("Test", 1, DateTime.Now);
        }
    }
}
