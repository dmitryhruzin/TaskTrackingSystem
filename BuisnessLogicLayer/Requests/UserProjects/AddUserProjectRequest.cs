using MediatR;

namespace BuisnessLogicLayer.Requests.UserProjects;

public record AddUserProjectRequest(
    int UserId,
    int TaskId,
    int PositionId) : IRequest;