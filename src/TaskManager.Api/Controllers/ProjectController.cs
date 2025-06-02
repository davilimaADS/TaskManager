using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.UseCases.Project.Create;
using TaskManager.Application.UseCases.Project.GetAll;
using TaskManager.Communication.Request.ProjectRequest;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ICreateProjectUseCase _createProjectUseCase;
        private readonly IGetAllProjectsUseCase _getAllProjectsUseCase;

        public ProjectController(ICreateProjectUseCase createProjectUseCase, IGetAllProjectsUseCase getAllProjectsUseCase)
        {
            _createProjectUseCase = createProjectUseCase;
            _getAllProjectsUseCase = getAllProjectsUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectRequest request)
        {
            var response = await _createProjectUseCase.Execute(request);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _getAllProjectsUseCase.Execute();
            return Ok(projects);
        }
    }

    }

