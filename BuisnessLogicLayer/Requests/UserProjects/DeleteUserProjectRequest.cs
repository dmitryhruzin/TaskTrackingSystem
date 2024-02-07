using MediatR;

namespace BuisnessLogicLayer.Requests.UserProjects;

public record DeleteUserProjectRequest(int Id) : IRequest;