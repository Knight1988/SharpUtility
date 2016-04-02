using NUnit.Framework;
using SharpUtility.Net;

namespace SharpUtility.Core.Tests.Net
{
    [TestFixture()]
    public class FormDataTests
    {
        [Test]
        public void ToStringTest()
        {
            var formData = new FormData
            {
                {"text_to_transcribe", "You there"},
                {"submit", "Show transcription"},
                {"output_dialect", "am"},
                {"output_style", "only_tr"},
                {"weak_forms", "on"},
                {"preBracket", "["},
                {"postBracket", "]"}
            };
            const string expected = "text_to_transcribe=You+there&submit=Show+transcription&output_dialect=am&output_style=only_tr&weak_forms=on&preBracket=%5b&postBracket=%5d";
            string actual = formData.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}
