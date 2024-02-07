using System.Linq.Expressions;
using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Positions;
using BuisnessLogicLayer.Responses.Positions;
using MediatR;

namespace BuisnessLogicLayer.Commands.Positions.GetPositionsByUserId;

public class GetPositionsByUserIdCommandHandler : IRequestHandler<GetPositionsByUserIdRequest, GetPositionsResponse>
{
    private readonly IPositionService _positionService;
    private readonly IMapper _mapper;

    public GetPositionsByUserIdCommandHandler(
        IPositionService positionService,
        IMapper mapper)
    {
        _positionService = positionService;
        _mapper = mapper;
    }

    public async Task<GetPositionsResponse> Handle(GetPositionsByUserIdRequest request, CancellationToken cancellationToken)
    {
        Expression<Func<Position, bool>> expression = p => p.UserProjects.Any(up => up.UserId == request.UserId);
        
        var models = await _positionService.GetAllByExpressionAsync(expression, cancellationToken);

        return new(_mapper.Map<IReadOnlyCollection<GetPositionResponse>>(models));
    }
}