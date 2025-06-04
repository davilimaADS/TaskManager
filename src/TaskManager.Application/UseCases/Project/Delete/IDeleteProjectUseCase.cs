namespace TaskManager.Application.UseCases.Project.Delete
{
    public interface IDeleteProjectUseCase
    {
        Task ExecuteAsync(Guid id);
    }
}
