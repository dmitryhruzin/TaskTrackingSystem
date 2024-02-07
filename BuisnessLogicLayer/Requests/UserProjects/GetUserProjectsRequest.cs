using BuisnessLogicLayer.Responses.UserProjects;
using MediatR;

namespace BuisnessLogicLayer.Requests.UserProjects;

public record GetUserProjectsRequest() : IRequest<GetUserProjectsResponse>;