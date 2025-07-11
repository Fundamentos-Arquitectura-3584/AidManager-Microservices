using MediatR;
using AidManager.Collaborate.Application.Queries;
using AidManager.Collaborate.Application.DTOs;
using AidManager.Collaborate.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AidManager.Collaborate.Domain.Entities; // Required for Comment

namespace AidManager.Collaborate.Application.Handlers;

public class GetFavoritePostsByUserIdQueryHandler : IRequestHandler<GetFavoritePostsByUserIdQuery, IEnumerable<PostDto>>
{
    private readonly IFavoritePostRepository _favoritePostRepository;
    private readonly IPostRepository _postRepository;
    // private readonly IUserRepository _userRepository; // For user details for PostDto
    // private readonly ICommentRepository _commentRepository; // For comments for PostDto

    public GetFavoritePostsByUserIdQueryHandler(
        IFavoritePostRepository favoritePostRepository,
        IPostRepository postRepository
        /*, IUserRepository userRepository, ICommentRepository commentRepository */)
    {
        _favoritePostRepository = favoritePostRepository;
        _postRepository = postRepository;
        // _userRepository = userRepository;
        // _commentRepository = commentRepository;
    }

    public async Task<IEnumerable<PostDto>> Handle(GetFavoritePostsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var favoriteEntries = await _favoritePostRepository.GetByUserIdAsync(request.UserId);
        if (favoriteEntries == null || !favoriteEntries.Any())
        {
            return new List<PostDto>();
        }

        var postDtos = new List<PostDto>();
        foreach (var favoriteEntry in favoriteEntries)
        {
            var post = await _postRepository.GetByIdAsync(favoriteEntry.PostId);
            if (post != null)
            {
                // Placeholder for user details
                var postUserName = $"User{post.UserId}"; // await _userRepository.GetUserNameByIdAsync(post.UserId);
                var postUserImage = $"user{post.UserId}.jpg"; // await _userRepository.GetUserImageByIdAsync(post.UserId);
                var postUserEmail = $"user{post.UserId}@example.com"; // await _userRepository.GetUserEmailByIdAsync(post.UserId);

                // Placeholder for fetching and mapping comments
                // var comments = await _commentRepository.GetByPostIdAsync(post.Id);
                var commentDtos = new List<CommentDto>(); // Map actual comments here
                if (post.Comments != null) // Assuming post object has comments loaded
                {
                    foreach (var comment in post.Comments)
                    {
                        // Placeholder for commenter details
                        var commenterName = $"User{comment.UserId}";
                        var commenterEmail = $"user{comment.UserId}@example.com";
                        var commenterImage = $"user{comment.UserId}.jpg";
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


                postDtos.Add(new PostDto(
                    post.Id,
                    post.Title,
                    post.Subject,
                    post.Description,
                    post.CreatedAt,
                    post.CompanyId,
                    post.UserId,
                    postUserName,
                    postUserImage,
                    postUserEmail,
                    post.Rating,
                    post.ImageUrls,
                    commentDtos
                ));
            }
        }
        return postDtos;
    }
}
