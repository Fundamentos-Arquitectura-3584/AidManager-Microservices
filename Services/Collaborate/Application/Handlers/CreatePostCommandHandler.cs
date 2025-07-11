using MediatR;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.DTOs;
using AidManager.Collaborate.Application.Interfaces;
using AidManager.Collaborate.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic; // For List
using System.Linq; // For Select

namespace AidManager.Collaborate.Application.Handlers;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, PostDto>
{
    private readonly IPostRepository _postRepository;
    // private readonly IUserRepository _userRepository; // For user details

    public CreatePostCommandHandler(IPostRepository postRepository /*, IUserRepository userRepository */)
    {
        _postRepository = postRepository;
        // _userRepository = userRepository;
    }

    public async Task<PostDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = new Post
        {
            Title = request.Title,
            Subject = request.Subject,
            Description = request.Description,
            CompanyId = request.CompanyId,
            UserId = request.UserId,
            ImageUrls = request.ImageUrls ?? new List<string>(),
            Comments = new List<Comment>(), // Initialize with empty comments
            CreatedAt = System.DateTime.UtcNow,
            Rating = 0 // Initial rating
        };

        var createdPost = await _postRepository.AddAsync(post);

        // Placeholder for fetching user details
        var userName = "PlaceholderUserName"; // await _userRepository.GetUserNameByIdAsync(createdPost.UserId);
        var userImage = "placeholder.jpg"; // await _userRepository.GetUserImageByIdAsync(createdPost.UserId);
        var userEmail = "placeholder@example.com"; // await _userRepository.GetUserEmailByIdAsync(createdPost.UserId);

        return new PostDto(
            createdPost.Id,
            createdPost.Title,
            createdPost.Subject,
            createdPost.Description,
            createdPost.CreatedAt,
            createdPost.CompanyId,
            createdPost.UserId,
            userName,
            userImage,
            userEmail,
            createdPost.Rating,
            createdPost.ImageUrls,
            new List<CommentDto>() // Empty comments DTO list for new post
        );
    }
}
