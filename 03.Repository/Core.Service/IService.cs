using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CORE_ENT = BusinessEntities.CoreEntity;

namespace Core.Service
{
    public interface IService<T> where T : CORE_ENT.IEntity
    {

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<T> Insert(T entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<T> Update(T entity);

        /// <summary>
        /// Gets entity by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<T> Get(string id);
        
        /// <summary>
        /// Deletes by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<T> Delete(string id);

        /// <summary>
        /// Paginations the entites.
        /// </summary>
        /// <param name="top">The top.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="ascending">if set to <c>true</c> [ascending].</param>
        /// <returns></returns>
        Task<IEnumerable<T>> Pagination(int top, int skip, Func<T, object> orderBy, bool ascending = true);

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAll();
    }
}
