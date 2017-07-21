using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using System.Web.Http;
using System.Web.Http.Results;

using BE = BusinessEntities;
using SVC = MongoDB.Service;

namespace WebApi.Controllers.Tests
{
    [TestClass()]
    public class BooksControllerTests
    {

        [TestMethod]
        public async Task WebApi_BooksController_Get_All()
        {
            #region Arrange

            var mockBooks = (new System.Collections.Generic.List<BE.Book>()
            {
                new BE.Book() {
                    Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                    Title = "Title_WebApi_BooksController_Get_All",
                    Description = "Description_WebApi_BooksController_Get_All",
                    Publisher = "Pulisher_WebApi_BooksController_Get_All",
                    Authors = new System.Collections.Generic.List<string>() {"Author1", "Autho2" }
                },
                new BE.Book() {
                    Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                    Title = "Title_WebApi_BooksController_Get_All",
                    Description = "Description_WebApi_BooksController_Get_All",
                    Publisher = "Pulisher_WebApi_BooksController_Get_All",
                    Authors = new System.Collections.Generic.List<string>() {"Author1", "Autho2" }
                }

            }).AsEnumerable();

            var mockBookService = new Mock<SVC.IBookService>();
            mockBookService.Setup(arg => arg.GetAll()).Returns(Task.FromResult(mockBooks));

            var controller = new BooksController(mockBookService.Object);

            #endregion

            //Act

            IHttpActionResult response = await controller.Get();
            var contentResult = response as OkNegotiatedContentResult<IQueryable<BE.Book>>;

            //Assert

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(contentResult.Content.Count(), mockBooks.Count());

        }

        [TestMethod]
        public async Task WebApi_BooksController_Get_By_Filter()
        {
            #region Arrange

            var filter = "Title_WebApi_BooksController_Get_All";

            var mockBooks = (new System.Collections.Generic.List<BE.Book>()
            {
                new BE.Book() {
                    Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                    Title = "Title_WebApi_BooksController_Get_All",
                    Description = "Description_WebApi_BooksController_Get_All",
                    Publisher = "Pulisher_WebApi_BooksController_Get_All",
                    Authors = new System.Collections.Generic.List<string>() {"Author1", "Autho2" }
                },
                new BE.Book() {
                    Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                    Title = "Title_WebApi_BooksController_Get_All",
                    Description = "Description_WebApi_BooksController_Get_All",
                    Publisher = "Pulisher_WebApi_BooksController_Get_All",
                    Authors = new System.Collections.Generic.List<string>() {"Author1", "Autho2" }
                }

            }).AsEnumerable();

            var mockBookService = new Mock<SVC.IBookService>();
            mockBookService.Setup(arg => arg.GetAll(filter)).Returns(Task.FromResult(mockBooks));

            var controller = new BooksController(mockBookService.Object);

            #endregion

            //Act

            IHttpActionResult response = await controller.Get(filter);
            var contentResult = response as OkNegotiatedContentResult<IQueryable<BE.Book>>;

            //Assert

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(contentResult.Content.Count(), mockBooks.Count());

        }

        [TestMethod]
        public async Task WebApi_BooksController_Get_By_Empty_NullFilter_BadRequest()
        {
            #region Arrange

            var mockBooks = (new System.Collections.Generic.List<BE.Book>()
            {
                new BE.Book() {
                    Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                    Title = "Title_WebApi_BooksController_Get_All",
                    Description = "Description_WebApi_BooksController_Get_All",
                    Publisher = "Pulisher_WebApi_BooksController_Get_All",
                    Authors = new System.Collections.Generic.List<string>() {"Author1", "Autho2" }
                },
                new BE.Book() {
                    Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                    Title = "Title_WebApi_BooksController_Get_All",
                    Description = "Description_WebApi_BooksController_Get_All",
                    Publisher = "Pulisher_WebApi_BooksController_Get_All",
                    Authors = new System.Collections.Generic.List<string>() {"Author1", "Autho2" }
                }

            }).AsEnumerable(); ;

            var mockBookService = new Mock<SVC.IBookService>();
            mockBookService.Setup(arg => arg.GetAll()).Returns(Task.FromResult(mockBooks));

            var controller = new BooksController(mockBookService.Object);

            #endregion

            //Act

            IHttpActionResult response = await controller.Get(null);
            var contentResult = response as BadRequestResult;

            //Assert

            Assert.IsNotNull(contentResult);

        }
    }
}