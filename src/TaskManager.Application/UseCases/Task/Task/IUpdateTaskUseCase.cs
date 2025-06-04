using TaskManager.Communication.Request.TaskRequest;
using TaskManager.Communication.Response.TaskResponse;

namespace TaskManager.Application.UseCases.Task.Task
{
    public interface IUpdateTaskUseCase
    {
        Task<UpdateTaskResponse> ExecuteAsync(Guid projectId, Guid taskId, UpdateTaskRequest request);
    }
}
