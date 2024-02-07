using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces
{
    /// <summary>
    ///   Describes an assignment repository
    /// </summary>
    public interface IAssignmentRepository : IRepository<Assignment>
    {
        /// <summary>Gets all assignments with details asynchronous.</summary>
        /// <returns>
        ///  The Assignments
        /// </returns>
        Task<IEnumerable<Assignment>> GetAllWithDetailsAsync();

        /// <summary>Gets an assignment by identifier with details asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   The Assignment
        /// </returns>
        Task<Assignment> GetByIdWithDetailsAsync(int id);
    }
}
