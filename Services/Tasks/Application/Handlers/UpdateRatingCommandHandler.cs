using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Tasks.Application.Commands;
using Tasks.Application.Interfaces;

namespace Tasks.Application.Handlers
{
    public class UpdateRatingCommandHandler : IRequestHandler<UpdateRatingCommand, bool>
    {
        private readonly IProjectRepository _projectRepository;

        public UpdateRatingCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<bool> Handle(UpdateRatingCommand request, CancellationToken cancellationToken)
        {
            return await _projectRepository.UpdateRatingAsync(request.ProjectId, request.Rating, cancellationToken);
        }
    }
}
