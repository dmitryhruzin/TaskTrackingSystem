using System.Security.Claims;

namespace BuisnessLogicLayer.Interfaces
{
    /// <summary>
    ///   Describes a generate token service
    /// </summary>
    public interface IGenerateTokenService
    {
        /// <summary>Generates the access token.</summary>
        /// <param name="claims">The claims.</param>
        /// <returns>
        ///   Access Token
        /// </returns>
        string GenerateAccessToken(IEnumerable<Claim> claims);

        /// <summary>Generates the refresh token.</summary>
        /// <returns>
        ///   Refresh Token
        /// </returns>
        string GenerateRefreshToken();

        /// <summary>Gets the principal from expired token.</summary>
        /// <param name="token">The token.</param>
        /// <returns>
        ///   ClaimsPrincipal
        /// </returns>
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
