using System.Linq.Expressions;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces;

public interface IPositionRepository : IRepository<Position>
{
    Task<IReadOnlyCollection<Position>> GetAllByExpressionAsync(Expression<Func<Position, bool>> expression, CancellationToken cancellationToken);

}