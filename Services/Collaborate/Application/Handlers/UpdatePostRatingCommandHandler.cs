using MediatR;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.DTOs;
using AidManager.Collaborate.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace AidManager.Collaborate.Application.Handlers;

public class UpdatePostRatingCommandHandler : IRequestHandler<UpdatePostRatingCommand, PostDto>
{
    private readonly IPostRepository _postRepository;
    // private readonly IUserRepository _userRepository; // For user details for DTO

    public UpdatePostRatingCommandHandler(IPostRepository postRepository /*, IUserRepository userRepository */)
    {
        _postRepository = postRepository;
        // _userRepository = userRepository;
    }

    public async Task<PostDto> Handle(UpdatePostRatingCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.PostId);
        if (post == null)
        {
            // Handle not found
            return null;
        }

        // Authorization for changing a rating directly might be restricted.
        // For instance, only admins or specific roles can do this.
        // The request.UserId can be used to check these permissions.
        // For this example, we'll assume the check is handled elsewhere or is allowed.
        // if (!_userService.UserHasRole(request.UserId, "Admin")) { throw new UnauthorizedAccessException(); }


        post.Rating = request.NewRating;
        await _postRepository.UpdateAsync(post);

        // Fetch user details for DTO (placeholder)
        var userName = "PlaceholderUserName"; // await _userRepository.GetUserNameByIdAsync(post.UserId);
        var userImage = "placeholder.jpg"; // await _userRepository.GetUserImageByIdAsync(post.UserId);
        var userEmail = "placeholder@example.com"; // await _userRepository.GetUserEmailByIdAsync(post.UserId);

        // Map comments to CommentDto (placeholder)
        var commentDtos = post.Comments?.Select(c => new CommentDto(
            c.Id,
            c.Content,
            c.UserId,
            "CommenterName",
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
