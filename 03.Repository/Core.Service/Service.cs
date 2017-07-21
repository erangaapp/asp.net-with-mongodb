using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CORE_ENT = BusinessEntities.CoreEntity;
using CORE_REPO = Core.Repository;

namespace Core.Service
{
    public abstract class Service<T> : IService<T> where T : class, CORE_ENT.IEntity
    {
        #region Private Fields
        private readonly CORE_REPO.IRepository<T> _repository;
        #endregion Private Fields

        #region Constructor
        protected Service(CORE_REPO.IRepository<T> repository) { _repository = repository; }
        #endregion Constructor

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual async Task<T> Insert(T entity)
        {
            return await this._repository.Insert(entity);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual async Task<T> Update(T entity)
        {
            return await this._repository.Update(entity);
        }

        /// <summary>
        /// Gets entity by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual async Task<T> Get(string id)
        {
            return await this._repository.Get(id);
        }

        /// <summary>
        /// Deletes by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual async Task<T> Delete(string id)
        {
            return await this._repository.Delete(id);
        }

        /// <summary>
        /// Paginations the entites.
        /// </summary>
        /// <param name="top">The top.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="ascending">if set to <c>true</c> [ascending].</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> Pagination(int top, int skip, Func<T, object> orderBy, bool ascending = true)
        {
            return await this._repository.Pagination(top, skip, orderBy, ascending);
        }


        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await this._repository.GetAll();
        }
    }
}
