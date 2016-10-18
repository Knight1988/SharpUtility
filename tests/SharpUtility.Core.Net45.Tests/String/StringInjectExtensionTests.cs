using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using SharpUtility.String;

namespace SharpUtility.Core.Tests.String
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
        public void InjectManyObjectTest()
        {
            /* Arrange */
            const string template = "{Prefix}{FirstName} {LastName}";
            var obj1 = new {FirstName = "Squall"};
            var obj2 = new {Prefix = "Mr.", LastName = "Leonhart"};

            /* Act */
            var actual = template.Inject(obj1, obj2);

            /* Assert */
            actual.Should().Be("Mr.Squall Leonhart");
        }

        [Test]
        public void InjectObjectTest()
        {
            /* Arrange */
            const string template = "{Prefix}{FirstName} {LastName}";
            var obj = new
            {
                Prefix = "Mr.",
                FirstName = "Squall",
                LastName = "Leonhart"
            };

            /* Act */
            var actual = template.Inject(obj);

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