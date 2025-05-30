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
    }
}
