using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Positions;
using BuisnessLogicLayer.Responses.Positions;
using MediatR;

namespace BuisnessLogicLayer.Commands.Positions.GetPosition;

public class GetPositionCommandHandler : IRequestHandler<GetPositionRequest, GetPositionResponse>
{
    private readonly IPositionService _positionService;
    private readonly IMapper _mapper;

    public GetPositionCommandHandler(
        IPositionService positionService,
        IMapper mapper)
    {
        _positionService = positionService;
        _mapper = mapper;
    }
    
    public async Task<GetPositionResponse> Handle(GetPositionRequest request, CancellationToken cancellationToken)
    {
        var model = await _positionService.GetByIdAsync(request.Id, cancellationToken);

        return _mapper.Map<GetPositionResponse>(model);
    }
}