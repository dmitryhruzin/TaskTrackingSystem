using BuisnessLogicLayer.Responses.Projects;
using MediatR;

namespace BuisnessLogicLayer.Requests.Projects;

public record GetProjectsRequest() : IRequest<GetProjectsResponse>;