using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tasks.Application.Commands;
using Tasks.Application.DTOs;
using Tasks.Application.Interfaces;
using Tasks.Domain.ValueObjects;
using System.Collections.Generic; // Added for List<TeamMemberDto>

namespace Tasks.Application.Handlers
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, ProjectResource?>
    {
        private readonly IProjectRepository _projectRepository;

        public UpdateProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ProjectResource?> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);
            if (project == null)
            {
                return null; // Or throw NotFoundException
            }

            project.Name = request.Name;
            project.Description = request.Description;
            project.CompanyId = request.CompanyId;
            project.ProjectDate = DateOnly.Parse(request.ProjectDate); // Add error handling
            project.ProjectTime = TimeOnly.Parse(request.ProjectTime); // Add error handling
            project.ProjectLocation = request.ProjectLocation;
            project.AuditDate = DateOnly.FromDateTime(DateTime.UtcNow); // Update audit date

            // Handle ImageUrl update carefully. This might mean replacing all images.
            // For simplicity, clearing and adding new ones. A more robust solution might compare existing/new.
            project.ImageUrls.Clear();
            project.ImageUrls.AddRange(request.ImageUrl.Select(url => new ProjectImage { Url = url, ProjectId = project.Id }));

            var success = await _projectRepository.UpdateAsync(project, cancellationToken);
            if (!success) return null; // Or handle error

            // Re-fetch or map updated project to DTO
            var updatedProject = await _projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);
            if (updatedProject == null) return null; // Should not happen if update was successful

            // Basic mapping to ProjectResource
            return new ProjectResource(
                updatedProject.Id,
                updatedProject.AuditDate,
                updatedProject.Name,
                updatedProject.Description,
                updatedProject.ProjectDate,
                updatedProject.ProjectTime,
                updatedProject.ProjectLocation,
                updatedProject.CompanyId,
                // Team members list would require fetching them based on updatedProject.TeamMemberUserIds
                // This part is simplified for now.
                new List<TeamMemberDto>(),
                updatedProject.ImageUrls.Select(img => img.Url).ToList(),
                updatedProject.Rating
            );
        }
    }
}
