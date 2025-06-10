using MediatR;
using System.Collections.Generic;
using Tasks.Application.DTOs; // For ProjectResource as response

namespace Tasks.Application.Commands
{
    public record CreateProjectCommand(
        string Name,
        string? Description,
        List<string> ImageUrl, // Input as list of strings
        int CompanyId,
        string ProjectDate, // Keep as string for now, parse in handler
        string ProjectTime, // Keep as string for now, parse in handler
        string ProjectLocation) : IRequest<ProjectResource>; // Returns the created ProjectResource
}
