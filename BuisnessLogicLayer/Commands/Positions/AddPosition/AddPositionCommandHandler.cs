using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Positions;
using FluentValidation;
using MediatR;

namespace BuisnessLogicLayer.Commands.Positions.AddPosition;

public class AddPositionCommandHandler : IRequestHandler<AddPositionRequest>
{
    private readonly IMapper _mapper;
    private readonly IPositionService _positionService;
    private readonly IValidator<AddPositionRequest> _validator;

    public AddPositionCommandHandler(
        IMapper mapper,
        IPositionService positionService,
        IValidator<AddPositionRequest> validator)
    {
        _mapper = mapper;
        _positionService = positionService;
        _validator = validator;
    }
    
    public async Task Handle(AddPositionRequest request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var model = _mapper.Map<Position>(request);

        await _positionService.AddAsync(model, cancellationToken);
    }
}