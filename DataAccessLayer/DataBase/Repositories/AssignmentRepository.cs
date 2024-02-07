using System.Linq.Expressions;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DataBase.Repositories;

public class AssignmentRepository : GenericRepository<Assignment>, IAssignmentRepository 
{
    public AssignmentRepository(TaskDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyCollection<Assignment>> GetAllWithDetailsAsync(CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(t => t.Manager)
            .Include(t => t.Status)
            .Include(t => t.Project)
            .Include(t => t.UserProjects)
                .ThenInclude(t => t.User)
            .ToListAsync(cancellationToken);
    }

    public async Task<Assignment> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(t => t.Manager)
            .Include(t => t.Status)
            .Include(t => t.Project)
            .Include(t => t.UserProjects)
                .ThenInclude(t => t.User)
            .FirstAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Assignment>> GetByExpressionAsync(Expression<Func<Assignment, bool>> expression, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(t => t.Manager)
            .Include(t => t.Status)
            .Include(t => t.Project)
            .Include(t => t.UserProjects)
                .ThenInclude(t => t.User)
            .Where(expression)
            .ToListAsync(cancellationToken);
    }
}