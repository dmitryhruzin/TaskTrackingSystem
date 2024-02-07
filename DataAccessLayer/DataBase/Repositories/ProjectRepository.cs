using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DataBase.Repositories
{
    /// <summary>
    ///  Implements a projectRepository
    /// </summary>
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        /// <summary>Initializes a new instance of the <see cref="ProjectRepository" /> class.</summary>
        /// <param name="dbContext">The database context.</param>
        public ProjectRepository(TaskDbContext dbContext) : base(dbContext)
        {

        }

        /// <summary>Gets all projects with details asynchronous.</summary>
        /// <returns>Projects</returns>
        public async Task<IEnumerable<Project>> GetAllWithDetailsAsync()
        {
            return await dbContext.Projects
                .Include(t => t.Status)
                .Include(t => t.Tasks)
                    .ThenInclude(t => t.Manager)
                .Include(t => t.Tasks)
                    .ThenInclude(t => t.UserProjects)
                    .ThenInclude(t => t.User)
                .ToListAsync();
        }

        /// <summary>Gets a project by identifier with details asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Project</returns>
        public async Task<Project> GetByIdWithDetailsAsync(int id)
        {
            return await dbContext.Projects
                .Include(t => t.Status)
                .Include(t => t.Tasks)
                    .ThenInclude(t => t.Manager)
                .Include(t => t.Tasks)
                    .ThenInclude(t => t.UserProjects)
                    .ThenInclude(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
