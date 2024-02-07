using BuisnessLogicLayer.Responses.Tokens;
using MediatR;

namespace BuisnessLogicLayer.Requests.Users;

public record LoginRequest(string Login, string Password) : IRequest<GetTokenResponse>;