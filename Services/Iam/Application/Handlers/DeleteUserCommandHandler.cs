using MediatR;
using AidManager.Iam.Application.Interfaces;
using AidManager.Iam.Domain.Entities;

namespace AidManager.Iam.Application.Commands;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserRepository _repository;

    public DeleteUserCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.FindByUsernameAsync(request.Username);
        if (user == null)
            return false;

        return await _repository.Remove(user);
    }
}
