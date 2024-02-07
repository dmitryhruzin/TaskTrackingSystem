using System.Linq.Expressions;
using BuisnessLogicLayer.Interfaces;

namespace BuisnessLogicLayer.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IReadOnlyCollection<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _unitOfWork.UserRepository.GetAllWithDetailsAsync(cancellationToken);
    }

    public async Task<User> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _unitOfWork.UserRepository.GetByIdWithDetailsAsync(id, cancellationToken);
    }

    public async Task AddAsync(User model, CancellationToken cancellationToken)
    {
        await _unitOfWork.UserRepository.AddAsync(model, cancellationToken);

        await _unitOfWork.SaveAsync(cancellationToken);
    }

    public async Task UpdateAsync(User model, CancellationToken cancellationToken)
    {
        _unitOfWork.UserRepository.Update(model);

        await _unitOfWork.SaveAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _unitOfWork.UserRepository.DeleteByIdAsync(id, cancellationToken);

        await _unitOfWork.SaveAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<User>> GetAllByExpressionAsync(Expression<Func<User, bool>> expression, CancellationToken cancellationToken)
    {
        return await _unitOfWork.UserRepository.GetAllByExpressionAsync(expression, cancellationToken);
    }
}