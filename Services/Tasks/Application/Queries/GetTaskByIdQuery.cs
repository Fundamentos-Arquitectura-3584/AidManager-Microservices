using MediatR;
using Tasks.Application.DTOs;

namespace Tasks.Application.Queries
{
    public record GetTaskByIdQuery(int Id) : IRequest<TaskItemDto?>;
}
