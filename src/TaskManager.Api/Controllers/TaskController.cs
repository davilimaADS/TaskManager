using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.UseCases.Task.Create;
using TaskManager.Application.UseCases.Task.Delete;
using TaskManager.Application.UseCases.Task.GetAll;
using TaskManager.Application.UseCases.Task.GetById;
using TaskManager.Application.UseCases.Task.Task;
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
        private readonly IUpdateTaskUseCase _updateTaskUseCase;
        private readonly IDeleteTaskUseCase _deleteTaskUseCase;

        public TaskController(ICreateTaskUseCase createTaskUseCase, IGetAllTaskUseCase getAllTaskUseCase, 
            IGetTaskByIdUseCase getTaskByIdUseCase, IUpdateTaskUseCase updateTaskUseCase, 
            IDeleteTaskUseCase deleteTaskUseCase)
        {
            _createTaskUseCase = createTaskUseCase;
            _getAllTaskUseCase = getAllTaskUseCase;
            _getTaskByIdUseCase = getTaskByIdUseCase;
            _updateTaskUseCase = updateTaskUseCase;
            _deleteTaskUseCase = deleteTaskUseCase;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateTaskRequest request)
        {
            var response = await _createTaskUseCase.Execute(request);
            return Ok(response);
        }
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(Guid projectId)
        {
            var tasks = await _getAllTaskUseCase.ExecuteAsync(projectId);
            return Ok(tasks);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<GetTaskByIdResponse>> GetById(Guid id)
        {
            var task = await _getTaskByIdUseCase.ExecuteAsync(id);
            return Ok(task);
        }
        [HttpPut("{taskId}")]
        [Authorize]
        public async Task<ActionResult<UpdateTaskResponse>> UpdateTask(Guid projectId, Guid taskId, [FromBody] UpdateTaskRequest request)
        {
            var response = await _updateTaskUseCase.ExecuteAsync(projectId, taskId, request);
            return Ok(response);
        }
        [HttpDelete("{taskId}")]
        [Authorize]
        public async Task<IActionResult> DeleteTask(Guid taskId)
        {
            await _deleteTaskUseCase.ExecuteAsync(taskId);
            return NoContent(); 
        }
    }
}
