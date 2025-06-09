using MediatR;
using AidManager.Iam.Application.Interfaces;
using AidManager.Iam.Domain.Entities;
using AidManager.Iam.Application.DTOs;

namespace AidManager.Iam.Application.Queries;

public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, UserDto?>
{
    private readonly IUserRepository _repository;

    public GetUserByUsernameQueryHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserDto?> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.FindByUsernameAsync(request.Username);
        return user == null
            ? null
            : new UserDto(user.Id, user.Username, (int)user.Role);
    }
}
