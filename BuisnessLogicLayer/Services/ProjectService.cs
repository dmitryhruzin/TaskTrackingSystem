using System.Linq.Expressions;
using BuisnessLogicLayer.Interfaces;

namespace BuisnessLogicLayer.Services;

public class ProjectService : IProjectService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProjectService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IReadOnlyCollection<Project>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _unitOfWork.ProjectRepository.GetAllWithDetailsAsync(cancellationToken);
    }

    public async Task<Project> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _unitOfWork.ProjectRepository.GetByIdWithDetailsAsync(id, cancellationToken);
    }

    public async Task AddAsync(Project model, CancellationToken cancellationToken)
    {
        await _unitOfWork.ProjectRepository.AddAsync(model, cancellationToken);

        await _unitOfWork.SaveAsync(cancellationToken);
    }

    public async Task UpdateAsync(Project model, CancellationToken cancellationToken)
    {
        _unitOfWork.ProjectRepository.Update(model);

        await _unitOfWork.SaveAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _unitOfWork.ProjectRepository.DeleteByIdAsync(id, cancellationToken);

        await _unitOfWork.SaveAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<Project>> GetAllByExpressionAsync(Expression<Func<Project, bool>> expression, CancellationToken cancellationToken)
    {
        return await _unitOfWork.ProjectRepository.GetByExpressionAsync(expression, cancellationToken);
    }
}