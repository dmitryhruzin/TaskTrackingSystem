using BuisnessLogicLayer.Responses.Projects;
using MediatR;

namespace BuisnessLogicLayer.Requests.Projects;

public record GetProjectsByUserIdRequest(int UserId) : IRequest<GetProjectsResponse>;