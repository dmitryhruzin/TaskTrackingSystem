using BuisnessLogicLayer.Responses.Positions;
using MediatR;

namespace BuisnessLogicLayer.Requests.Positions;

public record GetPositionsByUserIdRequest(int UserId) : IRequest<GetPositionsResponse>;