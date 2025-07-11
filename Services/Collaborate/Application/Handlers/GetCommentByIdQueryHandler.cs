using MediatR;
using AidManager.Collaborate.Application.Queries;
using AidManager.Collaborate.Application.DTOs;
using AidManager.Collaborate.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers;

public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, CommentDto?>
{
    private readonly ICommentRepository _commentRepository;
    // private readonly IUserRepository _userRepository; // For user details

    public GetCommentByIdQueryHandler(ICommentRepository commentRepository /*, IUserRepository userRepository */)
    {
        _commentRepository = commentRepository;
        // _userRepository = userRepository;
    }

    public async Task<CommentDto?> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdAsync(request.CommentId);
        if (comment == null)
        {
            return null;
        }

        // Placeholder for fetching user details
        var userName = "PlaceholderUserName"; // await _userRepository.GetUserNameByIdAsync(comment.UserId);
        var userEmail = "placeholder@example.com"; // await _userRepository.GetUserEmailByIdAsync(comment.UserId);
        var userImage = "placeholder.jpg"; // await _userRepository.GetUserImageByIdAsync(comment.UserId);

        return new CommentDto(
            comment.Id,
            comment.Content,
            comment.UserId,
            userName,
            userEmail,
            userImage,
            comment.PostId,
            comment.CreatedAt
        );
    }
}
