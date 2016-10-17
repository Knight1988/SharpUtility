﻿using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpUtility.InputSimulate;

namespace SharpUtility.Core.Tests.InputSimulate
{
    [TestFixture, Category("CloudIgnore")]
    class MouseSimulatorTests
    {
        private const string WindowTitle = "Genymotion for personal use - Google Nexus 5";

        [Test]
        public async Task MouseClickTests()
        {
            /* Arrange */
            var window = WindowList.GetAllWindows().Single(p => p.Caption.Contains(WindowTitle));
            var handle = window.Handle;

            /* Atc */
            await MouseSimulator.ClickAsync(handle, MouseButton.Left, 190, 400, 1, 250);
        }

        [Test]
        public async Task MouseClickDownDelayTests()
        {
            /* Arrange */
            var window = WindowList.GetAllWindows().Single(p => p.Caption.Contains(WindowTitle));
            var handle = window.Handle;

            /* Atc */
            await MouseSimulator.ClickAsync(handle, MouseButton.Left, 50, 50, 1, 10, 1000);
        }

        [Test]
        public async Task MouseClickRepeatClickTests()
        {
            /* Arrange */
            var window = WindowList.GetAllWindows().Single(p => p.Caption.Contains(WindowTitle));
            var handle = window.Handle;

            /* Atc */
            await MouseSimulator.ClickAsync(handle, MouseButton.Left, 50, 500, 5, 250);
        }

        [Test]
        public async Task MouseDownUpTests()
        {
            /* Arrange */
            var window = WindowList.GetAllWindows().Single(p => p.Caption.Contains(WindowTitle));
            var handle = window.Handle;

            /* Atc */
            MouseSimulator.Down(handle, MouseButton.Left, 50, 500);
            await Task.Delay(100);
            MouseSimulator.Up(handle, MouseButton.Left, 50, 500);
        }
    }
}
