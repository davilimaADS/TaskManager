using System;
using System.Threading.Tasks;
using DotNetTask = System.Threading.Tasks.Task;

namespace TaskManager.Application.UseCases.Project.Delete
{
    public interface IDeleteProjectUseCase
    {
        DotNetTask ExecuteAsync(Guid id);
    }
}