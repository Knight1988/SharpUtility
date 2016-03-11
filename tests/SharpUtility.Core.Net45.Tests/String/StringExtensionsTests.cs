using System;
using System.Linq;
using NUnit.Framework;
using SharpUtility.String;

namespace SharpUtility.Core.Tests.String
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        public void WordCountByListedCharsTest()
        {
            string groupOfWords = @"It's a text for counting of words, with different word " +
                           "boundaries and hyphenated word like the all-clear.Is it OK? ";
            Assert.AreEqual(20, groupOfWords.WordCountByListedChars());
        }

        [Test]
        public void WordCountByAlphaNumericTest()
        {
            string groupOfWords = @"It's a text for counting of words, with different word " +
                           "boundaries and hyphenated word like the all-clear.Is it OK? ";
            Assert.AreEqual(22, groupOfWords.WordCountByAlphaNumeric());
        }

        [Test]
        public void ContaintsTest()
        {
            var s = "Hello there";
            Assert.True(s.Contains("THERE", StringComparison.OrdinalIgnoreCase));
        }
    }
}
