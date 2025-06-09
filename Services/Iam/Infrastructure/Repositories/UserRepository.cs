using AidManager.Iam.Application.Interfaces;
using AidManager.Iam.Domain.Entities;
using AidManager.Iam.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly IamDbContext _context;
    public UserRepository(IamDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> FindByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User?> FindByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<IEnumerable<User>?> ListAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<bool> Remove(User user)
    {
        _context.Users.Remove(user);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> Update(User user)
    {
        _context.Users.Update(user);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }
}