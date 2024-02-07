using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BuisnessLogicLayer.Services.TokenServices
{
    /// <summary>
    ///   Implements a generateTokenService
    /// </summary>
    public class GenerateTokenService : IGenerateTokenService
    {
        readonly IOptions<TokenOptions> options;

        /// <summary>Initializes a new instance of the <see cref="GenerateTokenService" /> class.</summary>
        /// <param name="options">The options.</param>
        public GenerateTokenService(IOptions<TokenOptions> options)
        {
            this.options = options;
        }

        /// <summary>Generates the access token.</summary>
        /// <param name="claims">The claims.</param>
        /// <returns>Access Token</returns>
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var tokenParams = options.Value;

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenParams.Secret));

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: tokenParams.Issuer,
                audience: tokenParams.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return tokenString;
        }

        /// <summary>Generates the refresh token.</summary>
        /// <returns>Refresh Token</returns>
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using var rng = RandomNumberGenerator.Create();

            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }

        /// <summary>Gets the principal from expired token.</summary>
        /// <param name="token">The token.</param>
        /// <returns>ClaimsPrincipal</returns>
        /// <exception cref="Microsoft.IdentityModel.Tokens.SecurityTokenException">Invalid token</exception>
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenParams = options.Value;

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenParams.Secret)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (!jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}
