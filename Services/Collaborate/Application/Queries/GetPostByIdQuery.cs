using MediatR;
using AidManager.Collaborate.Application.DTOs;

namespace AidManager.Collaborate.Application.Queries;

public record GetPostByIdQuery(int PostId) : IRequest<PostDto?>;
