using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.UseCases.Project.Create;
using TaskManager.Application.UseCases.Project.GetAll;
using TaskManager.Application.UseCases.Project.GetById;
using TaskManager.Application.UseCases.Project.Update;
using TaskManager.Communication.Request.ProjectRequest;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ICreateProjectUseCase _createProjectUseCase;
        private readonly IGetAllProjectsUseCase _getAllProjectsUseCase;
        private readonly IGetProjectByIdUseCase _getProjectByIdUseCase;
        private readonly IUpdateProjectUseCase _updateProjectUseCase;

        public ProjectController(ICreateProjectUseCase createProjectUseCase, 
            IGetAllProjectsUseCase getAllProjectsUseCase, IGetProjectByIdUseCase getProjectByIdUseCase, 
            IUpdateProjectUseCase updateProjectUseCase)
        {
            _createProjectUseCase = createProjectUseCase;
            _getAllProjectsUseCase = getAllProjectsUseCase;
            _getProjectByIdUseCase = getProjectByIdUseCase;
            _updateProjectUseCase = updateProjectUseCase;
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

        [HttpGet("{id}")] 
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _getProjectByIdUseCase.ExecuteAsync(id);
            return Ok(response);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProjectRequest request)
        {
            var response = await _updateProjectUseCase.ExecuteAsync(id, request);
            return Ok(response);
        }
    }
}

