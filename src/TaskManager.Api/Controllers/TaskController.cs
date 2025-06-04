using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.UseCases.Task.Create;
using TaskManager.Application.UseCases.Task.GetAll;
using TaskManager.Application.UseCases.Task.GetById;
using TaskManager.Communication.Request.TaskRequest;
using TaskManager.Communication.Response.TaskResponse;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ICreateTaskUseCase _createTaskUseCase;
        private readonly IGetAllTaskUseCase _getAllTaskUseCase;
        private readonly IGetTaskByIdUseCase _getTaskByIdUseCase;

        public TaskController(ICreateTaskUseCase createTaskUseCase, IGetAllTaskUseCase getAllTaskUseCase, 
            IGetTaskByIdUseCase getTaskByIdUseCase)
        {
            _createTaskUseCase = createTaskUseCase;
            _getAllTaskUseCase = getAllTaskUseCase;
            _getTaskByIdUseCase = getTaskByIdUseCase;
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
        [HttpGet("{id}")]
        public async Task<ActionResult<GetTaskByIdResponse>> GetById(Guid id)
        {
            var task = await _getTaskByIdUseCase.ExecuteAsync(id);
            return Ok(task);
        }
    }
}
