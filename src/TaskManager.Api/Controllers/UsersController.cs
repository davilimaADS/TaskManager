using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.UseCases.User.Create;
using TaskManager.Application.UseCases.User.Login;
using TaskManager.Communication.Request.UserRequest;
using TaskManager.Communication.Response.UserResponse;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ICreateUserUseCase _createUserUseCase;
        private readonly ILoginUserUseCase _loginUserUseCase;
        public UsersController(ICreateUserUseCase createUserUseCase, ILoginUserUseCase loginUserUseCase)
        {
            _createUserUseCase = createUserUseCase;
            _loginUserUseCase = loginUserUseCase;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<CreateUserResponse>> Create([FromBody] CreateUserRequest request)
        {
            var response = await _createUserUseCase.Execute(request);
            return CreatedAtAction(nameof(Create), new { id = response.Id }, response);

        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginUserResponse>> Login([FromBody] LoginUserRequest request)
        {
            var response = await _loginUserUseCase.Execute(request);
            return Ok(response);
        }

    }
}

