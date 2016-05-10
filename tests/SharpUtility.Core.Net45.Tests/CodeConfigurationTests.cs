using System;
using NUnit.Framework;
using SharpUtility.Common;

namespace SharpUtility.Core.Tests
{
    [TestFixture]
    class CodeConfigurationTests
    {
        [Test]
        public void ExecuteFunctionTest()
        {
            // Act
            var num = CodeConfiguration.Try(() => 1, 0).Execute();

            // Assert
            Assert.AreEqual(1, num);
        }

        [Test]
        public void ExecuteFunctionThrowTest()
        {
            // Act
            var num = CodeConfiguration.Try(() =>
            {
                throw new Exception("Test");
#pragma warning disable 162
                return 1;
#pragma warning restore 162
            }, 0)
            .Catch<Exception>(e => 2, 2).Execute();

            // Assert
            Assert.AreEqual(2, num);
        }

        [Test]
        public void ExecuteActionTest()
        {
            // Arrange
            var num = 0;

            // Act
            CodeConfiguration.Try(() => { num++; }).Execute();

            // Assert
            Assert.AreEqual(1, num);
        }

        [Test]
        public void ExecuteActionThrowTest()
        {
            // Arrange
            var num = 0;

            // Act
            CodeConfiguration.Try(() =>
            {
                num++;
                throw new Exception("Test");
            }).Catch<Exception>(e =>{}, 2)
            .Execute();

            // Assert
            Assert.AreEqual(3, num);
        }
    }
}
