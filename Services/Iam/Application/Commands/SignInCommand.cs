using MediatR;
using AidManager.Iam.Domain.Entities;

public record SignInCommand(string Username, string Password) : IRequest<User?>;
