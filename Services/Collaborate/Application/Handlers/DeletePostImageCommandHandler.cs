using MediatR;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq; // Required for .ToList() or .Contains() if used

namespace AidManager.Collaborate.Application.Handlers;

public class DeletePostImageCommandHandler : IRequestHandler<DeletePostImageCommand, bool>
{
    private readonly IPostRepository _postRepository;
    // Potentially a service for managing image files if they are stored outside the DB (e.g., blob storage)

    public DeletePostImageCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<bool> Handle(DeletePostImageCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.PostId);
        if (post == null)
        {
            return false; // Post not found
        }

        // Authorization: Check if the user requesting deletion is the owner of the post
        if (post.UserId != request.UserId)
        {
            // Unauthorized to modify this post
            return false;
        }

        if (post.ImageUrls == null || !post.ImageUrls.Contains(request.ImageUrl))
        {
            return false; // Image URL not found in the post
        }

        post.ImageUrls.Remove(request.ImageUrl);
        // If images are stored in a blob storage, you'd also delete the file from there.
        // e.g., await _blobStorageService.DeleteImageAsync(request.ImageUrl);

        await _postRepository.UpdateAsync(post);
        return true;
    }
}
