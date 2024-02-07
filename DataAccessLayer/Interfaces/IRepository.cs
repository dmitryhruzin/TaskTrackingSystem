using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<IReadOnlyCollection<T>> GetAllAsync(CancellationToken cancellationToken);
    
    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);
    
    Task AddAsync(T entity, CancellationToken cancellationToken);
    
    void Delete(T entity);
    
    Task DeleteByIdAsync(int id, CancellationToken cancellationToken);
    
    void Update(T entity);
}