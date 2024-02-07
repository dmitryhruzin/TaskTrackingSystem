using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Positions;
using MediatR;

namespace BuisnessLogicLayer.Commands.Positions.DeletePosition;

public class DeletePositionCommandHandler : IRequestHandler<DeletePositionRequest>
{
    private readonly IPositionService _positionService;

    public DeletePositionCommandHandler(IPositionService positionService)
    {
        _positionService = positionService;
    }
    
    public async Task Handle(DeletePositionRequest request, CancellationToken cancellationToken)
    {
        await _positionService.DeleteAsync(request.Id, cancellationToken);
    }
}