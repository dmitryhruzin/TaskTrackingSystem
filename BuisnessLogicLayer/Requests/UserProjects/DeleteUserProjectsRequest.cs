using MediatR;

namespace BuisnessLogicLayer.Requests.UserProjects;

public record DeleteUserProjectsRequest(IEnumerable<DeleteUserProjectRequest> UserProjects) : IRequest;