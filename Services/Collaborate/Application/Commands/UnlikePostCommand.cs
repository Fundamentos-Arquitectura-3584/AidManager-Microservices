using MediatR;

namespace AidManager.Collaborate.Application.Commands;

// Returns bool indicating success
public record UnlikePostCommand(int PostId, int UserId) : IRequest<bool>;
