using MediatR;
using AidManager.Collaborate.Application.Queries;
using AidManager.Collaborate.Application.DTOs;
using AidManager.Collaborate.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers;

public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, EventDto?>
{
    private readonly IEventRepository _eventRepository;

    public GetEventByIdQueryHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<EventDto?> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
    {
        var eventItem = await _eventRepository.GetByIdAsync(request.EventId);
        if (eventItem == null)
        {
            return null;
        }

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
