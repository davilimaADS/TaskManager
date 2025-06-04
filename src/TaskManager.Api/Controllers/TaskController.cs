using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.UseCases.Task.Create;
using TaskManager.Communication.Request.TaskRequest;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ICreateTaskUseCase _createTaskUseCase;

        public TaskController(ICreateTaskUseCase createTaskUseCase)
        {
            _createTaskUseCase = createTaskUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskRequest request)
        {
            var response = await _createTaskUseCase.Execute(request);
            return Ok(response);
        }
  }
}
