
using TaskManager.Domain.HttpContext;
using TaskManager.Domain.Repositories.ProjectRepositories;
using TaskManager.Exception.ExceptionBase;

namespace TaskManager.Application.UseCases.Project.Delete
{
    public class DeleteProjectUseCase : IDeleteProjectUseCase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserContext _userContext;

        public DeleteProjectUseCase(IProjectRepository projectRepository, IUserContext userContext)
        {
            _projectRepository = projectRepository;
            _userContext = userContext;
        }



        public async Task ExecuteAsync(Guid id)
        {
            var userId = _userContext.GetCurrentUserId();

            var project = await _projectRepository.GetByIdAsync(id);

            if (project is null || project.OwnerId != userId || project.DeletedAt != null)
            {
                throw new ProjectNotFoundException(id);
            }

            project.DeletedAt = DateTime.UtcNow;

            await _projectRepository.DeleteAsync(project);
        }
    }
}
