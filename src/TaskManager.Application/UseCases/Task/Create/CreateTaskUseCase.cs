using TaskManager.Application.Validators.CreateValidator;
using TaskManager.Communication.Request.TaskRequest;
using TaskManager.Communication.Response.TaskResponse;
using TaskManager.Domain.Entities;
using TaskManager.Domain.HttpContext;
using TaskManager.Domain.Repositories.ProjectRepositories;
using TaskManager.Domain.Repositories.TaskRepositories;
using TaskManager.Exception.ExceptionBase;

namespace TaskManager.Application.UseCases.Task.Create
{
    public class CreateTaskUseCase : ICreateTaskUseCase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserContext _userContext;
        private readonly CreateTaskRequestValidator _validator;

        public CreateTaskUseCase(
            ITaskRepository taskRepository,
            IProjectRepository projectRepository,
            IUserContext userContext)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _userContext = userContext;
            _validator = new CreateTaskRequestValidator();
        }

        public async Task<CreateTaskResponse> Execute(CreateTaskRequest request)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidateException(errors);
            }

            var currentUserId = _userContext.GetCurrentUserId();

            // Verificar se o projeto existe e pertence ao usuário atual
            var project = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (project is null || project.OwnerId != currentUserId)
            {
                throw new ProjectNotFoundException(request.ProjectId); // <- uso correto
            }

            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                TaskType = request.TaskType,
                CreatedAt = DateTime.UtcNow,
                ProjectId = request.ProjectId,
                AssignedUserId = currentUserId
            };

            await _taskRepository.AddAsync(task);

            return new CreateTaskResponse
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                TaskType = task.TaskType,
                CreatedAt = task.CreatedAt,
                ProjectId = task.ProjectId,
            
            };
        }
    }
}
