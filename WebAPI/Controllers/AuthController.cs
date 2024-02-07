using BuisnessLogicLayer.Requests.Users;
using BuisnessLogicLayer.Responses.Tokens;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<ActionResult<GetTokenResponse>> Login([FromBody] LoginRequest request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
        
        [HttpPost("forgot")]
        public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            await _mediator.Send(request);

            return Ok();
        }
        
        [HttpPost("reset")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            await _mediator.Send(request);

            return Ok();
        }
    }
}
