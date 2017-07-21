using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Core.Repository.Tests
{
    [TestClass]
   
    public class MongoConfigTests
    {
        [TestMethod]
        [TestInitialize()]
        public void Core_Connection_String_And_MonboDB_Connection_Success()
        {
            //Arrange

            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MongoDb.ConnectionString"].ToString();
            Assert.IsNotNull(connectionString);
            Assert.AreNotEqual(connectionString, string.Empty);

            var client = new MongoDB.Driver.MongoClient(connectionString);
            MongoDB.Driver.MongoUrl mongoUrl = MongoDB.Driver.MongoUrl.Create(connectionString);

            //Act

            var testCollection = "TestCollection_" + DateTime.Now.ToString();
            client.GetDatabase(mongoUrl.DatabaseName).CreateCollection(testCollection);

            //Assert
            Assert.IsTrue(true);
        }
        
    }
}
