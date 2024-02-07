using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DataBase.Repositories;

public class UserProjectRepository : GenericRepository<UserProject>, IUserProjectRepository
{
    public UserProjectRepository(TaskDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyCollection<UserProject>> GetAllWithDetailsAsync(CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(t => t.Task.Manager)
            .Include(t => t.Task.Status)
            .Include(t => t.User)
            .Include(t => t.Position)
            .Include(t => t.Task.Project.Status)
            .ToListAsync(cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<UserProject> entities, CancellationToken cancellationToken)
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);
    }

    public async Task DeleteRangeAsync(IEnumerable<int> ids, CancellationToken cancellationToken)
    {
        var entities = await _dbSet
            .Where(e => ids.Contains(e.Id))
            .ToListAsync(cancellationToken);

        _dbSet.RemoveRange(entities);
    }
}