using System.Linq;
using System.Reflection;
using NUnit.Framework;
using SharpUtility.Reflection;

namespace SharpUtility.Core.Tests.Reflection
{
    [TestFixture]
    class AssemblyTests
    {
        [Test]
        public void GetInheritedTypesTest()
        {
            // Arrange
            var assembly = Assembly.GetAssembly(typeof (Delivery));

            // Act
            var inheriteds = assembly.GetInheritedTypes(typeof (Base));

            // Assert
            Assert.True(inheriteds.Contains(typeof(Delivery)));
        }

        [Test]
        public void GetInheritedFromInterfaceTest()
        {
            // Arrange
            var assembly = Assembly.GetAssembly(typeof(Delivery));

            // Act
            var inheriteds = assembly.GetInheritedTypes(typeof(IBase));

            // Assert
            Assert.True(inheriteds.Contains(typeof(Delivery)));
        }
    }

    public interface IBase
    {
        
    }

    public class Base : IBase
    {
         
    }

    public class Delivery : Base
    {
    }
}
