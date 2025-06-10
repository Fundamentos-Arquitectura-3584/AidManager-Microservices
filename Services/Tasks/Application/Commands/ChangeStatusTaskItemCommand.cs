using MediatR;
using Tasks.Application.DTOs; // Assuming a TaskItemDto or a generic response

namespace Tasks.Application.Features.Tasks.Commands.ChangeStatusTaskItem
{
    public class ChangeStatusTaskItemCommand : IRequest<BaseCommandResponse> // Assuming BaseCommandResponse for simplicity
    {
        public int Id { get; set; } // TaskItem ID
        public string NewStatus { get; set; }
        // Potentially add UserId if needed for authorization/logging
    }
}
