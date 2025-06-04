using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Repositories.TaskRepositories
{
    public interface ITaskRepository
    {
        Task AddAsync(TaskItem task);
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task<List<TaskItem>> GetAllByProjectIdAsync(Guid projectId);
        Task UpdateAsync(TaskItem task);
        Task DeleteAsync(TaskItem task);
    }
}
