using MediatR;
using System.Collections.Generic;
using Tasks.Application.DTOs;

namespace Tasks.Application.Queries
{
    public record GetTasksByProjectIdQuery(int ProjectId) : IRequest<IEnumerable<TaskItemDto>>;
}
