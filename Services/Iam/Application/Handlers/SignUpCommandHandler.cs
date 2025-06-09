using MediatR;
using AidManager.Iam.Application.Interfaces;
using AidManager.Iam.Domain.Entities;
using AidManager.Iam.Domain.Enums;

namespace AidManager.Iam.Application.Commands;

public class SignUpCommandHandler : IRequestHandler<SignUpCommand, User>
{
    private readonly IUserRepository _repository;

    public SignUpCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<User> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Username = request.Username,
            PasswordHash = request.Password,
            Role = (UserRole)request.UserRole
        };

        await _repository.AddAsync(user);
        return user;
    }
}
