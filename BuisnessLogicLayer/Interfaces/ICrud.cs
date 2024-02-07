namespace BuisnessLogicLayer.Interfaces;

public interface ICrud<T> where T : BaseEntity
{
    Task<IReadOnlyCollection<T>> GetAllAsync(CancellationToken cancellationToken);

    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);
    
    Task AddAsync(T model, CancellationToken cancellationToken);

    Task UpdateAsync(T model, CancellationToken cancellationToken);
    
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}