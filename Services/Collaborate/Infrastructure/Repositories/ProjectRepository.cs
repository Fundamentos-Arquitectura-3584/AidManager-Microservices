using AidManager.Collaborate.Application.Interfaces;
using AidManager.Collaborate.Domain.Entities;
using AidManager.Collaborate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly CollaborateDbContext _context;

    public ProjectRepository(CollaborateDbContext context)
    {
        _context = context;
    }

    public async Task<Project?> GetByIdAsync(int id)
    {
        return await _context.Projects
            // .Include(p => p.Events) // If you want to load related events, ensure Project entity has List<Event> Events property
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        return await _context.Projects
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<Project> AddAsync(Project project)
    {
        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();
        return project;
    }

    public async Task UpdateAsync(Project project)
    {
        _context.Entry(project).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project != null)
        {
            // Consider implications: deleting a project might require deleting associated events, tasks, etc.
            // This can be handled by database cascade rules or explicitly here.
            // For now, direct deletion of the project entity.
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
    }
}
