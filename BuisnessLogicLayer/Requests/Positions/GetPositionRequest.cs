using BuisnessLogicLayer.Responses.Positions;
using MediatR;

namespace BuisnessLogicLayer.Requests.Positions;

public record GetPositionRequest(int Id) : IRequest<GetPositionResponse>;