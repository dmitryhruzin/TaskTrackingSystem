using BuisnessLogicLayer.Interfaces;

namespace BuisnessLogicLayer.Services;

public class UserProjectService : IUserProjectService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserProjectService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyCollection<UserProject>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _unitOfWork.UserProjectRepository.GetAllAsync(cancellationToken);
    }

    public async Task<UserProject> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _unitOfWork.UserProjectRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task AddAsync(UserProject model, CancellationToken cancellationToken)
    {
        await _unitOfWork.UserProjectRepository.AddAsync(model, cancellationToken);

        await _unitOfWork.SaveAsync(cancellationToken);
    }

    public async Task UpdateAsync(UserProject model, CancellationToken cancellationToken)
    {
        _unitOfWork.UserProjectRepository.Update(model);

        await _unitOfWork.SaveAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _unitOfWork.UserProjectRepository.DeleteByIdAsync(id, cancellationToken);

        await _unitOfWork.SaveAsync(cancellationToken);
    }

    public async Task AddUserProjectsAsync(IEnumerable<UserProject> models, CancellationToken cancellationToken)
    {
        await _unitOfWork.UserProjectRepository.AddRangeAsync(models, cancellationToken);

        await _unitOfWork.SaveAsync(cancellationToken);
    }

    public async Task DeleteUserProjectsAsync(IEnumerable<int> ids, CancellationToken cancellationToken)
    {
        await _unitOfWork.UserProjectRepository.DeleteRangeAsync(ids, cancellationToken);

        await _unitOfWork.SaveAsync(cancellationToken);
    }
}