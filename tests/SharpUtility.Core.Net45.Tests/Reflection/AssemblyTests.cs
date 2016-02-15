using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using NUnit.Framework;
using SharpUtility.Core.PCL.Reflection;

namespace SharpUtility.Core.Tests.Reflection
{
    [TestFixture]
    class AssemblyTests
    {
        [Test]
        public void GetInheritedTypesTest()
        {
            var assembly = Assembly.GetAssembly(typeof (Delivery));

            var inheriteds = assembly.GetInheritedTypes(typeof (Base));

            Assert.AreEqual(typeof(Delivery), inheriteds.First());
        }
    }

    public class Base
    {
         
    }

    public class Delivery : Base
    {
    }
}
