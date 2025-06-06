using DotNetTask = System.Threading.Tasks.Task;

namespace TaskManager.Application.UseCases.Task.Delete
{
    public interface IDeleteTaskUseCase
    {
        DotNetTask ExecuteAsync( Guid taskId);
    }
}
