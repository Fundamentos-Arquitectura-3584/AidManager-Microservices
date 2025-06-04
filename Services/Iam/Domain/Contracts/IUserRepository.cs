using System.Threading.Tasks;
using AidManager.Iam.Domain.Entities;

namespace AidManager.Iam.Domain.Contracts;

public interface IUserRepository
{
    Task<User?> FindByUsernameAsync(string username);
    Task<User?> FindByIdAsync(int id);
    Task AddAsync(User user);
}