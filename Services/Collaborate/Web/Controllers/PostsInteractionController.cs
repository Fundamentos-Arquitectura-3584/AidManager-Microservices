using MediatR;
using Microsoft.AspNetCore.Mvc;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.Queries;
using AidManager.Collaborate.Application.DTOs; // For PostDto if returned
using System.Collections.Generic;
using System.Threading.Tasks;
// using System.Security.Claims; // For real user ID

namespace AidManager.Collaborate.Web.Controllers;

[ApiController]
[Route("api/collaborate/posts")] // Base route related to posts
public class PostsInteractionController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostsInteractionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // POST: api/collaborate/posts/{postId}/favorite
    [HttpPost("{postId}/favorite")]
    public async Task<IActionResult> FavoritePost(int postId)
    {
        // int userId = GetCurrentUserId(); // From auth token
        int placeholderUserId = 1; // Placeholder
        var command = new FavoritePostCommand(postId, placeholderUserId);
        var success = await _mediator.Send(command);
        return success ? Ok(new { Message = "Post favorited successfully." }) : BadRequest(new { Message = "Failed to favorite post. It might not exist or is already favorited." });
    }

    // DELETE: api/collaborate/posts/{postId}/favorite
    [HttpDelete("{postId}/favorite")]
    public async Task<IActionResult> RemoveFavoritePost(int postId)
    {
        // int userId = GetCurrentUserId();
        int placeholderUserId = 1; // Placeholder
        var command = new RemoveFavoritePostCommand(postId, placeholderUserId);
        var success = await _mediator.Send(command);
        return success ? NoContent() : NotFound(new { Message = "Favorite entry not found or could not be removed." });
    }

    // GET: api/collaborate/users/{userId}/favorites
    [HttpGet("/api/collaborate/users/{userId}/favorites")] // Specific route for user's favorites
    public async Task<ActionResult<IEnumerable<PostDto>>> GetFavoritePostsByUserId(int userId)
    {
        // In a real app, you might restrict this so users can only see their own favorites,
        // or admins can see anyone's. The `userId` parameter here implies looking up any user.
        // int currentUserId = GetCurrentUserId();
        // if (currentUserId != userId && !User.IsInRole("Admin")) return Forbid();

        var query = new GetFavoritePostsByUserIdQuery(userId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    // POST: api/collaborate/posts/{postId}/like
    [HttpPost("{postId}/like")]
    public async Task<IActionResult> LikePost(int postId)
    {
        // int userId = GetCurrentUserId();
        int placeholderUserId = 1; // Placeholder
        var command = new LikePostCommand(postId, placeholderUserId);
        var success = await _mediator.Send(command);
        // Consider returning the updated post or its new like count.
        return success ? Ok(new { Message = "Post liked successfully." }) : BadRequest(new { Message = "Failed to like post. It might not exist or is already liked." });
    }

    // DELETE: api/collaborate/posts/{postId}/like (as Unlike)
    [HttpDelete("{postId}/like")] // Using same endpoint with DELETE for "unlike"
    public async Task<IActionResult> UnlikePost(int postId)
    {
        // int userId = GetCurrentUserId();
        int placeholderUserId = 1; // Placeholder
        var command = new UnlikePostCommand(postId, placeholderUserId);
        var success = await _mediator.Send(command);
        return success ? NoContent() : NotFound(new { Message = "Like entry not found or could not be removed." });
    }

    // GET: api/collaborate/users/{userId}/likes
    [HttpGet("/api/collaborate/users/{userId}/likes")] // Specific route for user's liked posts
    public async Task<ActionResult<IEnumerable<PostDto>>> GetLikedPostsByUserId(int userId)
    {
        // Similar authorization considerations as GetFavoritePostsByUserId
        // int currentUserId = GetCurrentUserId();
        // if (currentUserId != userId && !User.IsInRole("Admin")) return Forbid();

        var query = new GetLikedPostsByUserIdQuery(userId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    // private int GetCurrentUserId()
    // {
    //     // var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    //     // return userIdClaim != null ? int.Parse(userIdClaim) : 0; // Or throw if not found for protected endpoints
    //     return 1; // Placeholder
    // }
}
