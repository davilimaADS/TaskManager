using FluentValidation;
using TaskManager.Communication.Request.UserRequest;
using TaskManager.Exception.ExceptionBase;

namespace TaskManager.Application.Validators.UserValidators
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must be at most 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(150).WithMessage("Email must be at most 150 characters.")
                .Must(email => email.EndsWith("@gmail.com")).WithMessage("Email must be a Gmail address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.");
        }

        public void ValidateAndThrow(CreateUserRequest request)
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
