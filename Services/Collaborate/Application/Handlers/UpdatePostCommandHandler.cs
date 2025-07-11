using MediatR;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.DTOs;
using AidManager.Collaborate.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq; // For .Select
using System.Collections.Generic; // For List

namespace AidManager.Collaborate.Application.Handlers;

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, PostDto>
{
    private readonly IPostRepository _postRepository;
    // private readonly IUserRepository _userRepository; // For user details if needed for DTO

    public UpdatePostCommandHandler(IPostRepository postRepository /*, IUserRepository userRepository */)
    {
        _postRepository = postRepository;
        // _userRepository = userRepository;
    }

    public async Task<PostDto> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.Id);
        if (post == null)
        {
            // Handle not found, e.g., throw NotFoundException or return null
            return null;
        }

        // Authorization: Check if the user requesting update is the owner of the post
        if (post.UserId != request.UserId)
        {
            // Handle unauthorized, e.g., throw UnauthorizedAccessException
            // For now, returning null to indicate failure (could be due to not found or auth)
            return null;
        }

        post.Title = request.Title;
        post.Subject = request.Subject;
        post.Description = request.Description;
        post.ImageUrls = request.ImageUrls ?? new List<string>();
        // CompanyId and UserId are generally not updatable directly this way.
        // Rating is updated via Like/Unlike or a specific UpdatePostRatingCommand.
        // CreatedAt is also not updatable.

        await _postRepository.UpdateAsync(post);

        // Fetch user details for DTO (placeholder)
        var userName = "PlaceholderUserName"; // await _userRepository.GetUserNameByIdAsync(post.UserId);
        var userImage = "placeholder.jpg"; // await _userRepository.GetUserImageByIdAsync(post.UserId);
        var userEmail = "placeholder@example.com"; // await _userRepository.GetUserEmailByIdAsync(post.UserId);

        // Map comments to CommentDto (placeholder, assumes comments are loaded with post or fetched separately)
        var commentDtos = post.Comments?.Select(c => new CommentDto(
            c.Id,
            c.Content,
            c.UserId,
            "CommenterName", // Placeholder - would need to fetch commenter details
            "commenter@example.com",
            "commenter.jpg",
            c.PostId,
            c.CreatedAt
        )).ToList() ?? new List<CommentDto>();

        return new PostDto(
            post.Id,
            post.Title,
            post.Subject,
            post.Description,
            post.CreatedAt,
            post.CompanyId,
            post.UserId,
            userName,
            userImage,
            userEmail,
            post.Rating,
            post.ImageUrls,
            commentDtos
        );
    }
}
