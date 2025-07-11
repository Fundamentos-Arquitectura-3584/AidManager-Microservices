using AidManager.Collaborate.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Interfaces;

public interface IFavoritePostRepository
{
    Task<FavoritePost?> GetAsync(int userId, int postId);
    Task<IEnumerable<FavoritePost>> GetByUserIdAsync(int userId);
    Task AddAsync(FavoritePost favoritePost);
    Task DeleteAsync(int userId, int postId);
}
