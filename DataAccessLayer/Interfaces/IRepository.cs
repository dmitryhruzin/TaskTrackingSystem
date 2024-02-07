using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces
{
    /// <summary>
    ///   Describes a generic repository
    /// </summary>
    /// <typeparam name="T">BaseEntity abstract class</typeparam>
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>Gets all entities asynchronous.</summary>
        /// <returns>
        ///   Entities
        /// </returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>Gets the entity by identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   Entity
        /// </returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>Adds entity asynchronous.</summary>
        /// <param name="entity">The entity.</param>
        Task AddAsync(T entity);

        /// <summary>Deletes entity specified entity.</summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);

        /// <summary>Deletes entity by identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        Task DeleteByIdAsync(int id);

        /// <summary>Updates entity specified entity.</summary>
        /// <param name="entity">The entity.</param>
        void Update(T entity);
    }
}
