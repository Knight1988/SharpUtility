using NUnit.Framework;
using SharpUtility.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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