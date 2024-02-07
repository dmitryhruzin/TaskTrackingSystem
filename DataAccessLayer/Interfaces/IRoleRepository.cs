using System.Linq.Expressions;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces;

public interface IRoleRepository : IRepository<Role>
{
    Task<IReadOnlyCollection<Role>> GetAllByExpressionAsync(Expression<Func<Role, bool>> expression, CancellationToken cancellationToken);
}