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

public class GetPostsByCompanyIdQueryHandler : IRequestHandler<GetPostsByCompanyIdQuery, IEnumerable<PostDto>>
{
    private readonly IPostRepository _postRepository;
    // Injections for IUserRepository, ICommentRepository would be needed for full DTO population

    public GetPostsByCompanyIdQueryHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<IEnumerable<PostDto>> Handle(GetPostsByCompanyIdQuery request, CancellationToken cancellationToken)
    {
        var posts = await _postRepository.GetByCompanyIdAsync(request.CompanyId);
        if (posts == null || !posts.Any())
        {
            return new List<PostDto>();
        }

        var postDtos = new List<PostDto>();
        foreach (var post in posts)
        {
            // Placeholder for user details
            var userName = $"User{post.UserId}";
            var userImage = $"user{post.UserId}.jpg";
            var userEmail = $"user{post.UserId}@example.com";

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
                userName,
                userImage,
                userEmail,
                post.Rating,
                post.ImageUrls,
                commentDtos
            ));
        }
        return postDtos;
    }
}
