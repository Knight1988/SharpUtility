using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpUtility.Core.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace SharpUtility.Core.Math.Tests
{
    [TestClass()]
    public class MathExtensionsTests
    {
        [TestMethod()]
        public void IsOddTest()
        {
            Assert.AreEqual(true, ((byte)1).IsOdd());
            Assert.AreEqual(true, ((sbyte)1).IsOdd());
            Assert.AreEqual(true, ((short)1).IsOdd());
            Assert.AreEqual(true, ((ushort)1).IsOdd());
            Assert.AreEqual(true, 1.IsOdd());
            Assert.AreEqual(true, ((uint)1).IsOdd());
            Assert.AreEqual(true, ((long)1).IsOdd());
            Assert.AreEqual(true, ((ulong)1).IsOdd());
            Assert.AreEqual(true, ((float)1).IsOdd());
            Assert.AreEqual(true, ((double)1).IsOdd());
            Assert.AreEqual(true, ((decimal)1).IsOdd());
        }
        
        [TestMethod()]
        public void IsEvenTest()
        {
            Assert.AreEqual(true, ((byte)2).IsEven());
            Assert.AreEqual(true, ((sbyte)2).IsEven());
            Assert.AreEqual(true, ((short)2).IsEven());
            Assert.AreEqual(true, ((ushort)2).IsEven());
            Assert.AreEqual(true, 2.IsEven());
            Assert.AreEqual(true, ((uint)2).IsEven());
            Assert.AreEqual(true, ((long)2).IsEven());
            Assert.AreEqual(true, ((ulong)2).IsEven());
            Assert.AreEqual(true, ((float)2).IsEven());
            Assert.AreEqual(true, ((double)2).IsEven());
            Assert.AreEqual(true, ((decimal)2).IsEven());
        }
    }
}
