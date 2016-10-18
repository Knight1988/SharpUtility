using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpUtility.Reflection;

namespace SharpUtility.Core.Tests.Reflection
{
    [TestFixture]
    public class ObjectExtensionTests
    {
        [Test]
        public void GetPrivateFieldValueTest()
        {
            /* Arrange */
            var obj = new TestClass();

            /* Act */
            var actual = obj.GetPrivateFieldValue<int>("_privateField");

            /* Assert */
            Assert.AreEqual(1, actual);
        }

        [Test]
        public void GetSubClassPrivateFieldValueTest()
        {
            /* Arrange */
            var obj = new MySubClass();

            /* Act */
            var actual = obj.GetPrivateFieldValue<int>("_privateField");

            /* Assert */
            Assert.AreEqual(1, actual);
        }

        [Test]
        public void GetPrivatePropertyValueTest()
        {
            /* Arrange */
            var obj = new TestClass();

            /* Act */
            var actual = obj.GetPrivatePropertyValue<int>("PrivateProperty");

            /* Assert */
            Assert.AreEqual(2, actual);
        }

        [Test]
        public void GetSubClassPrivatePropertyValueTest()
        {
            /* Arrange */
            var obj = new MySubClass();

            /* Act */
            var actual = obj.GetPrivatePropertyValue<int>("PrivateProperty");

            /* Assert */
            Assert.AreEqual(2, actual);
        }

        [Test]
        public void SetPrivateFieldValueTest()
        {
            /* Arrange */
            var obj = new TestClass();

            /* Act */
            obj.SetPrivateFieldValue("_privateField", 3);
            var actual = obj.GetPrivateFieldValue<int>("_privateField");

            /* Assert */
            Assert.AreEqual(3, actual);
        }

        [Test]
        public void SetSubClassPrivateFieldValueTest()
        {
            /* Arrange */
            var obj = new MySubClass();

            /* Act */
            obj.SetPrivateFieldValue("_privateField", 3);
            var actual = obj.GetPrivateFieldValue<int>("_privateField");

            /* Assert */
            Assert.AreEqual(3, actual);
        }

        [Test]
        public void SetPrivatePropertyValueTest()
        {
            /* Arrange */
            var obj = new TestClass();

            /* Act */
            obj.SetPrivatePropertyValue("PrivateProperty", 4);
            var actual = obj.GetPrivatePropertyValue<int>("PrivateProperty");

            /* Assert */
            Assert.AreEqual(4, actual);
        }

        [Test]
        public void SetSubClassPrivatePropertyValueTest()
        {
            /* Arrange */
            var obj = new MySubClass();

            /* Act */
            obj.SetPrivatePropertyValue("PrivateProperty", 4);
            var actual = obj.GetPrivatePropertyValue<int>("PrivateProperty");

            /* Assert */
            Assert.AreEqual(4, actual);
        }

        private class TestClass
        {
#pragma warning disable 169
            // ReSharper disable once UnusedMember.Local
            private int PrivateProperty { get; set; } = 2;
            private int _privateField = 1;
#pragma warning restore 169
        }

        private class MySubClass : TestClass
        {
        }
    }
}
