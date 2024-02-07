using BuisnessLogicLayer.Responses.Tokens;
using MediatR;

namespace BuisnessLogicLayer.Requests.Tokens;

public record RefreshTokenRequest(
    string AccessToken,
    string RefreshToken) : IRequest<GetTokenResponse>;