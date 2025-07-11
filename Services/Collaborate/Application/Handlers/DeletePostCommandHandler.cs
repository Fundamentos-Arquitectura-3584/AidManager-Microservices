using MediatR;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers;

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, bool>
{
    private readonly IPostRepository _postRepository;
    // Potentially ICommentRepository, ILikedPostRepository, IFavoritePostRepository to delete related data

    public DeletePostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.PostId);
        if (post == null)
        {
            return false; // Or throw NotFoundException
        }

        // Authorization: Check if the user requesting deletion is the owner of the post
        if (post.UserId != request.UserId)
        {
            // In a real application, throw an UnauthorizedAccessException or similar
            // For now, returning false to indicate failure due to authorization (or not found)
            return false;
        }

        // Business rule: Before deleting a post, consider what to do with its comments, likes, favorites.
        // Option 1: Cascade delete (if DB is set up for it, or handle here).
        // Option 2: Disallow deletion if there are dependencies.
        // Option 3: Soft delete.
        // For this example, we'll proceed with a hard delete of the post.
        // Associated data (comments, likes, favorites) would need separate handling if not cascaded by DB.

        await _postRepository.DeleteAsync(request.PostId);
        return true; // Assuming delete is successful
    }
}
