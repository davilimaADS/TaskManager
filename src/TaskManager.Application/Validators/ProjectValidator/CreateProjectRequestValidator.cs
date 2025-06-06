using FluentValidation;
using TaskManager.Communication.Request.ProjectRequest;

namespace TaskManager.Application.Validators.ProjectValidator
{
    public class CreateProjectRequestValidator : AbstractValidator<CreateProjectRequest>
    {
        public CreateProjectRequestValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("The project name is mandatory.")
               .MaximumLength(100).WithMessage("The project name must be a maximum of 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("The project description must be a maximum of 1000 characters.");
        }
    }
}
