using System.Linq.Expressions;

namespace BuisnessLogicLayer.Interfaces;

public interface IUserService : ICrud<User>
{
    Task<IReadOnlyCollection<User>> GetAllByExpressionAsync(Expression<Func<User, bool>> expression, CancellationToken cancellationToken);
}