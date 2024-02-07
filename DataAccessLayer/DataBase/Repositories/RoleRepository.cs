using System.Linq.Expressions;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DataBase.Repositories;

public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    public RoleRepository(TaskDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyCollection<Role>> GetAllByExpressionAsync(Expression<Func<Role, bool>> expression, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(r=>r.Users)
            .Where(expression)
            .ToListAsync(cancellationToken);
    }
}