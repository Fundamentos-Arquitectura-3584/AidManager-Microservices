using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tasks.Application.Queries;
using Tasks.Application.DTOs;
using Tasks.Application.Interfaces;

namespace Tasks.Application.Handlers
{
    public class GetFavoriteProjectsByUserIdQueryHandler : IRequestHandler<GetFavoriteProjectsByUserIdQuery, IEnumerable<ProjectResource>>
    {
        private readonly IProjectRepository _projectRepository;

        public GetFavoriteProjectsByUserIdQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<ProjectResource>> Handle(GetFavoriteProjectsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetFavoriteProjectsByUserIdAsync(request.UserId, cancellationToken);
            return projects.Select(p => new ProjectResource(
                p.Id, p.AuditDate, p.Name, p.Description, p.ProjectDate, p.ProjectTime, p.ProjectLocation, p.CompanyId,
                new List<TeamMemberDto>(), // Placeholder
                p.ImageUrls.Select(img => img.Url).ToList(), p.Rating
            )).ToList();
        }
    }
}
