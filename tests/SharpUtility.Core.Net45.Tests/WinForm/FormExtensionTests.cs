using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpUtility.WinForm;

namespace SharpUtility.Core.Tests.WinForm
{
    [TestFixture]
    public class FormExtensionTests
    {
        [Test]
        public void InvokeIfRequiredTest()
        {
            /* Arrange */
            var form = new FormTest();

            /* Act */
            var text = form.InvokeIfRequired(p => p.txtTextBox.Text);

            /* Assert */
            Assert.AreEqual("Test OK", text);
        }

        [Test]
        public async Task InvokeIfRequiredAsync()
        {
            /* Arrange */
            var form = new FormTest();

            /* Act */
            var text = await form.InvokeIfRequired(GetTextBox);

            /* Assert */
            Assert.AreEqual("Test OK", text);
        }

        private async Task<string> GetTextBox(FormTest formTest)
        {
            await Task.Yield();
            return formTest.txtTextBox.Text;
        }
    }
}
