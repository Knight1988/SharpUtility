using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpUtility.Core.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace SharpUtility.Core.Net.Tests
{
    [TestClass()]
    public class FormDataTests
    {
        [TestMethod()]
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
