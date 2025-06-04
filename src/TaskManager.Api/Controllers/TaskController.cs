using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.UseCases.Task.Create;
using TaskManager.Application.UseCases.Task.GetAll;
using TaskManager.Communication.Request.TaskRequest;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ICreateTaskUseCase _createTaskUseCase;
        private readonly IGetAllTaskUseCase _getAllTaskUseCase;

        public TaskController(ICreateTaskUseCase createTaskUseCase, IGetAllTaskUseCase getAllTaskUseCase)
        {
            _createTaskUseCase = createTaskUseCase;
            _getAllTaskUseCase = getAllTaskUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskRequest request)
        {
            var response = await _createTaskUseCase.Execute(request);
            return Ok(response);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll(Guid projectId)
        {
            var tasks = await _getAllTaskUseCase.ExecuteAsync(projectId);
            return Ok(tasks);
        }
    }
}
