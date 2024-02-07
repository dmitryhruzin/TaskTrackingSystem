using BuisnessLogicLayer.Models;

namespace BuisnessLogicLayer.Interfaces
{
    /// <summary>
    ///   Describes an authService
    /// </summary>
    public interface IAuthService
    {
        /// <summary>Logins.</summary>
        /// <param name="model">Login model.</param>
        /// <returns>
        ///   TokenModel
        /// </returns>
        Task<TokenModel> Login(LoginModel model);

        /// <summary>Forgot Password.</summary>
        /// <param name="model">Forgot Password Model.</param>
        Task ForgotPassword(ForgotPasswordModel model);

        /// <summary>Reset Password.</summary>
        /// <param name="model">Reset Password Model.</param>
        Task ResetPassword(ResetPasswordModel model);
    }
}
