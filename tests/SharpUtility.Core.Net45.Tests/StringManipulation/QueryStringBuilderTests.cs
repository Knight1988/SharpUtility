using FluentAssertions;
using NUnit.Framework;
using SharpUtility.StringManipulation;

namespace SharpUtility.Core.Tests.StringManipulation
{
    [TestFixture]
    public class QueryStringBuilderTests
    {
        [TestCase("?t=test+test&tl=en-US&sv=g1&vn=rjs&pitch=0.5&rate=0.5&vol=1", "t", "test test", "tl", "en-US", "sv",
             "g1", "vn", "rjs", "pitch", 0.5, "rate", 0.5, "vol", 1)]
        public void Test(string expected, params object[] pairs)
        {
            /* Arrage */
            var queryBuilder = new QueryStringBuilder();

            /* Act */
            for (var i = 0; i < pairs.Length; i += 2)
            {
                var key = pairs[i].ToString();
                var value = pairs[i + 1];
                queryBuilder.Add(key, value);
            }

            /* Assert */
            queryBuilder.ToString().Should().Be(expected);
        }
    }
}