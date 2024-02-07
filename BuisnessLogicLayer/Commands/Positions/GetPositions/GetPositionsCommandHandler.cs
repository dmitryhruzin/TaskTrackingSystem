using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Positions;
using BuisnessLogicLayer.Responses.Positions;
using MediatR;

namespace BuisnessLogicLayer.Commands.Positions.GetPositions;

public class GetPositionsCommandHandler : IRequestHandler<GetPositionsRequest, GetPositionsResponse>
{
    private readonly IPositionService _positionService;
    private readonly IMapper _mapper;

    public GetPositionsCommandHandler(
        IPositionService positionService,
        IMapper mapper)
    {
        _positionService = positionService;
        _mapper = mapper;
    }

    public async Task<GetPositionsResponse> Handle(GetPositionsRequest request, CancellationToken cancellationToken)
    {
        var models = await _positionService.GetAllAsync(cancellationToken);

        return new(_mapper.Map<IReadOnlyCollection<GetPositionResponse>>(models));
    }
}