using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using SharpUtility.String;

namespace SharpUtility.Core.Net4.Tests.String
{
    [TestFixture]
    public class StringInjectExtensionTests
    {
        [Test]
        public void InjectDictionaryTest()
        {
            /* Arrange */
            const string template = "{Prefix}{FirstName} {LastName}";
            var dic = new Dictionary<string, string>
            {
                {"Prefix", "Mr."},
                {"FirstName", "Squall"},
                {"LastName", "Leonhart"}
            };

            /* Act */
            var actual = template.Inject(dic);

            /* Assert */
            actual.Should().Be("Mr.Squall Leonhart");
        }

        [Test]
        public void InjectSingleValueTest()
        {
            /* Arrange */
            const string template = "{Prefix}{FirstName} {LastName}";

            /* Act */
            var actual = template.Inject("Prefix", "Mr.").Inject("FirstName", "Squall").Inject("LastName", "Leonhart");

            /* Assert */
            actual.Should().Be("Mr.Squall Leonhart");
        }
    }
}