using FluentValidation;
using TaskManager.Communication.Request.TaskRequest;
using TaskManager.Communication.Response.TaskResponse;
using TaskManager.Domain.HttpContext;
using TaskManager.Domain.Repositories.ProjectRepositories;
using TaskManager.Domain.Repositories.TaskRepositories;
using TaskManager.Exception.ExceptionBase;

namespace TaskManager.Application.UseCases.Task.Task
{
    public class UpdateTaskUseCase : IUpdateTaskUseCase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IValidator<UpdateTaskRequest> _validator;
        private readonly IUserContext _userContext;

        public UpdateTaskUseCase(
            ITaskRepository taskRepository,
            IProjectRepository projectRepository,
            IValidator<UpdateTaskRequest> validator,
            IUserContext userContext)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _validator = validator;
            _userContext = userContext;
        }

        public async Task<UpdateTaskResponse> ExecuteAsync(Guid projectId, Guid taskId, UpdateTaskRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidateException(errors);
            }

            var userId = _userContext.GetCurrentUserId();

            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null || project.OwnerId != userId)
            {
                throw new ProjectNotFoundException(projectId);
            }

            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null || task.ProjectId != projectId)
            {
                throw new TaskNotFoundException(taskId);
            }

            task.Title = request.Title;
            task.Description = request.Description;
            task.TaskType = request.TaskType;
            task.UpdatedAt = DateTime.UtcNow;

            await _taskRepository.UpdateAsync(task);

            return new UpdateTaskResponse
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                TaskType = task.TaskType,
                UpdatedAt = task.UpdatedAt ?? DateTime.UtcNow
            };
        }
    }
}
