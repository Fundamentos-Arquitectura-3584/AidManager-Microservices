using MediatR;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.DTOs;
using AidManager.Collaborate.Application.Interfaces;
using AidManager.Collaborate.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, EventDto>
{
    private readonly IEventRepository _eventRepository;

    public CreateEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<EventDto> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var newEvent = new Event
        {
            Name = request.Name,
            EventDate = request.EventDate,
            Location = request.Location,
            Description = request.Description,
            Color = request.Color,
            ProjectId = request.ProjectId
            // Assuming UserId in the command is for authorization/audit and not stored directly in Event entity
        };

        var createdEvent = await _eventRepository.AddAsync(newEvent);

        return new EventDto(
            createdEvent.Id,
            createdEvent.Name,
            createdEvent.EventDate,
            createdEvent.Location,
            createdEvent.Description,
            createdEvent.Color,
            createdEvent.ProjectId
        );
    }
}
