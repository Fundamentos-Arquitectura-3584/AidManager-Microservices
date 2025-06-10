using MediatR;
using System.Collections.Generic;
using Tasks.Application.DTOs;

namespace Tasks.Application.Queries
{
    public record GetFavoriteProjectsByUserIdQuery(int UserId) : IRequest<IEnumerable<ProjectResource>>;
}
