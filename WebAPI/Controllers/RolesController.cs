using BuisnessLogicLayer.Requests.Roles;
using BuisnessLogicLayer.Responses.Roles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RolesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<GetRolesResponse>> GetRoles()
    {
        var response = await _mediator.Send(new GetRolesRequest());
        
        return Ok(response);
    }
    
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<GetRoleResponse>> GetRoleById(int id)
    {
        var response = await _mediator.Send(new GetRoleRequest(id));
        
        return Ok(response);
    }
    
    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> AddRole([FromBody] AddRoleRequest request)
    {
        await _mediator.Send(request);

        return Ok();
    }
    
    [HttpPut]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> UpdateRole([FromBody] UpdateRoleRequest request)
    {
        await _mediator.Send(request);

        return Ok();
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> DeleteRole(int id)
    {
        await _mediator.Send(new DeleteRoleRequest(id));

        return Ok();
    }
}