using MediatR;
using System.Collections.Generic;
using Tasks.Application.DTOs;

namespace Tasks.Application.Queries
{
    public record GetTasksByCompanyIdQuery(int CompanyId) : IRequest<IEnumerable<TaskItemDto>>;
}
