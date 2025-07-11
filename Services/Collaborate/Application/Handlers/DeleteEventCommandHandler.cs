using MediatR;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers;

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, bool>
{
    private readonly IEventRepository _eventRepository;

    public DeleteEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<bool> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var eventItem = await _eventRepository.GetByIdAsync(request.EventId);
        if (eventItem == null)
        {
            return false; // Or throw NotFoundException
        }

        // Optional: Add authorization logic here if UserId in command should match event creator/owner
        // For example:
        // var project = await _projectRepository.GetByIdAsync(eventItem.ProjectId);
        // if (project?.OwnerId != request.UserId) // Assuming Project has an OwnerId
        // {
        //     throw new UnauthorizedAccessException("User is not authorized to delete this event.");
        // }


        await _eventRepository.DeleteAsync(request.EventId);
        return true; // Assuming delete is successful
    }
}
