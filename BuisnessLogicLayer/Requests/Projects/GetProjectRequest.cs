using BuisnessLogicLayer.Responses.Projects;
using MediatR;

namespace BuisnessLogicLayer.Requests.Projects;

public record GetProjectRequest(int Id) : IRequest<GetProjectResponse>;