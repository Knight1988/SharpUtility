using NUnit.Framework;
using SharpUtility.Core.String;

namespace SharpUtility.Core.Tests.String
{
    [TestFixture()]
    public class StringExtensionsTests
    {
        [Test()]
        public void WordCountByListedCharsTest()
        {
            string groupOfWords = @"It's a text for counting of words, with different word " +
                           "boundaries and hyphenated word like the all-clear.Is it OK? ";
            Assert.AreEqual(20, groupOfWords.WordCountByListedChars());
        }

        [Test()]
        public void WordCountByAlphaNumericTest()
        {
            string groupOfWords = @"It's a text for counting of words, with different word " +
                           "boundaries and hyphenated word like the all-clear.Is it OK? ";
            Assert.AreEqual(22, groupOfWords.WordCountByAlphaNumeric());
        }
    }
}
