using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

using CORE_REPO = Core.Repository;
using BE = BusinessEntities;
using CORE_MONGO_REPO = Core.Repository.MongoDb;
using System;

namespace MongoDB.Service.Tests
{
    [TestClass()]
    public class DemandServiceTests
    {
        private CORE_REPO.IRepository<BE.Demand> _demandRepository;
        private IDemandService _demandService;

        private CORE_REPO.IRepository<BE.User> _userRepository;
        private CORE_REPO.IRepository<BE.Book> _bookRepository;


        public DemandServiceTests()
        {
            this._demandRepository = new CORE_MONGO_REPO.MongoRepository<BE.Demand>(new MongoDB.Driver.MongoClient());
            this._demandService = new DemandService(this._demandRepository);

            this._userRepository = new CORE_MONGO_REPO.MongoRepository<BE.User>(new MongoDB.Driver.MongoClient());
            this._bookRepository = new CORE_MONGO_REPO.MongoRepository<BE.Book>(new MongoDB.Driver.MongoClient());

        }

        #region Get Method Tests

        [TestMethod]
        [TestCategory("Integration")]
        public async Task DemandSvc_Get_All_Success()
        {
            //Arrange
            var demands = await this._demandRepository.GetAll();

            //Act
            var DemandResult = await _demandService.GetAll();

            //Assert
            Assert.AreEqual(DemandResult.Count(), demands.Count());
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task DemandSvc_Get_By_User_Success()
        {
            #region Arrange

            var user = new BE.User
            {
                Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                FirstName = "Test_DemandSvc_Get_By_User_Success_Firstname_" + DateTime.Now.ToString(),
                Surname = "Test_DemandSvc_Get_By_User_Success_Surname_" + DateTime.Now.ToString(),
                Email = "mail@user.com",
                UserName = "Test_DemandSvc_Get_By_User_Success_Username_" + DateTime.Now.ToString(),
                PhoneNumber = "Test_DemandSvc_Get_By_User_Success_PhoneNumber_" + DateTime.Now.ToString(),
            };

            var book = new BE.Book()
            {
                Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                Authors = new System.Collections.Generic.List<string>(),
                Title = "Test_DemandSvc_Get_By_User_Success_Title",
                Description = "Test_DemandSvc_Get_By_User_Success_Description",
                Publisher = "Test_DemandSvc_Get_By_User_Success_Publisher",
            };

            var demand = new BE.Demand()
            {
                Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                Book = book,
                User = user
            };

            //Create new demand
            demand = await _demandRepository.Insert(demand);

            Assert.IsNotNull(demand);

            #endregion

            #region Act

            //Find demands for created user
            var DemandResult = await _demandService.GetByUser(user.UserName);
            var demands = from T in await _demandRepository.GetAll()
                          where T.User.UserName == user.UserName
                          select T;

            #endregion

            #region Assert

            Assert.AreEqual(DemandResult.Count(), demands.Count());

            #endregion

        }

        #endregion

        #region Insert Method Tests

        [TestMethod]
        [TestCategory("Integration")]
        public async Task DemandSvc_Insert_Success()
        {
            #region Arrange

            var user = new BE.User
            {

                Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                FirstName = "Test_DemandSvc_Insert_Success_Firstname_" + DateTime.Now.ToString(),
                Surname = "Test_DemandSvc_Insert_Success_Surname_" + DateTime.Now.ToString(),
                Email = "mail@user.com",
                UserName = "Test_DemandSvc_Insert_Success_Username_" + DateTime.Now.ToString(),
                PhoneNumber = "Test_DemandSvc_Insert_Success_PhoneNumber_" + DateTime.Now.ToString(),
            };

            var book = new BE.Book()
            {
                Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                Authors = new System.Collections.Generic.List<string>(),
                Title = "Test_DemandSvc_Insert_Success_Title",
                Description = "Test_DemandSvc_Insert_Success_Description",
                Publisher = "Test_DemandSvc_Insert_Success_Publisher",
            };

            #endregion

            #region Act

            var demand = new BE.Demand()
            {
                Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                Book = book,
                User = user
            };

            //Create new demand
            demand = await _demandRepository.Insert(demand);

            #endregion

            #region Assert

            Assert.IsNotNull(demand);
            Assert.IsNotNull(demand.Id);

            #endregion
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task DemandSvc_Delete_Success()
        {

            #region Arrange

            var user = new BE.User
            {
                Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                FirstName = "Test_DemandSvc_Delete_Success_Firstname_" + DateTime.Now.ToString(),
                Surname = "Test_DemandSvc_Delete_Success_Surname_" + DateTime.Now.ToString(),
                Email = "mail@user.com",
                UserName = "Test_DemandSvc_Delete_Success_Username_" + DateTime.Now.ToString(),
                PhoneNumber = "Test_DemandSvc_Delete_Success_PhoneNumber_" + DateTime.Now.ToString(),
            };

            var book = new BE.Book()
            {
                Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                Authors = new System.Collections.Generic.List<string>(),
                Title = "Test_DemandSvc_Delete_Success_Title",
                Description = "Test_DemandSvc_Delete_Success_Description",
                Publisher = "Test_DemandSvc_Delete_Success_Publisher",
            };


            #endregion

            #region Act

            var entity = new BE.Demand()
            {
                Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                Book = book,
                User = user
            };
            
            var insertedEntity = await this._demandRepository.Insert(entity);
            var getEntity = await this._demandRepository.Get(insertedEntity.Id);

            #endregion

            #region Assert

            Assert.IsNotNull(getEntity);
            Assert.AreEqual(getEntity.Id, insertedEntity.Id);

            var deletedEntity = await this._demandRepository.Delete(getEntity.Id);

            Assert.IsNotNull(deletedEntity);
            Assert.AreEqual(deletedEntity.Id, getEntity.Id);

            #endregion
        }

        #endregion
    }
}