using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Web.Mvc;
using Microsoft.CSharp;


using CORE_REPO = Core.Repository;
using BE = BusinessEntities;
using SVC = MongoDB.Service;
using CORE_DB = Core.Repository.MongoDb;


namespace WebApp.Controllers.Tests
{
    [TestClass()]
    public class DemandControllerTests
    {

        [TestMethod()]
        public void DemandController_Get_BooksDemand_Test()
        {
            var mockBookService = new Mock<SVC.IBookService>();
            var mockDemandService = new Mock<SVC.IDemandService>();
            var mockUserService = new Mock<SVC.IUserService>();
            var apiClient = new Mock<Models.IApiClient<BE.Book>>();

            var controller = new DemandController(mockBookService.Object, mockUserService.Object, mockDemandService.Object, apiClient.Object);

            ActionResult result = controller.BooksDemand();
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod()]
        public async Task DemandController_Get_MyDemands_Test()
        {
            #region Arrange

            var apiClient = new Mock<Models.IApiClient<BE.Book>>();
            var mockBookService = new Mock<SVC.IBookService>();
            var mockUserService = new Mock<SVC.IUserService>();

            var mockDemands = (new List<BE.Demand>()
            {
                new BE.Demand() {
                    Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                    User = new BE.User() {
                        Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                        Email = "user@mail.com"
                    }
                },
                new BE.Demand() {
                    Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                    User = new BE.User() {
                        Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                        Email = "user@mail.com"
                    }
                }

            }).AsEnumerable();

            var mockDemandService = new Mock<SVC.IDemandService>();
            mockDemandService.Setup(arg => arg.GetByUser("user@mail.com")).Returns(Task.FromResult(mockDemands));

            #region UserIdentity

            var controller = new DemandController(mockBookService.Object, mockUserService.Object, mockDemandService.Object, apiClient.Object);
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(x => x.HttpContext.User.Identity.Name).Returns("user@mail.com");
            mock.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true);
            controller.ControllerContext = mock.Object;

            #endregion

            #endregion

            //Act
            ActionResult result = await controller.MyDemands();
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            //Assert
            ViewResult vResult = result as ViewResult;
            Assert.IsInstanceOfType(vResult.Model, typeof(IEnumerable<BE.Demand>));

            var models = vResult.Model as IEnumerable<BE.Demand>;
            Assert.AreEqual(models.Count(), mockDemands.Count());

            //Check deleted alert status
            Assert.IsNull(vResult.ViewBag.Status);

        }

        [TestMethod()]
        public async Task DemandController_Get_MyDemands_IsAfterDelete_True_Test()
        {
            #region Arrange

            var apiClient = new Mock<Models.IApiClient<BE.Book>>();
            var mockBookService = new Mock<SVC.IBookService>();
            var mockUserService = new Mock<SVC.IUserService>();

            var mockDemands = (new List<BE.Demand>()
            {
                new BE.Demand() {
                    Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                    User = new BE.User() {
                        Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                        Email = "user@mail.com"
                    }
                },
                new BE.Demand() {
                    Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                    User = new BE.User() {
                        Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                        Email = "user@mail.com"
                    }
                }

            }).AsEnumerable();

            var mockDemandService = new Mock<SVC.IDemandService>();
            mockDemandService.Setup(arg => arg.GetByUser("user@mail.com")).Returns(Task.FromResult(mockDemands));

            #region UserIdentity

            var controller = new DemandController(mockBookService.Object, mockUserService.Object, mockDemandService.Object, apiClient.Object);
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(x => x.HttpContext.User.Identity.Name).Returns("user@mail.com");
            mock.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true);
            controller.ControllerContext = mock.Object;

            #endregion

            #endregion

