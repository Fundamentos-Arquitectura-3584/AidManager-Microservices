namespace AidManager.Iam.Application.Commands;

public record SignUpCommand(string Username, string Password, int UserRole);