using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories.TaskRepositories;
using TaskManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Infrastructure.Repositories.TaskRepositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(Guid id)
        {
            return await _context.Tasks
                .Include(t => t.Project) // incluir projeto se precisar
                .FirstOrDefaultAsync(t => t.Id == id && t.DeletedAt == null);
        }

        public async Task<List<TaskItem>> GetAllByProjectIdAsync(Guid projectId)
        {
            return await _context.Tasks
                .Where(t => t.ProjectId == projectId && t.DeletedAt == null)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task UpdateAsync(TaskItem task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TaskItem task)
        {
            task.DeletedAt = DateTime.UtcNow;
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }
    }
}
