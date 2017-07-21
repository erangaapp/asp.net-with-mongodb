using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

using CORE_REPO = Core.Repository;
using BE = BusinessEntities;
using CORE_MONGO_REPO = Core.Repository.MongoDb;

namespace MongoDB.Service.Tests
{
    [TestClass()]
    public class BookServiceTests
    {
        private CORE_REPO.IRepository<BE.Book> _bookRepository;
        private IBookService _bookService;

        public BookServiceTests()
        {
            this._bookRepository = new CORE_MONGO_REPO.MongoRepository<BE.Book>(new MongoDB.Driver.MongoClient());
            _bookService = new BookService(this._bookRepository);
        }

        #region Get Method Tests

        [TestMethod]
        [TestCategory("Integration")]
        public async Task BookSvc_Get_All_Success()
        {
            //Arrange
            var books = await this._bookRepository.GetAll();

            //Act
            var BookResult = await _bookService.GetAll();

            //Assert
            Assert.AreEqual(BookResult.Count(), books.Count());
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task BookSvc_Get_Filter_Eq_Title_Success()
        {
            //Arrange

            //Title of one book
            string filter = "The Black Death";
            var BookResult = await _bookRepository.GetAll();
            var filterdBooks = from T in BookResult
                               where T.Title.Equals(filter)
                               select T;

            //Act

            var books = await this._bookService.GetAll(filter);


            //Assert

            Assert.IsTrue(filterdBooks.Count() > 0);
            Assert.AreEqual(filterdBooks.Count(), books.Count());
        }

        /// <summary>
        /// Find filter conteins in Title,Description,Publisher and Authors
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [TestCategory("Integration")]
        public async Task BookSvc_Get_Filter_Conteins_Whatever_Success()
        {
            //Arrange

            //Somthing on title
            string filter = "University";
            var BookResult = await _bookRepository.GetAll();
            //Find filter conteins in Title,Description,Publisher and Authors
            var filterdBooks = from T in BookResult
                               where T.Title.Contains(filter) ||
                               T.Description.Contains(filter) ||
                               T.Publisher.Contains(filter) ||
                               (T.Authors.FindAll(a => a.Contains(filter)).Count > 0)
                               select T;

            //Act

            var books = await this._bookService.GetAll(filter);


            //Assert

            Assert.AreEqual(filterdBooks.Count(), books.Count());
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task BookSvc_Get_Null_Entity_Filter_Success()
        {
            //Arrange

            //Empty string on filter
            string filter = string.Empty;

            //Act

            var books = await this._bookService.GetAll(filter);


            //Assert

            Assert.IsNull(books);
        }

        #endregion

    }
}