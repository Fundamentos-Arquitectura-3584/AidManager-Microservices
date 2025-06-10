using MediatR;
using System;
using Tasks.Application.DTOs; // For TaskItemDto as response

namespace Tasks.Application.Commands
{
    public record UpdateTaskItemCommand(
        int Id,
        string Title,
        string? Description,
        DateOnly DueDate,
        string State,
        int AssigneeId,
        int ProjectId) : IRequest<TaskItemDto?>; // Returns updated TaskItemDto or null
}
