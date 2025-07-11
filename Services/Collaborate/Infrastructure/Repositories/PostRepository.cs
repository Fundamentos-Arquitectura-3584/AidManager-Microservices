using AidManager.Collaborate.Application.Interfaces;
using AidManager.Collaborate.Domain.Entities;
using AidManager.Collaborate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly CollaborateDbContext _context;

    public PostRepository(CollaborateDbContext context)
    {
        _context = context;
    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        // Eagerly load comments for a post. This is one way to handle related data.
        // Adjust .Include() statements based on what related data you typically need with a Post.
        return await _context.Posts
            .Include(p => p.Comments) // Assuming Post entity has a List<Comment> Comments property
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        return await _context.Posts
            .Include(p => p.Comments) // Consider if comments are always needed for "all posts" scenarios
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Post>> GetByCompanyIdAsync(int companyId)
    {
        return await _context.Posts
            .Where(p => p.CompanyId == companyId)
            .Include(p => p.Comments)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Post>> GetByUserIdAsync(int userId)
    {
        return await _context.Posts
            .Where(p => p.UserId == userId)
            .Include(p => p.Comments)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<Post> AddAsync(Post post)
    {
        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task UpdateAsync(Post post)
    {
        _context.Entry(post).State = EntityState.Modified;
        // If ImageUrls or Comments are modified, EF Core change tracking should handle it.
        // For complex scenarios (e.g., deleting comments not in the updated list), more logic might be needed here.
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post != null)
        {
            // EF Core will handle cascade deletes for Comments if configured in OnModelCreating.
            // LikedPosts and FavoritePosts related to this post would need to be handled separately
            // if not set up for cascade delete or if they are in different DbContexts/tables.
            // For simplicity, we assume they are handled (e.g. by DB cascade or are not direct FK relations to Post table).
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }
    }
}
