using BuisnessLogicLayer.Responses.Users;
using MediatR;

namespace BuisnessLogicLayer.Requests.Users;

public record GetUsersRequest() : IRequest<GetUsersResponse>;