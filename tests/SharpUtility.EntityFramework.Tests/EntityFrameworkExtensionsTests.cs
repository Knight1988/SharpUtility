using NUnit.Framework;
using System.Configuration;
using System.Linq;

namespace SharpUtility.EntityFramework.Tests
{
    [TestFixture()]
    public class EntityFrameworkExtensionsTests
    {
        protected string MetaData = "res://*/AdventureWorksModel.csdl|res://*/AdventureWorksModel.ssdl|res://*/AdventureWorksModel.msl";

        [SetUp]
        public void Setup()
        {
            var sql =
                @"RESTORE DATABASE [AdventureWorks2008R2] FROM  DISK = N'E:\SQL\BACKUP\AdventureWorks2008R2-Full Database Backup.bak' WITH  FILE = 1,  MOVE N'AdventureWorks2008R2_Data' TO N'E:\SQL\DATA\AdventureWorks2008R2.mdf',  MOVE N'AdventureWorks2008R2_Log' TO N'E:\SQL\DATA\AdventureWorks2008R2_1.LDF',  NOUNLOAD,  REPLACE,  STATS = 10";
        }

        [Test()]
        public void ToEntityConnectionTest()
        {
            // Arrange
            var db = new Entities(ConfigurationManager.ConnectionStrings["AdventureWorkConnection"].ConnectionString.ToEntityFrameworkConnectionString(MetaData));
            
            // Act
            var actual = db.AWBuildVersions.Any();

            // Assert
            Assert.True(actual);
        }
    }
}