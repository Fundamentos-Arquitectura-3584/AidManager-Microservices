using MediatR;

namespace Tasks.Application.Commands
{
    // Assuming returns boolean for success
    public record SaveProjectAsFavoriteCommand(int UserId, int ProjectId) : IRequest<bool>;
}
