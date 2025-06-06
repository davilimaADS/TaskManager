using TaskManager.Communication.Response.TaskResponse;

namespace TaskManager.Application.UseCases.Task.GetById
{
    public interface IGetTaskByIdUseCase
    {
        Task<GetTaskByIdResponse> ExecuteAsync(Guid id);
    }
}
