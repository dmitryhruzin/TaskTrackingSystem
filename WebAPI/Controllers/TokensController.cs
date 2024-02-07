using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    ///   TokensController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        readonly ITokenService tokenService;

        /// <summary>Initializes a new instance of the <see cref="TokensController" /> class.</summary>
        /// <param name="tokenService">The token service.</param>
        public TokensController(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        /// <summary>Refreshes the token.</summary>
        /// <param name="tokenModel">The token model.</param>
        /// <returns>
        ///   OkObjectResult
        /// </returns>
        [HttpPost("refresh")]
        public async Task<ActionResult<TokenModel>> Refresh([FromBody] TokenModel tokenModel)
        {
            var model = await tokenService.Refresh(tokenModel);

            return Ok(model);
        }

        /// <summary>Revokes the token.</summary>
        /// <param name="tokenModel">The token model.</param>
        /// <returns>
        ///   OkObjectResult
        /// </returns>
        [HttpPost("revoke"), Authorize]
        public async Task<ActionResult> Revoke([FromBody] TokenModel tokenModel)
        {
            await tokenService.Revoke(tokenModel);

            return Ok();
        }
    }
}
