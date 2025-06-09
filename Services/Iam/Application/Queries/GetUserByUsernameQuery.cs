using AidManager.Iam.Application.DTOs;
using MediatR;

namespace AidManager.Iam.Application.Queries;

public record GetUserByUsernameQuery(string Username) : IRequest<UserDto?>;
