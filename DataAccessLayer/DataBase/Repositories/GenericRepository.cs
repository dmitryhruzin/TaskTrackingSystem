using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DataBase.Repositories;

public class GenericRepository<T> : IRepository<T> where T : BaseEntity
{
    protected private readonly DbSet<T> _dbSet;

    public GenericRepository(TaskDbContext dbContext)
    {
        _dbSet = dbContext.Set<T>();
    }
    
    public async Task<IReadOnlyCollection<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbSet
            .FirstAsync(e => e.Id == id, cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task DeleteByIdAsync(int id, CancellationToken cancellationToken)
    {
        Delete(await GetByIdAsync(id, cancellationToken));
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }
}