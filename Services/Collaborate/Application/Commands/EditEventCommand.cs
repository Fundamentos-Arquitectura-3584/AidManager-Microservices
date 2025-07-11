using MediatR;
using AidManager.Collaborate.Application.DTOs; // Assuming EventDto might be returned
using System;

namespace AidManager.Collaborate.Application.Commands;

// Returns the updated EventDto
public record EditEventCommand(
    int Id, // EventId
    string Name,
    string Location,
    string Description,
    string Color, // Added Color as it's part of Event
    DateTime EventDate, // Added EventDate
    int UserId // User performing the edit
) : IRequest<EventDto>;
