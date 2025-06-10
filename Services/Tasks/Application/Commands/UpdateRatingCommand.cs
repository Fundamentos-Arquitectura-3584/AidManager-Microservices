using MediatR;

namespace Tasks.Application.Commands
{
    public record UpdateRatingCommand(int ProjectId, double Rating) : IRequest<bool>;
}
