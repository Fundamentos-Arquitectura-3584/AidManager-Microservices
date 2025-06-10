using MediatR;
using Tasks.Application.DTOs;

namespace Tasks.Application.Queries
{
    public record GetProjectByIdQuery(int Id) : IRequest<ProjectResource?>;
}
