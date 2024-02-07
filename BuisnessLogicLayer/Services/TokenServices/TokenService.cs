using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Validation;
using Microsoft.AspNetCore.Identity;

namespace BuisnessLogicLayer.Services.TokenServices
{
    /// <summary>
    ///   Implements a tokenService
    /// </summary>
    public class TokenService : ITokenService
    {
        readonly UserManager<User> userManager;

        readonly IGenerateTokenService generateTokenService;

        /// <summary>Initializes a new instance of the <see cref="TokenService" /> class.</summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="generateTokenService">The generate token service.</param>
        public TokenService(UserManager<User> userManager, IGenerateTokenService generateTokenService)
        {
            this.userManager = userManager;
            this.generateTokenService = generateTokenService;
        }

        /// <summary>Refreshes the token.</summary>
        /// <param name="model">The model.</param>
        /// <returns>TokenModel</returns>
        /// <exception cref="BuisnessLogicLayer.Validation.TaskTrackingException">Invalid request</exception>
        public async Task<TokenModel> Refresh(TokenModel model)
        {
            Validate(model);

            var accessToken = model.AccessToken;

            var refreshToken = model.RefreshToken;

            var principal = generateTokenService.GetPrincipalFromExpiredToken(accessToken);

            var username = principal.Identity.Name;

            var user = await userManager.FindByNameAsync(username);

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                throw new TaskTrackingException("Invalid request");

            var newAccessToken = generateTokenService.GenerateAccessToken(principal.Claims);

            return new TokenModel()
            {
                AccessToken = newAccessToken,
                RefreshToken = refreshToken
            };
        }

        /// <summary>Revokes the token.</summary>
        /// <param name="model">The model.</param>
        /// <exception cref="BuisnessLogicLayer.Validation.TaskTrackingException">User not found</exception>
        public async Task Revoke(TokenModel model)
        {
            Validate(model);

            var principal = generateTokenService.GetPrincipalFromExpiredToken(model.AccessToken);

            var username = principal.Identity.Name;

            var user = await userManager.FindByNameAsync(username);

            if (user is null)
                throw new TaskTrackingException("User not found");

            user.RefreshToken = null;

            await userManager.UpdateAsync(user);
        }

        static void Validate(TokenModel model)
        {
            if (model is null)
                throw new TaskTrackingException("Model is null");

            if (string.IsNullOrEmpty(model.AccessToken) || string.IsNullOrEmpty(model.RefreshToken))
                throw new TaskTrackingException("Access token or refresh token is null or empty");
        }
    }
}
