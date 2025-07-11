using MediatR;
using AidManager.Collaborate.Application.Queries;
using AidManager.Collaborate.Application.DTOs;
using AidManager.Collaborate.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers;

public class GetCommentsByPostIdQueryHandler : IRequestHandler<GetCommentsByPostIdQuery, IEnumerable<CommentDto>>
{
    private readonly ICommentRepository _commentRepository;
    // private readonly IUserRepository _userRepository; // For user details

    public GetCommentsByPostIdQueryHandler(ICommentRepository commentRepository /*, IUserRepository userRepository */)
    {
        _commentRepository = commentRepository;
        // _userRepository = userRepository;
    }

    public async Task<IEnumerable<CommentDto>> Handle(GetCommentsByPostIdQuery request, CancellationToken cancellationToken)
    {
        var comments = await _commentRepository.GetByPostIdAsync(request.PostId);
        if (comments == null || !comments.Any())
        {
            return new List<CommentDto>();
        }

        var commentDtos = new List<CommentDto>();
        foreach (var comment in comments)
        {
            // Placeholder for fetching user details for each comment
            var userName = $"User{comment.UserId}"; // await _userRepository.GetUserNameByIdAsync(comment.UserId);
            var userEmail = $"user{comment.UserId}@example.com"; // await _userRepository.GetUserEmailByIdAsync(comment.UserId);
            var userImage = $"user{comment.UserId}.jpg"; // await _userRepository.GetUserImageByIdAsync(comment.UserId);

            commentDtos.Add(new CommentDto(
                comment.Id,
                comment.Content,
                comment.UserId,
                userName,
                userEmail,
                userImage,
                comment.PostId,
                comment.CreatedAt
            ));
        }
        return commentDtos;
    }
}
