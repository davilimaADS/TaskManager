using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManager.Application.UseCases.User.Create;
using TaskManager.Application.UseCases.User.Login;
using TaskManager.Application.UseCases.User.Profile;
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
        private readonly IGetUserProfileUseCase _getUserProfileUseCase;
        public UsersController(ICreateUserUseCase createUserUseCase, ILoginUserUseCase loginUserUseCase, IGetUserProfileUseCase getUserProfileUseCase)
        {
            _createUserUseCase = createUserUseCase;
            _loginUserUseCase = loginUserUseCase;
            _getUserProfileUseCase = getUserProfileUseCase;
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

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userId, out var guid))
            {
                return Unauthorized();
            }

            var result = await _getUserProfileUseCase.Execute(guid);
            return Ok(result);
        }
    }
}

