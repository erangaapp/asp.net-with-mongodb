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
    /// User service
    /// </summary>
    public interface IUserService : SVC.IService<BE.User>
    {
        /// <summary>
        /// Get user by user name
        /// </summary>
        /// <param name="userName">User name</param>
        /// <returns></returns>
        Task<BE.User> GetByUserName(string userName);

    }

    public class UserService : SVC.Service<BE.User>, IUserService
    {
        private readonly CORE_REPO.IRepository<BE.User> _repository;

        /// <summary>
        /// Initializes a new instance of the User service.
        /// </summary>
        /// <param name="repository">User repository</param>
        public UserService(CORE_REPO.IRepository<BE.User> repository) : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get user by user name
        /// </summary>
        /// <param name="userName">User name</param>
        /// <returns></returns>
        public async Task<BE.User> GetByUserName(string userName)
        {
            try
            {
                var query = from T in await _repository.GetAll()
                            where T.UserName.Equals(userName)
                            select T;

                return (query.Count() > 0 ? query.First() : null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Inserts the User.
        /// </summary>
        /// <param name="entity">The User entity.</param>
        /// <returns>Created User entity.</returns>
        public override async Task<BE.User> Insert(BE.User entity)
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
        /// Updates the User
        /// </summary>
        /// <param name="entity">The User entity</param>
        /// <returns>Updated User entity</returns>
        public override async Task<BE.User> Update(BE.User entity)
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
        /// Delete the User
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The deleted User was deleted.</returns>
        public override async Task<BE.User> Delete(string id)
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
        /// Gell all available Users
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<BE.User>> GetAll()
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
