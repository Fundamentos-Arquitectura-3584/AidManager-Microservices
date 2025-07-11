using MediatR;
using AidManager.Collaborate.Application.Queries;
using AidManager.Collaborate.Application.DTOs;
using AidManager.Collaborate.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AidManager.Collaborate.Domain.Entities; // For Comment

namespace AidManager.Collaborate.Application.Handlers;

public class GetLikedPostsByUserIdQueryHandler : IRequestHandler<GetLikedPostsByUserIdQuery, IEnumerable<PostDto>>
{
    private readonly ILikedPostRepository _likedPostRepository;
    private readonly IPostRepository _postRepository;
    // IUserRepository, ICommentRepository for full DTO population

    public GetLikedPostsByUserIdQueryHandler(
        ILikedPostRepository likedPostRepository,
        IPostRepository postRepository)
    {
        _likedPostRepository = likedPostRepository;
        _postRepository = postRepository;
    }

    public async Task<IEnumerable<PostDto>> Handle(GetLikedPostsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var likedEntries = await _likedPostRepository.GetByUserIdAsync(request.UserId);
        if (likedEntries == null || !likedEntries.Any())
        {
            return new List<PostDto>();
        }

        var postDtos = new List<PostDto>();
        foreach (var likedEntry in likedEntries)
        {
            var post = await _postRepository.GetByIdAsync(likedEntry.PostId);
            if (post != null)
            {
                // Placeholder for user details
                var postUserName = $"User{post.UserId}";
                var postUserImage = $"user{post.UserId}.jpg";
                var postUserEmail = $"user{post.UserId}@example.com";

                // Placeholder for comments
                var commentDtos = new List<CommentDto>();
                if (post.Comments != null) // Assuming comments are loaded
                {
                    foreach (var comment in post.Comments)
                    {
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
