using BuisnessLogicLayer.Responses.Users;
using MediatR;

namespace BuisnessLogicLayer.Requests.Users;

public record GetUserRequest(int Id) : IRequest<GetUserResponse>;