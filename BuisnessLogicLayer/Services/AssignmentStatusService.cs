using BuisnessLogicLayer.Interfaces;

namespace BuisnessLogicLayer.Services;

public class AssignmentStatusService : IAssignmentStatusService
{
    private readonly IUnitOfWork _unitOfWork;

    public AssignmentStatusService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IReadOnlyCollection<AssignmentStatus>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _unitOfWork.AssignmentStatusRepository.GetAllAsync(cancellationToken);
    }

    public async Task<AssignmentStatus> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _unitOfWork.AssignmentStatusRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task AddAsync(AssignmentStatus model, CancellationToken cancellationToken)
    {
        await _unitOfWork.AssignmentStatusRepository.AddAsync(model, cancellationToken);

        await _unitOfWork.SaveAsync(cancellationToken);
    }

    public async Task UpdateAsync(AssignmentStatus model, CancellationToken cancellationToken)
    {
        _unitOfWork.AssignmentStatusRepository.Update(model);

        await _unitOfWork.SaveAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _unitOfWork.AssignmentStatusRepository.DeleteByIdAsync(id, cancellationToken);

        await _unitOfWork.SaveAsync(cancellationToken);
    }
}