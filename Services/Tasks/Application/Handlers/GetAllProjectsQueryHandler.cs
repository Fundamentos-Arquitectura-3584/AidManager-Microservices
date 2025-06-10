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
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, IEnumerable<ProjectResource>>
    {
        private readonly IProjectRepository _projectRepository;

        public GetAllProjectsQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<ProjectResource>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetAllByCompanyIdAsync(request.CompanyId, cancellationToken);
            return projects.Select(p => new ProjectResource(
                p.Id, p.AuditDate, p.Name, p.Description, p.ProjectDate, p.ProjectTime, p.ProjectLocation, p.CompanyId,
                new List<TeamMemberDto>(), // Placeholder
                p.ImageUrls.Select(img => img.Url).ToList(), p.Rating
            )).ToList();
        }
    }
}
