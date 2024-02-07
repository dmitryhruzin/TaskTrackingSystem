using MediatR;

namespace BuisnessLogicLayer.Requests.Projects;

public record DeleteProjectRequest(int Id) : IRequest;