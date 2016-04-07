using System;
using NUnit.Framework;
using SharpUtility.Common;

namespace SharpUtility.Core.Tests
{
    [TestFixture]
    class CodeConfigurationTests
    {
        [Test]
        public void ExecuteActionTest()
        {
            // Arrange
            var config = new CodeConfiguration();
            var num = 0;

            // Act
            config.Execute(() => { num++; });

            // Assert
            Assert.AreEqual(1, num);
        }

        [Test]
        public void ExecuteActionRetryTest()
        {
            // Arrange
            var config = new CodeConfiguration();
            var num = 0;
            // Act
            config.Execute(() =>
            {
                num++;
                throw new Exception();
            }, e =>
            {
                num++;
            });

            // Assert
            Assert.AreEqual(4, num);
        }

        [Test]
        public void ExecuteFuncRetryTest()
        {
            // Arrange
            var config = new CodeConfiguration();
            var num = 0;
            // Act
            var actual = config.Execute(() =>
            {
                num++;
                throw new Exception();
            }, e =>
            {
                num++;
                return num;
            });

            // Assert
            Assert.AreEqual(4, actual);
        }

        [Test]
        public void ExecuteFuncTest()
        {
            // Arrange
            var config = new CodeConfiguration();

            // Act
            var result = config.Execute(() => true);

            // Assert
            Assert.True(result);
        }
    }
}
