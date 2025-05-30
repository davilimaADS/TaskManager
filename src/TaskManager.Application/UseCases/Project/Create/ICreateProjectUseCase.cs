using TaskManager.Communication.Request.ProjectRequest;
using TaskManager.Communication.Response.ProjectResponse;

namespace TaskManager.Application.UseCases.Project.Create
{
    public interface ICreateProjectUseCase
    {
        Task<CreateProjectResponse> Execute(CreateProjectRequest request);
    }
}
