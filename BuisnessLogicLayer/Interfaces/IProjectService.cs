using System.Linq.Expressions;

namespace BuisnessLogicLayer.Interfaces;

public interface IProjectService : ICrud<Project>
{
    Task<IReadOnlyCollection<Project>> GetAllByExpressionAsync(Expression<Func<Project, bool>> expression, CancellationToken cancellationToken);
}