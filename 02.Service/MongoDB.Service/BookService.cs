using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BE = BusinessEntities;
using SVC = Core.Service;
using CORE_REPO = Core.Repository;

namespace MongoDB.Service
{
    /// <summary>
    /// Book service
    /// </summary>
    public interface IBookService : SVC.IService<BE.Book>
    {
        /// <summary>
        /// Get filterd books by Title,Description,Publisher,Authors 
        /// </summary>
        /// <param name="name">publisher name</param>
        /// <returns>Filterd books by Title,Description,Publisher,Authors </returns>
        Task<IEnumerable<BE.Book>> GetAll(string filter);

        /// <summary>
        /// Get books by Ids
        /// </summary>
        /// <param name="bookIds">List of book Ids</param>
        /// <returns>Return BookID match with book Ids</returns>
        Task<IEnumerable<BE.Book>> GetByIds(List<string> bookIds);

        /// <summary>
        /// Get books by Title and Author
        /// </summary>
        /// <returns>Return Books match Title and author</returns>
        Task<IEnumerable<BE.Book>> GetByAuthorAndTitle(string title, string author);
    }

    public class BookService : SVC.Service<BE.Book>, IBookService
    {
        private readonly CORE_REPO.IRepository<BE.Book> _repository;

        /// <summary>
        /// Initializes a new instance of the book service.
        /// </summary>
        /// <param name="repository">Book repository</param>
        public BookService(CORE_REPO.IRepository<BE.Book> repository) : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get books by publisher
        /// </summary>
        /// <param name="name">publisher name</param>
        /// <returns></returns>
        public async Task<IEnumerable<BE.Book>> GetAll(string filter)
        {
            try
            {
                if (string.IsNullOrEmpty(filter))
                    return null;

                var query = from T in await _repository.GetAll()
                            where T.Title.Contains(filter) ||
                            T.Description.Contains(filter) ||
                            T.Publisher.Contains(filter) ||
                            (T.Authors.FindAll(a => a.Contains(filter)).Count > 0)
                            select T;

                return query;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get books by Ids
        /// </summary>
        /// <param name="bookIds">List of book Ids</param>
        /// <returns>Return BookID match with book Ids</returns>
        public async Task<IEnumerable<BE.Book>> GetByIds(List<string> bookIds)
        {
            try
            {
                var query = from T in await _repository.GetAll()
                            where bookIds.Contains(T.Id)
                            select T;

                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get books by Title and Author
        /// </summary>
        /// <returns>Return Books match Title and author</returns>
        public async Task<IEnumerable<BE.Book>> GetByAuthorAndTitle(string title, string author)
        {
            try
            {
                var query = from T in await _repository.GetAll()
                            where T.Title == title && T.Authors.Contains(author)
                            select T;

                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Inserts the book.
        /// </summary>
        /// <param name="entity">The book entity.</param>
        /// <returns>Created book entity.</returns>
        public override async Task<BE.Book> Insert(BE.Book entity)
        {
            try
            {
                // add business logic here
                return await _repository.Insert(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Updates the book
        /// </summary>
        /// <param name="entity">The book entity</param>
        /// <returns>Updated book entity</returns>
        public override async Task<BE.Book> Update(BE.Book entity)
        {
            try
            {
                // add business logic here
                return await _repository.Update(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Delete the book
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The deleted book was deleted.</returns>
        public override async Task<BE.Book> Delete(string id)
        {
            try
            {
                // add business logic here
                return await _repository.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gell all available books
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<BE.Book>> GetAll()
        {
            try
            {
                // add business logic here
                return await _repository.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
