using MediatR;
using AidManager.Collaborate.Application.DTOs; // Assuming PostDto might be returned
using System.Collections.Generic;

namespace AidManager.Collaborate.Application.Commands;

// Returns the updated PostDto
public record UpdatePostCommand(
    int Id, // PostId
    string Title,
    string Subject,
    string Description,
    List<string> ImageUrls, // Allows updating images
    int UserId // User performing the update for authorization
) : IRequest<PostDto>;
