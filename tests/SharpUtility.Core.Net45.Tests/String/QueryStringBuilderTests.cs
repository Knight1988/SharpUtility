using NUnit.Framework;
using SharpUtility.Core.String;

namespace SharpUtility.Core.Tests.String
{
    [TestFixture]
    public class QueryStringBuilderTests
    {
        [Test]
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
