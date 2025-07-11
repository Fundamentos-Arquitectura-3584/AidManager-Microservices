using AidManager.Collaborate.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Interfaces;

public interface ILikedPostRepository
{
    Task<LikedPost?> GetAsync(int userId, int postId);
    Task<IEnumerable<LikedPost>> GetByUserIdAsync(int userId);
    Task AddAsync(LikedPost likedPost);
    Task DeleteAsync(int userId, int postId);
    Task<int> GetLikesCountForPostAsync(int postId); // For updating Post.Rating
}
