using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories.ProjectRepositories;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Infrastructure.Repositories.ProjectRepositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Project>> GetAllAsync(Guid userId)
        {
            return await _context.Projects
               .Where(p => p.OwnerId == userId && p.DeletedAt == null)
               .OrderByDescending(p => p.CreatedAt)
               .ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(Guid id)
        {
            return await _context.Projects
           .Include(p => p.Owner)
           .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task UpdateAsync(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }
    }
}
