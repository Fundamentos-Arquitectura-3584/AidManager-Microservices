using MediatR;
using System.Collections.Generic;
using Tasks.Application.DTOs;

namespace Tasks.Application.Queries
{
    public record GetTasksByUserIdQuery(int UserId) : IRequest<IEnumerable<TaskItemDto>>;
}
