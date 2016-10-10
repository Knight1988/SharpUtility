using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NUnit.Framework;
using SharpUtility.InputSimulate;

namespace SharpUtility.Core.Tests.InputSimulate
{
    [TestFixture]
    public class KeyboardSimulatorTests
    {
        private const string WindowTitle = "Genymotion for personal use - Google Nexus 5";

        [Test]
        public void KeyboardTest()
        {
            /* Arrange */
            var window = WindowList.GetAllWindows().Single(p => p.Caption.Contains(WindowTitle));
            var handle = window.Handle;

            /* Atc */
            KeyboardSimulator.Up(handle, Keys.A);
            KeyboardSimulator.Up(handle, Keys.S);
            KeyboardSimulator.Up(handle, Keys.D);
        }
    }
}
