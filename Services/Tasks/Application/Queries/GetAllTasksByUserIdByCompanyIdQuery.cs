using MediatR;
using System.Collections.Generic;
using Tasks.Application.DTOs;

namespace Tasks.Application.Queries
{
    public record GetAllTasksByUserIdByCompanyIdQuery(int UserId, int CompanyId) : IRequest<IEnumerable<TaskItemDto>>;
}
