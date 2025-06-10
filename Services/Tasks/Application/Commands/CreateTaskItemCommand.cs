using MediatR;
using System;
using Tasks.Application.DTOs; // For TaskItemDto as response

namespace Tasks.Application.Commands
{
    public record CreateTaskItemCommand(
        string Title,
        string? Description,
        DateOnly DueDate,
        int ProjectId,
        string State,
        int AssigneeId) : IRequest<TaskItemDto>; // Returns the created TaskItemDto
}
