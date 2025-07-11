using AidManager.Collaborate.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Interfaces;

public interface IPostRepository
{
    Task<Post?> GetByIdAsync(int id);
    Task<IEnumerable<Post>> GetAllAsync(); // Consider pagination for real-world scenarios
    Task<IEnumerable<Post>> GetByCompanyIdAsync(int companyId);
    Task<IEnumerable<Post>> GetByUserIdAsync(int userId);
    Task<Post> AddAsync(Post post);
    Task UpdateAsync(Post post);
    Task DeleteAsync(int id);
    // Task<IEnumerable<Post>> GetLikedPostsByUserIdAsync(int userId); // This might be better handled by a query that joins Post and LikedPost
    // Task<IEnumerable<Post>> GetFavoritePostsByUserIdAsync(int userId); // Similar to liked posts
}
