using TaskManager.Communication.Response.TaskResponse;

namespace TaskManager.Application.UseCases.Task.GetAll
{
    public interface IGetAllTaskUseCase
    {
        Task<List<GetAllTaskResponse>> ExecuteAsync(Guid projectId);
    }
}
