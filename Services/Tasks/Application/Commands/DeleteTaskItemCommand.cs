using MediatR;

namespace Tasks.Application.Commands
{
    public record DeleteTaskItemCommand(int Id) : IRequest<bool>;
}
