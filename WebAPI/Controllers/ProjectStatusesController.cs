using BuisnessLogicLayer.Requests.ProjectStatuses;
using BuisnessLogicLayer.Responses.ProjectStatuses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectStatusesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectStatusesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [Authorize(Roles = "User, Manager")]
    public async Task<ActionResult<GetProjectStatusesResponse>> GetStatuses()
    {
        var response = await _mediator.Send(new GetProjectStatusesRequest());
        
        return Ok(response);
    }
        
    [HttpGet("{id}")]
    [Authorize(Roles = "User, Manager")]
    public async Task<ActionResult<GetProjectStatusResponse>> GetStatus(int id)
    {
        var response = await _mediator.Send(new GetProjectStatusRequest(id));
        
        return Ok(response);
    }
        
    [HttpPost]
    [Authorize(Roles = "Manager")]
    public async Task<ActionResult> AddStatus([FromBody] AddProjectStatusRequest request)
    {
        await _mediator.Send(request);

        return Ok();
    }
        
    [HttpPut]
    [Authorize(Roles = "Manager")]
    public async Task<ActionResult> UpdateStatus([FromBody] UpdateProjectStatusRequest request)
    {
        await _mediator.Send(request);

        return Ok();
    }
        
    [HttpDelete("{id}")]
    [Authorize(Roles = "Manager")]
    public async Task<ActionResult> DeleteStatus(int id)
    {
        await _mediator.Send(new DeleteProjectStatusRequest(id));

        return Ok();
    }
}