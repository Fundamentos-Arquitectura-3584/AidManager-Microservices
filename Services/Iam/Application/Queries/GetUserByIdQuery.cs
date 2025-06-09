using MediatR;
using AidManager.Iam.Application.DTOs; // Adjust as needed

namespace AidManager.Iam.Application.Queries;

public record GetUserByIdQuery(int Id) : IRequest<UserDto?>;
