using MediatR;
using AidManager.Collaborate.Application.DTOs; // Assuming you'll return PostDto
using System.Collections.Generic;

namespace AidManager.Collaborate.Application.Queries;

// Returns a list of PostDto that the user has liked
public record GetLikedPostsByUserIdQuery(int UserId) : IRequest<IEnumerable<PostDto>>;
