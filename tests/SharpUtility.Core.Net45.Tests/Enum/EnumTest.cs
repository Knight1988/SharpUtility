using NUnit.Framework;
using SharpUtility.Enum;

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
