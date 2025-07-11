using MediatR;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.Interfaces;
using AidManager.Collaborate.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers;

public class FavoritePostCommandHandler : IRequestHandler<FavoritePostCommand, bool>
{
    private readonly IFavoritePostRepository _favoritePostRepository;
    private readonly IPostRepository _postRepository; // To ensure post exists

    public FavoritePostCommandHandler(IFavoritePostRepository favoritePostRepository, IPostRepository postRepository)
    {
        _favoritePostRepository = favoritePostRepository;
        _postRepository = postRepository;
    }

    public async Task<bool> Handle(FavoritePostCommand request, CancellationToken cancellationToken)
    {
        // Check if post exists
        var post = await _postRepository.GetByIdAsync(request.PostId);
        if (post == null)
        {
            return false; // Post not found
        }

        // Check if already favorited
        var existingFavorite = await _favoritePostRepository.GetAsync(request.UserId, request.PostId);
        if (existingFavorite != null)
        {
            return true; // Already favorited, treat as success or return specific status
        }

        var favoritePost = new FavoritePost
        {
            UserId = request.UserId,
            PostId = request.PostId
        };

        await _favoritePostRepository.AddAsync(favoritePost);
        return true;
    }
}
