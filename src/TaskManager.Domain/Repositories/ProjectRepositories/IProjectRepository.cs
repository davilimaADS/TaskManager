using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Repositories.ProjectRepositories
{
    public interface IProjectRepository
    {
        Task AddAsync(Project project);
    }
}
