using BuisnessLogicLayer.Responses.UserProjects;
using MediatR;

namespace BuisnessLogicLayer.Requests.UserProjects;

public record GetUserProjectRequest(int Id) : IRequest<GetUserProjectResponse>;