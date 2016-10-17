using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpUtility.StringManipulation;

namespace SharpUtility.Core.Tests.StringManipulation
{
    [TestFixture]
    public class Base64Tests
    {
        [TestCase("Hello", "SGVsbG8=")]
        [TestCase("Hello World", "SGVsbG8gV29ybGQ=")]
        public void DecodeTest(string str, string encoded)
        {
            /* Act */
            var decoded = Base64.Base64Decode(encoded);

            /* Assert */
            Assert.AreEqual(str, decoded);
        }
    }
}
