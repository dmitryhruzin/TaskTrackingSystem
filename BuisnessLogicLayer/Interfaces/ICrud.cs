namespace BuisnessLogicLayer.Interfaces
{
    /// <summary>
    ///   Describes a Crud service
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICrud<T> where T : class
    {
        /// <summary>Gets all models asynchronous.</summary>
        /// <returns>
        ///   Models
        /// </returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>Gets the model by identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   Model
        /// </returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>Adds the model asynchronous.</summary>
        /// <param name="model">The model.</param>
        Task AddAsync(T model);

        /// <summary>Updates the model asynchronous.</summary>
        /// <param name="model">The model.</param>
        Task UpdateAsync(T model);

        /// <summary>Deletes the model by id asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        Task DeleteAsync(int id);
    }
}
