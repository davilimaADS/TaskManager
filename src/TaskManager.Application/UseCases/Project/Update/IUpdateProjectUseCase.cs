using TaskManager.Communication.Request.ProjectRequest;
using TaskManager.Communication.Response.ProjectResponse;

namespace TaskManager.Application.UseCases.Project.Update
{
    public interface IUpdateProjectUseCase
    {
        Task<UpdateProjectResponse> ExecuteAsync(Guid id, UpdateProjectRequest request);
    }
}
