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
    /// Demand service
    /// </summary>
    public interface IDemandService : SVC.IService<BE.Demand>
    {
        /// <summary>
        /// Get books by user
        /// </summary>
        /// <param user="user">user name</param>
        /// <returns>IEnumerable<Demand> for user</returns>
        Task<IEnumerable<BE.Demand>> GetByUser(string userName);
    }

    public class DemandService : SVC.Service<BE.Demand>, IDemandService
    {
        private readonly CORE_REPO.IRepository<BE.Demand> _repository;

        /// <summary>
        /// Initializes a new instance of the book service.
        /// </summary>
        /// <param name="repository">Demand repository</param>
        public DemandService(CORE_REPO.IRepository<BE.Demand> repository) : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get books by user
        /// </summary>
        /// <param name="name">publisher name</param>
        /// <returns>IEnumerable<Demand> for user</returns>
        public async Task<IEnumerable<BE.Demand>> GetByUser(string userName)
        {
            var query = from T in await _repository.GetAll()
                        where T.User.UserName == userName
                        select T;

            return query;
        }

        /// <summary>
        /// Inserts the book.
        /// </summary>
        /// <param name="entity">The book entity.</param>
        /// <returns>Created book entity.</returns>
        public override async Task<BE.Demand> Insert(BE.Demand entity)
        {
            // add business logic here
            return await _repository.Insert(entity);
        }

        /// <summary>
        /// Updates the book
        /// </summary>
        /// <param name="entity">The book entity</param>
        /// <returns>Updated book entity</returns>
        public override async Task<BE.Demand> Update(BE.Demand entity)
        {
            // add business logic here
            return await _repository.Update(entity);
        }

        /// <summary>
        /// Delete the book
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The deleted book was deleted.</returns>
        public override async Task<BE.Demand> Delete(string id)
        {
            // add business logic here
            return await _repository.Delete(id);
        }

        /// <summary>
        /// Gell all available books
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<BE.Demand>> GetAll()
        {
            // add business logic here
            return await _repository.GetAll();
        }

    }
}
