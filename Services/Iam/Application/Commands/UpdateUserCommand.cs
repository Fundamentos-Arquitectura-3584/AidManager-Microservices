namespace AidManager.Iam.Application.Commands;

public record UpdateUserCommand(string OldUsername, string Username, string Password, int UserRole);