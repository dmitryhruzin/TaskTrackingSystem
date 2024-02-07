using System.Linq.Expressions;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DataBase.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(TaskDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyCollection<User>> GetAllByExpressionAsync(Expression<Func<User, bool>> expression, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(t => t.Tasks)
                .ThenInclude(t => t.Project)
            .Include(t => t.UserProjects)
                .ThenInclude(t => t.Task.Project)
            .Include(t => t.UserProjects)
                .ThenInclude(t => t.Position)
            .Include(t => t.Roles)
            .Where(expression)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<User>> GetAllWithDetailsAsync(CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(t => t.Tasks)
                .ThenInclude(t => t.Project)
            .Include(t => t.UserProjects)
                .ThenInclude(t => t.Task.Project)
            .Include(t => t.UserProjects)
                .ThenInclude(t => t.Position)
            .Include(t => t.Roles)
            .ToListAsync(cancellationToken);
    }

    public async Task<User> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(t => t.Tasks)
                .ThenInclude(t => t.Project)
            .Include(t => t.UserProjects)
                .ThenInclude(t => t.Task.Project)
            .Include(t => t.UserProjects)
                .ThenInclude(t => t.Position)
            .Include(t => t.Roles)
            .FirstAsync(t => t.Id == id, cancellationToken);
    }
}