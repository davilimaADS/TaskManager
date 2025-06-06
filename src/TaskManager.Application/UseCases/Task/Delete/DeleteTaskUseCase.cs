using TaskManager.Domain.HttpContext;
using TaskManager.Domain.Repositories.TaskRepositories;
using TaskManager.Exception.ExceptionBase;
using DotNetTask = System.Threading.Tasks.Task;
namespace TaskManager.Application.UseCases.Task.Delete
{
    public class DeleteTaskUseCase : IDeleteTaskUseCase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserContext _userContext;

        public DeleteTaskUseCase(ITaskRepository taskRepository, IUserContext userContext)
        {
            _taskRepository = taskRepository;
            _userContext = userContext;
        }

        public async DotNetTask ExecuteAsync(Guid taskId)
        {
            var userId = _userContext.GetCurrentUserId();

            var task = await _taskRepository.GetByIdAsync(taskId);

            if (task == null)
                throw new TaskNotFoundException(taskId);

            if (task.AssignedUserId != userId && task.Project.OwnerId != userId)
            {
                throw new UnauthorizedAccessException("User is not allowed to delete this task.");
            }

            await _taskRepository.DeleteAsync(task);
        }
    }
}
