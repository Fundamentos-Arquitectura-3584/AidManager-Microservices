using MediatR;
using Microsoft.AspNetCore.Mvc;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.Queries;
using AidManager.Collaborate.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Web.Controllers;

[ApiController]
[Route("api/collaborate/[controller]")] // Added 'collaborate' to the route for service distinction
public class CommentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CommentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/collaborate/comments/post/{postId}
    [HttpGet("post/{postId}")]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetCommentsByPostId(int postId)
    {
        var query = new GetCommentsByPostIdQuery(postId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    // GET: api/collaborate/comments/{commentId}
    [HttpGet("{commentId}")]
    public async Task<ActionResult<CommentDto>> GetCommentById(int commentId)
    {
        var query = new GetCommentByIdQuery(commentId);
        var result = await _mediator.Send(query);
        return result != null ? Ok(result) : NotFound(new { Message = $"Comment with ID {commentId} not found." });
    }

    // POST: api/collaborate/comments/post/{postId} (or just /comments and include PostId in command)
    // Let's make it more RESTful by associating with a post resource directly
    // POST: api/collaborate/posts/{postId}/comments
    [HttpPost("/api/collaborate/posts/{postId}/comments")] // Overriding route for clarity
    public async Task<ActionResult<CommentDto>> AddPostComment(int postId, [FromBody] AddCommentCommandPayload payload)
    {
        // Assuming UserId would come from JWT token / authentication context in a real app
        // For now, let's say it's passed in or a default is used by the handler if not security critical for this example
        // int userId = GetCurrentUserId(); // Placeholder for getting authenticated user's ID

        // The command itself takes UserId, so it must be provided.
        // This payload helps structure the request body if UserId is also in the body.
        // If UserId is from token, the command can be constructed with it.
        // For simplicity, let's assume payload provides UserId for now.
        if (payload.UserId == 0) return BadRequest("UserId must be provided.");

        var command = new AddCommentCommand(payload.UserId, payload.Content, postId);
        var result = await _mediator.Send(command);

        // Return 201 Created with the location of the new resource and the resource itself
        return CreatedAtAction(nameof(GetCommentById), new { commentId = result.Id }, result);
    }

    // DELETE: api/collaborate/comments/{commentId}
    // The plan has DeleteCommentCommand(int PostId,int CommentId) but it should be (int CommentId, int UserId)
    // The route reflects this: we identify comment by its ID, and UserId for auth comes from token.
    [HttpDelete("{commentId}")]
    public async Task<IActionResult> DeleteComment(int commentId)
    {
        // Assume UserId is extracted from the authenticated user's context (e.g., JWT token)
        // For testing, you might pass it or use a default.
        // int currentUserId = GetCurrentUserId(); // e.g. User.FindFirst(ClaimTypes.NameIdentifier)?.Value
        int placeholderUserId = 1; // Placeholder: In real app, get from auth.

        var command = new DeleteCommentCommand(commentId, placeholderUserId);
        var success = await _mediator.Send(command);

        return success ? NoContent() : NotFound(new { Message = $"Comment with ID {commentId} not found or user not authorized." });
    }

    // --- Helper for payload ---
    public record AddCommentCommandPayload(string Content, int UserId);

    // Placeholder for getting current user ID - in a real app, this would come from HttpContext.User
    // private int GetCurrentUserId() { /* ... logic to get user ID ... */ return 1; }
}
