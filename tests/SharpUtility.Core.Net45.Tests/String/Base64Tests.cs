using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using SharpUtility.String;

namespace SharpUtility.Core.Tests.String
{
    [TestFixture]
    public class Base64Tests
    {
        [TestCase("Hello", "SGVsbG8=")]
        [TestCase("Hello World", "SGVsbG8gV29ybGQ=")]
        public void DecodeTest(string str, string encoded)
        {
            /* Act */
            var actual = Base64.Decode(encoded);

            /* Assert */
            actual.Should().Be(str);
        }

        [TestCase("Hello", "SGVsbG8=")]
        [TestCase("Hello World", "SGVsbG8gV29ybGQ=")]
        public void EncodeTest(string str, string encoded)
        {
            /* Act */
            var actual = Base64.Encode(str);

            /* Assert */
            actual.Should().Be(encoded);
        }
    }
}
