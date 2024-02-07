using System.Linq.Expressions;

namespace BuisnessLogicLayer.Interfaces;

public interface IPositionService : ICrud<Position>
{
    Task<IReadOnlyCollection<Position>> GetAllByExpressionAsync(Expression<Func<Position, bool>> expression, CancellationToken cancellationToken);
}