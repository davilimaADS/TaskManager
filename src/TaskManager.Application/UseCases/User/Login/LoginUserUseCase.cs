using TaskManager.Application.Validators.UserValidator;
using TaskManager.Communication.Request.UserRequest;
using TaskManager.Communication.Response.UserResponse;
using TaskManager.Domain.Repositories.TokenRepositories;
using TaskManager.Domain.Repositories.UserRepositories;
using TaskManager.Exception.ExceptionBase;

namespace TaskManager.Application.UseCases.User.Login
{
    public class LoginUserUseCase : ILoginUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly LoginUserValidator _validator;

        public LoginUserUseCase(
            IUserRepository userRepository,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _validator = new LoginUserValidator();
        }

        public async Task<LoginUserResponse> Execute(LoginUserRequest request)
        {
            // 1. Validação
            var result = _validator.Validate(request);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidateException(errors);
            }

            // 2. Buscar usuário
            var user = await _userRepository.GetByEmail(request.Email);
            if (user == null)
            {
                throw new ErrorOnValidateException(new List<string> { "Email ou senha inválidos." });
            }

            // 3. Verificar senha
            var senhaCorreta = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!senhaCorreta)
            {
                throw new ErrorOnValidateException(new List<string> { "Email ou senha inválidos." });
            }

            // 4. Gerar token
            var token = _jwtTokenGenerator.GenerateToken(user);

            // 5. Retornar dados
            return new LoginUserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Token = token
            };
        }
    }
}
