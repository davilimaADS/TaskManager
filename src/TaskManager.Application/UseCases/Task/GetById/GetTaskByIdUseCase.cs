using TaskManager.Communication.Response.TaskResponse;
using TaskManager.Domain.HttpContext;
using TaskManager.Domain.Repositories.TaskRepositories;
using TaskManager.Exception.ExceptionBase;

namespace TaskManager.Application.UseCases.Task.GetById
{
    public class GetTaskByIdUseCase : IGetTaskByIdUseCase
    {

        private readonly ITaskRepository _taskRepository;
        private readonly IUserContext _userContext;

        public GetTaskByIdUseCase(ITaskRepository taskRepository, IUserContext userContext)
        {
            _taskRepository = taskRepository;
            _userContext = userContext;
        }

        public async Task<GetTaskByIdResponse> ExecuteAsync(Guid id)
        {
            var currentUserId = _userContext.GetCurrentUserId();

            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null || task.Project == null || task.Project.OwnerId != currentUserId)
            {
                throw new TaskNotFoundException(id);
            }

            return new GetTaskByIdResponse
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                TaskType = task.TaskType.ToString(),
                CreatedAt = task.CreatedAt,
                ProjectId = task.ProjectId,
            };
        }
    }
}
