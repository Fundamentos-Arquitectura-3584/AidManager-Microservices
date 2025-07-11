using MediatR;
using AidManager.Collaborate.Application.Queries;
using AidManager.Collaborate.Application.DTOs;
using AidManager.Collaborate.Application.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic; // For List
using AidManager.Collaborate.Domain.Entities; // For Comment

namespace AidManager.Collaborate.Application.Handlers;

public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, PostDto?>
{
    private readonly IPostRepository _postRepository;
    // private readonly IUserRepository _userRepository; // For user details
    // private readonly ICommentRepository _commentRepository; // For comments

    public GetPostByIdQueryHandler(IPostRepository postRepository /*, IUserRepository userRepository, ICommentRepository commentRepository */)
    {
        _postRepository = postRepository;
        // _userRepository = userRepository;
        // _commentRepository = commentRepository;
    }

    public async Task<PostDto?> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.PostId);
        if (post == null)
        {
            return null;
        }

        // Placeholder for fetching user details
        var userName = $"User{post.UserId}"; // await _userRepository.GetUserNameByIdAsync(post.UserId);
        var userImage = $"user{post.UserId}.jpg"; // await _userRepository.GetUserImageByIdAsync(post.UserId);
        var userEmail = $"user{post.UserId}@example.com"; // await _userRepository.GetUserEmailByIdAsync(post.UserId);

        // Placeholder for fetching and mapping comments
        // In a real app, post.Comments might be loaded by EF Core if configured,
        // or you might need to explicitly fetch them using _commentRepository.GetByPostIdAsync(post.Id)
        var commentDtos = new List<CommentDto>();
        if (post.Comments != null) // Assuming post.Comments is populated (e.g. via Include in repository)
        {
            foreach (var comment in post.Comments)
            {
                // Placeholder for fetching commenter details
                var commenterName = $"User{comment.UserId}"; // await _userRepository.GetUserNameByIdAsync(comment.UserId);
                var commenterEmail = $"user{comment.UserId}@example.com"; // await _userRepository.GetUserEmailByIdAsync(comment.UserId);
                var commenterImage = $"user{comment.UserId}.jpg"; // await _userRepository.GetUserImageByIdAsync(comment.UserId);

                commentDtos.Add(new CommentDto(
                    comment.Id,
                    comment.Content,
                    comment.UserId,
                    commenterName,
                    commenterEmail,
                    commenterImage,
                    comment.PostId,
                    comment.CreatedAt
                ));
            }
        }

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
