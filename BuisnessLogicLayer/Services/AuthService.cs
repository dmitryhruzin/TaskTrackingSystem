using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;

namespace BuisnessLogicLayer.Services
{
    /// <summary>
    ///   Implements a authService
    /// </summary>
    public class AuthService : IAuthService
    {
        readonly UserManager<User> userManager;

        readonly IGenerateTokenService generateTokenService;

        readonly IEmailService emailService;

        /// <summary>Initializes a new instance of the <see cref="AuthService" /> class.</summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="generateTokenService">The generate token service.</param>
        public AuthService(UserManager<User> userManager, 
            IGenerateTokenService generateTokenService, 
            IEmailService emailService)
        {
            this.userManager = userManager;
            this.generateTokenService = generateTokenService;
            this.emailService = emailService;
        }

        /// <summary>Logins.</summary>
        /// <param name="model">Login model.</param>
        /// <returns>TokenModel</returns>
        /// <exception cref="BuisnessLogicLayer.Validation.TaskTrackingException">User not found</exception>
        public async Task<TokenModel> Login(LoginModel model)
        {
            Validate(model);

            var user = await userManager.FindByEmailAsync(model.Login);

            if (user is null || !await userManager.CheckPasswordAsync(user, model.Password))
                throw new TaskTrackingException("User not found");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            foreach (var item in await userManager.GetRolesAsync(user))
                claims.Add(new Claim(ClaimTypes.Role, item));

            var accessToken = generateTokenService.GenerateAccessToken(claims);

            var refreshToken = generateTokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;

            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            await userManager.UpdateAsync(user);

            return new TokenModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task ForgotPassword(ForgotPasswordModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user is not null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);

                var parameters = new Dictionary<string, string>
                {
                    {"token", token },
                    {"email", model.Email }
                };

                var callback = QueryHelpers.AddQueryString(model.ClientURI, parameters);

                var message = new MessageModel(model.Email,
                    "Password reset code.",
                    $"Click this link to reset your password:\n{callback}\n\n" +
                    $"If it wasn't you, ignore this email please.\n" +
                    $"Thanks, The Task tracking system team.");

                await emailService.SendEmailAsync(message);
            }
        }

        public async Task ResetPassword(ResetPasswordModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            await userManager.ResetPasswordAsync(user, model.Token, model.Password);
        }

        static void Validate(LoginModel model)
        {
            if (model is null)
                throw new TaskTrackingException("Model is null");

            if (string.IsNullOrEmpty(model.Login) || string.IsNullOrEmpty(model.Password))
                throw new TaskTrackingException("Login or password is null or empty");
        }
    }
}
