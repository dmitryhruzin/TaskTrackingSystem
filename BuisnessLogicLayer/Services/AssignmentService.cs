using System.Linq.Expressions;
using BuisnessLogicLayer.Interfaces;

namespace BuisnessLogicLayer.Services;

public class AssignmentService : IAssignmentService
{
    private readonly IUnitOfWork _unitOfWork;

    public AssignmentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IReadOnlyCollection<Assignment>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _unitOfWork.AssignmentRepository.GetAllWithDetailsAsync(cancellationToken);
    }

    public async Task<Assignment> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _unitOfWork.AssignmentRepository.GetByIdWithDetailsAsync(id, cancellationToken);
    }

    public async Task AddAsync(Assignment model, CancellationToken cancellationToken)
    {
        await _unitOfWork.AssignmentRepository.AddAsync(model, cancellationToken);

        await _unitOfWork.SaveAsync(cancellationToken);
    }

    public async Task UpdateAsync(Assignment model, CancellationToken cancellationToken)
    {
        _unitOfWork.AssignmentRepository.Update(model);

        await _unitOfWork.SaveAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _unitOfWork.AssignmentRepository.DeleteByIdAsync(id, cancellationToken);

        await _unitOfWork.SaveAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<Assignment>> GetByExpressionAsync(Expression<Func<Assignment, bool>> expression, CancellationToken cancellationToken)
    {
        return await _unitOfWork.AssignmentRepository.GetByExpressionAsync(expression, cancellationToken);
    }
}