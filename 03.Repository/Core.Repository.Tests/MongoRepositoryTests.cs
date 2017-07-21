using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Repository.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessEntities.CoreEntity;

namespace Core.Repository.MongoDb.Tests
{
    [TestClass()]
    public class MongoRepositoryTests
    {
        private IRepository<Entity> _entityRepository;

        public MongoRepositoryTests()
        {
            this._entityRepository = new MongoRepository<Entity>(new MongoDB.Driver.MongoClient());
        }

        #region Get Method Tests
        
        [TestMethod]
        [TestCategory("Integration")]
        public async Task EntityRepo_Get_Valid_Entity_Success()
        {
            //Arrange

            var entity = new Entity();
            var insertedEntity = await this._entityRepository.Insert(entity);
            var entityResult = await this._entityRepository.Get(insertedEntity.Id);

            //Act

            var prds = await this._entityRepository.GetAll();


            //Assert

            Assert.AreEqual(entityResult.Id, insertedEntity.Id);
        }
        
        [TestMethod]
        [TestCategory("Integration")]
        public async Task EntityRepo_Get_NotExistingId_Entity_Success()
        {
            //Arrange
            var invalidId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

            //Act

            var entityResult = await this._entityRepository.Get(invalidId);

            //Assert
            Assert.IsNull(entityResult);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [ExpectedException(typeof(FormatException))]
        public async Task EntityRepo_Get_InvalidId_Entity_Success()
        {
            var entityResult = await this._entityRepository.Get("invalidId");
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task EntityRepo_Get_Null_Entity_Success()
        {
            //Act
            var entityResult = await this._entityRepository.Get(null);

            //Assert
            Assert.IsNull(entityResult);
        }

        #endregion

        #region Insert Method Tests
        [TestMethod]
        [TestCategory("Integration")]
        public async Task EntityRepo_Insert_Empty_Entity_Success()
        {
            //Arrange
            var entityResult = await this._entityRepository.Insert(new Entity());

            //Assert
            Assert.IsNotNull(entityResult);
            Assert.IsNotNull(entityResult.Id);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task EntityRepo_Insert_Null_Entity_Exception()
        {
            await this._entityRepository.Insert(null);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [ExpectedException(typeof(EntityDuplicateException))]
        public async Task EntityRepo_Insert_Duplicated_Entity_Exception()
        {
            //Arrange
            var entityId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            var entity = new Entity()
            {
                Id = entityId,
            };

            //Act
            await this._entityRepository.Insert(entity);
            await this._entityRepository.Insert(entity);
        }

        #endregion

        #region Update Method Tests
        [TestMethod]
        [TestCategory("Integration")]
        public async Task EntityRepo_Update_Valid_Entity_Success()
        {
            //Arrange
            var entity = new Entity();
            var insertedEntity = await this._entityRepository.Insert(entity);
            var entityResult = await this._entityRepository.Get(insertedEntity.Id);

            //Act
            var updatedEntity = await this._entityRepository.Update(entityResult);

            //Assert
            Assert.IsTrue(updatedEntity.Version > insertedEntity.Version);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [ExpectedException(typeof(EntityConflictException))]
        public async Task EntityRepo_Update_Version_Conflit_Entity_Exception()
        {
            //Arrange
            var entity = new Entity();

            //Acts
            var insertedEntity = await this._entityRepository.Insert(entity);
            var entityResult1 = await this._entityRepository.Get(insertedEntity.Id);
            var entityResult2 = await this._entityRepository.Get(insertedEntity.Id);

            var updatedEntity1 = await this._entityRepository.Update(entityResult1);
            var updatedEntity2 = await this._entityRepository.Update(entityResult2);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task EntityRepo_Update_Null_Entity_Exception()
        {
            //Act
            await this._entityRepository.Update(null);
        }

        #endregion

        #region Delete Method Tests
        [TestMethod]
        [TestCategory("Integration")]
        public async Task EntityRepo_Delete_Valid_Entity_Success()
        {
            //Arrange

            var entity = new Entity();
            var insertedEntity = await this._entityRepository.Insert(entity);
            var getEntity = await this._entityRepository.Get(insertedEntity.Id);

            //Assert
            Assert.IsNotNull(getEntity);
            Assert.AreEqual(getEntity.Id, insertedEntity.Id);

            var deletedEntity = await this._entityRepository.Delete(getEntity.Id);

            Assert.IsNotNull(deletedEntity);
            Assert.AreEqual(deletedEntity.Id, getEntity.Id);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task EntityRepo_Delete_NotExistingId_Format_Entity_Success()
        {

            //Arrange

            var invalidId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();


            //Act

            var deletedEntity = await this._entityRepository.Delete(invalidId);


            //Assert

            Assert.IsNull(deletedEntity);

        }

        [TestMethod]
        [TestCategory("Integration")]
        [ExpectedException(typeof(FormatException))]
        public async Task EntityRepo_Delete_Invalid_Id_Format_Entity_Success()
        {
            var deletedEntity = await this._entityRepository.Delete("InvalidEntityId");
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task EntityRepo_Delete_Null_Entity_Success()
        {
            //Arrange
            var deletedEntity = await this._entityRepository.Delete(null);

            //Assert
            Assert.IsNull(deletedEntity);
        }

        #endregion

    }
}