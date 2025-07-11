using AidManager.Collaborate.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Interfaces;

public interface ICommentRepository
{
    Task<Comment?> GetByIdAsync(int id);
    Task<IEnumerable<Comment>> GetByPostIdAsync(int postId);
    Task<Comment> AddAsync(Comment comment);
    Task UpdateAsync(Comment comment);
    Task DeleteAsync(int id);
}
