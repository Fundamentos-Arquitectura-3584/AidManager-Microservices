using System;
using MediatR;
using AidManager.Collaborate.Application.DTOs; // Assuming EventDto might be returned

namespace AidManager.Collaborate.Application.Commands;

// Returns the created EventDto
public record CreateEventCommand(
    string Name,
    DateTime EventDate,
    string Location,
    string Description,
    string Color,
    int ProjectId,
    int UserId // User creating the event
) : IRequest<EventDto>;
