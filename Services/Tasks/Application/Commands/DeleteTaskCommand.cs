using MediatR;

namespace Tasks.Application.Commands
{
    public record DeleteTaskCommand(int Id, int ProjectId) : IRequest<bool>;
}
