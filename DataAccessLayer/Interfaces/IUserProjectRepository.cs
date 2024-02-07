using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces;

public interface IUserProjectRepository : IRepository<UserProject>
{
    Task<IReadOnlyCollection<UserProject>> GetAllWithDetailsAsync(CancellationToken cancellationToken);

    Task AddRangeAsync(IEnumerable<UserProject> entities, CancellationToken cancellationToken);

    Task DeleteRangeAsync(IEnumerable<int> ids, CancellationToken cancellationToken);
}