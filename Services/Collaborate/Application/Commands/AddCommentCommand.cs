using MediatR;
using AidManager.Collaborate.Application.DTOs; // Assuming CommentDto might be returned

namespace AidManager.Collaborate.Application.Commands;

// Returns the created CommentDto
public record AddCommentCommand(int UserId, string Content, int PostId) : IRequest<CommentDto>;
