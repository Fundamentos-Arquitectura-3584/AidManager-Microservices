using MediatR;

namespace Tasks.Application.Commands
{
    public record RemoveProjectAsFavoriteCommand(int UserId, int ProjectId) : IRequest<bool>;
}
