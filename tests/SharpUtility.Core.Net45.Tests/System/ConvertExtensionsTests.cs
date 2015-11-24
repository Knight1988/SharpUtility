using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpUtility.Core;
using NUnit.Framework;

namespace SharpUtility.Core.Tests
{
    [TestFixture()]
    public class ConvertExtensionsTests
    {
        [Test()]
        public void ListTryParseTest()
        {
            var list1 = new List<string>(){"asd"};
            var list2 = ((object)list1).TryParse(new List<string>());
            //var list2 = new List<string>() { "qwe" };
            var actual = list1.Except(list2);
            Assert.AreEqual(0, actual.Count());
        }

        [Test()]
        public void DateTimeTryParseTest()
        {
            var date1 = new DateTime(2000, 1, 1);
            var date2 = ((object)date1).TryParse(DateTime.Now);
            Assert.AreEqual(0, date2.CompareTo(date1));
        }
    }
}
