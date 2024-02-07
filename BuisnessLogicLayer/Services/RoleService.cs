using System.Linq.Expressions;
using BuisnessLogicLayer.Interfaces;

namespace BuisnessLogicLayer.Services;

public class RoleService : IRoleService
{
    private readonly IUnitOfWork _unitOfWork;

    public RoleService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IReadOnlyCollection<Role>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _unitOfWork.RoleRepository.GetAllAsync(cancellationToken);
    }

    public async Task<Role> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _unitOfWork.RoleRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task AddAsync(Role model, CancellationToken cancellationToken)
    {
        await _unitOfWork.RoleRepository.AddAsync(model, cancellationToken);

        await _unitOfWork.SaveAsync(cancellationToken);
    }

    public async Task UpdateAsync(Role model, CancellationToken cancellationToken)
    {
        _unitOfWork.RoleRepository.Update(model);

        await _unitOfWork.SaveAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _unitOfWork.RoleRepository.DeleteByIdAsync(id, cancellationToken);

        await _unitOfWork.SaveAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<Role>> GetAllByExpressionAsync(Expression<Func<Role, bool>> expression, CancellationToken cancellationToken)
    {
        return await _unitOfWork.RoleRepository.GetAllByExpressionAsync(expression, cancellationToken);
    }
}