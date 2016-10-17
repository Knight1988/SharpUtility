using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using SharpUtility.Enum;
using SharpUtility.StringManipulation;

namespace SharpUtility.Core.Tests.StringManipulation
{
    [TestFixture]
    public class RandomStringTests
    {
        [TestCase(CharLimit.LowerCase, CharLimit.UpperCase, CharLimit.Number)]
        [TestCase(CharLimit.UpperCase, CharLimit.LowerCase, CharLimit.Number)]
        [TestCase(CharLimit.Number, CharLimit.UpperCase, CharLimit.LowerCase)]
        public void BasicTest(CharLimit limit, CharLimit notContain1, CharLimit notContain2)
        {
            var rd = new RandomString();
            rd.SetCharLimit(limit);

            var str = rd.Next(10);

            str.Should().MatchRegex($"[{limit.GetStringValue()}]")
                .And.NotMatchRegex(notContain1.GetStringValue())
                .And.NotMatchRegex(notContain2.GetStringValue());
        }

        [TestCase(CharLimit.LowerCase, CharLimit.UpperCase, CharLimit.Number)]
        [TestCase(CharLimit.Number, CharLimit.UpperCase, CharLimit.LowerCase)]
        [TestCase(CharLimit.Number, CharLimit.LowerCase, CharLimit.UpperCase)]
        public void CombineTest(CharLimit limit1, CharLimit limit2, CharLimit notContain)
        {
            var rd = new RandomString();
            rd.SetCharLimit(limit1 | limit2);

            var str = rd.Next(10);

            str.Should().MatchRegex($"[{limit1.GetStringValue()}{limit2.GetStringValue()}]")
                .And.NotMatchRegex(notContain.GetStringValue());
        }
    }
}