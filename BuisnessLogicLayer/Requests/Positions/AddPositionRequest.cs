using MediatR;

namespace BuisnessLogicLayer.Requests.Positions;

public record AddPositionRequest(
    string Name,
    string Description) : IRequest;