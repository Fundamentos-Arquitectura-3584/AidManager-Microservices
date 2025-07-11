using MediatR;

namespace AidManager.Collaborate.Application.Commands;

// Returns bool indicating success (e.g., if like was successfully registered)
// This will also trigger an update to the Post's rating.
public record LikePostCommand(int PostId, int UserId) : IRequest<bool>;
