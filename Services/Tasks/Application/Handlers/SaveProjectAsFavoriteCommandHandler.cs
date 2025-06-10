using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Tasks.Application.Commands;
using Tasks.Application.Interfaces;
using Tasks.Domain.ValueObjects;

namespace Tasks.Application.Handlers
{
    public class SaveProjectAsFavoriteCommandHandler : IRequestHandler<SaveProjectAsFavoriteCommand, bool>
    {
        private readonly IProjectRepository _projectRepository;

        public SaveProjectAsFavoriteCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<bool> Handle(SaveProjectAsFavoriteCommand request, CancellationToken cancellationToken)
        {
            var favorite = new FavoriteProject(request.UserId, request.ProjectId);
            await _projectRepository.SaveFavoriteAsync(favorite, cancellationToken);
            return true; // Assuming success if no exception
        }
    }
}
