using MediatR;

namespace BuisnessLogicLayer.Requests.Positions;

public record UpdatePositionRequest(
    int Id,
    string Name,
    string Description) : IRequest;