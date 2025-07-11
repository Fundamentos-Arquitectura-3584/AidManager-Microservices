using MediatR;

namespace AidManager.Collaborate.Application.Commands;

// Returns bool indicating success
public record DeleteCommentCommand(int CommentId, int UserId) : IRequest<bool>;
