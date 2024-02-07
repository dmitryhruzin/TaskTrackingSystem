using System.Linq.Expressions;
using BuisnessLogicLayer.Interfaces;

namespace BuisnessLogicLayer.Services;

public class PositionService : IPositionService
{
    private readonly IUnitOfWork _unitOfWork;

    public PositionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IReadOnlyCollection<Position>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _unitOfWork.PositionRepository.GetAllAsync(cancellationToken);
    }

    public async Task<Position> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _unitOfWork.PositionRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task AddAsync(Position model, CancellationToken cancellationToken)
    {
        await _unitOfWork.PositionRepository.AddAsync(model, cancellationToken);

        await _unitOfWork.SaveAsync(cancellationToken);
    }

    public async Task UpdateAsync(Position model, CancellationToken cancellationToken)
    {
        _unitOfWork.PositionRepository.Update(model);

        await _unitOfWork.SaveAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _unitOfWork.PositionRepository.DeleteByIdAsync(id, cancellationToken);

        await _unitOfWork.SaveAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<Position>> GetAllByExpressionAsync(Expression<Func<Position, bool>> expression, CancellationToken cancellationToken)
    {
        return await _unitOfWork.PositionRepository.GetAllByExpressionAsync(expression, cancellationToken);
    }
}