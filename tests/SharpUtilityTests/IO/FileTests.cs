using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpUtility.Core.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace SharpUtility.Core.IO.Tests
{
    [TestClass()]
    public class FileTests
    {
        [TestMethod()]
        public void UnlockerLocationTest()
        {
            var location = File.UnlockerLocation;
            Assert.AreEqual("C:\\Program Files\\Unlocker\\Unlocker.exe", location);
        }
    }
}
