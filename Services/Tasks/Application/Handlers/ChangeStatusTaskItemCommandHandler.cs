using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Tasks.Application.Features.Tasks.Commands.ChangeStatusTaskItem;
using Tasks.Application.Interfaces;
using Tasks.Application.DTOs; // For BaseCommandResponse
using Tasks.Domain.Entities; // For TaskItem
using Tasks.Domain.Enums; // Assuming TaskStatus is an enum

namespace Tasks.Application.Handlers
{
    public class ChangeStatusTaskItemCommandHandler : IRequestHandler<ChangeStatusTaskItemCommand, BaseCommandResponse>
    {
        private readonly ITaskItemRepository _taskItemRepository;

        public ChangeStatusTaskItemCommandHandler(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        public async Task<BaseCommandResponse> Handle(ChangeStatusTaskItemCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var taskItem = await _taskItemRepository.GetByIdAsync(request.Id);

            if (taskItem == null)
            {
                response.Success = false;
                response.Message = "TaskItem not found.";
                return response;
            }

            // Assuming TaskStatus is an enum and needs parsing.
            // Add validation for the NewStatus value if it's an enum or has specific allowed values.
            // For example: if (Enum.TryParse<TaskStatus>(request.NewStatus, true, out var newStatusEnum))
            // For now, assuming it's a string field directly on TaskItem or conversion is handled in setter/domain logic
            taskItem.Status = request.NewStatus; // Or taskItem.SetStatus(newStatusEnum);

            await _taskItemRepository.UpdateAsync(taskItem);

            response.Success = true;
            response.Message = "TaskItem status updated successfully.";
            response.Data = taskItem.Id; // Or the updated TaskItemDto
            return response;
        }
    }
}
