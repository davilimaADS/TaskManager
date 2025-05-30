using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.UseCases.Project.Create;
using TaskManager.Communication.Request.ProjectRequest;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ICreateProjectUseCase _createProjectUseCase;

        public ProjectController(ICreateProjectUseCase createProjectUseCase)
        {
            _createProjectUseCase = createProjectUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectRequest request)
        {
            var response = await _createProjectUseCase.Execute(request);
            return Ok(response);
        }
    }
}
