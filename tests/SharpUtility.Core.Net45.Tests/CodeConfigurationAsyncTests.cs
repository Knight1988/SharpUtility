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
        public async Task ExecuteActionTest()
        {
            // Arrange
            var config = new CodeConfiguration();
            var num = 0;

            // Act
            await config.ExecuteAsync(() => { num++; });

            // Assert
            Assert.AreEqual(1, num);
        }

        [Test]
        public async Task ExecuteActionAsyncTest()
        {
            // Arrange
            var config = new CodeConfiguration();
            var num = 0;

            // Act
            await config.ExecuteAsync(async () =>
            {
                await Task.Yield();
                num++;
            });

            // Assert
            Assert.AreEqual(1, num);
        }

        [Test]
        public async Task ExecuteActionRetryTest()
        {
            // Arrange
            var config = new CodeConfiguration();
            var num = 0;
            // Act
            await config.ExecuteAsync(() =>
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
        public async Task ExecuteActionAsyncRetryTest()
        {
            // Arrange
            var config = new CodeConfiguration();
            var num = 0;
            // Act
            await config.ExecuteAsync(async () =>
            {
                await Task.Yield();
                num++;
                throw new Exception();
            }, async e =>
            {
                await Task.Yield();
                num++;
            });

            // Assert
            Assert.AreEqual(4, num);
        }

        [Test]
        public async Task ExecuteFuncRetryTest()
        {
            // Arrange
            var config = new CodeConfiguration();
            var num = 0;
            // Act
            var actual = await config.ExecuteAsync(() =>
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
        public async Task ExecuteFuncAsyncRetryTest()
        {
            // Arrange
            var config = new CodeConfiguration();
            var num = 0;
            // Act
            var actual = await config.ExecuteAsync(async () =>
            {
                await Task.Yield();
                num++;
                throw new Exception();
            }, async e =>
            {
                await Task.Yield();
                num++;
                return num;
            });

            // Assert
            Assert.AreEqual(4, actual);
        }

        [Test]
        public async Task ExecuteFuncTest()
        {
            // Arrange
            var config = new CodeConfiguration();

            // Act
            var result = await config.ExecuteAsync(() => true);

            // Assert
            Assert.True(result);
        }

        [Test]
        public async Task ExecuteFuncAsyncTest()
        {
            // Arrange
            var config = new CodeConfiguration();

            // Act
            var result = await config.ExecuteAsync(async () =>
            {
                await Task.Yield();
                return true;
            });

            // Assert
            Assert.True(result);
        }
    }
}
