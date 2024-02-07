using BuisnessLogicLayer.Interfaces;

namespace BuisnessLogicLayer.Services;

public class ProjectStatusService : IProjectStatusService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProjectStatusService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IReadOnlyCollection<ProjectStatus>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _unitOfWork.ProjectStatusRepository.GetAllAsync(cancellationToken);
    }

    public async Task<ProjectStatus> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _unitOfWork.ProjectStatusRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task AddAsync(ProjectStatus model, CancellationToken cancellationToken)
    {
        await _unitOfWork.ProjectStatusRepository.AddAsync(model, cancellationToken);

        await _unitOfWork.SaveAsync(cancellationToken);
    }

    public async Task UpdateAsync(ProjectStatus model, CancellationToken cancellationToken)
    {
        _unitOfWork.ProjectStatusRepository.Update(model);

        await _unitOfWork.SaveAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _unitOfWork.ProjectStatusRepository.DeleteByIdAsync(id, cancellationToken);

        await _unitOfWork.SaveAsync(cancellationToken);
    }
}