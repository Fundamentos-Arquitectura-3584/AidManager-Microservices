using MediatR;
using Microsoft.AspNetCore.Mvc;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.Queries;
using AidManager.Collaborate.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System; // For DateTime

namespace AidManager.Collaborate.Web.Controllers;

[ApiController]
[Route("api/collaborate/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IMediator _mediator;

    public EventsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // POST: api/collaborate/events
    [HttpPost]
    public async Task<ActionResult<EventDto>> CreateNewEvent([FromBody] CreateEventCommandPayload payload)
    {
        // int currentUserId = GetCurrentUserId(); // Get from auth context
        int placeholderUserId = 1; // Placeholder

        var command = new CreateEventCommand(
            payload.Name,
            payload.EventDate,
            payload.Location,
            payload.Description,
            payload.Color,
            payload.ProjectId,
            placeholderUserId // Pass the authenticated user's ID
        );
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetEventById), new { eventId = result.Id }, result);
    }

    // GET: api/collaborate/events
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventDto>>> GetAllEvents()
    {
        var query = new GetAllEventsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    // GET: api/collaborate/events/{eventId}
    [HttpGet("{eventId}")]
    public async Task<ActionResult<EventDto>> GetEventById(int eventId)
    {
        var query = new GetEventByIdQuery(eventId);
        var result = await _mediator.Send(query);
        return result != null ? Ok(result) : NotFound(new { Message = $"Event with ID {eventId} not found." });
    }


    // GET: api/collaborate/events/project/{projectId}
    [HttpGet("project/{projectId}")]
    public async Task<ActionResult<IEnumerable<EventDto>>> GetEventsByProjectId(int projectId)
    {
        var query = new GetEventsByProjectIdQuery(projectId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    // PUT: api/collaborate/events/{eventId}
    [HttpPut("{eventId}")]
    public async Task<ActionResult<EventDto>> UpdateEvent(int eventId, [FromBody] EditEventCommandPayload payload)
    {
        // int currentUserId = GetCurrentUserId(); // Get from auth context
        int placeholderUserId = 1; // Placeholder

        var command = new EditEventCommand(
            eventId,
            payload.Name,
            payload.Location,
            payload.Description,
            payload.Color,
            payload.EventDate,
            placeholderUserId // Pass the authenticated user's ID for authorization in handler
        );
        var result = await _mediator.Send(command);
        return result != null ? Ok(result) : NotFound(new { Message = $"Event with ID {eventId} not found or update failed." });
    }

    // DELETE: api/collaborate/events/{eventId}
    [HttpDelete("{eventId}")]
    public async Task<IActionResult> DeleteEvent(int eventId)
    {
        // int currentUserId = GetCurrentUserId(); // Get from auth context
        int placeholderUserId = 1; // Placeholder

        var command = new DeleteEventCommand(eventId, placeholderUserId);
        var success = await _mediator.Send(command);
        return success ? NoContent() : NotFound(new { Message = $"Event with ID {eventId} not found or user not authorized." });
    }

    // --- Payloads for request bodies ---
    public record CreateEventCommandPayload(string Name, DateTime EventDate, string Location, string Description, string Color, int ProjectId);
    public record EditEventCommandPayload(string Name, DateTime EventDate, string Location, string Description, string Color);

    // private int GetCurrentUserId() { /* ... logic to get user ID ... */ return 1; }
}
