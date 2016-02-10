using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpUtility.Core.Enum;

namespace SharpUtility.Core.Tests.Enum
{
    [TestFixture]
    class EnumTest
    {
        [Test]
        public void GetStringValueTest()
        {
            Assert.AreEqual("Test1", TestEnum.Test1.GetStringValue());
        }
    }
}
