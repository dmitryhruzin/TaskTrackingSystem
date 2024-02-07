using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DataBase.Repositories
{
    /// <summary>
    ///   Implements a genericRepository
    /// </summary>
    /// <typeparam name="T">BaseEntity abstract class</typeparam>
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected private readonly TaskDbContext dbContext;

        /// <summary>Initializes a new instance of the <see cref="GenericRepository{T}" /> class.</summary>
        /// <param name="dbContext">The database context.</param>
        public GenericRepository(TaskDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>Adds entity asynchronous.</summary>
        /// <param name="entity">The entity.</param>
        public async Task AddAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
        }

        /// <summary>Deletes entity specified entity.</summary>
        /// <param name="entity">The entity.</param>
        public void Delete(T entity)
        {
            dbContext.Set<T>().Remove(entity);
        }

        /// <summary>Deletes entity by identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteByIdAsync(int id)
        {
            Delete(await GetByIdAsync(id));
        }

        /// <summary>Gets all entities asynchronous.</summary>
        /// <returns>Entities</returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        /// <summary>Gets the entity by identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Entity</returns>
        public async Task<T> GetByIdAsync(int id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        /// <summary>Updates entity specified entity.</summary>
        /// <param name="entity">The entity.</param>
        public void Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
        }
    }
}
