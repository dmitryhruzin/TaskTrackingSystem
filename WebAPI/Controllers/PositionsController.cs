using BuisnessLogicLayer.Requests.Positions;
using BuisnessLogicLayer.Responses.Positions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PositionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PositionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "User, Manager")]
    public async Task<ActionResult<GetPositionResponse>> GetPositionById(int id)
    {
        var response = await _mediator.Send(new GetPositionRequest(id));

        return Ok(response);
    }

    [HttpGet]
    [Authorize(Roles = "User, Manager")]
    public async Task<ActionResult<GetPositionsResponse>> GetPositions()
    {
        var response = await _mediator.Send(new GetPositionsRequest());

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> AddPosition(AddPositionRequest request)
    {
        await _mediator.Send(request);

        return Ok();
    }

    [HttpPut]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> UpdatePosition(UpdatePositionRequest request)
    {
        await _mediator.Send(request);

        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> DeletePosition(int id)
    {
        await _mediator.Send(new DeletePositionRequest(id));

        return Ok();
    }
}