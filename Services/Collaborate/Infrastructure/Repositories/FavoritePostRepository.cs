using AidManager.Collaborate.Application.Interfaces;
using AidManager.Collaborate.Domain.Entities;
using AidManager.Collaborate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Infrastructure.Repositories;

public class FavoritePostRepository : IFavoritePostRepository
{
    private readonly CollaborateDbContext _context;

    public FavoritePostRepository(CollaborateDbContext context)
    {
        _context = context;
    }

    public async Task<FavoritePost?> GetAsync(int userId, int postId)
    {
        return await _context.FavoritePosts.FindAsync(userId, postId);
    }

    public async Task<IEnumerable<FavoritePost>> GetByUserIdAsync(int userId)
    {
        return await _context.FavoritePosts
            .Where(fp => fp.UserId == userId)
            .ToListAsync();
        // If you need to include Post details, you'd do a join or include here,
        // but the repository returns FavoritePost entities.
        // The handler would then fetch Post details if needed for a PostDto.
    }

    public async Task AddAsync(FavoritePost favoritePost)
    {
        // Check if it already exists to prevent duplicate primary key issues if not handled by DB constraints.
        // However, FindAsync before AddAsync is often redundant if the PK is well-defined.
        // The context tracks entities, and SaveChanges will fail if a PK violation occurs.
        var existing = await _context.FavoritePosts.FindAsync(favoritePost.UserId, favoritePost.PostId);
        if (existing == null)
        {
            await _context.FavoritePosts.AddAsync(favoritePost);
            await _context.SaveChangesAsync();
        }
        // else: it already exists, decide if an exception should be thrown or just ignore.
        // For idempotent operations, ignoring might be fine.
    }

    public async Task DeleteAsync(int userId, int postId)
    {
        var favoritePost = await _context.FavoritePosts.FindAsync(userId, postId);
        if (favoritePost != null)
        {
            _context.FavoritePosts.Remove(favoritePost);
            await _context.SaveChangesAsync();
        }
    }
}
