using MediatR;
using AidManager.Iam.Application.Interfaces;
using AidManager.Iam.Domain.Entities;
using AidManager.Iam.Domain.Enums;

namespace AidManager.Iam.Application.Commands;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IUserRepository _repository;

    public UpdateUserCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.FindByUsernameAsync(request.OldUsername);
        if (user == null)
            return false;

        user.Username = request.Username;
        user.PasswordHash = request.Password;
        user.Role = (UserRole)request.UserRole;
        return await _repository.Update(user);
    }
}
