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
    public class UserServiceTests
    {
        private CORE_REPO.IRepository<BE.User> _userRepository;
        private IUserService _userService;

        public UserServiceTests()
        {
            this._userRepository = new CORE_MONGO_REPO.MongoRepository<BE.User>(new MongoDB.Driver.MongoClient());
            _userService = new UserService(this._userRepository);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task UserSvc_Get_User_By_Username_Success()
        {
            //Arrange
            var user =  new BE.User
            {
                FirstName = "Test_Get_User_By_Username_Success_Firstname_" + DateTime.Now.ToString(),
                Surname = "Test_Get_User_By_Username_Success_Surname_" + DateTime.Now.ToString(),
                Email = "mail@user.com",
                UserName = "Test_Get_User_By_Username_Success_Username_" + DateTime.Now.ToString(),
                PhoneNumber = "Test_Get_User_By_Username_Success_PhoneNumber_" + DateTime.Now.ToString(),
            };

            //Act

            user = await this._userService.Insert(user);

            Assert.IsNotNull(user.Id);
            Assert.IsNotNull(user);

            var USERRESULT = await this._userService.GetByUserName(user.UserName);

            //Assert

            Assert.AreEqual(USERRESULT.Id, user.Id);
        }

    }
}