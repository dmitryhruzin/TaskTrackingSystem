using System.Linq.Expressions;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DataBase.Repositories;

public class PositionRepository : GenericRepository<Position>, IPositionRepository
{
    public PositionRepository(TaskDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyCollection<Position>> GetAllByExpressionAsync(Expression<Func<Position, bool>> expression, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Where(expression)
            .Include(t=>t.UserProjects)
                .ThenInclude(t=>t.User)
            .ToListAsync(cancellationToken);
    }
}