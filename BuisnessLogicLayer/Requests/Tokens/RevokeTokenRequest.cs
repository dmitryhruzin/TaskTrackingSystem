using MediatR;

namespace BuisnessLogicLayer.Requests.Tokens;

public record RevokeTokenRequest(string AccessToken) : IRequest;