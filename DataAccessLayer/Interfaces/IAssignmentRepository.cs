using System.Linq.Expressions;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces;

public interface IAssignmentRepository : IRepository<Assignment>
{
    Task<IReadOnlyCollection<Assignment>> GetAllWithDetailsAsync(CancellationToken cancellationToken);
    
    Task<Assignment> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken);
    
    Task<IReadOnlyCollection<Assignment>> GetByExpressionAsync(Expression<Func<Assignment, bool>> expression, CancellationToken cancellationToken);
}