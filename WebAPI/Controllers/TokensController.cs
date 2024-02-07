using BuisnessLogicLayer.Requests.Tokens;
using BuisnessLogicLayer.Responses.Tokens;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public TokensController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost("refresh")]
        public async Task<ActionResult<GetTokenResponse>> Refresh([FromBody] RefreshTokenRequest request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
        
        [HttpPost("revoke"), Authorize]
        public async Task<ActionResult> Revoke([FromBody] RevokeTokenRequest request)
        {
            await _mediator.Send(request);

            return Ok();
        }
    }
}
