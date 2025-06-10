using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Tasks.Application.Commands;
using Tasks.Application.Interfaces;

namespace Tasks.Application.Handlers
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, bool>
    {
        private readonly ITaskItemRepository _taskItemRepository;

        public DeleteTaskCommandHandler(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            // Could add a check: var task = await _taskItemRepository.GetByIdAsync(request.Id);
            // if (task == null || task.ProjectId != request.ProjectId) return false;
            return await _taskItemRepository.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
