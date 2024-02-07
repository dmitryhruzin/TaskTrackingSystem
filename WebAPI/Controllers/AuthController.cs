using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    ///   AuthController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IAuthService authService;

        /// <summary>Initializes a new instance of the <see cref="AuthController" /> class.</summary>
        /// <param name="authService">The authentication service.</param>
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        /// <summary>Logins.</summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns>
        ///   ObjectResult
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<TokenModel>> Login([FromBody] LoginModel loginModel)
        {
            var model = await authService.Login(loginModel);

            return Ok(model);
        }

        /// <summary>Forgot Password.</summary>
        /// <param name="model">The forgot password model.</param>
        /// <returns>
        ///   ObjectResult
        /// </returns>
        [HttpPost("forgot")]
        public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
        {
            await authService.ForgotPassword(model);

            return Ok();
        }

        /// <summary>Reset Password.</summary>
        /// <param name="model">The reset password model.</param>
        /// <returns>
        ///   ObjectResult
        /// </returns>
        [HttpPost("reset")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            await authService.ResetPassword(model);

            return Ok();
        }
    }
}
