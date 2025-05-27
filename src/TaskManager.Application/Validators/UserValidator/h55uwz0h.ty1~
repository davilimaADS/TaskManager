using FluentValidation;
using System.Data;
using TaskManager.Communication.Request.UserRequest;
using TaskManager.Exception.ExceptionBase;

namespace TaskManager.Application.Validators.UserValidator
{
    public class LoginUserValidator : AbstractValidator<LoginUserRequest>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("Email is required.")
                 .EmailAddress().WithMessage("Invalid email format.")
                 .Matches(@"@gmail\.com$").WithMessage("Email must be a Gmail address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");
        }

        public void ValidateAndThrow(LoginUserRequest request)
        {
            var validationResult = Validate(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidateException(errors);
            }
        }
    }
}
