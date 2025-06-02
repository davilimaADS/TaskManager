using TaskManager.Communication.Response.ProjectResponse;

namespace TaskManager.Application.UseCases.Project.GetAll
{
    public interface IGetAllProjectsUseCase
    {
        Task<List<GetAllProjectsResponse>> Execute();
    }
}
