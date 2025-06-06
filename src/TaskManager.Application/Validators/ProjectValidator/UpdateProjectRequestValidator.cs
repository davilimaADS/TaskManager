using FluentValidation;
using TaskManager.Communication.Request.ProjectRequest;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Validators.ProjectValidator
{
    public class UpdateProjectRequestValidator : AbstractValidator<UpdateProjectRequest>
    {

        public UpdateProjectRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("The project name is mandatory.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("The project description is mandatory.");

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Invalid status. Allowed values: InProgress, Completed, Cancelled.");
        }

    }
}
