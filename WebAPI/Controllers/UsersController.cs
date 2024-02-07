using BuisnessLogicLayer.Requests.Assignments;
using BuisnessLogicLayer.Requests.Positions;
using BuisnessLogicLayer.Requests.Projects;
using BuisnessLogicLayer.Requests.Roles;
using BuisnessLogicLayer.Requests.Users;
using BuisnessLogicLayer.Responses.Assignments;
using BuisnessLogicLayer.Responses.Positions;
using BuisnessLogicLayer.Responses.Projects;
using BuisnessLogicLayer.Responses.Roles;
using BuisnessLogicLayer.Responses.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<GetUsersResponse>> GetUsers()
        {
            var response = await _mediator.Send(new GetUsersRequest());
            
            return Ok(response);
        }
        
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<GetUserResponse>> GetUserById(int id)
        {
            var response = await _mediator.Send(new GetUserRequest(id));
            
            return Ok(response);
        }
        
        [HttpGet("email/{email}")]
        [Authorize]
        public async Task<ActionResult<GetUserResponse>> GetUserByEmail(string email)
        {
            var response = await _mediator.Send(new GetUserByEmailRequest(email));

            return Ok(response);
        }
        
        [HttpGet("{id}/positions")]
        [Authorize(Roles = "Manager, User")]
        public async Task<ActionResult<GetPositionsResponse>> GetPositionsByUserId(int id)
        {
            var response = await _mediator.Send(new GetPositionsByUserIdRequest(id));
            
            return Ok(response);
        }
        
        [HttpGet("{id}/projects")]
        [Authorize(Roles = "Manager, User")]
        public async Task<ActionResult<GetProjectsResponse>> GetProjectsByUserId(int id)
        {
            var response = await _mediator.Send(new GetProjectsByUserIdRequest(id));
            
            return Ok(response);
        }
        
        [HttpGet("{id}/tasks")]
        [Authorize(Roles = "Manager, User")]
        public async Task<ActionResult<GetAssignmentsResponse>> GetTasksByUserId(int id)
        {
            var response = await _mediator.Send(new GetAssignmentsByUserIdRequest(id));
            
            return Ok(response);
        }
        
        [HttpGet("{id}/manager/tasks")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<GetAssignmentsResponse>> GetTasksByManagerId(int id)
        {
            var response = await _mediator.Send(new GetAssignmentsByManagerIdRequest(id));
            
            return Ok(response);
        }
        
        [HttpGet("{id}/roles")]
        [Authorize]
        public async Task<ActionResult<GetRolesResponse>> GetRolesByUserId(int id)
        {
            var response = await _mediator.Send(new GetRolesByUserIdRequest(id));
            
            return Ok(response);
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AddUser([FromBody] AddUserRequest request)
        {
            await _mediator.Send(request);

            return Ok();
        }
        
        [HttpPost("{id}/role")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> AddToRole(int id, [FromBody] GetRoleResponse response)
        {
            await _mediator.Send(new AddToRoleRequest(id, response));

            return Ok();
        }
        
        [HttpPost("{id}/roles")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> AddToRoles(int id, [FromBody] IReadOnlyCollection<GetRoleResponse> response)
        {
            await _mediator.Send(new AddToRolesRequest(id, response));

            return Ok();
        }
        
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        {
            await _mediator.Send(request);

            return Ok();
        }
        
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await _mediator.Send(new DeleteUserRequest(id));

            return Ok();
        }
        
        [HttpDelete("{id}/role")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> DeleteToRole(int id, [FromQuery] int roleId)
        {
            await _mediator.Send(new DeleteToRoleRequest(id, roleId));

            return Ok();
        }
        
        [HttpDelete("{id}/roles")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> DeleteToRoles(int id, [FromQuery] IReadOnlyCollection<int> roleIds)
        {
            await _mediator.Send(new DeleteToRolesRequest(id, roleIds));

            return Ok();
        }
    }
}
