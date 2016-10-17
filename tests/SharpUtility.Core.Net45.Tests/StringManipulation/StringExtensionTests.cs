using System;
using FluentAssertions;
using NUnit.Framework;
using SharpUtility.StringManipulation;

namespace SharpUtility.Core.Tests.StringManipulation
{
    [TestFixture]
    public class StringExtensionTests
    {
        [TestCase(20, "It's a text for counting of words, with different word boundaries and hyphenated word like the all-clear.Is it OK? ")]
        public void WordCountByListedCharsTest(int countExpected, string text)
        {
            /* Arrange */

            /* Act */
            var count = text.WordCountByListedChars();

            /* Assert */
            count.Should().Be(countExpected);
        }

        [TestCase(22, "It's a text for counting of words, with different word boundaries and hyphenated word like the all-clear.Is it OK? ")]
        public void WordCountByAlphaNumericTest(int countExpected, string text)
        {
            /* Arrange */

            /* Act */
            var count = text.WordCountByAlphaNumeric();

            /* Assert */
            count.Should().Be(countExpected);
        }

        [TestCase("Hello there", "THERE", StringComparison.OrdinalIgnoreCase)]
        public void ContaintsTest(string testString, string contain, StringComparison comparison)
        {
            /* Arrange */

            /* Act */
            var isContain = testString.Contains("THERE", StringComparison.OrdinalIgnoreCase);

            /* Assert */
            isContain.Should().BeTrue();
        }

        [TestCase("   Hello     there      ", "Hello there")]
        public void TrimSpacesTest(string text, string expected)
        {
            /* Arrange */

            /* Act */
            var actual = text.TrimSpaces();

            /* Assert */
            actual.Should().Be(expected);
        }
    }
}
