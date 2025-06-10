using MediatR;
using System.Collections.Generic;
using Tasks.Application.DTOs;

namespace Tasks.Application.Queries
{
    public record GetAllProjectsByUserIdQuery(int UserId) : IRequest<IEnumerable<ProjectResource>>;
}
