using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.DataBase.Repositories
{
    /// <summary>
    ///   Implements a projectStatusRepository
    /// </summary>
    public class ProjectStatusRepository : GenericRepository<ProjectStatus>, IProjectStatusRepository
    {
        /// <summary>Initializes a new instance of the <see cref="ProjectStatusRepository" /> class.</summary>
        /// <param name="dbContext">The database context.</param>
        public ProjectStatusRepository(TaskDbContext dbContext) : base(dbContext)
        {

        }
    }
}
