using MediatR;

namespace BuisnessLogicLayer.Requests.Positions;

public record DeletePositionRequest(int Id) : IRequest;