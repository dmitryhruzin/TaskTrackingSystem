using BuisnessLogicLayer.Responses.Users;
using MediatR;

namespace BuisnessLogicLayer.Requests.Users;

public record GetUserByEmailRequest(string Email) : IRequest<GetUserResponse>;