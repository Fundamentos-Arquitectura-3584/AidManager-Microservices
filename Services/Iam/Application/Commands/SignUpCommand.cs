using MediatR;
using AidManager.Iam.Domain.Entities; // Adjust as needed

namespace AidManager.Iam.Application.Commands;

public record SignUpCommand(string Username, string Password, int UserRole) : IRequest<User>;
