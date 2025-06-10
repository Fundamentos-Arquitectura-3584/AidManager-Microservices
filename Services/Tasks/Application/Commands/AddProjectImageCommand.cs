using MediatR;
using System.Collections.Generic;
using Tasks.Application.DTOs; // Or specific response DTO if needed

namespace Tasks.Application.Commands
{
    // Assuming this command returns boolean or some status. For now, Unit (void).
    // Or perhaps the updated ProjectResource? Let's assume a boolean success for now.
    public record AddProjectImageCommand(int ProjectId, List<string> ImagesUrl) : IRequest<bool>;
}
