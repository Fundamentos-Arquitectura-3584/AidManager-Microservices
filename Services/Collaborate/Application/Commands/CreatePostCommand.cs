using MediatR;
using AidManager.Collaborate.Application.DTOs; // Assuming PostDto might be returned
using System.Collections.Generic;

namespace AidManager.Collaborate.Application.Commands;

// Returns the created PostDto
public record CreatePostCommand(
    string Title,
    string Subject,
    string Description,
    int CompanyId,
    int UserId,
    List<string> ImageUrls
) : IRequest<PostDto>;
