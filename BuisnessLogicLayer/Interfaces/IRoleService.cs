using System.Linq.Expressions;

namespace BuisnessLogicLayer.Interfaces;

public interface IRoleService : ICrud<Role>
{
    Task<IReadOnlyCollection<Role>> GetAllByExpressionAsync(Expression<Func<Role, bool>> expression, CancellationToken cancellationToken);
}