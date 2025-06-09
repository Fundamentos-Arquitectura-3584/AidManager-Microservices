using MediatR;

namespace AidManager.Iam.Application.Commands;

public record DeleteUserCommand(string Username) : IRequest<bool>;
