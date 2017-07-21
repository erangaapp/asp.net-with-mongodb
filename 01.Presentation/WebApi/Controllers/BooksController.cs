using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.Description;

using BE = BusinessEntities;
using SVC = MongoDB.Service;

namespace WebApi.Controllers
{
    /// <summary>
    /// Books api
    /// </summary>
    public class BooksController : ApiController
    {
        private readonly SVC.IBookService bookService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookService"></param>
        public BooksController(SVC.IBookService bookService)
        {
            this.bookService = bookService;
        }

        /// <summary>
        /// Get all the books
        /// </summary>
        /// <returns>All books </returns>
        [HttpGet]
        [EnableQuery]
        [ResponseType(typeof(IQueryable<BE.Book>))]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var query = await bookService.GetAll();
                return Ok<IQueryable<BE.Book>>(query.AsQueryable());
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Filterd books by filter
        /// </summary>
        /// <param name="filter">filter conteins Title,Description,Publisher,Authors </param>
        /// <returns>All Books filter conteins Title,Description,Publisher,Authors</returns>
        [HttpGet]
        [EnableQuery]
        [ResponseType(typeof(IQueryable<BE.Book>))]
        public async Task<IHttpActionResult> Get(string filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter))
                    return BadRequest();

                var query = await bookService.GetAll(filter);
                return Ok<IQueryable<BE.Book>>(query.AsQueryable());
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

    }

}
