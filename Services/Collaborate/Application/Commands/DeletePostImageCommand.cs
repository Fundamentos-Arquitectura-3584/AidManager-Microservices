using MediatR;

namespace AidManager.Collaborate.Application.Commands;

// Returns bool indicating success
public record DeletePostImageCommand(int PostId, string ImageUrl, int UserId) : IRequest<bool>;
