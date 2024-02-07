using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Positions;
using FluentValidation;
using MediatR;

namespace BuisnessLogicLayer.Commands.Positions.UpdatePosition;

public class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionRequest>
{
    private readonly IMapper _mapper;
    private readonly IPositionService _positionService;
    private readonly IValidator<UpdatePositionRequest> _validator;

    public UpdatePositionCommandHandler(
        IMapper mapper,
        IPositionService positionService,
        IValidator<UpdatePositionRequest> validator)
    {
        _mapper = mapper;
        _positionService = positionService;
        _validator = validator;
    }
    
    public async Task Handle(UpdatePositionRequest request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var model = _mapper.Map<Position>(request);

        await _positionService.UpdateAsync(model, cancellationToken);
    }
}