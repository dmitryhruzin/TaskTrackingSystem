using System.Linq.Expressions;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DataBase.Repositories;

public class ProjectRepository : GenericRepository<Project>, IProjectRepository
{
    public ProjectRepository(TaskDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyCollection<Project>> GetAllWithDetailsAsync(CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(t => t.Status)
            .Include(t => t.Tasks)
                .ThenInclude(t => t.Manager)
            .Include(t => t.Tasks)
                .ThenInclude(t => t.UserProjects)
                .ThenInclude(t => t.User)
            .ToListAsync(cancellationToken);
    }

    public async Task<Project> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(t => t.Status)
            .Include(t => t.Tasks)
                .ThenInclude(t => t.Manager)
            .Include(t => t.Tasks)
                .ThenInclude(t => t.UserProjects)
                .ThenInclude(t => t.User)
            .FirstAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Project>> GetByExpressionAsync(Expression<Func<Project, bool>> expression, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(t => t.Status)
            .Include(t => t.Tasks)
                .ThenInclude(t => t.Manager)
            .Include(t => t.Tasks)
                .ThenInclude(t => t.UserProjects)
                .ThenInclude(t => t.User)
            .Where(expression)
            .ToListAsync(cancellationToken);
    }
}