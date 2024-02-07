using BuisnessLogicLayer.Requests.AssignmentStatuses;
using BuisnessLogicLayer.Responses.Assignments;
using BuisnessLogicLayer.Responses.AssignmentStatuses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AssignmentStatusesController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssignmentStatusesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "User, Manager")]
    public async Task<ActionResult<GetAssignmentStatusesResponse>> GetStatuses()
    {
        var response = await _mediator.Send(new GetAssignmentStatusesRequest());

        return Ok(response);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "User, Manager")]
    public async Task<ActionResult<GetAssignmentStatusResponse>> GetStatus(int id)
    {
        var response = await _mediator.Send(new GetAssignmentStatusRequest(id));

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Roles = "Manager")]
    public async Task<ActionResult> AddStatus([FromBody] AddAssignmentStatusRequest request)
    {
        await _mediator.Send(request);

        return Ok();
    }

    [HttpPut]
    [Authorize(Roles = "Manager")]
    public async Task<ActionResult> UpdateStatus([FromBody] UpdateAssignmentStatusRequest request)
    {
        await _mediator.Send(request);

        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Manager")]
    public async Task<ActionResult> DeleteStatus(int id)
    {
        await _mediator.Send(new DeleteAssignmentStatusRequest(id));

        return Ok();
    }
}