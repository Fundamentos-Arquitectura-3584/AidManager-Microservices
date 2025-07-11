using MediatR;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers;

public class UnlikePostCommandHandler : IRequestHandler<UnlikePostCommand, bool>
{
    private readonly ILikedPostRepository _likedPostRepository;
    private readonly IPostRepository _postRepository;

    public UnlikePostCommandHandler(ILikedPostRepository likedPostRepository, IPostRepository postRepository)
    {
        _likedPostRepository = likedPostRepository;
        _postRepository = postRepository;
    }

    public async Task<bool> Handle(UnlikePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.PostId);
        if (post == null)
        {
            // Post doesn't exist, so can't unlike it.
            // Or, if it did exist but was deleted, the like might still be in LikedPost table.
            // Depending on desired behavior, this could be true (if like record is removed) or false.
            // Let's assume for now that if post doesn't exist, we can't proceed.
            // However, a more robust implementation might allow removing the Like record even if Post is gone.
            return false;
        }

        var existingLike = await _likedPostRepository.GetAsync(request.UserId, request.PostId);
        if (existingLike == null)
        {
            return false; // Not liked, or already unliked
        }

        await _likedPostRepository.DeleteAsync(request.UserId, request.PostId);

        // Update post rating
        post.Rating = await _likedPostRepository.GetLikesCountForPostAsync(request.PostId);
        await _postRepository.UpdateAsync(post);

        return true;
    }
}
