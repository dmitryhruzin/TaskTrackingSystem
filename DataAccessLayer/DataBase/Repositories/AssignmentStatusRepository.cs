using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.DataBase.Repositories
{
    /// <summary>
    ///   Implements an assignmentStatusRepository
    /// </summary>
    public class AssignmentStatusRepository : GenericRepository<AssignmentStatus>, IAssignmentStatusRepository
    {
        /// <summary>Initializes a new instance of the <see cref="AssignmentStatusRepository" /> class.</summary>
        /// <param name="dbContext">The database context.</param>
        public AssignmentStatusRepository(TaskDbContext dbContext) : base(dbContext)
        {

        }
    }
}
