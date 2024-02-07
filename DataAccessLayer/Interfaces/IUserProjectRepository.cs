using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces
{
    /// <summary>
    ///   Describes an user project repository
    /// </summary>
    public interface IUserProjectRepository : IRepository<UserProject>
    {
        /// <summary>Gets all userprojects with details asynchronous.</summary>
        /// <returns>
        ///   UserProjects
        /// </returns>
        Task<IEnumerable<UserProject>> GetAllWithDetailsAsync();
    }
}
