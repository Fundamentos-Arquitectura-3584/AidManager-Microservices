using MediatR;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.Interfaces;
using AidManager.Collaborate.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers;

public class LikePostCommandHandler : IRequestHandler<LikePostCommand, bool>
{
    private readonly ILikedPostRepository _likedPostRepository;
    private readonly IPostRepository _postRepository;

    public LikePostCommandHandler(ILikedPostRepository likedPostRepository, IPostRepository postRepository)
    {
        _likedPostRepository = likedPostRepository;
        _postRepository = postRepository;
    }

    public async Task<bool> Handle(LikePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.PostId);
        if (post == null)
        {
            return false; // Post not found
        }

        var existingLike = await _likedPostRepository.GetAsync(request.UserId, request.PostId);
        if (existingLike != null)
        {
            return true; // Already liked, consider this a success or handle idempotency
        }

        var likedPost = new LikedPost
        {
            UserId = request.UserId,
            PostId = request.PostId
        };

        await _likedPostRepository.AddAsync(likedPost);

        // Update post rating
        post.Rating = await _likedPostRepository.GetLikesCountForPostAsync(request.PostId);
        await _postRepository.UpdateAsync(post);

        return true;
    }
}
