using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces
{
    /// <summary>
    ///   Describes a project repository
    /// </summary>
    public interface IProjectRepository : IRepository<Project>
    {
        /// <summary>Gets all projects with details asynchronous.</summary>
        /// <returns>
        ///   Projects
        /// </returns>
        Task<IEnumerable<Project>> GetAllWithDetailsAsync();

        /// <summary>Gets a project by identifier with details asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   Project
        /// </returns>
        Task<Project> GetByIdWithDetailsAsync(int id);
    }
}
