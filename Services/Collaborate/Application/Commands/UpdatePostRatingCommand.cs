using MediatR;
using AidManager.Collaborate.Application.DTOs;

namespace AidManager.Collaborate.Application.Commands;

// This command is for explicitly setting a rating, perhaps by an admin or a specific system process.
// Liking/Unliking will adjust rating implicitly via their respective commands.
// Returns the updated PostDto.
public record UpdatePostRatingCommand(int PostId, int NewRating, int UserId) : IRequest<PostDto>;
