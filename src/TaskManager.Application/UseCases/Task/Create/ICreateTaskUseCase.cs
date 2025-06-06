using TaskManager.Communication.Request.TaskRequest;
using TaskManager.Communication.Response.TaskResponse;

namespace TaskManager.Application.UseCases.Task.Create
{
    public interface ICreateTaskUseCase
    {
        Task<CreateTaskResponse> Execute(CreateTaskRequest request);
    }
}
