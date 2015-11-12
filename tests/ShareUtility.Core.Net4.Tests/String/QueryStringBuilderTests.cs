using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpUtility.Core.String;
using NUnit.Framework;
namespace SharpUtility.Core.String.Tests
{
    [TestFixture()]
    public class QueryStringBuilderTests
    {
        [Test()]
        public void ToStringTest()
        {
            var sb = new QueryStringBuilder
            {
                {"t", "test test"},
                {"tl", "en-US"},
                {"sv", "g1"},
                {"vn", "rjs"},
                {"pitch", 0.5},
                {"rate", 0.5},
                {"vol", 1}
            };
            const string expected = "?t=test+test&tl=en-US&sv=g1&vn=rjs&pitch=0.5&rate=0.5&vol=1";
            Assert.AreEqual(expected, sb.ToString());
        }
    }
}
