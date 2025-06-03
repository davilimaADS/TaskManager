using TaskManager.Communication.Response.ProjectResponse;

namespace TaskManager.Application.UseCases.Project.GetById
{
    public interface IGetProjectByIdUseCase
    {
        Task<GetProjectByIdResponse> ExecuteAsync(Guid id);
    }
}
