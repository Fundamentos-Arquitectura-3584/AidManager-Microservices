using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tasks.Application.Commands;
using Tasks.Application.DTOs;
using Tasks.Application.Interfaces;
using Tasks.Domain.Entities;

namespace Tasks.Application.Handlers
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskItemDto>
    {
        private readonly ITaskItemRepository _taskItemRepository;
        // May need a user service to get AssigneeName and AssigneeImage

        public CreateTaskCommandHandler(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        public async Task<TaskItemDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var taskItem = new TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate,
                ProjectId = request.ProjectId,
                State = request.State, // Consider validating state
                AssigneeId = request.AssigneeId,
                CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow) // Or set by DB
            };

            var createdTask = await _taskItemRepository.AddAsync(taskItem, cancellationToken);

            // Basic mapping (AssigneeName, AssigneeImage are placeholders)
            return new TaskItemDto(
                createdTask.Id,
                createdTask.Title,
                createdTask.Description,
                createdTask.CreatedAt,
                createdTask.DueDate,
                createdTask.State,
                createdTask.AssigneeId,
                "Assignee Name Placeholder", // Placeholder
                "Assignee Image Placeholder", // Placeholder
                createdTask.ProjectId
            );
        }
    }
}
