using BuisnessLogicLayer.Responses.Users;
using MediatR;

namespace BuisnessLogicLayer.Requests.Users;

public record GetUsersByProjectIdRequest(int ProjectId) : IRequest<GetUsersResponse>;