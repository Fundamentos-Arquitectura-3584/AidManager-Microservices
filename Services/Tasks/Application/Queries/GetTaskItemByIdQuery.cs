using MediatR;
using Tasks.Application.DTOs;

namespace Tasks.Application.Queries
{
    public record GetTaskItemByIdQuery(int Id) : IRequest<TaskItemDto?>;
}
