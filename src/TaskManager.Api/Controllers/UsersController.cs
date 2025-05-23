using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.UseCases.User.Create;
using TaskManager.Communication.Request.UserRequest;
using TaskManager.Communication.Response.UserResponse;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ICreateUserUseCase _createUserUseCase;

        public UsersController(ICreateUserUseCase createUserUseCase)
        {
            _createUserUseCase = createUserUseCase;
        }

        [HttpPost]
        public async Task<ActionResult<CreateUserResponse>> Create([FromBody] CreateUserRequest request)
        {
            var response = await _createUserUseCase.Execute(request);
            return CreatedAtAction(nameof(Create), new { id = response.Id }, response);
        }
    }
}

