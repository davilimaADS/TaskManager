using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Repositories.ProjectRepositories
{
    public interface IProjectRepository
    {
        Task AddAsync(Project project);
        Task<List<Project>> GetAllAsync( Guid userId);
        Task<Project?> GetByIdAsync(Guid id);
        Task UpdateAsync(Project project);
        Task DeleteAsync(Project project);
    }
}