            //Act
            ActionResult result = await controller.MyDemands(true);
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            //Assert
            ViewResult vResult = result as ViewResult;
            Assert.AreEqual(vResult.ViewBag.Status, "DeleteSuccess");

        }

        [TestMethod()]
        public async Task DemandController_Get_Delete_BooksDemand_Test()
        {
            #region Arrange

            var apiClient = new Mock<Models.IApiClient<BE.Book>>();
            var mockBookService = new Mock<SVC.IBookService>();
            var mockUserService = new Mock<SVC.IUserService>();

            var Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

            var mockDemandService = new Mock<SVC.IDemandService>();
            mockDemandService.Setup(arg => arg.Get(Id)).Returns(Task.FromResult(new BE.Demand() { Id = Id }));

            #region UserIdentity

            var controller = new DemandController(mockBookService.Object, mockUserService.Object, mockDemandService.Object, apiClient.Object);
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(x => x.HttpContext.User.Identity.Name).Returns("user@mail.com");
            mock.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true);
            controller.ControllerContext = mock.Object;

            #endregion

            #endregion

            //Act
            ActionResult result = await controller.Delete(Id);
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            //Assert
            ViewResult vResult = result as ViewResult;
            var model = vResult.Model as BE.Demand;
            Assert.AreEqual(model.Id, Id);
        }

        [TestMethod()]
        public async Task DemandController_POST_Delete_BooksDemand_Test()
        {
            #region Arrange

            var apiClient = new Mock<Models.IApiClient<BE.Book>>();
            var mockBookService = new Mock<SVC.IBookService>();
            var mockUserService = new Mock<SVC.IUserService>();

            var Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

            var mockDemandService = new Mock<SVC.IDemandService>();
            mockDemandService.Setup(arg => arg.Delete(Id)).Returns(Task.FromResult(new BE.Demand() { Id = Id }));

            #region UserIdentity

            var controller = new DemandController(mockBookService.Object, mockUserService.Object, mockDemandService.Object, apiClient.Object);
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(x => x.HttpContext.User.Identity.Name).Returns("user@mail.com");
            mock.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true);
            controller.ControllerContext = mock.Object;

            #endregion

            #endregion

            //Act
            ActionResult result = await controller.DeleteDemand(Id);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

            //Assert
            RedirectToRouteResult rResult = result as RedirectToRouteResult;
            Assert.AreEqual(rResult.RouteValues["IsAfterDelete"], true);
            Assert.AreEqual(rResult.RouteValues["action"], "MyDemands");

        }

        [TestMethod()]
        public async Task DemandController_POST_BooksDemand_Success()
        {
            #region Arrange

            var apiClient = new Mock<Models.IApiClient<BE.Book>>();
            var mockDemandService = new Mock<SVC.IDemandService>();

            var BookIds = new List<string>() { MongoDB.Bson.ObjectId.GenerateNewId().ToString(), MongoDB.Bson.ObjectId.GenerateNewId().ToString() };
            var mockBooks = (new List<BE.Book>()
            {
                new BE.Book() {
                    Id = BookIds[0],
                    Title = "TestBook1",
                    Description = "Test Book1 description",
                    Publisher =  "Test Book1 publisher",
                    Authors = new List<string>() { "Author1", "Author2" }
                },
                new BE.Book() {
                    Id = BookIds[1],
                    Title = "TestBook2",
                    Description = "Test Book2 description",
                    Publisher =  "Test Book2 publisher",
                    Authors = new List<string>() { "Author1", "Author2" }
                }

            }).AsEnumerable();

            var mockBookService = new Mock<SVC.IBookService>();
            mockBookService.Setup(arg => arg.GetByIds(BookIds)).Returns(Task.FromResult(mockBooks));

            var mockUserService = new Mock<SVC.IUserService>();
            mockUserService.Setup(arg => arg.GetByUserName("user@mail.com")).Returns(Task.FromResult(new BE.User()
            {
                Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                FirstName = "DemandController_POST_BooksDemand_Success_Firstname_" + DateTime.Now.ToString(),
                Surname = "DemandController_POST_BooksDemand_Success_Surname_" + DateTime.Now.ToString(),
                Email = "user@mail.com",
                UserName = "DemandController_POST_BooksDemand_Success_Username_" + DateTime.Now.ToString(),
                PhoneNumber = "DemandController_POST_BooksDemand_Success_PhoneNumber_" + DateTime.Now.ToString(),

            }));

            #region UserIdentity

            var controller = new DemandController(mockBookService.Object, mockUserService.Object, mockDemandService.Object, apiClient.Object);
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(x => x.HttpContext.User.Identity.Name).Returns("user@mail.com");
            mock.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true);
            controller.ControllerContext = mock.Object;

            string ids = string.Empty;
            foreach (var id in BookIds)
                ids += id + ",";

            #endregion

            #endregion

            //Act
            ActionResult result = await controller.BooksDemand(ids);
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));

            //Assert
            PartialViewResult rResult = result as PartialViewResult;
            Assert.AreEqual(rResult.ViewBag.Status, "Success");
            Assert.AreEqual(rResult.ViewName, "_BookDemand");

            var models = rResult.Model as IEnumerable<BE.Book>;
            Assert.AreEqual(models.Count(), mockBooks.Count());
        }

        [TestMethod()]
        public async Task DemandController_POST_BooksDemand_ZeroIds()
        {
            #region Arrange

            var apiClient = new Mock<Models.IApiClient<BE.Book>>();
            var mockDemandService = new Mock<SVC.IDemandService>();

            var BookIds = new List<string>() { MongoDB.Bson.ObjectId.GenerateNewId().ToString(), MongoDB.Bson.ObjectId.GenerateNewId().ToString() };
            var mockBooks = (new List<BE.Book>()
            {
                new BE.Book() {
                    Id = BookIds[0],
                    Title = "TestBook1",
                    Description = "Test Book1 description",
                    Publisher =  "Test Book1 publisher",
                    Authors = new List<string>() { "Author1", "Author2" }
                },
                new BE.Book() {
                    Id = BookIds[1],
                    Title = "TestBook2",
                    Description = "Test Book2 description",
                    Publisher =  "Test Book2 publisher",
                    Authors = new List<string>() { "Author1", "Author2" }
                }

            }).AsEnumerable();

            var mockBookService = new Mock<SVC.IBookService>();
            mockBookService.Setup(arg => arg.GetByIds(BookIds)).Returns(Task.FromResult(mockBooks));

            var mockUserService = new Mock<SVC.IUserService>();
            mockUserService.Setup(arg => arg.GetByUserName("user@mail.com")).Returns(Task.FromResult(new BE.User()
            {
                Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                FirstName = "DemandController_POST_BooksDemand_ZeroIds_Firstname_" + DateTime.Now.ToString(),
                Surname = "DemandController_POST_BooksDemand_ZeroIds_Surname_" + DateTime.Now.ToString(),
                Email = "user@mail.com",
                UserName = "DemandController_POST_BooksDemand_ZeroIds_Username_" + DateTime.Now.ToString(),
                PhoneNumber = "DemandController_POST_BooksDemand_ZeroIds_PhoneNumber_" + DateTime.Now.ToString(),

            }));

            #region UserIdentity

            var controller = new DemandController(mockBookService.Object, mockUserService.Object, mockDemandService.Object, apiClient.Object);
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(x => x.HttpContext.User.Identity.Name).Returns("user@mail.com");
            mock.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true);
            controller.ControllerContext = mock.Object;

            string ids = string.Empty;

            #endregion

            #endregion

            //Act
            ActionResult result = await controller.BooksDemand(ids);
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));

            //Assert
            PartialViewResult prResult = result as PartialViewResult;
            Assert.AreEqual(prResult.ViewBag.Status, "NotSelected");
            Assert.AreEqual(prResult.ViewName, "_BookDemand");
            Assert.IsNull(prResult.Model);

        }

        [TestMethod()]
        public async Task DemandController_POST_BooksDemand_ZeroBooks()
        {
            #region Arrange

            var apiClient = new Mock<Models.IApiClient<BE.Book>>();
            var mockDemandService = new Mock<SVC.IDemandService>();

            var BookIds = new List<string>() { MongoDB.Bson.ObjectId.GenerateNewId().ToString(), MongoDB.Bson.ObjectId.GenerateNewId().ToString() };
            var mockBooks = (new List<BE.Book>(){ }).AsEnumerable();

            var mockBookService = new Mock<SVC.IBookService>();
            mockBookService.Setup(arg => arg.GetByIds(BookIds)).Returns(Task.FromResult(mockBooks));

            var mockUserService = new Mock<SVC.IUserService>();
            mockUserService.Setup(arg => arg.GetByUserName("user@mail.com")).Returns(Task.FromResult(new BE.User()
            {
                Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                FirstName = "DemandController_POST_BooksDemand_ZeroBooks_Firstname_" + DateTime.Now.ToString(),
                Surname = "DemandController_POST_BooksDemand_ZeroBooks_Surname_" + DateTime.Now.ToString(),
                Email = "user@mail.com",
                UserName = "DemandController_POST_BooksDemand_ZeroBooks_Username_" + DateTime.Now.ToString(),
                PhoneNumber = "DemandController_POST_BooksDemand_ZeroBooks_PhoneNumber_" + DateTime.Now.ToString(),

            }));

            #region UserIdentity

            var controller = new DemandController(mockBookService.Object, mockUserService.Object, mockDemandService.Object, apiClient.Object);
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(x => x.HttpContext.User.Identity.Name).Returns("user@mail.com");
            mock.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true);
            controller.ControllerContext = mock.Object;

            string ids = string.Empty;

            #endregion

            #endregion

            //Act
            ActionResult result = await controller.BooksDemand(ids);
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));

            //Assert
            PartialViewResult prResult = result as PartialViewResult;
            Assert.AreEqual(prResult.ViewBag.Status, "NotSelected");
            Assert.AreEqual(prResult.ViewName, "_BookDemand");
            Assert.IsNull(prResult.Model);

        }

        [TestMethod()]
        public async Task DemandController_Get_SearchBooks_Success_With_Filter()
        {
            #region Arrange

            var apiClient = new Mock<Models.IApiClient<BE.Book>>();
            var mockDemandService = new Mock<SVC.IDemandService>();

            var mockBooks = (new List<BE.Book>()
            {
                new BE.Book() {
                    Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                    Title = "TestBook1",
                    Description = "Test Book1 description",
                    Publisher =  "Test Book1 publisher",
                    Authors = new List<string>() { "Author1", "Author2" }
                },
                new BE.Book() {
                    Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                    Title = "TestBook2",
                    Description = "Test Book2 description",
                    Publisher =  "Test Book2 publisher",
                    Authors = new List<string>() { "Author1", "Author2" }
                }

            }).ToList();

            var query = "TestBook1";
            string path = "api/Books/Get?filter=" + query;
            var mockBookService = new Mock<SVC.IBookService>();
            apiClient.Setup(arg => arg.Search(path)).Returns(Task.FromResult(mockBooks));

            var mockUserService = new Mock<SVC.IUserService>();
            

            #region UserIdentity

            var controller = new DemandController(mockBookService.Object, mockUserService.Object, mockDemandService.Object, apiClient.Object);
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(x => x.HttpContext.User.Identity.Name).Returns("user@mail.com");
            mock.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true);
            controller.ControllerContext = mock.Object;

            #endregion

            #endregion

            //Act
            ActionResult result = await controller.SearchBooks(query);
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));

            //Assert
            PartialViewResult rResult = result as PartialViewResult;
            Assert.AreEqual(rResult.ViewName, "_BooksRearchResult");

            var models = rResult.Model as List<BE.Book>;
            Assert.AreEqual(models.Count(), mockBooks.Count());
        }

        [TestMethod()]
        public async Task DemandController_Get_SearchBooks_Success_Without_Filter()
        {
            #region Arrange

            var apiClient = new Mock<Models.IApiClient<BE.Book>>();
            var mockDemandService = new Mock<SVC.IDemandService>();

            var mockBooks = (new List<BE.Book>()
            {
                new BE.Book() {
                    Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                    Title = "TestBook1",
                    Description = "Test Book1 description",
                    Publisher =  "Test Book1 publisher",
                    Authors = new List<string>() { "Author1", "Author2" }
                },
                new BE.Book() {
                    Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                    Title = "TestBook2",
                    Description = "Test Book2 description",
                    Publisher =  "Test Book2 publisher",
                    Authors = new List<string>() { "Author1", "Author2" }
                }

            }).ToList();

            var query = string.Empty;
            string path = "api/Books/Get";
            var mockBookService = new Mock<SVC.IBookService>();
            apiClient.Setup(arg => arg.Search(path)).Returns(Task.FromResult(mockBooks));

            var mockUserService = new Mock<SVC.IUserService>();


            #region UserIdentity

            var controller = new DemandController(mockBookService.Object, mockUserService.Object, mockDemandService.Object, apiClient.Object);
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(x => x.HttpContext.User.Identity.Name).Returns("user@mail.com");
            mock.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true);
            controller.ControllerContext = mock.Object;

            #endregion

            #endregion

            //Act
            ActionResult result = await controller.SearchBooks(query);
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));

            //Assert
            PartialViewResult rResult = result as PartialViewResult;
            Assert.AreEqual(rResult.ViewName, "_BooksRearchResult");

            var models = rResult.Model as List<BE.Book>;
            Assert.AreEqual(models.Count(), mockBooks.Count());
        }
    }
}