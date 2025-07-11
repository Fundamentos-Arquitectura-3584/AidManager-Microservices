using MediatR;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.DTOs;
using AidManager.Collaborate.Application.Interfaces;
using AidManager.Collaborate.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers;

public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, CommentDto>
{
    private readonly ICommentRepository _commentRepository;
    // private readonly IUserRepository _userRepository; // To get UserName, UserEmail, UserImage

    public AddCommentCommandHandler(ICommentRepository commentRepository /*, IUserRepository userRepository */)
    {
        _commentRepository = commentRepository;
        // _userRepository = userRepository;
    }

    public async Task<CommentDto> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = new Comment
        {
            UserId = request.UserId,
            Content = request.Content,
            PostId = request.PostId,
            CreatedAt = System.DateTime.UtcNow
        };

        var createdComment = await _commentRepository.AddAsync(comment);

        // In a real scenario, you would fetch user details from the IUserRepository
        // For now, we'll use placeholders. This might involve an HTTP call to another service (IAM)
        // or access to a shared user database/cache.
        var userName = "PlaceholderUserName"; // await _userRepository.GetUserNameByIdAsync(createdComment.UserId);
        var userEmail = "placeholder@example.com"; // await _userRepository.GetUserEmailByIdAsync(createdComment.UserId);
        var userImage = "placeholder.jpg"; // await _userRepository.GetUserImageByIdAsync(createdComment.UserId);

        return new CommentDto(
            createdComment.Id,
            createdComment.Content,
            createdComment.UserId,
            userName,
            userEmail,
            userImage,
            createdComment.PostId,
            createdComment.CreatedAt
        );
    }
}
