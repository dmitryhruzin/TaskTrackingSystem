namespace BuisnessLogicLayer.Responses.Tokens;

public record GetTokenResponse(
    string AccessToken,
    string RefreshToken);