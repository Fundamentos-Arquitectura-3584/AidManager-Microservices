using MediatR;

namespace Tasks.Application.Commands
{
    public record DeleteProjectCommand(int ProjectId) : IRequest<bool>;
}
