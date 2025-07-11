using AidManager.Collaborate.Application.Interfaces;
using AidManager.Collaborate.Domain.Entities;
using AidManager.Collaborate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Infrastructure.Repositories;

public class LikedPostRepository : ILikedPostRepository
{
    private readonly CollaborateDbContext _context;

    public LikedPostRepository(CollaborateDbContext context)
    {
        _context = context;
    }

    public async Task<LikedPost?> GetAsync(int userId, int postId)
    {
        return await _context.LikedPosts.FindAsync(userId, postId);
    }

    public async Task<IEnumerable<LikedPost>> GetByUserIdAsync(int userId)
    {
        return await _context.LikedPosts
            .Where(lp => lp.UserId == userId)
            .ToListAsync();
    }

    public async Task AddAsync(LikedPost likedPost)
    {
        var existing = await _context.LikedPosts.FindAsync(likedPost.UserId, likedPost.PostId);
        if (existing == null)
        {
            await _context.LikedPosts.AddAsync(likedPost);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int userId, int postId)
    {
        var likedPost = await _context.LikedPosts.FindAsync(userId, postId);
        if (likedPost != null)
        {
            _context.LikedPosts.Remove(likedPost);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<int> GetLikesCountForPostAsync(int postId)
    {
        return await _context.LikedPosts.CountAsync(lp => lp.PostId == postId);
    }
}
