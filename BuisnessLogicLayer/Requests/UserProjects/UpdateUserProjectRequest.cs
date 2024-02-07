using MediatR;

namespace BuisnessLogicLayer.Requests.UserProjects;

public record UpdateUserProjectRequest(
    int Id,
    int UserId,
    int TaskId,
    int PositionId) : IRequest;