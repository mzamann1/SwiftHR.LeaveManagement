using MediatR;
using Microsoft.AspNetCore.Mvc;
using SwiftHR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using SwiftHR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using SwiftHR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using SwiftHR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using SwiftHR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SwiftHR.LeaveManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/<LeaveTypesController>
    [HttpGet]
    public async Task<ActionResult<List<LeaveTypeDto>>> Get()
    {
        var leaveTypes = await _mediator.Send(new GetLeaveTypesQuery());
        return Ok(leaveTypes);
    }

    // GET api/<LeaveTypesController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveTypeDto>> Get(int id)
    {
        var leaveType = await _mediator.Send(new GetLeaveTypesDetailQuery(id));

        if (leaveType is null)
            return NotFound();

        return Ok(leaveType);
    }

    // POST api/<LeaveTypesController>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateLeaveTypeCommand leaveType)
    {
        var response = await _mediator.Send(leaveType);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    // PUT api/<LeaveTypesController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(UpdateLeaveTypeCommand leavetype)
    {
        await _mediator.Send(leavetype);
        return NoContent();
    }

    // DELETE api/<LeaveTypesController>/5
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteLeaveTypeCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}