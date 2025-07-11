using MediatR;
using Microsoft.AspNetCore.Mvc;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.Queries;
using AidManager.Collaborate.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
// using System.Security.Claims; // For User ID

namespace AidManager.Collaborate.Web.Controllers;

[ApiController]
[Route("api/collaborate/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // POST: api/collaborate/posts
    [HttpPost]
    public async Task<ActionResult<PostDto>> CreateNewPost([FromBody] CreatePostCommandPayload payload)
    {
        // int userId = GetCurrentUserId(); // From auth context
        // For this command, UserId is part of the payload as per intern's design.
        // Ensure UserId is validated / matches authenticated user in a real scenario.
        if (payload.UserId == 0) return BadRequest("UserId must be provided in payload.");

        var command = new CreatePostCommand(
            payload.Title,
            payload.Subject,
            payload.Description,
            payload.CompanyId,
            payload.UserId,
            payload.ImageUrls ?? new List<string>()
        );
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetPostById), new { postId = result.Id }, result);
    }

    // PUT: api/collaborate/posts/{postId}
    [HttpPut("{postId}")]
    public async Task<ActionResult<PostDto>> UpdatePost(int postId, [FromBody] UpdatePostCommandPayload payload)
    {
        // int currentUserId = GetCurrentUserId(); // From auth context for authorization
        int placeholderUserId = 1; // Placeholder for authorization in handler

        var command = new UpdatePostCommand(
            postId,
            payload.Title,
            payload.Subject,
            payload.Description,
            payload.ImageUrls ?? new List<string>(),
            placeholderUserId // This UserId is for authorization in the handler
        );
        var result = await _mediator.Send(command);
        return result != null ? Ok(result) : NotFound(new { Message = $"Post with ID {postId} not found or update failed." });
    }

    // PUT: api/collaborate/posts/{postId}/rating
    [HttpPut("{postId}/rating")]
    public async Task<ActionResult<PostDto>> UpdatePostRating(int postId, [FromBody] UpdatePostRatingCommandPayload payload)
    {
        // int currentUserId = GetCurrentUserId(); // For authorization
        int placeholderUserId = 1; // Placeholder, for authorization in handler (e.g. if only admin can set rating)

        var command = new UpdatePostRatingCommand(postId, payload.NewRating, placeholderUserId);
        var result = await _mediator.Send(command);
        return result != null ? Ok(result) : NotFound(new { Message = $"Post with ID {postId} not found or rating update failed." });
    }


    // GET: api/collaborate/posts/{postId}
    [HttpGet("{postId}")]
    public async Task<ActionResult<PostDto>> GetPostById(int postId)
    {
        var query = new GetPostByIdQuery(postId);
        var result = await _mediator.Send(query);
        return result != null ? Ok(result) : NotFound(new { Message = $"Post with ID {postId} not found." });
    }

    // DELETE: api/collaborate/posts/{postId}
    [HttpDelete("{postId}")]
    public async Task<IActionResult> DeletePostById(int postId)
    {
        // int currentUserId = GetCurrentUserId(); // For authorization
        int placeholderUserId = 1; // Placeholder

        var command = new DeletePostCommand(postId, placeholderUserId);
        var success = await _mediator.Send(command);
        return success ? NoContent() : NotFound(new { Message = $"Post with ID {postId} not found or user not authorized." });
    }

    // GET: api/collaborate/posts/company/{companyId}
    [HttpGet("company/{companyId}")]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetAllPostsByCompanyId(int companyId)
    {
        var query = new GetPostsByCompanyIdQuery(companyId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    // GET: api/collaborate/posts/user/{userId}
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetAllPostsByUserId(int userId)
    {
        // Authorization: users might only see their own posts, or public posts.
        // int currentUserId = GetCurrentUserId();
        // if (currentUserId != userId && !User.IsInRole("Admin")) { /* Potentially filter or forbid */ }

        var query = new GetPostsByUserIdQuery(userId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    // Note: GetLikedPostsByUserId is in PostsInteractionController as per plan.

    // --- Payloads for request bodies ---
    public record CreatePostCommandPayload(string Title, string Subject, string Description, int CompanyId, int UserId, List<string>? ImageUrls);
    public record UpdatePostCommandPayload(string Title, string Subject, string Description, List<string>? ImageUrls);
    public record UpdatePostRatingCommandPayload(int NewRating);

    // private int GetCurrentUserId()
    // {
    //     // var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    //     // return userIdClaim != null ? int.Parse(userIdClaim) : 0;
    //     return 1; // Placeholder
    // }
}
