using AidManager.Collaborate.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Interfaces;

public interface IProjectRepository
{
    Task<Project?> GetByIdAsync(int id);
    Task<IEnumerable<Project>> GetAllAsync();
    // Add other specific query methods if needed, e.g., GetByCompanyIdAsync
    Task<Project> AddAsync(Project project);
    Task UpdateAsync(Project project);
    Task DeleteAsync(int id);
}
