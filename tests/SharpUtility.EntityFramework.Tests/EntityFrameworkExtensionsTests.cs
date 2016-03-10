using NUnit.Framework;
using System.Configuration;
using System.Linq;

namespace SharpUtility.EntityFramework.Tests
{
    [TestFixture()]
    public class EntityFrameworkExtensionsTests
    {
        protected string metaData = "res://*/AdventureWorksModel.csdl|res://*/AdventureWorksModel.ssdl|res://*/AdventureWorksModel.msl";

        [Test()]
        public void ToEntityConnectionTest()
        {
            // Arrange
            var db = new Entities(ConfigurationManager.ConnectionStrings["AdventureWorkConnection"].ConnectionString.ToEntityFrameworkConnectionString(metaData));
            
            // Act
            var actual = db.AWBuildVersions.Any();

            // Assert
            Assert.True(actual);
        }
    }
}