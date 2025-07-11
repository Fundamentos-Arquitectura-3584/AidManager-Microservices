using MediatR;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.DTOs;
using AidManager.Collaborate.Application.Interfaces;
using AidManager.Collaborate.Domain.Entities; // Required for Event
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers;

public class EditEventCommandHandler : IRequestHandler<EditEventCommand, EventDto>
{
    private readonly IEventRepository _eventRepository;
    // Potentially IProjectRepository for authorization if needed

    public EditEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<EventDto> Handle(EditEventCommand request, CancellationToken cancellationToken)
    {
        var eventItem = await _eventRepository.GetByIdAsync(request.Id);
        if (eventItem == null)
        {
            // Or throw a custom NotFoundException
            // For simplicity, returning null if DTO is the return type, or handle as per API design.
            // If EventDto is non-nullable, an exception is more appropriate.
            // Let's assume for now the controller will handle a null DTO as NotFound.
            return null;
        }

        // Authorization check:
        // This is a simplified check. In a real app, you might need to check if request.UserId
        // is the creator of the event, or part of the project team, etc.
        // For instance, if the Event had a CreatorUserId field:
        // if (eventItem.CreatorUserId != request.UserId) { throw new UnauthorizedAccessException(); }
        // Or, if linked to a project, check project ownership/membership.
        // For now, we'll assume if the user has the event ID, they can edit (placeholder logic).

        eventItem.Name = request.Name;
        eventItem.Location = request.Location;
        eventItem.Description = request.Description;
        eventItem.Color = request.Color;
        eventItem.EventDate = request.EventDate;
        // ProjectId is usually not changed during an edit, but if it were, it would be part of the command.

        await _eventRepository.UpdateAsync(eventItem);

        return new EventDto(
            eventItem.Id,
            eventItem.Name,
            eventItem.EventDate,
            eventItem.Location,
            eventItem.Description,
            eventItem.Color,
            eventItem.ProjectId
        );
    }
}
