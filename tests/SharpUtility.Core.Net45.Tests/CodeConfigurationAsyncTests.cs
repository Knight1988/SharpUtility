using System;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpUtility.Common;

namespace SharpUtility.Core.Tests
{
    [TestFixture]
    class CodeConfigurationAsyncTests
    {
        [Test]
        public async Task ExecuteFunctionTest()
        {
            // Act
            var num = await CodeConfiguration.Try(async () => await Task.FromResult(1), 0, 0).ExecuteAsync();

            // Assert
            Assert.AreEqual(1, num);
        }

        [Test]
        public async Task ExecuteFunctionThrowTest()
        {
            // Act
            var num = await CodeConfiguration.Try(async () =>
            {
                throw new Exception("Test");
#pragma warning disable 162
                return await Task.FromResult(1);
#pragma warning restore 162
            }, 0, 0)
            .Catch<Exception>(async e => await Task.FromResult(2), 2).ExecuteAsync();

            // Assert
            Assert.AreEqual(2, num);
        }

        [Test]
        public async Task ExecuteActionAsyncTest()
        {
            // Arrange
            var num = 0;

            // Act
            await CodeConfiguration.Try(async () =>
            {
                await Task.Yield();
                num++;
            }, 0, 0).ExecuteAsync();

            // Assert
            Assert.AreEqual(1, num);
        }

        [Test]
        public async Task ExecuteActionRetryTest()
        {
            // Arrange
            var num = 0;
                // Act
            await CodeConfiguration.Try(async () =>
            {
                await Task.Yield();
                num++;
                throw new Exception("Test");
            }, 0, 0).Catch<Exception>(async e =>
            {
                await Task.Yield();
            }, 2).ExecuteAsync();

            // Assert
            Assert.AreEqual(3, num);
        }
    }
}
