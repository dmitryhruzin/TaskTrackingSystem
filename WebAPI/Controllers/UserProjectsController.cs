using BuisnessLogicLayer.Requests.UserProjects;
using BuisnessLogicLayer.Responses.UserProjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public UserProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [Authorize(Roles = "User, Manager")]
        public async Task<ActionResult<GetUserProjectsResponse>> GetUserProjects()
        {
            var response = await _mediator.Send(new GetUserProjectsRequest());
            
            return Ok(response);
        }
        
        [HttpGet("{id}")]
        [Authorize(Roles = "User, Manager")]
        public async Task<ActionResult<GetUserProjectResponse>> GetUserProjectById(int id)
        {
            var response = await _mediator.Send(new GetUserProjectRequest(id));
            
            return Ok(response);
        }

        [HttpPost("range")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> AddUserProjects([FromBody] AddUserProjectsRequest request)
        {
            await _mediator.Send(request);

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> AddUserProject([FromBody] AddUserProjectRequest request)
        {
            await _mediator.Send(request);

            return Ok();
        }
        
        [HttpPut]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> UpdateUserProject([FromBody] UpdateUserProjectRequest request)
        {
            await _mediator.Send(request);

            return Ok();
        }
        
        [HttpDelete]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> DeleteUserProjects([FromQuery] DeleteUserProjectsRequest request)
        {
            await _mediator.Send(request);

            return Ok();
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> DeleteUserProject(int id)
        {
            await _mediator.Send(new DeleteUserProjectRequest(id));

            return Ok();
        }
    }
}
