using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using SharpUtility.Common;

namespace SharpUtility.Core.Tests.Common
{
    [TestFixture]
    public class ConvertExtensionsTests
    {
        [Test]
        public void ListTryParseTest()
        {
            var list1 = new List<string> {"asd"};
            //var list2 = ((object)list1).TryParse(new List<string>());
            //var list2 = new List<string>() { "qwe" };
            //var actual = list1.Except(list2);
            //Assert.AreEqual(0, actual.Count());
        }

        [Test]
        public void DateTimeTryParseTest()
        {
            var date1 = new DateTime(2000, 1, 1);
            //var date2 = ((object)date1).TryParse(DateTime.Now);
            //Assert.AreEqual(0, date2.CompareTo(date1));
        }

        [Test]
        [TestCase(1302203200000, "4/7/2011")]
        [TestCase(1463131655693, "5/13/2016")]
        [TestCase(1302134400000, "4/7/2011")]
        [TestCase(1463097600000, "5/13/2016")]
        public void JsTimeTicksToDateTimeTest(long jsTimeTicks, string date)
        {
            /* Act */
            var actual = jsTimeTicks.JsTimeTicksToDateTime().Date;
            var expected = DateTime.Parse(date);

            /* Assert */
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(1302134400000, "4/7/2011")]
        [TestCase(1463097600000, "5/13/2016")]
        public void ToJsTimeTicksTest(long jsTimeTicks, string date)
        {
            /* Act */
            var dateTime = DateTime.Parse(date);
            var actual = dateTime.ToJsTimeTicks();
            var expected = jsTimeTicks;

            /* Assert */
            Assert.AreEqual(expected, actual);
        }
    }
}
