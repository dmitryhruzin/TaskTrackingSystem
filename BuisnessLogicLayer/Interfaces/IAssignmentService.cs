using System.Linq.Expressions;

namespace BuisnessLogicLayer.Interfaces;

public interface IAssignmentService : ICrud<Assignment>
{
    Task<IReadOnlyCollection<Assignment>> GetByExpressionAsync(Expression<Func<Assignment, bool>> expression, CancellationToken cancellationToken);
}