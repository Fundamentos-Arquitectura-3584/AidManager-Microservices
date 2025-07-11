using MediatR;

namespace AidManager.Collaborate.Application.Commands;

// Returns bool indicating success
public record DeletePostCommand(int PostId, int UserId) : IRequest<bool>;
