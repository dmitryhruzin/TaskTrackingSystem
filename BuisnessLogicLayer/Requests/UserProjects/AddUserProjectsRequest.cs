using MediatR;

namespace BuisnessLogicLayer.Requests.UserProjects;

public record AddUserProjectsRequest(IEnumerable<AddUserProjectRequest> UserProjects) : IRequest;