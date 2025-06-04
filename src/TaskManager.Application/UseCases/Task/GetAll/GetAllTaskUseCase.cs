using TaskManager.Communication.Response.TaskResponse;
using TaskManager.Domain.HttpContext;
using TaskManager.Domain.Repositories.ProjectRepositories;
using TaskManager.Domain.Repositories.TaskRepositories;
using TaskManager.Exception.ExceptionBase;

namespace TaskManager.Application.UseCases.Task.GetAll
{
    public class GetAllTaskUseCase : IGetAllTaskUseCase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserContext _userContext;

        public GetAllTaskUseCase(
            ITaskRepository taskRepository,
            IProjectRepository projectRepository,
            IUserContext userContext)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _userContext = userContext;
        }

        public async Task<List<GetAllTaskResponse>> ExecuteAsync(Guid projectId)
        {
            var currentUserId = _userContext.GetCurrentUserId();

            // Verifica se o projeto existe e pertence ao usuário logado
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null || project.OwnerId != currentUserId || project.DeletedAt != null)
            {
                throw new ProjectNotFoundException(projectId);
            }

            // Busca as tasks do projeto
            var tasks = await _taskRepository.GetAllByProjectIdAsync(projectId);

            // Mapeia para DTO
            return tasks.Select(task => new GetAllTaskResponse
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                TaskType = task.TaskType.ToString(),
                CreatedAt = task.CreatedAt,
                ProjectId = task.ProjectId
            }).ToList();
        }
    }
}
