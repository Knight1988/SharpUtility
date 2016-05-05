using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SharpUtility.WinForm;

namespace SharpUtility.Core.Tests.WinForm
{
    [TestFixture]
    public class FormExtensionTests
    {
        private FormTest _form;

        [SetUp]
        public void Setup()
        {
            _form = new FormTest();
        }

        [Test]
        public void InvokeIfRequiredTest()
        {
            /* Act */
            var text = GetText();

            /* Assert */
            Assert.AreEqual("Test OK", text);
        }

        [Test]
        public async Task InvokeIfRequiredAsync()
        {
            /* Act */
            var text = await GetTextAsync();

            /* Assert */
            Assert.AreEqual("Test OK", text);
        }

        private async Task<string> GetTextAsync()
        {
            await Task.FromResult(true);
            return _form.InvokeIfRequired(p => p.txtTextBox.Text);
        }

        private string GetText()
        {
            return _form.InvokeIfRequired(p => p.txtTextBox.Text);
        }
    }
}
