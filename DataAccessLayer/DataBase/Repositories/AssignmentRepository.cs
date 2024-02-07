using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DataBase.Repositories
{
    /// <summary>
    ///   Implements an assignmentRepository
    /// </summary>
    public class AssignmentRepository : GenericRepository<Assignment>, IAssignmentRepository
    {
        /// <summary>Initializes a new instance of the <see cref="AssignmentRepository" /> class.</summary>
        /// <param name="dbContext">The database context.</param>
        public AssignmentRepository(TaskDbContext dbContext) : base(dbContext)
        {

        }

        /// <summary>Gets all assignments with details asynchronous.</summary>
        /// <returns>The Assignments</returns>
        public async Task<IEnumerable<Assignment>> GetAllWithDetailsAsync()
        {
            return await dbContext.Assignments
                .Include(t => t.Manager)
                .Include(t => t.Status)
                .Include(t => t.Project)
                .Include(t => t.UserProjects)
                    .ThenInclude(t => t.User)
                .ToListAsync();
        }

        /// <summary>Gets an assignment by identifier with details asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The Assignment</returns>
        public async Task<Assignment> GetByIdWithDetailsAsync(int id)
        {
            return await dbContext.Assignments
                .Include(t => t.Manager)
                .Include(t => t.Status)
                .Include(t => t.Project)
                .Include(t => t.UserProjects)
                    .ThenInclude(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
