using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.DataBase.Repositories;

public class ProjectStatusRepository : GenericRepository<ProjectStatus>, IProjectStatusRepository
{
    public ProjectStatusRepository(TaskDbContext dbContext) : base(dbContext)
    {
    }
}